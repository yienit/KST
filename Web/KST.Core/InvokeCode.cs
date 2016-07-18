using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace KST.Core
{
    /// <summary>
    /// 系统接口调用代码枚举
    /// </summary>
    public enum InvokeCode
    {
        //================================  系统常用代码(0xx)  =================================

        /// <summary>
        /// 调用成功
        /// </summary>
        [Description("调用成功")]
        SYS_INVOKE_SUCCESS = 1,

        /// <summary>
        /// 时间戳过期
        /// </summary>
        [Description("时间戳过期")]
        SYS_TIMESTAMP_ERROR = 3,

        /// <summary>
        /// 签名错误
        /// </summary>
        [Description("签名错误")]
        SYS_SIGN_ERROR = 4,

        /// <summary>
        /// 超过接口调用频率
        /// </summary>
        [Description("超过接口调用频率")]
        SYS_RATE_LIMIT_ERROR = 5,

        /// <summary>
        /// 服务器内部异常
        /// </summary>
        [Description("服务器内部异常")]
        SYS_INNER_ERROR = 6,

        /// <summary>
        /// 参数错误或参数不符合指定的格式
        /// </summary>
        [Description("参数错误或参数不符合指定的格式")]
        SYS_ARG_ERROR = 7,

        /// <summary>
        /// 无效的或未知的Cmd
        /// </summary>
        [Description("无效的或未知的Cmd")]
        SYS_UNKNOW_CMD = 8,

        /// <summary>
        /// 验证码错误或过期
        /// </summary>
        [Description("验证码错误或过期")]
        SYS_CAPTCHA_ERROR = 9,

        /// <summary>
        /// Session过期
        /// </summary>
        [Description("Session过期,需重新登录")]
        SYS_SESSION_TIME_OUT_ERROR = 10,

        /// <summary>
        /// 操作的对象不存在
        /// </summary>
        [Description("操作的对象不存在")]
        SYS_OBJECT_NOT_EXIST_ERROR = 11,


        //====================================== 权限Service模块代码(1xx)  ==============================

        /// <summary>
        /// 因权限检测失败而无法进行此操作
        /// </summary>
        [Description("因权限检测失败而无法进行此操作")]
        PERMISSION_CHECK_FAILD_ERROR = 101,

        /// <summary>
        /// 非法操作不属于自己的数据
        /// </summary>
        [Description("非法操作不属于自己的数据")]
        PERMISSION_NOT_MINE_DATA_ERROR = 102,

        //======================================  短信模块代码(2xx)  ==============================

        /// <summary>
        /// 短信平台接口异常
        /// </summary>
        [Description("短信平台接口异常")]
        SMS_YUNPIAN_ERROR = 201,

        /// <summary>
        /// 同个IP获取验证码时间间隔超过规定时间
        /// </summary>
        [Description("频繁请求验证码")]
        SMS_CAPTCHA_IP_LIMIT = 202,

        /// <summary>
        /// 因手机号已注册无法发送注册验证码
        /// </summary>
        [Description("手机号已注册")]
        SMS_PHONE_EXIST_WHEN_REG = 203,

        /// <summary>
        /// 因账号不存在无法发送找回密码验证码
        /// </summary>
        [Description("账号不存在")]
        SMS_USER_NOT_EXIST_WHEN_GET_PWD = 204,

        /// <summary>
        /// 未知的短信验证码类型
        /// </summary>
        [Description("未知的短信验证码类型")]
        SMS_UNKNOW_CODE_TYPE_ERROR = 205,


        //================================ 培训机构数据Service模块代码(3xx)  ==============================

        /// <summary>
        /// 注册的培训机构名称已存在
        /// </summary>
        [Description("机构名称已存在")]
        AGENCY_NAME_EXIST_ERROR = 301,


        //================================ 账号数据Service模块代码(4xx)  ==============================

        /// <summary>
        /// 机构管理员登录失败
        /// </summary>
        [Description("账号与密码不匹配")]
        ACCOUNT_AGENCY_ADMIN_LOGIN_ERROR = 401,

        /// <summary>
        /// 账号不存在
        /// </summary>
        [Description("账号不存在")]
        ACCOUNT_NOT_EIXST_ERROR = 402,

        /// <summary>
        /// 该账号所属的培训机构状态已被冻结
        /// </summary>
        [Description("该账号所属的培训机构状态已被冻结")]
        ACCOUNT_AGENCY_STATE_ERROR = 403,

        /// <summary>
        /// 修改管理员账号密码时原密码错误
        /// </summary>
        [Description("原密码错误")]
        ACCOUNT_OLD_PWD_ERROR = 404,

        /// <summary>
        /// 机构管理员账号不允许删除
        /// </summary>
        [Description("机构管理员账号不允许删除")]
        ACCOUNT_ADMIN_CREATOR_NOT_DELETE_ERROR = 405,

        /// <summary>
        /// 机构管理员电话号码已存在
        /// </summary>
        [Description("电话号码已存在")]
        ACCOUNT_ADMIN_PHONE_EXIST_ERROR = 406,


        //================================ 题库数据Service模块代码(5xx)  ==============================

        /// <summary>
        /// 上传的题库文件格式错误
        /// </summary>
        [Description("上传的题库文件格式错误")]
        ITEM_FILE_FORMAT_ERROR = 501,

        /// <summary>
        /// 题库文件不存在
        /// </summary>
        [Description("题库文件不存在")]
        ITEM_FILE_NOT_EXIST_ERROR = 502,

        /// <summary>
        /// 章节名称已存在
        /// </summary>
        [Description("章节名称已存在")]
        ITEM_CHAPTER_NAME_EXIST_ERROR = 503,

        //================================ 试卷数据Service模块代码(6xx)  ==============================

        /// <summary>
        /// 操作的对象不存在
        /// </summary>
        [Description("操作的对象不存在")]
        PAPER_OBJECT_NOT_EXIST_ERROR = 601,

        //================================ 用户数据Service模块代码(7xx)  ==============================

        /// <summary>
        /// 操作的对象不存在
        /// </summary>
        [Description("操作的对象不存在")]
        USER_OBJECT_NOT_EXIST_ERROR = 701,

        //================================ 记录数据Service模块代码(8xx)  ==============================

        /// <summary>
        /// 操作的对象不存在
        /// </summary>
        [Description("操作的对象不存在")]
        RECORD_OBJECT_NOT_EXIST_ERROR = 801,

    }
}
