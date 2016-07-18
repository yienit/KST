using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KST.DAL;
using KST.DTO;
using KST.Model;
using KST.Core;
using KST.Model.Model;

namespace KST.Service
{
    /// <summary>
    /// 题库管理服务，提供对系统课程、章节、题库等数据管理服务
    /// </summary>
    public class ItemDataService
    {
        private CourseDAL courseDAL = DALFactory.Instance.CourseDAL;
        private ChapterDAL chapterDAL = DALFactory.Instance.ChapterDAL;

        private SingleItemDAL singleDAL = DALFactory.Instance.SingleItemDAL;
        private MultipleItemDAL multipleDAL = DALFactory.Instance.MultipleItemDAL;
        private JudgeItemDAL judgeDAL = DALFactory.Instance.JudgeItemDAL;
        private UncertainItemDAL uncertainDAL = DALFactory.Instance.UncertainItemDAL;
        private UncertainSubChoiceDAL uncertainSubChoiceDAL = DALFactory.Instance.UncertainSubChoiceDAL;
        private FenLuItemDAL fenluDAL = DALFactory.Instance.FenLuItemDAL;
        private NumberBlankItemDAL numberBlankDAL = DALFactory.Instance.NumberBlankItemDAL;

        private log4net.ILog log = log4net.LogManager.GetLogger(typeof(ItemDataService));

        #region Course

        /// <summary>
        /// 根据科目主键ID获取科目信息
        /// </summary>
        public ServiceInvokeDTO<Course> GetCourseByID(int courseID)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<Course> result = null;
            try
            {
                Course course = courseDAL.GetByID(courseID);
                result = new ServiceInvokeDTO<Course>(InvokeCode.SYS_INVOKE_SUCCESS, course);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 获取培训机构的所有考试科目信息
        /// </summary>
        public ServiceInvokeDTO<List<Course>> GetAgencyCourses(int agencyID)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<List<Course>> result = null;
            try
            {
                List<Course> courses = courseDAL.GetByAgencyID(agencyID);
                result = new ServiceInvokeDTO<List<Course>>(InvokeCode.SYS_INVOKE_SUCCESS, courses);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        #endregion

        #region Chapter

        /// <summary>
        /// 根据课程主键ID获取考试课程下的所有章节信息
        /// </summary>
        public ServiceInvokeDTO<List<Chapter>> GetAgencyChapters(int courseID)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<List<Chapter>> result = null;
            try
            {
                List<Chapter> chapters = chapterDAL.GetByCourseID(courseID);
                result = new ServiceInvokeDTO<List<Chapter>>(InvokeCode.SYS_INVOKE_SUCCESS, chapters);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 根据主键ID获取章节
        /// </summary>
        public ServiceInvokeDTO<Chapter> GetChapterByID(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<Chapter> result = null;
            try
            {
                Chapter chapter = chapterDAL.GetByID(id);
                result = new ServiceInvokeDTO<Chapter>(InvokeCode.SYS_INVOKE_SUCCESS, chapter);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 添加章节
        /// </summary>
        public ServiceInvokeDTO AddChapter(Chapter chapter)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                // 检测章节名称是否存在
                Chapter dbChapter = chapterDAL.GetByName(chapter.CourseID, chapter.Name);
                if (dbChapter == null)
                {
                    // 叠加章节序号
                    chapter.ChapterIndex = chapterDAL.GetLastChapterIndex(chapter.CourseID) + 1;

                    chapterDAL.Insert(chapter);
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.ITEM_CHAPTER_NAME_EXIST_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 更新章节
        /// </summary>
        public ServiceInvokeDTO UpdateChapter(Chapter chapter)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                Chapter dbChapter = chapterDAL.GetByID(chapter.ID);
                if (dbChapter != null)
                {
                    // 检测章节名称是否存在
                    Chapter chapterWithName = chapterDAL.GetByName(dbChapter.CourseID, chapter.Name);
                    if (chapterWithName != null && chapterWithName.ID != chapter.ID)
                    {
                        result = new ServiceInvokeDTO(InvokeCode.ITEM_CHAPTER_NAME_EXIST_ERROR);
                    }
                    else
                    {
                        chapter.ChapterIndex = dbChapter.ChapterIndex;
                        chapterDAL.Update(chapter);
                        result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                    }
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 上调章节序号
        /// </summary>
        public ServiceInvokeDTO UpChapter(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                Chapter currentChapter = chapterDAL.GetByID(id);
                if (currentChapter != null)
                {
                    // 前一个章节
                    Chapter foreChapter = chapterDAL.GetForeChapter(currentChapter.CourseID, currentChapter.ChapterIndex);

                    if (foreChapter != null)
                    {
                        // 互换章节序号
                        chapterDAL.UpdateChapterIndex(currentChapter.ID, foreChapter.ChapterIndex);
                        chapterDAL.UpdateChapterIndex(foreChapter.ID, currentChapter.ChapterIndex);
                    }

                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 下调章节序号
        /// </summary>
        public ServiceInvokeDTO DownChapter(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                Chapter currentChapter = chapterDAL.GetByID(id);
                if (currentChapter != null)
                {
                    // 后一个章节
                    Chapter afterChapter = chapterDAL.GetAfterChapter(currentChapter.CourseID, currentChapter.ChapterIndex);

                    if (afterChapter != null)
                    {
                        // 互换章节序号
                        chapterDAL.UpdateChapterIndex(currentChapter.ID, afterChapter.ChapterIndex);
                        chapterDAL.UpdateChapterIndex(afterChapter.ID, currentChapter.ChapterIndex);
                    }

                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除章节
        /// </summary>
        public ServiceInvokeDTO DeleteChapter(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                Chapter dbChapter = chapterDAL.GetByID(id);
                if (dbChapter != null)
                {
                    chapterDAL.DeleteByID(id);
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除章节(批量删除)
        /// </summary>
        public ServiceInvokeDTO DeleteChapter(List<int> ids)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                chapterDAL.DeleteInBatch(ids);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        #endregion

        #region Single

        /// <summary>
        /// 以分页的形式单选题
        /// </summary>
        public ServiceInvokeDTO<QueryResultDTO<SingleItemDTO>> QuerySingle(QueryArgsDTO<SingleItem> queryDTO, int courseID)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<QueryResultDTO<SingleItemDTO>> result = null;
            try
            {
                QueryResultDTO<SingleItemDTO> resultData = null;

                // -->DTO
                QueryResultDTO<SingleItem> queryData = singleDAL.Query(queryDTO, courseID);
                if (queryData != null)
                {
                    resultData = new QueryResultDTO<SingleItemDTO>();
                    resultData.PageIndex = queryData.PageIndex;
                    resultData.PageSize = queryData.PageSize;
                    resultData.TotalRecordCount = queryData.TotalRecordCount;

                    List<SingleItemDTO> dtos = new List<SingleItemDTO>();
                    if (queryData.List != null)
                    {
                        foreach (var single in queryData.List)
                        {
                            SingleItemDTO singleDTO = new SingleItemDTO(single);
                            singleDTO.ChapterName = chapterDAL.GetByID(single.ChapterID).Name;
                            dtos.Add(singleDTO);
                        }
                    }

                    resultData.List = dtos;
                }

                result = new ServiceInvokeDTO<QueryResultDTO<SingleItemDTO>>(InvokeCode.SYS_INVOKE_SUCCESS, resultData);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<QueryResultDTO<SingleItemDTO>>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 根据主键ID获取单选题
        /// </summary>
        public ServiceInvokeDTO<SingleItemDTO> GetSingleByID(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<SingleItemDTO> result = null;
            try
            {
                SingleItemDTO singleDTO = null;

                // --> DTO
                SingleItem single = singleDAL.GetByID(id);
                if (single != null)
                {
                    singleDTO = new SingleItemDTO(single);
                    singleDTO.ChapterName = chapterDAL.GetByID(single.ChapterID).Name;
                }
                result = new ServiceInvokeDTO<SingleItemDTO>(InvokeCode.SYS_INVOKE_SUCCESS, singleDTO);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 添加单选题
        /// </summary>
        public ServiceInvokeDTO AddSingle(SingleItem single)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                singleDAL.Insert(single);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 添加单选题(批量添加)
        /// </summary>
        public ServiceInvokeDTO AddSingle(List<SingleItem> singles)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                singleDAL.Insert(singles);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 更新单选题
        /// </summary>
        public ServiceInvokeDTO UpdateSingle(SingleItem single)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                SingleItem dbSingle = singleDAL.GetByID(single.ID);
                if (dbSingle != null)
                {
                    singleDAL.Update(single);
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除单选题
        /// </summary>
        public ServiceInvokeDTO DeleteSingle(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                SingleItem dbSingle = singleDAL.GetByID(id);
                if (dbSingle != null)
                {
                    singleDAL.DeleteByID(id);
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除单选题(批量删除)
        /// </summary>
        public ServiceInvokeDTO DeleteSingle(List<int> ids)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                singleDAL.DeleteInBatch(ids);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        #endregion

        #region Multiple

        /// <summary>
        /// 以分页的形式多选题
        /// </summary>
        public ServiceInvokeDTO<QueryResultDTO<MultipleItemDTO>> QueryMultiple(QueryArgsDTO<MultipleItem> queryDTO, int courseID)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<QueryResultDTO<MultipleItemDTO>> result = null;
            try
            {
                QueryResultDTO<MultipleItemDTO> resultData = null;

                // -->DTO
                QueryResultDTO<MultipleItem> queryData = multipleDAL.Query(queryDTO, courseID);
                if (queryData != null)
                {
                    resultData = new QueryResultDTO<MultipleItemDTO>();
                    resultData.PageIndex = queryData.PageIndex;
                    resultData.PageSize = queryData.PageSize;
                    resultData.TotalRecordCount = queryData.TotalRecordCount;

                    List<MultipleItemDTO> dtos = new List<MultipleItemDTO>();
                    if (queryData.List != null)
                    {
                        foreach (var multiple in queryData.List)
                        {
                            MultipleItemDTO multipleDTO = new MultipleItemDTO(multiple);
                            multipleDTO.ChapterName = chapterDAL.GetByID(multiple.ChapterID).Name;
                            dtos.Add(multipleDTO);
                        }
                    }

                    resultData.List = dtos;
                }

                result = new ServiceInvokeDTO<QueryResultDTO<MultipleItemDTO>>(InvokeCode.SYS_INVOKE_SUCCESS, resultData);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<QueryResultDTO<MultipleItemDTO>>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 根据主键ID获取多选题
        /// </summary>
        public ServiceInvokeDTO<MultipleItemDTO> GetMultipleByID(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<MultipleItemDTO> result = null;
            try
            {
                MultipleItemDTO multipleDTO = null;

                // --> DTO
                MultipleItem multiple = multipleDAL.GetByID(id);
                if (multiple != null)
                {
                    multipleDTO = new MultipleItemDTO(multiple);
                    multipleDTO.ChapterName = chapterDAL.GetByID(multiple.ChapterID).Name;
                }
                result = new ServiceInvokeDTO<MultipleItemDTO>(InvokeCode.SYS_INVOKE_SUCCESS, multipleDTO);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 添加多选题
        /// </summary>
        public ServiceInvokeDTO AddMultiple(MultipleItem multiple)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                multipleDAL.Insert(multiple);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 添加多选题(批量添加)
        /// </summary>
        public ServiceInvokeDTO AddMultiple(List<MultipleItem> multiples)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                multipleDAL.Insert(multiples);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 更新多选题
        /// </summary>
        public ServiceInvokeDTO UpdateMultiple(MultipleItem multiple)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                MultipleItem dbMulitiple = multipleDAL.GetByID(multiple.ID);
                if (dbMulitiple != null)
                {
                    multipleDAL.Update(multiple);
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除多选题
        /// </summary>
        public ServiceInvokeDTO DeleteMultiple(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                MultipleItem dbMultiple = multipleDAL.GetByID(id);
                if (dbMultiple != null)
                {
                    multipleDAL.DeleteByID(id);
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除多选题(批量删除)
        /// </summary>
        public ServiceInvokeDTO DeleteMultiple(List<int> ids)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                multipleDAL.DeleteInBatch(ids);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        #endregion

        #region Judge

        /// <summary>
        /// 以分页的形式判断题
        /// </summary>
        public ServiceInvokeDTO<QueryResultDTO<JudgeItemDTO>> QueryJudge(QueryArgsDTO<JudgeItem> queryDTO, int courseID)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<QueryResultDTO<JudgeItemDTO>> result = null;
            try
            {
                QueryResultDTO<JudgeItemDTO> resultData = null;

                // -->DTO
                QueryResultDTO<JudgeItem> queryData = judgeDAL.Query(queryDTO, courseID);
                if (queryData != null)
                {
                    resultData = new QueryResultDTO<JudgeItemDTO>();
                    resultData.PageIndex = queryData.PageIndex;
                    resultData.PageSize = queryData.PageSize;
                    resultData.TotalRecordCount = queryData.TotalRecordCount;

                    List<JudgeItemDTO> dtos = new List<JudgeItemDTO>();
                    if (queryData.List != null)
                    {
                        foreach (var judge in queryData.List)
                        {
                            JudgeItemDTO judgeDTO = new JudgeItemDTO(judge);
                            judgeDTO.ChapterName = chapterDAL.GetByID(judge.ChapterID).Name;
                            dtos.Add(judgeDTO);
                        }
                    }

                    resultData.List = dtos;
                }

                result = new ServiceInvokeDTO<QueryResultDTO<JudgeItemDTO>>(InvokeCode.SYS_INVOKE_SUCCESS, resultData);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<QueryResultDTO<JudgeItemDTO>>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 根据主键ID获取判断题
        /// </summary>
        public ServiceInvokeDTO<JudgeItemDTO> GetJudgeByID(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<JudgeItemDTO> result = null;
            try
            {
                JudgeItemDTO judgeDTO = null;

                // --> DTO
                JudgeItem judge = judgeDAL.GetByID(id);
                if (judge != null)
                {
                    judgeDTO = new JudgeItemDTO(judge);
                    judgeDTO.ChapterName = chapterDAL.GetByID(judge.ChapterID).Name;
                }
                result = new ServiceInvokeDTO<JudgeItemDTO>(InvokeCode.SYS_INVOKE_SUCCESS, judgeDTO);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 添加判断题
        /// </summary>
        public ServiceInvokeDTO AddJudge(JudgeItem judge)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                judgeDAL.Insert(judge);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 添加判断题(批量添加)
        /// </summary>
        public ServiceInvokeDTO AddJudge(List<JudgeItem> judges)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                judgeDAL.Insert(judges);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 更新判断题
        /// </summary>
        public ServiceInvokeDTO UpdateJudge(JudgeItem judge)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                JudgeItem dbJudge = judgeDAL.GetByID(judge.ID);
                if (dbJudge != null)
                {
                    judgeDAL.Update(judge);
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除判断题
        /// </summary>
        public ServiceInvokeDTO DeleteJudge(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                JudgeItem dbJudge = judgeDAL.GetByID(id);
                if (dbJudge != null)
                {
                    judgeDAL.DeleteByID(id);
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除判断题(批量删除)
        /// </summary>
        public ServiceInvokeDTO DeleteJudge(List<int> ids)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                judgeDAL.DeleteInBatch(ids);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        #endregion

        #region Uncertain

        /// <summary>
        /// 以分页的形式查询不定项选择题
        /// </summary>
        public ServiceInvokeDTO<QueryResultDTO<UncertainItemDTO>> QueryUncertain(QueryArgsDTO<UncertainItem> queryDTO, int courseID)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<QueryResultDTO<UncertainItemDTO>> result = null;
            try
            {
                QueryResultDTO<UncertainItemDTO> resultData = null;

                // -->DTO
                QueryResultDTO<UncertainItem> queryData = uncertainDAL.Query(queryDTO, courseID);
                if (queryData != null)
                {
                    resultData = new QueryResultDTO<UncertainItemDTO>();
                    resultData.PageIndex = queryData.PageIndex;
                    resultData.PageSize = queryData.PageSize;
                    resultData.TotalRecordCount = queryData.TotalRecordCount;

                    List<UncertainItemDTO> dtos = new List<UncertainItemDTO>();
                    if (queryData.List != null)
                    {
                        foreach (var uncertain in queryData.List)
                        {
                            UncertainItemDTO uncertainDTO = new UncertainItemDTO(uncertain);
                            uncertainDTO.ChapterName = chapterDAL.GetByID(uncertain.ChapterID).Name;
                            uncertainDTO.SubChoices = uncertainSubChoiceDAL.GetByUncertainItemID(uncertain.ID);
                            dtos.Add(uncertainDTO);
                        }
                    }

                    resultData.List = dtos;
                }

                result = new ServiceInvokeDTO<QueryResultDTO<UncertainItemDTO>>(InvokeCode.SYS_INVOKE_SUCCESS, resultData);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<QueryResultDTO<UncertainItemDTO>>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 根据主键ID获取不定项选择题
        /// </summary>
        public ServiceInvokeDTO<UncertainItemDTO> GetUncertainByID(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<UncertainItemDTO> result = null;
            try
            {
                UncertainItemDTO uncertainDTO = null;

                // --> DTO
                UncertainItem uncertain = uncertainDAL.GetByID(id);
                if (uncertain != null)
                {
                    uncertainDTO = new UncertainItemDTO(uncertain);
                    uncertainDTO.ChapterName = chapterDAL.GetByID(uncertain.ChapterID).Name;
                    uncertainDTO.SubChoices = uncertainSubChoiceDAL.GetByUncertainItemID(uncertain.ID);
                }
                result = new ServiceInvokeDTO<UncertainItemDTO>(InvokeCode.SYS_INVOKE_SUCCESS, uncertainDTO);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 添加不定项选择题
        /// </summary>
        public ServiceInvokeDTO AddUncertain(UncertainItem uncertain)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                uncertainDAL.Insert(uncertain);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 添加不定项选择题(批量添加)
        /// </summary>
        public ServiceInvokeDTO AddUncertain(List<UncertainItem> uncertains)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                uncertainDAL.Insert(uncertains);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 更新不定项选择题
        /// </summary>
        public ServiceInvokeDTO UpdateUncertain(UncertainItem uncertain)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                UncertainItem dbUncertain = uncertainDAL.GetByID(uncertain.ID);
                if (dbUncertain != null)
                {
                    uncertainDAL.Update(uncertain);
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除不定项选择题
        /// </summary>
        public ServiceInvokeDTO DeleteUncertain(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                UncertainItem dbUncertain = uncertainDAL.GetByID(id);
                if (dbUncertain != null)
                {
                    uncertainDAL.DeleteByID(id);
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除不定项选择题(批量删除)
        /// </summary>
        public ServiceInvokeDTO DeleteUncertain(List<int> ids)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                uncertainDAL.DeleteInBatch(ids);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        //=======================================  不定项选择题子选择题  ======================================

        /// <summary>
        /// 根据主键ID获取不定项选择题子选择题
        /// </summary>
        public ServiceInvokeDTO<UncertainSubChoice> GetUncertainSubChoiceByID(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<UncertainSubChoice> result = null;
            try
            {
                UncertainSubChoice subchoice = uncertainSubChoiceDAL.GetByID(id);
                result = new ServiceInvokeDTO<UncertainSubChoice>(InvokeCode.SYS_INVOKE_SUCCESS, subchoice);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 获取指定不定项选择的所有子选择题
        /// </summary>
        public ServiceInvokeDTO<List<UncertainSubChoice>> GetUncertainSubChoiceByUncertainID(int uncertainItemID)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<List<UncertainSubChoice>> result = null;
            try
            {
                List<UncertainSubChoice> subchoices = uncertainSubChoiceDAL.GetByUncertainItemID(uncertainItemID);
                result = new ServiceInvokeDTO<List<UncertainSubChoice>>(InvokeCode.SYS_INVOKE_SUCCESS, subchoices);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 添加不定项选择题子选择题
        /// </summary>
        public ServiceInvokeDTO AddUncertainSubChoice(UncertainSubChoice uncertainSubChoice)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                // 叠加题目序号
                uncertainSubChoice.ItemIndex = uncertainSubChoiceDAL.GetLastItemIndex(uncertainSubChoice.UncertainItemID) + 1;

                uncertainSubChoiceDAL.Insert(uncertainSubChoice);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 添加不定项选择题子选择题(批量添加)
        /// </summary>
        public ServiceInvokeDTO AddUncertainSubChoice(List<UncertainSubChoice> uncertainSubChoices)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                uncertainSubChoiceDAL.Insert(uncertainSubChoices);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 更新不定项选择题子选择题
        /// </summary>
        public ServiceInvokeDTO UpdateUncertainSubChoice(UncertainSubChoice uncertainSubChoice)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                UncertainSubChoice dbSubChoice = uncertainSubChoiceDAL.GetByID(uncertainSubChoice.ID);
                if (dbSubChoice != null)
                {
                    uncertainSubChoiceDAL.Update(uncertainSubChoice);
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 上调不定项子选择题题目序号
        /// </summary>
        public ServiceInvokeDTO UpUncertainSubChoice(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                UncertainSubChoice currentSubchoice = uncertainSubChoiceDAL.GetByID(id);
                if (currentSubchoice != null)
                {
                    // 前一个题目
                    UncertainSubChoice foreSubchoice = uncertainSubChoiceDAL.GetForeItem(currentSubchoice.UncertainItemID, currentSubchoice.ItemIndex);

                    if (foreSubchoice != null)
                    {
                        // 互换题目序号
                        uncertainSubChoiceDAL.UpdateItemIndex(currentSubchoice.ID, foreSubchoice.ItemIndex);
                        uncertainSubChoiceDAL.UpdateItemIndex(foreSubchoice.ID, currentSubchoice.ItemIndex);
                    }

                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 下调不定项子选择题题目序号
        /// </summary>
        public ServiceInvokeDTO DownUncertainSubChoice(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                UncertainSubChoice currentSubchoice = uncertainSubChoiceDAL.GetByID(id);
                if (currentSubchoice != null)
                {
                    // 后一个题目
                    UncertainSubChoice afterSubchoice = uncertainSubChoiceDAL.GetAfterItem(currentSubchoice.UncertainItemID, currentSubchoice.ItemIndex);

                    if (afterSubchoice != null)
                    {
                        // 互换题目序号
                        uncertainSubChoiceDAL.UpdateItemIndex(currentSubchoice.ID, afterSubchoice.ItemIndex);
                        uncertainSubChoiceDAL.UpdateItemIndex(afterSubchoice.ID, currentSubchoice.ItemIndex);
                    }

                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除不定项选择题子选择题
        /// </summary>
        public ServiceInvokeDTO DeleteUncertainSubChoice(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {

                UncertainSubChoice dbSubChoice = uncertainSubChoiceDAL.GetByID(id);
                if (dbSubChoice != null)
                {
                    uncertainSubChoiceDAL.DeleteByID(id);
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除不定项选择题子选择题(批量删除)
        /// </summary>
        public ServiceInvokeDTO DeleteUncertainSubChoiceInBatch(List<int> ids)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                uncertainSubChoiceDAL.DeleteInBatch(ids);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }


        #endregion

        #region FenLu

        /// <summary>
        /// 以分页的形式查询分录题
        /// </summary>
        public ServiceInvokeDTO<QueryResultDTO<FenLuItemDTO>> QueryFenLu(QueryArgsDTO<FenLuItem> queryDTO, int courseID)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<QueryResultDTO<FenLuItemDTO>> result = null;
            try
            {
                QueryResultDTO<FenLuItemDTO> resultData = null;

                // -->DTO
                QueryResultDTO<FenLuItem> queryData = fenluDAL.Query(queryDTO, courseID);
                if (queryData != null)
                {
                    resultData = new QueryResultDTO<FenLuItemDTO>();
                    resultData.PageIndex = queryData.PageIndex;
                    resultData.PageSize = queryData.PageSize;
                    resultData.TotalRecordCount = queryData.TotalRecordCount;

                    List<FenLuItemDTO> dtos = new List<FenLuItemDTO>();
                    if (queryData.List != null)
                    {
                        foreach (var fenlu in queryData.List)
                        {
                            FenLuItemDTO fenluDTO = new FenLuItemDTO(fenlu);
                            fenluDTO.ChapterName = chapterDAL.GetByID(fenlu.ChapterID).Name;
                            fenluDTO.Answers = fenluDAL.GetAnswers(fenlu.ID);
                            dtos.Add(fenluDTO);
                        }
                    }

                    resultData.List = dtos;
                }

                result = new ServiceInvokeDTO<QueryResultDTO<FenLuItemDTO>>(InvokeCode.SYS_INVOKE_SUCCESS, resultData);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<QueryResultDTO<FenLuItemDTO>>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 根据主键ID获取分录题
        /// </summary>
        public ServiceInvokeDTO<FenLuItemDTO> GetFenLuByID(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<FenLuItemDTO> result = null;
            try
            {
                FenLuItemDTO fenluDTO = null;

                // --> DTO
                FenLuItem fenlu = fenluDAL.GetByID(id);
                if (fenlu != null)
                {
                    fenluDTO = new FenLuItemDTO(fenlu);
                    fenluDTO.ChapterName = chapterDAL.GetByID(fenlu.ChapterID).Name;
                    fenluDTO.Answers = fenluDAL.GetAnswers(fenlu.ID);
                }
                result = new ServiceInvokeDTO<FenLuItemDTO>(InvokeCode.SYS_INVOKE_SUCCESS, fenluDTO);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 添加分录题
        /// </summary>
        public ServiceInvokeDTO AddFenLu(FenLuItem fenlu, List<FenLuAnswer> answers)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                fenluDAL.Insert(fenlu, answers);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 更新分录题
        /// </summary>
        public ServiceInvokeDTO UpdateFenLu(FenLuItem fenlu, List<FenLuAnswer> answers)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                FenLuItem dbFenlu = fenluDAL.GetByID(fenlu.ID);
                if (dbFenlu != null)
                {
                    fenluDAL.Update(fenlu, answers);
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除分录题
        /// </summary>
        public ServiceInvokeDTO DeleteFenLu(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                FenLuItem dbFenlu = fenluDAL.GetByID(id);
                if (dbFenlu != null)
                {
                    fenluDAL.DeleteByID(id);
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除分录题(批量删除)
        /// </summary>
        public ServiceInvokeDTO DeleteFenLu(List<int> ids)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                fenluDAL.DeleteInBatch(ids);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        #endregion

        #region NumberBlank

        /// <summary>
        /// 以分页的形式查询数字填空题
        /// </summary>
        public ServiceInvokeDTO<QueryResultDTO<NumberBlankItemDTO>> QueryNumberBlank(QueryArgsDTO<NumberBlankItem> queryDTO, int courseID)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<QueryResultDTO<NumberBlankItemDTO>> result = null;
            try
            {
                QueryResultDTO<NumberBlankItemDTO> resultData = null;

                // -->DTO
                QueryResultDTO<NumberBlankItem> queryData = numberBlankDAL.Query(queryDTO, courseID);
                if (queryData != null)
                {
                    resultData = new QueryResultDTO<NumberBlankItemDTO>();
                    resultData.PageIndex = queryData.PageIndex;
                    resultData.PageSize = queryData.PageSize;
                    resultData.TotalRecordCount = queryData.TotalRecordCount;

                    List<NumberBlankItemDTO> dtos = new List<NumberBlankItemDTO>();
                    if (queryData.List != null)
                    {
                        foreach (var numberBlank in queryData.List)
                        {
                            NumberBlankItemDTO numberBlankDTO = new NumberBlankItemDTO(numberBlank);
                            numberBlankDTO.ChapterName = chapterDAL.GetByID(numberBlank.ChapterID).Name;
                            numberBlankDTO.Answers = numberBlankDAL.GetAnswers(numberBlank.ID);
                            dtos.Add(numberBlankDTO);
                        }
                    }

                    resultData.List = dtos;
                }

                result = new ServiceInvokeDTO<QueryResultDTO<NumberBlankItemDTO>>(InvokeCode.SYS_INVOKE_SUCCESS, resultData);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                result = new ServiceInvokeDTO<QueryResultDTO<NumberBlankItemDTO>>(InvokeCode.SYS_INNER_ERROR);
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 根据主键ID获取数字填空题
        /// </summary>
        public ServiceInvokeDTO<NumberBlankItemDTO> GetNumberBlankByID(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO<NumberBlankItemDTO> result = null;
            try
            {
                NumberBlankItemDTO numberBlankDTO = null;

                // --> DTO
                NumberBlankItem numberBlank = numberBlankDAL.GetByID(id);
                if (numberBlank != null)
                {
                    numberBlankDTO = new NumberBlankItemDTO(numberBlank);
                    numberBlankDTO.ChapterName = chapterDAL.GetByID(numberBlank.ChapterID).Name;
                    numberBlankDTO.Answers = numberBlankDAL.GetAnswers(numberBlank.ID);
                }
                result = new ServiceInvokeDTO<NumberBlankItemDTO>(InvokeCode.SYS_INVOKE_SUCCESS, numberBlankDTO);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 添加数字填空题
        /// </summary>
        public ServiceInvokeDTO AddNumberBlank(NumberBlankItem numberBlank, List<NumberBlankAnswer> answers)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                numberBlankDAL.Insert(numberBlank, answers);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 更新数字填空题
        /// </summary>
        public ServiceInvokeDTO UpdateNumberBlank(NumberBlankItem numberBlank, List<NumberBlankAnswer> answers)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                NumberBlankItem dbNumberBlank = numberBlankDAL.GetByID(numberBlank.ID);
                if (dbNumberBlank != null)
                {
                    numberBlankDAL.Update(numberBlank, answers);
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除数字填空题
        /// </summary>
        public ServiceInvokeDTO DeleteNumberBlank(int id)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                NumberBlankItem dbNumberBlank = numberBlankDAL.GetByID(id);
                if (dbNumberBlank != null)
                {
                    numberBlankDAL.DeleteByID(id);
                    result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
                }
                else
                {
                    result = new ServiceInvokeDTO(InvokeCode.SYS_OBJECT_NOT_EXIST_ERROR);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        /// <summary>
        /// 删除数字填空题(批量删除)
        /// </summary>
        public ServiceInvokeDTO DeleteNumberBlank(List<int> ids)
        {
            log.Debug(Constant.DEBUG_START);
            ServiceInvokeDTO result = null;
            try
            {
                numberBlankDAL.DeleteInBatch(ids);
                result = new ServiceInvokeDTO(InvokeCode.SYS_INVOKE_SUCCESS);
            }
            catch (Exception ex)
            {
                log.Error(ex);
                throw ex;
            }
            log.Debug(Constant.DEBUG_END);

            return result;
        }

        #endregion
    }
}
