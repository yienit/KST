using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace KST.Util
{
    /// <summary>
    /// 枚举拓展辅助类
    /// </summary>
    public static class EnumUtil
    {
        /// <summary>
        /// 获取指定枚举值的描述文本
        /// </summary>
        /// <example>
        ///  string msg = ServiceCode.INVOKE_SUCCESS.GetDescription();
        /// </example>
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }

        /// <summary>
        /// 根据描述文本获取指定枚举值
        /// </summary>
        /// <example>
        /// var invokeSuccessCode = EnumUtil.GetFromDescription<ServiceCode>("this is a description");
        /// </example>
        public static T GetFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", "description");
            // or return default(T);
        }
    }
}
