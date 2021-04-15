
namespace CatalogViewer
{
    partial class APIDataViewer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(APIDataViewer));
            this.label1 = new System.Windows.Forms.Label();
            this.tbx_URL = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbx_Data = new System.Windows.Forms.TextBox();
            this.btn_Sunmit = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "URL:";
            // 
            // tbx_URL
            // 
            this.tbx_URL.Location = new System.Drawing.Point(50, 37);
            this.tbx_URL.Name = "tbx_URL";
            this.tbx_URL.Size = new System.Drawing.Size(847, 20);
            this.tbx_URL.TabIndex = 1;
            this.tbx_URL.Text = resources.GetString("tbx_URL.Text");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(323, 26);
            this.label3.TabIndex = 6;
            this.label3.Text = "Richemont API Data Browser";
            // 
            // tbx_Data
            // 
            this.tbx_Data.Location = new System.Drawing.Point(50, 88);
            this.tbx_Data.Multiline = true;
            this.tbx_Data.Name = "tbx_Data";
            this.tbx_Data.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbx_Data.Size = new System.Drawing.Size(847, 542);
            this.tbx_Data.TabIndex = 7;
            // 
            // btn_Sunmit
            // 
            this.btn_Sunmit.Location = new System.Drawing.Point(903, 37);
            this.btn_Sunmit.Name = "btn_Sunmit";
            this.btn_Sunmit.Size = new System.Drawing.Size(75, 23);
            this.btn_Sunmit.TabIndex = 8;
            this.btn_Sunmit.Text = "Submit";
            this.btn_Sunmit.UseVisualStyleBackColor = true;
            this.btn_Sunmit.Click += new System.EventHandler(this.btn_Sunmit_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(47, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "JSON Data";
            // 
            // APIDataViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 642);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btn_Sunmit);
            this.Controls.Add(this.tbx_Data);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbx_URL);
            this.Controls.Add(this.label1);
            this.Name = "APIDataViewer";
            this.Text = "APIDataViewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbx_URL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbx_Data;
        private System.Windows.Forms.Button btn_Sunmit;
        private System.Windows.Forms.Label label4;
    }
}