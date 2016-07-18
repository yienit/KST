using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KST.Web
{

    #region Header

    /// <summary>
    /// 头部Tab菜单
    /// </summary>
    public enum HeaderTabMenu
    {
        /// <summary>
        /// 机构主页
        /// </summary>
        AgencyHome,

        /// <summary>
        /// 题库管理
        /// </summary>
        ItemManagement,

        /// <summary>
        /// 试卷管理
        /// </summary>
        PaperManagement,

        /// <summary>
        /// 统计分析
        /// </summary>
        StatisticsManagement
    }

    #endregion

    /// <summary>
    /// 机构管理侧边栏菜单
    /// </summary>
    public enum AgencySideMenu
    {

    }

    /// <summary>
    /// 试卷管理侧边栏菜单
    /// </summary>
    public enum PaperSideMenu
    {

    }

    /// <summary>
    /// VIP题库管理侧边栏菜单
    /// </summary>
    public enum VipSideMenu
    {

    }

    /// <summary>
    /// 题库管理侧边栏菜单
    /// </summary>
    public enum ItemSideMenu
    {

    }
}