using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace KST.Web
{
    /// <summary>
    /// 文件下载辅助类
    /// </summary>
    public class DownloadUtil
    {
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileName">文件下载时显示的名称</param>
        /// <param name="filePath">文件路径</param>
        public static FileResult Download(string fileName, string filePath)
        {
            FileResult fileResult = null;

            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.Charset = "utf-8";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AppendHeader("content-Disposition", string.Format("attachment; filename={0}", HttpUtility.UrlEncode(fileName)));
            FileInfo file = new FileInfo(filePath);
            HttpContext.Current.Response.WriteFile(file.FullName);
            HttpContext.Current.Response.End();

            return fileResult;
        }
    }
}