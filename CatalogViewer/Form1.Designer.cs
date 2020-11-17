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
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.num_ImageFullSize = new System.Windows.Forms.NumericUpDown();
            this.lbl_CallMessage = new System.Windows.Forms.Label();
            this.lbl_CallingFlag = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnl_Footer = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.radImageEditor1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radListView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTreeView1)).BeginInit();
            this.pnl_ArticleCall.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_ImageFullSize)).BeginInit();
            this.pnl_Footer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Article Number:";
            // 
            // tbx_ArticleNum
            // 
            this.tbx_ArticleNum.Location = new System.Drawing.Point(84, 37);
            this.tbx_ArticleNum.Name = "tbx_ArticleNum";
            this.tbx_ArticleNum.Size = new System.Drawing.Size(130, 20);
            this.tbx_ArticleNum.TabIndex = 2;
            this.tbx_ArticleNum.Text = "CRW69011Z4";
            // 
            // btn_CallService
            // 
            this.btn_CallService.BackColor = System.Drawing.SystemColors.Control;
            this.btn_CallService.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_CallService.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_CallService.Location = new System.Drawing.Point(219, 36);
            this.btn_CallService.Name = "btn_CallService";
            this.btn_CallService.Size = new System.Drawing.Size(117, 23);
            this.btn_CallService.TabIndex = 3;
            this.btn_CallService.Text = "Call the Service";
            this.btn_CallService.UseVisualStyleBackColor = false;
            this.btn_CallService.Click += new System.EventHandler(this.btn_CallService_Click);
            // 
            // radImageEditor1
            // 
            this.radImageEditor1.AllowDrop = true;
            this.radImageEditor1.BackColor = System.Drawing.SystemColors.Control;
            this.radImageEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radImageEditor1.Location = new System.Drawing.Point(189, 66);
            this.radImageEditor1.Name = "radImageEditor1";
            // 
            // 
            // 
            this.radImageEditor1.RootElement.AutoSize = false;
            this.radImageEditor1.RootElement.ScaleTransform = new System.Drawing.SizeF(1F, 1F);
            this.radImageEditor1.Size = new System.Drawing.Size(562, 416);
            this.radImageEditor1.TabIndex = 5;
            this.radImageEditor1.Text = "radImageEditor1";
            this.radImageEditor1.ImageLoaded += new System.ComponentModel.AsyncCompletedEventHandler(this.RadImageEditor1_ImageLoaded);
            ((Telerik.WinControls.UI.RadImageEditorElement)(this.radImageEditor1.GetChildAt(0))).BackColor = System.Drawing.Color.White;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radImageEditor1.GetChildAt(0).GetChildAt(0).GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.White;
            ((Telerik.WinControls.UI.ImageEditorZoomElement)(this.radImageEditor1.GetChildAt(0).GetChildAt(2))).Enabled = true;
            ((Telerik.WinControls.UI.RadTrackBarElement)(this.radImageEditor1.GetChildAt(0).GetChildAt(2).GetChildAt(0))).GradientStyle = Telerik.WinControls.GradientStyles.Solid;
            ((Telerik.WinControls.UI.RadTrackBarElement)(this.radImageEditor1.GetChildAt(0).GetChildAt(2).GetChildAt(0))).BackColor = System.Drawing.Color.Transparent;
            ((Telerik.WinControls.Primitives.FillPrimitive)(this.radImageEditor1.GetChildAt(0).GetChildAt(2).GetChildAt(0).GetChildAt(0).GetChildAt(0))).BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(241)))), ((int)(((byte)(252)))));
            // 
            // radListView1
            // 
            this.radListView1.AllowArbitraryItemHeight = true;
            this.radListView1.AllowArbitraryItemWidth = true;
            this.radListView1.AllowColumnReorder = false;
            this.radListView1.AllowColumnResize = false;
            this.radListView1.AllowEdit = false;
            this.radListView1.AllowRemove = false;
            this.radListView1.Dock = System.Windows.Forms.DockStyle.Left;
            this.radListView1.FullRowSelect = false;
            this.radListView1.ItemSize = new System.Drawing.Size(100, 200);
            this.radListView1.Location = new System.Drawing.Point(0, 66);
            this.radListView1.Name = "radListView1";
            this.radListView1.ShowColumnHeaders = false;
            this.radListView1.Size = new System.Drawing.Size(189, 416);
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
            this.radTreeView1.Location = new System.Drawing.Point(751, 66);
            this.radTreeView1.Name = "radTreeView1";
            this.radTreeView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radTreeView1.Size = new System.Drawing.Size(198, 416);
            this.radTreeView1.SpacingBetweenNodes = -1;
            this.radTreeView1.TabIndex = 9;
            // 
            // pnl_ArticleCall
            // 
            this.pnl_ArticleCall.Controls.Add(this.label5);
            this.pnl_ArticleCall.Controls.Add(this.label4);
            this.pnl_ArticleCall.Controls.Add(this.num_ImageFullSize);
            this.pnl_ArticleCall.Controls.Add(this.lbl_CallMessage);
            this.pnl_ArticleCall.Controls.Add(this.lbl_CallingFlag);
            this.pnl_ArticleCall.Controls.Add(this.label3);
            this.pnl_ArticleCall.Controls.Add(this.btn_CallService);
            this.pnl_ArticleCall.Controls.Add(this.label1);
            this.pnl_ArticleCall.Controls.Add(this.tbx_ArticleNum);
            this.pnl_ArticleCall.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_ArticleCall.Location = new System.Drawing.Point(0, 0);
            this.pnl_ArticleCall.Name = "pnl_ArticleCall";
            this.pnl_ArticleCall.Size = new System.Drawing.Size(949, 66);
            this.pnl_ArticleCall.TabIndex = 11;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(512, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "pixels";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(342, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Image Width (0=Off):";
            // 
            // num_ImageFullSize
            // 
            this.num_ImageFullSize.Increment = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.num_ImageFullSize.Location = new System.Drawing.Point(448, 37);
            this.num_ImageFullSize.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.num_ImageFullSize.Name = "num_ImageFullSize";
            this.num_ImageFullSize.Size = new System.Drawing.Size(61, 20);
            this.num_ImageFullSize.TabIndex = 8;
            // 
            // lbl_CallMessage
            // 
            this.lbl_CallMessage.AutoSize = true;
            this.lbl_CallMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CallMessage.Location = new System.Drawing.Point(552, 40);
            this.lbl_CallMessage.Name = "lbl_CallMessage";
            this.lbl_CallMessage.Size = new System.Drawing.Size(0, 13);
            this.lbl_CallMessage.TabIndex = 7;
            // 
            // lbl_CallingFlag
            // 
            this.lbl_CallingFlag.AutoSize = true;
            this.lbl_CallingFlag.BackColor = System.Drawing.Color.Brown;
            this.lbl_CallingFlag.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbl_CallingFlag.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CallingFlag.ForeColor = System.Drawing.Color.Yellow;
            this.lbl_CallingFlag.Location = new System.Drawing.Point(390, 2);
            this.lbl_CallingFlag.Name = "lbl_CallingFlag";
            this.lbl_CallingFlag.Size = new System.Drawing.Size(191, 24);
            this.lbl_CallingFlag.TabIndex = 6;
            this.lbl_CallingFlag.Text = " Calling the Service";
            this.lbl_CallingFlag.Visible = false;
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
            this.label3.Size = new System.Drawing.Size(398, 26);
            this.label3.TabIndex = 5;
            this.label3.Text = "Richemont Product Catalog Viewer  ";
            // 
            // pnl_Footer
            // 
            this.pnl_Footer.Controls.Add(this.checkBox1);
            this.pnl_Footer.Controls.Add(this.btn_Exit);
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
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
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
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.radImageEditor1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radListView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTreeView1)).EndInit();
            this.pnl_ArticleCall.ResumeLayout(false);
            this.pnl_ArticleCall.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_ImageFullSize)).EndInit();
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_CallingFlag;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label lbl_CallMessage;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown num_ImageFullSize;
    }
}

