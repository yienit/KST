using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace NuiLib
{
    /// <summary>
    /// 360安全卫士样式头部Panel控件
    /// </summary>
    public class NuiHeaderPanel : Panel
    {
        #region Field

        private List<NuiHeaderButton> headerBtnList;         // 头部按钮列表
        private int currentFocusBtnIndex = 0;                // 当前Focus状态按钮索引

        #endregion

        #region Property

        /// <summary>
        /// 头部按钮列表
        /// </summary>
        public List<NuiHeaderButton> HeaderButtons
        {
            get { return headerBtnList; }
            set
            {
                headerBtnList = value;
                CaculateHeaderBtnLocation();
                Invalidate();
            }
        }

        /// <summary>
        /// 当前Focus状态的头部按钮索引(从0开始)
        /// </summary>
        public int FocusIndex
        {
            get { return currentFocusBtnIndex; }
            set
            {
                if (headerBtnList != null && value != currentFocusBtnIndex)
                {
                    NuiHeaderButton headerBtn = headerBtnList[value];

                    // 清除原来Focus状态的为Normal
                    headerBtnList[currentFocusBtnIndex].State = NuiHeaderButtonState.Normal;
                    headerBtnList[value].State = NuiHeaderButtonState.Focus;
                    currentFocusBtnIndex = value;

                    this.Invalidate();
                }
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public NuiHeaderPanel()
        {
            // 开启双缓冲
            this.SetStyle(ControlStyles.ResizeRedraw |
              ControlStyles.OptimizedDoubleBuffer |
              ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();

            // 设置默认
            this.Left = 0;
            this.BackColor = Color.Transparent;
            this.Width = 406;
            this.Height = 80;
        }

        #endregion

        #region Override

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (headerBtnList != null)
            {
                foreach (NuiHeaderButton headerBtn in headerBtnList)
                {
                    if (headerBtn.ClientRectangle.Contains(e.Location))
                    {
                        if (headerBtn.State == NuiHeaderButtonState.Normal)
                        {
                            headerBtn.State = NuiHeaderButtonState.Enter;
                        }
                    }
                    else
                    {
                        if (headerBtn.State != NuiHeaderButtonState.Focus)
                        {
                            headerBtn.State = NuiHeaderButtonState.Normal;
                        }
                    }
                }
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (headerBtnList != null)
            {
                if (e.Button == MouseButtons.Left)
                {
                    for (int index = 0; index < headerBtnList.Count; index++)
                    {
                        if (index != currentFocusBtnIndex)
                        {
                            NuiHeaderButton headerBtn = headerBtnList[index];
                            if (headerBtn.ClientRectangle.Contains(e.Location))
                            {
                                // 清除原来Focus状态的为Normal
                                headerBtnList[currentFocusBtnIndex].State = NuiHeaderButtonState.Normal;
                                headerBtnList[index].State = NuiHeaderButtonState.Focus;
                                currentFocusBtnIndex = index;
                            }
                        }
                    }
                }
            }
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (headerBtnList != null)
            {
                for (int index = 0; index < headerBtnList.Count; index++)
                {
                    NuiHeaderButton headerBtn = headerBtnList[index];
                    if (headerBtn.ClientRectangle.Contains(e.Location))
                    {
                        NuiHeaderButtonEventArgs args = new NuiHeaderButtonEventArgs(e.Button, e.X, e.Y, index, headerBtn);
                        headerBtn.OnMouseClick(this, args);
                    }
                }
            }

            base.OnMouseUp(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            // 避免鼠标离开Panel时focus状态的按钮也被绘制
            if (headerBtnList != null)
            {
                foreach (var headerBtn in headerBtnList)
                {
                    if (headerBtn.State != NuiHeaderButtonState.Focus)
                    {
                        headerBtn.State = NuiHeaderButtonState.Normal;
                    }
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (headerBtnList != null)
            {
                foreach (NuiHeaderButton headerBtn in headerBtnList)
                {
                    headerBtn.Render(e);
                }
            }
        }

        #endregion

        #region Private

        /// <summary>
        /// 动态计算头部按钮的坐标位置
        /// </summary>
        private void CaculateHeaderBtnLocation()
        {
            if (headerBtnList != null)
            {
                int btnTop = 5;
                int firstBtnLeft = 10;
                int btnsOffset = 5;

                for (int index = 0; index < headerBtnList.Count; index++)
                {
                    if (index == 0)
                    {
                        headerBtnList[index].Location = new Point(firstBtnLeft, btnTop);
                    }
                    else
                    {
                        headerBtnList[index].Location = new Point(headerBtnList[index - 1].ClientRectangle.Right + btnsOffset, btnTop);
                    }
                }

                if (headerBtnList.Count > 0)
                {
                    this.Width = headerBtnList[headerBtnList.Count - 1].ClientRectangle.Right + 10;
                }
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// 添加头部按钮
        /// </summary>
        public void AddHeaderButton(NuiHeaderButton headerBtn)
        {
            if (headerBtnList == null)
            {
                headerBtnList = new List<NuiHeaderButton>();
            }

            headerBtnList.Add(headerBtn);
            CaculateHeaderBtnLocation();
            this.Invalidate();
        }

        /// <summary>
        /// 删除头部按钮
        /// </summary>
        public void RemoveHeaderButton(NuiHeaderButton headerBtn)
        {
            if (headerBtnList != null)
            {
                headerBtnList.Remove(headerBtn);
                CaculateHeaderBtnLocation();
                this.Invalidate();
            }
        }

        /// <summary>
        /// 删除头部按钮
        /// </summary>
        public void RemoveHeaderButton(int index)
        {
            if (headerBtnList != null)
            {
                headerBtnList.RemoveAt(index);
                CaculateHeaderBtnLocation();
                this.Invalidate();
            }
        }

        #endregion
    }
}
