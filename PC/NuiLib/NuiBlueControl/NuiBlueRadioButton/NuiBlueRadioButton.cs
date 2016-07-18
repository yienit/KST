using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace NuiLib.NuiBlueControl
{
    /// <summary>
    /// 腾讯QQ蓝色单选框按钮
    /// </summary>
    public class NuiBlueRadioButton : RadioButton
    {
        #region Field

        private NuiBlueControlState state = NuiBlueControlState.Normal;
        private Image dotImg = Properties.Resources.blue_radiobox_dot;
        private Font defaultFont = new Font("微软雅黑", 9);

        private static readonly ContentAlignment RightAlignment = ContentAlignment.TopRight | ContentAlignment.BottomRight | ContentAlignment.MiddleRight;
        private static readonly ContentAlignment LeftAlignment = ContentAlignment.TopLeft | ContentAlignment.BottomLeft | ContentAlignment.MiddleLeft;

        #endregion

        #region Constructor

        public NuiBlueRadioButton()
        {
            SetStyles();
            this.BackColor = Color.Transparent;
            this.Font = defaultFont;
        }

        #endregion

        #region Properites

        [Browsable(true)]
        [Description("获取QQRadioButton左边正方形的宽度")]
        protected virtual int CheckRectWidth
        {
            get { return 12; }
        }

        #endregion

        #region Override

        protected override void OnMouseEnter(EventArgs e)
        {
            state = NuiBlueControlState.Hover;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            state = NuiBlueControlState.Normal;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
            {
                state = NuiBlueControlState.Down;
            }
            base.OnMouseDown(mevent);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
            {
                if (ClientRectangle.Contains(mevent.Location))
                {
                    state = NuiBlueControlState.Hover;
                }
                else
                {
                    state = NuiBlueControlState.Normal;
                }
            }
            base.OnMouseUp(mevent);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            if (Enabled)
            {
                state = NuiBlueControlState.Normal;
            }
            else
            {
                state = NuiBlueControlState.Disabled;
            }
            base.OnEnabledChanged(e);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            base.OnPaintBackground(pevent);

            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            Rectangle circleRect, textRect;
            CalculateRect(out circleRect, out textRect);

            if (Enabled == false)
            {
                state = NuiBlueControlState.Disabled;
            }

            switch (state)
            {
                case NuiBlueControlState.Hover:
                case NuiBlueControlState.Down:
                    DrawHighLightCircle(g, circleRect);
                    break;
                case NuiBlueControlState.Disabled:
                    DrawDisabledCircle(g, circleRect);
                    break;
                default:
                    DrawNormalCircle(g, circleRect);
                    break;
            }

            Color textColor = (Enabled == true) ? ForeColor : SystemColors.GrayText;
            TextRenderer.DrawText(
                g,
                Text,
                Font,
                textRect,
                textColor,
                MethodHelper.GetTextFormatFlags(TextAlign, RightToLeft == RightToLeft.Yes));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (dotImg != null)
                {
                    dotImg.Dispose();
                }
                if (defaultFont != null)
                {
                    defaultFont.Dispose();
                }
            }

            dotImg = null;
            defaultFont = null;
            base.Dispose(disposing);
        }

        #endregion

        #region Private

        private void DrawNormalCircle(Graphics g, Rectangle circleRect)
        {
            g.FillEllipse(Brushes.White, circleRect);
            using (Pen borderPen = new Pen(NuiBlueColorTable.QQBorderColor))
            {
                g.DrawEllipse(borderPen, circleRect);
            }
            if (Checked)
            {
                circleRect.Inflate(-2, -2);
                g.DrawImage(
                    dotImg,
                    new Rectangle(circleRect.X + 1, circleRect.Y + 1, circleRect.Width - 1, circleRect.Height - 1),
                    0,
                    0,
                    dotImg.Width,
                    dotImg.Height,
                    GraphicsUnit.Pixel);
            }
        }

        private void DrawHighLightCircle(Graphics g, Rectangle circleRect)
        {
            DrawNormalCircle(g, circleRect);
            using (Pen p = new Pen(NuiBlueColorTable.QQHoverInnerColor))
            {
                g.DrawEllipse(p, circleRect);

                circleRect.Inflate(1, 1);
                p.Color = NuiBlueColorTable.QQHoverColor;

                g.DrawEllipse(p, circleRect);
            }
        }

        private void DrawDisabledCircle(Graphics g, Rectangle circleRect)
        {
            g.DrawEllipse(SystemPens.ControlDark, circleRect);
            if (Checked)
            {
                circleRect.Inflate(-2, -2);
                g.DrawImage(
                    dotImg,
                    new Rectangle(circleRect.X + 1, circleRect.Y + 1, circleRect.Width - 1, circleRect.Height - 1),
                    0,
                    0,
                    dotImg.Width,
                    dotImg.Height,
                    GraphicsUnit.Pixel);
            }
        }

        private void SetStyles()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();
        }

        private void CalculateRect(out Rectangle circleRect, out Rectangle textRect)
        {
            circleRect = new Rectangle(
                0, 0, CheckRectWidth, CheckRectWidth);
            textRect = Rectangle.Empty;
            bool bCheckAlignLeft = (int)(LeftAlignment & CheckAlign) != 0;
            bool bCheckAlignRight = (int)(RightAlignment & CheckAlign) != 0;
            bool bRightToLeft = (RightToLeft == RightToLeft.Yes);

            if ((bCheckAlignLeft && !bRightToLeft) ||
                (bCheckAlignRight && bRightToLeft))
            {
                switch (CheckAlign)
                {
                    case ContentAlignment.TopRight:
                    case ContentAlignment.TopLeft:
                        circleRect.Y = 2;
                        break;
                    case ContentAlignment.MiddleRight:
                    case ContentAlignment.MiddleLeft:
                        circleRect.Y = (Height - CheckRectWidth) / 2;
                        break;
                    case ContentAlignment.BottomRight:
                    case ContentAlignment.BottomLeft:
                        circleRect.Y = Height - CheckRectWidth - 2;
                        break;
                }

                circleRect.X = 1;

                textRect = new Rectangle(
                    circleRect.Right + 2,
                    0,
                    Width - circleRect.Right - 4,
                    Height);
            }
            else if ((bCheckAlignRight && !bRightToLeft)
                || (bCheckAlignLeft && bRightToLeft))
            {
                switch (CheckAlign)
                {
                    case ContentAlignment.TopLeft:
                    case ContentAlignment.TopRight:
                        circleRect.Y = 2;
                        break;
                    case ContentAlignment.MiddleLeft:
                    case ContentAlignment.MiddleRight:
                        circleRect.Y = (Height - CheckRectWidth) / 2;
                        break;
                    case ContentAlignment.BottomLeft:
                    case ContentAlignment.BottomRight:
                        circleRect.Y = Height - CheckRectWidth - 2;
                        break;
                }

                circleRect.X = Width - CheckRectWidth - 1;

                textRect = new Rectangle(
                    2, 0, Width - CheckRectWidth - 6, Height);
            }
            else
            {
                switch (CheckAlign)
                {
                    case ContentAlignment.TopCenter:
                        circleRect.Y = 2;
                        textRect.Y = circleRect.Bottom + 2;
                        textRect.Height = Height - CheckRectWidth - 6;
                        break;
                    case ContentAlignment.MiddleCenter:
                        circleRect.Y = (Height - CheckRectWidth) / 2;
                        textRect.Y = 0;
                        textRect.Height = Height;
                        break;
                    case ContentAlignment.BottomCenter:
                        circleRect.Y = Height - CheckRectWidth - 2;
                        textRect.Y = 0;
                        textRect.Height = Height - CheckRectWidth - 6;
                        break;
                }

                circleRect.X = (Width - CheckRectWidth) / 2;

                textRect.X = 2;
                textRect.Width = Width - 4;
            }
        }

        #endregion
    }
}
