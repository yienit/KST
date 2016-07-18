using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using NuiLib.NuiBlueControl;

namespace NuiLib
{
    /// <summary>
    /// 360安全卫士样式的复选框按钮
    /// </summary>
    public class NuiCheckBox : CheckBox
    {
        #region Field

        private NuiBlueControlState state = NuiBlueControlState.Normal;
        private Font defaultFont = new Font("微软雅黑", 9);
        private Image resourceImage = Properties.Resources.checkbox;

        #endregion

        #region Constructor

        public NuiCheckBox()
        {
            SetStyles();
            this.BackColor = Color.Transparent;
            this.Font = defaultFont;
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

        protected override void OnGotFocus(EventArgs e)
        {
            state = NuiBlueControlState.Focus;
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            state = NuiBlueControlState.Normal;
            base.OnLostFocus(e);
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

            const int IMAGE_WIDTH = 15;
            const int IMAGE_HEIGHT = 15;
            Rectangle imageRect = new Rectangle(0, 3, IMAGE_WIDTH, IMAGE_HEIGHT);
            Rectangle textRect = new Rectangle(imageRect.Right + 2, 0, Width - imageRect.Right - 4, Height);

            System.Diagnostics.Debug.WriteLine(this.CheckState.ToString());

            int startX = 0;
            if (Enabled == false) { state = NuiBlueControlState.Disabled; }
            switch (this.CheckState)
            {
                case CheckState.Unchecked:
                    {
                        switch (state)
                        {
                            case NuiBlueControlState.Normal: startX = 0; break;
                            case NuiBlueControlState.Hover: startX = IMAGE_WIDTH; break;
                            case NuiBlueControlState.Focus: startX = IMAGE_WIDTH; break;
                            case NuiBlueControlState.Down: startX = IMAGE_WIDTH * 2; break;
                            case NuiBlueControlState.Disabled: startX = IMAGE_WIDTH * 3; break;
                            default: break;
                        }
                    }
                    break;
                case CheckState.Checked:
                    {
                        switch (state)
                        {
                            case NuiBlueControlState.Normal: startX = IMAGE_WIDTH * 4; break;
                            case NuiBlueControlState.Hover: startX = IMAGE_WIDTH * 5; break;
                            case NuiBlueControlState.Focus: startX = IMAGE_WIDTH * 5; break;
                            case NuiBlueControlState.Down: startX = IMAGE_WIDTH * 6; break;
                            case NuiBlueControlState.Disabled: startX = IMAGE_WIDTH * 7; break;
                            default: break;
                        }
                    }
                    break;
                case CheckState.Indeterminate:
                    {
                        switch (state)
                        {
                            case NuiBlueControlState.Normal: startX = IMAGE_WIDTH * 8; break;
                            case NuiBlueControlState.Hover: startX = IMAGE_WIDTH * 9; break;
                            case NuiBlueControlState.Focus: startX = IMAGE_WIDTH * 9; break;
                            case NuiBlueControlState.Down: startX = IMAGE_WIDTH * 10; break;
                            case NuiBlueControlState.Disabled: startX = IMAGE_WIDTH * 11; break;
                            default: break;
                        }
                    }
                    break;
                default:
                    break;
            }

            g.DrawImage(resourceImage, imageRect, new Rectangle(startX, 0, IMAGE_WIDTH, IMAGE_HEIGHT), GraphicsUnit.Pixel);

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
                if (defaultFont != null)
                {
                    defaultFont.Dispose();
                }
            }
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

        #endregion
    }
}
