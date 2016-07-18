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
    /// NuiHeaderPanel控件中的头部按钮
    /// </summary>
    public class NuiHeaderButton
    {
        #region Field

        private NuiHeaderPanel containerPanel;

        private NuiHeaderButtonState state = NuiHeaderButtonState.Normal;
        private Point location;
        private Image image;
        private Size imageSize = new Size(48, 48);
        private Rectangle clientRect = Rectangle.Empty;
        private const int BUTTON_WIDTH = 80;
        private const int BUTTON_HEIGHT = 75;

        private string text = "HeaderButton";
        private Font font = new Font("宋体", 9.0f);
        private Color foreColor = Color.White;

        // 图片矩形和文本矩形
        private Rectangle imageRect = Rectangle.Empty;
        private Rectangle textRect = Rectangle.Empty;

        private static Image enterImage = Properties.Resources.header_btn_enter;
        private static Image focusImage = Properties.Resources.header_btn_focus;

        #endregion

        #region Property

        /// <summary>
        /// 父容器Panel面板
        /// </summary>
        public NuiHeaderPanel ContainerPanel
        {
            get { return containerPanel; }
            set { containerPanel = value; }
        }

        /// <summary>
        /// 按钮当前的状态
        /// </summary>
        public NuiHeaderButtonState State
        {
            get { return state; }
            set
            {
                if (state != value)
                {
                    state = value;
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// 按钮左上角相对于父容器所在的位置
        /// </summary>
        public Point Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
                CaculateInnerRect();
                Invalidate();
            }
        }

        /// <summary>
        /// 按钮的宽度
        /// </summary>
        public int Width { get { return BUTTON_WIDTH; } }

        /// <summary>
        /// 按钮的高度
        /// </summary>
        public int Height { get { return BUTTON_HEIGHT; } }

        /// <summary>
        /// 按钮所在的工作矩形区域
        /// </summary>
        public Rectangle ClientRectangle
        {
            get
            {
                return clientRect;
            }
        }

        /// <summary>
        /// 按钮上方区域需要显示的图片
        /// </summary>
        public Image Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                CaculateInnerRect();
                Invalidate();
            }
        }

        /// <summary>
        /// 图片的大小
        /// </summary>
        public Size ImageSize
        {
            get
            {
                return imageSize;
            }
            set
            {
                imageSize = value;
                CaculateInnerRect();
                Invalidate();
            }
        }

        /// <summary>
        /// 按钮下方区域需要显示的文本
        /// </summary>
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 按钮下方文本的字体
        /// </summary>
        public Font Font
        {
            get
            {
                return font;
            }
            set
            {
                font = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 按钮下方文本的颜色
        /// </summary>
        public Color ForeColor
        {
            get
            {
                return foreColor;
            }
            set
            {
                foreColor = value;
                Invalidate();
            }
        }

        #endregion

        #region Event

        /// <summary>
        /// 鼠标单击事件
        /// </summary>
        public event NuiHeaderButtonEventHandler MouseClick;

        #endregion

        #region Private

        /// <summary>
        /// 计算图标矩形和文本矩形
        /// </summary>
        private void CaculateInnerRect()
        {
            const int IMAGE_LEFT_OFFSET = 16;
            const int IMAGE_TOP_OFFSET = 5;
            imageRect = (image == null) ? Rectangle.Empty : new Rectangle(
                location.X + IMAGE_LEFT_OFFSET,
                location.Y + IMAGE_TOP_OFFSET,
                imageSize.Width,
                imageSize.Height);

            // 上下Padding为3，高度以字体的高度为基数
            int yOffset = 3;
            int fontHeight = TextRenderer.MeasureText(text, font).Height;

            textRect = new Rectangle(imageRect.Location.X,
                imageRect.Location.Y + imageRect.Height,
                imageRect.Width,
                fontHeight + 2 * yOffset);

            // 动态计算ClientRectangle
            clientRect = new Rectangle(location.X, location.Y, BUTTON_WIDTH, BUTTON_HEIGHT);
        }

        /// <summary>
        /// 使该按钮的坐标矩形无效从而导致让父容器Panel重绘按钮
        /// </summary>
        private void Invalidate()
        {
            this.containerPanel.Invalidate();
        }

        /// <summary>
        /// 根据按钮的状态、坐标等数据绘制按钮
        /// </summary>
        public void Render(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // 绘制按钮上方部分的图片
            g.DrawImage(image, imageRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);

            // 绘制按钮下方部分的文本
            int xOffset = 10;
            int yOffset = 10;
            Rectangle textInnerRect = textRect;
            textInnerRect.Inflate(xOffset, yOffset);
            TextRenderer.DrawText(g, text, font, textInnerRect, foreColor);

            // 绘制背景图片
            switch (state)
            {
                case NuiHeaderButtonState.Normal:
                    break;
                case NuiHeaderButtonState.Enter:
                    g.DrawImage(enterImage, ClientRectangle);
                    break;
                case NuiHeaderButtonState.Focus:
                    g.DrawImage(focusImage, ClientRectangle);
                    break;
                default:
                    break;
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// 响应鼠标单击事件
        /// </summary>
        public void OnMouseClick(object sender, NuiHeaderButtonEventArgs e)
        {
            if (MouseClick != null)
            {
                MouseClick(sender, e);
            }
        }

        #endregion
    }
}

/*
*              Client Rectangle
*      *********************************
*      *           Padding             *
*      *   ************************    *               
*      *   *                      *    *
*      *   *                      *    *
*      *   *                      *    *
*      *   *   Icon Rectangle     *    *
 *     *   *      (48 x 48)       *    *
*      *   *                      *    *
*      *   *                      *    *
*      *   ************************    *
*      *   *                      *    *
*      *   *   Text Rectangle     *    *
*      *   *                      *    *
*      *   ************************    *
*      *                               *
*      *********************************
* 
* 
*/
