using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using KST.Core;
using KST.DTO;
using KST.Model;
using KST.Service;
using KST.Util;


namespace KST.API.Controllers
{
    /// <summary>
    /// 用户账号实体Controller
    /// </summary>
    public class UserController : ApiController
    {
        private SecurityService securityService = ServiceFactory.Instance.SecurityService;
        private AgencyDataService userService = ServiceFactory.Instance.UserService;
        private SmsService smsService = ServiceFactory.Instance.SmsService;
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(UserController));

        [HttpGet]
        [HttpPost]
        public HttpResponseMessage Index()
        {
            HttpResponseMessage response = null;
            try
            {
                string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
                if (!string.IsNullOrEmpty(cmd))
                {
                    switch (cmd.ToLower())
                    {
                        case Cmd.USER_GET_BY_ID: response = GetUserByID(Request); break;
                        case Cmd.USER_GET_BY_PHONE: response = GetUserByPhone(Request); break;
                        case Cmd.USER_GET_BY_EMAIL: response = GetUserByEmail(Request); break;
                        case Cmd.USER_REGISTER: response = Register(Request); break;
                        case Cmd.USER_LOGIN: response = Login(Request); break;
                        case Cmd.USER_UPDATE_PASSWORD: response = UpdatePassword(Request); break;
                        case Cmd.USER_SET_NEW_PASSWORD: response = SetNewPassword(Request); break;
                        default: response = Request.CreateResponse(HttpStatusCode.OK, new ServiceInvokeDTO(InvokeCode.SYS_UNKNOW_CMD)); break;
                    }
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.OK, new ServiceInvokeDTO(InvokeCode.SYS_UNKNOW_CMD));
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                response = Request.CreateResponse(HttpStatusCode.OK, new ServiceInvokeDTO(InvokeCode.SYS_INNER_ERROR));
            }
            return response;
        }

        /// <summary>
        /// 根据主键ID获取用户
        /// </summary>
        private HttpResponseMessage GetUserByID(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string idString = ApiQueryUtil.QueryArgByGet("user_id");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd},
                { Constant.HTTP_HEADER_RANDOM, random},
                { "user_id", idString }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO<UserDTO> result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    result = userService.GetUserByID(Convert.ToInt32(idString));
                }
                else
                {
                    result = new ServiceInvokeDTO<UserDTO>(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<UserDTO>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 根据手机号码获取用户
        /// </summary>
        private HttpResponseMessage GetUserByPhone(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string phone = ApiQueryUtil.QueryArgByGet("phone");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd},
                { Constant.HTTP_HEADER_RANDOM, random},
                { "phone", phone }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO<UserDTO> result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    result = userService.GetUserByPhone(phone);
                }
                else
                {
                    result = new ServiceInvokeDTO<UserDTO>(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<UserDTO>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 根据邮箱获取用户
        /// </summary>
        private HttpResponseMessage GetUserByEmail(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string email = ApiQueryUtil.QueryArgByGet("email");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd},
                { Constant.HTTP_HEADER_RANDOM, random},
                { "email", email }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO<UserDTO> result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    result = userService.GetUserByEmail(email);
                }
                else
                {
                    result = new ServiceInvokeDTO<UserDTO>(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<UserDTO>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 注册账号
        /// </summary>
        private HttpResponseMessage Register(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string phone = ApiQueryUtil.QueryArgByPost("phone");
            string code = ApiQueryUtil.QueryArgByPost("code");
            string nickName = ApiQueryUtil.QueryArgByPost("nick_name");
            string password = ApiQueryUtil.QueryArgByPost("pwd");
            string regTypeString = ApiQueryUtil.QueryArgByPost("reg_type");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd },
                { Constant.HTTP_HEADER_RANDOM, random },
                { "phone", phone },
                { "code", code },
                { "nick_name", nickName },
                { "pwd", password },
                { "reg_type", regTypeString }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO<UserDTO> result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    // Check captcha
                    ServiceInvokeDTO captchaResult = securityService.CheckCaptcha(Service.Constant.CAPTCHA_TYPE_REG, phone, code);
                    if (captchaResult != null && captchaResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                    {
                        User user = new User();
                        user.Phone = phone;
                        user.Password = password;
                        user.NickName = nickName;
                        user.RegisterType = Convert.ToInt32(regTypeString);

                        result = userService.AddUser(user);
                    }
                    else
                    {
                        result = new ServiceInvokeDTO<UserDTO>(InvokeCode.SYS_CAPTCHA_ERROR);
                    }
                }
                else
                {
                    result = new ServiceInvokeDTO<UserDTO>(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<UserDTO>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        private HttpResponseMessage Login(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string userName = ApiQueryUtil.QueryArgByPost("user_name");
            string password = ApiQueryUtil.QueryArgByPost("pwd");

            string terminalTypeString = ApiQueryUtil.QueryArgByPost("terminal_type");
            string terminalVersion = ApiQueryUtil.QueryArgByPost("terminal_version");
            string appVersion = ApiQueryUtil.QueryArgByPost("app_version");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd },
                { Constant.HTTP_HEADER_RANDOM, random },
                { "user_name", userName },
                { "pwd", password },
                { "terminal_type", terminalTypeString },
                { "terminal_version", terminalVersion },
                { "app_version", appVersion }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO<UserDTO> result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    LoginRecord loginRecord = new LoginRecord();
                    loginRecord.TerminalType = Convert.ToInt32(terminalTypeString);
                    loginRecord.TerminalVersion = terminalVersion;
                    loginRecord.AppVersion = appVersion;
                    loginRecord.LoginTime = DateTime.Now;

                    result = userService.Login(userName, password, loginRecord);
                }
                else
                {
                    result = new ServiceInvokeDTO<UserDTO>(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<UserDTO>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        private HttpResponseMessage UpdatePassword(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string idString = ApiQueryUtil.QueryArgByPost("user_id");
            string oldPwd = ApiQueryUtil.QueryArgByPost("old_pwd");
            string newPwd = ApiQueryUtil.QueryArgByPost("new_pwd");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd },
                { Constant.HTTP_HEADER_RANDOM, random },
                { "user_id", idString },
                { "old_pwd", oldPwd },
                { "new_pwd", newPwd }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    result = userService.UpdatePassword(Convert.ToInt32(idString), oldPwd, newPwd);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 设置新密码
        /// </summary>
        private HttpResponseMessage SetNewPassword(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string phone = ApiQueryUtil.QueryArgByPost("phone");
            string code = ApiQueryUtil.QueryArgByPost("code");
            string newPwd = ApiQueryUtil.QueryArgByPost("new_pwd");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd },
                { Constant.HTTP_HEADER_RANDOM, random },
                { "phone", phone },
                { "code", code },
                { "new_pwd", newPwd }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    // Check captcha
                    ServiceInvokeDTO captchaResult = securityService.CheckCaptcha(Service.Constant.CAPTCHA_TYPE_GET_PWD, phone, code);
                    if (captchaResult != null && captchaResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                    {
                        result = userService.ResetPassword(phone, newPwd);
                    }
                    else
                    {
                        result = new ServiceInvokeDTO(InvokeCode.SYS_CAPTCHA_ERROR);
                    }
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}