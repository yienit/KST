using FluentValidation.Results;
using KST.Core;
using KST.DAL;
using KST.DTO;
using KST.Model;
using KST.Model.Validator;
using System;

namespace KST.Service
{
    /// <summary>
    /// 记录数据服务,提供用户登录记录、机构管理员操作记录、意见反馈等数据管理服务
    /// </summary>
    public class RecordDataService
    {
        private AdminDoRecordDAL adminDoRecordDAL = DALFactory.Instance.AdminDoRecordDAL;
        private FeedbackRecordDAL feedBackDAL = DALFactory.Instance.FeedbackRecordDAL;

        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(RecordDataService));

        #region AdminDoRecord

        /// <summary>
        /// 以分页的形式查询管理员操作记录
        /// </summary>
        public ServiceInvokeDTO<QueryResultDTO<AdminDoRecord>> QueryAdminDoRecord(QueryArgsDTO<AdminDoRecord> queryDTO, int agencyID, DateTime startDate, DateTime endDate)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<QueryResultDTO<AdminDoRecord>> result = null;
            try
            {
                QueryResultDTO<AdminDoRecord> queryData = adminDoRecordDAL.Query(queryDTO, agencyID, startDate, endDate);
                result = new ServiceInvokeDTO<QueryResultDTO<AdminDoRecord>>(InvokeCode.SYS_INVOKE_SUCCESS, queryData);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<QueryResultDTO<AdminDoRecord>>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 根据主键ID获取管理员操作记录
        /// </summary>
        public ServiceInvokeDTO<AdminDoRecord> GetAdminDoRecordByID(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<AdminDoRecord> result = null;
            try
            {
                AdminDoRecord doRecord = adminDoRecordDAL.GetByID(id);
                result = new ServiceInvokeDTO<AdminDoRecord>(InvokeCode.SYS_INVOKE_SUCCESS, doRecord);
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
        /// 添加管理员操作记录
        /// </summary>
        public ServiceInvokeDTO AddAdminDoRecord(AdminDoRecord doRecord)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                adminDoRecordDAL.Insert(doRecord);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                // 记录异常但不抛出
                log.Error(ex);
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        #endregion

        #region FeedbackRecord

        /// <summary>
        /// 添加用户意见反馈记录
        /// </summary>
        public ServiceInvokeDTO AddFeedBackRecord(FeedbackRecord feedback)
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

        #endregion
    }
}
