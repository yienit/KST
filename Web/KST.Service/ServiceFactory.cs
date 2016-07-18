
namespace KST.Service
{
    /// <summary>
    /// Factory for service.
    /// </summary>
    public class ServiceFactory
    {
        private static ServiceFactory instance;

        private AgencyDataService agencyDataService;
        private ItemDataService itemDataService;
        private PaperDataService paperDataService;
        private UserDataService userDataService;
        private RecordDataService recordDataService;

        private CommonDataService commonDataService;
        private SecurityService securityService;
        private PermissionService permissionService;

        private static readonly object instanceLocker = new object();
        private static readonly object agencyDataServiceLocker = new object();
        private static readonly object itemDataServiceLocker = new object();
        private static readonly object paperDataServiceLocker = new object();
        private static readonly object userDataServiceLocker = new object();
        private static readonly object recordDataServiceLocker = new object();
        private static readonly object commonDataServiceLocker = new object();
        private static readonly object securityServiceLocker = new object();
        private static readonly object permissionServiceLocker = new object();

        private ServiceFactory() { }

        /// <summary>
        /// Gets instance for ServiceFactory.
        /// </summary>
        public static ServiceFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (instanceLocker)
                    {
                        if (instance == null)
                        {
                            instance = new ServiceFactory();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Gets instance of AgencyDataService.
        /// </summary>
        public AgencyDataService AgencyDataService
        {
            get
            {
                if (agencyDataService == null)
                {
                    lock (agencyDataServiceLocker)
                    {
                        if (agencyDataService == null)
                        {
                            agencyDataService = new AgencyDataService();
                        }
                    }
                }
                return agencyDataService;
            }
        }

        /// <summary>
        /// Gets instance of ItemDataService.
        /// </summary>
        public ItemDataService ItemDataService
        {
            get
            {
                if (itemDataService == null)
                {
                    lock (itemDataServiceLocker)
                    {
                        if (itemDataService == null)
                        {
                            itemDataService = new ItemDataService();
                        }
                    }
                }
                return itemDataService;
            }
        }

        /// <summary>
        /// Gets instance of PaperDataService.
        /// </summary>
        public PaperDataService PaperDataService
        {
            get
            {
                if (paperDataService == null)
                {
                    lock (paperDataServiceLocker)
                    {
                        if (paperDataService == null)
                        {
                            paperDataService = new PaperDataService();
                        }
                    }
                }
                return paperDataService;
            }
        }

        /// <summary>
        /// Gets instance of UserDataService.
        /// </summary>
        public UserDataService UserDataService
        {
            get
            {
                if (userDataService == null)
                {
                    lock (userDataServiceLocker)
                    {
                        if (userDataService == null)
                        {
                            userDataService = new UserDataService();
                        }
                    }
                }
                return userDataService;
            }
        }

        /// <summary>
        /// Gets instance of RecordDataService.
        /// </summary>
        public RecordDataService RecordDataService
        {
            get
            {
                if (recordDataService == null)
                {
                    lock (recordDataServiceLocker)
                    {
                        if (recordDataService == null)
                        {
                            recordDataService = new RecordDataService();
                        }
                    }
                }
                return recordDataService;
            }
        }

        /// <summary>
        /// Gets instance of CommonDataService.
        /// </summary>
        public CommonDataService CommonDataService
        {
            get
            {
                if (commonDataService == null)
                {
                    lock (commonDataServiceLocker)
                    {
                        if (commonDataService == null)
                        {
                            commonDataService = new CommonDataService();
                        }
                    }
                }
                return commonDataService;
            }
        }

        /// <summary>
        /// Gets instance of SecurityService.
        /// </summary>
        public SecurityService SecurityService
        {
            get
            {
                if (securityService == null)
                {
                    lock (securityServiceLocker)
                    {
                        if (securityService == null)
                        {
                            securityService = new SecurityService();
                        }
                    }
                }
                return securityService;
            }
        }

        /// <summary>
        /// Gets instance of PermissionService.
        /// </summary>
        public PermissionService PermissionService
        {
            get
            {
                if (permissionService == null)
                {
                    lock (permissionServiceLocker)
                    {
                        if (permissionService == null)
                        {
                            permissionService = new PermissionService();
                        }
                    }
                }
                return permissionService;
            }
        }
    }
}
