
namespace KST.DAL
{
    /// <summary>
    /// Factory for DAL.
    /// </summary>
    public class DALFactory
    {
        private static DALFactory instance;

        // Agency data
        private AgencyDAL agencyDAL;
        private AgencyAdminDAL agencyAdminDAL;
        private AgencyConfigDAL agencyConfigDAL;
        private AgencyCreatorDAL agencyCreatorDAL;

        // Item data
        private SysCourseDAL sysCourseDAL;
        private SysChapterDAL sysChapterDAL;
        private CourseDAL courseDAL;
        private ChapterDAL chapterDAL;
        private SingleItemDAL singleItemDAL;
        private MultipleItemDAL multipleItemDAL;
        private JudgeItemDAL judgeItemDAL;
        private UncertainItemDAL uncertainItemDAL;
        private UncertainSubChoiceDAL uncertainSubChoiceDAL;
        private FenLuItemDAL fenLuItemDAL;
        private NumberBlankItemDAL numberBlankItemDAL;

        // Paper data
        private PaperDAL paperDAL;
        private PaperSingleDAL paperSingleDAL;
        private PaperMultipleDAL paperMultipleDAL;
        private PaperJudgeDAL paperJudgeDAL;

        // User data
        private UserDAL userDAL;
        private MyCommentDAL myCommentDAL;
        private MyPostErrorItemDAL myPostErrorItemDAL;
        private MyErrorDAL myErrorDAL;
        private MyFavoriteDAL myFavoriteDAL;
        private MyScoreDAL myScoreDAL;

        // Record data
        private CaptchaRecordDAL captchaDAL;
        private ClientLoginRecordDAL clientLoginRecordDAL;
        private AdminDoRecordDAL adminDoRecordDAL;
        private FeedbackRecordDAL feedbackRecordDAL;

        //========================= Locker  ===================================

        private static readonly object instanceLocker = new object();

        // Agency data
        private static readonly object agencyDALLocker = new object();
        private static readonly object agencyAdminDALLocker = new object();
        private static readonly object agencyConfigDALLocker = new object();
        private static readonly object agencyCreatorDALLocker = new object();

        // Item data
        private static readonly object sysCourseDALLocker = new object();
        private static readonly object sysChapterDALLocker = new object();
        private static readonly object courseDALLocker = new object();
        private static readonly object chapterDALLocker = new object();
        private static readonly object singleItemDALLocker = new object();
        private static readonly object multipleItemDALLocker = new object();
        private static readonly object judgeItemDALLocker = new object();
        private static readonly object uncertainItemDALLocker = new object();
        private static readonly object uncertainSubChoiceDALLocker = new object();
        private static readonly object fenLuItemDALLocker = new object();
        private static readonly object numberBlankItemDALLocker = new object();

        // Paper Data
        private static readonly object paperDALLocker = new object();
        private static readonly object paperSingleDALLocker = new object();
        private static readonly object paperMultipleDALLocker = new object();
        private static readonly object paperJudgeDALLocker = new object();

        // User data
        private static readonly object userDALLocker = new object();
        private static readonly object myCommentDALLocker = new object();
        private static readonly object myPostErrorItemDALLocker = new object();
        private static readonly object myErrorDALLocker = new object();
        private static readonly object myFavoriteDALLocker = new object();
        private static readonly object myScoreDALLocker = new object();

        // Record data
        private static readonly object captchaDALLocker = new object();
        private static readonly object clientLoginRecordDALLocker = new object();
        private static readonly object adminDoRecordDALLocker = new object();
        private static readonly object feedbackRecordDALLocker = new object();

        #region Constructor

        private DALFactory() { }

        #endregion

        /// <summary>
        /// Gets DAL factory instance.
        /// </summary>
        public static DALFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (instanceLocker)
                    {
                        if (instance == null)
                        {
                            instance = new DALFactory();
                        }
                    }
                }
                return instance;
            }
        }

        //===================================================

        #region Agency Data DAL

        /// <summary>
        /// Gets AgencyDAL instance object.
        /// </summary>
        public AgencyDAL AgencyDAL
        {
            get
            {
                if (agencyDAL == null)
                {
                    lock (agencyDALLocker)
                    {
                        if (agencyDAL == null)
                        {
                            agencyDAL = new AgencyDAL();
                        }
                    }
                }
                return agencyDAL;
            }
        }

        /// <summary>
        /// Gets AgencyAdminDAL instance object.
        /// </summary>
        public AgencyAdminDAL AgencyAdminDAL
        {
            get
            {
                if (agencyAdminDAL == null)
                {
                    lock (agencyAdminDALLocker)
                    {
                        if (agencyAdminDAL == null)
                        {
                            agencyAdminDAL = new AgencyAdminDAL();
                        }
                    }
                }
                return agencyAdminDAL;
            }
        }

        /// <summary>
        /// Gets AgencyConfigDAL instance object.
        /// </summary>
        public AgencyConfigDAL AgencyConfigDAL
        {
            get
            {
                if (agencyConfigDAL == null)
                {
                    lock (agencyConfigDALLocker)
                    {
                        if (agencyConfigDAL == null)
                        {
                            agencyConfigDAL = new AgencyConfigDAL();
                        }
                    }
                }
                return agencyConfigDAL;
            }
        }

        /// <summary>
        /// Gets AgencyCreatorDAL instance object.
        /// </summary>
        public AgencyCreatorDAL AgencyCreatorDAL
        {
            get
            {
                if (agencyCreatorDAL == null)
                {
                    lock (agencyCreatorDALLocker)
                    {
                        if (agencyCreatorDAL == null)
                        {
                            agencyCreatorDAL = new AgencyCreatorDAL();
                        }
                    }
                }
                return agencyCreatorDAL;
            }
        }

        #endregion

        #region Item Data DAL

        /// <summary>
        /// Gets SysCourseDAL instance.
        /// </summary>
        public SysCourseDAL SysCourseDAL
        {
            get
            {
                if (sysCourseDAL == null)
                {
                    lock (sysCourseDALLocker)
                    {
                        if (sysCourseDAL == null)
                        {
                            sysCourseDAL = new SysCourseDAL();
                        }
                    }
                }
                return sysCourseDAL;
            }
        }

        /// <summary>
        /// Gets SysChapterDAL instance.
        /// </summary>
        public SysChapterDAL SysChapterDAL
        {
            get
            {
                if (sysChapterDAL == null)
                {
                    lock (sysChapterDALLocker)
                    {
                        if (sysChapterDAL == null)
                        {
                            sysChapterDAL = new SysChapterDAL();
                        }
                    }
                }
                return sysChapterDAL;
            }
        }

        /// <summary>
        /// Gets CourseDAL instance.
        /// </summary>
        public CourseDAL CourseDAL
        {
            get
            {
                if (courseDAL == null)
                {
                    lock (courseDALLocker)
                    {
                        if (courseDAL == null)
                        {
                            courseDAL = new CourseDAL();
                        }
                    }
                }
                return courseDAL;
            }
        }

        /// <summary>
        /// Gets CourseDAL instance.
        /// </summary>
        public ChapterDAL ChapterDAL
        {
            get
            {
                if (chapterDAL == null)
                {
                    lock (chapterDALLocker)
                    {
                        if (chapterDAL == null)
                        {
                            chapterDAL = new ChapterDAL();
                        }
                    }
                }
                return chapterDAL;
            }
        }

        /// <summary>
        /// Gets SingleItemDAL instance.
        /// </summary>
        public SingleItemDAL SingleItemDAL
        {
            get
            {
                if (singleItemDAL == null)
                {
                    lock (singleItemDALLocker)
                    {
                        if (singleItemDAL == null)
                        {
                            singleItemDAL = new SingleItemDAL();
                        }
                    }
                }
                return singleItemDAL;
            }
        }

        /// <summary>
        /// Gets MultipleItemDAL instance.
        /// </summary>
        public MultipleItemDAL MultipleItemDAL
        {
            get
            {
                if (multipleItemDAL == null)
                {
                    lock (multipleItemDALLocker)
                    {
                        if (multipleItemDAL == null)
                        {
                            multipleItemDAL = new MultipleItemDAL();
                        }
                    }
                }
                return multipleItemDAL;
            }
        }

        /// <summary>
        /// Gets JudgeItemDAL instance.
        /// </summary>
        public JudgeItemDAL JudgeItemDAL
        {
            get
            {
                if (judgeItemDAL == null)
                {
                    lock (judgeItemDALLocker)
                    {
                        if (judgeItemDAL == null)
                        {
                            judgeItemDAL = new JudgeItemDAL();
                        }
                    }
                }
                return judgeItemDAL;
            }
        }

        /// <summary>
        /// Gets UncertainItemDAL instance.
        /// </summary>
        public UncertainItemDAL UncertainItemDAL
        {
            get
            {
                if (uncertainItemDAL == null)
                {
                    lock (uncertainItemDALLocker)
                    {
                        if (uncertainItemDAL == null)
                        {
                            uncertainItemDAL = new UncertainItemDAL();
                        }
                    }
                }
                return uncertainItemDAL;
            }
        }

        /// <summary>
        /// Gets UncertainSubChoiceDAL instance.
        /// </summary>
        public UncertainSubChoiceDAL UncertainSubChoiceDAL
        {
            get
            {
                if (uncertainSubChoiceDAL == null)
                {
                    lock (uncertainSubChoiceDALLocker)
                    {
                        if (uncertainSubChoiceDAL == null)
                        {
                            uncertainSubChoiceDAL = new UncertainSubChoiceDAL();
                        }
                    }
                }
                return uncertainSubChoiceDAL;
            }
        }

        /// <summary>
        /// Gets FenLuItemDAL instance.
        /// </summary>
        public FenLuItemDAL FenLuItemDAL
        {
            get
            {
                if (fenLuItemDAL == null)
                {
                    lock (fenLuItemDALLocker)
                    {
                        if (fenLuItemDAL == null)
                        {
                            fenLuItemDAL = new FenLuItemDAL();
                        }
                    }
                }
                return fenLuItemDAL;
            }
        }

        /// <summary>
        /// Gets NumberBlankItemDAL instance.
        /// </summary>
        public NumberBlankItemDAL NumberBlankItemDAL
        {
            get
            {
                if (numberBlankItemDAL == null)
                {
                    lock (numberBlankItemDALLocker)
                    {
                        if (numberBlankItemDAL == null)
                        {
                            numberBlankItemDAL = new NumberBlankItemDAL();
                        }
                    }
                }
                return numberBlankItemDAL;
            }
        }

        #endregion

        #region Paper Data DAL

        /// <summary>
        /// Gets PaperDAL instance.
        /// </summary>
        public PaperDAL PaperDAL
        {
            get
            {
                if (paperDAL == null)
                {
                    lock (paperDALLocker)
                    {
                        if (paperDAL == null)
                        {
                            paperDAL = new PaperDAL();
                        }
                    }
                }
                return paperDAL;
            }
        }

        /// <summary>
        /// Gets PaperSingleDAL instance.
        /// </summary>
        public PaperSingleDAL PaperSingleDAL
        {
            get
            {
                if (paperSingleDAL == null)
                {
                    lock (paperSingleDALLocker)
                    {
                        if (paperSingleDAL == null)
                        {
                            paperSingleDAL = new PaperSingleDAL();
                        }
                    }
                }
                return paperSingleDAL;
            }
        }

        /// <summary>
        /// Gets PaperMultipleDAL instance.
        /// </summary>
        public PaperMultipleDAL PaperMultipleDAL
        {
            get
            {
                if (paperMultipleDAL == null)
                {
                    lock (paperMultipleDALLocker)
                    {
                        if (paperMultipleDAL == null)
                        {
                            paperMultipleDAL = new PaperMultipleDAL();
                        }
                    }
                }
                return paperMultipleDAL;
            }
        }

        /// <summary>
        /// Gets PaperJudgeDAL instance.
        /// </summary>
        public PaperJudgeDAL PaperJudgeDAL
        {
            get
            {
                if (paperJudgeDAL == null)
                {
                    lock (paperJudgeDALLocker)
                    {
                        if (paperJudgeDAL == null)
                        {
                            paperJudgeDAL = new PaperJudgeDAL();
                        }
                    }
                }
                return paperJudgeDAL;
            }
        }

        #endregion

        #region User Data DAL

        /// <summary>
        /// Gets UserDAL instance object.
        /// </summary>
        public UserDAL UserDAL
        {
            get
            {
                if (userDAL == null)
                {
                    lock (userDALLocker)
                    {
                        if (userDAL == null)
                        {
                            userDAL = new UserDAL();
                        }
                    }
                }
                return userDAL;
            }
        }

        /// <summary>
        /// Gets MyCommentDAL instance.
        /// </summary>
        public MyCommentDAL MyCommentDAL
        {
            get
            {
                if (myCommentDAL == null)
                {
                    lock (myCommentDALLocker)
                    {
                        if (myCommentDAL == null)
                        {
                            myCommentDAL = new MyCommentDAL();
                        }
                    }
                }
                return myCommentDAL;
            }
        }

        /// <summary>
        /// Gets MyPostErrorItemDAL instance.
        /// </summary>
        public MyPostErrorItemDAL MyPostErrorItemDAL
        {
            get
            {
                if (myPostErrorItemDAL == null)
                {
                    lock (myPostErrorItemDALLocker)
                    {
                        if (myPostErrorItemDAL == null)
                        {
                            myPostErrorItemDAL = new MyPostErrorItemDAL();
                        }
                    }
                }
                return myPostErrorItemDAL;
            }
        }

        /// <summary>
        /// Gets MyErrorDAL instance.
        /// </summary>
        public MyErrorDAL MyErrorDAL
        {
            get
            {
                if (myErrorDAL == null)
                {
                    lock (myErrorDALLocker)
                    {
                        if (myErrorDAL == null)
                        {
                            myErrorDAL = new MyErrorDAL();
                        }
                    }
                }
                return myErrorDAL;
            }
        }

        /// <summary>
        /// Gets MyFavoriteDAL instance.
        /// </summary>
        public MyFavoriteDAL MyFavoriteDAL
        {
            get
            {
                if (myFavoriteDAL == null)
                {
                    lock (myFavoriteDALLocker)
                    {
                        if (myFavoriteDAL == null)
                        {
                            myFavoriteDAL = new MyFavoriteDAL();
                        }
                    }
                }
                return myFavoriteDAL;
            }
        }

        /// <summary>
        /// Gets MyScoreDAL instance.
        /// </summary>
        public MyScoreDAL MyScoreDAL
        {
            get
            {
                if (myScoreDAL == null)
                {
                    lock (myScoreDALLocker)
                    {
                        if (myScoreDAL == null)
                        {
                            myScoreDAL = new MyScoreDAL();
                        }
                    }
                }
                return myScoreDAL;
            }
        }

        #endregion

        #region Record Data DAL

        /// <summary>
        /// Gets CaptchaRecordDAL instance object.
        /// </summary>
        public CaptchaRecordDAL CaptchaRecordDAL
        {
            get
            {
                if (captchaDAL == null)
                {
                    lock (captchaDALLocker)
                    {
                        if (captchaDAL == null)
                        {
                            captchaDAL = new CaptchaRecordDAL();
                        }
                    }
                }
                return captchaDAL;
            }
        }

        /// <summary>
        /// Gets ClientLoginRecordDAL instance.
        /// </summary>
        public ClientLoginRecordDAL ClientLoginRecordDAL
        {
            get
            {
                if (clientLoginRecordDAL == null)
                {
                    lock (clientLoginRecordDALLocker)
                    {
                        if (clientLoginRecordDAL == null)
                        {
                            clientLoginRecordDAL = new ClientLoginRecordDAL();
                        }
                    }
                }
                return clientLoginRecordDAL;
            }
        }

        /// <summary>
        /// Gets AdminDoRecordDAL instance.
        /// </summary>
        public AdminDoRecordDAL AdminDoRecordDAL
        {
            get
            {
                if (adminDoRecordDAL == null)
                {
                    lock (adminDoRecordDALLocker)
                    {
                        if (adminDoRecordDAL == null)
                        {
                            adminDoRecordDAL = new AdminDoRecordDAL();
                        }
                    }
                }
                return adminDoRecordDAL;
            }
        }

        /// <summary>
        /// Gets FeedbackRecordDAL instance object.
        /// </summary>
        public FeedbackRecordDAL FeedbackRecordDAL
        {
            get
            {
                if (feedbackRecordDAL == null)
                {
                    lock (feedbackRecordDALLocker)
                    {
                        if (feedbackRecordDAL == null)
                        {
                            feedbackRecordDAL = new FeedbackRecordDAL();
                        }
                    }
                }
                return feedbackRecordDAL;
            }
        }

        #endregion
    }
}
