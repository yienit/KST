using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

namespace NuiLib
{
    /// <summary>
    /// 阴影窗体,跟随宿主窗体移动而移动
    /// </summary>
    partial class NuiFormShadow : Form
    {
        private NuiFormBase formBase;   // 宿主窗体

        #region Constructor

        public NuiFormShadow(NuiFormBase main)
        {
            //将控制层传值过来
            this.formBase = main;
            InitializeComponent();

            //置顶窗体
            TopMost = formBase.TopMost;
            formBase.BringToFront();

            //是否在任务栏显示
            ShowInTaskbar = false;

            //无边框模式
            FormBorderStyle = FormBorderStyle.None;

            //设置绘图层显示位置
            this.Location = new Point(formBase.Location.X - 5, formBase.Location.Y - 5);

            //设置ICO
            Icon = formBase.Icon;
            ShowIcon = formBase.ShowIcon;

            //设置大小
            Width = formBase.Width + 10;
            Height = formBase.Height + 10;

            //设置标题名
            Text = formBase.Text;

            //绘图层窗体移动
            formBase.LocationChanged += new EventHandler(BaseFormLocationChanged);
            formBase.SizeChanged += new EventHandler(BaseFormSizeChanged);
            formBase.VisibleChanged += new EventHandler(BaseFormVisibleChanged);

            DrawShadow();
        }

        #endregion

        #region Override

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cParms = base.CreateParams;
                cParms.ExStyle |= Win32.WS_EX_LAYERED;
                return cParms;
            }
        }

        #endregion

        #region Private

        /// <summary>
        /// 宿主窗体位置改变事件
        /// </summary>
        private void BaseFormLocationChanged(object sender, EventArgs e)
        {
            Location = new Point(formBase.Left - 5, formBase.Top - 5);
        }

        /// <summary>
        /// 宿主窗体大小改变事件
        /// </summary>
        private void BaseFormSizeChanged(object sender, EventArgs e)
        {
            // 设置大小
            Width = formBase.Width + 10;
            Height = formBase.Height + 10;
            DrawShadow();
        }

        /// <summary>
        /// 宿主窗体现实或隐藏事件
        /// </summary>
        private void BaseFormVisibleChanged(object sender, EventArgs e)
        {
            this.Visible = formBase.Visible;
        }

        /// <summary>
        /// 绘制边框阴影图片
        /// </summary>
        public void DrawShadow()
        {
            // 绘制绘图层背景
            Bitmap bitmap = new Bitmap(formBase.Width + 10, formBase.Height + 10);
            Rectangle backlightLTRB = new Rectangle(20, 20, 20, 20); // 窗体光泽重绘边界

            Graphics g = Graphics.FromImage(bitmap);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            Render.DrawNineRect(
                g,
                Properties.Resources.window_shadow,
                new Rectangle(0, 2, ClientSize.Width, ClientSize.Height),
                Rectangle.FromLTRB(backlightLTRB.X, backlightLTRB.Y, backlightLTRB.Width, backlightLTRB.Height),
                1,
                1
            );

            IntPtr oldBits = IntPtr.Zero;
            IntPtr screenDC = Win32.GetDC(IntPtr.Zero);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr memDc = Win32.CreateCompatibleDC(screenDC);

            try
            {
                Win32.Point topLoc = new Win32.Point(Left, Top);
                Win32.Size bitMapSize = new Win32.Size(Width, Height);
                Win32.BLENDFUNCTION blendFunc = new Win32.BLENDFUNCTION();
                Win32.Point srcLoc = new Win32.Point(0, 0);

                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                oldBits = Win32.SelectObject(memDc, hBitmap);

                blendFunc.BlendOp = Win32.AC_SRC_OVER;
                blendFunc.SourceConstantAlpha = Byte.MaxValue;
                blendFunc.AlphaFormat = Win32.AC_SRC_ALPHA;
                blendFunc.BlendFlags = 0;

                Win32.UpdateLayeredWindow(
                    this.Handle,
                    screenDC,
                    ref topLoc,
                    ref bitMapSize,
                    memDc,
                    ref srcLoc,
                    0,
                    ref blendFunc,
                    Win32.ULW_ALPHA
                );
            }
            finally
            {
                if (hBitmap != IntPtr.Zero)
                {
                    Win32.SelectObject(memDc, oldBits);
                    Win32.DeleteObject(hBitmap);
                }
                Win32.ReleaseDC(IntPtr.Zero, screenDC);
                Win32.DeleteDC(memDc);
            }
        }

        #endregion
    }
}
