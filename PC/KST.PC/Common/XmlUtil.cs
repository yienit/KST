using System.IO;
using System.Xml;

namespace KST
{
    /// <summary>
    /// XML文件操作辅助类类
    /// </summary>
    public class XmlUtil
    {
        private static XmlDocument doc = new XmlDocument();

        /// <summary>
        /// 获取XML文件中指定节点的值
        /// 如果该节点不存在或没有值则返回一个默认的值
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <param name="nodePath">节点路径(xPath格式)</param>
        /// <param name="defaultValue">返回的默认值,默认为空</param>
        public static string ReadValue(string filePath, string nodePath, string defaultValue = "")
        {
            string value = defaultValue;
            if (File.Exists(filePath))
            {
                try
                {
                    doc.Load(filePath);
                    XmlNode resultNode = doc.SelectSingleNode(nodePath);

                    if (resultNode != null)     //节点存在
                    {
                        value = !string.IsNullOrEmpty(resultNode.InnerText) ? resultNode.InnerText : defaultValue;
                    }
                }
                catch { }
            }
            return value;
        }

        /// <summary>
        /// 设置XML文件中指定节点的值
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <param name="nodePath">节点路径(xPath格式)</param>
        /// <param name="value">需要设置的值</param>
        public static bool WriteValue(string filePath, string nodePath, string value)
        {
            bool isSucceed = false;
            if (File.Exists(filePath))
            {
                try
                {
                    doc.Load(filePath);
                    XmlNode resultNode = doc.SelectSingleNode(nodePath);

                    if (resultNode != null)     //节点存在
                    {
                        resultNode.InnerText = value;
                        doc.Save(filePath);
                        isSucceed = true;
                    }
                }
                catch { isSucceed = false; }
            }
            return isSucceed;
        }

        /// <summary>
        /// 获取XML文件中指定节点的属性值
        /// 如果该节点不存在或属性不存在则返回一个默认的值
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <param name="nodePath">节点路径(xPath格式)</param>
        /// <param name="name">属性名称</param>
        /// <param name="defaultValue">返回的默认值,默认为空</param>
        public static string ReadAttribute(string filePath, string nodePath, string name, string defaultValue = "")
        {
            string value = defaultValue;
            if (File.Exists(filePath))
            {
                try
                {
                    doc.Load(filePath);
                    XmlNode resultNode = doc.SelectSingleNode(nodePath);

                    if (resultNode != null)     //节点存在
                    {
                        XmlElement element = (XmlElement)resultNode;
                        value = element.GetAttribute(name);
                    }
                }
                catch { }
            }

            return value;
        }

        /// <summary>
        /// 设置XML文件中指定节点的属性值
        /// </summary>
        /// <param name="filePath">XML文件路径</param>
        /// <param name="nodePath">节点路径(xPath格式)</param>
        /// <param name="name">属性名称</param>
        /// <param name="value">属性值</param>
        public static bool WriteAttribute(string filePath, string nodePath, string name, string value)
        {
            bool isSucceed = false;
            if (File.Exists(filePath))
            {
                try
                {
                    doc.Load(filePath);
                    XmlNode resultNode = doc.SelectSingleNode(nodePath);

                    if (resultNode != null)     //节点存在
                    {
                        XmlElement element = (XmlElement)resultNode;
                        element.SetAttribute(name, value);
                        doc.Save(filePath);
                        isSucceed = true;
                    }
                }
                catch { isSucceed = false; }
            }
            return isSucceed;
        }
    }
}
