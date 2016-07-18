using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KST.ControlEx
{
    /// <summary>
    /// 工作面板WorkPanel基类
    /// </summary>
    public class WorkPanelBase : UserControl
    {
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // WorkPanelBase
            // 
            this.Name = "WorkPanelBase";
            this.Size = new System.Drawing.Size(273, 219);
            this.ResumeLayout(false);

        }
    }
}
