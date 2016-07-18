using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace NuiLib
{
    /// <summary>
    /// NuiHeaderButton按钮控件事件参数
    /// </summary>
    public class NuiHeaderButtonEventArgs : EventArgs
    {
        private int x;
        private int y;
        private MouseButtons mouseBtn;
        private int index;
        private NuiHeaderButton headerBtn;

        /// <summary>
        /// 使用鼠标按钮、鼠标坐标和触发该事件的Tab按钮和及其索引来初始化该事件参数
        /// </summary>
        public NuiHeaderButtonEventArgs(MouseButtons mouseBtn, int x, int y, int index, NuiHeaderButton tabBtn)
        {
            this.mouseBtn = mouseBtn;
            this.x = x;
            this.y = y;
            this.index = index;
            this.headerBtn = tabBtn;
        }

        /// <summary>
        /// 获取按下的鼠标键
        /// </summary>
        public MouseButtons MouseButton { get { return mouseBtn; } }

        /// <summary>
        /// 获取鼠标按下的的 HeaderButton 按钮对象
        /// </summary>
        public NuiHeaderButton HeaderButton { get { return headerBtn; } }

        /// <summary>
        /// 触发该事件的按钮的按钮索引(以0开始)
        /// </summary>
        public int Index { get { return index; } }

        /// <summary>
        /// 获取鼠标在产生鼠标事件时的 x 坐标（以像素为单位）
        /// </summary>
        public int X { get { return x; } }

        /// <summary>
        /// 获取鼠标在产生鼠标事件时的 y 坐标（以像素为单位）
        /// </summary>
        public int Y { get { return y; } }

        /// <summary>
        /// 包含鼠标的 x 和 y 坐标（以像素为单位）。
        /// </summary>
        public Point Location { get { return new Point(x, y); } }
    }
}
