using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuiLib
{
    /// <summary>
    /// 图片按钮状态枚举
    /// </summary>
    public enum NuiImageButtonState
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
        /// 焦点状态
        /// </summary>
        Focus,

        /// <summary>
        /// 禁止状态
        /// </summary>
        Disabled
    }
}
