using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KST.DAL;
using KST.DTO;
using KST.Model;
using KST.Model.Validator;
using FluentValidation.Results;
using KST.Core;

namespace KST.Service
{
    /// <summary>
    /// 辅助通用数据服务,提供获取系统时间、意见反馈等数据管理服务
    /// </summary>
    public class CommonDataService
    {
        private FeedbackDAL feedBackDAL = DALFactory.Instance.FeedbackDAL;
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(CommonDataService));

        /// <summary>
        /// 添加用户意见反馈
        /// </summary>
        public ServiceInvokeDTO AddFeedBack(Feedback feedback)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;

            try
            {
                // 验证参数
                FeedbackValidator validator = new FeedbackValidator();
                ValidationResult validatorResult = validator.Validate(feedback);
                if (validatorResult.IsValid)
                {
                    feedBackDAL.Insert(feedback);
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_ARG_ERROR);
                    log.Error(string.Format(Constant.DEBUG_ARG_ERROR_FORMATER, validatorResult.Errors[0].ErrorMessage));
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
    }
}
