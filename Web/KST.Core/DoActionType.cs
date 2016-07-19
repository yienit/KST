using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace KST.Core
{
    /// <summary>
    /// 操作类型枚举(用于权限检测及操作日志记录)
    /// </summary>
    public enum DoActionType
    {
        #region Account

        /// <summary>
        /// 登录
        /// </summary>
        [Description("登录")]
        Login,

        /// <summary>
        /// 注销
        /// </summary>
        [Description("注销")]
        Logout,

        /// <summary>
        /// 修改密码
        /// </summary>
        [Description("修改密码")]
        UpdatePassword,

        #endregion

        #region Agency

        /// <summary>
        /// 修改机构信息
        /// </summary>
        [Description("修改机构信息")]
        UpdateAgency,

        /// <summary>
        /// 修改机构配置
        /// </summary>
        [Description("修改机构配置")]
        UpdageAgencyConfig,

        #endregion

        #region AgencyAdmin

        /// <summary>
        /// 添加管理员
        /// </summary>
        [Description("添加管理员")]
        AddAdmin,

        /// <summary>
        /// 更新管理员
        /// </summary>
        [Description("更新管理员")]
        UpdateAdmin,

        /// <summary>
        /// 删除管理员
        /// </summary>
        [Description("删除管理员")]
        DeleteAdmin,

        /// <summary>
        /// 删除管理员(批量)
        /// </summary>
        [Description("删除管理员(批量)")]
        DeleteAdminInBatch,

        #endregion

        #region Chapter

        /// <summary>
        /// 添加章节
        /// </summary>
        [Description("添加章节")]
        AddChapter,

        /// <summary>
        /// 更新章节
        /// </summary>
        [Description("更新章节")]
        UpdateChapter,

        /// <summary>
        /// 上调章节序号
        /// </summary>
        [Description("上调章节序号")]
        UpChapter,

        /// <summary>
        /// 下调章节序号
        /// </summary>
        [Description("下调章节序号")]
        DownChapter,

        /// <summary>
        /// 删除章节
        /// </summary>
        [Description("删除章节")]
        DeleteChapter,

        /// <summary>
        /// 删除章节(批量)
        /// </summary>
        [Description("删除章节(批量)")]
        DeleteChapterInBatch,

        #endregion

        #region Single

        /// <summary>
        /// 添加单选题
        /// </summary>
        [Description("添加单选题")]
        AddSingle,

        /// <summary>
        /// 更新单选题
        /// </summary>
        [Description("更新单选题")]
        UpdateSingle,

        /// <summary>
        /// 删除单选题
        /// </summary>
        [Description("删除单选题")]
        DeleteSingle,

        /// <summary>
        /// 删除单选题(批量)
        /// </summary>
        [Description("删除单选题(批量")]
        DeleteSingleInBatch,

        #endregion

        #region Multiple

        /// <summary>
        /// 添加多选题
        /// </summary>
        [Description("添加多选题")]
        AddMultiple,

        /// <summary>
        /// 更新多选题
        /// </summary>
        [Description("更新多选题")]
        UpdateMultiple,

        /// <summary>
        /// 删除多选题
        /// </summary>
        [Description("删除多选题")]
        DeleteMultiple,

        /// <summary>
        /// 删除多选题(批量)
        /// </summary>
        [Description("删除多选题(批量)")]
        DeleteMultipleInBatch,

        #endregion

        #region Judge

        /// <summary>
        /// 添加判断题
        /// </summary>
        [Description("添加判断题")]
        AddJudge,

        /// <summary>
        /// 更新判断题
        /// </summary>
        [Description("更新判断题")]
        UpdateJudge,

        /// <summary>
        /// 删除判断题
        /// </summary>
        [Description("删除判断题")]
        DeleteJudge,

        /// <summary>
        /// 删除判断题(批量)
        /// </summary>
        [Description("删除判断题(批量)")]
        DeleteJudgeInBatch,

        #endregion

        #region Uncertain

        /// <summary>
        /// 添加不定项选择题
        /// </summary>
        [Description("添加不定项选择题")]
        AddUncertain,

        /// <summary>
        /// 更新不定项选择题
        /// </summary>
        [Description("更新不定项选择题")]
        UpdateUncertain,

        /// <summary>
        /// 删除不定项选择题
        /// </summary>
        [Description("删除不定项选择题")]
        DeleteUncertain,

        /// <summary>
        /// 删除不定项选择题(批量)
        /// </summary>
        [Description("删除不定项选择题(批量)")]
        DeleteUncertainInBatch,

        //==================== 子选择题  ======================

        /// <summary>
        /// 添加不定项子选择题
        /// </summary>
        [Description("添加不定项子选择题")]
        AddUncertainSubChoice,

        /// <summary>
        /// 更新不定项子选择题
        /// </summary>
        [Description("更新不定项子选择题")]
        UpdateUncertainSubChoice,

        /// <summary>
        /// 上调不定项子选择题题目序号
        /// </summary>
        [Description("上调不定项子选择题题目序号")]
        UpUncertainSubChoice,

        /// <summary>
        /// 下调不定项子选择题题目序号
        /// </summary>
        [Description("下调不定项子选择题题目序号")]
        DownUncertainSubChoice,

        /// <summary>
        /// 删除不定项子选择题
        /// </summary>
        [Description("删除不定项子选择题")]
        DeleteUncertainSubChoice,

        /// <summary>
        /// 删除不定项子选择题(批量)
        /// </summary>
        [Description("删除不定项子选择题(批量)")]
        DeleteUncertainSubChoiceInBatch,

        #endregion

        #region FenLu

        /// <summary>
        /// 添加分录题
        /// </summary>
        [Description("添加分录题")]
        AddFenLu,

        /// <summary>
        /// 更新分录题
        /// </summary>
        [Description("更新分录题")]
        UpdateFenLu,

        /// <summary>
        /// 删除分录题
        /// </summary>
        [Description("删除分录题")]
        DeleteFenLu,

        /// <summary>
        /// 删除分录题(批量)
        /// </summary>
        [Description("删除分录题(批量)")]
        DeleteFenLuInBatch,

        #endregion

        #region NumberBlank

        /// <summary>
        /// 添加数字填空题
        /// </summary>
        [Description("添加数字填空题")]
        AddNumberBlank,

        /// <summary>
        /// 更新数字填空题
        /// </summary>
        [Description("更新数字填空题")]
        UpdateNumberBlank,

        /// <summary>
        /// 删除数字填空题
        /// </summary>
        [Description("删除数字填空题")]
        DeleteNumberBlank,

        /// <summary>
        /// 删除数字填空题(批量)
        /// </summary>
        [Description("删除数字填空题(批量)")]
        DeleteNumberBlankInBatch,

        #endregion
    }
}
