﻿using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using KST.Model;
using System.IO;
using System;


namespace KST.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    public class MvcApplication : System.Web.HttpApplication
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(MvcApplication));

        protected void Application_Start()
        {
            log.Debug(Constant.DEBUG_START);

            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            StopAppDomainRestart.Init();
            InitConfig();
            InitLogFileWatcher();

            log.Debug(Constant.DEBUG_END);
        }

        /// <summary>
        /// 初始化配置文件
        /// </summary>
        private static void InitConfig()
        {
            // DAL
            KST.DAL.ConnectionManager.ConnectionString = Config.ConnectionString;

            // Service
            KST.Service.Config.CaptchaGetTimeSpan = Config.CaptchaGetTimeSpan;
            KST.Service.Config.CaptchaExpireTime = Config.CaptchaExpireTime;
            KST.Service.Config.SmsApiKey = Config.SmsApiKey;
            KST.Service.Config.SmsRegTemplate = Config.SmsRegTemplate;
            KST.Service.Config.SmsGetPasswordTemplate = Config.SmsGetPasswordTemplate;
        }

        /// <summary>
        /// 初始化日志文件监视器
        /// </summary>
        private static void InitLogFileWatcher()
        {
            string logPath = AppDomain.CurrentDomain.BaseDirectory + @"Log\";

            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }

            FileSystemWatcher logFireWatcher = new FileSystemWatcher(logPath, "*.TXT");
            logFireWatcher.EnableRaisingEvents = true;
            logFireWatcher.Created += new FileSystemEventHandler(LogFireWatcherCreated);
        }

        /// <summary>
        /// 日志文件个数监控函数
        /// </summary>
        private static void LogFireWatcherCreated(object sender, FileSystemEventArgs e)
        {
            try
            {
                string logPath = AppDomain.CurrentDomain.BaseDirectory + @"Log\";
                DirectoryInfo dir = new DirectoryInfo(logPath);
                FileInfo[] logFiles = dir.GetFiles();
                if (logFiles != null && logFiles.Length > Config.MaxLogFileCount)
                {
                    int needDeleteCount = logFiles.Length - Config.MaxLogFileCount;
                    for (int i = 0; i < needDeleteCount; i++)
                    {
                        File.Delete(logFiles[i].FullName);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }
}