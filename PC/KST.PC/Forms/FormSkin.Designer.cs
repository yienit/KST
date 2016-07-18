namespace KST
{
    partial class FormSkin
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
            this.panelWaiting = new System.Windows.Forms.Panel();
            this.panelWaitImageAndText = new System.Windows.Forms.Panel();
            this.lblWaiting = new System.Windows.Forms.Label();
            this.picboxWaiting = new System.Windows.Forms.PictureBox();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.lblSkinInitTip = new System.Windows.Forms.Label();
            this.panelWaiting.SuspendLayout();
            this.panelWaitImageAndText.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picboxWaiting)).BeginInit();
            this.panelContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelWaiting
            // 
            this.panelWaiting.AutoScroll = true;
            this.panelWaiting.BackgroundImage = global::KST.Properties.Resources.skin_form_bkg;
            this.panelWaiting.Controls.Add(this.panelWaitImageAndText);
            this.panelWaiting.Location = new System.Drawing.Point(219, 94);
            this.panelWaiting.Name = "panelWaiting";
            this.panelWaiting.Size = new System.Drawing.Size(242, 71);
            this.panelWaiting.TabIndex = 0;
            // 
            // panelWaitImageAndText
            // 
            this.panelWaitImageAndText.BackColor = System.Drawing.Color.Transparent;
            this.panelWaitImageAndText.Controls.Add(this.lblWaiting);
            this.panelWaitImageAndText.Controls.Add(this.picboxWaiting);
            this.panelWaitImageAndText.Location = new System.Drawing.Point(12, 12);
            this.panelWaitImageAndText.Name = "panelWaitImageAndText";
            this.panelWaitImageAndText.Size = new System.Drawing.Size(205, 53);
            this.panelWaitImageAndText.TabIndex = 1;
            // 
            // lblWaiting
            // 
            this.lblWaiting.AutoSize = true;
            this.lblWaiting.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblWaiting.Location = new System.Drawing.Point(53, 19);
            this.lblWaiting.Name = "lblWaiting";
            this.lblWaiting.Size = new System.Drawing.Size(146, 17);
            this.lblWaiting.TabIndex = 2;
            this.lblWaiting.Text = "正在加载皮肤，请稍等......";
            // 
            // picboxWaiting
            // 
            this.picboxWaiting.BackColor = System.Drawing.Color.Transparent;
            this.picboxWaiting.Image = global::KST.Properties.Resources.waiting;
            this.picboxWaiting.Location = new System.Drawing.Point(9, 12);
            this.picboxWaiting.Name = "picboxWaiting";
            this.picboxWaiting.Size = new System.Drawing.Size(30, 30);
            this.picboxWaiting.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picboxWaiting.TabIndex = 0;
            this.picboxWaiting.TabStop = false;
            // 
            // panelContainer
            // 
            this.panelContainer.AutoScroll = true;
            this.panelContainer.BackgroundImage = global::KST.Properties.Resources.skin_form_bkg;
            this.panelContainer.Controls.Add(this.lblSkinInitTip);
            this.panelContainer.Location = new System.Drawing.Point(219, 208);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(242, 71);
            this.panelContainer.TabIndex = 1;
            this.panelContainer.Visible = false;
            // 
            // lblSkinInitTip
            // 
            this.lblSkinInitTip.AutoSize = true;
            this.lblSkinInitTip.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblSkinInitTip.Location = new System.Drawing.Point(76, 29);
            this.lblSkinInitTip.Name = "lblSkinInitTip";
            this.lblSkinInitTip.Size = new System.Drawing.Size(80, 17);
            this.lblSkinInitTip.TabIndex = 3;
            this.lblSkinInitTip.Text = "暂无可选皮肤";
            this.lblSkinInitTip.Visible = false;
            // 
            // FormSkin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::KST.Properties.Resources.frame_bkg;
            this.ClientSize = new System.Drawing.Size(680, 552);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.panelWaiting);
            this.IsDrawIcon = false;
            this.IsShowMaxOrRestoreButton = false;
            this.MaximumSize = new System.Drawing.Size(1366, 728);
            this.Name = "FormSkin";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "皮肤中心";
            this.panelWaiting.ResumeLayout(false);
            this.panelWaitImageAndText.ResumeLayout(false);
            this.panelWaitImageAndText.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picboxWaiting)).EndInit();
            this.panelContainer.ResumeLayout(false);
            this.panelContainer.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelWaiting;
        private System.Windows.Forms.PictureBox picboxWaiting;
        private System.Windows.Forms.Panel panelWaitImageAndText;
        private System.Windows.Forms.Label lblWaiting;
        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Label lblSkinInitTip;
    }
}