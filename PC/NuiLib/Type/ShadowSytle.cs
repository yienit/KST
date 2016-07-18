using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NuiLib
{
    /// <summary>
    /// 窗体阴影呈现样式
    /// </summary>
    public enum ShadowSytle
    {
        /// <summary>
        /// 不显示窗体阴影
        /// </summary>
        None,

        /// <summary>
        /// 应用 CS_DropSHADOW 样式创建的窗体阴影
        /// </summary>
        ApiShadow,

        /// <summary>
        /// 使用阴影窗体来创建窗体阴影
        /// </summary>
        ShadowForm
    }
}
