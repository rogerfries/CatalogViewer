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
            this.radImageEditor1 = new Telerik.WinControls.UI.RadImageEditor();
            this.radListView1 = new Telerik.WinControls.UI.RadListView();
            this.radTreeView1 = new Telerik.WinControls.UI.RadTreeView();
            this.radThemeManager1 = new Telerik.WinControls.RadThemeManager();
            this.pnl_ArticleCall = new System.Windows.Forms.Panel();
            this.btn_CallService = new System.Windows.Forms.Button();
            this.cbx_Channel = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pb_BrandLogo = new System.Windows.Forms.PictureBox();
            this.tbx_CallMessage = new System.Windows.Forms.TextBox();
            this.pb_Download = new System.Windows.Forms.ProgressBar();
            this.lbl_ReadyIndicator = new System.Windows.Forms.Label();
            this.btn_Abort = new System.Windows.Forms.Button();
            this.cbx_ArticleNum = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.num_ImageFullSize = new System.Windows.Forms.NumericUpDown();
            this.lbl_CallingFlag = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnl_Footer = new System.Windows.Forms.Panel();
            this.lbl_AppVersion = new System.Windows.Forms.Label();
            this.lbl_Timings = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.radImageEditor1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radListView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTreeView1)).BeginInit();
            this.pnl_ArticleCall.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_BrandLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_ImageFullSize)).BeginInit();
            this.pnl_Footer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Article Number:";
            // 
            // radImageEditor1
            // 
            this.radImageEditor1.AllowDrop = true;
            this.radImageEditor1.BackColor = System.Drawing.SystemColors.Control;
            this.radImageEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radImageEditor1.Location = new System.Drawing.Point(189, 94);
            this.radImageEditor1.Name = "radImageEditor1";
            // 
            // 
            // 
            this.radImageEditor1.RootElement.AutoSize = false;
            this.radImageEditor1.RootElement.ScaleTransform = new System.Drawing.SizeF(1F, 1F);
            this.radImageEditor1.Size = new System.Drawing.Size(562, 419);
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
            this.radListView1.Location = new System.Drawing.Point(0, 94);
            this.radListView1.Name = "radListView1";
            this.radListView1.SelectLastAddedItem = false;
            this.radListView1.ShowColumnHeaders = false;
            this.radListView1.Size = new System.Drawing.Size(189, 419);
            this.radListView1.TabIndex = 8;
            this.radListView1.ViewType = Telerik.WinControls.UI.ListViewType.IconsView;
            this.radListView1.SelectedItemChanged += new System.EventHandler(this.radListView1_SelectedItemChanged);
            // 
            // radTreeView1
            // 
            this.radTreeView1.BackColor = System.Drawing.SystemColors.Control;
            this.radTreeView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.radTreeView1.Dock = System.Windows.Forms.DockStyle.Right;
            this.radTreeView1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.radTreeView1.ForeColor = System.Drawing.Color.Black;
            this.radTreeView1.Location = new System.Drawing.Point(751, 94);
            this.radTreeView1.Name = "radTreeView1";
            this.radTreeView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.radTreeView1.Size = new System.Drawing.Size(198, 419);
            this.radTreeView1.SpacingBetweenNodes = -1;
            this.radTreeView1.TabIndex = 9;
            // 
            // pnl_ArticleCall
            // 
            this.pnl_ArticleCall.Controls.Add(this.btn_CallService);
            this.pnl_ArticleCall.Controls.Add(this.cbx_Channel);
            this.pnl_ArticleCall.Controls.Add(this.label6);
            this.pnl_ArticleCall.Controls.Add(this.panel1);
            this.pnl_ArticleCall.Controls.Add(this.btn_Abort);
            this.pnl_ArticleCall.Controls.Add(this.cbx_ArticleNum);
            this.pnl_ArticleCall.Controls.Add(this.label5);
            this.pnl_ArticleCall.Controls.Add(this.label4);
            this.pnl_ArticleCall.Controls.Add(this.num_ImageFullSize);
            this.pnl_ArticleCall.Controls.Add(this.lbl_CallingFlag);
            this.pnl_ArticleCall.Controls.Add(this.label3);
            this.pnl_ArticleCall.Controls.Add(this.label1);
            this.pnl_ArticleCall.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_ArticleCall.Location = new System.Drawing.Point(0, 0);
            this.pnl_ArticleCall.Name = "pnl_ArticleCall";
            this.pnl_ArticleCall.Size = new System.Drawing.Size(949, 94);
            this.pnl_ArticleCall.TabIndex = 11;
            // 
            // btn_CallService
            // 
            this.btn_CallService.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_CallService.ForeColor = System.Drawing.Color.Green;
            this.btn_CallService.Location = new System.Drawing.Point(213, 35);
            this.btn_CallService.Name = "btn_CallService";
            this.btn_CallService.Size = new System.Drawing.Size(93, 45);
            this.btn_CallService.TabIndex = 18;
            this.btn_CallService.Text = "Call the Service";
            this.btn_CallService.UseVisualStyleBackColor = true;
            this.btn_CallService.Click += new System.EventHandler(this.btn_CallService_Click);
            // 
            // cbx_Channel
            // 
            this.cbx_Channel.FormattingEnabled = true;
            this.cbx_Channel.Items.AddRange(new object[] {
            "IPADRETAIL",
            "WEB"});
            this.cbx_Channel.Location = new System.Drawing.Point(96, 58);
            this.cbx_Channel.Name = "cbx_Channel";
            this.cbx_Channel.Size = new System.Drawing.Size(107, 21);
            this.cbx_Channel.TabIndex = 17;
            this.cbx_Channel.Text = "IPADRETAIL";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(46, 61);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 16;
            this.label6.Text = "Channel:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pb_BrandLogo);
            this.panel1.Controls.Add(this.tbx_CallMessage);
            this.panel1.Controls.Add(this.pb_Download);
            this.panel1.Controls.Add(this.lbl_ReadyIndicator);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(549, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 94);
            this.panel1.TabIndex = 15;
            // 
            // pb_BrandLogo
            // 
            this.pb_BrandLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pb_BrandLogo.Location = new System.Drawing.Point(241, 32);
            this.pb_BrandLogo.Name = "pb_BrandLogo";
            this.pb_BrandLogo.Size = new System.Drawing.Size(152, 59);
            this.pb_BrandLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pb_BrandLogo.TabIndex = 16;
            this.pb_BrandLogo.TabStop = false;
            // 
            // tbx_CallMessage
            // 
            this.tbx_CallMessage.BackColor = System.Drawing.SystemColors.Window;
            this.tbx_CallMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbx_CallMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_CallMessage.ForeColor = System.Drawing.Color.Green;
            this.tbx_CallMessage.Location = new System.Drawing.Point(3, 32);
            this.tbx_CallMessage.Multiline = true;
            this.tbx_CallMessage.Name = "tbx_CallMessage";
            this.tbx_CallMessage.ReadOnly = true;
            this.tbx_CallMessage.Size = new System.Drawing.Size(232, 56);
            this.tbx_CallMessage.TabIndex = 17;
            this.tbx_CallMessage.Visible = false;
            // 
            // pb_Download
            // 
            this.pb_Download.Location = new System.Drawing.Point(3, 12);
            this.pb_Download.Name = "pb_Download";
            this.pb_Download.Size = new System.Drawing.Size(306, 10);
            this.pb_Download.Step = 1;
            this.pb_Download.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pb_Download.TabIndex = 14;
            this.pb_Download.Visible = false;
            // 
            // lbl_ReadyIndicator
            // 
            this.lbl_ReadyIndicator.AutoSize = true;
            this.lbl_ReadyIndicator.BackColor = System.Drawing.Color.Green;
            this.lbl_ReadyIndicator.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ReadyIndicator.ForeColor = System.Drawing.Color.Yellow;
            this.lbl_ReadyIndicator.Location = new System.Drawing.Point(315, 4);
            this.lbl_ReadyIndicator.Name = "lbl_ReadyIndicator";
            this.lbl_ReadyIndicator.Size = new System.Drawing.Size(79, 25);
            this.lbl_ReadyIndicator.TabIndex = 12;
            this.lbl_ReadyIndicator.Text = "Ready";
            this.lbl_ReadyIndicator.Visible = false;
            // 
            // btn_Abort
            // 
            this.btn_Abort.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Abort.ForeColor = System.Drawing.Color.Red;
            this.btn_Abort.Location = new System.Drawing.Point(316, 36);
            this.btn_Abort.Name = "btn_Abort";
            this.btn_Abort.Size = new System.Drawing.Size(62, 44);
            this.btn_Abort.TabIndex = 13;
            this.btn_Abort.Text = "Abort";
            this.btn_Abort.UseVisualStyleBackColor = true;
            this.btn_Abort.Visible = false;
            this.btn_Abort.Click += new System.EventHandler(this.btn_Abort_Click);
            // 
            // cbx_ArticleNum
            // 
            this.cbx_ArticleNum.FormattingEnabled = true;
            this.cbx_ArticleNum.Items.AddRange(new object[] {
            "CRWJTA0002",
            "CRW20012C4",
            "CRW5200024",
            "CRW5200025",
            "CRW6800151",
            "CRW51007Q4",
            "CRWGBB0008",
            "CRWE9004Z3",
            "CRW1529756",
            "CRWSTA0030",
            "CRB4036753",
            "CRB6035517",
            "CRB6035518",
            "CRB6021300",
            "CRB6035916",
            "CRB6035816",
            "CRB4050548",
            "CRB6048516",
            "CRB7219700"});
            this.cbx_ArticleNum.Location = new System.Drawing.Point(96, 34);
            this.cbx_ArticleNum.Name = "cbx_ArticleNum";
            this.cbx_ArticleNum.Size = new System.Drawing.Size(107, 21);
            this.cbx_ArticleNum.TabIndex = 11;
            this.cbx_ArticleNum.Text = "CRWJTA0002";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(479, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "pixels";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(412, 41);
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
            this.num_ImageFullSize.Location = new System.Drawing.Point(415, 57);
            this.num_ImageFullSize.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.num_ImageFullSize.Name = "num_ImageFullSize";
            this.num_ImageFullSize.Size = new System.Drawing.Size(61, 20);
            this.num_ImageFullSize.TabIndex = 8;
            // 
            // lbl_CallingFlag
            // 
            this.lbl_CallingFlag.AutoSize = true;
            this.lbl_CallingFlag.BackColor = System.Drawing.Color.Brown;
            this.lbl_CallingFlag.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbl_CallingFlag.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_CallingFlag.ForeColor = System.Drawing.Color.Yellow;
            this.lbl_CallingFlag.Location = new System.Drawing.Point(387, 2);
            this.lbl_CallingFlag.Name = "lbl_CallingFlag";
            this.lbl_CallingFlag.Size = new System.Drawing.Size(156, 24);
            this.lbl_CallingFlag.TabIndex = 6;
            this.lbl_CallingFlag.Text = " Calling Service";
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
            this.pnl_Footer.Controls.Add(this.lbl_AppVersion);
            this.pnl_Footer.Controls.Add(this.lbl_Timings);
            this.pnl_Footer.Controls.Add(this.checkBox1);
            this.pnl_Footer.Controls.Add(this.btn_Exit);
            this.pnl_Footer.Controls.Add(this.label2);
            this.pnl_Footer.Controls.Add(this.pictureBox1);
            this.pnl_Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_Footer.Location = new System.Drawing.Point(0, 513);
            this.pnl_Footer.Name = "pnl_Footer";
            this.pnl_Footer.Size = new System.Drawing.Size(949, 35);
            this.pnl_Footer.TabIndex = 15;
            // 
            // lbl_AppVersion
            // 
            this.lbl_AppVersion.AutoSize = true;
            this.lbl_AppVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AppVersion.Location = new System.Drawing.Point(318, 18);
            this.lbl_AppVersion.Name = "lbl_AppVersion";
            this.lbl_AppVersion.Size = new System.Drawing.Size(56, 12);
            this.lbl_AppVersion.TabIndex = 5;
            this.lbl_AppVersion.Text = "App Version";
            // 
            // lbl_Timings
            // 
            this.lbl_Timings.AutoSize = true;
            this.lbl_Timings.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Timings.Location = new System.Drawing.Point(560, 14);
            this.lbl_Timings.Name = "lbl_Timings";
            this.lbl_Timings.Size = new System.Drawing.Size(0, 12);
            this.lbl_Timings.TabIndex = 4;
            this.lbl_Timings.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbl_Timings_MouseDoubleClick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(469, 10);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(83, 17);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Show Menu";
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
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(318, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "Copyright © 2020 Richemont";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::CatalogViewer.Properties.Resources.richemontlogo_aim;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(318, 35);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(949, 548);
            this.Controls.Add(this.radImageEditor1);
            this.Controls.Add(this.radTreeView1);
            this.Controls.Add(this.radListView1);
            this.Controls.Add(this.pnl_ArticleCall);
            this.Controls.Add(this.pnl_Footer);
            this.MinimumSize = new System.Drawing.Size(965, 587);
            this.Name = "Form1";
            this.Text = "Richemont Product Catalog Viewer";
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.radImageEditor1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radListView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radTreeView1)).EndInit();
            this.pnl_ArticleCall.ResumeLayout(false);
            this.pnl_ArticleCall.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_BrandLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_ImageFullSize)).EndInit();
            this.pnl_Footer.ResumeLayout(false);
            this.pnl_Footer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
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
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown num_ImageFullSize;
        private System.Windows.Forms.Label lbl_Timings;
        private System.Windows.Forms.ComboBox cbx_ArticleNum;
        private System.Windows.Forms.Label lbl_ReadyIndicator;
        private System.Windows.Forms.Button btn_Abort;
        private System.Windows.Forms.ProgressBar pb_Download;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_AppVersion;
        private System.Windows.Forms.ComboBox cbx_Channel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pb_BrandLogo;
        private System.Windows.Forms.Button btn_CallService;
        private System.Windows.Forms.TextBox tbx_CallMessage;
    }
}

