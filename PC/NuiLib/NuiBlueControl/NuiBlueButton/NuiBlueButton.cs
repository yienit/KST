using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace NuiLib.NuiBlueControl
{
    /// <summary>
    /// 腾讯QQ蓝色按钮控件
    /// </summary>
    public class NuiBlueButton : Button
    {
        #region Field

        private NuiBlueControlState state = NuiBlueControlState.Normal;
        private Font defaultFont = new Font("微软雅黑", 9);

        private Image normalImg = Properties.Resources.blue_btn_normal;
        private Image hightImg = Properties.Resources.blue_btn_highlight;
        private Image focusImg = Properties.Resources.blue_btn_focus;
        private Image downImg = Properties.Resources.blue_btn_down;

        #endregion

        #region Constructor

        public NuiBlueButton()
        {
            SetStyles();
            this.Font = defaultFont;
            this.Size = new Size(68, 23);
        }

        #endregion

        #region Properites

        private int ImageWidth
        {
            get
            {
                if (Image == null)
                {
                    return 16;
                }
                else
                {
                    return Image.Width;
                }
            }

        }

        #endregion

        #region Override

        protected override void OnMouseEnter(EventArgs e)
        {
            state = NuiBlueControlState.Hover;
            this.Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (state == NuiBlueControlState.Hover && Focused)
            {
                state = NuiBlueControlState.Focus;
            }
            else if (state == NuiBlueControlState.Focus)
            {
                state = NuiBlueControlState.Focus;
            }
            else
            {
                state = NuiBlueControlState.Normal;
            }
            this.Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
            {
                state = NuiBlueControlState.Down;
            }
            this.Invalidate();
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
                    state = NuiBlueControlState.Focus;
                }
            }
            this.Invalidate();
            base.OnMouseUp(mevent);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            state = NuiBlueControlState.Normal;
            this.Invalidate();
            base.OnLostFocus(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            state = NuiBlueControlState.Focus;
            this.Invalidate();
            base.OnGotFocus(e);
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
            this.Invalidate();
            base.OnEnabledChanged(e);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;

            Rectangle imageRect, textRect;
            CalculateRect(out imageRect, out textRect);

            if (!Enabled)
            {
                state = NuiBlueControlState.Disabled;
            }
            switch (state)
            {
                case NuiBlueControlState.Normal:

                    Render.DrawNineRectEx(
                        g, normalImg,
                        ClientRectangle,
                        new Rectangle(0, 0, normalImg.Width, normalImg.Height));
                    break;
                case NuiBlueControlState.Hover:

                    Render.DrawNineRectEx(
                        g, hightImg,
                        ClientRectangle,
                        new Rectangle(0, 0, hightImg.Width, hightImg.Height));
                    break;
                case NuiBlueControlState.Focus:

                    Render.DrawNineRectEx(
                        g, focusImg,
                        ClientRectangle,
                        new Rectangle(0, 0, focusImg.Width, focusImg.Height));
                    break;
                case NuiBlueControlState.Down:
                    Render.DrawNineRectEx(
                       g, downImg,
                       ClientRectangle,
                       new Rectangle(0, 0, downImg.Width, downImg.Height));
                    break;
                case NuiBlueControlState.Disabled:
                    DrawDisabledButton(g);
                    break;
                default:
                    break;
            }

            if (Image != null)
            {
                g.DrawImage(Image, imageRect, 0, 0, Image.Width, Image.Height, GraphicsUnit.Pixel);
            }

            Color textColor = Enabled ? ForeColor : SystemColors.GrayText;
            TextRenderer.DrawText(
                  g,
                  Text,
                  Font,
                  textRect,
                  textColor,
                  GetTextFormatFlags(TextAlign, RightToLeft == RightToLeft.Yes));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (normalImg != null) { normalImg.Dispose(); }
                if (hightImg != null) { hightImg.Dispose(); }
                if (downImg != null) { downImg.Dispose(); }
                if (focusImg != null) { focusImg.Dispose(); }
                if (defaultFont != null) { defaultFont.Dispose(); }
            }

            normalImg = null;
            hightImg = null;
            focusImg = null;
            downImg = null;
            defaultFont = null;
            base.Dispose(disposing);
        }

        #endregion

        #region Private

        private void SetStyles()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();
        }

        private void CalculateRect(out Rectangle imageRect, out Rectangle textRect)
        {
            imageRect = Rectangle.Empty;
            textRect = Rectangle.Empty;
            if (Image == null)
            {
                textRect = new Rectangle(
                   3,
                   0,
                   Width - 6,
                   Height);
                return;
            }
            switch (TextImageRelation)
            {
                case TextImageRelation.Overlay:
                    imageRect = new Rectangle(
                        3,
                        (Height - ImageWidth) / 2,
                        ImageWidth,
                        ImageWidth);
                    textRect = new Rectangle(
                        3,
                        0,
                        Width - 6,
                        Height);
                    break;
                case TextImageRelation.ImageAboveText:
                    imageRect = new Rectangle(
                        (Width - ImageWidth) / 2,
                        3,
                        ImageWidth,
                        ImageWidth);
                    textRect = new Rectangle(
                        3,
                        imageRect.Bottom,
                        Width - 6,
                        Height - imageRect.Bottom - 2);
                    break;
                case TextImageRelation.ImageBeforeText:
                    imageRect = new Rectangle(
                        6,
                        (Height - ImageWidth) / 2,
                        ImageWidth,
                        ImageWidth);
                    textRect = new Rectangle(
                        imageRect.Right + 6,
                        0,
                        Width - imageRect.Right - 12,
                        Height);
                    break;
                case TextImageRelation.TextAboveImage:
                    imageRect = new Rectangle(
                        (Width - ImageWidth) / 2,
                        Height - ImageWidth - 3,
                        ImageWidth,
                        ImageWidth);
                    textRect = new Rectangle(
                        0,
                        3,
                        Width,
                        Height - imageRect.Y - 3);
                    break;
                case TextImageRelation.TextBeforeImage:
                    imageRect = new Rectangle(
                        Width - ImageWidth - 6,
                        (Height - ImageWidth) / 2,
                        ImageWidth,
                        ImageWidth);
                    textRect = new Rectangle(
                        3,
                        0,
                        imageRect.X - 3,
                        Height);
                    break;
            }

            if (RightToLeft == RightToLeft.Yes)
            {
                imageRect.X = Width - imageRect.Right;
                textRect.X = Width - textRect.Right;
            }
        }

        private void DrawDisabledButton(Graphics g)
        {
            int radius = 4;
            //此处让其宽度减1，让其由Normal态平滑自然的过渡到Disabled态，保持按钮高度一致。
            using (GraphicsPath borderPath = Render.CreateRoundPath(new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height - 1), radius))
            {
                using (Pen disalbedPen = new Pen(Color.FromArgb(156, 165, 177)))
                {
                    g.DrawPath(disalbedPen, borderPath);
                }

                //背景层渐变,向内缩小1个像素
                Rectangle backRect = new Rectangle(ClientRectangle.X + 1, ClientRectangle.Y + 1, ClientRectangle.Width - 2, ClientRectangle.Height - 2 - 1);
                using (GraphicsPath innerPath = Render.CreateRoundPath(backRect, radius))
                {
                    using (LinearGradientBrush lBrush = new LinearGradientBrush(backRect, Color.FromArgb(247, 252, 254), Color.FromArgb(230, 240, 243), LinearGradientMode.Vertical))
                    {
                        g.FillPath(lBrush, innerPath);
                    }
                }
            }
        }

        internal static TextFormatFlags GetTextFormatFlags(ContentAlignment alignment, bool rightToleft)
        {
            TextFormatFlags flags = TextFormatFlags.WordBreak |
                TextFormatFlags.SingleLine;
            if (rightToleft)
            {
                flags |= TextFormatFlags.RightToLeft | TextFormatFlags.Right;
            }

            switch (alignment)
            {
                case ContentAlignment.BottomCenter:
                    flags |= TextFormatFlags.Bottom | TextFormatFlags.HorizontalCenter;
                    break;
                case ContentAlignment.BottomLeft:
                    flags |= TextFormatFlags.Bottom | TextFormatFlags.Left;
                    break;
                case ContentAlignment.BottomRight:
                    flags |= TextFormatFlags.Bottom | TextFormatFlags.Right;
                    break;
                case ContentAlignment.MiddleCenter:
                    flags |= TextFormatFlags.HorizontalCenter |
                        TextFormatFlags.VerticalCenter;
                    break;
                case ContentAlignment.MiddleLeft:
                    flags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Left;
                    break;
                case ContentAlignment.MiddleRight:
                    flags |= TextFormatFlags.VerticalCenter | TextFormatFlags.Right;
                    break;
                case ContentAlignment.TopCenter:
                    flags |= TextFormatFlags.Top | TextFormatFlags.HorizontalCenter;
                    break;
                case ContentAlignment.TopLeft:
                    flags |= TextFormatFlags.Top | TextFormatFlags.Left;
                    break;
                case ContentAlignment.TopRight:
                    flags |= TextFormatFlags.Top | TextFormatFlags.Right;
                    break;
            }
            return flags;
        }

        #endregion
    }
}
