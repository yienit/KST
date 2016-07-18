using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using NuiLib;
using System.ComponentModel;

namespace KST.ControlEx
{
    /// <summary>
    /// 皮肤主题Item按钮
    /// </summary>
    public class SkinItemButton : RadioButton
    {
        #region Field

        private const int SHADOW_RECT_HEIGHT = 35;
        private static readonly Image NORMAL_BKG = Properties.Resources.skin_item_bkg_normal;
        private static readonly Image HOVER_BKG = Properties.Resources.skin_item_bkg_hover;
        private static readonly Image SELECT_BKG = Properties.Resources.skin_item_select;

        private ControlState state = ControlState.Normal;
        private string skinName;

        #endregion

        #region Constructor

        public SkinItemButton()
        {
            this.Cursor = Cursors.Hand;
        }

        #endregion

        #region Property

        /// <summary>
        /// 皮肤ID
        /// </summary>
        [Browsable(true)]
        public string SkinID { get; set; }

        /// <summary>
        /// 皮肤名称
        /// </summary>
        [Browsable(true)]
        public string SkinName
        {
            get { return skinName; }
            set { skinName = value; Invalidate(); }
        }

        /// <summary>
        /// 皮肤主窗体背景图片
        /// </summary>
        [Browsable(true)]
        public Image SkinFrameBkg { get; set; }

        #endregion

        #region Override

        protected override void OnMouseEnter(EventArgs e)
        {
            if (state != ControlState.Down)
            {
                state = ControlState.Hover; ;
            }
            this.Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (state != ControlState.Down)
            {
                state = ControlState.Normal;
            }
            this.Invalidate();
            base.OnMouseLeave(e);
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            state = this.Checked ? ControlState.Down : ControlState.Normal;
            Invalidate();
            base.OnCheckedChanged(e);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;

            // Draw image
            if (this.Image != null)
            {
                Render.DrawNineRectEx(g, this.Image, this.ClientRectangle, new Rectangle(Point.Empty, this.Image.Size));
            }

            switch (state)
            {
                case ControlState.Normal:
                    Render.DrawNineRectEx(g, NORMAL_BKG, this.ClientRectangle, new Rectangle(Point.Empty, NORMAL_BKG.Size));
                    break;
                case ControlState.Hover:
                    DrawShadowRectAndText(g, skinName);
                    Render.DrawNineRectEx(g, HOVER_BKG, this.ClientRectangle, new Rectangle(Point.Empty, HOVER_BKG.Size));
                    break;
                case ControlState.Down:
                    DrawShadowRectAndText(g, skinName);
                    g.DrawImage(
                        SELECT_BKG,
                        new Rectangle(this.Width - 30, this.Height - 32, SELECT_BKG.Width, SELECT_BKG.Height),
                        new Rectangle(Point.Empty, SELECT_BKG.Size),
                        GraphicsUnit.Pixel);
                    Render.DrawNineRectEx(g, HOVER_BKG, this.ClientRectangle, new Rectangle(Point.Empty, HOVER_BKG.Size));
                    break;
                default: break;
            }
        }

        #endregion

        #region Private

        /// <summary>
        /// 绘制高亮状态下的阴影矩形和文字
        /// </summary>
        private void DrawShadowRectAndText(Graphics g, string text)
        {
            using (SolidBrush brush = new SolidBrush(Color.FromArgb(150, Color.Black)))
            {
                Rectangle shadowRect = new Rectangle(0, this.Height - SHADOW_RECT_HEIGHT, this.Width, SHADOW_RECT_HEIGHT);
                g.FillRectangle(brush, shadowRect);

                if (!string.IsNullOrEmpty(text))
                {
                    using (Font font = new Font("微软雅黑", 9.0f))
                    {
                        brush.Color = Color.White;
                        g.DrawString(skinName, font, brush, 12, shadowRect.Top + 5);
                    }
                }
            }
        }

        #endregion

    }
}
