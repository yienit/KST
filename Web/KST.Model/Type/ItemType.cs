using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KST.Model
{
    /// <summary>
    /// 题型枚举
    /// </summary>
    public enum ItemType
    {
        /// <summary>
        /// 单选题
        /// </summary>
        Single = 1,

        /// <summary>
        /// 多选题
        /// </summary>
        Multiple = 2,

        /// <summary>
        /// 判断题
        /// </summary>
        Judge = 3,

        /// <summary>
        /// 填空题
        /// </summary>
        Blank =4,

        /// <summary>
        /// 不定项选择题
        /// </summary>
        Uncertain = 5,

        /// <summary>
        /// 分录题(会计基础专有)
        /// </summary>
        Fenlu = 6,

        /// <summary>
        /// 数字填空题(会计基础专有)
        /// </summary>
        NumberBlank = 7
    }
}
