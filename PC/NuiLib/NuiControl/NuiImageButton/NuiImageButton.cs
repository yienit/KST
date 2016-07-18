using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace NuiLib
{
    /// <summary>
    /// 图片按钮控件，使用四态图图片(普通,高亮,按下,禁止)来模拟按钮的不同状态的显示效果
    /// </summary>
    public class NuiImageButton : Button
    {
        private NuiImageButtonState state = NuiImageButtonState.Normal;

        protected override void OnMouseEnter(EventArgs e)
        {
            state = NuiImageButtonState.Hover;
            this.Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (state == NuiImageButtonState.Hover && Focused)
            {
                state = NuiImageButtonState.Focus;
            }
            else if (state == NuiImageButtonState.Focus)
            {
                state = NuiImageButtonState.Focus;
            }
            else
            {
                state = NuiImageButtonState.Normal;
            }
            this.Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            if (mevent.Button == MouseButtons.Left)
            {
                state = NuiImageButtonState.Down;
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
                    state = NuiImageButtonState.Hover;
                }
                else
                {
                    state = NuiImageButtonState.Focus;
                }
            }
            this.Invalidate();
            base.OnMouseUp(mevent);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            state = NuiImageButtonState.Normal;
            this.Invalidate();
            base.OnLostFocus(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            state = NuiImageButtonState.Focus;
            base.OnGotFocus(e);
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            if (Enabled)
            {
                state = NuiImageButtonState.Normal;
            }
            else
            {
                state = NuiImageButtonState.Disabled;
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

            if (this.Image != null)
            {
                int BUTTON_WIDTH = this.Image.Width / 4;
                int BUTON_HEIGHT = this.Image.Height;
                if (!Enabled) { state = NuiImageButtonState.Disabled; }

                switch (state)
                {
                    case NuiImageButtonState.Normal:
                        Render.DrawNineRectEx(
                            g,
                            this.Image,
                            ClientRectangle,
                            new Rectangle(0, 0, BUTTON_WIDTH, BUTON_HEIGHT));
                        break;
                    case NuiImageButtonState.Hover:
                        Render.DrawNineRectEx(
                              g,
                              this.Image,
                              ClientRectangle,
                              new Rectangle(BUTTON_WIDTH, 0, BUTTON_WIDTH, BUTON_HEIGHT));
                        break;
                    case NuiImageButtonState.Focus:
                        Render.DrawNineRectEx(
                           g,
                           this.Image,
                           ClientRectangle,
                           new Rectangle(BUTTON_WIDTH, 0, BUTTON_WIDTH, BUTON_HEIGHT));
                        break;

                    case NuiImageButtonState.Down:
                        Render.DrawNineRectEx(
                             g,
                             this.Image,
                             ClientRectangle,
                             new Rectangle(BUTTON_WIDTH * 2, 0, BUTTON_WIDTH, BUTON_HEIGHT));
                        break;
                    case NuiImageButtonState.Disabled:
                        Render.DrawNineRectEx(
                           g,
                           this.Image,
                           ClientRectangle,
                           new Rectangle(BUTTON_WIDTH * 3, 0, BUTTON_WIDTH, BUTON_HEIGHT));
                        break;
                    default:
                        break;
                }
            }

            if (this.Text.Length != 0)
            {
                Rectangle textRect = new Rectangle(3, 0, Width - 6, Height);
                Color textColor = Enabled ? ForeColor : SystemColors.GrayText;
                TextRenderer.DrawText(
                      g,
                      Text,
                      Font,
                      textRect,
                      textColor,
                      GetTextFormatFlags(TextAlign, RightToLeft == RightToLeft.Yes));
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
    }
}
