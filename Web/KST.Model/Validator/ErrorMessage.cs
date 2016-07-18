using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KST.Model.Validator
{
    /// <summary>
    /// 参数验证错误提示字符串定义类
    /// </summary>
    public class ErrorMessage
    {
        public const string NOT_NULL_MSG = "参数不能为null";
        public const string NOT_EMPTY_MSG = "参数不能为空";
        public const string NOT_EMAIL_ADDRESS_MSG = "邮箱格式不正确";
        public const string NOT_LENGTH_MSG = "参数长度验证失败";
        public const string NOT_RANGE_MSG = "参数不在指定范围内";
    }
}
