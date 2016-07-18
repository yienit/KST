using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KST.ControlEx
{
    /// <summary>
    /// 控件状态枚举
    /// </summary>
    internal enum ControlState
    {
        /// <summary>
        /// 正常状态
        /// </summary>
        Normal,

        /// <summary>
        /// 高亮状态
        /// </summary>
        Hover,

        /// <summary>
        /// 按下状态
        /// </summary>
        Down,

        /// <summary>
        /// 禁止状态
        /// </summary>
        Disabled
    }
}
