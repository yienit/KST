using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KST.Core
{
    /// <summary>
    /// 操作类型枚举(用于权限检测及操作日志记录)
    /// </summary>
    public enum DoActionType
    {
        #region AgencyAdmin

        /// <summary>
        /// 添加管理员
        /// </summary>
        AddAdmin,

        /// <summary>
        /// 更新管理员
        /// </summary>
        UpdateAdmin,

        /// <summary>
        /// 删除管理员
        /// </summary>
        DeleteAdmin,

        /// <summary>
        /// 删除管理员(批量)
        /// </summary>
        DeleteAdminInBatch,

        #endregion

        #region Chapter

        /// <summary>
        /// 添加章节
        /// </summary>
        AddChapter,

        /// <summary>
        /// 更新章节
        /// </summary>
        UpdateChapter,

        /// <summary>
        /// 上调章节序号
        /// </summary>
        UpChapter,

        /// <summary>
        /// 下调章节序号
        /// </summary>
        DownChapter,

        /// <summary>
        /// 删除章节
        /// </summary>
        DeleteChapter,

        /// <summary>
        /// 删除章节(批量)
        /// </summary>
        DeleteChapterInBatch,

        #endregion

        #region Single

        /// <summary>
        /// 添加单选题
        /// </summary>
        AddSingle,

        /// <summary>
        /// 更新单选题
        /// </summary>
        UpdateSingle,

        /// <summary>
        /// 删除单选题
        /// </summary>
        DeleteSingle,

        /// <summary>
        /// 删除单选题(批量)
        /// </summary>
        DeleteSingleInBatch,

        #endregion

        #region Multiple

        /// <summary>
        /// 添加多选题
        /// </summary>
        AddMultiple,

        /// <summary>
        /// 更新多选题
        /// </summary>
        UpdateMultiple,

        /// <summary>
        /// 删除多选题
        /// </summary>
        DeleteMultiple,

        /// <summary>
        /// 删除多选题(批量)
        /// </summary>
        DeleteMultipleInBatch,

        #endregion

        #region Judge

        /// <summary>
        /// 添加判断题
        /// </summary>
        AddJudge,

        /// <summary>
        /// 更新判断题
        /// </summary>
        UpdateJudge,

        /// <summary>
        /// 删除判断题
        /// </summary>
        DeleteJudge,

        /// <summary>
        /// 删除判断题(批量)
        /// </summary>
        DeleteJudgeInBatch,

        #endregion

        #region Uncertain

        /// <summary>
        /// 添加不定项选择题
        /// </summary>
        AddUncertain,

        /// <summary>
        /// 更新不定项选择题
        /// </summary>
        UpdateUncertain,

        /// <summary>
        /// 删除不定项选择题
        /// </summary>
        DeleteUncertain,

        /// <summary>
        /// 删除不定项选择题(批量)
        /// </summary>
        DeleteUncertainInBatch,

        //==================== 子选择题  ======================

        /// <summary>
        /// 添加不定项子选择题
        /// </summary>
        AddUncertainSubChoice,

        /// <summary>
        /// 更新不定项子选择题
        /// </summary>
        UpdateUncertainSubChoice,

        /// <summary>
        /// 上调不定项子选择题题目序号
        /// </summary>
        UpUncertainSubChoice,

        /// <summary>
        /// 下调不定项子选择题题目序号
        /// </summary>
        DownUncertainSubChoice,

        /// <summary>
        /// 删除不定项子选择题
        /// </summary>
        DeleteUncertainSubChoice,

        /// <summary>
        /// 删除不定项子选择题(批量)
        /// </summary>
        DeleteUncertainSubChoiceInBatch,

        #endregion

        #region FenLu

        /// <summary>
        /// 添加分录题
        /// </summary>
        AddFenLu,

        /// <summary>
        /// 更新分录题
        /// </summary>
        UpdateFenLu,

        /// <summary>
        /// 删除分录题
        /// </summary>
        DeleteFenLu,

        /// <summary>
        /// 删除分录题(批量)
        /// </summary>
        DeleteFenLuInBatch,

        #endregion

        #region NumberBlank

        /// <summary>
        /// 添加数字填空题
        /// </summary>
        AddNumberBlank,

        /// <summary>
        /// 更新数字填空题
        /// </summary>
        UpdateNumberBlank,

        /// <summary>
        /// 删除数字填空题
        /// </summary>
        DeleteNumberBlank,

        /// <summary>
        /// 删除数字填空题(批量)
        /// </summary>
        DeleteNumberBlankInBatch,

        #endregion
    }
}
