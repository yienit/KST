using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using NuiLib;
using KST.ControlEx;

namespace KST
{
    public partial class FormMain : NuiFormBase
    {
        #region Constructor

        public FormMain()
        {
            InitializeComponent();

            InitHeaderPanel();
            InitWorkPanel();
            InitMainMenuRender();
            SetCurrentWorkPanel(WorkPanel.MyHome);

            RegisterEvent();
        }

        #endregion

        #region Override

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Win32.WM_SHOW_FRAME)
            {
                ShowMe();
            }
            base.WndProc(ref m);
        }

        #endregion

        #region Privare

        /// <summary>
        /// 初始化头部工具栏Panel(先设置再替换)
        /// </summary>
        private void InitHeaderPanel()
        {
            Font headerBtnFont = new Font("微软雅黑", 9.0f, FontStyle.Bold);

            NuiHeaderButton dntjBtn = new NuiHeaderButton();
            dntjBtn.ContainerPanel = this.headerPanel;
            dntjBtn.Image = Properties.Resources.ico_my_home;
            dntjBtn.Text = "个人首页";
            dntjBtn.Font = headerBtnFont;
            dntjBtn.State = NuiHeaderButtonState.Focus;
            dntjBtn.MouseClick += delegate(object sender, NuiHeaderButtonEventArgs e)
            {
                if (e.MouseButton == MouseButtons.Left)
                {
                    SetCurrentWorkPanel(WorkPanel.MyHome);
                }
            };
            this.headerPanel.AddHeaderButton(dntjBtn);

            NuiHeaderButton mmcsBtn = new NuiHeaderButton();
            mmcsBtn.ContainerPanel = this.headerPanel;
            mmcsBtn.Image = Properties.Resources.ico_practice;
            mmcsBtn.Text = "试题练习";
            mmcsBtn.Font = headerBtnFont;
            mmcsBtn.MouseClick += delegate(object sender, NuiHeaderButtonEventArgs e)
            {
                if (e.MouseButton == MouseButtons.Left)
                {
                    SetCurrentWorkPanel(WorkPanel.Practice);
                }
            };
            this.headerPanel.AddHeaderButton(mmcsBtn);

            NuiHeaderButton xtxfBtn = new NuiHeaderButton();
            xtxfBtn.ContainerPanel = this.headerPanel;
            xtxfBtn.Image = Properties.Resources.ico_exam_simulation;
            xtxfBtn.Text = "模拟考试";
            xtxfBtn.Font = headerBtnFont;
            xtxfBtn.MouseClick += delegate(object sender, NuiHeaderButtonEventArgs e)
            {
                if (e.MouseButton == MouseButtons.Left)
                {
                    SetCurrentWorkPanel(WorkPanel.ExamSimulation);
                }
            };
            this.headerPanel.AddHeaderButton(xtxfBtn);

            NuiHeaderButton dnqlBtn = new NuiHeaderButton();
            dnqlBtn.ContainerPanel = this.headerPanel;
            dnqlBtn.Image = Properties.Resources.ico_vip_papers;
            dnqlBtn.Text = "VIP题库";
            dnqlBtn.Font = headerBtnFont;
            dnqlBtn.MouseClick += delegate(object sender, NuiHeaderButtonEventArgs e)
            {
                if (e.MouseButton == MouseButtons.Left)
                {
                    SetCurrentWorkPanel(WorkPanel.VipPapers);
                }
            };
            this.headerPanel.AddHeaderButton(dnqlBtn);

            NuiHeaderButton yhjsBtn = new NuiHeaderButton();
            yhjsBtn.ContainerPanel = this.headerPanel;
            yhjsBtn.Image = Properties.Resources.ico_previous_papers;
            yhjsBtn.Text = "历年真题";
            yhjsBtn.Font = headerBtnFont;
            yhjsBtn.MouseClick += delegate(object sender, NuiHeaderButtonEventArgs e)
            {
                if (e.MouseButton == MouseButtons.Left)
                {
                    SetCurrentWorkPanel(WorkPanel.PreviousPapers);
                }
            };
            this.headerPanel.AddHeaderButton(yhjsBtn);

            NuiHeaderButton dnzjBtn = new NuiHeaderButton();
            dnzjBtn.ContainerPanel = this.headerPanel;
            dnzjBtn.Image = Properties.Resources.ico_my_wrong;
            dnzjBtn.Text = "我的错题";
            dnzjBtn.Font = headerBtnFont;
            dnzjBtn.MouseClick += delegate(object sender, NuiHeaderButtonEventArgs e)
            {
                if (e.MouseButton == MouseButtons.Left)
                {
                    SetCurrentWorkPanel(WorkPanel.MyWrong);
                }
            };
            this.headerPanel.AddHeaderButton(dnzjBtn);

            NuiHeaderButton sjzsBtn = new NuiHeaderButton();
            sjzsBtn.ContainerPanel = this.headerPanel;
            sjzsBtn.Image = Properties.Resources.ico_my_favorite;
            sjzsBtn.Text = "我的收藏";
            sjzsBtn.Font = headerBtnFont;
            sjzsBtn.MouseClick += delegate(object sender, NuiHeaderButtonEventArgs e)
            {
                if (e.MouseButton == MouseButtons.Left)
                {
                    SetCurrentWorkPanel(WorkPanel.MyFavorite);
                }
            };
            this.headerPanel.AddHeaderButton(sjzsBtn);

            NuiHeaderButton rjgjBtn = new NuiHeaderButton();
            rjgjBtn.ContainerPanel = this.headerPanel;
            rjgjBtn.Image = Properties.Resources.ico_more_tools;
            rjgjBtn.Text = "更多功能";
            rjgjBtn.Font = headerBtnFont;
            rjgjBtn.MouseClick += delegate(object sender, NuiHeaderButtonEventArgs e)
            {
                if (e.MouseButton == MouseButtons.Left)
                {
                    SetCurrentWorkPanel(WorkPanel.MoreTools);
                }
            };
            this.headerPanel.AddHeaderButton(rjgjBtn);
        }

        /// <summary>
        /// 初始化工作区域Panel
        /// </summary>
        private void InitWorkPanel()
        {
            if (this.headerPanel != null)
            {
                int left = 0;
                int top = this.headerPanel.Bottom;
                Size size = this.Size;

                foreach (Control control in this.Controls)
                {
                    if (control is WorkPanelBase)
                    {
                        control.Left = left;
                        control.Top = top;
                        control.Size = size;
                    }
                }
            }
        }

        /// <summary>
        /// 初始化主菜单Render
        /// </summary>
        private void InitMainMenuRender()
        {
            this.mainMenu.Renderer = new NuiContextMenuStripRender();
        }

        /// <summary>
        /// 设置当前显示的工作面板Panel
        /// </summary>
        private void SetCurrentWorkPanel(WorkPanel workPanel)
        {
            // 设置所有的WorkPanel不可见
            foreach (Control control in this.Controls)
            {
                if (control is WorkPanelBase)
                {
                    control.Visible = false;
                }
            }

            // 设置需要显示的WorkPanel
            switch (workPanel)
            {
                case WorkPanel.MyHome: this.myHomeWorkPanel.Visible = true; break;
                case WorkPanel.Practice: this.practiceWorkPanel.Visible = true; break;
                case WorkPanel.ExamSimulation: this.examSimulationWorkPanel.Visible = true; break;
                case WorkPanel.VipPapers: this.vipPapersWorkPanel.Visible = true; break;
                case WorkPanel.PreviousPapers: this.previousPapersWorkPanel.Visible = true; break;
                case WorkPanel.MyWrong: this.myWrongWorkPanel.Visible = true; break;
                case WorkPanel.MyFavorite: this.myFavoriteWorkPanel.Visible = true; break;
                case WorkPanel.MoreTools: this.moreToolsWorkPanel.Visible = true; break;
                default: break;
            }
        }

        /// <summary>
        /// 窗体重复运行时显示窗体
        /// </summary>
        private void ShowMe()
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }

            // get our current "TopMost" value (ours will always be false though)
            bool top = TopMost;

            // make our form jump to the top of everything
            TopMost = true;

            // set it back to whatever it was
            TopMost = top;
        }

        #endregion

        #region EventHandler

        /// <summary>
        /// 注册事件函数
        /// </summary>
        private void RegisterEvent()
        {
            this.SizeChanged += new EventHandler(FormSizeChanged);
            this.Load += new EventHandler(FormMainLoad);
            this.MenuButtonClick += new MouseEventHandler(FormMenuButtonClick);
            this.SkinButtonClick += new MouseEventHandler(FormSkinButtonClick);

            this.mainMenuAboutUs.Click += new EventHandler(MainMenuAboutClick);
        }

        /// <summary>
        /// 窗体加载事件
        /// </summary>
        private void FormMainLoad(object sender, EventArgs e)
        {
            SkinUtil.LoadSkin(Config.SkinID);
        }

        /// <summary>
        /// 窗体大小改变事件
        /// </summary>
        private void FormSizeChanged(object sender, EventArgs e)
        {
            InitWorkPanel();
        }

        /// <summary>
        /// 窗体主菜单按钮单击事件
        /// </summary>
        private void FormMenuButtonClick(object sender, MouseEventArgs e)
        {
            Point showLocation = PointToScreen(new Point(e.Location.X - 10, 22));
            this.mainMenu.Show(showLocation);
        }

        /// <summary>
        /// 窗体换肤按钮单击事件
        /// </summary>
        private void FormSkinButtonClick(object sender, MouseEventArgs e)
        {
            new FormSkin().ShowDialog(this);
        }

        /// <summary>
        /// 窗体主菜单-关于选项单击事件
        /// </summary>
        private void MainMenuAboutClick(object sender, EventArgs e)
        {
            new FormAbout().ShowDialog(this);
        }

        #endregion

        #region Public

        /// <summary>
        /// 加载皮肤
        /// </summary>
        public void LoadSkin(Skin skin)
        {
            if (skin != null)
            {
                // 设置窗体背景图片
                if (skin.FrameBkg != null)
                {
                    this.BackgroundImage = skin.FrameBkg;
                }

                // 设置窗体头部按钮文字颜色
                foreach (var headerBtn in this.headerPanel.HeaderButtons)
                {
                    headerBtn.ForeColor = skin.IconTextColor;
                }
            }

            // 保存当前皮肤
            Config.SkinID = skin.SkinID;
        }

        #endregion
    }
}
