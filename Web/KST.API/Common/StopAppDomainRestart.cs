using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace KST.API
{
    /// <summary>
    /// Stops the ASP.NET AppDomain being restarted (which clears 
    /// Session state, Cache etc.) whenever a folder is deleted. 
    /// </summary>
    public class StopAppDomainRestart
    {
        /// <summary>
        /// 防止删除目录时Appdomain重启导致session 丢失
        /// </summary>
        public static void Init()
        {
            PropertyInfo p = typeof(System.Web.HttpRuntime).GetProperty("FileChangesMonitor", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
            object o = p.GetValue(null, null);
            FieldInfo f = o.GetType().GetField("_dirMonSubdirs", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.IgnoreCase);
            object monitor = f.GetValue(o);
            MethodInfo m = monitor.GetType().GetMethod("StopMonitoring", BindingFlags.Instance | BindingFlags.NonPublic);
            m.Invoke(monitor, new object[] { });
        }
    }
}