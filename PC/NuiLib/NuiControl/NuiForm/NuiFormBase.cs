using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace NuiLib
{
    /// <summary>
    /// 窗体基类
    /// </summary>
    public partial class NuiFormBase : Form
    {
        #region Field

        private bool isClipChildren = true;                                 // 是否应用WS_CLIPCHILDREN样式
        private bool isCanResize = true;                                    // 是否可调整窗体大小

        private bool isCanMove = true;                                      // 是否可移动窗体
        private bool isWindowInDrag = false;                                // 窗体是否在移动状态
        private Point mouseStartLocation = Point.Empty;                     // 窗体开始移动时鼠标的坐标位置

        private ShadowSytle shadowSytle = ShadowSytle.ShadowForm;           // 窗体阴影呈现方式
        private NuiFormShadow shadowForm = null;                            // 阴影窗体

        private Image bkgImage;                                             // 窗体背景图片

        private List<SysButton> sysBtnList;                                 // 窗体系统按钮列表
        private bool isLeftMouseDown = false;                               // 鼠标左键是否按下

        private bool isDisabledCloseButton = false;                         // 是否禁止关闭按钮
        private bool isShowCloseButton = true;                              // 是否显示关闭按钮
        private bool isShowMaxOrRestoreButton = true;                       // 是否显示最大化或还原按钮
        private bool isShowMinButton = true;                                // 是否显示最小化按钮
        private bool isShowMenuButton = false;                              // 是否显示主菜单按钮
        private bool isShowSkinButton = false;                              // 是否显示换肤按钮

        private bool isDrawIcon = true;                                     // 是否绘制图标
        private Font textFont = new Font("微软雅黑", 10.0f, FontStyle.Bold); // 窗体标题文字字体
        private Color textForeColor = Color.FromArgb(250, Color.White);     // 窗体标题文字颜色

        private ToolTip duiToolTip = new ToolTip();                         // 用于显示提示字符串的ToolTip工具
        private string lastToolTipString = string.Empty;                    // 上一个提示字符串

        #endregion

        #region Property

        /// <summary>
        /// 是否应用WS_CLIPCHILDREN样式,该样式可防止因窗体控件太多出现闪烁
        /// </summary>
        [Browsable(true)]
        public bool IsClipChildren
        {
            get { return isClipChildren; }
            set { isClipChildren = value; }
        }

        /// <summary>
        /// 是否显示窗体阴影
        /// </summary>
        [Browsable(true)]
        public ShadowSytle ShadowSytle
        {
            get { return shadowSytle; }
            set { shadowSytle = value; }
        }

        /// <summary>
        /// 是否可移动窗体
        /// </summary>
        [Browsable(true)]
        public bool IsCanMove
        {
            get { return isCanMove; }
            set { isCanMove = value; }
        }

        /// <summary>
        /// 是否可调整窗体大小
        /// </summary>
        [Browsable(true)]
        public bool IsCanResize
        {
            get { return isCanResize; }
            set { isCanResize = value; }
        }

        /// <summary>
        /// 是否禁止关闭按钮
        /// </summary>
        [Browsable(true)]
        public bool IsDisabledCloseButton
        {
            get { return isDisabledCloseButton; }
            set { isDisabledCloseButton = value; this.Invalidate(); }
        }

        /// <summary>
        /// 是否是否显示最大化或还原按钮
        /// </summary>
        [Browsable(true)]
        public bool IsShowCloseButton
        {
            get { return isShowCloseButton; }
            set { isShowCloseButton = value; this.Invalidate(); }
        }

        /// <summary>
        /// 是否显示最大化或还原按钮
        /// </summary>
        [Browsable(true)]
        public bool IsShowMaxOrRestoreButton
        {
            get { return isShowMaxOrRestoreButton; }
            set { isShowMaxOrRestoreButton = value; this.Invalidate(); }
        }

        /// <summary>
        /// 是否显示最小化按钮
        /// </summary>
        [Browsable(true)]
        public bool IsShowMinButton
        {
            get { return isShowMinButton; }
            set { isShowMinButton = value; this.Invalidate(); }
        }

        /// <summary>
        /// 是否显示主菜单按钮
        /// </summary>
        [Browsable(true)]
        public bool IsShowMenuButton
        {
            get { return isShowMenuButton; }
            set { isShowMenuButton = value; this.Invalidate(); }
        }

        /// <summary>
        /// 是否显示换肤按钮
        /// </summary>
        [Browsable(true)]
        public bool IsShowSkinButton
        {
            get { return isShowSkinButton; }
            set { isShowSkinButton = value; this.Invalidate(); }
        }

        /// <summary>
        /// 是否绘制窗体图标
        /// </summary>
        [Browsable(true)]
        public bool IsDrawIcon
        {
            get { return isDrawIcon; }
            set { isDrawIcon = value; this.Invalidate(); }
        }

        /// <summary>
        /// 窗体标题文字字体
        /// </summary>
        [Browsable(true)]
        public Font TextFont
        {
            get { return textFont; }
            set { textFont = value; this.Invalidate(); }
        }

        /// <summary>
        /// 窗体标题文字颜色
        /// </summary>
        [Browsable(true)]
        public Color TextForeColor
        {
            get { return textForeColor; }
            set { textForeColor = value; this.Invalidate(); }
        }

        /// <summary>
        /// 重写背景图片
        /// </summary>
        public override Image BackgroundImage
        {
            get { return bkgImage; }
            set { bkgImage = value; Invalidate(); }
        }

        #endregion

        #region Event

        /// <summary>
        /// 主菜单按钮鼠标左键单击事件
        /// </summary>
        [Browsable(true)]
        public event MouseEventHandler MenuButtonClick;

        /// <summary>
        /// 换肤按钮鼠标左键单击事件
        /// </summary>
        [Browsable(true)]
        public event MouseEventHandler SkinButtonClick;

        #endregion

        #region Constructor

        public NuiFormBase()
        {
            InitializeComponent();

            // 设置双缓冲
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();

            // Event
            this.SizeChanged += new EventHandler(NuiFormBaseSizeChanged);
        }

        #endregion

        #region Override

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (!DesignMode)
                {
                    if (isShowMaxOrRestoreButton) { cp.Style |= Win32.WS_MAXIMIZEBOX; }
                    if (isShowMinButton) { cp.Style |= Win32.WS_MINIMIZEBOX; }
                    if (isClipChildren) { cp.ExStyle |= Win32.WS_CLIPCHILDREN; }
                    if (shadowSytle == ShadowSytle.ApiShadow) { cp.ClassStyle |= Win32.CS_DropSHADOW; }
                }
                return cp;
            }
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (shadowSytle == ShadowSytle.ShadowForm)
            {
                if (Visible)
                {
                    if (!DesignMode)
                    {
                        // 启用窗口淡入淡出
                        Win32.AnimateWindow(this.Handle, 200, Win32.AW_BLEND | Win32.AW_ACTIVATE);

                        if (shadowForm == null)
                        {
                            shadowForm = new NuiFormShadow(this);
                            shadowForm.Show(this);
                        }
                    }
                    base.OnVisibleChanged(e);
                }
                else
                {
                    base.OnVisibleChanged(e);
                }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (shadowSytle == NuiLib.ShadowSytle.ShadowForm)
            {
                if (shadowForm != null)
                {
                    shadowForm.Close();
                }
                //Win32.AnimateWindow(this.Handle, 150, Win32.AW_BLEND | Win32.AW_HIDE);
            }

            base.OnClosing(e);
        }

        protected override void OnCreateControl()
        {
            InitSysButtons();
            this.OnSizeChanged(null);
            base.OnCreateControl();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case Win32.WM_ERASEBKGND:
                    m.Result = IntPtr.Zero;
                    break;
                case Win32.WM_NCHITTEST:
                    WmNcHitTest(ref m);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;

            // Draw background image
            DrawBackgroundImage(g);

            // Draw system buttons
            DrawSysButtons(g);

            // Draw icon and text
            DrawIconAndText(g);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            foreach (var sysBtn in sysBtnList)
            {
                if (sysBtn.Visible && sysBtn.State != SysButtonState.Disabled)
                {
                    sysBtn.State = SysButtonState.Normal;
                }
            }
            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            bool isHideToolTip = true;
            string currentTooltipString = string.Empty;

            foreach (var sysBtn in sysBtnList)
            {
                if (sysBtn.Visible)
                {
                    if (sysBtn.LocationRect.Contains(e.Location))
                    {
                        if (sysBtn.State != SysButtonState.Disabled)
                        {
                            sysBtn.State = isLeftMouseDown ? SysButtonState.Down : SysButtonState.Hover;
                        }

                        isHideToolTip = false;
                        currentTooltipString = sysBtn.ToolTip;
                    }
                    else
                    {
                        if (sysBtn.State != SysButtonState.Disabled)
                        {
                            sysBtn.State = SysButtonState.Normal;
                        }
                    }
                }
            }

            // 处理tooltip
            if (isHideToolTip)
            {
                HideToolTip();
                lastToolTipString = string.Empty;
            }
            else
            {
                if (!string.IsNullOrEmpty(currentTooltipString) && !currentTooltipString.Equals(lastToolTipString))
                {
                    ShowTooTip(currentTooltipString);
                    lastToolTipString = currentTooltipString;
                }
            }

            // 窗体移动
            if (isCanMove && isWindowInDrag)
            {
                Point mouseScreenLocation = PointToScreen(e.Location);
                this.Location = new Point(mouseScreenLocation.X - mouseStartLocation.X, mouseScreenLocation.Y - mouseStartLocation.Y);
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                bool isInSysBtnRects = false;
                isLeftMouseDown = true;
                foreach (var sysBtn in sysBtnList)
                {
                    if (sysBtn.Visible)
                    {
                        if (sysBtn.LocationRect.Contains(e.Location))
                        {
                            if (sysBtn.State != SysButtonState.Disabled)
                            {
                                sysBtn.State = SysButtonState.Down;
                            }
                            isInSysBtnRects = true;
                        }
                    }
                }

                if (isCanMove && !isInSysBtnRects)
                {
                    isWindowInDrag = true;
                    mouseStartLocation = e.Location;
                }
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isLeftMouseDown = false;
                foreach (var sysBtn in sysBtnList)
                {
                    if (sysBtn.Visible)
                    {
                        if (sysBtn.LocationRect.Contains(e.Location))
                        {
                            if (sysBtn.State != SysButtonState.Disabled)
                            {
                                sysBtn.OnMouseDown(this, e);
                            }
                        }
                    }
                }

                if (isCanMove)
                {
                    isWindowInDrag = false;
                }
            }
            base.OnMouseUp(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            this.Invalidate();
            base.OnTextChanged(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (textFont != null)
                {
                    textFont.Dispose();
                    textFont = null;
                }
            }
            base.Dispose(disposing);
        }

        #endregion

        #region Event Handler

        /// <summary>
        /// 窗体大小改变事件
        /// </summary>
        public void NuiFormBaseSizeChanged(object sender, EventArgs e)
        {
            /*******************************************************
            *
            *  Override OnSizeChanged 将会导致在窗体大小改变时 ShadowForm不绘制窗体阴影。
            * 
            ********************************************************/

            this.MaximumSize = Screen.PrimaryScreen.WorkingArea.Size;

            CalculateSysButtonUI();
            this.Invalidate();
        }

        #endregion

        #region Private

        /// <summary>
        /// 初始化窗体系统按钮列表
        /// </summary>
        private void InitSysButtons()
        {
            sysBtnList = new List<SysButton>();

            if (isShowCloseButton)
            {
                SysButton closeBtn = new SysButton();
                closeBtn.Parent = this;
                closeBtn.Name = SysButtonName.Close;
                closeBtn.State = isDisabledCloseButton ? SysButtonState.Disabled : SysButtonState.Normal;
                closeBtn.Visible = isShowCloseButton;
                closeBtn.ResourceImage = Properties.Resources.sysbtn_close;
                closeBtn.ToolTip = "关闭";

                closeBtn.Size = new Size(27, 22);
                closeBtn.MouseDown += delegate(object sender, MouseEventArgs e)
                {
                    if (e.Button == MouseButtons.Left) { this.Close(); }
                };
                sysBtnList.Add(closeBtn);
            }

            if (isShowMaxOrRestoreButton)
            {
                SysButton maxOrRestoreBtn = new SysButton();
                maxOrRestoreBtn.Parent = this;
                maxOrRestoreBtn.Name = SysButtonName.MaxOrRestore;
                maxOrRestoreBtn.State = SysButtonState.Normal;
                maxOrRestoreBtn.Visible = isShowMaxOrRestoreButton;

                maxOrRestoreBtn.ResourceImage = Properties.Resources.sysbtn_max;
                maxOrRestoreBtn.ToolTip = "最大化";

                maxOrRestoreBtn.Size = new Size(27, 22);
                maxOrRestoreBtn.MouseDown += delegate(object sender, MouseEventArgs e)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        if (WindowState == FormWindowState.Normal)
                        {
                            WindowState = FormWindowState.Maximized;
                            maxOrRestoreBtn.ResourceImage = Properties.Resources.sysbtn_restore;
                            maxOrRestoreBtn.ToolTip = "还原";
                        }
                        else if (WindowState == FormWindowState.Maximized)
                        {
                            WindowState = FormWindowState.Normal;
                            maxOrRestoreBtn.ResourceImage = Properties.Resources.sysbtn_max;
                            maxOrRestoreBtn.ToolTip = "最大化";
                        }
                    }
                };
                sysBtnList.Add(maxOrRestoreBtn);
            }

            if (isShowMinButton)
            {
                SysButton minBtn = new SysButton();
                minBtn.Parent = this;
                minBtn.Name = SysButtonName.Min;
                minBtn.State = SysButtonState.Normal;
                minBtn.Visible = isShowMinButton;

                minBtn.ResourceImage = Properties.Resources.sysbtn_min;
                minBtn.ToolTip = "最小化";

                minBtn.Size = new Size(27, 22);
                minBtn.MouseDown += delegate(object sender, MouseEventArgs e)
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        WindowState = FormWindowState.Minimized;
                    }
                };
                sysBtnList.Add(minBtn);
            }

            if (isShowMenuButton)
            {
                SysButton memuBtn = new SysButton();
                memuBtn.Parent = this;
                memuBtn.Name = SysButtonName.Menu;
                memuBtn.State = SysButtonState.Normal;
                memuBtn.Visible = isShowMenuButton;

                memuBtn.ResourceImage = Properties.Resources.sysbtn_menu;
                memuBtn.ToolTip = "主菜单";

                memuBtn.Size = new Size(27, 22);
                memuBtn.MouseDown += OnMenuButtonClick;

                sysBtnList.Add(memuBtn);
            }

            if (isShowSkinButton)
            {
                SysButton skinBtn = new SysButton();
                skinBtn.Parent = this;
                skinBtn.Name = SysButtonName.Skin;
                skinBtn.State = SysButtonState.Normal;
                skinBtn.Visible = isShowSkinButton;

                skinBtn.ResourceImage = Properties.Resources.sysbtn_skin;
                skinBtn.ToolTip = "换肤";

                skinBtn.Size = new Size(27, 22);
                skinBtn.MouseDown += OnSkinButtonClick;
                sysBtnList.Add(skinBtn);
            }
        }

        /// <summary>
        /// 计算窗体系统按钮的坐标
        /// </summary>
        private void CalculateSysButtonUI()
        {
            const int TOP_OFFSET = 0;
            const int RIGHT_OFFSET = 5;

            if (sysBtnList != null && sysBtnList.Count > 0)
            {
                int buttonWidth = sysBtnList[0].Size.Width;
                for (int i = 0; i < sysBtnList.Count; i++)
                {
                    // 计算坐标
                    sysBtnList[i].Location = new Point(this.Width - RIGHT_OFFSET - (i + 1) * buttonWidth, TOP_OFFSET);

                    // 修改图片
                    if (sysBtnList[i].Name == SysButtonName.MaxOrRestore)
                    {
                        if (this.WindowState == FormWindowState.Maximized)
                        {
                            sysBtnList[i].ResourceImage = Properties.Resources.sysbtn_restore;
                            sysBtnList[i].ToolTip = "还原";
                        }
                        else if (this.WindowState == FormWindowState.Normal)
                        {
                            sysBtnList[i].ResourceImage = Properties.Resources.sysbtn_max;
                            sysBtnList[i].ToolTip = "最大化";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 绘制窗体背景图片
        /// </summary>
        private void DrawBackgroundImage(Graphics g)
        {
            if (bkgImage != null)
            {
                if (this.Width > bkgImage.Width || this.Height > bkgImage.Height)
                {
                    g.DrawImage(bkgImage, ClientRectangle, new Rectangle(0, 0, bkgImage.Width, bkgImage.Height), GraphicsUnit.Pixel);
                }
                else
                {
                    g.DrawImage(bkgImage, ClientRectangle, ClientRectangle, GraphicsUnit.Pixel);
                }
            }
        }

        /// <summary>
        /// 绘制窗体系统按钮
        /// </summary>
        private void DrawSysButtons(Graphics g)
        {
            foreach (var sysBtn in sysBtnList)
            {
                if (sysBtn.Visible)
                {
                    switch (sysBtn.State)
                    {
                        case SysButtonState.Normal:
                            g.DrawImage(
                                sysBtn.ResourceImage,
                                sysBtn.LocationRect,
                                new Rectangle(0, 0, sysBtn.Size.Width, sysBtn.Size.Height),
                                GraphicsUnit.Pixel);
                            break;
                        case SysButtonState.Hover:
                            g.DrawImage(
                                sysBtn.ResourceImage,
                                sysBtn.LocationRect,
                                new Rectangle(1 * sysBtn.Size.Width, 0, sysBtn.Size.Width, sysBtn.Size.Height),
                                GraphicsUnit.Pixel);
                            break;
                        case SysButtonState.Down:
                            g.DrawImage(
                                sysBtn.ResourceImage,
                                sysBtn.LocationRect,
                                new Rectangle(2 * sysBtn.Size.Width, 0, sysBtn.Size.Width, sysBtn.Size.Height),
                                GraphicsUnit.Pixel);
                            break;
                        case SysButtonState.DownLeave:
                            g.DrawImage(
                               sysBtn.ResourceImage,
                               sysBtn.LocationRect,
                               new Rectangle(2 * sysBtn.Size.Width, 0, sysBtn.Size.Width, sysBtn.Size.Height),
                               GraphicsUnit.Pixel);
                            break;
                        case SysButtonState.Disabled:
                            g.DrawImage(
                                sysBtn.ResourceImage,
                                sysBtn.LocationRect,
                                new Rectangle(3 * sysBtn.Size.Width, 0, sysBtn.Size.Width, sysBtn.Size.Height),
                                GraphicsUnit.Pixel);
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 绘制窗体图标和文本
        /// </summary>
        private void DrawIconAndText(Graphics g)
        {
            Rectangle iconRect = Rectangle.Empty;
            Rectangle textRect = Rectangle.Empty;

            if (isDrawIcon && this.Icon != null)
            {
                iconRect = new Rectangle(new Point(8, 6), SystemInformation.SmallIconSize);

                if (this.Text.Length > 0)
                {
                    textRect = new Rectangle(iconRect.Right + 2, iconRect.Top - (textFont.Height - iconRect.Height) / 2, Width - (8 + iconRect.Width + 2 + 27 * 2), textFont.Height);
                }
            }
            else
            {
                if (this.Text.Length > 0)
                {
                    textRect = new Rectangle(8, 4, Width - 8, textFont.Height);
                }
            }

            if (isDrawIcon && this.Icon != null)
            {
                g.DrawIcon(this.Icon, iconRect);
            }

            if (this.Text.Length > 0)
            {
                TextRenderer.DrawText(
                g,
                this.Text,
                textFont,
                textRect,
                textForeColor,
                TextFormatFlags.SingleLine | TextFormatFlags.EndEllipsis);
            }
        }

        /// <summary>
        /// 处理鼠标调整窗体大小消息
        /// </summary>
        private void WmNcHitTest(ref Message m)
        {
            int wparam = m.LParam.ToInt32();
            Point mouseLocation = new Point(MethodHelper.LOWORD(wparam), MethodHelper.HIWORD(wparam));
            mouseLocation = PointToClient(mouseLocation);

            if (WindowState != FormWindowState.Maximized)
            {
                if (isCanResize == true)
                {
                    if (mouseLocation.X < 5 && mouseLocation.Y < 5)
                    {
                        m.Result = new IntPtr(Win32.HTTOPLEFT);
                        return;
                    }

                    if (mouseLocation.X > Width - 5 && mouseLocation.Y < 5)
                    {
                        m.Result = new IntPtr(Win32.HTTOPRIGHT);
                        return;
                    }

                    if (mouseLocation.X < 5 && mouseLocation.Y > Height - 5)
                    {
                        m.Result = new IntPtr(Win32.HTBOTTOMLEFT);
                        return;
                    }

                    if (mouseLocation.X > Width - 5 && mouseLocation.Y > Height - 5)
                    {
                        m.Result = new IntPtr(Win32.HTBOTTOMRIGHT);
                        return;
                    }

                    if (mouseLocation.Y < 3)
                    {
                        m.Result = new IntPtr(Win32.HTTOP);
                        return;
                    }

                    if (mouseLocation.Y > Height - 3)
                    {
                        m.Result = new IntPtr(Win32.HTBOTTOM);
                        return;
                    }

                    if (mouseLocation.X < 3)
                    {
                        m.Result = new IntPtr(Win32.HTLEFT);
                        return;
                    }

                    if (mouseLocation.X > Width - 3)
                    {
                        m.Result = new IntPtr(Win32.HTRIGHT);
                        return;
                    }
                }
            }
            m.Result = new IntPtr(Win32.HTCLIENT);
        }

        /// <summary>
        /// 显示ToolTip
        /// </summary>
        private void ShowTooTip(string toolTipText)
        {
            duiToolTip.SetToolTip(this, toolTipText);
            duiToolTip.Active = true;
        }

        /// <summary>
        /// 隐藏ToolTip
        /// </summary>
        private void HideToolTip()
        {
            duiToolTip.Active = false;
        }

        #endregion

        #region Public

        /// <summary>
        /// 主菜单按钮鼠标左键单击事件
        /// </summary>
        public void OnMenuButtonClick(object sender, MouseEventArgs e)
        {
            if (MenuButtonClick != null)
            {
                MenuButtonClick(sender, e);
            }
        }

        /// <summary>
        /// 换肤按钮鼠标左键单击事件
        /// </summary>
        public void OnSkinButtonClick(object sender, MouseEventArgs e)
        {
            if (SkinButtonClick != null)
            {
                SkinButtonClick(sender, e);
            }
        }

        #endregion
    }
}
