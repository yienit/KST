using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using NuiLib;

namespace KST
{
    /// <summary>
    /// 窗体主题皮肤类(头部按钮文本颜色及主窗体背景图片)
    /// </summary>
    public class Skin
    {
        /// <summary>
        /// 皮肤ID
        /// </summary>
        public string SkinID { get; set; }

        /// <summary>
        /// 皮肤名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 主窗体背景图片文件名称
        /// </summary>
        public string FrameBkgName { get; set; }

        /// <summary>
        /// 主窗体背景图片
        /// </summary>
        public Image FrameBkg { get; set; }

        /// <summary>
        /// 皮肤预览图片文件名称
        /// </summary>
        public string SkinViewName { get; set; }

        /// <summary>
        /// 皮肤预览图片
        /// </summary>
        public Image SkinView { get; set; }

        /// <summary>
        /// 头部按钮文本颜色
        /// </summary>
        public Color IconTextColor { get; set; }

        public Skin()
        {

        }

        public Skin(Image formBkg, Color iconTextColor)
        {
            this.FrameBkg = formBkg;
            this.IconTextColor = iconTextColor;
        }
    }
}
