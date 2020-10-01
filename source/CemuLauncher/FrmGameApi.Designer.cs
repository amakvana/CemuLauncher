namespace CemuLauncher
{
    partial class FrmGameApi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmGameApi));
            this.label1 = new System.Windows.Forms.Label();
            this.cboGameApi = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Graphics API:";
            // 
            // cboGameApi
            // 
            this.cboGameApi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGameApi.FormattingEnabled = true;
            this.cboGameApi.Items.AddRange(new object[] {
            "OpenGL",
            "Vulkan"});
            this.cboGameApi.Location = new System.Drawing.Point(90, 12);
            this.cboGameApi.Name = "cboGameApi";
            this.cboGameApi.Size = new System.Drawing.Size(133, 21);
            this.cboGameApi.TabIndex = 1;
            // 
            // FrmGameApi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(235, 49);
            this.Controls.Add(this.cboGameApi);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmGameApi";
            this.Text = "Cemu Launcher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmGameApi_FormClosing);
            this.Load += new System.EventHandler(this.FrmGameApi_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboGameApi;
    }
}