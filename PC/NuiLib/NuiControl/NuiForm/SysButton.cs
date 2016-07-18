using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace NuiLib
{
    /// <summary>
    /// 窗体关闭、还原、缩小等系统按钮类
    /// </summary>
    internal class SysButton
    {
        private Form parent;                                    // 父窗体容器
        private SysButtonName name;                             // 按钮名称
        private SysButtonState state;                           // 按钮状态
        private bool visible = true;                            // 按钮可见性
        private Point location = Point.Empty;                   // 按钮坐标
        private Size size = Size.Empty;                         // 按钮大小
        private Rectangle locationRect = Rectangle.Empty;       // 坐标矩形
        private Image resourceImage;                            // 图片资源
        private string toolTip;                                 // 提示字符串

        /// <summary>
        /// 父窗体容器
        /// </summary>
        public Form Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        /// <summary>
        /// 按钮名称
        /// </summary>
        public SysButtonName Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// 按钮当前状态
        /// </summary>
        public SysButtonState State
        {
            get { return state; }
            set
            {
                if (state != value)
                {
                    state = value;
                    parent.Invalidate();
                }
            }
        }

        /// <summary>
        /// 按钮是否可见
        /// </summary>
        public bool Visible
        {
            get { return visible; }
            set
            {
                if (visible != value)
                {
                    parent.Invalidate();
                }
                visible = value;
            }
        }

        /// <summary>
        /// 按钮左上角相对于其容器的左上角的坐标。 
        /// </summary>
        public Point Location
        {
            get { return location; }
            set
            {
                location = value;
                locationRect = new Rectangle(location, size);
                parent.Invalidate();
            }
        }

        /// <summary>
        /// 按钮大小
        /// </summary>
        public Size Size
        {
            get { return size; }
            set
            {
                size = value;
                locationRect = new Rectangle(location, size);
                parent.Invalidate();
            }
        }

        /// <summary>
        /// 按钮坐标矩形
        /// </summary>
        public Rectangle LocationRect
        {
            get
            {
                return visible ? locationRect : Rectangle.Empty;
            }
        }

        /// <summary>
        /// 资源图片
        /// </summary>
        public Image ResourceImage
        {
            get { return resourceImage; }
            set { resourceImage = value; parent.Invalidate(); }
        }

        /// <summary>
        /// 按钮提示文本
        /// </summary>
        public string ToolTip
        {
            get { return toolTip; }
            set { toolTip = value; }
        }

        /// <summary>
        /// 鼠标按下事件
        /// </summary>
        public event MouseEventHandler MouseDown;

        /// <summary>
        /// 响应鼠标按下事件
        /// </summary>
        public void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (MouseDown != null)
            {
                MouseDown(sender, e);
            }
        }
    }
}
