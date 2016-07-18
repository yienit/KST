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
using KST.Web.Filters;

namespace KST.Web.Controllers
{
    /// <summary>
    /// 机构信息及数据控制器
    /// </summary>
    [SessionTimeOutFilter]
    public class AgencyController : Controller
    {
        private const string VIEW_INDEX = "index";

        private const string VIEW_AGENCY_DETAIL = @"agency\agency-detail";

        private const string VIEW_ADMIN_LIST = @"admin\admin-list";
        private const string VIEW_ADMIN_ADD = @"admin\admin-add";
        private const string VIEW_ADMIN_UPDATE = @"admin\admin-update";
        private const string VIEW_ADMIN_DETAIL = @"admin\admin-detail";

        private const string VIEW_USER_LIST = "user-list";

        private AgencyDataService agencyDataService = ServiceFactory.Instance.AgencyDataService;
        private SecurityService securityService = ServiceFactory.Instance.SecurityService;
        private PermissionService permissionService = ServiceFactory.Instance.PermissionService;
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(AgencyController));

        #region View

        /// <summary>
        /// 索引页面
        /// </summary>
        public ActionResult Index()
        {
            return View(VIEW_INDEX);
        }

        /// <summary>
        /// 机构详情页面
        /// </summary>
        public ActionResult Detail()
        {
            return View(VIEW_AGENCY_DETAIL);
        }

        #region Admin

        /// <summary>
        /// 管理员账号管理页面
        /// </summary>
        public ActionResult AdminList()
        {
            return View(VIEW_ADMIN_LIST);
        }

        /// <summary>
        /// 管理员添加页面
        /// </summary>
        public ActionResult AdminAddTemplate()
        {
            return View(VIEW_ADMIN_ADD);
        }

        /// <summary>
        /// 管理员更新页面
        /// </summary>
        public ActionResult AdminUpdateTemplate()
        {
            return View(VIEW_ADMIN_UPDATE);
        }

        /// <summary>
        /// 管理员详情页面
        /// </summary>
        public ActionResult AdminDetailTemplate()
        {
            return View(VIEW_ADMIN_DETAIL);
        }

        #endregion

        #region User

        /// <summary>
        /// 学生账号管理页面
        /// </summary>
        public ActionResult UserList()
        {
            return View(VIEW_USER_LIST);
        }

        #endregion

        #endregion

        #region Hanlder

        #region Agency

        /// <summary>
        /// 获取培训机构详情
        /// </summary>
        [HttpGet]
        public ActionResult GetAgencyDetail()
        {
            log.Debug(Constant.DEBUG_START);

            string agencyIDString = ApiQueryUtil.QueryArgByGet("agency_id");

            ServiceInvokeDTO<AgencyDTO> result = null;
            try
            {
                int agencyID = Convert.ToInt32(agencyIDString);
                result = agencyDataService.GetAgencyDetail(agencyID);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<AgencyDTO>(InvokeCode.SYS_INNER_ERROR);
            }

            string json = JsonConvert.SerializeObject(result, Formatting.Indented, Constant.TIME_CONVERTER);
            log.Debug(Constant.DEBUG_END);

            return Content(json, Constant.JSON_MIME_TYPE);
        }

        /// <summary>
        /// 更新培训机构名称
        /// </summary>
        [HttpPost]
        public ActionResult UpdateAgencyName()
        {
            log.Debug(Constant.DEBUG_START);

            string name = ApiQueryUtil.QueryArgByPost("name");

            ServiceInvokeDTO result = null;
            try
            {
                int agencyID = (Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO).Agency.ID;
                result = agencyDataService.UpdateAgencyName(agencyID, name);
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
        /// 设置培训机构是否开启设备锁参数
        /// </summary>
        [HttpPost]
        public ActionResult SetIsLockDeviceConfig()
        {
            log.Debug(Constant.DEBUG_START);

            string isLockDeviceString = ApiQueryUtil.QueryArgByPost("is_lock_device");

            ServiceInvokeDTO result = null;
            try
            {
                int agencyID = (Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO).Agency.ID;
                int isLockDevice = Convert.ToInt32(isLockDeviceString);
                result = agencyDataService.SetIsLockDeviceConfig(agencyID, isLockDevice);
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
        /// 更新培训机构联系方式参数设置
        /// </summary>
        [HttpPost]
        public ActionResult UpdateAgencyContactConfig()
        {
            log.Debug(Constant.DEBUG_START);

            string contact = ApiQueryUtil.QueryArgByPost("contact");

            ServiceInvokeDTO result = null;
            try
            {
                int agencyID = (Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO).Agency.ID;
                result = agencyDataService.UpdateAgencyContactConfig(agencyID, contact);
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
        /// 更新培训机构公告通知参数设置
        /// </summary>
        [HttpPost]
        public ActionResult UpdateAgencyNoticeConfig()
        {
            log.Debug(Constant.DEBUG_START);

            string notice = ApiQueryUtil.QueryArgByPost("notice");

            ServiceInvokeDTO result = null;
            try
            {
                int agencyID = (Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO).Agency.ID;
                result = agencyDataService.UpdateAgencyNoticeConfig(agencyID, notice);
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

        #region Admin

        /// <summary>
        /// 管理员修改密码
        /// </summary>
        [HttpPost]
        public ActionResult UpdateAdminPwd()
        {
            log.Debug(Constant.DEBUG_START);

            string oldPwd = ApiQueryUtil.QueryArgByPost("old_pwd");
            string newPwd = ApiQueryUtil.QueryArgByPost("new_pwd");

            ServiceInvokeDTO result = null;
            try
            {
                int adminID = (Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO).ID;
                result = agencyDataService.UpdateAdminPassword(adminID, oldPwd, newPwd);
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
        /// 分页查询培训机构管理员信息
        /// </summary>
        [HttpGet]
        public ActionResult QueryAdmin()
        {
            log.Debug(Constant.DEBUG_START);

            string pageSizeString = ApiQueryUtil.QueryArgByGet("limit");
            string offsetString = ApiQueryUtil.QueryArgByGet("offset");

            string chineseName = ApiQueryUtil.QueryArgByGet("chinese_name");
            string phone = ApiQueryUtil.QueryArgByGet("phone");

            QueryResultDTO<AgencyAdmin> queryData = null;
            try
            {
                QueryArgsDTO<AgencyAdmin> queryDTO = new QueryArgsDTO<AgencyAdmin>();
                queryDTO.PageSize = Convert.ToInt32(pageSizeString);
                queryDTO.PageIndex = Convert.ToInt32(offsetString) / Convert.ToInt32(pageSizeString) + 1;

                queryDTO.Model.AgencyID = (Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO).Agency.ID;
                queryDTO.Model.ChineseName = chineseName;
                queryDTO.Model.Phone = phone;
                queryDTO.Model.Level = (AdminLevel)(-1);

                queryData = agencyDataService.QueryAdmin(queryDTO).Data;
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            string json = JsonConvert.SerializeObject(queryData, Formatting.Indented, Constant.TIME_CONVERTER);
            log.Debug(Constant.DEBUG_END);

            return Content(json, Constant.JSON_MIME_TYPE);
        }

        /// <summary>
        /// 添加管理员
        /// </summary>
        [HttpPost]
        public ActionResult AddAdmin()
        {
            log.Debug(Constant.DEBUG_START);

            string chineseName = ApiQueryUtil.QueryArgByPost("chinese_name");
            string phone = ApiQueryUtil.QueryArgByPost("phone");

            ServiceInvokeDTO result = null;
            try
            {
                AgencyAdmin admin = new AgencyAdmin();
                admin.AgencyID = (Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO).Agency.ID;
                admin.ChineseName = chineseName;
                admin.UserName = phone;
                admin.Phone = phone;
                admin.Level = AdminLevel.AgencyItemAdmin;

                result = agencyDataService.AddAdmin(admin);
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
        /// 更新管理员
        /// </summary>
        [HttpPost]
        public ActionResult UpdateAdmin()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");
            string chineseName = ApiQueryUtil.QueryArgByPost("chinese_name");
            string phone = ApiQueryUtil.QueryArgByPost("phone");

            ServiceInvokeDTO result = null;
            try
            {
                int adminID = Convert.ToInt32(idString);

                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.UpdateAdmin, currentAdmin.Agency.ID, adminID);
                if (checkResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    AgencyAdmin admin = new AgencyAdmin();
                    admin.ID = adminID;
                    admin.ChineseName = chineseName;
                    admin.Phone = phone;
                    result = agencyDataService.UpdateAdmin(admin);
                }
                else
                {
                    result = checkResult;
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
        /// 删除管理员
        /// </summary>
        [HttpPost]
        public ActionResult DeleteAdmin()
        {
            log.Debug(Constant.DEBUG_START);

            string adminIDString = ApiQueryUtil.QueryArgByPost("id");

            ServiceInvokeDTO result = null;
            try
            {
                int adminID = Convert.ToInt32(adminIDString);

                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.DeleteAdmin, currentAdmin.Agency.ID, adminID);
                if (checkResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    result = agencyDataService.DeleteAdmin(adminID);
                }
                else
                {
                    result = checkResult;
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
        /// 删除管理员(批量)
        /// </summary>
        [HttpPost]
        public ActionResult DeleteAdminInBatch()
        {
            log.Debug(Constant.DEBUG_START);

            string idListJson = ApiQueryUtil.QueryArgByPost("id_list");

            ServiceInvokeDTO result = null;
            try
            {
                List<int> adminIDList = JsonConvert.DeserializeObject<List<int>>(idListJson);

                bool isRightPermission = false;
                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                foreach (var adminID in adminIDList)
                {
                    ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.DeleteAdminInBatch, currentAdmin.Agency.ID, adminID);
                    if (checkResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                    {
                        isRightPermission = true;
                    }
                    else
                    {
                        isRightPermission = false;
                        break;
                    }
                }
                if (isRightPermission)
                {
                    result = agencyDataService.DeleteAdmin(adminIDList);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.PERMISSION_NOT_MINE_DATA_ERROR);
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

        #endregion

        #endregion
    }
}
