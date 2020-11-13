namespace CatalogViewer
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.tbx_ArticleNum = new System.Windows.Forms.TextBox();
            this.btn_CallService = new System.Windows.Forms.Button();
            this.radImageEditor1 = new Telerik.WinControls.UI.RadImageEditor();
            this.radListView1 = new Telerik.WinControls.UI.RadListView();
            this.radTreeView1 = new Telerik.WinControls.UI.RadTreeView();
            this.radThemeManager1 = new Telerik.WinControls.RadThemeManager();
            this.pnl_ArticleCall = new System.Windows.Forms.Panel();
            this.lbl_Working = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.pnl_Footer = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.radImageEditor1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radListView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTreeView1)).BeginInit();
            this.pnl_ArticleCall.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.pnl_Footer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Article Number:";
            // 
            // tbx_ArticleNum
            // 
            this.tbx_ArticleNum.Location = new System.Drawing.Point(84, 6);
            this.tbx_ArticleNum.Name = "tbx_ArticleNum";
            this.tbx_ArticleNum.Size = new System.Drawing.Size(131, 20);
            this.tbx_ArticleNum.TabIndex = 2;
            this.tbx_ArticleNum.Text = "CRW69011Z4";
            // 
            // btn_CallService
            // 
            this.btn_CallService.Location = new System.Drawing.Point(220, 4);
            this.btn_CallService.Name = "btn_CallService";
            this.btn_CallService.Size = new System.Drawing.Size(98, 23);
            this.btn_CallService.TabIndex = 3;
            this.btn_CallService.Text = "Call the Service";
            this.btn_CallService.UseVisualStyleBackColor = true;
            this.btn_CallService.Click += new System.EventHandler(this.btn_CallService_Click);
            // 
            // radImageEditor1
            // 
            this.radImageEditor1.AllowDrop = true;
            this.radImageEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radImageEditor1.Enabled = false;
            this.radImageEditor1.Location = new System.Drawing.Point(150, 34);
            this.radImageEditor1.Name = "radImageEditor1";
            // 
            // 
            // 
            this.radImageEditor1.RootElement.AutoSize = false;
            this.radImageEditor1.RootElement.ScaleTransform = new System.Drawing.SizeF(1F, 1F);
            this.radImageEditor1.Size = new System.Drawing.Size(601, 448);
            this.radImageEditor1.TabIndex = 5;
            this.radImageEditor1.Text = "radImageEditor1";
            this.radImageEditor1.ThemeName = "ControlDefault";
            // 
            // radListView1
            // 
            this.radListView1.AllowColumnReorder = false;
            this.radListView1.AllowColumnResize = false;
            this.radListView1.AllowEdit = false;
            this.radListView1.AllowRemove = false;
            this.radListView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.radListView1.FullRowSelect = false;
            this.radListView1.ItemSize = new System.Drawing.Size(150, 200);
            this.radListView1.Location = new System.Drawing.Point(0, 34);
            this.radListView1.Name = "radListView1";
            this.radListView1.ShowColumnHeaders = false;
            this.radListView1.Size = new System.Drawing.Size(150, 448);
            this.radListView1.TabIndex = 8;
            this.radListView1.ViewType = Telerik.WinControls.UI.ListViewType.IconsView;
            this.radListView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.radListView1_MouseDoubleClick);
            // 
            // radTreeView1
            // 
            this.radTreeView1.BackColor = System.Drawing.SystemColors.Control;
            this.radTreeView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.radTreeView1.Dock = System.Windows.Forms.DockStyle.Right;
            this.radTreeView1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.radTreeView1.ForeColor = System.Drawing.Color.Black;
            this.radTreeView1.Location = new System.Drawing.Point(751, 34);
            this.radTreeView1.Name = "radTreeView1";
            this.radTreeView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radTreeView1.Size = new System.Drawing.Size(198, 448);
            this.radTreeView1.SpacingBetweenNodes = -1;
            this.radTreeView1.TabIndex = 9;
            // 
            // pnl_ArticleCall
            // 
            this.pnl_ArticleCall.Controls.Add(this.lbl_Working);
            this.pnl_ArticleCall.Controls.Add(this.label3);
            this.pnl_ArticleCall.Controls.Add(this.btn_CallService);
            this.pnl_ArticleCall.Controls.Add(this.label1);
            this.pnl_ArticleCall.Controls.Add(this.tbx_ArticleNum);
            this.pnl_ArticleCall.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_ArticleCall.Location = new System.Drawing.Point(0, 0);
            this.pnl_ArticleCall.Name = "pnl_ArticleCall";
            this.pnl_ArticleCall.Size = new System.Drawing.Size(949, 34);
            this.pnl_ArticleCall.TabIndex = 11;
            // 
            // lbl_Working
            // 
            this.lbl_Working.AutoSize = true;
            this.lbl_Working.BackColor = System.Drawing.Color.Brown;
            this.lbl_Working.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbl_Working.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Working.ForeColor = System.Drawing.Color.Yellow;
            this.lbl_Working.Location = new System.Drawing.Point(416, 3);
            this.lbl_Working.Name = "lbl_Working";
            this.lbl_Working.Size = new System.Drawing.Size(119, 24);
            this.lbl_Working.TabIndex = 6;
            this.lbl_Working.Text = " WORKING ";
            this.lbl_Working.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Right;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label3.Location = new System.Drawing.Point(551, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(398, 26);
            this.label3.TabIndex = 5;
            this.label3.Text = "Richemont Product Catalog Viewer  ";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(567, 6);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(51, 20);
            this.numericUpDown1.TabIndex = 4;
            // 
            // pnl_Footer
            // 
            this.pnl_Footer.Controls.Add(this.checkBox1);
            this.pnl_Footer.Controls.Add(this.btn_Exit);
            this.pnl_Footer.Controls.Add(this.numericUpDown1);
            this.pnl_Footer.Controls.Add(this.label2);
            this.pnl_Footer.Controls.Add(this.pictureBox1);
            this.pnl_Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_Footer.Location = new System.Drawing.Point(0, 482);
            this.pnl_Footer.Name = "pnl_Footer";
            this.pnl_Footer.Size = new System.Drawing.Size(949, 35);
            this.pnl_Footer.TabIndex = 15;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(636, 10);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(115, 17);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Show Image Menu";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // btn_Exit
            // 
            this.btn_Exit.Dock = System.Windows.Forms.DockStyle.Right;
            this.btn_Exit.Location = new System.Drawing.Point(874, 0);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(75, 35);
            this.btn_Exit.TabIndex = 2;
            this.btn_Exit.Text = "Exit";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(321, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Copyright 2020 Richemont";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pictureBox1.Image = global::CatalogViewer.Properties.Resources.richemontlogo_aim;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(318, 35);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 517);
            this.Controls.Add(this.radImageEditor1);
            this.Controls.Add(this.radTreeView1);
            this.Controls.Add(this.radListView1);
            this.Controls.Add(this.pnl_ArticleCall);
            this.Controls.Add(this.pnl_Footer);
            this.Name = "Form1";
            this.Text = "Richemont Product Catalog Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.radImageEditor1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radListView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTreeView1)).EndInit();
            this.pnl_ArticleCall.ResumeLayout(false);
            this.pnl_ArticleCall.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.pnl_Footer.ResumeLayout(false);
            this.pnl_Footer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbx_ArticleNum;
        private System.Windows.Forms.Button btn_CallService;
        private Telerik.WinControls.UI.RadImageEditor radImageEditor1;
        private Telerik.WinControls.UI.RadListView radListView1;
        private Telerik.WinControls.UI.RadTreeView radTreeView1;
        private Telerik.WinControls.RadThemeManager radThemeManager1;
        private System.Windows.Forms.Panel pnl_ArticleCall;
        private System.Windows.Forms.Panel pnl_Footer;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_Working;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

