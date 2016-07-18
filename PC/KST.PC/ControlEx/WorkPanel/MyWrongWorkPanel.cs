using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KST.ControlEx
{
    /// <summary>
    /// 我的错题WorkPanel
    /// </summary>
    public partial class MyWrongWorkPanel : WorkPanelBase
    {
        public MyWrongWorkPanel()
        {
            InitializeComponent();
        }

        private void btnEnableDemo_Click(object sender, EventArgs e)
        {
            this.btnOk.Enabled = !this.btnOk.Enabled;
        }

        private void nuiImageButton8_Click(object sender, EventArgs e)
        {
            this.btnExamine.Enabled = !this.btnExamine.Enabled;
        }
    }
}
