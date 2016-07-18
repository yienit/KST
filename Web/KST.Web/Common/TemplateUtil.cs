using System;
using System.Collections.Generic;
using System.IO;
using NPOI.SS.UserModel;
using KST.Model;
using KST.DTO;
using KST.Service;
using KST.Core;


namespace KST.Web
{
    /// <summary>
    /// 读取题库Excel模板数据辅助类
    /// </summary>
    public class TemplateUtil
    {
        private const string TEXT_EMPTY_ERROR = "必填的字段为空错误";
        private const string IS_VIP_ITEM_ERROR = "题库类型格式错误";
        private const string CHAPTER_ID_ERROR = "试题章节名称格式错误";
        private const string ANSWER_ARG_ERROR = "试题答案格式错误";
        private const string DIFFICULTY_ARG_ERROR = "试题难易度格式错误";

        private const string IS_VIP_ITEM_NO = "普通题库";
        private const string IS_VIP_ITEM_YES = "VIP题库";

        private const string ANSWER_A = "A";
        private const string ANSWER_B = "B";
        private const string ANSWER_C = "C";
        private const string ANSWER_D = "D";

        private const string ANSWER_CORRECT = "正确";
        private const string ANSWER_ERROR = "错误";

        private const string MULTIPLE_ANSWER_SPILT = "、";

        private const int DIFFICULTY_MIN = 1;
        private const int DIFFICULTY_MAX = 5;

        /// <summary>
        /// 读取单选题题库模板数据
        /// </summary>
        /// <param name="adminDTO">管理员对象</param>
        /// <param name="courseID">课程主键ID</param>
        /// <param name="fileName">Excel文件路径名称</param>
        /// <param name="isFirstRowTitle">第一行是否为标题</param>
        public static List<SingleItem> ReadSingleTemplate(AgencyAdminDTO adminDTO, int courseID, string fileName, bool isFirstRowTitle)
        {
            List<SingleItem> singles = new List<SingleItem>();

            // 获取所有章节
            ServiceInvokeDTO<List<Chapter>> chaptersResult = ServiceFactory.Instance.ItemDataService.GetAgencyChapters(courseID);
            if (chaptersResult.Code == InvokeCode.SYS_INVOKE_SUCCESS && chaptersResult.Data != null && chaptersResult.Data.Count > 0)
            {
                List<Chapter> chapters = chaptersResult.Data;

                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    // 默认读取第一张表
                    IWorkbook workBook = WorkbookFactory.Create(fs);
                    ISheet sheet = workBook.GetSheetAt(0);

                    int startRowIndex = 0;
                    if (isFirstRowTitle && sheet.LastRowNum >= 1)
                    {
                        startRowIndex = 1;
                    }

                    for (int index = startRowIndex; index <= sheet.LastRowNum; index++)
                    {
                        IRow row = sheet.GetRow(index);
                        if (row != null)
                        {
                            // 题库类型
                            ICell isVipItemCell = row.GetCell(0); if (isVipItemCell == null) { throw new ArgumentException(TEXT_EMPTY_ERROR); }
                            string isVipItem = isVipItemCell.ToString().Trim();
                            if (!isVipItem.Equals(IS_VIP_ITEM_NO) && !isVipItem.Equals(IS_VIP_ITEM_YES))
                            {
                                throw new ArgumentException(IS_VIP_ITEM_ERROR);
                            }

                            // 所属章节
                            ICell chapterCell = row.GetCell(1); if (chapterCell == null) { throw new ArgumentException(TEXT_EMPTY_ERROR); }
                            string chapterName = chapterCell.ToString().Trim();
                            int chapterID = 0;
                            foreach (var chapter in chapters)
                            {
                                if (chapterName.Equals(chapter.Name))
                                {
                                    chapterID = chapter.ID;
                                    break;
                                }
                            }
                            if (chapterID == 0)
                            {
                                throw new ArgumentException(CHAPTER_ID_ERROR);
                            }

                            ICell titleCell = row.GetCell(2); if (titleCell == null) { throw new ArgumentException(TEXT_EMPTY_ERROR); }
                            ICell aCell = row.GetCell(3); if (aCell == null) { throw new ArgumentException(TEXT_EMPTY_ERROR); }
                            ICell bCell = row.GetCell(4); if (bCell == null) { throw new ArgumentException(TEXT_EMPTY_ERROR); }
                            ICell cCell = row.GetCell(5); if (cCell == null) { throw new ArgumentException(TEXT_EMPTY_ERROR); }
                            ICell dCell = row.GetCell(6); if (dCell == null) { throw new ArgumentException(TEXT_EMPTY_ERROR); }

                            // 答案
                            ICell amswerCell = row.GetCell(7); if (amswerCell == null) { throw new ArgumentException(TEXT_EMPTY_ERROR); }
                            string answer = amswerCell.ToString().Trim().ToUpper();
                            if (!answer.Equals(ANSWER_A) && !answer.Equals(ANSWER_B) && !answer.Equals(ANSWER_C) && !answer.Equals(ANSWER_D))
                            {
                                throw new ArgumentException(ANSWER_ARG_ERROR);
                            }

                            // 试题难易度
                            ICell difficltyCell = row.GetCell(9); if (difficltyCell == null) { throw new ArgumentException(TEXT_EMPTY_ERROR); }
                            string difficltyString = difficltyCell.ToString().Trim();

                            int difficulty = 0;
                            if (int.TryParse(difficltyString, out difficulty))
                            {
                                if (difficulty < DIFFICULTY_MIN || difficulty > DIFFICULTY_MAX)
                                {
                                    throw new ArgumentException(DIFFICULTY_ARG_ERROR);
                                }
                            }
                            else
                            {
                                throw new ArgumentException(DIFFICULTY_ARG_ERROR);
                            }

                            SingleItem single = new SingleItem();
                            single.AgencyID = adminDTO.Agency.ID;
                            single.ChapterID = chapterID;
                            single.IsVipItem = isVipItem.Equals(IS_VIP_ITEM_YES) ? 1 : 0;
                            single.Title = titleCell.ToString();
                            single.A = aCell.ToString();
                            single.B = bCell.ToString();
                            single.C = cCell.ToString();
                            single.D = dCell.ToString();
                            single.Answer = answer;
                            single.Annotation = row.GetCell(8) == null ? string.Empty : row.GetCell(8).ToString();
                            single.Difficulty = difficulty;
                            single.AddPerson = adminDTO.ChineseName;

                            singles.Add(single);
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentException(CHAPTER_ID_ERROR);
            }

            return singles;
        }

        /// <summary>
        /// 读取多选题题库模板数据
        /// </summary>
        /// <param name="adminDTO">管理员对象</param>
        /// <param name="courseID">课程主键ID</param>
        /// <param name="fileName">Excel文件路径名称</param>
        /// <param name="isFirstRowTitle">第一行是否为标题</param>
        public static List<MultipleItem> ReadMultipleTemplate(AgencyAdminDTO adminDTO, int courseID, string fileName, bool isFirstRowTitle)
        {
            List<MultipleItem> multiples = new List<MultipleItem>();

            // 获取所有章节
            ServiceInvokeDTO<List<Chapter>> chaptersResult = ServiceFactory.Instance.ItemDataService.GetAgencyChapters(courseID);
            if (chaptersResult.Code == InvokeCode.SYS_INVOKE_SUCCESS && chaptersResult.Data != null && chaptersResult.Data.Count > 0)
            {
                List<Chapter> chapters = chaptersResult.Data;

                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    // 默认读取第一张表
                    IWorkbook workBook = WorkbookFactory.Create(fs);
                    ISheet sheet = workBook.GetSheetAt(0);

                    int startRowIndex = 0;
                    if (isFirstRowTitle && sheet.LastRowNum >= 1)
                    {
                        startRowIndex = 1;
                    }

                    for (int index = startRowIndex; index <= sheet.LastRowNum; index++)
                    {
                        IRow row = sheet.GetRow(index);
                        if (row != null)
                        {
                            // 题库类型
                            ICell isVipItemCell = row.GetCell(0); if (isVipItemCell == null) { throw new ArgumentException(TEXT_EMPTY_ERROR); }
                            string isVipItem = isVipItemCell.ToString().Trim();
                            if (!isVipItem.Equals(IS_VIP_ITEM_NO) && !isVipItem.Equals(IS_VIP_ITEM_YES))
                            {
                                throw new ArgumentException(IS_VIP_ITEM_ERROR);
                            }

                            // 所属章节
                            ICell chapterCell = row.GetCell(1); if (chapterCell == null) { throw new ArgumentException(TEXT_EMPTY_ERROR); }
                            string chapterName = chapterCell.ToString().Trim();
                            int chapterID = 0;
                            foreach (var chapter in chapters)
                            {
                                if (chapterName.Equals(chapter.Name))
                                {
                                    chapterID = chapter.ID;
                                    break;
                                }
                            }
                            if (chapterID == 0)
                            {
                                throw new ArgumentException(CHAPTER_ID_ERROR);
                            }

                            ICell titleCell = row.GetCell(2); if (titleCell == null) { throw new ArgumentException(TEXT_EMPTY_ERROR); }
                            ICell aCell = row.GetCell(3); if (aCell == null) { throw new ArgumentException(TEXT_EMPTY_ERROR); }
                            ICell bCell = row.GetCell(4); if (bCell == null) { throw new ArgumentException(TEXT_EMPTY_ERROR); }
                            ICell cCell = row.GetCell(5); if (cCell == null) { throw new ArgumentException(TEXT_EMPTY_ERROR); }
                            ICell dCell = row.GetCell(6); if (dCell == null) { throw new ArgumentException(TEXT_EMPTY_ERROR); }

                            // 答案
                            ICell amswerCell = row.GetCell(7); if (amswerCell == null) { throw new ArgumentException(TEXT_EMPTY_ERROR); }
                            string answer = amswerCell.ToString().Trim().ToUpper();
                            if (answer.Length == 1)
                            {
                                if (!answer.Equals(ANSWER_A) && !answer.Equals(ANSWER_B) && !answer.Equals(ANSWER_C) && !answer.Equals(ANSWER_D))
                                {
                                    throw new ArgumentException(ANSWER_ARG_ERROR);
                                }
                            }
                            else
                            {
                                if (!answer.Contains(MULTIPLE_ANSWER_SPILT))
                                {
                                    throw new ArgumentException(ANSWER_ARG_ERROR);
                                }
                            }

                            // 试题难易度
                            ICell difficltyCell = row.GetCell(9); if (difficltyCell == null) { throw new ArgumentException(TEXT_EMPTY_ERROR); }
                            string difficltyString = difficltyCell.ToString().Trim();

                            int difficulty = 0;
                            if (int.TryParse(difficltyString, out difficulty))
                            {
                                if (difficulty < DIFFICULTY_MIN || difficulty > DIFFICULTY_MAX)
                                {
                                    throw new ArgumentException(DIFFICULTY_ARG_ERROR);
                                }
                            }
                            else
                            {
                                throw new ArgumentException(DIFFICULTY_ARG_ERROR);
                            }

                            MultipleItem multiple = new MultipleItem();
                            multiple.AgencyID = adminDTO.Agency.ID;
                            multiple.ChapterID = chapterID;
                            multiple.IsVipItem = isVipItem.Equals(IS_VIP_ITEM_YES) ? 1 : 0;
                            multiple.Title = titleCell.ToString();
                            multiple.A = aCell.ToString();
                            multiple.B = bCell.ToString();
                            multiple.C = cCell.ToString();
                            multiple.D = dCell.ToString();
                            multiple.Answer = answer;
                            multiple.Annotation = row.GetCell(8) == null ? string.Empty : row.GetCell(8).ToString();
                            multiple.Difficulty = difficulty;
                            multiple.AddPerson = adminDTO.ChineseName;

                            multiples.Add(multiple);
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentException(CHAPTER_ID_ERROR);
            }

            return multiples;
        }

        /// <summary>
        /// 读取判断题题库模板数据
        /// </summary>
        /// <param name="adminDTO">管理员对象</param>
        /// <param name="courseID">课程主键ID</param>
        /// <param name="fileName">Excel文件路径名称</param>
        /// <param name="isFirstRowTitle">第一行是否为标题</param>
        public static List<JudgeItem> ReadJudgeTemplate(AgencyAdminDTO adminDTO, int courseID, string fileName, bool isFirstRowTitle)
        {
            List<JudgeItem> judges = new List<JudgeItem>();

            // 获取所有章节
            ServiceInvokeDTO<List<Chapter>> chaptersResult = ServiceFactory.Instance.ItemDataService.GetAgencyChapters(courseID);
            if (chaptersResult.Code == InvokeCode.SYS_INVOKE_SUCCESS && chaptersResult.Data != null && chaptersResult.Data.Count > 0)
            {
                List<Chapter> chapters = chaptersResult.Data;

                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    // 默认读取第一张表
                    IWorkbook workBook = WorkbookFactory.Create(fs);
                    ISheet sheet = workBook.GetSheetAt(0);

                    int startRowIndex = 0;
                    if (isFirstRowTitle && sheet.LastRowNum >= 1)
                    {
                        startRowIndex = 1;
                    }

                    for (int index = startRowIndex; index <= sheet.LastRowNum; index++)
                    {
                        IRow row = sheet.GetRow(index);
                        if (row != null)
                        {
                            // 题库类型
                            ICell isVipItemCell = row.GetCell(0); if (isVipItemCell == null) { throw new ArgumentException(TEXT_EMPTY_ERROR); }
                            string isVipItem = isVipItemCell.ToString().Trim();
                            if (!isVipItem.Equals(IS_VIP_ITEM_NO) && !isVipItem.Equals(IS_VIP_ITEM_YES))
                            {
                                throw new ArgumentException(IS_VIP_ITEM_ERROR);
                            }

                            // 所属章节
                            ICell chapterCell = row.GetCell(1); if (chapterCell == null) { throw new ArgumentException(TEXT_EMPTY_ERROR); }
                            string chapterName = chapterCell.ToString().Trim();
                            int chapterID = 0;
                            foreach (var chapter in chapters)
                            {
                                if (chapterName.Equals(chapter.Name))
                                {
                                    chapterID = chapter.ID;
                                    break;
                                }
                            }
                            if (chapterID == 0)
                            {
                                throw new ArgumentException(CHAPTER_ID_ERROR);
                            }

                            ICell titleCell = row.GetCell(2); if (titleCell == null) { throw new ArgumentException(TEXT_EMPTY_ERROR); }

                            // 答案
                            ICell amswerCell = row.GetCell(3); if (amswerCell == null) { throw new ArgumentException(TEXT_EMPTY_ERROR); }
                            string answerString = amswerCell.ToString().Trim().ToUpper();
                            if (!answerString.Equals(ANSWER_CORRECT) && !answerString.Equals(ANSWER_ERROR))
                            {
                                throw new ArgumentException(ANSWER_ARG_ERROR);
                            }

                            // 试题难易度
                            ICell difficltyCell = row.GetCell(5); if (difficltyCell == null) { throw new ArgumentException(TEXT_EMPTY_ERROR); }
                            string difficltyString = difficltyCell.ToString().Trim();

                            int difficulty = 0;
                            if (int.TryParse(difficltyString, out difficulty))
                            {
                                if (difficulty < DIFFICULTY_MIN || difficulty > DIFFICULTY_MAX)
                                {
                                    throw new ArgumentException(DIFFICULTY_ARG_ERROR);
                                }
                            }
                            else
                            {
                                throw new ArgumentException(DIFFICULTY_ARG_ERROR);
                            }

                            JudgeItem judge = new JudgeItem();
                            judge.AgencyID = adminDTO.Agency.ID;
                            judge.ChapterID = chapterID;
                            judge.IsVipItem = isVipItem.Equals(IS_VIP_ITEM_YES) ? 1 : 0;
                            judge.Title = titleCell.ToString();
                            judge.Answer = answerString.Equals(ANSWER_CORRECT) ? 1 : 0;
                            judge.Annotation = row.GetCell(4) == null ? string.Empty : row.GetCell(4).ToString();
                            judge.Difficulty = difficulty;
                            judge.AddPerson = adminDTO.ChineseName;

                            judges.Add(judge);
                        }
                    }
                }
            }
            else
            {
                throw new ArgumentException(CHAPTER_ID_ERROR);
            }

            return judges;
        }
    }
}