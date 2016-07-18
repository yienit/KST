using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuiLib
{
    /// <summary>
    /// 窗体系统按钮状态枚举
    /// </summary>
    internal enum SysButtonState
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
        /// 鼠标按下状态离开控件状态
        /// </summary>
        DownLeave,

        /// <summary>
        /// 禁止状态
        /// </summary>
        Disabled
    }
}
