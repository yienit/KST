using System;
using System.Web.Mvc;
using KST.Core;
using KST.DTO;
using KST.Service;
using KST.Util;
using KST.Web.Filters;
using Newtonsoft.Json;
using System.Web;
using KST.Model;
using System.Collections.Generic;
using System.IO;
using KST.Model.Model;

namespace KST.Web.Controllers
{
    /// <summary>
    /// 课程、章节、题库数据管理控制器
    /// </summary>
    [SessionTimeOutFilter]
    public class ItemController : Controller
    {
        private const string VIEW_INDEX = "index";
        private const string VIEW_KJJC_FENXI_INDEX = "kjjc-index";

        private const string VIEW_CHAPTER_LIST = @"chapter\chapter-list";
        private const string VIEW_CHAPTER_ADD = @"chapter\chapter-add";
        private const string VIEW_CHAPTER_UPDATE = @"chapter\chapter-update";
        private const string VIEW_CHAPTER_DETAIL = @"chapter\chapter-detail";

        private const string VIEW_SINGLE_LIST = @"single\single-list";
        private const string VIEW_SINGLE_ADD = @"single\single-add";
        private const string VIEW_SINGLE_UPDATE = @"single\single-update";
        private const string VIEW_SINGLE_DETAIL = @"single\single-detail";
        private const string VIEW_SINGLE_QUERY = @"single\single-query";
        private const string VIEW_SINGLE_IMPORT = @"single\single-import";

        private const string VIEW_MULTIPLE_LIST = @"multiple\multiple-list";
        private const string VIEW_MULTIPLE_ADD = @"multiple\multiple-add";
        private const string VIEW_MULTIPLE_UPDATE = @"multiple\multiple-update";
        private const string VIEW_MULTIPLE_DETAIL = @"multiple\multiple-detail";
        private const string VIEW_MULTIPLE_QUERY = @"multiple\multiple-query";
        private const string VIEW_MULTIPLE_IMPORT = @"multiple\multiple-import";

        private const string VIEW_JUDGE_LIST = @"judge\judge-list";
        private const string VIEW_JUDGE_ADD = @"judge\judge-add";
        private const string VIEW_JUDGE_UPDATE = @"judge\judge-update";
        private const string VIEW_JUDGE_DETAIL = @"judge\judge-detail";
        private const string VIEW_JUDGE_QUERY = @"judge\judge-query";
        private const string VIEW_JUDGE_IMPORT = @"judge\judge-import";

        private const string VIEW_UNCERTAIN_LIST = @"uncertain\uncertain-list";
        private const string VIEW_UNCERTAIN_ADD = @"uncertain\uncertain-add";
        private const string VIEW_UNCERTAIN_UPDATE = @"uncertain\uncertain-update";
        private const string VIEW_UNCERTAIN_DETAIL = @"uncertain\uncertain-detail";
        private const string VIEW_UNCERTAIN_QUERY = @"uncertain\uncertain-query";

        private const string VIEW_UNCERTAIN_SUB_CHOICE_LIST = @"uncertain\subchoice-list";
        private const string VIEW_UNCERTAIN_SUB_CHOICE_ADD = @"uncertain\subchoice-add";
        private const string VIEW_UNCERTAIN_SUB_CHOICE_UPDATE = @"uncertain\subchoice-update";
        private const string VIEW_UNCERTAIN_SUB_CHOICE_DETAIL = @"uncertain\subchoice-detail";

        private const string VIEW_FENLU_LIST = @"fenlu\fenlu-list";
        private const string VIEW_FENLU_ADD = @"fenlu\fenlu-add";
        private const string VIEW_FENLU_UPDATE = @"fenlu\fenlu-update";
        private const string VIEW_FENLU_DETAIL = @"fenlu\fenlu-detail";
        private const string VIEW_FENLU_QUERY = @"fenlu\fenlu-query";

        private const string VIEW_NUMBERBLANK_LIST = @"numberblank\numberblank-list";
        private const string VIEW_NUMBERBLANK_ADD = @"numberblank\numberblank-add";
        private const string VIEW_NUMBERBLANK_UPDATE = @"numberblank\numberblank-update";
        private const string VIEW_NUMBERBLANK_DETAIL = @"numberblank\numberblank-detail";
        private const string VIEW_NUMBERBLANK_QUERY = @"numberblank\numberblank-query";

        private ItemDataService itemDataService = ServiceFactory.Instance.ItemDataService;
        private PermissionService permissionService = ServiceFactory.Instance.PermissionService;
        private RecordDataService recordDataService = ServiceFactory.Instance.RecordDataService;
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ItemController));

        #region View

        /// <summary>
        /// 索引页面
        /// </summary>
        public ActionResult Index()
        {
            return View(VIEW_INDEX);
        }

        /// <summary>
        /// 会计基础分析题索引页面
        /// </summary>
        public ActionResult KjjcIndex()
        {
            return View(VIEW_KJJC_FENXI_INDEX);
        }

        #region Chapter

        /// <summary>
        /// 章节列表管理页面
        /// </summary>
        public ActionResult ChapterList()
        {
            return View(VIEW_CHAPTER_LIST);
        }

        /// <summary>
        /// 章节添加页面
        /// </summary>
        public ActionResult ChapterAddTemplate()
        {
            return View(VIEW_CHAPTER_ADD);
        }

        /// <summary>
        /// 章节修改页面
        /// </summary>
        public ActionResult ChapterUpdateTemplate()
        {
            return View(VIEW_CHAPTER_UPDATE);
        }

        /// <summary>
        /// 章节详情页面
        /// </summary>
        public ActionResult ChapterDetailTemplate()
        {
            return View(VIEW_CHAPTER_DETAIL);
        }

        #endregion

        #region Single

        /// <summary>
        /// 单选题列表管理页面
        /// </summary>
        public ActionResult SingleList()
        {
            return View(VIEW_SINGLE_LIST);
        }

        /// <summary>
        /// 单选题添加页面
        /// </summary>
        public ActionResult SingleAddTemplate()
        {
            return View(VIEW_SINGLE_ADD);
        }

        /// <summary>
        /// 单选题修改页面
        /// </summary>
        public ActionResult SingleUpdateTemplate()
        {
            return View(VIEW_SINGLE_UPDATE);
        }

        /// <summary>
        /// 单选题详情页面
        /// </summary>
        public ActionResult SingleDetailTemplate()
        {
            return View(VIEW_SINGLE_DETAIL);
        }

        /// <summary>
        /// 单选题查询页面
        /// </summary>
        public ActionResult SingleQueryTemplate()
        {
            return View(VIEW_SINGLE_QUERY);
        }

        /// <summary>
        /// 单选题导入页面
        /// </summary>
        public ActionResult SingleImportTemplate()
        {
            return View(VIEW_SINGLE_IMPORT);
        }

        #endregion

        #region Multiple

        /// <summary>
        /// 多选题列表管理页面
        /// </summary>
        public ActionResult MultipleList()
        {
            return View(VIEW_MULTIPLE_LIST);
        }

        /// <summary>
        /// 多选题添加页面
        /// </summary>
        public ActionResult MultipleAddTemplate()
        {
            return View(VIEW_MULTIPLE_ADD);
        }

        /// <summary>
        /// 多选题修改页面
        /// </summary>
        public ActionResult MultipleUpdateTemplate()
        {
            return View(VIEW_MULTIPLE_UPDATE);
        }

        /// <summary>
        /// 多选题详情页面
        /// </summary>
        public ActionResult MultipleDetailTemplate()
        {
            return View(VIEW_MULTIPLE_DETAIL);
        }

        /// <summary>
        /// 多选题查询页面
        /// </summary>
        public ActionResult MultipleQueryTemplate()
        {
            return View(VIEW_MULTIPLE_QUERY);
        }

        /// <summary>
        /// 多选题导入页面
        /// </summary>
        public ActionResult MultipleImportTemplate()
        {
            return View(VIEW_MULTIPLE_IMPORT);
        }

        #endregion

        #region Judge

        /// <summary>
        /// 判断题列表管理页面
        /// </summary>
        public ActionResult JudgeList()
        {
            return View(VIEW_JUDGE_LIST);
        }

        /// <summary>
        /// 判断题添加页面
        /// </summary>
        public ActionResult JudgeAddTemplate()
        {
            return View(VIEW_JUDGE_ADD);
        }

        /// <summary>
        /// 判断题修改页面
        /// </summary>
        public ActionResult JudgeUpdateTemplate()
        {
            return View(VIEW_JUDGE_UPDATE);
        }

        /// <summary>
        /// 判断题详情页面
        /// </summary>
        public ActionResult JudgeDetailTemplate()
        {
            return View(VIEW_JUDGE_DETAIL);
        }

        /// <summary>
        /// 判断题查询页面
        /// </summary>
        public ActionResult JudgeQueryTemplate()
        {
            return View(VIEW_JUDGE_QUERY);
        }

        /// <summary>
        /// 判断题导入页面
        /// </summary>
        public ActionResult JudgeImportTemplate()
        {
            return View(VIEW_JUDGE_IMPORT);
        }

        #endregion

        #region Uncertain

        /// <summary>
        /// 不定向选择题列表管理页面
        /// </summary>
        public ActionResult UncertainList()
        {
            return View(VIEW_UNCERTAIN_LIST);
        }

        /// <summary>
        /// 不定向选择题添加页面
        /// </summary>
        public ActionResult UncertainAddTemplate()
        {
            return View(VIEW_UNCERTAIN_ADD);
        }

        /// <summary>
        /// 不定向选择题修改页面
        /// </summary>
        public ActionResult UncertainUpdateTemplate()
        {
            return View(VIEW_UNCERTAIN_UPDATE);
        }

        /// <summary>
        /// 不定向选择题详情页面
        /// </summary>
        public ActionResult UncertainDetailTemplate()
        {
            return View(VIEW_UNCERTAIN_DETAIL);
        }

        /// <summary>
        /// 不定向选择题查询页面
        /// </summary>
        public ActionResult UncertainQueryTemplate()
        {
            return View(VIEW_UNCERTAIN_QUERY);
        }

        //================================ 不定项选择题子选择题  ================================


        /// <summary>
        /// 不定项子选择题列表管理页面
        /// </summary>
        public ActionResult UncertainSubChoiceList()
        {
            return View(VIEW_UNCERTAIN_SUB_CHOICE_LIST);
        }

        /// <summary>
        /// 不定项子选择题添加页面
        /// </summary>
        public ActionResult UncertainSubChoiceAddTemplate()
        {
            return View(VIEW_UNCERTAIN_SUB_CHOICE_ADD);
        }

        /// <summary>
        /// 不定项子选择题修改页面
        /// </summary>
        public ActionResult UncertainSubChoiceUpdateTemplate()
        {
            return View(VIEW_UNCERTAIN_SUB_CHOICE_UPDATE);
        }

        /// <summary>
        /// 不定项子选择题详情页面
        /// </summary>
        public ActionResult UncertainSubChoiceDetailTemplate()
        {
            return View(VIEW_UNCERTAIN_SUB_CHOICE_DETAIL);
        }

        #endregion

        #region FenLu

        /// <summary>
        /// 分录题列表管理页面
        /// </summary>
        public ActionResult FenLuList()
        {
            return View(VIEW_FENLU_LIST);
        }

        /// <summary>
        /// 分录题添加页面
        /// </summary>
        public ActionResult FenLuAddTemplate()
        {
            return View(VIEW_FENLU_ADD);
        }

        /// <summary>
        /// 分录题修改页面
        /// </summary>
        public ActionResult FenLuUpdateTemplate()
        {
            return View(VIEW_FENLU_UPDATE);
        }

        /// <summary>
        /// 分录题详情页面
        /// </summary>
        public ActionResult FenLuDetailTemplate()
        {
            return View(VIEW_FENLU_DETAIL);
        }

        /// <summary>
        /// 分录题查询页面
        /// </summary>
        public ActionResult FenLuQueryTemplate()
        {
            return View(VIEW_FENLU_QUERY);
        }

        #endregion

        #region NumberBlank

        /// <summary>
        /// 数字填空题列表管理页面
        /// </summary>
        public ActionResult NumberBlankList()
        {
            return View(VIEW_NUMBERBLANK_LIST);
        }

        /// <summary>
        /// 数字填空题添加页面
        /// </summary>
        public ActionResult NumberBlankAddTemplate()
        {
            return View(VIEW_NUMBERBLANK_ADD);
        }

        /// <summary>
        /// 数字填空题修改页面
        /// </summary>
        public ActionResult NumberBlankUpdateTemplate()
        {
            return View(VIEW_NUMBERBLANK_UPDATE);
        }

        /// <summary>
        /// 数字填空题详情页面
        /// </summary>
        public ActionResult NumberBlankDetailTemplate()
        {
            return View(VIEW_NUMBERBLANK_DETAIL);
        }

        /// <summary>
        /// 数字填空题查询页面
        /// </summary>
        public ActionResult NumberBlankQueryTemplate()
        {
            return View(VIEW_NUMBERBLANK_QUERY);
        }

        #endregion

        #endregion

        #region Handler

        #region Course & Chapter

        /// <summary>
        /// 切换当前课程科目
        /// </summary>
        [HttpPost]
        public ActionResult ExchangeCourse()
        {
            log.Debug(Constant.DEBUG_START);

            string courseIDString = ApiQueryUtil.QueryArgByPost("course_id");
            ServiceInvokeDTO result = null;
            try
            {
                HttpCookie cookie = Request.Cookies[Constant.COOKIE_NAME];
                if (cookie == null)
                {
                    cookie = new HttpCookie(Constant.COOKIE_NAME);
                    cookie.Expires = DateTime.Now.AddDays(Constant.COOKIE_EXPIRES_DAY);
                    cookie.Values.Add(Constant.COOKIE_KEY_COURSE_ID, courseIDString);
                    Response.Cookies.Add(cookie);
                }
                else
                {
                    cookie.Values[Constant.COOKIE_KEY_COURSE_ID] = courseIDString;
                    Response.Cookies.Set(cookie);
                }

                int courseID = Convert.ToInt32(cookie[Constant.COOKIE_KEY_COURSE_ID]);
                Session[Constant.SESSION_KEY_COURSE] = itemDataService.GetCourseByID(courseID).Data;

                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
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
        /// 获取所有章节
        /// </summary>
        [HttpGet]
        public ActionResult GetAllChapter()
        {
            log.Debug(Constant.DEBUG_START);

            List<Chapter> chapters = null;
            try
            {
                int courseID = (Session[Constant.SESSION_KEY_COURSE] as Course).ID;
                ServiceInvokeDTO<List<Chapter>> result = itemDataService.GetAgencyChapters(courseID);
                if (result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    chapters = result.Data;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            string json = JsonConvert.SerializeObject(new { data = chapters }, Formatting.Indented, Constant.TIME_CONVERTER);
            log.Debug(Constant.DEBUG_END);

            return Content(json, Constant.JSON_MIME_TYPE);
        }

        /// <summary>
        /// 添加章节
        /// </summary>
        [HttpPost]
        public ActionResult AddChapter()
        {
            log.Debug(Constant.DEBUG_START);

            string name = ApiQueryUtil.QueryArgByPost("name");

            ServiceInvokeDTO result = null;
            try
            {
                Chapter chapter = new Chapter();
                chapter.CourseID = (Session[Constant.SESSION_KEY_COURSE] as Course).ID;
                chapter.Name = name;

                result = itemDataService.AddChapter(chapter);

                // Write admin do record.
                if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;

                    AdminDoRecord doRecord = new AdminDoRecord();
                    doRecord.AdminID = currentAdmin.ID;
                    doRecord.AdminName = currentAdmin.ChineseName;
                    doRecord.DoTime = DateTime.Now;
                    doRecord.DoName = DoActionType.AddChapter.GetDescription();
                    doRecord.DoContent = string.Format("新章节名称：{0}", name);
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
        /// 更新章节
        /// </summary>
        [HttpPost]
        public ActionResult UpdateChapter()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");
            string name = ApiQueryUtil.QueryArgByPost("name");

            ServiceInvokeDTO result = null;
            try
            {
                Chapter chapter = new Chapter();
                chapter.ID = Convert.ToInt32(idString);
                chapter.Name = name;

                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.UpdateChapter, currentAdmin.Agency.ID, chapter.ID);
                if (checkResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    result = itemDataService.UpdateChapter(chapter);

                    // Write admin do record.
                    if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                    {
                        AdminDoRecord doRecord = new AdminDoRecord();
                        doRecord.AdminID = currentAdmin.ID;
                        doRecord.AdminName = currentAdmin.ChineseName;
                        doRecord.DoTime = DateTime.Now;
                        doRecord.DoName = DoActionType.UpdateChapter.GetDescription();
                        doRecord.DoContent = string.Format("新更新的章节名称：{0}", name);
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
        /// 上调章节序号
        /// </summary>
        [HttpPost]
        public ActionResult UpChapter()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");

            ServiceInvokeDTO result = null;
            try
            {
                int id = Convert.ToInt32(idString);

                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.UpChapter, currentAdmin.Agency.ID, id);
                if (checkResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    result = itemDataService.UpChapter(id);

                    // Write admin do record.
                    if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                    {
                        AdminDoRecord doRecord = new AdminDoRecord();
                        doRecord.AdminID = currentAdmin.ID;
                        doRecord.AdminName = currentAdmin.ChineseName;
                        doRecord.DoTime = DateTime.Now;
                        doRecord.DoName = DoActionType.UpChapter.GetDescription();
                        doRecord.DoContent = string.Format("章节主键ID：{0}", id);
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
        /// 下调章节序号
        /// </summary>
        [HttpPost]
        public ActionResult DownChapter()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");

            ServiceInvokeDTO result = null;
            try
            {
                int id = Convert.ToInt32(idString);

                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.DownChapter, currentAdmin.Agency.ID, id);
                if (checkResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    result = itemDataService.DownChapter(id);

                    // Write admin do record.
                    if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                    {
                        AdminDoRecord doRecord = new AdminDoRecord();
                        doRecord.AdminID = currentAdmin.ID;
                        doRecord.AdminName = currentAdmin.ChineseName;
                        doRecord.DoTime = DateTime.Now;
                        doRecord.DoName = DoActionType.DownChapter.GetDescription();
                        doRecord.DoContent = string.Format("章节主键ID：{0}", id);
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
        /// 删除章节
        /// </summary>
        [HttpPost]
        public ActionResult DeleteChapter()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");

            ServiceInvokeDTO result = null;
            try
            {
                int id = Convert.ToInt32(idString);

                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.DeleteChapter, currentAdmin.Agency.ID, id);
                if (checkResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    result = itemDataService.DeleteChapter(id);

                    // Write admin do record.
                    if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                    {
                        AdminDoRecord doRecord = new AdminDoRecord();
                        doRecord.AdminID = currentAdmin.ID;
                        doRecord.AdminName = currentAdmin.ChineseName;
                        doRecord.DoTime = DateTime.Now;
                        doRecord.DoName = DoActionType.DeleteChapter.GetDescription();
                        doRecord.DoContent = string.Format("章节主键ID：{0}", id);
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
        /// 删除章节(批量)
        /// </summary>
        [HttpPost]
        public ActionResult DeleteChapterInBatch()
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
                    ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.DeleteChapterInBatch, currentAdmin.Agency.ID, id);
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
                    result = itemDataService.DeleteChapter(idList);

                    // Write admin do record.
                    if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                    {
                        AdminDoRecord doRecord = new AdminDoRecord();
                        doRecord.AdminID = currentAdmin.ID;
                        doRecord.AdminName = currentAdmin.ChineseName;
                        doRecord.DoTime = DateTime.Now;
                        doRecord.DoName = DoActionType.DeleteChapterInBatch.GetDescription();
                        doRecord.DoContent = string.Format("章节主键ID：{0}", idListJson);
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

        #region Single

        /// <summary>
        /// 分页查询单选题
        /// </summary>
        [HttpGet]
        public ActionResult QuerySingle()
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

                queryData = itemDataService.QuerySingle(queryDTO, courseID).Data;
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
        /// 添加单选题
        /// </summary>
        [HttpPost]
        public ActionResult AddSingle()
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

                result = itemDataService.AddSingle(single);

                // Write admin do record.
                if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                    AdminDoRecord doRecord = new AdminDoRecord();
                    doRecord.AdminID = currentAdmin.ID;
                    doRecord.AdminName = currentAdmin.ChineseName;
                    doRecord.DoTime = DateTime.Now;
                    doRecord.DoName = DoActionType.AddSingle.GetDescription();
                    doRecord.DoContent = string.Format("新单选题标题：{0}", title);
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
        /// 更新单选题
        /// </summary>
        [HttpPost]
        public ActionResult UpdateSingle()
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
                    result = itemDataService.UpdateSingle(single);

                    // Write admin do record.
                    if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                    {
                        AdminDoRecord doRecord = new AdminDoRecord();
                        doRecord.AdminID = currentAdmin.ID;
                        doRecord.AdminName = currentAdmin.ChineseName;
                        doRecord.DoTime = DateTime.Now;
                        doRecord.DoName = DoActionType.UpdateSingle.GetDescription();
                        doRecord.DoContent = string.Format("被更新的主键ID：{0}", idString);
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
        /// 删除单选题
        /// </summary>
        [HttpPost]
        public ActionResult DeleteSingle()
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
                    result = itemDataService.DeleteSingle(id);

                    // Write admin do record.
                    if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                    {
                        AdminDoRecord doRecord = new AdminDoRecord();
                        doRecord.AdminID = currentAdmin.ID;
                        doRecord.AdminName = currentAdmin.ChineseName;
                        doRecord.DoTime = DateTime.Now;
                        doRecord.DoName = DoActionType.DeleteSingle.GetDescription();
                        doRecord.DoContent = string.Format("被删除的主键ID：{0}", idString);
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
        /// 删除单选题(批量)
        /// </summary>
        [HttpPost]
        public ActionResult DeleteSingleInBatch()
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
                    result = itemDataService.DeleteSingle(idList);

                    // Write admin do record.
                    if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                    {
                        AdminDoRecord doRecord = new AdminDoRecord();
                        doRecord.AdminID = currentAdmin.ID;
                        doRecord.AdminName = currentAdmin.ChineseName;
                        doRecord.DoTime = DateTime.Now;
                        doRecord.DoName = DoActionType.DeleteSingleInBatch.GetDescription();
                        doRecord.DoContent = string.Format("被删除的主键ID：{0}", idListJson);
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

        /// <summary>
        /// 下载单选题题库模板文件
        /// </summary>
        [HttpGet]
        public ActionResult DownloadSingleTemplateFile()
        {
            string templatePath = Server.MapPath("/") + @"Files\Template\template_item_single.xls";
            return DownloadUtil.Download("single_template.xls", templatePath);
        }

        /// <summary>
        /// 上传单选题题库Excel文件至服务器
        /// </summary>
        [HttpPost]
        public ActionResult UploadSingleExcelFile()
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
        /// 开始导入单选题题库
        /// </summary>
        [HttpPost]
        public ActionResult StartLoadSingleExcelFile()
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
                        result = itemDataService.AddSingle(singles);

                        // 3.删除文件
                        System.IO.File.Delete(tempFilePath);

                        result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);

                        // Write admin do record.
                        if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                        {
                            AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                            AdminDoRecord doRecord = new AdminDoRecord();
                            doRecord.AdminID = currentAdmin.ID;
                            doRecord.AdminName = currentAdmin.ChineseName;
                            doRecord.DoTime = DateTime.Now;
                            doRecord.DoName = DoActionType.AddSingleInBatch.GetDescription();
                            doRecord.DoContent = string.Empty;
                            doRecord.Remark = string.Empty;
                            recordDataService.AddAdminDoRecord(doRecord);
                        }
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

        #region Multiple

        /// <summary>
        /// 分页查询多选题
        /// </summary>
        [HttpGet]
        public ActionResult QueryMultiple()
        {
            log.Debug(Constant.DEBUG_START);

            string pageSizeString = ApiQueryUtil.QueryArgByGet("limit");
            string offsetString = ApiQueryUtil.QueryArgByGet("offset");

            string chapterIDString = ApiQueryUtil.QueryArgByGet("chapter_id");
            string isVipItemString = ApiQueryUtil.QueryArgByGet("is_vip_item");
            string title = ApiQueryUtil.QueryArgByGet("title");
            string difficultyString = ApiQueryUtil.QueryArgByGet("difficulty");
            string addPerson = ApiQueryUtil.QueryArgByGet("add_person");

            QueryResultDTO<MultipleItemDTO> queryData = null;
            try
            {
                QueryArgsDTO<MultipleItem> queryDTO = new QueryArgsDTO<MultipleItem>();
                queryDTO.PageSize = Convert.ToInt32(pageSizeString);
                queryDTO.PageIndex = Convert.ToInt32(offsetString) / Convert.ToInt32(pageSizeString) + 1;

                queryDTO.Model.AgencyID = (Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO).Agency.ID;
                queryDTO.Model.ChapterID = Convert.ToInt32(chapterIDString);
                queryDTO.Model.IsVipItem = Convert.ToInt32(isVipItemString);
                queryDTO.Model.Title = title;
                queryDTO.Model.Difficulty = string.IsNullOrEmpty(difficultyString) ? -1 : Convert.ToInt32(difficultyString);
                queryDTO.Model.AddPerson = addPerson;

                int courseID = (Session[Constant.SESSION_KEY_COURSE] as Course).ID;

                queryData = itemDataService.QueryMultiple(queryDTO, courseID).Data;
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
        /// 添加多选题
        /// </summary>
        [HttpPost]
        public ActionResult AddMultiple()
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
                MultipleItem multiple = new MultipleItem();
                multiple.AgencyID = (Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO).Agency.ID;
                multiple.IsVipItem = Convert.ToInt32(isVipItemString);
                multiple.ChapterID = Convert.ToInt32(chapterIDString);

                multiple.Title = title;
                multiple.A = a;
                multiple.B = b;
                multiple.C = c;
                multiple.D = d;
                multiple.Answer = answer;
                multiple.Annotation = annotation;
                multiple.Difficulty = Convert.ToInt32(difficultyString);
                multiple.AddPerson = (Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO).ChineseName;

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
                    multiple.Image = imageBytes;
                }

                result = itemDataService.AddMultiple(multiple);

                // Write admin do record.
                if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                    AdminDoRecord doRecord = new AdminDoRecord();
                    doRecord.AdminID = currentAdmin.ID;
                    doRecord.AdminName = currentAdmin.ChineseName;
                    doRecord.DoTime = DateTime.Now;
                    doRecord.DoName = DoActionType.AddMultiple.GetDescription();
                    doRecord.DoContent = string.Format("新添加的多选题标题：{0}", title);
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
        /// 更新多选题
        /// </summary>
        [HttpPost]
        public ActionResult UpdateMultiple()
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
                MultipleItem multiple = new MultipleItem();
                multiple.ID = Convert.ToInt32(idString);
                multiple.IsVipItem = Convert.ToInt32(isVipItemString);
                multiple.ChapterID = Convert.ToInt32(chapterIDString);

                multiple.Title = title;
                multiple.A = a;
                multiple.B = b;
                multiple.C = c;
                multiple.D = d;
                multiple.Answer = answer;
                multiple.Annotation = annotation;
                multiple.Difficulty = Convert.ToInt32(difficultyString);

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
                    multiple.Image = imageBytes;
                }

                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.UpdateMultiple, currentAdmin.Agency.ID, multiple.ID);
                if (checkResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    result = itemDataService.UpdateMultiple(multiple);

                    // Write admin do record.
                    if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                    {
                        AdminDoRecord doRecord = new AdminDoRecord();
                        doRecord.AdminID = currentAdmin.ID;
                        doRecord.AdminName = currentAdmin.ChineseName;
                        doRecord.DoTime = DateTime.Now;
                        doRecord.DoName = DoActionType.UpdateMultiple.GetDescription();
                        doRecord.DoContent = string.Format("被更新的主键ID：{0}", idString);
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
        /// 删除多选题
        /// </summary>
        [HttpPost]
        public ActionResult DeleteMultiple()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");

            ServiceInvokeDTO result = null;
            try
            {
                int id = Convert.ToInt32(idString);

                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.DeleteMultiple, currentAdmin.Agency.ID, id);
                if (checkResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    result = itemDataService.DeleteMultiple(id);

                    // Write admin do record.
                    if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                    {
                        AdminDoRecord doRecord = new AdminDoRecord();
                        doRecord.AdminID = currentAdmin.ID;
                        doRecord.AdminName = currentAdmin.ChineseName;
                        doRecord.DoTime = DateTime.Now;
                        doRecord.DoName = DoActionType.DeleteMultiple.GetDescription();
                        doRecord.DoContent = string.Format("被删除的主键ID：{0}", idString);
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
        /// 删除多选题(批量)
        /// </summary>
        [HttpPost]
        public ActionResult DeleteMultipleInBatch()
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
                    ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.DeleteMultipleInBatch, currentAdmin.Agency.ID, id);
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
                    result = itemDataService.DeleteMultiple(idList);

                    // Write admin do record.
                    if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                    {
                        AdminDoRecord doRecord = new AdminDoRecord();
                        doRecord.AdminID = currentAdmin.ID;
                        doRecord.AdminName = currentAdmin.ChineseName;
                        doRecord.DoTime = DateTime.Now;
                        doRecord.DoName = DoActionType.DeleteMultipleInBatch.GetDescription();
                        doRecord.DoContent = string.Format("被删除的主键ID：{0}", idListJson);
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

        /// <summary>
        /// 下载多选题题库模板文件
        /// </summary>
        [HttpGet]
        public ActionResult DownloadMultipleTemplateFile()
        {
            string templatePath = Server.MapPath("/") + @"Files\Template\template_item_multiple.xls";
            return DownloadUtil.Download("multiple_template.xls", templatePath);
        }

        /// <summary>
        /// 上传多选题题库Excel文件至服务器
        /// </summary>
        [HttpPost]
        public ActionResult UploadMultipleExcelFile()
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
        /// 开始导入多选题题库
        /// </summary>
        [HttpPost]
        public ActionResult StartLoadMultipleExcelFile()
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
                        List<MultipleItem> multiples = TemplateUtil.ReadMultipleTemplate(currentUser, currentCourse.ID, tempFilePath, true);

                        // 2.批量添加
                        result = itemDataService.AddMultiple(multiples);

                        // 3.删除文件
                        System.IO.File.Delete(tempFilePath);

                        result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);

                        // Write admin do record.
                        if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                        {
                            AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                            AdminDoRecord doRecord = new AdminDoRecord();
                            doRecord.AdminID = currentAdmin.ID;
                            doRecord.AdminName = currentAdmin.ChineseName;
                            doRecord.DoTime = DateTime.Now;
                            doRecord.DoName = DoActionType.AddMultipleInBatch.GetDescription();
                            doRecord.DoContent = string.Empty;
                            doRecord.Remark = string.Empty;
                            recordDataService.AddAdminDoRecord(doRecord);
                        }
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

        #region Judge

        /// <summary>
        /// 分页查询判断题
        /// </summary>
        [HttpGet]
        public ActionResult QueryJudge()
        {
            log.Debug(Constant.DEBUG_START);

            string pageSizeString = ApiQueryUtil.QueryArgByGet("limit");
            string offsetString = ApiQueryUtil.QueryArgByGet("offset");

            string chapterIDString = ApiQueryUtil.QueryArgByGet("chapter_id");
            string isVipItemString = ApiQueryUtil.QueryArgByGet("is_vip_item");
            string title = ApiQueryUtil.QueryArgByGet("title");
            string difficultyString = ApiQueryUtil.QueryArgByGet("difficulty");
            string addPerson = ApiQueryUtil.QueryArgByGet("add_person");

            QueryResultDTO<JudgeItemDTO> queryData = null;
            try
            {
                QueryArgsDTO<JudgeItem> queryDTO = new QueryArgsDTO<JudgeItem>();
                queryDTO.PageSize = Convert.ToInt32(pageSizeString);
                queryDTO.PageIndex = Convert.ToInt32(offsetString) / Convert.ToInt32(pageSizeString) + 1;

                queryDTO.Model.AgencyID = (Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO).Agency.ID;
                queryDTO.Model.ChapterID = Convert.ToInt32(chapterIDString);
                queryDTO.Model.IsVipItem = Convert.ToInt32(isVipItemString);
                queryDTO.Model.Title = title;
                queryDTO.Model.Difficulty = string.IsNullOrEmpty(difficultyString) ? -1 : Convert.ToInt32(difficultyString);
                queryDTO.Model.AddPerson = addPerson;

                int courseID = (Session[Constant.SESSION_KEY_COURSE] as Course).ID;

                queryData = itemDataService.QueryJudge(queryDTO, courseID).Data;
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
        /// 添加判断题
        /// </summary>
        [HttpPost]
        public ActionResult AddJudge()
        {
            log.Debug(Constant.DEBUG_START);

            string isVipItemString = ApiQueryUtil.QueryArgByPost("is_vip_item");
            string chapterIDString = ApiQueryUtil.QueryArgByPost("chapter_id");

            string title = ApiQueryUtil.QueryArgByPost("title");
            HttpPostedFileBase imageFile = Request.Files["image"];
            string answerString = ApiQueryUtil.QueryArgByPost("answer");
            string annotation = ApiQueryUtil.QueryArgByPost("annotation");
            string difficultyString = ApiQueryUtil.QueryArgByPost("difficulty");

            ServiceInvokeDTO result = null;
            try
            {
                JudgeItem judge = new JudgeItem();
                judge.AgencyID = (Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO).Agency.ID;
                judge.IsVipItem = Convert.ToInt32(isVipItemString);
                judge.ChapterID = Convert.ToInt32(chapterIDString);

                judge.Title = title;
                judge.Answer = Convert.ToInt32(answerString);
                judge.Annotation = annotation;
                judge.Difficulty = Convert.ToInt32(difficultyString);
                judge.AddPerson = (Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO).ChineseName;

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
                    judge.Image = imageBytes;
                }

                result = itemDataService.AddJudge(judge);

                // Write admin do record.
                if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                    AdminDoRecord doRecord = new AdminDoRecord();
                    doRecord.AdminID = currentAdmin.ID;
                    doRecord.AdminName = currentAdmin.ChineseName;
                    doRecord.DoTime = DateTime.Now;
                    doRecord.DoName = DoActionType.AddJudge.GetDescription();
                    doRecord.DoContent = string.Format("新添加的判断题标题：{0}", title);
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
        /// 更新判断题
        /// </summary>
        [HttpPost]
        public ActionResult UpdateJudge()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");
            string isVipItemString = ApiQueryUtil.QueryArgByPost("is_vip_item");
            string chapterIDString = ApiQueryUtil.QueryArgByPost("chapter_id");

            string title = ApiQueryUtil.QueryArgByPost("title");
            HttpPostedFileBase imageFile = Request.Files["image"];
            string answerString = ApiQueryUtil.QueryArgByPost("answer");
            string annotation = ApiQueryUtil.QueryArgByPost("annotation");
            string difficultyString = ApiQueryUtil.QueryArgByPost("difficulty");

            ServiceInvokeDTO result = null;
            try
            {
                JudgeItem judge = new JudgeItem();
                judge.ID = Convert.ToInt32(idString);
                judge.IsVipItem = Convert.ToInt32(isVipItemString);
                judge.ChapterID = Convert.ToInt32(chapterIDString);

                judge.Title = title;
                judge.Answer = Convert.ToInt32(answerString);
                judge.Annotation = annotation;
                judge.Difficulty = Convert.ToInt32(difficultyString);

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
                    judge.Image = imageBytes;
                }

                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.UpdateJudge, currentAdmin.Agency.ID, judge.ID);
                if (checkResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    result = itemDataService.UpdateJudge(judge);

                    // Write admin do record.
                    if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                    {
                        AdminDoRecord doRecord = new AdminDoRecord();
                        doRecord.AdminID = currentAdmin.ID;
                        doRecord.AdminName = currentAdmin.ChineseName;
                        doRecord.DoTime = DateTime.Now;
                        doRecord.DoName = DoActionType.UpdateJudge.GetDescription();
                        doRecord.DoContent = string.Format("被更新的主键ID：{0}", idString);
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
        /// 删除判断题
        /// </summary>
        [HttpPost]
        public ActionResult DeleteJudge()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");

            ServiceInvokeDTO result = null;
            try
            {
                int id = Convert.ToInt32(idString);

                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.DeleteJudge, currentAdmin.Agency.ID, id);
                if (checkResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    result = itemDataService.DeleteJudge(id);

                    // Write admin do record.
                    if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                    {
                        AdminDoRecord doRecord = new AdminDoRecord();
                        doRecord.AdminID = currentAdmin.ID;
                        doRecord.AdminName = currentAdmin.ChineseName;
                        doRecord.DoTime = DateTime.Now;
                        doRecord.DoName = DoActionType.DeleteJudge.GetDescription();
                        doRecord.DoContent = string.Format("被删除的主键ID：{0}", idString);
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
        /// 删除判断题(批量)
        /// </summary>
        [HttpPost]
        public ActionResult DeleteJudgeInBatch()
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
                    ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.DeleteJudgeInBatch, currentAdmin.Agency.ID, id);
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
                    result = itemDataService.DeleteJudge(idList);

                    // Write admin do record.
                    if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                    {
                        AdminDoRecord doRecord = new AdminDoRecord();
                        doRecord.AdminID = currentAdmin.ID;
                        doRecord.AdminName = currentAdmin.ChineseName;
                        doRecord.DoTime = DateTime.Now;
                        doRecord.DoName = DoActionType.DeleteJudgeInBatch.GetDescription();
                        doRecord.DoContent = string.Format("被删除的主键ID：{0}", idListJson);
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

        /// <summary>
        /// 下载判断题题库模板文件
        /// </summary>
        [HttpGet]
        public ActionResult DownloadJudgeTemplateFile()
        {
            string templatePath = Server.MapPath("/") + @"Files\Template\template_item_judge.xls";
            return DownloadUtil.Download("judge_template.xls", templatePath);
        }

        /// <summary>
        /// 上传判断题题库Excel文件至服务器
        /// </summary>
        [HttpPost]
        public ActionResult UploadJudgeExcelFile()
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
        /// 开始导入判断题题库
        /// </summary>
        [HttpPost]
        public ActionResult StartLoadJudgeExcelFile()
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
                        List<JudgeItem> multiples = TemplateUtil.ReadJudgeTemplate(currentUser, currentCourse.ID, tempFilePath, true);

                        // 2.批量添加
                        result = itemDataService.AddJudge(multiples);

                        // 3.删除文件
                        System.IO.File.Delete(tempFilePath);

                        result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);

                        // Write admin do record.
                        if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                        {
                            AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                            AdminDoRecord doRecord = new AdminDoRecord();
                            doRecord.AdminID = currentAdmin.ID;
                            doRecord.AdminName = currentAdmin.ChineseName;
                            doRecord.DoTime = DateTime.Now;
                            doRecord.DoName = DoActionType.AddJudgeInBatch.GetDescription();
                            doRecord.DoContent = string.Empty;
                            doRecord.Remark = string.Empty;
                            recordDataService.AddAdminDoRecord(doRecord);
                        }
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

        #region Uncertain

        /// <summary>
        /// 分页查询不定项选择题
        /// </summary>
        [HttpGet]
        public ActionResult QueryUncertain()
        {
            log.Debug(Constant.DEBUG_START);

            string pageSizeString = ApiQueryUtil.QueryArgByGet("limit");
            string offsetString = ApiQueryUtil.QueryArgByGet("offset");

            string chapterIDString = ApiQueryUtil.QueryArgByGet("chapter_id");
            string isVipItemString = ApiQueryUtil.QueryArgByGet("is_vip_item");
            string title = ApiQueryUtil.QueryArgByGet("title");
            string difficultyString = ApiQueryUtil.QueryArgByGet("difficulty");
            string addPerson = ApiQueryUtil.QueryArgByGet("add_person");

            QueryResultDTO<UncertainItemDTO> queryData = null;
            try
            {
                QueryArgsDTO<UncertainItem> queryDTO = new QueryArgsDTO<UncertainItem>();
                queryDTO.PageSize = Convert.ToInt32(pageSizeString);
                queryDTO.PageIndex = Convert.ToInt32(offsetString) / Convert.ToInt32(pageSizeString) + 1;

                queryDTO.Model.AgencyID = (Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO).Agency.ID;
                queryDTO.Model.ChapterID = Convert.ToInt32(chapterIDString);
                queryDTO.Model.IsVipItem = Convert.ToInt32(isVipItemString);
                queryDTO.Model.Title = title;
                queryDTO.Model.Difficulty = string.IsNullOrEmpty(difficultyString) ? -1 : Convert.ToInt32(difficultyString);
                queryDTO.Model.AddPerson = addPerson;

                int courseID = (Session[Constant.SESSION_KEY_COURSE] as Course).ID;

                queryData = itemDataService.QueryUncertain(queryDTO, courseID).Data;
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
        /// 添加不定项选择题
        /// </summary>
        [HttpPost]
        public ActionResult AddUncertain()
        {
            log.Debug(Constant.DEBUG_START);

            string isVipItemString = ApiQueryUtil.QueryArgByPost("is_vip_item");
            string chapterIDString = ApiQueryUtil.QueryArgByPost("chapter_id");

            string title = ApiQueryUtil.QueryArgByPost("title");
            HttpPostedFileBase imageFile = Request.Files["image"];
            string difficultyString = ApiQueryUtil.QueryArgByPost("difficulty");

            ServiceInvokeDTO result = null;
            try
            {
                UncertainItem uncertain = new UncertainItem();
                uncertain.AgencyID = (Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO).Agency.ID;
                uncertain.IsVipItem = Convert.ToInt32(isVipItemString);
                uncertain.ChapterID = Convert.ToInt32(chapterIDString);

                uncertain.Title = title;
                uncertain.Difficulty = Convert.ToInt32(difficultyString);
                uncertain.AddPerson = (Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO).ChineseName;

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
                    uncertain.Image = imageBytes;
                }

                result = itemDataService.AddUncertain(uncertain);
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
        /// 更新不定项选择题
        /// </summary>
        [HttpPost]
        public ActionResult UpdateUncertain()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");
            string isVipItemString = ApiQueryUtil.QueryArgByPost("is_vip_item");
            string chapterIDString = ApiQueryUtil.QueryArgByPost("chapter_id");

            string title = ApiQueryUtil.QueryArgByPost("title");
            HttpPostedFileBase imageFile = Request.Files["image"];
            string difficultyString = ApiQueryUtil.QueryArgByPost("difficulty");

            ServiceInvokeDTO result = null;
            try
            {
                UncertainItem uncertain = new UncertainItem();
                uncertain.ID = Convert.ToInt32(idString);
                uncertain.IsVipItem = Convert.ToInt32(isVipItemString);
                uncertain.ChapterID = Convert.ToInt32(chapterIDString);

                uncertain.Title = title;
                uncertain.Difficulty = Convert.ToInt32(difficultyString);

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
                    uncertain.Image = imageBytes;
                }

                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.UpdateUncertain, currentAdmin.Agency.ID, uncertain.ID);
                if (checkResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    result = itemDataService.UpdateUncertain(uncertain);
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
        /// 删除不定项选择题
        /// </summary>
        [HttpPost]
        public ActionResult DeleteUncertain()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");

            ServiceInvokeDTO result = null;
            try
            {
                int id = Convert.ToInt32(idString);

                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.DeleteUncertain, currentAdmin.Agency.ID, id);
                if (checkResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    result = itemDataService.DeleteUncertain(id);
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
        /// 删除不定项选择题(批量)
        /// </summary>
        [HttpPost]
        public ActionResult DeleteUncertainInBatch()
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
                    ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.DeleteUncertainInBatch, currentAdmin.Agency.ID, id);
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
                    result = itemDataService.DeleteUncertain(idList);
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

        //=========================== 子选择题  ============================

        /// <summary>
        /// 获取所有子选择题 
        /// </summary>
        [HttpGet]
        public ActionResult GetAllUncertainSubChoice()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByGet("uncertain_id");

            List<UncertainSubChoice> subchoices = null;
            try
            {
                int uncertainID = Convert.ToInt32(idString);

                ServiceInvokeDTO<List<UncertainSubChoice>> result = itemDataService.GetUncertainSubChoiceByUncertainID(uncertainID);
                if (result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    subchoices = result.Data;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }

            string json = JsonConvert.SerializeObject(new { data = subchoices }, Formatting.Indented, Constant.TIME_CONVERTER);
            log.Debug(Constant.DEBUG_END);

            return Content(json, Constant.JSON_MIME_TYPE);
        }

        /// <summary>
        /// 添加子选择题
        /// </summary>
        [HttpPost]
        public ActionResult AddUncertainSubChoice()
        {
            log.Debug(Constant.DEBUG_START);

            string uncertainIDString = ApiQueryUtil.QueryArgByPost("uncertain_id");

            string title = ApiQueryUtil.QueryArgByPost("title");
            HttpPostedFileBase imageFile = Request.Files["image"];
            string a = ApiQueryUtil.QueryArgByPost("a");
            string b = ApiQueryUtil.QueryArgByPost("b");
            string c = ApiQueryUtil.QueryArgByPost("c");
            string d = ApiQueryUtil.QueryArgByPost("d");
            string answer = ApiQueryUtil.QueryArgByPost("answer");
            string annotation = ApiQueryUtil.QueryArgByPost("annotation");

            ServiceInvokeDTO result = null;
            try
            {
                UncertainSubChoice subchoice = new UncertainSubChoice();
                subchoice.UncertainItemID = Convert.ToInt32(uncertainIDString);

                subchoice.Title = title;
                subchoice.A = a;
                subchoice.B = b;
                subchoice.C = c;
                subchoice.D = d;
                subchoice.Answer = answer;
                subchoice.Annotation = annotation;
                subchoice.AddPerson = (Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO).ChineseName;

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
                    subchoice.Image = imageBytes;
                }

                result = itemDataService.AddUncertainSubChoice(subchoice);
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
        /// 更新子选择题
        /// </summary>
        [HttpPost]
        public ActionResult UpdateUncertainSubChoice()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");

            string title = ApiQueryUtil.QueryArgByPost("title");
            HttpPostedFileBase imageFile = Request.Files["image"];
            string a = ApiQueryUtil.QueryArgByPost("a");
            string b = ApiQueryUtil.QueryArgByPost("b");
            string c = ApiQueryUtil.QueryArgByPost("c");
            string d = ApiQueryUtil.QueryArgByPost("d");
            string answer = ApiQueryUtil.QueryArgByPost("answer");
            string annotation = ApiQueryUtil.QueryArgByPost("annotation");

            ServiceInvokeDTO result = null;
            try
            {
                UncertainSubChoice subchoice = new UncertainSubChoice();
                subchoice.ID = Convert.ToInt32(idString);

                subchoice.Title = title;
                subchoice.A = a;
                subchoice.B = b;
                subchoice.C = c;
                subchoice.D = d;
                subchoice.Answer = answer;
                subchoice.Annotation = annotation;

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
                    subchoice.Image = imageBytes;
                }

                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.UpdateUncertainSubChoice, currentAdmin.Agency.ID, subchoice.ID);
                if (checkResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    result = itemDataService.UpdateUncertainSubChoice(subchoice);
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
        /// 上调不定项子选择题题目序号
        /// </summary>
        [HttpPost]
        public ActionResult UpUncertainSubChoice()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");

            ServiceInvokeDTO result = null;
            try
            {
                int id = Convert.ToInt32(idString);

                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.UpUncertainSubChoice, currentAdmin.Agency.ID, id);
                if (checkResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    result = itemDataService.UpUncertainSubChoice(id);
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
        /// 下调不定项子选择题题目序号
        /// </summary>
        [HttpPost]
        public ActionResult DownUncertainSubChoice()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");

            ServiceInvokeDTO result = null;
            try
            {
                int id = Convert.ToInt32(idString);

                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.DownUncertainSubChoice, currentAdmin.Agency.ID, id);
                if (checkResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    result = itemDataService.DownUncertainSubChoice(id);
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
        /// 删除子选择题
        /// </summary>
        [HttpPost]
        public ActionResult DeleteUncertainSubChoice()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");

            ServiceInvokeDTO result = null;
            try
            {
                int id = Convert.ToInt32(idString);

                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.DeleteUncertainSubChoice, currentAdmin.Agency.ID, id);
                if (checkResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    result = itemDataService.DeleteUncertainSubChoice(id);
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
        /// 删除子选择题(批量)
        /// </summary>
        [HttpPost]
        public ActionResult DeleteUncertainSubChoiceInBatch()
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
                    ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.DeleteUncertainSubChoiceInBatch, currentAdmin.Agency.ID, id);
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
                    result = itemDataService.DeleteUncertainSubChoiceInBatch(idList);
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

        #region FenLu

        /// <summary>
        /// 分页查询分录题
        /// </summary>
        [HttpGet]
        public ActionResult QueryFenLu()
        {
            log.Debug(Constant.DEBUG_START);

            string pageSizeString = ApiQueryUtil.QueryArgByGet("limit");
            string offsetString = ApiQueryUtil.QueryArgByGet("offset");

            string chapterIDString = ApiQueryUtil.QueryArgByGet("chapter_id");
            string isVipItemString = ApiQueryUtil.QueryArgByGet("is_vip_item");
            string title = ApiQueryUtil.QueryArgByGet("title");
            string difficultyString = ApiQueryUtil.QueryArgByGet("difficulty");
            string addPerson = ApiQueryUtil.QueryArgByGet("add_person");

            QueryResultDTO<FenLuItemDTO> queryData = null;
            try
            {
                QueryArgsDTO<FenLuItem> queryDTO = new QueryArgsDTO<FenLuItem>();
                queryDTO.PageSize = Convert.ToInt32(pageSizeString);
                queryDTO.PageIndex = Convert.ToInt32(offsetString) / Convert.ToInt32(pageSizeString) + 1;

                queryDTO.Model.AgencyID = (Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO).Agency.ID;
                queryDTO.Model.ChapterID = Convert.ToInt32(chapterIDString);
                queryDTO.Model.IsVipItem = Convert.ToInt32(isVipItemString);
                queryDTO.Model.Title = title;
                queryDTO.Model.Difficulty = string.IsNullOrEmpty(difficultyString) ? -1 : Convert.ToInt32(difficultyString);
                queryDTO.Model.AddPerson = addPerson;

                int courseID = (Session[Constant.SESSION_KEY_COURSE] as Course).ID;

                queryData = itemDataService.QueryFenLu(queryDTO, courseID).Data;
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
        /// 添加分录题
        /// </summary>
        [HttpPost]
        public ActionResult AddFenLu()
        {
            log.Debug(Constant.DEBUG_START);

            string isVipItemString = ApiQueryUtil.QueryArgByPost("is_vip_item");
            string chapterIDString = ApiQueryUtil.QueryArgByPost("chapter_id");
            string title = ApiQueryUtil.QueryArgByPost("title");
            HttpPostedFileBase imageFile = Request.Files["image"];
            string difficultyString = ApiQueryUtil.QueryArgByPost("difficulty");

            string answersJsonString = ApiQueryUtil.QueryArgByPost("answers");

            ServiceInvokeDTO result = null;
            try
            {
                FenLuItem fenlu = new FenLuItem();
                fenlu.AgencyID = (Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO).Agency.ID;
                fenlu.IsVipItem = Convert.ToInt32(isVipItemString);
                fenlu.ChapterID = Convert.ToInt32(chapterIDString);
                fenlu.Title = title;
                fenlu.Difficulty = Convert.ToInt32(difficultyString);
                fenlu.AddPerson = (Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO).ChineseName;

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
                    fenlu.Image = imageBytes;
                }

                List<FenLuAnswer> answers = JsonConvert.DeserializeObject<List<FenLuAnswer>>(answersJsonString);

                result = itemDataService.AddFenLu(fenlu, answers);
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
        /// 更新分录题
        /// </summary>
        [HttpPost]
        public ActionResult UpdateFenLu()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");
            string isVipItemString = ApiQueryUtil.QueryArgByPost("is_vip_item");
            string chapterIDString = ApiQueryUtil.QueryArgByPost("chapter_id");
            string title = ApiQueryUtil.QueryArgByPost("title");
            HttpPostedFileBase imageFile = Request.Files["image"];
            string difficultyString = ApiQueryUtil.QueryArgByPost("difficulty");

            string answersJsonString = ApiQueryUtil.QueryArgByPost("answers");

            ServiceInvokeDTO result = null;
            try
            {
                FenLuItem fenlu = new FenLuItem();
                fenlu.ID = Convert.ToInt32(idString);
                fenlu.IsVipItem = Convert.ToInt32(isVipItemString);
                fenlu.ChapterID = Convert.ToInt32(chapterIDString);
                fenlu.Title = title;
                fenlu.Difficulty = Convert.ToInt32(difficultyString);

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
                    fenlu.Image = imageBytes;
                }

                List<FenLuAnswer> answers = JsonConvert.DeserializeObject<List<FenLuAnswer>>(answersJsonString);

                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.UpdateFenLu, currentAdmin.Agency.ID, fenlu.ID);
                if (checkResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    result = itemDataService.UpdateFenLu(fenlu, answers);
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
        /// 删除分录题
        /// </summary>
        [HttpPost]
        public ActionResult DeleteFenLu()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");

            ServiceInvokeDTO result = null;
            try
            {
                int id = Convert.ToInt32(idString);

                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.DeleteFenLu, currentAdmin.Agency.ID, id);
                if (checkResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    result = itemDataService.DeleteFenLu(id);
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
        /// 删除分录题(批量)
        /// </summary>
        [HttpPost]
        public ActionResult DeleteFenLuInBatch()
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
                    ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.DeleteFenLuInBatch, currentAdmin.Agency.ID, id);
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
                    result = itemDataService.DeleteFenLu(idList);
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

        #region NumberBlank

        /// <summary>
        /// 分页查询数字填空题
        /// </summary>
        [HttpGet]
        public ActionResult QueryNumberBlank()
        {
            log.Debug(Constant.DEBUG_START);

            string pageSizeString = ApiQueryUtil.QueryArgByGet("limit");
            string offsetString = ApiQueryUtil.QueryArgByGet("offset");

            string chapterIDString = ApiQueryUtil.QueryArgByGet("chapter_id");
            string isVipItemString = ApiQueryUtil.QueryArgByGet("is_vip_item");
            string title = ApiQueryUtil.QueryArgByGet("title");
            string difficultyString = ApiQueryUtil.QueryArgByGet("difficulty");
            string addPerson = ApiQueryUtil.QueryArgByGet("add_person");

            QueryResultDTO<NumberBlankItemDTO> queryData = null;
            try
            {
                QueryArgsDTO<NumberBlankItem> queryDTO = new QueryArgsDTO<NumberBlankItem>();
                queryDTO.PageSize = Convert.ToInt32(pageSizeString);
                queryDTO.PageIndex = Convert.ToInt32(offsetString) / Convert.ToInt32(pageSizeString) + 1;

                queryDTO.Model.AgencyID = (Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO).Agency.ID;
                queryDTO.Model.ChapterID = Convert.ToInt32(chapterIDString);
                queryDTO.Model.IsVipItem = Convert.ToInt32(isVipItemString);
                queryDTO.Model.Title = title;
                queryDTO.Model.Difficulty = string.IsNullOrEmpty(difficultyString) ? -1 : Convert.ToInt32(difficultyString);
                queryDTO.Model.AddPerson = addPerson;

                int courseID = (Session[Constant.SESSION_KEY_COURSE] as Course).ID;

                queryData = itemDataService.QueryNumberBlank(queryDTO, courseID).Data;
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
        /// 添加数字填空题
        /// </summary>
        [HttpPost]
        public ActionResult AddNumberBlank()
        {
            log.Debug(Constant.DEBUG_START);

            string isVipItemString = ApiQueryUtil.QueryArgByPost("is_vip_item");
            string chapterIDString = ApiQueryUtil.QueryArgByPost("chapter_id");
            string title = ApiQueryUtil.QueryArgByPost("title");
            HttpPostedFileBase image1File = Request.Files["image1"];
            string image1Subtext = ApiQueryUtil.QueryArgByPost("image1_subtext");
            HttpPostedFileBase image2File = Request.Files["image2"];
            string image2Subtext = ApiQueryUtil.QueryArgByPost("image2_subtext");
            string difficultyString = ApiQueryUtil.QueryArgByPost("difficulty");

            string answersJsonString = ApiQueryUtil.QueryArgByPost("answers");

            ServiceInvokeDTO result = null;
            try
            {
                NumberBlankItem numberBlank = new NumberBlankItem();
                numberBlank.AgencyID = (Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO).Agency.ID;
                numberBlank.IsVipItem = Convert.ToInt32(isVipItemString);
                numberBlank.ChapterID = Convert.ToInt32(chapterIDString);
                numberBlank.Title = title;
                numberBlank.Image1SubText = image1Subtext;
                numberBlank.Image2SubText = image2Subtext;
                numberBlank.Difficulty = Convert.ToInt32(difficultyString);
                numberBlank.AddPerson = (Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO).ChineseName;

                if (image1File != null)
                {
                    byte[] imageBytes = null;
                    using (Stream inputStream = image1File.InputStream)
                    {
                        MemoryStream memoryStream = inputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            inputStream.CopyTo(memoryStream);
                        }
                        imageBytes = memoryStream.ToArray();
                    }
                    numberBlank.Image1 = imageBytes;
                }

                if (image2File != null)
                {
                    byte[] imageBytes = null;
                    using (Stream inputStream = image2File.InputStream)
                    {
                        MemoryStream memoryStream = inputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            inputStream.CopyTo(memoryStream);
                        }
                        imageBytes = memoryStream.ToArray();
                    }
                    numberBlank.Image2 = imageBytes;
                }

                List<NumberBlankAnswer> answers = JsonConvert.DeserializeObject<List<NumberBlankAnswer>>(answersJsonString);

                result = itemDataService.AddNumberBlank(numberBlank, answers);
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
        /// 更新数字填空题
        /// </summary>
        [HttpPost]
        public ActionResult UpdateNumberBlank()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");
            string isVipItemString = ApiQueryUtil.QueryArgByPost("is_vip_item");
            string chapterIDString = ApiQueryUtil.QueryArgByPost("chapter_id");
            string title = ApiQueryUtil.QueryArgByPost("title");
            HttpPostedFileBase image1File = Request.Files["image1"];
            string image1Subtext = ApiQueryUtil.QueryArgByPost("image1_subtext");
            HttpPostedFileBase image2File = Request.Files["image2"];
            string image2Subtext = ApiQueryUtil.QueryArgByPost("image2_subtext");
            string difficultyString = ApiQueryUtil.QueryArgByPost("difficulty");

            string answersJsonString = ApiQueryUtil.QueryArgByPost("answers");

            ServiceInvokeDTO result = null;
            try
            {
                NumberBlankItem numberBlank = new NumberBlankItem();
                numberBlank.ID = Convert.ToInt32(idString);
                numberBlank.IsVipItem = Convert.ToInt32(isVipItemString);
                numberBlank.ChapterID = Convert.ToInt32(chapterIDString);
                numberBlank.Title = title;
                numberBlank.Image1SubText = image1Subtext;
                numberBlank.Image2SubText = image2Subtext;
                numberBlank.Difficulty = Convert.ToInt32(difficultyString);

                if (image1File != null)
                {
                    byte[] imageBytes = null;
                    using (Stream inputStream = image1File.InputStream)
                    {
                        MemoryStream memoryStream = inputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            inputStream.CopyTo(memoryStream);
                        }
                        imageBytes = memoryStream.ToArray();
                    }
                    numberBlank.Image1 = imageBytes;
                }

                if (image2File != null)
                {
                    byte[] imageBytes = null;
                    using (Stream inputStream = image2File.InputStream)
                    {
                        MemoryStream memoryStream = inputStream as MemoryStream;
                        if (memoryStream == null)
                        {
                            memoryStream = new MemoryStream();
                            inputStream.CopyTo(memoryStream);
                        }
                        imageBytes = memoryStream.ToArray();
                    }
                    numberBlank.Image2 = imageBytes;
                }

                List<NumberBlankAnswer> answers = JsonConvert.DeserializeObject<List<NumberBlankAnswer>>(answersJsonString);

                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.UpdateNumberBlank, currentAdmin.Agency.ID, numberBlank.ID);
                if (checkResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    result = itemDataService.UpdateNumberBlank(numberBlank, answers);
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
        /// 删除数字填空题
        /// </summary>
        [HttpPost]
        public ActionResult DeleteNumberBlank()
        {
            log.Debug(Constant.DEBUG_START);

            string idString = ApiQueryUtil.QueryArgByPost("id");

            ServiceInvokeDTO result = null;
            try
            {
                int id = Convert.ToInt32(idString);

                AgencyAdminDTO currentAdmin = Session[Constant.SESSION_KEY_ADMIN] as AgencyAdminDTO;
                ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.DeleteNumberBlank, currentAdmin.Agency.ID, id);
                if (checkResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    result = itemDataService.DeleteNumberBlank(id);
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
        /// 删除数字填空题(批量)
        /// </summary>
        [HttpPost]
        public ActionResult DeleteNumberBlankInBatch()
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
                    ServiceInvokeDTO checkResult = permissionService.CheckPermission(DoActionType.DeleteNumberBlankInBatch, currentAdmin.Agency.ID, id);
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
                    result = itemDataService.DeleteNumberBlank(idList);
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
