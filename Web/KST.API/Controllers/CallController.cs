using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using KST.Core;
using KST.DTO;
using KST.Model;
using KST.Service;
using KST.Util;

namespace KST.API.Controllers
{
    /// <summary>
    /// 通话记录控制器
    /// </summary>
    public class CallController : ApiController
    {
        private UserDataService userDataService = ServiceFactory.Instance.UserDataService;
        private SecurityService securityService = ServiceFactory.Instance.SecurityService;
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(CallController));

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
                        case Cmd.CALL_QUERY: response = QueryCall(Request); break;
                        case Cmd.CALLRECORD_GET_BY_ID: response = GetCallRecordByID(Request); break;
                        case Cmd.CALLRECORD_GET_BY_DETAIL: response = GetCallRecordByDetail(Request); break;
                        case Cmd.CALLRECORD_GET_ALL: response = GetCallRecordByUserID(Request); break;
                        case Cmd.CALLRECORD_ADD: response = AddCallRecord(Request); break;
                        case Cmd.CALLRECORD_DELETE: response = DeleteCallRecord(Request); break;

                        case Cmd.CALLVOICE_GET_BY_ID: response = GetCallVoiceByID(Request); break;
                        case Cmd.CALLVOICE_GET_BY_CALL_ID: response = GetCallVoiceByCallID(Request); break;
                        case Cmd.CALLVOICE_GET_BY_DETAIL: response = GetCallVoiceByCallDetail(Request); break;
                        case Cmd.CALLVOICE_UPLOAD: response = AddCallVoice(Request); break;
                        case Cmd.CALLVOICE_DELETE: response = DeleteCallVoice(Request); break;
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
        /// 分页查询通话记录信息
        /// </summary>
        private HttpResponseMessage QueryCall(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string pageIndexString = ApiQueryUtil.QueryArgByGet("page_index");
            string pageSizeString = ApiQueryUtil.QueryArgByGet("page_size");

            string userIDString = ApiQueryUtil.QueryArgByGet("user_id");
            string typeString = ApiQueryUtil.QueryArgByGet("type");
            string number = ApiQueryUtil.QueryArgByPost("number");
            string name = ApiQueryUtil.QueryArgByPost("name");

            string startDateString = ApiQueryUtil.QueryArgByGet("start_date");
            string endDateString = ApiQueryUtil.QueryArgByGet("end_date");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd},
                { Constant.HTTP_HEADER_RANDOM, random},
                { "page_index", pageIndexString},
                { "page_size", pageSizeString},
                { "user_id", userIDString},
                { "type", typeString },
                { "number", number },
                { "name", name },
                { "start_date", startDateString },
                { "end_date", endDateString }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO<QueryResultDTO<CallRecordDTO>> result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    QueryArgsDTO<Agency> queryDTO = new QueryArgsDTO<Agency>();
                    queryDTO.PageIndex = Convert.ToInt32(pageIndexString);
                    queryDTO.PageSize = Convert.ToInt32(pageSizeString);

                    queryDTO.Model.UserID = string.IsNullOrEmpty(userIDString) ? -1 : Convert.ToInt32(userIDString);
                    queryDTO.Model.Type = string.IsNullOrEmpty(typeString) ? -1 : Convert.ToInt32(typeString);
                    queryDTO.Model.Number = number;
                    queryDTO.Model.Name = name;

                    DateTime startDate = string.IsNullOrEmpty(startDateString) ? DateTime.MinValue : Convert.ToDateTime(startDateString);
                    DateTime endDate = string.IsNullOrEmpty(endDateString) ? DateTime.MinValue : Convert.ToDateTime(endDateString);

                    result = userDataService.QueryCallRecord(queryDTO, startDate, endDate);
                }
                else
                {
                    result = new ServiceInvokeDTO<QueryResultDTO<CallRecordDTO>>(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<QueryResultDTO<CallRecordDTO>>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 根据主键ID获取通话记录
        /// </summary>
        private HttpResponseMessage GetCallRecordByID(HttpRequestMessage request)
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

            ServiceInvokeDTO<CallRecordDTO> result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    result = userDataService.GetCallRecordByID(Convert.ToInt32(idString));

                    // 设置DTO数据中的录音文件下载地址
                    if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                    {
                        CallRecordDTO dto = result.Data;
                        if (dto != null && dto.CallVoice != null)
                        {
                            dto.CallVoice.DownloadUrl = string.Format(Constant.CALLVOICE_DOWNLOAD_FORMAT,
                                request.Headers.Host, dto.CallVoice.FileSaveName);
                        }
                    }
                }
                else
                {
                    result = new ServiceInvokeDTO<CallRecordDTO>(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<CallRecordDTO>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 根据日期、通话类型、对方通话号码获取通话记录
        /// </summary>
        private HttpResponseMessage GetCallRecordByDetail(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string timeString = ApiQueryUtil.QueryArgByGet("time");
            string typeString = ApiQueryUtil.QueryArgByGet("type");
            string number = ApiQueryUtil.QueryArgByGet("number");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd},
                { Constant.HTTP_HEADER_RANDOM, random},
                { "time", timeString },
                { "type", typeString },
                { "number", number }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO<CallRecordDTO> result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    result = userDataService.GetCallRecordByDetail(Convert.ToDateTime(timeString), Convert.ToInt32(typeString), number);

                    // 设置DTO数据中的录音文件下载地址
                    if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                    {
                        CallRecordDTO dto = result.Data;
                        if (dto != null && dto.CallVoice != null)
                        {
                            dto.CallVoice.DownloadUrl = string.Format(Constant.CALLVOICE_DOWNLOAD_FORMAT,
                                request.Headers.Host, dto.CallVoice.FileSaveName);
                        }
                    }
                }
                else
                {
                    result = new ServiceInvokeDTO<CallRecordDTO>(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<CallRecordDTO>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 获取指定用户通话记录列表
        /// </summary>
        private HttpResponseMessage GetCallRecordByUserID(HttpRequestMessage request)
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

            ServiceInvokeDTO<List<CallRecordDTO>> result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    result = userDataService.GetCallRecordByUserID(Convert.ToInt32(userIDString));

                    // 设置DTO数据中的录音文件下载地址
                    if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS && result.Data != null)
                    {
                        foreach (var dto in result.Data)
                        {
                            if (dto != null && dto.CallVoice != null)
                            {
                                dto.CallVoice.DownloadUrl = string.Format(Constant.CALLVOICE_DOWNLOAD_FORMAT,
                                    request.Headers.Host, dto.CallVoice.FileSaveName);
                            }
                        }
                    }
                }
                else
                {
                    result = new ServiceInvokeDTO<List<CallRecordDTO>>(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<List<CallRecordDTO>>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 添加通话记录
        /// </summary>
        private HttpResponseMessage AddCallRecord(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string userIDString = ApiQueryUtil.QueryArgByPost("user_id");
            string timeString = ApiQueryUtil.QueryArgByPost("time");
            string duration = ApiQueryUtil.QueryArgByPost("duration");
            string typeString = ApiQueryUtil.QueryArgByPost("type");
            string number = ApiQueryUtil.QueryArgByPost("number");
            string name = ApiQueryUtil.QueryArgByPost("name");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd },
                { Constant.HTTP_HEADER_RANDOM, random },
                { "user_id", userIDString },
                { "time", timeString },
                { "duration", duration },
                { "type", typeString },
                { "number", number },
                { "name", name }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO<CallRecordDTO> result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    Agency callRecord = new Agency();
                    callRecord.UserID = Convert.ToInt32(userIDString);
                    callRecord.Time = Convert.ToDateTime(timeString);
                    callRecord.Duration = duration;
                    callRecord.Type = Convert.ToInt32(typeString);
                    callRecord.Number = number;
                    callRecord.Name = name;

                    result = userDataService.AddCallRecord(callRecord);

                    // 设置DTO数据中的录音文件下载地址
                    if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                    {
                        CallRecordDTO dto = result.Data;
                        if (dto != null && dto.CallVoice != null)
                        {
                            dto.CallVoice.DownloadUrl = string.Format(Constant.CALLVOICE_DOWNLOAD_FORMAT,
                                request.Headers.Host, dto.CallVoice.FileSaveName);
                        }
                    }
                }
                else
                {
                    result = new ServiceInvokeDTO<CallRecordDTO>(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<CallRecordDTO>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 删除通话记录
        /// </summary>
        private HttpResponseMessage DeleteCallRecord(HttpRequestMessage request)
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

                    try
                    {
                        // 删除通话记录录音文件
                        ServiceInvokeDTO<CallRecordDTO> recordResult = userDataService.GetCallRecordByID(id);
                        if (recordResult != null && recordResult.Code == InvokeCode.SYS_INVOKE_SUCCESS && recordResult.Data != null)
                        {
                            CallRecordDTO callRecord = recordResult.Data;
                            ServiceInvokeDTO<CallVoice> voiceResult = userDataService.GetCallVoiceByCallRecordID(callRecord.ID);
                            if (voiceResult != null && voiceResult.Code == InvokeCode.SYS_INVOKE_SUCCESS && voiceResult.Data != null)
                            {
                                string filePath = HttpContext.Current.Request.MapPath(string.Format("~/Files/CallVoices/{0}", voiceResult.Data.FileSaveName));
                                if (System.IO.File.Exists(filePath))
                                {
                                    System.IO.File.Delete(filePath);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Error(ex);
                    }

                    result = userDataService.DeleteCallRecord(id);
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

        //================================================================================

        /// <summary>
        /// 根据主键ID获取通话录音信息
        /// </summary>
        private HttpResponseMessage GetCallVoiceByID(HttpRequestMessage request)
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

            ServiceInvokeDTO<CallVoiceDTO> result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    ServiceInvokeDTO<CallVoice> getResult = userDataService.GetCallVoiceByID(Convert.ToInt32(idString));

                    // Model --> DTO
                    if (getResult != null && getResult.Code == InvokeCode.SYS_INVOKE_SUCCESS && getResult.Data != null)
                    {
                        CallVoiceDTO dto = new CallVoiceDTO(getResult.Data);
                        dto.DownloadUrl = string.Format(Constant.CALLVOICE_DOWNLOAD_FORMAT, request.Headers.Host, getResult.Data.FileSaveName);

                        result = new ServiceInvokeDTO<CallVoiceDTO>(InvokeCode.SYS_INVOKE_SUCCESS, dto);
                    }
                    else
                    {
                        result = new ServiceInvokeDTO<CallVoiceDTO>(getResult.Code);
                    }
                }
                else
                {
                    result = new ServiceInvokeDTO<CallVoiceDTO>(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<CallVoiceDTO>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 根据通话记录主键ID获取通话录音信息
        /// </summary>
        private HttpResponseMessage GetCallVoiceByCallID(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string callIDString = ApiQueryUtil.QueryArgByGet("call_id");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd},
                { Constant.HTTP_HEADER_RANDOM, random},
                { "call_id", callIDString }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO<CallVoiceDTO> result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    ServiceInvokeDTO<CallVoice> getResult = userDataService.GetCallVoiceByCallRecordID(Convert.ToInt32(callIDString));

                    // Model --> DTO
                    if (getResult != null && getResult.Code == InvokeCode.SYS_INVOKE_SUCCESS && getResult.Data != null)
                    {
                        CallVoiceDTO dto = new CallVoiceDTO(getResult.Data);
                        dto.DownloadUrl = string.Format(Constant.CALLVOICE_DOWNLOAD_FORMAT, request.Headers.Host, getResult.Data.FileSaveName);

                        result = new ServiceInvokeDTO<CallVoiceDTO>(InvokeCode.SYS_INVOKE_SUCCESS, dto);
                    }
                    else
                    {
                        result = new ServiceInvokeDTO<CallVoiceDTO>(getResult.Code);
                    }
                }
                else
                {
                    result = new ServiceInvokeDTO<CallVoiceDTO>(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<CallVoiceDTO>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 根据日期、通话类型、对方通话号码获取通话录音信息
        /// </summary>
        private HttpResponseMessage GetCallVoiceByCallDetail(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string timeString = ApiQueryUtil.QueryArgByGet("time");
            string typeString = ApiQueryUtil.QueryArgByGet("type");
            string number = ApiQueryUtil.QueryArgByGet("number");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd},
                { Constant.HTTP_HEADER_RANDOM, random},
                { "time", timeString },
                { "type", typeString },
                { "number", number }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO<CallVoiceDTO> result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    ServiceInvokeDTO<CallVoice> getResult = userDataService.GetCallVoiceByCallRecordDetail(Convert.ToDateTime(timeString), Convert.ToInt32(typeString), number);

                    // Model --> DTO
                    if (getResult != null && getResult.Code == InvokeCode.SYS_INVOKE_SUCCESS && getResult.Data != null)
                    {
                        CallVoiceDTO dto = new CallVoiceDTO(getResult.Data);
                        dto.DownloadUrl = string.Format(Constant.CALLVOICE_DOWNLOAD_FORMAT, request.Headers.Host, getResult.Data.FileSaveName);

                        result = new ServiceInvokeDTO<CallVoiceDTO>(InvokeCode.SYS_INVOKE_SUCCESS, dto);
                    }
                    else
                    {
                        result = new ServiceInvokeDTO<CallVoiceDTO>(getResult.Code);
                    }
                }
                else
                {
                    result = new ServiceInvokeDTO<CallVoiceDTO>(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<CallVoiceDTO>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 上传通话录音文件
        /// </summary>
        private HttpResponseMessage AddCallVoice(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string callIDString = ApiQueryUtil.QueryArgByPost("call_id");
            HttpPostedFile voiceFile = HttpContext.Current.Request.Files["file"];

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd },
                { Constant.HTTP_HEADER_RANDOM, random },
                { "call_id", callIDString }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO<CallVoiceDTO> result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    if (voiceFile != null)
                    {
                        // 删除已有数据和文件
                        int callID = Convert.ToInt32(callIDString);
                        ServiceInvokeDTO<CallVoice> oldCallVoiceDTO = userDataService.GetCallVoiceByCallRecordID(callID);
                        if (oldCallVoiceDTO != null && oldCallVoiceDTO.Code == InvokeCode.SYS_INVOKE_SUCCESS && oldCallVoiceDTO.Data != null)
                        {
                            userDataService.DeleteCallVoiceByCallRecordID(callID);
                            string oldFilePath = HttpContext.Current.Request.MapPath(string.Format("~/Files/CallVoices/{0}", oldCallVoiceDTO.Data.FileSaveName));
                            if (System.IO.File.Exists(oldFilePath))
                            {
                                System.IO.File.Delete(oldFilePath);
                            }
                        }

                        // 保存文件及数据
                        string filePostName = voiceFile.FileName;
                        string fileSaveName = Guid.NewGuid().ToString();
                        string ext = System.IO.Path.GetExtension(filePostName);

                        string filePath = HttpContext.Current.Request.MapPath(string.Format("~/Files/CallVoices/{0}{1}", fileSaveName, ext));
                        voiceFile.SaveAs(filePath);

                        CallVoice newCallVoice = new CallVoice();
                        newCallVoice.CallRecordID = callID;
                        newCallVoice.FilePostName = filePostName;
                        newCallVoice.FileSaveName = fileSaveName + ext;
                        newCallVoice.FileSize = voiceFile.ContentLength / 1024;
                        ServiceInvokeDTO<CallVoice> addResult = userDataService.AddCallVoice(newCallVoice);

                        // Model --> DTO
                        if (addResult != null && addResult.Code == InvokeCode.SYS_INVOKE_SUCCESS && addResult.Data != null)
                        {
                            CallVoiceDTO dto = new CallVoiceDTO(addResult.Data);
                            dto.DownloadUrl = string.Format(Constant.CALLVOICE_DOWNLOAD_FORMAT, request.Headers.Host, addResult.Data.FileSaveName);

                            result = new ServiceInvokeDTO<CallVoiceDTO>(InvokeCode.SYS_INVOKE_SUCCESS, dto);
                        }
                        else
                        {
                            result = new ServiceInvokeDTO<CallVoiceDTO>(addResult.Code);
                        }
                    }
                    else
                    {
                        result = new ServiceInvokeDTO<CallVoiceDTO>(InvokeCode.CALLVOICE_FILE_NEEDED);
                    }
                }
                else
                {
                    result = new ServiceInvokeDTO<CallVoiceDTO>(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<CallVoiceDTO>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 删除通话录音文件
        /// </summary>
        private HttpResponseMessage DeleteCallVoice(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string callIDString = ApiQueryUtil.QueryArgByPost("call_id");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd },
                { Constant.HTTP_HEADER_RANDOM, random },
                { "call_id", callIDString }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    int id = Convert.ToInt32(callIDString);

                    // 删除通话记录录音文件
                    ServiceInvokeDTO<CallRecordDTO> recordResult = userDataService.GetCallRecordByID(id);
                    if (recordResult != null && recordResult.Code == InvokeCode.SYS_INVOKE_SUCCESS && recordResult.Data != null)
                    {
                        CallRecordDTO callRecord = recordResult.Data;
                        ServiceInvokeDTO<CallVoice> voiceResult = userDataService.GetCallVoiceByCallRecordID(callRecord.ID);
                        if (voiceResult != null && voiceResult.Code == InvokeCode.SYS_INVOKE_SUCCESS && voiceResult.Data != null)
                        {
                            string filePath = HttpContext.Current.Request.MapPath(string.Format("~/Files/CallVoices/{0}", voiceResult.Data.FileSaveName));
                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                            }
                        }
                    }

                    // 删除录音数据
                    result = userDataService.DeleteCallVoiceByCallRecordID(id);
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
