using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KST.Model
{
    /// <summary>
    /// 试卷类型枚举
    /// </summary>
    public enum PaperType
    {
        /// <summary>
        /// 模拟试卷
        /// </summary>
        SimulationPaper = 0,

        /// <summary>
        /// 历届试卷
        /// </summary>
        PastPaper = 1,

        /// <summary>
        /// VIP试卷
        /// </summary>
        VipPaper = 2
    }
}
