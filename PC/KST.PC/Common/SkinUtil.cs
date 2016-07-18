using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NuiLib;
using System.Drawing;
using System.IO;

namespace KST
{
    /// <summary>
    /// 窗体换肤辅助类
    /// </summary>
    public class SkinUtil
    {
        // 系统默认窗体背景图片及头部按钮文本默认颜色
        private static Image defaultFrameBkg = Properties.Resources.frame_bkg;
        private static Color defaultIconTextColor = Color.White;
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(SkinUtil));

        /// <summary>
        /// 根据皮肤ID加载窗体皮肤
        /// </summary>
        public static void LoadSkin(string skinID)
        {
            try
            {
                string skinPath = AppDomain.CurrentDomain.BaseDirectory + Constant.THEMES_PATH + skinID + @"\";
                if (Directory.Exists(skinPath))
                {
                    string skinXmlFile = skinPath + Constant.SKIN_XML;
                    if (File.Exists(skinXmlFile))
                    {
                        Skin skin = new Skin();
                        skin.SkinID = skinID;
                        skin.Name = XmlUtil.ReadValue(skinXmlFile, Constant.SKIN_XPATH_SKIN_NAME);
                        skin.FrameBkgName = XmlUtil.ReadValue(skinXmlFile, Constant.SKIN_XPATH_FRAME_BKG);
                        skin.FrameBkg = Image.FromFile(skinPath + skin.FrameBkgName);
                        skin.SkinViewName = XmlUtil.ReadValue(skinXmlFile, Constant.SKIN_XPATH_SKIN_VIEW);
                        skin.SkinView = Image.FromFile(skinPath + skin.SkinViewName);

                        // Icon Text Color
                        skin.IconTextColor = ColorTranslator.FromHtml(XmlUtil.ReadValue(skinXmlFile, Constant.SKIN_XPATH_ICON_TEXT_COLOR));

                        FormInstanceManager.Instance.FormMain.LoadSkin(skin);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                FormInstanceManager.Instance.FormMain.LoadSkin(new Skin(defaultFrameBkg, defaultIconTextColor));
            }
        }
    }
}
