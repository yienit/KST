using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace NuiLib
{
    /// <summary>
    /// 360安全卫士主菜单样式ContextMenuStrip弹出菜单渲染类
    /// </summary>
    public class NuiContextMenuStripRender : ToolStripRenderer
    {
        private static readonly Color BACKGROUND_COLOR = Color.FromArgb(250, 250, 250);
        private static readonly Color FORE_COLOR = SystemColors.ControlText;
        private static readonly Color BORDER_COLOR = Color.FromArgb(197, 197, 197);
        private static readonly Color SEPARATOR_COLOR = Color.FromArgb(197, 197, 197);

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            using (SolidBrush brush = new SolidBrush(BACKGROUND_COLOR))
            {
                e.Graphics.FillRectangle(brush, e.AffectedBounds);
            }
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            using (GraphicsPath path = CreatePath(e.AffectedBounds))
            {
                using (Pen pen = new Pen(BORDER_COLOR))
                {
                    e.Graphics.DrawPath(pen, path);
                }
            }
        }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            ToolStripItem item = e.Item;

            if (!item.Enabled)
            {
                return;
            }

            if (item.Selected)
            {
                Rectangle rect = new Rectangle(Point.Empty, e.Item.Size);
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(225, 225, 225)))
                {
                    e.Graphics.FillRectangle(brush, rect);
                }
            }
            else
            {
                base.OnRenderMenuItemBackground(e);
            }
        }

        protected override void OnRenderItemImage(ToolStripItemImageRenderEventArgs e)
        {
            Rectangle rect = e.ImageRectangle;
            rect.X += 4;
            ToolStripItemImageRenderEventArgs ne = new ToolStripItemImageRenderEventArgs(e.Graphics, e.Item, e.Image, rect);

            base.OnRenderItemImage(ne);
        }

        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            e.TextColor = FORE_COLOR;
            base.OnRenderItemText(e);
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            if (e.Item.Enabled)
            {
                e.ArrowColor = FORE_COLOR;
            }
            base.OnRenderArrow(e);
        }

        protected override void OnRenderSeparator(ToolStripSeparatorRenderEventArgs e)
        {
            Rectangle rect = e.Item.ContentRectangle;
            using (Pen pen = new Pen(SEPARATOR_COLOR))
            {
                e.Graphics.DrawLine(pen, -1, rect.Y, rect.Right + 2, rect.Y);
            }
        }

        /// <summary>
        /// 从矩形中创建路径对象
        /// </summary>
        private GraphicsPath CreatePath(Rectangle rect)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new Rectangle(rect.X, rect.Y, rect.Width - 1, rect.Height - 1));
            path.CloseFigure();

            return path;
        }
    }
}
