using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NuiLib.NuiBlueControl;

namespace KST.ControlEx
{
    public partial class PracticeWorkPanel : WorkPanelBase
    {
        public PracticeWorkPanel()
        {
            InitializeComponent();
        }

        private void nuiBlueGlassButton2_Click(object sender, EventArgs e)
        {
            NuiBlueMessageBox.Show(FormInstanceManager.Instance.FormMain, "网络连接错误", NuiBlueMessageBoxIcon.Error);
        }

        private void nuiBlueButton3_Click(object sender, EventArgs e)
        {
            NuiBlueMessageBox.Show(FormInstanceManager.Instance.FormMain, "打印成功", NuiBlueMessageBoxIcon.OK);
        }


    }
}
