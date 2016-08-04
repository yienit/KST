using System;
using System.Collections.Generic;
using KST.DAL;
using KST.DTO;
using KST.Model;
using KST.Core;
using KST.Util;

namespace KST.Service
{
    /// <summary>
    /// 用户数据服务，提供用户账号、我的试题评论、试题报错、我的错题、我的收藏、我的成绩等数据管理等服务
    /// </summary>
    public class UserDataService
    {
        private UserDAL userDAL = DALFactory.Instance.UserDAL;
        private AgencyDAL agencyDAL = DALFactory.Instance.AgencyDAL;
        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(UserDataService));

        #region User

        /// <summary>
        /// 以分页的形式查询学生用户
        /// </summary>
        public ServiceInvokeDTO<QueryResultDTO<User>> QueryUser(QueryArgsDTO<User> queryDTO)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<QueryResultDTO<User>> result = null;
            try
            {
                QueryResultDTO<User> resultData = userDAL.Query(queryDTO);
                result = new ServiceInvokeDTO<QueryResultDTO<User>>(InvokeCode.SYS_INVOKE_SUCCESS, resultData);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<QueryResultDTO<User>>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 根据主键ID获取学生用户
        /// </summary>
        public ServiceInvokeDTO<UserDTO> GetUserByID(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<UserDTO> result = null;
            try
            {
                UserDTO userDTO = null;

                // --> DTO
                User user = userDAL.GetByID(id);
                if (user != null)
                {
                    userDTO = new UserDTO(user);
                    userDTO.Agency = agencyDAL.GetByID(user.AgencyID);
                }
                result = new ServiceInvokeDTO<UserDTO>(InvokeCode.SYS_INVOKE_SUCCESS, userDTO);
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
        /// 添加学生用户
        /// </summary>
        public ServiceInvokeDTO AddUser(User user)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                // 检测电话号码
                User dbUserWithPhone = userDAL.GetByPhone(user.Phone);
                if (dbUserWithPhone == null)
                {
                    user.Password = SecurityUtil.MD5(SecurityUtil.MD5(user.Phone) + Constant.USER_SALT_KEY);
                    userDAL.Insert(user);
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.ACCOUNT_USER_PHONE_EXIST_ERROR);
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
        /// 添加学生用户(批量添加)
        /// </summary>
        public ServiceInvokeDTO AddUser(List<User> users)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                // 检测电话号码
                bool isPhoneExist = false;
                foreach (var user in users)
                {
                    User dbUserWithPhone = userDAL.GetByPhone(user.Phone);
                    if (dbUserWithPhone != null)
                    {
                        isPhoneExist = true;
                        break;
                    }

                    // 撒盐加密
                    user.Password = SecurityUtil.MD5(SecurityUtil.MD5(user.Phone) + Constant.USER_SALT_KEY);
                }

                if (!isPhoneExist)
                {
                    userDAL.Insert(users);
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.ACCOUNT_USER_PHONE_EXIST_ERROR);
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
        /// 更新学生用户
        /// </summary>
        public ServiceInvokeDTO UpdateUser(User user)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                User dbUserWithPhone = userDAL.GetByPhone(user.Phone);
                if (dbUserWithPhone != null && dbUserWithPhone.ID != user.ID)
                {
                    result = new ServiceInvokeDTO(InvokeCode.ACCOUNT_USER_PHONE_EXIST_ERROR);
                }
                else
                {
                    userDAL.Update(user);
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
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
        /// 删除学生用户
        /// </summary>
        public ServiceInvokeDTO DeleteUser(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                userDAL.DeleteByID(id);
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

        /// <summary>
        /// 删除学生用户(批量删除)
        /// </summary>
        public ServiceInvokeDTO DeleteUser(List<int> ids)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                userDAL.DeleteInBatch(ids);
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
