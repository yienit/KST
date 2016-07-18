namespace KST
{
    partial class FormAbout
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
            this.panelContainer = new System.Windows.Forms.Panel();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblVersionTip = new System.Windows.Forms.Label();
            this.picboxSlogan = new System.Windows.Forms.PictureBox();
            this.picboxLogo = new System.Windows.Forms.PictureBox();
            this.panelContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picboxSlogan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.Controls.Add(this.lblCopyright);
            this.panelContainer.Controls.Add(this.lblVersion);
            this.panelContainer.Controls.Add(this.lblVersionTip);
            this.panelContainer.Controls.Add(this.picboxSlogan);
            this.panelContainer.Controls.Add(this.picboxLogo);
            this.panelContainer.Location = new System.Drawing.Point(0, 30);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(519, 220);
            this.panelContainer.TabIndex = 0;
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Font = new System.Drawing.Font("微软雅黑", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblCopyright.ForeColor = System.Drawing.Color.Gray;
            this.lblCopyright.Location = new System.Drawing.Point(32, 147);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(258, 16);
            this.lblCopyright.TabIndex = 6;
            this.lblCopyright.Text = "Copyright(c) 51kaoshitong.com. All Rights Reserved.";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblVersion.Location = new System.Drawing.Point(108, 120);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(25, 17);
            this.lblVersion.TabIndex = 5;
            this.lblVersion.Text = "2.1";
            // 
            // lblVersionTip
            // 
            this.lblVersionTip.AutoSize = true;
            this.lblVersionTip.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblVersionTip.Location = new System.Drawing.Point(32, 120);
            this.lblVersionTip.Name = "lblVersionTip";
            this.lblVersionTip.Size = new System.Drawing.Size(80, 17);
            this.lblVersionTip.TabIndex = 2;
            this.lblVersionTip.Text = "主程序版本：";
            // 
            // picboxSlogan
            // 
            this.picboxSlogan.Image = global::KST.Properties.Resources.slogan;
            this.picboxSlogan.Location = new System.Drawing.Point(25, 37);
            this.picboxSlogan.Name = "picboxSlogan";
            this.picboxSlogan.Size = new System.Drawing.Size(180, 66);
            this.picboxSlogan.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picboxSlogan.TabIndex = 1;
            this.picboxSlogan.TabStop = false;
            // 
            // picboxLogo
            // 
            this.picboxLogo.Image = global::KST.Properties.Resources.png_logo_128;
            this.picboxLogo.Location = new System.Drawing.Point(348, 33);
            this.picboxLogo.Name = "picboxLogo";
            this.picboxLogo.Size = new System.Drawing.Size(128, 128);
            this.picboxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picboxLogo.TabIndex = 0;
            this.picboxLogo.TabStop = false;
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::KST.Properties.Resources.frame_bkg;
            this.ClientSize = new System.Drawing.Size(520, 251);
            this.Controls.Add(this.panelContainer);
            this.IsCanResize = false;
            this.IsDrawIcon = false;
            this.IsShowMaxOrRestoreButton = false;
            this.IsShowMinButton = false;
            this.MaximumSize = new System.Drawing.Size(1366, 728);
            this.Name = "FormAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "关于我们";
            this.TextFont = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelContainer.ResumeLayout(false);
            this.panelContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picboxSlogan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picboxLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.PictureBox picboxLogo;
        private System.Windows.Forms.PictureBox picboxSlogan;
        private System.Windows.Forms.Label lblVersionTip;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblCopyright;
    }
}