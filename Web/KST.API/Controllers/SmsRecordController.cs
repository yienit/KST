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
    /// Sms短信发送记录实体控制器
    /// </summary>
    public class SmsRecordController : ApiController
    {
        private UserDataService userDataService = ServiceFactory.Instance.UserDataService;
        private SecurityService securityService = ServiceFactory.Instance.SecurityService;
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(SmsRecordController));

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
                        case Cmd.SMSRECORD_QUERY: response = QuerySmsRecord(Request); break;
                        case Cmd.SMSRECORD_GET_BY_ID: response = GetSmsRecordByID(Request); break;
                        case Cmd.SMSRECORD_GET_ALL: response = GetSmsRecordByUserID(Request); break;
                        case Cmd.SMSRECORD_ADD: response = AddSmsRecord(Request); break;
                        case Cmd.SMSRECORD_DELETE: response = DeleteSmsRecord(Request); break;
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
        /// 分页查询短信记录
        /// </summary>
        private HttpResponseMessage QuerySmsRecord(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string pageIndexString = ApiQueryUtil.QueryArgByGet("page_index");
            string pageSizeString = ApiQueryUtil.QueryArgByGet("page_size");
            string userIDString = ApiQueryUtil.QueryArgByGet("user_id");
            string receiverPhone = ApiQueryUtil.QueryArgByGet("receiver_phone");
            string receiverName = ApiQueryUtil.QueryArgByGet("receiver_name");
            string content = ApiQueryUtil.QueryArgByGet("content");
            string sendByTypeString = ApiQueryUtil.QueryArgByGet("send_by_type");
            string startDateString = ApiQueryUtil.QueryArgByGet("start_date");
            string endDateString = ApiQueryUtil.QueryArgByGet("end_date");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd},
                { Constant.HTTP_HEADER_RANDOM, random},
                { "page_index", pageIndexString},
                { "page_size", pageSizeString},
                { "user_id", userIDString},
                { "receiver_phone", receiverPhone },
                { "receiver_name", receiverName },
                { "content", content },
                { "send_by_type", sendByTypeString },
                { "start_date", startDateString },
                { "end_date", endDateString }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO<QueryResultDTO<SmsRecord>> result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    QueryArgsDTO<SmsRecord> queryDTO = new QueryArgsDTO<SmsRecord>();
                    queryDTO.PageIndex = Convert.ToInt32(pageIndexString);
                    queryDTO.PageSize = Convert.ToInt32(pageSizeString);
                    queryDTO.Model.UserID = string.IsNullOrEmpty(userIDString) ? -1 : Convert.ToInt32(userIDString);
                    queryDTO.Model.ReceiverPhone = receiverPhone;
                    queryDTO.Model.ReceiverName = receiverName;
                    queryDTO.Model.Content = content;
                    queryDTO.Model.SendByType = string.IsNullOrEmpty(sendByTypeString) ? -1 : Convert.ToInt32(sendByTypeString);

                    DateTime startDate = string.IsNullOrEmpty(startDateString) ? DateTime.MinValue : Convert.ToDateTime(startDateString);
                    DateTime endDate = string.IsNullOrEmpty(endDateString) ? DateTime.MinValue : Convert.ToDateTime(endDateString);

                    result = userDataService.QuerySmsRecord(queryDTO, startDate, endDate);
                }
                else
                {
                    result = new ServiceInvokeDTO<QueryResultDTO<SmsRecord>>(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<QueryResultDTO<SmsRecord>>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 根据主键ID获取短信记录
        /// </summary>
        private HttpResponseMessage GetSmsRecordByID(HttpRequestMessage request)
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

            ServiceInvokeDTO<SmsRecord> result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    result = userDataService.GetSmsRecordByID(Convert.ToInt32(idString));
                }
                else
                {
                    result = new ServiceInvokeDTO<SmsRecord>(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<SmsRecord>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 获取指定用户短信记录列表
        /// </summary>
        private HttpResponseMessage GetSmsRecordByUserID(HttpRequestMessage request)
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

            ServiceInvokeDTO<List<SmsRecord>> result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    result = userDataService.GetSmsRecordByUserID(Convert.ToInt32(userIDString));
                }
                else
                {
                    result = new ServiceInvokeDTO<List<SmsRecord>>(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<List<SmsRecord>>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 添加短信记录
        /// </summary>
        private HttpResponseMessage AddSmsRecord(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string userIDString = ApiQueryUtil.QueryArgByPost("user_id");
            string receiverPhone = ApiQueryUtil.QueryArgByPost("receiver_phone");
            string receiverName = ApiQueryUtil.QueryArgByPost("receiver_name");
            string content = ApiQueryUtil.QueryArgByPost("content");
            string sendByTypeString = ApiQueryUtil.QueryArgByPost("sendby_type");
            string sendTimeString = ApiQueryUtil.QueryArgByPost("send_time");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd },
                { Constant.HTTP_HEADER_RANDOM, random },
                { "user_id", userIDString },
                { "receiver_phone", receiverPhone },
                { "receiver_name", receiverName },
                { "content", content },
                { "sendby_type", sendByTypeString },
                { "send_time", sendTimeString }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO<SmsRecord> result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    SmsRecord smsRecord = new SmsRecord();
                    smsRecord.UserID = Convert.ToInt32(userIDString);
                    smsRecord.ReceiverPhone = receiverPhone;
                    smsRecord.ReceiverName = receiverName;
                    smsRecord.Content = content;
                    smsRecord.SendByType = Convert.ToInt32(sendByTypeString);
                    smsRecord.SendTime = Convert.ToDateTime(sendTimeString);

                    result = userDataService.AddSmsRecord(smsRecord);
                }
                else
                {
                    result = new ServiceInvokeDTO<SmsRecord>(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<SmsRecord>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 删除短信记录
        /// </summary>
        private HttpResponseMessage DeleteSmsRecord(HttpRequestMessage request)
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
                    result = userDataService.DeleteSmsRecord(id);
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
