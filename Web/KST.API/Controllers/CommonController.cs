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
    /// 常用辅助控制器
    /// </summary>
    public class CommonController : ApiController
    {
        private SecurityService securityService = ServiceFactory.Instance.SecurityService;
        private CommonService commonService = ServiceFactory.Instance.CommonService;
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(CommonController));

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
                        case Cmd.COMMON_ADD_FEEDBACK: response = AddFeedBack(Request); break;
                        case Cmd.COMMON_GET_NEW_VERSION: response = GetNewVersion(Request); break;
                        case Cmd.COMMON_GET_UTC: response = GetServerTime(Request); break;
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
        /// 添加意见反馈
        /// </summary>
        private HttpResponseMessage AddFeedBack(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string content = ApiQueryUtil.QueryArgByPost("content");
            string contact = ApiQueryUtil.QueryArgByPost("contact");
            string terminalTypeString = ApiQueryUtil.QueryArgByPost("terminal_type");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd },
                { Constant.HTTP_HEADER_RANDOM, random },
                { "content", content },
                { "contact", contact },
                { "terminal_type", terminalTypeString }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    FeedBack feedback = new FeedBack();
                    feedback.Content = content;
                    feedback.Contact = contact;
                    feedback.TerminalType = Convert.ToInt32(terminalTypeString);

                    result = commonService.AddFeedBack(feedback);
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
        /// 获取客户端最新版本
        /// </summary>
        private HttpResponseMessage GetNewVersion(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string terminalTypeString = ApiQueryUtil.QueryArgByGet("terminal_type");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd },
                { Constant.HTTP_HEADER_RANDOM, random },
                { "terminal_type", terminalTypeString }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO<AppVersionDTO> result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    int terminalType = Convert.ToInt32(terminalTypeString);
                    switch (terminalType)
                    {
                        case Service.Constant.TERMINAL_TYPE_PC:
                            result = new ServiceInvokeDTO<AppVersionDTO>(InvokeCode.SYS_INVOKE_SUCCESS, Config.PCVersion);
                            break;
                        case Service.Constant.TERMINAL_TYPE_ANDROID:
                            result = new ServiceInvokeDTO<AppVersionDTO>(InvokeCode.SYS_INVOKE_SUCCESS, Config.AndroidVersion);
                            break;
                        case Service.Constant.TERMINAL_TYPE_IOS:
                            result = new ServiceInvokeDTO<AppVersionDTO>(InvokeCode.SYS_INVOKE_SUCCESS, Config.IOSVersion);
                            break;
                        default:
                            result = new ServiceInvokeDTO<AppVersionDTO>(InvokeCode.SYS_ARG_ERROR);
                            break;
                    }
                }
                else
                {
                    result = new ServiceInvokeDTO<AppVersionDTO>(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<AppVersionDTO>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        /// <summary>
        /// 获取服务器UTC格式时间
        /// </summary>
        private HttpResponseMessage GetServerTime(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);
            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd },
                { Constant.HTTP_HEADER_RANDOM, random }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO<string> result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    result = new ServiceInvokeDTO<string>(InvokeCode.SYS_INVOKE_SUCCESS, InvokeCode.SYS_INVOKE_SUCCESS.GetDescription(), DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    result = new ServiceInvokeDTO<string>(InvokeCode.SYS_SIGN_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<string>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
