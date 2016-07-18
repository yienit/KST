using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace KST.Model
{
    /// <summary>
    /// 机构管理员账号级别枚举
    /// </summary>
    public enum AdminLevel
    {
        /// <summary>
        /// 机构题库管理员
        /// </summary>
        [Description("题库管理员")]
        AgencyItemAdmin = 0,

        /// <summary>
        /// 机构创建者管理员
        /// </summary>
        [Description("机构管理员")]
        AgencyCreatorAdmin = 1
    }
}
