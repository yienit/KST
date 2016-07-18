using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NuiLib.NuiBlueControl
{
    /// <summary>
    /// 腾讯QQ玻璃按钮控件
    /// </summary>
    public class NuiBlueGlassButton : PictureBox, IButtonControl
    {
        #region  Fileds

        private DialogResult dialogResult;
        private bool isDefault = false;
        private bool holdingSpace = false;

        private NuiBlueControlState state = NuiBlueControlState.Normal;
        private Font defaultFont = new Font("微软雅黑", 9);

        private Image glassHotImg = Properties.Resources.blue_glassbtn_hot;
        private Image glassDownImg = Properties.Resources.blue_glassbtn_down;

        private ToolTip toolTip = new ToolTip();

        #endregion

        #region Constructor

        public NuiBlueGlassButton()
            : base()
        {
            this.BackColor = Color.Transparent;
            this.Size = new Size(75, 23);
            this.BorderStyle = BorderStyle.None;
            this.Font = defaultFont;
        }

        #endregion

        #region IButtonControl Members

        public DialogResult DialogResult
        {
            get
            {
                return dialogResult;
            }
            set
            {
                if (Enum.IsDefined(typeof(DialogResult), value))
                {
                    dialogResult = value;
                }
            }
        }

        public void NotifyDefault(bool value)
        {
            if (isDefault != value)
            {
                isDefault = value;
            }
        }

        public void PerformClick()
        {
            base.OnClick(EventArgs.Empty);
        }

        #endregion

        #region  Properties

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [Description("The text associated with the control.")]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [Description("The font used to display text in the control.")]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
            }
        }

        [Description("当鼠标放在控件可见处的提示文本")]
        public string ToolTipText { get; set; }

        #endregion

        #region Description Changes
        [Description("Controls how the ImageButton will handle image placement and control sizing.")]
        public new PictureBoxSizeMode SizeMode { get { return base.SizeMode; } set { base.SizeMode = value; } }

        [Description("Controls what type of border the ImageButton should have.")]
        public new BorderStyle BorderStyle { get { return base.BorderStyle; } set { base.BorderStyle = value; } }
        #endregion

        #region Hiding

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ImageLayout BackgroundImageLayout { get { return base.BackgroundImageLayout; } set { base.BackgroundImageLayout = value; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Image BackgroundImage { get { return base.BackgroundImage; } set { base.BackgroundImage = value; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new String ImageLocation { get { return base.ImageLocation; } set { base.ImageLocation = value; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Image ErrorImage { get { return base.ErrorImage; } set { base.ErrorImage = value; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Image InitialImage { get { return base.InitialImage; } set { base.InitialImage = value; } }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool WaitOnLoad { get { return base.WaitOnLoad; } set { base.WaitOnLoad = value; } }
        #endregion

        #region override

        protected override void OnMouseEnter(EventArgs e)
        {
            //show tool tip 
            if (ToolTipText != string.Empty)
            {
                HideToolTip();
                ShowTooTip(ToolTipText);
            }

            state = NuiBlueControlState.Hover;
            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            state = NuiBlueControlState.Normal;
            Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                state = NuiBlueControlState.Down;
            }
            Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                state = ClientRectangle.Contains(e.Location) ? NuiBlueControlState.Hover : NuiBlueControlState.Normal;
            }
            Invalidate();
            base.OnMouseUp(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {

            state = NuiBlueControlState.Normal;
            Invalidate();
            holdingSpace = false;
            base.OnLostFocus(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            state = NuiBlueControlState.Focus;
            Invalidate();
            base.OnGotFocus(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            Refresh();
            base.OnTextChanged(e);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Rectangle imageRect, textRect;
            CalculateRect(out imageRect, out textRect);
            switch (state)
            {
                case NuiBlueControlState.Hover:
                    Render.DrawNineRectEx(
                        pe.Graphics,
                        glassHotImg,
                        ClientRectangle,
                        new Rectangle(0, 0, glassHotImg.Width, glassHotImg.Height));
                    break;
                case NuiBlueControlState.Down:
                    Render.DrawNineRectEx(
                        pe.Graphics,
                        glassDownImg,
                        ClientRectangle,
                        new Rectangle(0, 0, glassDownImg.Width, glassDownImg.Height));
                    break;
                default:
                    break;
            }

            if (Image != null)
            {
                pe.Graphics.DrawImage(
                    Image,
                    imageRect,
                    new Rectangle(0, 0, Image.Width, Image.Height),
                    GraphicsUnit.Pixel);
            }

            if (Text.Trim().Length != 0)
            {
                TextRenderer.DrawText(
                    pe.Graphics,
                    Text,
                    Font,
                    textRect,
                    SystemColors.ControlText);
            }
        }

        public override bool PreProcessMessage(ref Message msg)
        {
            if (msg.Msg == Win32.WM_KEYUP)
            {
                if (holdingSpace)
                {
                    if ((int)msg.WParam == (int)Keys.Space)
                    {
                        OnMouseUp(null);
                        PerformClick();
                    }
                    else if ((int)msg.WParam == (int)Keys.Escape
                        || (int)msg.WParam == (int)Keys.Tab)
                    {
                        holdingSpace = false;
                        OnMouseUp(null);
                    }
                }
                return true;
            }
            else if (msg.Msg == Win32.WM_KEYDOWN)
            {
                if ((int)msg.WParam == (int)Keys.Space)
                {
                    holdingSpace = true;
                    OnMouseDown(null);
                }
                else if ((int)msg.WParam == (int)Keys.Enter)
                {
                    PerformClick();
                }
                return true;
            }
            else
                return base.PreProcessMessage(ref msg);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (defaultFont != null)
                    defaultFont.Dispose();
                if (glassDownImg != null)
                    glassDownImg.Dispose();
                if (glassHotImg != null)
                    glassHotImg.Dispose();
                if (toolTip != null)
                    toolTip.Dispose();
            }
            defaultFont = null;
            glassDownImg = null;
            glassHotImg = null;
            toolTip = null;
            base.Dispose(disposing);
        }



        #endregion

        #region Private

        private void CalculateRect(out Rectangle imageRect, out Rectangle textRect)
        {
            imageRect = Rectangle.Empty;
            textRect = Rectangle.Empty;
            if (Image == null && !string.IsNullOrEmpty(Text))
            {
                textRect = new Rectangle(3, 0, Width - 6, Height);
            }
            else if (Image != null && string.IsNullOrEmpty(Text))
            {
                imageRect = new Rectangle((Width - Image.Width) / 2, (Height - Image.Height) / 2, Image.Width, Image.Height);
            }
            else if (Image != null && !string.IsNullOrEmpty(Text))
            {
                imageRect = new Rectangle(6, (Height - Image.Height) / 2, Image.Width, Image.Height);
                textRect = new Rectangle(imageRect.Right + 1, 0, Width - 6 * 2 - imageRect.Width - 1, Height);
            }
        }

        private void ShowTooTip(string toolTipText)
        {
            toolTip.Active = true;
            toolTip.SetToolTip(this, toolTipText);
        }

        private void HideToolTip()
        {
            toolTip.Active = false;
        }

        #endregion
    }
}
