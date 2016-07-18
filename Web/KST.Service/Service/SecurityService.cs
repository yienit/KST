using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using KST.Core;
using KST.DAL;
using KST.DTO;
using KST.Util;
using KST.Model;
using RestSharp;
using Newtonsoft.Json;

namespace KST.Service
{
    /// <summary>
    /// 安全服务,提供短信验、图片的发送及校验,API接口参数签名校验服务
    /// </summary>
    public class SecurityService
    {
        private const int API_REQUEST_TIME_OUT = 6000;                      // 第三方接口调用超时时间
        private const string YUNPIAN_HOST = "http://yunpian.com/v1/";       // 短信平台Host

        private AgencyAdminDAL adminDAL = DALFactory.Instance.AgencyAdminDAL;
        private CaptchaRecordDAL captchaRecordDAL = DALFactory.Instance.CaptchaRecordDAL;
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(SecurityService));

        #region Sign

        /// <summary>
        /// 检测参数Sign是否合法
        /// </summary>
        /// <param name="args">参数按照首字母升序排序之后的KV字典</param>
        /// <param name="secrect">参数签名密钥</param>
        /// <param name="sign">待验证的签名</param>
        public bool CheckSign(Dictionary<string, string> args, string secrect, string sign)
        {
            bool isSuccess = false;
            if (args != null)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append(secrect);
                foreach (var item in args)
                {
                    builder.Append(item.Key);
                    builder.Append(item.Value);
                }
                builder.Append(secrect);
                string resultSign = KST.Util.SecurityUtil.MD5(builder.ToString());
                if (resultSign.Equals(sign, StringComparison.OrdinalIgnoreCase))
                {
                    isSuccess = true;
                }
            }
            return isSuccess;
        }

        #endregion

        #region Captcha

        /// <summary>
        /// 发送注册账号短信验证码,并记录此次用户的IP地址
        /// </summary>
        public ServiceInvokeDTO SendRegCaptcha(string phone, string ip)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                if (RegExpUtil.IsMobile(phone))
                {
                    // 1. 检测是否已注册
                    AgencyAdmin dbAdmin = adminDAL.GetByPhone(phone);
                    if (dbAdmin == null)
                    {
                        // 2. 检测IP限制
                        CaptchaRecord dbCaptcha = captchaRecordDAL.GetCode(CaptchaCodeType.RegCode, ip, phone, Config.CaptchaGetTimeSpan);
                        if (dbCaptcha == null)
                        {
                            string code = GenerateCode();
                            // result = InvokeYPToSendSms(phone, code, CaptchaCodeType.RegCode);
                            result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                            if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                            {
                                CaptchaRecord captchaRecord = new CaptchaRecord();

                                captchaRecord.CodeType = CaptchaCodeType.RegCode;
                                captchaRecord.IP = ip;
                                captchaRecord.Phone = phone;
                                captchaRecord.Code = code;
                                captchaRecord.SendTime = DateTime.Now;

                                captchaRecordDAL.Insert(captchaRecord);
                            }
                        }
                        else
                        {
                            result = new ServiceInvokeDTO(InvokeCode.SMS_CAPTCHA_IP_LIMIT);
                        }
                    }
                    else
                    {
                        result = new ServiceInvokeDTO(InvokeCode.SMS_PHONE_EXIST_WHEN_REG);
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
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 发送找回密码短信验证码(机构管理员),并记录此次用户的IP地址
        /// </summary>
        public ServiceInvokeDTO SendGetPasswordCaptcha(string phone, string ip)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                if (RegExpUtil.IsMobile(phone))
                {
                    // 1. 检测是否已注册
                    AgencyAdmin dbAdmin = adminDAL.GetByPhone(phone);
                    if (dbAdmin != null)
                    {
                        // 2. 检测IP限制
                        CaptchaRecord dbCaptcha = captchaRecordDAL.GetCode(CaptchaCodeType.GetPwdCode, ip, phone, Config.CaptchaGetTimeSpan);
                        if (dbCaptcha == null)
                        {
                            string code = GenerateCode();
                            // result = InvokeYPToSendSms(phone, code, CaptchaCodeType.GetPwdCode);
                            result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                            if (result != null && result.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                            {
                                CaptchaRecord captchaRecord = new CaptchaRecord();

                                captchaRecord.CodeType = CaptchaCodeType.GetPwdCode;
                                captchaRecord.IP = ip;
                                captchaRecord.Phone = phone;
                                captchaRecord.Code = code;
                                captchaRecord.SendTime = DateTime.Now;

                                captchaRecordDAL.Insert(captchaRecord);
                            }
                        }
                        else
                        {
                            result = new ServiceInvokeDTO(InvokeCode.SMS_CAPTCHA_IP_LIMIT);
                        }
                    }
                    else
                    {
                        result = new ServiceInvokeDTO(InvokeCode.SMS_USER_NOT_EXIST_WHEN_GET_PWD);
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
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 检测验证码是否正确
        /// </summary>
        public ServiceInvokeDTO CheckCaptcha(CaptchaCodeType type, string phone, string code)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                string dbCode = captchaRecordDAL.GetCode(type, phone, Config.CaptchaExpireTime);
                if (!string.IsNullOrEmpty(dbCode) && dbCode.Equals(code))
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_CAPTCHA_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        #endregion

        #region Private

        /// <summary>
        /// 生成短信验证码
        /// </summary>
        private string GenerateCode()
        {
            return new Random().Next(123456, 999999).ToString();
        }

        /// <summary>
        /// 调用云片网络短信发送平台API地址发送短信
        /// </summary>
        private ServiceInvokeDTO InvokeYPToSendSms(string phone, string code, CaptchaCodeType codeType)
        {
            log.Debug(Constant.DEBUG_START);

            ServiceInvokeDTO result = null;

            // 调用第三方接口发送短信
            RestClient client = new RestClient(YUNPIAN_HOST);
            RestRequest request = new RestRequest("sms/send.json", Method.POST);
            request.Timeout = API_REQUEST_TIME_OUT;
            request.AddParameter("apikey", Config.SmsApiKey);
            request.AddParameter("mobile", phone);

            // 短信内容
            string content = string.Empty;
            switch (codeType)
            {
                case CaptchaCodeType.RegCode: content = string.Format(Config.SmsRegTemplate, code); break;
                case CaptchaCodeType.GetPwdCode: content = string.Format(Config.SmsGetPasswordTemplate, code); break;
                default: break;
            }
            request.AddParameter("text", content);

            IRestResponse response = client.Execute(request);
            if (response.ErrorException == null)
            {
                YPSmsResultDTO ypResult = JsonConvert.DeserializeObject<YPSmsResultDTO>(response.Content);
                if (ypResult.code == Constant.YP_INVOKE_SUCCESS_CODE)
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    log.Error(ypResult.msg);
                    result = new ServiceInvokeDTO(InvokeCode.SMS_YUNPIAN_ERROR);
                }
            }
            else
            {
                log.Error(response.ErrorException);
                result = new ServiceInvokeDTO(InvokeCode.SMS_YUNPIAN_ERROR);
            }

            log.Debug(Constant.DEBUG_END);

            return result;
        }

        #endregion
    }
}
