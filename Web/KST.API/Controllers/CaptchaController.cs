using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using KST.Core;
using KST.DTO;
using KST.Service;
using KST.Util;

namespace KST.API.Controllers
{
    /// <summary>
    /// 图片验证码控制器
    /// </summary>
    public class CaptchaController : ApiController
    {
        private SecurityService securityService = ServiceFactory.Instance.SecurityService;
        private SmsService smsService = ServiceFactory.Instance.SmsService;
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(CaptchaController));

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
                        case Cmd.CAPTCHA_GET_IMAGE: response = GetImageCaptcha(Request); break;
                        case Cmd.CAPTCHA_GET_SMS: response = GetSmsCaptcha(Request); break;
                        case Cmd.CAPTCHA_CHECK: response = CheckCaptcha(Request); break;
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
        /// 获取图形验证码
        /// </summary>
        private HttpResponseMessage GetImageCaptcha(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd },
                { Constant.HTTP_HEADER_RANDOM, random }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    CaptchaImageDTO captchaDTO = securityService.GenerateCaptcha();
                    using (MemoryStream stream = new MemoryStream())
                    {
                        captchaDTO.Image.Save(stream, ImageFormat.Png);

                        HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK);
                        response.Content = new ByteArrayContent(stream.ToArray());
                        response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");

                        log.Debug(Constant.DEBUG_END);

                        return response;
                    }
                }
                else
                {
                    return request.CreateResponse(HttpStatusCode.OK, new ServiceInvokeDTO<string>(InvokeCode.SYS_SIGN_ERROR));
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return request.CreateResponse(HttpStatusCode.OK, new ServiceInvokeDTO<string>(InvokeCode.SYS_INNER_ERROR));
            }
        }

        /// <summary>
        /// 获取短信验证码
        /// </summary>
        private HttpResponseMessage GetSmsCaptcha(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string typeString = ApiQueryUtil.QueryArgByPost("type");
            string phone = ApiQueryUtil.QueryArgByPost("phone");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd },
                { Constant.HTTP_HEADER_RANDOM, random },
                { "type", typeString },
                { "phone", phone }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    int type = Convert.ToInt32(typeString);
                    switch (type)
                    {
                        case Service.Constant.CAPTCHA_TYPE_REG: result = smsService.SendRegCaptcha(phone); break;
                        case Service.Constant.CAPTCHA_TYPE_GET_PWD: result = smsService.SendGetPasswordCaptcha(phone); break;
                        case Service.Constant.CAPTCHA_TYPE_LOGIN: result = smsService.SendLoginCaptcha(phone); break;
                        default: result = new ServiceInvokeDTO(InvokeCode.SYS_ARG_ERROR); break;
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

        /// <summary>
        /// 检测验证码
        /// </summary>
        private HttpResponseMessage CheckCaptcha(HttpRequestMessage request)
        {
            log.Debug(Constant.DEBUG_START);

            string sign = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_SIGN);
            string cmd = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_CMD);
            string random = ApiQueryUtil.QueryHeader(Constant.HTTP_HEADER_RANDOM);

            string typeString = ApiQueryUtil.QueryArgByGet("type");
            string phone = ApiQueryUtil.QueryArgByGet("phone");
            string code = ApiQueryUtil.QueryArgByGet("code");

            Dictionary<string, string> args = new Dictionary<string, string>() 
            { 
                { Constant.HTTP_HEADER_CMD, cmd },
                { Constant.HTTP_HEADER_RANDOM, random },
                { "type", typeString },
                { "phone", phone },
                { "code", code }
            }.OrderBy(element => element.Key).ToDictionary(o => o.Key, p => p.Value);

            ServiceInvokeDTO result = null;
            try
            {
                // Check sign
                if (securityService.CheckSign(args, Config.ApiSignSecretKey, sign))
                {
                    int type = Convert.ToInt32(typeString);
                    result = securityService.CheckCaptcha(type, phone, code);
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
