using NuiLib;
namespace KST
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.headerPanel = new NuiLib.NuiHeaderPanel();
            this.mainMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mainMenuSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuExchangeCourse = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mainMenuUpdate = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuFeedback = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuWebsite = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuAboutUs = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mainMenuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.moreToolsWorkPanel = new KST.ControlEx.MoreToolsWorkPanel();
            this.myFavoriteWorkPanel = new KST.ControlEx.MyFavoriteWorkPanel();
            this.myWrongWorkPanel = new KST.ControlEx.MyWrongWorkPanel();
            this.previousPapersWorkPanel = new KST.ControlEx.PreviousPapersWorkPanel();
            this.vipPapersWorkPanel = new KST.ControlEx.VipPapersWorkPanel();
            this.examSimulationWorkPanel = new KST.ControlEx.ExamSimulationWorkPanel();
            this.practiceWorkPanel = new KST.ControlEx.PracticeWorkPanel();
            this.myHomeWorkPanel = new KST.ControlEx.MyHomeWorkPanel();
            this.mainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.Transparent;
            this.headerPanel.FocusIndex = 0;
            this.headerPanel.HeaderButtons = null;
            this.headerPanel.Location = new System.Drawing.Point(0, 20);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(615, 80);
            this.headerPanel.TabIndex = 0;
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuSetting,
            this.mainMenuExchangeCourse,
            this.mainMenuSeparator1,
            this.mainMenuUpdate,
            this.mainMenuFeedback,
            this.mainMenuWebsite,
            this.mainMenuAboutUs,
            this.mainMenuSeparator2,
            this.mainMenuExit});
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(125, 194);
            // 
            // mainMenuSetting
            // 
            this.mainMenuSetting.Image = global::KST.Properties.Resources.main_menu_setting;
            this.mainMenuSetting.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.mainMenuSetting.Name = "mainMenuSetting";
            this.mainMenuSetting.Size = new System.Drawing.Size(124, 22);
            this.mainMenuSetting.Text = "系统设置";
            // 
            // mainMenuExchangeCourse
            // 
            this.mainMenuExchangeCourse.Image = global::KST.Properties.Resources.main_menu_exchange;
            this.mainMenuExchangeCourse.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.mainMenuExchangeCourse.Name = "mainMenuExchangeCourse";
            this.mainMenuExchangeCourse.Size = new System.Drawing.Size(124, 22);
            this.mainMenuExchangeCourse.Text = "切换科目";
            // 
            // mainMenuSeparator1
            // 
            this.mainMenuSeparator1.Name = "mainMenuSeparator1";
            this.mainMenuSeparator1.Size = new System.Drawing.Size(121, 6);
            // 
            // mainMenuUpdate
            // 
            this.mainMenuUpdate.Image = global::KST.Properties.Resources.main_menu_update;
            this.mainMenuUpdate.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.mainMenuUpdate.Name = "mainMenuUpdate";
            this.mainMenuUpdate.Size = new System.Drawing.Size(124, 22);
            this.mainMenuUpdate.Text = "检测更新";
            // 
            // mainMenuFeedback
            // 
            this.mainMenuFeedback.Image = global::KST.Properties.Resources.main_menu_message;
            this.mainMenuFeedback.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.mainMenuFeedback.Name = "mainMenuFeedback";
            this.mainMenuFeedback.Size = new System.Drawing.Size(124, 22);
            this.mainMenuFeedback.Text = "意见反馈";
            // 
            // mainMenuWebsite
            // 
            this.mainMenuWebsite.Image = global::KST.Properties.Resources.main_menu_home;
            this.mainMenuWebsite.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.mainMenuWebsite.Name = "mainMenuWebsite";
            this.mainMenuWebsite.Size = new System.Drawing.Size(124, 22);
            this.mainMenuWebsite.Text = "官方网站";
            // 
            // mainMenuAboutUs
            // 
            this.mainMenuAboutUs.Image = global::KST.Properties.Resources.main_menu_about_us;
            this.mainMenuAboutUs.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.mainMenuAboutUs.Name = "mainMenuAboutUs";
            this.mainMenuAboutUs.Size = new System.Drawing.Size(124, 22);
            this.mainMenuAboutUs.Text = "关于我们";
            // 
            // mainMenuSeparator2
            // 
            this.mainMenuSeparator2.Name = "mainMenuSeparator2";
            this.mainMenuSeparator2.Size = new System.Drawing.Size(121, 6);
            // 
            // mainMenuExit
            // 
            this.mainMenuExit.Image = global::KST.Properties.Resources.main_menu_sign_out;
            this.mainMenuExit.Name = "mainMenuExit";
            this.mainMenuExit.Size = new System.Drawing.Size(124, 22);
            this.mainMenuExit.Text = "退出系统";
            // 
            // moreToolsWorkPanel
            // 
            this.moreToolsWorkPanel.BackColor = System.Drawing.Color.White;
            this.moreToolsWorkPanel.Location = new System.Drawing.Point(363, 210);
            this.moreToolsWorkPanel.Name = "moreToolsWorkPanel";
            this.moreToolsWorkPanel.Size = new System.Drawing.Size(95, 62);
            this.moreToolsWorkPanel.TabIndex = 8;
            // 
            // myFavoriteWorkPanel
            // 
            this.myFavoriteWorkPanel.BackColor = System.Drawing.Color.White;
            this.myFavoriteWorkPanel.Location = new System.Drawing.Point(246, 210);
            this.myFavoriteWorkPanel.Name = "myFavoriteWorkPanel";
            this.myFavoriteWorkPanel.Size = new System.Drawing.Size(95, 62);
            this.myFavoriteWorkPanel.TabIndex = 7;
            // 
            // myWrongWorkPanel
            // 
            this.myWrongWorkPanel.BackColor = System.Drawing.Color.White;
            this.myWrongWorkPanel.Location = new System.Drawing.Point(129, 210);
            this.myWrongWorkPanel.Name = "myWrongWorkPanel";
            this.myWrongWorkPanel.Size = new System.Drawing.Size(95, 62);
            this.myWrongWorkPanel.TabIndex = 6;
            // 
            // previousPapersWorkPanel
            // 
            this.previousPapersWorkPanel.BackColor = System.Drawing.Color.White;
            this.previousPapersWorkPanel.Location = new System.Drawing.Point(12, 210);
            this.previousPapersWorkPanel.Name = "previousPapersWorkPanel";
            this.previousPapersWorkPanel.Size = new System.Drawing.Size(95, 62);
            this.previousPapersWorkPanel.TabIndex = 5;
            // 
            // vipPapersWorkPanel
            // 
            this.vipPapersWorkPanel.BackColor = System.Drawing.Color.White;
            this.vipPapersWorkPanel.Location = new System.Drawing.Point(363, 122);
            this.vipPapersWorkPanel.Name = "vipPapersWorkPanel";
            this.vipPapersWorkPanel.Size = new System.Drawing.Size(95, 62);
            this.vipPapersWorkPanel.TabIndex = 4;
            // 
            // examSimulationWorkPanel
            // 
            this.examSimulationWorkPanel.BackColor = System.Drawing.Color.White;
            this.examSimulationWorkPanel.Location = new System.Drawing.Point(248, 122);
            this.examSimulationWorkPanel.Name = "examSimulationWorkPanel";
            this.examSimulationWorkPanel.Size = new System.Drawing.Size(95, 62);
            this.examSimulationWorkPanel.TabIndex = 3;
            // 
            // practiceWorkPanel
            // 
            this.practiceWorkPanel.BackColor = System.Drawing.Color.White;
            this.practiceWorkPanel.Location = new System.Drawing.Point(136, 122);
            this.practiceWorkPanel.Name = "practiceWorkPanel";
            this.practiceWorkPanel.Size = new System.Drawing.Size(95, 62);
            this.practiceWorkPanel.TabIndex = 2;
            // 
            // myHomeWorkPanel
            // 
            this.myHomeWorkPanel.BackColor = System.Drawing.Color.White;
            this.myHomeWorkPanel.Location = new System.Drawing.Point(12, 122);
            this.myHomeWorkPanel.Name = "myHomeWorkPanel";
            this.myHomeWorkPanel.Size = new System.Drawing.Size(95, 62);
            this.myHomeWorkPanel.TabIndex = 1;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::KST.Properties.Resources.frame_bkg;
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.moreToolsWorkPanel);
            this.Controls.Add(this.myFavoriteWorkPanel);
            this.Controls.Add(this.myWrongWorkPanel);
            this.Controls.Add(this.previousPapersWorkPanel);
            this.Controls.Add(this.vipPapersWorkPanel);
            this.Controls.Add(this.examSimulationWorkPanel);
            this.Controls.Add(this.practiceWorkPanel);
            this.Controls.Add(this.myHomeWorkPanel);
            this.Controls.Add(this.headerPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsCanResize = false;
            this.IsDrawIcon = false;
            this.IsShowMaxOrRestoreButton = false;
            this.IsShowMenuButton = true;
            this.IsShowSkinButton = true;
            this.MaximumSize = new System.Drawing.Size(1366, 728);
            this.MinimumSize = new System.Drawing.Size(300, 200);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "考试通";
            this.TextFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mainMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private NuiHeaderPanel headerPanel;
        private ControlEx.MyHomeWorkPanel myHomeWorkPanel;
        private ControlEx.PracticeWorkPanel practiceWorkPanel;
        private ControlEx.ExamSimulationWorkPanel examSimulationWorkPanel;
        private ControlEx.VipPapersWorkPanel vipPapersWorkPanel;
        private ControlEx.PreviousPapersWorkPanel previousPapersWorkPanel;
        private ControlEx.MyWrongWorkPanel myWrongWorkPanel;
        private ControlEx.MyFavoriteWorkPanel myFavoriteWorkPanel;
        private ControlEx.MoreToolsWorkPanel moreToolsWorkPanel;
        private System.Windows.Forms.ContextMenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem mainMenuSetting;
        private System.Windows.Forms.ToolStripMenuItem mainMenuExchangeCourse;
        private System.Windows.Forms.ToolStripMenuItem mainMenuUpdate;
        private System.Windows.Forms.ToolStripSeparator mainMenuSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mainMenuFeedback;
        private System.Windows.Forms.ToolStripMenuItem mainMenuWebsite;
        private System.Windows.Forms.ToolStripMenuItem mainMenuAboutUs;
        private System.Windows.Forms.ToolStripSeparator mainMenuSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mainMenuExit;



    }
}