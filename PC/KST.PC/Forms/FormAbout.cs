using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NuiLib;

namespace KST
{
    /// <summary>
    /// 关于窗体
    /// </summary>
    public partial class FormAbout : NuiFormBase
    {
        public FormAbout()
        {
            InitializeComponent();
            this.Load += new EventHandler(FormAboutLoad);
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void FormAboutLoad(object sender, EventArgs e)
        {
            CaculateUI();

            // 设置背景图片
            FormMain mainForm = this.Owner as FormMain;
            if (mainForm != null)
            {
                this.BackgroundImage = mainForm.BackgroundImage;
            }

            // 设置版本号
            this.lblVersion.Text = Application.ProductVersion;
        }

        /// <summary>
        /// 计算各个控件的位置坐标
        /// </summary>
        private void CaculateUI()
        {
            const int CONTAINER_TOP = 30;

            this.panelContainer.Left = 0;
            this.panelContainer.Top = CONTAINER_TOP;
            this.panelContainer.Width = this.Width;
            this.panelContainer.Height = this.Height - CONTAINER_TOP;
            this.panelContainer.BackColor = Color.FromArgb(246, 246, 246);
        }
    }
}
