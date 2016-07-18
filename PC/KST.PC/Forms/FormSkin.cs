using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NuiLib;
using KST.ControlEx;
using System.IO;
using System.Threading;

namespace KST
{
    /// <summary>
    /// 皮肤窗体
    /// </summary>
    public partial class FormSkin : NuiFormBase
    {
        private const int SKIN_ITEM_TOP = 15;
        private const int SKIN_ITEM_LEFT = 20;
        private const int SKIN_ITEM_X_OFFSET = 20;
        private const int SKIN_ITEM_Y_OFFSET = 10;
        private const int SKIN_ITEM_WIDTH = 200;
        private const int SKIN_ITEM_HEIGHT = 118;
        private const int SKIN_ITEM_COUNT_PER_LINE = 3;

        private int addedSkinItemCount = 0;
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(FormSkin));

        public FormSkin()
        {
            InitializeComponent();
            this.Load += new EventHandler(FormSkinLoad);
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void FormSkinLoad(object sender, EventArgs e)
        {
            CaculateUI();

            // 设置背景图片
            FormMain mainForm = this.Owner as FormMain;
            if (mainForm != null)
            {
                this.BackgroundImage = mainForm.BackgroundImage;
            }

            // 开启线程加载皮肤
            Thread initSkinThread = new Thread(() => { InitSkins(); });
            initSkinThread.Start();
        }

        /// <summary>
        /// 皮肤按钮单击事件
        /// </summary>
        private void SkinItemClick(object sender, EventArgs e)
        {
            SkinItemButton skinItemButton = sender as SkinItemButton;
            if (skinItemButton != null)
            {
                // 设置本窗体背景图片
                if (skinItemButton.SkinFrameBkg != null)
                {
                    this.BackgroundImage = skinItemButton.SkinFrameBkg;
                }

                // 设置主窗体皮肤
                SkinUtil.LoadSkin(skinItemButton.SkinID);
            }
        }

        /// <summary>
        /// 计算各个控件的位置坐标
        /// </summary>
        private void CaculateUI()
        {
            const int CONTAINER_TOP = 30;

            this.panelWaiting.Left = 0;
            this.panelWaiting.Top = CONTAINER_TOP;
            this.panelWaiting.Width = this.Width;
            this.panelWaiting.Height = this.Height - CONTAINER_TOP;

            this.panelWaitImageAndText.Left = (this.panelWaiting.Width - this.panelWaitImageAndText.Width) / 2;
            this.panelWaitImageAndText.Top = (this.panelWaiting.Height - this.panelWaitImageAndText.Height) / 2;

            this.panelContainer.Location = this.panelWaiting.Location;
            this.panelContainer.Size = this.panelWaiting.Size;

            this.lblSkinInitTip.Left = (this.panelContainer.Width - this.lblSkinInitTip.Width) / 2;
            this.lblSkinInitTip.Top = (this.panelContainer.Height - this.lblSkinInitTip.Height) / 2;
        }

        /// <summary>
        /// 初始化所有皮肤
        /// </summary>
        private void InitSkins()
        {
            try
            {
                string themesPath = AppDomain.CurrentDomain.BaseDirectory + Constant.THEMES_PATH;
                if (Directory.Exists(themesPath))
                {
                    string[] skinsFolders = Directory.GetDirectories(themesPath);
                    if (skinsFolders != null && skinsFolders.Length > 0)
                    {
                        foreach (var skinFolder in skinsFolders)
                        {
                            string skinXmlFile = skinFolder + @"\" + Constant.SKIN_XML;
                            if (File.Exists(skinXmlFile))
                            {
                                SkinItemButton skinItem = new SkinItemButton();

                                skinItem.Width = SKIN_ITEM_WIDTH;
                                skinItem.Height = SKIN_ITEM_HEIGHT;
                                skinItem.Left = SKIN_ITEM_LEFT + (skinItem.Width + SKIN_ITEM_X_OFFSET) * (addedSkinItemCount % SKIN_ITEM_COUNT_PER_LINE);
                                skinItem.Top = SKIN_ITEM_TOP + (skinItem.Height + SKIN_ITEM_Y_OFFSET) * (addedSkinItemCount / SKIN_ITEM_COUNT_PER_LINE);

                                skinItem.SkinID = XmlUtil.ReadValue(skinXmlFile, Constant.SKIN_XPATH_SKIN_ID);
                                skinItem.SkinName = XmlUtil.ReadValue(skinXmlFile, Constant.SKIN_XPATH_SKIN_NAME);
                                skinItem.Image = Image.FromFile(skinFolder + @"\" + XmlUtil.ReadValue(skinXmlFile, Constant.SKIN_XPATH_SKIN_VIEW));
                                skinItem.SkinFrameBkg = Image.FromFile(skinFolder + @"\" + XmlUtil.ReadValue(skinXmlFile, Constant.SKIN_XPATH_FRAME_BKG));
                                skinItem.Checked = skinItem.SkinID.Equals(Config.SkinID);
                                skinItem.Click += new EventHandler(SkinItemClick);

                                this.panelContainer.Controls.Add(skinItem);

                                addedSkinItemCount++;
                            }
                        }

                        UIInvokeUtil.InvokeVisible(this.lblSkinInitTip, false);
                        UIInvokeUtil.InvokeVisible(this.panelContainer, true);
                        UIInvokeUtil.InvokeVisible(this.panelWaiting, false);
                    }
                    else
                    {
                        UIInvokeUtil.InvokeForeColor(this.lblSkinInitTip, SystemColors.ControlText);
                        UIInvokeUtil.InvokeText(this.lblSkinInitTip, "暂无可选皮肤");
                        UIInvokeUtil.InvokeVisible(this.lblSkinInitTip, true);
                        UIInvokeUtil.InvokeVisible(this.panelContainer, true);
                        UIInvokeUtil.InvokeVisible(this.panelWaiting, false);
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);

                // 清除已添加的皮肤
                List<Control> needDeleteControls = new List<Control>();
                foreach (var control in this.panelContainer.Controls)
                {
                    SkinItemButton skinItem = control as SkinItemButton;
                    if (skinItem != null)
                    {
                        needDeleteControls.Add(skinItem);
                    }
                }
                foreach (var needDeleteControl in needDeleteControls)
                {
                    this.panelContainer.Controls.Remove(needDeleteControl);
                }

                UIInvokeUtil.InvokeForeColor(this.lblSkinInitTip, Color.Red);
                UIInvokeUtil.InvokeText(this.lblSkinInitTip, "初始化皮肤失败");
                UIInvokeUtil.InvokeVisible(this.lblSkinInitTip, true);
                UIInvokeUtil.InvokeVisible(this.panelContainer, true);
                UIInvokeUtil.InvokeVisible(this.panelWaiting, false);
            }
        }
    }
}
