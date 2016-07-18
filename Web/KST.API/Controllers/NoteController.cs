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
    /// 云笔记实体控制器
    /// </summary>
    public class NoteController : ApiController
    {
        private SecurityService securityService = ServiceFactory.Instance.SecurityService;
        private UserDataService userDataService = ServiceFactory.Instance.UserDataService;
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(NoteController));

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
                        case Cmd.NOTE_QUERY: response = QueryNote(Request); break;
                        case Cmd.NOTE_GET_BY_ID: response = GetNoteByID(Request); break;
                        case Cmd.NOTE_GET_ALL: response = GetNoteByUserID(Request); break;
                        case Cmd.NOTE_ADD: response = AddNote(Request); break;
                        case Cmd.NOTE_UPDATE: response = UpdateNote(Request); break;
                        case Cmd.NOTE_DELETE: response = DeleteNote(Request); break;
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
        /// 分页查询云笔记信息
        /// </summary>
        private HttpResponseMessage QueryNote(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string pageIndexString = ApiQueryUtil.QueryArgByGet("page_index");
            string pageSizeString = ApiQueryUtil.QueryArgByGet("page_size");
            string userIDString = ApiQueryUtil.QueryArgByGet("user_id");
            string title = ApiQueryUtil.QueryArgByGet("title");
            string content = ApiQueryUtil.QueryArgByGet("content");
            string startDateString = ApiQueryUtil.QueryArgByGet("start_date");
            string endDateString = ApiQueryUtil.QueryArgByGet("end_date");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd},
                { Constant.HTTP_HEADER_RANDOM, random},
                { "page_index", pageIndexString},
                { "page_size", pageSizeString},
                { "user_id", userIDString},
                { "title", title },
                { "content", content },
                { "start_date", startDateString },
                { "end_date", endDateString }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO<QueryResultDTO<Note>> result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    QueryArgsDTO<Note> queryDTO = new QueryArgsDTO<Note>();
                    queryDTO.PageIndex = Convert.ToInt32(pageIndexString);
                    queryDTO.PageSize = Convert.ToInt32(pageSizeString);
                    queryDTO.Model.UserID = string.IsNullOrEmpty(userIDString) ? -1 : Convert.ToInt32(userIDString);
                    queryDTO.Model.Title = title;
                    queryDTO.Model.Content = content;

                    DateTime startDate = string.IsNullOrEmpty(startDateString) ? DateTime.MinValue : Convert.ToDateTime(startDateString);
                    DateTime endDate = string.IsNullOrEmpty(endDateString) ? DateTime.MinValue : Convert.ToDateTime(endDateString);

                    result = userDataService.QueryNote(queryDTO, startDate, endDate);
                }
                else
                {
                    result = new ServiceInvokeDTO<QueryResultDTO<Note>>(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<QueryResultDTO<Note>>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 根据主键ID获取云笔记
        /// </summary>
        private HttpResponseMessage GetNoteByID(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string idString = ApiQueryUtil.QueryArgByGet("id");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd},
                { Constant.HTTP_HEADER_RANDOM, random},
                { "id", idString }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO<Note> result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    result = userDataService.GetNoteByID(Convert.ToInt32(idString));
                }
                else
                {
                    result = new ServiceInvokeDTO<Note>(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<Note>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 获取指定用户云笔记列表
        /// </summary>
        private HttpResponseMessage GetNoteByUserID(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string userIDString = ApiQueryUtil.QueryArgByGet("user_id");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd},
                { Constant.HTTP_HEADER_RANDOM, random},
                { "user_id", userIDString }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO<List<Note>> result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    result = userDataService.GetNoteByUserID(Convert.ToInt32(userIDString));
                }
                else
                {
                    result = new ServiceInvokeDTO<List<Note>>(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<List<Note>>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 添加云笔记
        /// </summary>
        private HttpResponseMessage AddNote(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string userIDString = ApiQueryUtil.QueryArgByPost("user_id");
            string title = ApiQueryUtil.QueryArgByPost("title");
            string content = ApiQueryUtil.QueryArgByPost("content");
            string createTimeString = ApiQueryUtil.QueryArgByPost("create_time");
            string remindTimeString = ApiQueryUtil.QueryArgByPost("remind_time");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd },
                { Constant.HTTP_HEADER_RANDOM, random },
                { "user_id", userIDString },
                { "title", title },
                { "content", content },
                { "create_time", createTimeString },
                { "remind_time", remindTimeString }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO<Note> result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    Note note = new Note();
                    note.UserID = Convert.ToInt32(userIDString);
                    note.Title = title;
                    note.Content = content;
                    note.CreateTime = Convert.ToDateTime(createTimeString);
                    note.RemindTime = Convert.ToDateTime(remindTimeString);

                    result = userDataService.AddNote(note);
                }
                else
                {
                    result = new ServiceInvokeDTO<Note>(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<Note>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 更新云笔记
        /// </summary>
        private HttpResponseMessage UpdateNote(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string idString = ApiQueryUtil.QueryArgByPost("id");
            string title = ApiQueryUtil.QueryArgByPost("title");
            string content = ApiQueryUtil.QueryArgByPost("content");
            string remindTimeString = ApiQueryUtil.QueryArgByPost("remind_time");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd },
                { Constant.HTTP_HEADER_RANDOM, random },
                { "id", idString },
                { "title", title },
                { "content", content },
                { "remind_time", remindTimeString }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    Note note = new Note();
                    note.ID = Convert.ToInt32(idString);
                    note.Title = title;
                    note.Content = content;
                    note.RemindTime = Convert.ToDateTime(remindTimeString);

                    result = userDataService.UpdateNote(note);
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
        /// 删除云笔记
        /// </summary>
        private HttpResponseMessage DeleteNote(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string idString = ApiQueryUtil.QueryArgByPost("id");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd },
                { Constant.HTTP_HEADER_RANDOM, random },
                { "id", idString }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    int id = Convert.ToInt32(idString);
                    result = userDataService.DeleteNote(id);
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
