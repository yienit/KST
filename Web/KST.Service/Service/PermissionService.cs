using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KST.DTO;
using KST.Core;
using KST.DAL;
using KST.Model;

namespace KST.Service
{
    /// <summary>
    /// 权限服务,提供检测用户或管理员操作权限
    /// </summary>
    public class PermissionService
    {
        private AgencyAdminDAL agencyAdminDAL = DALFactory.Instance.AgencyAdminDAL;
        private ChapterDAL chapterDAL = DALFactory.Instance.ChapterDAL;
        private SingleItemDAL singleDAL = DALFactory.Instance.SingleItemDAL;
        private MultipleItemDAL multipleDAL = DALFactory.Instance.MultipleItemDAL;
        private JudgeItemDAL judgeDAL = DALFactory.Instance.JudgeItemDAL;
        private UncertainItemDAL uncertainDAL = DALFactory.Instance.UncertainItemDAL;
        private UncertainSubChoiceDAL uncertainSubChoiceDAL = DALFactory.Instance.UncertainSubChoiceDAL;
        private FenLuItemDAL fenluDAL = DALFactory.Instance.FenLuItemDAL;
        private NumberBlankItemDAL numberBlankDAL = DALFactory.Instance.NumberBlankItemDAL;

        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(PermissionService));

        /// <summary>
        /// 检测操作权限(检测被操作资源是否属于操作者)
        /// </summary>
        /// <param name="actionType">操作类型</param>
        /// <param name="operatorAgencyID">操作者所属机构主键ID</param>
        /// <param name="resourceID">被操作资源主键ID</param>
        public ServiceInvokeDTO CheckPermission(DoActionType actionType, int operatorAgencyID, int resourceID)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                switch (actionType)
                {
                    case DoActionType.UpdateAdmin:
                    case DoActionType.DeleteAdmin:
                    case DoActionType.DeleteAdminInBatch:
                        result = CheckAdminPermission(operatorAgencyID, resourceID); break;

                    case DoActionType.UpdateChapter:
                    case DoActionType.UpChapter:
                    case DoActionType.DownChapter:
                    case DoActionType.DeleteChapter:
                    case DoActionType.DeleteChapterInBatch:
                        result = CheckChaperPermission(operatorAgencyID, resourceID); break;

                    case DoActionType.UpdateSingle:
                    case DoActionType.DeleteSingle:
                    case DoActionType.DeleteSingleInBatch:
                        result = CheckSinglePermission(operatorAgencyID, resourceID); break;

                    case DoActionType.UpdateMultiple:
                    case DoActionType.DeleteMultiple:
                    case DoActionType.DeleteMultipleInBatch:
                        result = CheckMultiplePermission(operatorAgencyID, resourceID); break;

                    case DoActionType.UpdateJudge:
                    case DoActionType.DeleteJudge:
                    case DoActionType.DeleteJudgeInBatch:
                        result = CheckJudgePermission(operatorAgencyID, resourceID); break;

                    case DoActionType.UpdateUncertain:
                    case DoActionType.DeleteUncertain:
                    case DoActionType.DeleteUncertainInBatch:
                        result = CheckUncertainPermission(operatorAgencyID, resourceID); break;

                    case DoActionType.UpdateUncertainSubChoice:
                    case DoActionType.UpUncertainSubChoice:
                    case DoActionType.DownUncertainSubChoice:
                    case DoActionType.DeleteUncertainSubChoice:
                    case DoActionType.DeleteUncertainSubChoiceInBatch:
                        result = CheckUncertainSubChoicePermission(operatorAgencyID, resourceID); break;

                    case DoActionType.UpdateFenLu:
                    case DoActionType.DeleteFenLu:
                    case DoActionType.DeleteFenLuInBatch:
                        result = CheckFenLuPermission(operatorAgencyID, resourceID); break;

                    case DoActionType.UpdateNumberBlank:
                    case DoActionType.DeleteNumberBlank:
                    case DoActionType.DeleteNumberBlankInBatch:
                        result = CheckNumberBlankPermission(operatorAgencyID, resourceID); break;

                    default: break;
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 检测管理员资源操作权限
        /// </summary>
        private ServiceInvokeDTO CheckAdminPermission(int operatorAgencyID, int resourceID)
        {
            ServiceInvokeDTO result = new ServiceInvokeDTO(InvokeCode.PERMISSION_CHECK_FAILD_ERROR);

            var operatedResource = agencyAdminDAL.GetByID(resourceID);
            if (operatedResource != null)
            {
                if (operatedResource.AgencyID == operatorAgencyID)
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.PERMISSION_NOT_MINE_DATA_ERROR);
                }
            }

            return result;
        }

        /// <summary>
        /// 检测章节资源操作权限
        /// </summary>
        private ServiceInvokeDTO CheckChaperPermission(int operatorAgencyID, int resourceID)
        {
            ServiceInvokeDTO result = new ServiceInvokeDTO(InvokeCode.PERMISSION_CHECK_FAILD_ERROR);

            int resourceAgencyID = chapterDAL.GetAgencyID(resourceID);
            if (resourceAgencyID == operatorAgencyID)
            {
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            else
            {
                result = new ServiceInvokeDTO(InvokeCode.PERMISSION_NOT_MINE_DATA_ERROR);
            }

            return result;
        }

        /// <summary>
        /// 检测单选题资源操作权限
        /// </summary>
        private ServiceInvokeDTO CheckSinglePermission(int operatorAgencyID, int resourceID)
        {
            ServiceInvokeDTO result = new ServiceInvokeDTO(InvokeCode.PERMISSION_CHECK_FAILD_ERROR);

            var operatedResource = singleDAL.GetByID(resourceID);
            if (operatedResource != null)
            {
                if (operatedResource.AgencyID == operatorAgencyID)
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.PERMISSION_NOT_MINE_DATA_ERROR);
                }
            }

            return result;
        }

        /// <summary>
        /// 检测多选题资源操作权限
        /// </summary>
        private ServiceInvokeDTO CheckMultiplePermission(int operatorAgencyID, int resourceID)
        {
            ServiceInvokeDTO result = new ServiceInvokeDTO(InvokeCode.PERMISSION_CHECK_FAILD_ERROR);

            var operatedResource = multipleDAL.GetByID(resourceID);
            if (operatedResource != null)
            {
                if (operatedResource.AgencyID == operatorAgencyID)
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.PERMISSION_NOT_MINE_DATA_ERROR);
                }
            }

            return result;
        }

        /// <summary>
        /// 检测判断题资源操作权限
        /// </summary>
        private ServiceInvokeDTO CheckJudgePermission(int operatorAgencyID, int resourceID)
        {
            ServiceInvokeDTO result = new ServiceInvokeDTO(InvokeCode.PERMISSION_CHECK_FAILD_ERROR);

            var operatedResource = judgeDAL.GetByID(resourceID);
            if (operatedResource != null)
            {
                if (operatedResource.AgencyID == operatorAgencyID)
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.PERMISSION_NOT_MINE_DATA_ERROR);
                }
            }

            return result;
        }

        /// <summary>
        /// 检测不定项选择题资源操作权限
        /// </summary>
        private ServiceInvokeDTO CheckUncertainPermission(int operatorAgencyID, int resourceID)
        {
            ServiceInvokeDTO result = new ServiceInvokeDTO(InvokeCode.PERMISSION_CHECK_FAILD_ERROR);

            var operatedResource = uncertainDAL.GetByID(resourceID);
            if (operatedResource != null)
            {
                if (operatedResource.AgencyID == operatorAgencyID)
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.PERMISSION_NOT_MINE_DATA_ERROR);
                }
            }

            return result;
        }

        /// <summary>
        /// 检测不定项子选择题资源操作权限
        /// </summary>
        private ServiceInvokeDTO CheckUncertainSubChoicePermission(int operatorAgencyID, int resourceID)
        {
            ServiceInvokeDTO result = new ServiceInvokeDTO(InvokeCode.PERMISSION_CHECK_FAILD_ERROR);

            int resourceAgencyID = uncertainSubChoiceDAL.GetAgencyID(resourceID);
            if (resourceAgencyID == operatorAgencyID)
            {
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            else
            {
                result = new ServiceInvokeDTO(InvokeCode.PERMISSION_NOT_MINE_DATA_ERROR);
            }

            return result;
        }

        /// <summary>
        /// 检测分录题资源操作权限
        /// </summary>
        private ServiceInvokeDTO CheckFenLuPermission(int operatorAgencyID, int resourceID)
        {
            ServiceInvokeDTO result = new ServiceInvokeDTO(InvokeCode.PERMISSION_CHECK_FAILD_ERROR);

            var operatedResource = fenluDAL.GetByID(resourceID);
            if (operatedResource != null)
            {
                if (operatedResource.AgencyID == operatorAgencyID)
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.PERMISSION_NOT_MINE_DATA_ERROR);
                }
            }

            return result;
        }

        /// <summary>
        /// 检测数字填空题资源操作权限
        /// </summary>
        private ServiceInvokeDTO CheckNumberBlankPermission(int operatorAgencyID, int resourceID)
        {
            ServiceInvokeDTO result = new ServiceInvokeDTO(InvokeCode.PERMISSION_CHECK_FAILD_ERROR);

            var operatedResource = numberBlankDAL.GetByID(resourceID);
            if (operatedResource != null)
            {
                if (operatedResource.AgencyID == operatorAgencyID)
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.PERMISSION_NOT_MINE_DATA_ERROR);
                }
            }

            return result;
        }

    }
}
