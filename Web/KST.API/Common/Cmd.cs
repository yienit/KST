using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KST.API
{
    /// <summary>
    /// API接口Cmd参数定义类
    /// </summary>
    public class Cmd
    {
        #region User

        /// <summary>
        /// 根据主键ID获取用户
        /// </summary>
        public const string USER_GET_BY_ID = "get_by_id";

        /// <summary>
        /// 根据手机号码获取用户
        /// </summary>
        public const string USER_GET_BY_PHONE = "get_by_phone";

        /// <summary>
        /// 根据电子邮箱获取用户
        /// </summary>
        public const string USER_GET_BY_EMAIL = "get_by_email";

        /// <summary>
        /// 注册账号
        /// </summary>
        public const string USER_REGISTER = "reg";

        /// <summary>
        /// 登录
        /// </summary>
        public const string USER_LOGIN = "login";

        /// <summary>
        /// 修改密码
        /// </summary>
        public const string USER_UPDATE_PASSWORD = "update_pwd";

        /// <summary>
        /// 设置新密码
        /// </summary>
        public const string USER_SET_NEW_PASSWORD = "set_new_pwd";

        #endregion

        #region Captcha

        /// <summary>
        /// 获取图片验证码
        /// </summary>
        public const string CAPTCHA_GET_IMAGE = "get_image";

        /// <summary>
        /// 获取短信验证码
        /// </summary>
        public const string CAPTCHA_GET_SMS = "get_sms";

        /// <summary>
        /// 检查验证码
        /// </summary>
        public const string CAPTCHA_CHECK = "check";

        #endregion

        #region Call

        /// <summary>
        /// 分页查询通话记录
        /// </summary>
        public const string CALL_QUERY = "query";

        /// <summary>
        /// 根据主键ID获取通话记录信息
        /// </summary>
        public const string CALLRECORD_GET_BY_ID = "get_call_by_id";

        /// <summary>
        /// 根据日期、通话类型、对方号码获取通话记录
        /// </summary>
        public const string CALLRECORD_GET_BY_DETAIL = "get_call_by_detail";

        /// <summary>
        /// 获取指定用户通话记录列表
        /// </summary>
        public const string CALLRECORD_GET_ALL = "get_all_call";

        /// <summary>
        /// 添加通话记录
        /// </summary>
        public const string CALLRECORD_ADD = "add_call";

        /// <summary>
        /// 删除通话记录
        /// </summary>
        public const string CALLRECORD_DELETE = "delete_call";


        //======================================================================

        /// <summary>
        /// 根据主键ID获取通话录音信息
        /// </summary>
        public const string CALLVOICE_GET_BY_ID = "get_voice_by_id";

        /// <summary>
        /// 根据通话记录主键ID获取通话录音信息
        /// </summary>
        public const string CALLVOICE_GET_BY_CALL_ID = "get_voice_by_call_id";

        /// <summary>
        /// 根据日期、通话类型、对方号码获取通话录音信息
        /// </summary>
        public const string CALLVOICE_GET_BY_DETAIL = "get_voice_by_detail";

        /// <summary>
        /// 上传通话录音
        /// </summary>
        public const string CALLVOICE_UPLOAD = "upload_voice";

        /// <summary>
        /// 删除通话录音
        /// </summary>
        public const string CALLVOICE_DELETE = "delete_voice";

        #endregion

        #region Contact

        /// <summary>
        /// 分页查询联系人
        /// </summary>
        public const string CONTACT_QUERY = "query";

        /// <summary>
        /// 根据主键ID获取联系人信息
        /// </summary>
        public const string CONTACT_GET_BY_ID = "get_by_id";

        /// <summary>
        /// 获取指定用户联系人列表
        /// </summary>
        public const string CONTACT_GET_ALL = "get_all";

        /// <summary>
        /// 添加联系人
        /// </summary>
        public const string CONTACT_ADD = "add";

        /// <summary>
        /// 更新联系人
        /// </summary>
        public const string CONTACT_UPDATE = "update";

        /// <summary>
        /// 删除联系人
        /// </summary>
        public const string CONTACT_DELETE = "delete";

        /// <summary>
        /// 同步联系人
        /// </summary>
        public const string CONTACT_SYNC = "sync";

        #endregion

        #region SmsRecord

        /// <summary>
        /// 分页查询短信记录
        /// </summary>
        public const string SMSRECORD_QUERY = "query";

        /// <summary>
        /// 根据主键ID获取短信发送记录信息
        /// </summary>
        public const string SMSRECORD_GET_BY_ID = "get_by_id";

        /// <summary>
        /// 获取指定用户短信发送记录列表
        /// </summary>
        public const string SMSRECORD_GET_ALL = "get_all";

        /// <summary>
        /// 添加短信发送记录
        /// </summary>
        public const string SMSRECORD_ADD = "add";

        /// <summary>
        /// 删除短信发送记录
        /// </summary>
        public const string SMSRECORD_DELETE = "delete";

        #endregion

        #region Note

        /// <summary>
        /// 分页查询云笔记
        /// </summary>
        public const string NOTE_QUERY = "query";

        /// <summary>
        /// 根据主键ID获取云笔记信息
        /// </summary>
        public const string NOTE_GET_BY_ID = "get_by_id";

        /// <summary>
        /// 获取指定用户云笔记列表
        /// </summary>
        public const string NOTE_GET_ALL = "get_all";

        /// <summary>
        /// 添加云笔记
        /// </summary>
        public const string NOTE_ADD = "add";

        /// <summary>
        /// 更新云笔记
        /// </summary>
        public const string NOTE_UPDATE = "update";

        /// <summary>
        /// 删除云笔记
        /// </summary>
        public const string NOTE_DELETE = "delete";

        #endregion

        #region  Common

        /// <summary>
        /// 添加意见反馈
        /// </summary>
        public const string COMMON_ADD_FEEDBACK = "add_feedback";

        /// <summary>
        /// 获取APP最新版本
        /// </summary>
        public const string COMMON_GET_NEW_VERSION = "get_new_version";

        /// <summary>
        /// 获取服务器UTC格式时间
        /// </summary>
        public const string COMMON_GET_UTC = "get_utc";

        #endregion
    }
}
