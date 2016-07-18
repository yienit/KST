using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace KST
{
    static class Program
    {
        private static Mutex mutex = new Mutex(true, "cfea6d89-c1c9-4631-a163-0dc027170c0c");
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(Program));

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                // 禁止程序重复运行
                if (mutex.WaitOne(TimeSpan.Zero, true))
                {
                    // 初始化日志文件监视器
                    InitLogFileWatcher();

                    Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    Application.Run(FormInstanceManager.Instance.FormMain);

                    // 释放Metux
                    mutex.ReleaseMutex();
                }
                else
                {
                    // 显示已运行主窗体
                    Win32.PostMessage((IntPtr)Win32.HWND_BROADCAST, Win32.WM_SHOW_FRAME, IntPtr.Zero, IntPtr.Zero);
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
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
        static void LogFireWatcherCreated(object sender, FileSystemEventArgs e)
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
