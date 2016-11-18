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
using System.IO;

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

        private const string VIEW_USER_LIST = @"user\user-list";
        private const string VIEW_USER_ADD = @"user\user-add";
        private const string VIEW_USER_UPDATE = @"user\user-update";
        private const string VIEW_USER_DETAIL = @"user\user-detail";
        private const string VIEW_USER_QUERY = @"user\user-query";
        private const string VIEW_USER_IMPORT = @"user\user-import";

        private const string VIEW_ADMIN_DORECORD_LIST = @"dorecord\dorecord-list";
        private const string VIEW_ADMIN_DORECORD_QUERY = @"dorecord\dorecord-query";
        private const string VIEW_ADMIN_DORECORD_DETAIL = @"dorecord\dorecord-detail";

        private AgencyDataService agencyDataService = ServiceFactory.Instance.AgencyDataService;
        private SecurityService securityService = ServiceFactory.Instance.SecurityService;
        private PermissionService permissionService = ServiceFactory.Instance.PermissionService;
        private RecordDataService recordDataService = ServiceFactory.Instance.RecordDataService;
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

        /// <summary>
        /// 学生账号添加页面
        /// </summary>
        public ActionResult UserAddTemplate()
        {
            return View(VIEW_USER_ADD);
        }

        /// <summary>
        /// 学生账号修改页面
        /// </summary>
        public ActionResult UserUpdateTemplate()
        {
            return View(VIEW_USER_UPDATE);
        }

        /// <summary>
        /// 学生账号详情页面
        /// </summary>
        public ActionResult UserDetailTemplate()
        {
            return View(VIEW_USER_DETAIL);
        }

        /// <summary>
        /// 学生账号查询页面
        /// </summary>
        public ActionResult UserQueryTemplate()
        {
            return View(VIEW_USER_QUERY);
        }

        /// <summary>
        /// 学生账号导入页面
        /// </summary>
        public ActionResult UserImportTemplate()
        {
            return View(VIEW_USER_IMPORT);
        }

        #endregion

        #region AdminDoRecord

        /// <summary>
        /// 管理员操作记录列表管理页面
        /// </summary>
        public ActionResult DoRecordList()
        {
            return View(VIEW_ADMIN_DORECORD_LIST);
        }

        /// <summary>
        /// 管理员操作记录查询页面
        /// </summary>
        public ActionResult DoRecordQueryTemplate()
        {
            return View(VIEW_ADMIN_DORECORD_QUERY);
        }

        /// <summary>
        /// 管理员操作记录详情页面
        /// </summary>
        public ActionResult DoRecordDetailTemplate()
        {
            return View(VIEW_ADMIN_DORECORD_DETAIL);
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
                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                int agencyID = currentAdmin.Agency.ID;
                result = agencyDataService.UpdateAgencyName(agencyID, name);

                // Write admin do record.
                if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    AdminDoRecord doRecord = new AdminDoRecord();
                    doRecord.AdminID = currentAdmin.ID;
                    doRecord.AdminName = currentAdmin.ChineseName;
                    doRecord.DoTime = DateTime.Now;
                    doRecord.DoName = DoActionType.UpdateAgency.GetDescription();
                    doRecord.DoContent = string.Format("修改机构/学校/公司名称为:{0}", name);
                    doRecord.Remark = string.Empty;
                    recordDataService.AddAdminDoRecord(doRecord);
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
                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                int agencyID = currentAdmin.Agency.ID;
                int isLockDevice = Convert.ToInt32(isLockDeviceString);
                result = agencyDataService.SetIsLockDeviceConfig(agencyID, isLockDevice);

                // Write admin do record.
                if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    AdminDoRecord doRecord = new AdminDoRecord();
                    doRecord.AdminID = currentAdmin.ID;
                    doRecord.AdminName = currentAdmin.ChineseName;
                    doRecord.DoTime = DateTime.Now;
                    doRecord.DoName = DoActionType.UpdageAgencyConfig.GetDescription();
                    doRecord.DoContent = isLockDevice == 1 ? "打开设备锁" : "关闭设备锁";
                    doRecord.Remark = string.Empty;
                    recordDataService.AddAdminDoRecord(doRecord);
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
                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                int agencyID = currentAdmin.Agency.ID;
                result = agencyDataService.UpdateAgencyContactConfig(agencyID, contact);

                // Write admin do record.
                if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    AdminDoRecord doRecord = new AdminDoRecord();
                    doRecord.AdminID = currentAdmin.ID;
                    doRecord.AdminName = currentAdmin.ChineseName;
                    doRecord.DoTime = DateTime.Now;
                    doRecord.DoName = DoActionType.UpdageAgencyConfig.GetDescription();
                    doRecord.DoContent = string.Format("修改联系方式为:{0}", contact);
                    doRecord.Remark = string.Empty;
                    recordDataService.AddAdminDoRecord(doRecord);
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
                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                int agencyID = currentAdmin.Agency.ID;
                result = agencyDataService.UpdateAgencyNoticeConfig(agencyID, notice);

                // Write admin do record.
                if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    AdminDoRecord doRecord = new AdminDoRecord();
                    doRecord.AdminID = currentAdmin.ID;
                    doRecord.AdminName = currentAdmin.ChineseName;
                    doRecord.DoTime = DateTime.Now;
                    doRecord.DoName = DoActionType.UpdageAgencyConfig.GetDescription();
                    doRecord.DoContent = string.Format("修改通知公告为:{0}", notice);
                    doRecord.Remark = string.Empty;
                    recordDataService.AddAdminDoRecord(doRecord);
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
                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                int adminID = currentAdmin.ID;
                result = agencyDataService.UpdateAdminPassword(adminID, oldPwd, newPwd);

                // Write admin do record.
                if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    AdminDoRecord doRecord = new AdminDoRecord();
                    doRecord.AdminID = currentAdmin.ID;
                    doRecord.AdminName = currentAdmin.ChineseName;
                    doRecord.DoTime = DateTime.Now;
                    doRecord.DoName = DoActionType.UpdatePassword.GetDescription();
                    doRecord.DoContent = string.Empty;
                    doRecord.Remark = string.Empty;
                    recordDataService.AddAdminDoRecord(doRecord);
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
                admin.Phone = phone;
                admin.Level = AdminLevel.AgencyItemAdmin;

                result = agencyDataService.AddAdmin(admin);

                // Write admin do record.
                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    AdminDoRecord doRecord = new AdminDoRecord();
                    doRecord.AdminID = currentAdmin.ID;
                    doRecord.AdminName = currentAdmin.ChineseName;
                    doRecord.DoTime = DateTime.Now;
                    doRecord.DoName = DoActionType.AddAdmin.GetDescription();
                    doRecord.DoContent = string.Format("新管理员姓名：{0}", chineseName);
                    doRecord.Remark = string.Empty;
                    recordDataService.AddAdminDoRecord(doRecord);
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

                    // Write admin do record.
                    if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                    {
                        AdminDoRecord doRecord = new AdminDoRecord();
                        doRecord.AdminID = currentAdmin.ID;
                        doRecord.AdminName = currentAdmin.ChineseName;
                        doRecord.DoTime = DateTime.Now;
                        doRecord.DoName = DoActionType.UpdateAdmin.GetDescription();
                        doRecord.DoContent = string.Format("被更新的管理员ID：{0}", idString);
                        doRecord.Remark = string.Empty;
                        recordDataService.AddAdminDoRecord(doRecord);
                    }
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

                    // Write admin do record.
                    if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                    {
                        AdminDoRecord doRecord = new AdminDoRecord();
                        doRecord.AdminID = currentAdmin.ID;
                        doRecord.AdminName = currentAdmin.ChineseName;
                        doRecord.DoTime = DateTime.Now;
                        doRecord.DoName = DoActionType.DeleteAdmin.GetDescription();
                        doRecord.DoContent = string.Format("被删除的管理员ID：{0}", adminIDString);
                        doRecord.Remark = string.Empty;
                        recordDataService.AddAdminDoRecord(doRecord);
                    }
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

                    // Write admin do record.
                    if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                    {
                        AdminDoRecord doRecord = new AdminDoRecord();
                        doRecord.AdminID = currentAdmin.ID;
                        doRecord.AdminName = currentAdmin.ChineseName;
                        doRecord.DoTime = DateTime.Now;
                        doRecord.DoName = DoActionType.DeleteAdminInBatch.GetDescription();
                        doRecord.DoContent = string.Empty;
                        doRecord.Remark = string.Empty;
                        recordDataService.AddAdminDoRecord(doRecord);
                    }
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

        #region User

        /// <summary>
        /// 分页查询学生账号
        /// </summary>
        [HttpGet]
        public ActionResult QueryUser()
        {
            log.Debug(Constant.DEBUG_START);

            string pageSizeString = ApiQueryUtil.QueryArgByGet("limit");
            string offsetString = ApiQueryUtil.QueryArgByGet("offset");

            string chapterIDString = ApiQueryUtil.QueryArgByGet("chapter_id");
            string isVipItemString = ApiQueryUtil.QueryArgByGet("is_vip_item");
            string title = ApiQueryUtil.QueryArgByGet("title");
            string difficultyString = ApiQueryUtil.QueryArgByGet("difficulty");
            string addPerson = ApiQueryUtil.QueryArgByGet("add_person");

            QueryResultDTO<SingleItemDTO> queryData = null;
            try
            {
                QueryArgsDTO<SingleItem> queryDTO = new QueryArgsDTO<SingleItem>();
                queryDTO.PageSize = Convert.ToInt32(pageSizeString);
                queryDTO.PageIndex = Convert.ToInt32(offsetString) / Convert.ToInt32(pageSizeString) + 1;

                queryDTO.Model.AgencyID = (Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO).Agency.ID;
                queryDTO.Model.ChapterID = Convert.ToInt32(chapterIDString);
                queryDTO.Model.IsVipItem = Convert.ToInt32(isVipItemString);
                queryDTO.Model.Title = title;
                queryDTO.Model.Difficulty = string.IsNullOrEmpty(difficultyString) ? -1 : Convert.ToInt32(difficultyString);
                queryDTO.Model.AddPerson = addPerson;

                int courseID = (Session[Constant.SESSION_KEY_COURSE] as Course).ID;

                //queryData = itemDataService.QuerySingle(queryDTO, courseID).Data;
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
        /// 添加学生账号
        /// </summary>
        [HttpPost]
        public ActionResult AddUser()
        {
            log.Debug(Constant.DEBUG_START);

            string isVipItemString = ApiQueryUtil.QueryArgByPost("is_vip_item");
            string chapterIDString = ApiQueryUtil.QueryArgByPost("chapter_id");

            string title = ApiQueryUtil.QueryArgByPost("title");
            HttpPostedFileBase imageFile = Request.Files["image"];
            string a = ApiQueryUtil.QueryArgByPost("a");
            string b = ApiQueryUtil.QueryArgByPost("b");
            string c = ApiQueryUtil.QueryArgByPost("c");
            string d = ApiQueryUtil.QueryArgByPost("d");
            string answer = ApiQueryUtil.QueryArgByPost("answer");
            string annotation = ApiQueryUtil.QueryArgByPost("annotation");
            string difficultyString = ApiQueryUtil.QueryArgByPost("difficulty");

            ServiceInvokeDTO result = null;
            try
            {
                SingleItem single = new SingleItem();
                single.AgencyID = (Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO).Agency.ID;
                single.IsVipItem = Convert.ToInt32(isVipItemString);
                single.ChapterID = Convert.ToInt32(chapterIDString);

                single.Title = title;
                single.A = a;
                single.B = b;
                single.C = c;
                single.D = d;
                single.Answer = answer;
                single.Annotation = annotation;
                single.Difficulty = Convert.ToInt32(difficultyString);
                single.AddPerson = (Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO).ChineseName;

                if (imageFile != null)
                {
                    byte[] imageBytes = null;
                    using (Stream inputStream = imageFile.InputStream)
                    {
                        MemoryStream memoryStream = inputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            inputStream.CopyTo(memoryStream);
                        }
                        imageBytes = memoryStream.ToArray();
                    }
                    single.Image = imageBytes;
                }

                //result = itemDataService.AddSingle(single);
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
        /// 更新学生账号
        /// </summary>
        [HttpPost]
        public ActionResult UpdateUser()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");
            string isVipItemString = ApiQueryUtil.QueryArgByPost("is_vip_item");
            string chapterIDString = ApiQueryUtil.QueryArgByPost("chapter_id");

            string title = ApiQueryUtil.QueryArgByPost("title");
            HttpPostedFileBase imageFile = Request.Files["image"];
            string a = ApiQueryUtil.QueryArgByPost("a");
            string b = ApiQueryUtil.QueryArgByPost("b");
            string c = ApiQueryUtil.QueryArgByPost("c");
            string d = ApiQueryUtil.QueryArgByPost("d");
            string answer = ApiQueryUtil.QueryArgByPost("answer");
            string annotation = ApiQueryUtil.QueryArgByPost("annotation");
            string difficultyString = ApiQueryUtil.QueryArgByPost("difficulty");

            ServiceInvokeDTO result = null;
            try
            {
                SingleItem single = new SingleItem();
                single.ID = Convert.ToInt32(idString);
                single.IsVipItem = Convert.ToInt32(isVipItemString);
                single.ChapterID = Convert.ToInt32(chapterIDString);

                single.Title = title;
                single.A = a;
                single.B = b;
                single.C = c;
                single.D = d;
                single.Answer = answer;
                single.Annotation = annotation;
                single.Difficulty = Convert.ToInt32(difficultyString);

                if (imageFile != null)
                {
                    byte[] imageBytes = null;
                    using (Stream inputStream = imageFile.InputStream)
                    {
                        MemoryStream memoryStream = inputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            inputStream.CopyTo(memoryStream);
                        }
                        imageBytes = memoryStream.ToArray();
                    }
                    single.Image = imageBytes;
                }

                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.UpdateSingle, currentAdmin.Agency.ID, single.ID);
                if (checkResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    //result = itemDataService.UpdateSingle(single);
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
        /// 删除学生账号
        /// </summary>
        [HttpPost]
        public ActionResult DeleteUser()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");

            ServiceInvokeDTO result = null;
            try
            {
                int id = Convert.ToInt32(idString);

                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.DeleteSingle, currentAdmin.Agency.ID, id);
                if (checkResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    //result = itemDataService.DeleteSingle(id);
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
        /// 删除学生账号(批量)
        /// </summary>
        [HttpPost]
        public ActionResult DeleteUserInBatch()
        {
            log.Debug(Constant.DEBUG_START);

            string idListJson = ApiQueryUtil.QueryArgByPost("id_list");

            ServiceInvokeDTO result = null;
            try
            {
                List<int> idList = JsonConvert.DeserializeObject<List<int>>(idListJson);

                bool isRightPermission = false;
                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                foreach (var id in idList)
                {
                    ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.DeleteSingleInBatch, currentAdmin.Agency.ID, id);
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
                    //result = itemDataService.DeleteSingle(idList);
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

        /// <summary>
        /// 下载学生账号模板文件
        /// </summary>
        [HttpGet]
        public ActionResult DownloadUserTemplateFile()
        {
            string templatePath = Server.MapPath("/") + @"Files\Template\template_item_single.xls";
            return DownloadUtil.Download("single_template.xls", templatePath);
        }

        /// <summary>
        /// 上传学生账号Excel文件至服务器
        /// </summary>
        [HttpPost]
        public ActionResult UploadUserExcelFile()
        {
            log.Debug(Constant.DEBUG_START);

            // 接收上传后的文件
            HttpPostedFileBase file = Request.Files["Filedata"];

            ServiceInvokeDTO<string> result = null;
            try
            {
                if (file != null)
                {
                    // 判断临时文件夹是否存在，不存在则创建
                    string tempFolder = Server.MapPath("/") + @"Files\TempData";
                    if (!System.IO.Directory.Exists(tempFolder))
                    {
                        System.IO.Directory.CreateDirectory(tempFolder);
                    }

                    // 保存文件
                    string ext = System.IO.Path.GetExtension(file.FileName).ToLower();
                    if (ext.Equals(".xls") || ext.Equals(".xlsx"))
                    {
                        string tempFileName = string.Format("{0}{1}", Guid.NewGuid(), ext);
                        file.SaveAs(tempFolder + @"\" + tempFileName);
                        result = new ServiceInvokeDTO<string>(InvokeCode.SYS_INVOKE_SUCCESS, tempFileName);
                    }
                    else
                    {
                        result = new ServiceInvokeDTO<string>(InvokeCode.ITEM_FILE_FORMAT_ERROR, null);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<string>(InvokeCode.SYS_INNER_ERROR, null);
            }

            string json = JsonConvert.SerializeObject(result, Formatting.Indented, Constant.TIME_CONVERTER);
            log.Debug(Constant.DEBUG_END);

            return Content(json, Constant.JSON_MIME_TYPE);
        }

        /// <summary>
        /// 开始导入学生账号
        /// </summary>
        [HttpPost]
        public ActionResult StartLoadUserExcelFile()
        {
            log.Debug(Constant.DEBUG_START);

            // 题库Excel文件
            string fileName = Request["file_name"];

            ServiceInvokeDTO result = null;
            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    string tempFilePath = Server.MapPath("/") + @"Files\TempData\" + fileName;

                    if (System.IO.File.Exists(tempFilePath))
                    {
                        // 1.处理数据并校验
                        Course currentCourse = (Course)Session[Constant.SESSION_KEY_COURSE];
                        AgencyAdminDTO currentUser = (AgencyAdminDTO)Session[Constant.SESSION_KEY_ADMIN];
                        List<SingleItem> singles = TemplateUtil.ReadSingleTemplate(currentUser, currentCourse.ID, tempFilePath, true);

                        // 2.批量添加
                        //result = itemDataService.AddSingle(singles);

                        // 3.删除文件
                        System.IO.File.Delete(tempFilePath);

                        result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                    }
                    else
                    {
                        result = new ServiceInvokeDTO(InvokeCode.ITEM_FILE_NOT_EXIST_ERROR);
                    }
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.ITEM_FILE_FORMAT_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INNER_ERROR, ex.Message);

                // 删除文件
                if (!string.IsNullOrEmpty(fileName))
                {
                    string tempFilePath = Server.MapPath("/") + @"Files\TempData\" + fileName;
                    System.IO.File.Delete(tempFilePath);
                }
            }

            string json = JsonConvert.SerializeObject(result, Formatting.Indented, Constant.TIME_CONVERTER);
            log.Debug(Constant.DEBUG_END);

            return Content(json, Constant.JSON_MIME_TYPE);
        }

        #endregion

        #region AdminDoRecord

        /// <summary>
        /// 分页查询培训机构管理员操作记录信息
        /// </summary>
        [HttpGet]
        public ActionResult QueryDoRecord()
        {
            log.Debug(Constant.DEBUG_START);

            string pageSizeString = ApiQueryUtil.QueryArgByGet("limit");
            string offsetString = ApiQueryUtil.QueryArgByGet("offset");

            string chineseName = ApiQueryUtil.QueryArgByGet("chinese_name");
            string doName = ApiQueryUtil.QueryArgByGet("do_name");
            string startDateString = ApiQueryUtil.QueryArgByGet("start_date");
            string endDateString = ApiQueryUtil.QueryArgByGet("end_date");

            QueryResultDTO<AdminDoRecord> queryData = null;
            try
            {
                QueryArgsDTO<AdminDoRecord> queryDTO = new QueryArgsDTO<AdminDoRecord>();
                queryDTO.PageSize = Convert.ToInt32(pageSizeString);
                queryDTO.PageIndex = Convert.ToInt32(offsetString) / Convert.ToInt32(pageSizeString) + 1;

                queryDTO.Model.AdminName = chineseName;
                queryDTO.Model.DoName = doName;

                int agencyID = (Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO).Agency.ID;
                DateTime startDate = string.IsNullOrEmpty(startDateString) ? DateTime.MinValue : Convert.ToDateTime(startDateString);
                DateTime endDate = string.IsNullOrEmpty(endDateString) ? DateTime.MinValue : Convert.ToDateTime(endDateString);

                queryData = recordDataService.QueryAdminDoRecord(queryDTO, agencyID, startDate, endDate).Data;
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            string json = JsonConvert.SerializeObject(queryData, Formatting.Indented, Constant.TIME_CONVERTER);
            log.Debug(Constant.DEBUG_END);

            return Content(json, Constant.JSON_MIME_TYPE);
        }

        #endregion

        #endregion
    }
}
