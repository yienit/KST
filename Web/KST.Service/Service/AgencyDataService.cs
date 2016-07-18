using System;
using FluentValidation.Results;
using KST.DAL;
using KST.DTO;
using KST.Model;
using KST.Core;
using KST.Util;
using KST.Model.Validator;
using System.Collections.Generic;

namespace KST.Service
{
    /// <summary>
    /// 机构数据服务，提供机构注册、机构管理员账号等数据管理服务
    /// </summary>
    public class AgencyDataService
    {
        private AgencyDAL agencyDAL = DALFactory.Instance.AgencyDAL;
        private AgencyAdminDAL agencyAdminDAL = DALFactory.Instance.AgencyAdminDAL;
        private AgencyCreatorDAL agencyCreatorDAL = DALFactory.Instance.AgencyCreatorDAL;
        private AgencyConfigDAL agencyConfigDAL = DALFactory.Instance.AgencyConfigDAL;

        private SecurityService securityService = ServiceFactory.Instance.SecurityService;
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(AgencyDataService));

        #region Agency

        /// <summary>
        /// 注册培训机构
        /// </summary>
        public ServiceInvokeDTO AgencyReg(string phone, string captcha, string agencyName, string chineseName, string password)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                // 检测验证码
                ServiceInvokeDTO checkCaptchaResult = securityService.CheckCaptcha(CaptchaCodeType.RegCode, phone, captcha);
                if (checkCaptchaResult != null && checkCaptchaResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    // 检测手机号是否注册
                    AgencyAdmin dbAdmin = agencyAdminDAL.GetByPhone(phone);
                    if (dbAdmin == null)
                    {
                        // 检测机构名称是否存在
                        Agency dbAgency = agencyDAL.GetByName(agencyName);
                        if (dbAgency == null)
                        {
                            Agency agency = new Agency();
                            agency.Name = agencyName;
                            agency.RegTime = DateTime.Now;

                            AgencyAdmin admin = new AgencyAdmin();
                            admin.ChineseName = chineseName;
                            admin.UserName = phone;
                            admin.Password = SecurityUtil.MD5(password + Constant.ADMIN_SALT_KEY);
                            admin.Phone = phone;
                            admin.Email = string.Empty;
                            admin.Level = AdminLevel.AgencyCreatorAdmin;

                            // 验证参数
                            AgencyValidator validator = new AgencyValidator();
                            ValidationResult validatorResult = validator.Validate(agency);
                            if (validatorResult.IsValid)
                            {
                                agencyDAL.Insert(agency, admin);
                                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                            }
                            else
                            {
                                result = new ServiceInvokeDTO(InvokeCode.SYS_ARG_ERROR);
                                log.Error(string.Format(Constant.DEBUG_ARG_ERROR_FORMATER, validatorResult.Errors[0].ErrorMessage));
                            }
                        }
                        else
                        {
                            result = new ServiceInvokeDTO(InvokeCode.AGENCY_NAME_EXIST_ERROR);
                        }
                    }
                    else
                    {
                        result = new ServiceInvokeDTO(InvokeCode.SMS_PHONE_EXIST_WHEN_REG);
                    }
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

        /// <summary>
        /// 获取培训机构详情
        /// </summary>
        public ServiceInvokeDTO<AgencyDTO> GetAgencyDetail(int agencyID)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<AgencyDTO> result = null;
            try
            {
                Agency agency = agencyDAL.GetByID(agencyID);
                if (agency != null)
                {
                    // 拼装DTO
                    AgencyConfig config = agencyConfigDAL.GetByAgencyID(agencyID);
                    int creatorID = agencyCreatorDAL.GetCreatorIDByAgencyID(agencyID);
                    AgencyDTO agencyDTO = new AgencyDTO(agency, config, creatorID);

                    result = new ServiceInvokeDTO<AgencyDTO>(InvokeCode.SYS_INVOKE_SUCCESS, agencyDTO);
                }
                else
                {
                    result = new ServiceInvokeDTO<AgencyDTO>(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
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
        /// 更新培训机构名称
        /// </summary>
        public ServiceInvokeDTO UpdateAgencyName(int agencyID, string name)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                Agency agency = agencyDAL.GetByID(agencyID);
                if (agency != null)
                {
                    // 检测机构名称是否存在
                    Agency agencyWithName = agencyDAL.GetByName(name);
                    if (agencyWithName != null && agencyWithName.ID != agencyID)
                    {
                        result = new ServiceInvokeDTO(InvokeCode.AGENCY_NAME_EXIST_ERROR);
                    }
                    else
                    {
                        agency.Name = name;
                        agencyDAL.Update(agency);
                        result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                    }
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
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
        /// 设置培训机构是否开启设备锁参数
        /// </summary>
        public ServiceInvokeDTO SetIsLockDeviceConfig(int agencyID, int isLockDevice)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                AgencyConfig config = agencyConfigDAL.GetByAgencyID(agencyID);
                if (config != null)
                {
                    config.IsLockDevice = isLockDevice;
                    agencyConfigDAL.Update(config);

                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
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
        /// 更新培训机构联系方式参数设置
        /// </summary>
        public ServiceInvokeDTO UpdateAgencyContactConfig(int agencyID, string contact)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                AgencyConfig config = agencyConfigDAL.GetByAgencyID(agencyID);
                if (config != null)
                {
                    config.Contact = contact;
                    agencyConfigDAL.Update(config);

                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
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
        /// 更新培训机构公告通知参数设置
        /// </summary>
        public ServiceInvokeDTO UpdateAgencyNoticeConfig(int agencyID, string notice)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                AgencyConfig config = agencyConfigDAL.GetByAgencyID(agencyID);
                if (config != null)
                {
                    config.Notice = notice;
                    agencyConfigDAL.Update(config);

                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
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

        #region Admin

        /// <summary>
        /// 培训机构管理员登录
        /// </summary>
        public ServiceInvokeDTO<AgencyAdminDTO> AdminLogin(string userName, string password)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<AgencyAdminDTO> result = null;
            try
            {
                AgencyAdmin dbAdmin = agencyAdminDAL.GetByAccount(userName);
                if (dbAdmin != null)
                {
                    // 校验机构状态
                    Agency agency = agencyDAL.GetByID(dbAdmin.AgencyID);
                    if (agency != null && agency.State == AgencyState.Normal)
                    {
                        string saltPassword = SecurityUtil.MD5(password + Constant.ADMIN_SALT_KEY);
                        if (saltPassword.Equals(dbAdmin.Password))
                        {
                            // 拼装DTO
                            AgencyConfig config = agencyConfigDAL.GetByAgencyID(dbAdmin.AgencyID);
                            int creatorID = agencyCreatorDAL.GetCreatorIDByAgencyID(dbAdmin.AgencyID);
                            AgencyDTO agencyDto = new AgencyDTO(agency, config, creatorID);

                            AgencyAdminDTO adminDTO = new AgencyAdminDTO(dbAdmin, agencyDto);
                            result = new ServiceInvokeDTO<AgencyAdminDTO>(InvokeCode.SYS_INVOKE_SUCCESS, adminDTO);
                        }
                        else
                        {
                            result = new ServiceInvokeDTO<AgencyAdminDTO>(InvokeCode.ACCOUNT_AGENCY_ADMIN_LOGIN_ERROR);
                        }
                    }
                    else
                    {
                        result = new ServiceInvokeDTO<AgencyAdminDTO>(InvokeCode.ACCOUNT_AGENCY_STATE_ERROR);
                    }
                }
                else
                {
                    result = new ServiceInvokeDTO<AgencyAdminDTO>(InvokeCode.ACCOUNT_AGENCY_ADMIN_LOGIN_ERROR);
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
        /// 重置培训机构管理员密码(找回密码)
        /// </summary>
        public ServiceInvokeDTO ResetAdminPassword(string phone, string captcha, string newpwd)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                // 检测验证码
                ServiceInvokeDTO checkCaptchaResult = securityService.CheckCaptcha(CaptchaCodeType.GetPwdCode, phone, captcha);
                if (checkCaptchaResult != null && checkCaptchaResult.Code == InvokeCode.SYS_INVOKE_SUCCESS)
                {
                    // 检测账号是否存在
                    AgencyAdmin dbAdmin = agencyAdminDAL.GetByPhone(phone);
                    if (dbAdmin != null)
                    {
                        dbAdmin.Password = SecurityUtil.MD5(newpwd + Constant.ADMIN_SALT_KEY);
                        agencyAdminDAL.Update(dbAdmin);

                        result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                    }
                    else
                    {
                        result = new ServiceInvokeDTO(InvokeCode.ACCOUNT_NOT_EIXST_ERROR);
                    }
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

        /// <summary>
        /// 修改培训机构管理员密码
        /// </summary>
        public ServiceInvokeDTO UpdateAdminPassword(int adminID, string oldPwd, string newPwd)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                AgencyAdmin dbAdmin = agencyAdminDAL.GetByID(adminID);
                if (dbAdmin != null)
                {
                    if (SecurityUtil.MD5(oldPwd + Constant.ADMIN_SALT_KEY).Equals(dbAdmin.Password))
                    {
                        dbAdmin.Password = SecurityUtil.MD5(newPwd + Constant.ADMIN_SALT_KEY);
                        agencyAdminDAL.Update(dbAdmin);

                        result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                    }
                    else
                    {
                        result = new ServiceInvokeDTO(InvokeCode.ACCOUNT_OLD_PWD_ERROR);
                    }
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
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
        /// 以分页的形式管理员信息
        /// </summary>
        public ServiceInvokeDTO<QueryResultDTO<AgencyAdmin>> QueryAdmin(QueryArgsDTO<AgencyAdmin> queryDTO)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<QueryResultDTO<AgencyAdmin>> result = null;
            try
            {
                QueryResultDTO<AgencyAdmin> queryData = agencyAdminDAL.Query(queryDTO);
                result = new ServiceInvokeDTO<QueryResultDTO<AgencyAdmin>>(InvokeCode.SYS_INVOKE_SUCCESS, queryData);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<QueryResultDTO<AgencyAdmin>>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 根据主键ID获取管理员信息
        /// </summary>
        public ServiceInvokeDTO<AgencyAdmin> GetAdminByID(int adminID)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<AgencyAdmin> result = null;
            try
            {
                AgencyAdmin dbAdmin = agencyAdminDAL.GetByID(adminID);
                result = new ServiceInvokeDTO<AgencyAdmin>(InvokeCode.SYS_INVOKE_SUCCESS, dbAdmin);
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
        /// 添加机构管理员
        /// </summary>
        public ServiceInvokeDTO AddAdmin(AgencyAdmin admin)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                AgencyAdmin dbAdmin = agencyAdminDAL.GetByPhone(admin.Phone);
                if (dbAdmin == null)
                {
                    admin.Password = SecurityUtil.MD5(SecurityUtil.MD5(admin.Phone) + Constant.ADMIN_SALT_KEY);
                    agencyAdminDAL.Insert(admin);
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.ACCOUNT_ADMIN_PHONE_EXIST_ERROR);
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
        /// 更新机构管理员
        /// </summary>
        public ServiceInvokeDTO UpdateAdmin(AgencyAdmin admin)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                AgencyAdmin dbAdmin = agencyAdminDAL.GetByID(admin.ID);
                if (dbAdmin != null)
                {
                    AgencyAdmin adminWithPhone = agencyAdminDAL.GetByPhone(admin.Phone);
                    if (adminWithPhone != null && adminWithPhone.ID != admin.ID)
                    {
                        result = new ServiceInvokeDTO(InvokeCode.ACCOUNT_ADMIN_PHONE_EXIST_ERROR);
                    }
                    else
                    {
                        admin.AgencyID = dbAdmin.AgencyID;
                        admin.UserName = dbAdmin.UserName;
                        admin.Password = dbAdmin.Password;
                        admin.Email = dbAdmin.Email;
                        admin.Level = dbAdmin.Level;
                        agencyAdminDAL.Update(admin);
                        result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                    }
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
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
        /// 删除机构管理员
        /// </summary>
        public ServiceInvokeDTO DeleteAdmin(int adminID)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                AgencyAdmin dbAdmin = agencyAdminDAL.GetByID(adminID);
                if (dbAdmin != null)
                {
                    // 机构管理员账号检测
                    if (dbAdmin.Level == AdminLevel.AgencyCreatorAdmin)
                    {
                        result = new ServiceInvokeDTO(InvokeCode.ACCOUNT_ADMIN_CREATOR_NOT_DELETE_ERROR);
                    }
                    else
                    {
                        agencyAdminDAL.DeleteByID(adminID);
                        result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                    }
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
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
        /// 删除机构管理员(批量删除)
        /// </summary>
        public ServiceInvokeDTO DeleteAdmin(List<int> adminIDs)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                agencyAdminDAL.DeleteInBatch(adminIDs);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
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
