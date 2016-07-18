using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KST.Util;
using KST.DTO;
using KST.Model;
using Newtonsoft.Json;
using KST.Core;
using KST.Service;

namespace KST.Web.Controllers
{
    /// <summary>
    /// 官网主页控制器
    /// </summary>
    public class HomeController : Controller
    {
        private const string VIEW_INDEX = "index";
        private const string VIEW_DOWNLOAD = "download";
        private const string VIEW_DOCUMENT = "document";
        private const string VIEW_ABOUT = "about";
        private const string VIEW_LOGIN = "login";
        private const string VIEW_REG = "reg";

        private AgencyDataService agencyDataService = ServiceFactory.Instance.AgencyDataService;
        private ItemDataService itemDataService = ServiceFactory.Instance.ItemDataService;
        private SecurityService securityService = ServiceFactory.Instance.SecurityService;
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(HomeController));

        #region View

        /// <summary>
        /// 首页页面
        /// </summary>
        public ActionResult Index()
        {
            return View(VIEW_INDEX);
        }

        /// <summary>
        /// 产品下载页面
        /// </summary>
        public ActionResult Download()
        {
            return View(VIEW_DOWNLOAD);
        }

        /// <summary>
        /// 文档页面
        /// </summary>
        public ActionResult Document()
        {
            return View(VIEW_DOCUMENT);
        }

        /// <summary>
        /// 关于页面
        /// </summary>
        public ActionResult About()
        {
            return View(VIEW_ABOUT);
        }

        /// <summary>
        /// 登录页面
        /// </summary>
        public ActionResult Login()
        {
            string tipMsg = TempData[Constant.LOG_TIP_VIEW_AND_TEMP_KEY] == null ? string.Empty : TempData[Constant.LOG_TIP_VIEW_AND_TEMP_KEY].ToString();
            ViewData[Constant.LOG_TIP_VIEW_AND_TEMP_KEY] = tipMsg;

            return View(VIEW_LOGIN);
        }

        /// <summary>
        /// 注册页面
        /// </summary>
        public ActionResult Reg()
        {
            return View(VIEW_REG);
        }

        #endregion

        #region Handler

        /// <summary>
        /// 获取短信验证码
        /// </summary>
        [HttpPost]
        public ActionResult GetCaptcha()
        {
            log.Debug(Constant.DEBUG_START);

            string codeTypeString = ApiQueryUtil.QueryArgByPost("code_type");
            string phone = ApiQueryUtil.QueryArgByPost("phone");
            string ip = Request.UserHostAddress;

            ServiceInvokeDTO result = null;
            try
            {
                if (RegExpUtil.IsMobile(phone))
                {
                    CaptchaCodeType type = (CaptchaCodeType)Convert.ToInt32(codeTypeString);
                    switch (type)
                    {
                        case CaptchaCodeType.RegCode: result = securityService.SendRegCaptcha(phone, ip); break;
                        case CaptchaCodeType.GetPwdCode: result = securityService.SendGetPasswordCaptcha(phone, ip); break;
                        default: result = result = new ServiceInvokeDTO(InvokeCode.SMS_UNKNOW_CODE_TYPE_ERROR); break;
                    }
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_ARG_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INNER_ERROR);
            }

            string json = JsonConvert.SerializeObject(result, Formatting.Indented, Constant.TIME_CONVERTER);
            log.Debug(Constant.DEBUG_END);

            return Content(json, Constant.JSON_MIME_TYPE);
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        [HttpPost]
        public ActionResult RegEx()
        {
            log.Debug(Constant.DEBUG_START);

            string phone = ApiQueryUtil.QueryArgByPost("phone");
            string captcha = ApiQueryUtil.QueryArgByPost("captcha");
            string agencyName = ApiQueryUtil.QueryArgByPost("agency_name");
            string chineseName = ApiQueryUtil.QueryArgByPost("chinese_name");
            string password = ApiQueryUtil.QueryArgByPost("pwd");

            ServiceInvokeDTO result = null;
            try
            {
                result = agencyDataService.AgencyReg(phone, captcha, agencyName, chineseName, password);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INNER_ERROR);
            }

            string json = JsonConvert.SerializeObject(result, Formatting.Indented, Constant.TIME_CONVERTER);
            log.Debug(Constant.DEBUG_END);

            return Content(json, Constant.JSON_MIME_TYPE);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        [HttpPost]
        public ActionResult LoginEx()
        {
            log.Debug(Constant.DEBUG_START);

            string userName = ApiQueryUtil.QueryArgByPost("user_name");
            string password = ApiQueryUtil.QueryArgByPost("pwd_hidden");

            try
            {
                // Write cookies
                HttpCookie cookie = Request.Cookies[Constant.COOKIE_NAME];
                if (cookie == null)
                {
                    cookie = new HttpCookie(Constant.COOKIE_NAME);
                    cookie.Expires = DateTime.Now.AddDays(Constant.COOKIE_EXPIRES_DAY);
                    cookie.Values.Add(Constant.COOKIE_KEY_USER_NAME, userName);
                    Response.Cookies.Add(cookie);
                }
                else
                {
                    cookie.Values[Constant.COOKIE_KEY_USER_NAME] = userName;
                    Response.Cookies.Set(cookie);
                }

                ServiceInvokeDTO<AgencyAdminDTO> loginResult = agencyDataService.AdminLogin(userName, password);
                if (loginResult != null && loginResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    // Write session
                    Session[Constant.SESSION_KEY_ADMIN] = loginResult.Data;

                    int courseID = itemDataService.GetAgencyCourses(loginResult.Data.Agency.ID).Data[0].ID;
                    if (cookie != null && cookie[Constant.COOKIE_KEY_COURSE_ID] != null)
                    {
                        courseID = Convert.ToInt32(cookie[Constant.COOKIE_KEY_COURSE_ID]);
                    }
                    Session[Constant.SESSION_KEY_COURSE] = itemDataService.GetCourseByID(courseID).Data;

                    // To dashboard
                    if (loginResult.Data.Level == AdminLevel.AgencyCreatorAdmin)
                    {
                        return RedirectToAction("index", "agency");
                    }
                    else
                    {
                        return RedirectToAction("index", "item");
                    }
                }
                else
                {
                    //Pass result string to login action.
                    TempData[Constant.LOG_TIP_VIEW_AND_TEMP_KEY] = loginResult.Message;
                    return RedirectToAction("login", "home");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return View("~/Views/Shared/error.cshtml");
            }
        }

        /// <summary>
        /// 用户注销
        /// </summary>
        [HttpGet]
        public ActionResult Logout()
        {
            log.Debug(Constant.DEBUG_START);

            try
            {
                // Remove session
                if (Session[Constant.SESSION_KEY_ADMIN] != null)
                {
                    Session[Constant.SESSION_KEY_ADMIN] = null;
                }
                if (Session[Constant.SESSION_KEY_COURSE] != null)
                {
                    Session[Constant.SESSION_KEY_COURSE] = null;
                }
                return RedirectToAction("login", "home");
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return View("~/Views/Shared/error.cshtml");
            }
        }

        /// <summary>
        /// 用户找回密码
        /// </summary>
        [HttpPost]
        public ActionResult SetNewPwd()
        {
            log.Debug(Constant.DEBUG_START);

            string phone = ApiQueryUtil.QueryArgByPost("phone");
            string captcha = ApiQueryUtil.QueryArgByPost("captcha");
            string newpwd = ApiQueryUtil.QueryArgByPost("pwd");

            ServiceInvokeDTO result = null;
            try
            {
                result = agencyDataService.ResetAdminPassword(phone, captcha, newpwd);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INNER_ERROR);
            }

            string json = JsonConvert.SerializeObject(result, Formatting.Indented, Constant.TIME_CONVERTER);
            log.Debug(Constant.DEBUG_END);

            return Content(json, Constant.JSON_MIME_TYPE);
        }

        #endregion
    }
}
