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
            this.rlv_Images = new Telerik.WinControls.UI.RadListView();
            this.treeviewData = new Telerik.WinControls.UI.RadTreeView();
            this.radThemeManager1 = new Telerik.WinControls.RadThemeManager();
            this.pnl_ArticleCall = new System.Windows.Forms.Panel();
            this.pnl_ActivityIndicator = new System.Windows.Forms.Panel();
            this.lbl_FormFlagBottom = new System.Windows.Forms.Label();
            this.lbl_FormFlagTop = new System.Windows.Forms.Label();
            this.cbx_BaseURL = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_CallService = new System.Windows.Forms.Button();
            this.cbx_Channel = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pnl_ReadyPanel = new System.Windows.Forms.Panel();
            this.pb_BrandLogo = new System.Windows.Forms.PictureBox();
            this.tbx_CallMessage = new System.Windows.Forms.TextBox();
            this.pb_Download = new System.Windows.Forms.ProgressBar();
            this.lbl_ReadyIndicator = new System.Windows.Forms.Label();
            this.lbl_ProgressBar = new System.Windows.Forms.Label();
            this.btn_Abort = new System.Windows.Forms.Button();
            this.cbx_ArticleNum = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.num_ImageFullSize = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.pnl_Footer = new System.Windows.Forms.Panel();
            this.cbx_LogTimings = new System.Windows.Forms.CheckBox();
            this.drp_Timings = new System.Windows.Forms.ComboBox();
            this.lbl_AppVersion = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radSplitContainer1 = new Telerik.WinControls.UI.RadSplitContainer();
            this.splitPanel1 = new Telerik.WinControls.UI.SplitPanel();
            this.pnl_DataTreeview = new System.Windows.Forms.Panel();
            this.pnl_DataSearch = new System.Windows.Forms.Panel();
            this.btn_TreeLocateReset = new System.Windows.Forms.Button();
            this.btn_LocateValue = new System.Windows.Forms.Button();
            this.tbx_LocateValue = new System.Windows.Forms.TextBox();
            this.cbx_DataSearchTokens = new System.Windows.Forms.ComboBox();
            this.splitPanel2 = new Telerik.WinControls.UI.SplitPanel();
            this.treeviewBOM = new Telerik.WinControls.UI.RadTreeView();
            this.pnl_Treeviews = new System.Windows.Forms.Panel();
            this.pnl_Editor = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.radImageEditor1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlv_Images)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeviewData)).BeginInit();
            this.pnl_ArticleCall.SuspendLayout();
            this.pnl_ActivityIndicator.SuspendLayout();
            this.pnl_ReadyPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_BrandLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_ImageFullSize)).BeginInit();
            this.pnl_Footer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).BeginInit();
            this.radSplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).BeginInit();
            this.splitPanel1.SuspendLayout();
            this.pnl_DataTreeview.SuspendLayout();
            this.pnl_DataSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).BeginInit();
            this.splitPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeviewBOM)).BeginInit();
            this.pnl_Treeviews.SuspendLayout();
            this.pnl_Editor.SuspendLayout();
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
            this.radImageEditor1.Location = new System.Drawing.Point(0, 0);
            this.radImageEditor1.Name = "radImageEditor1";
            // 
            // 
            // 
            this.radImageEditor1.RootElement.AutoSize = false;
            this.radImageEditor1.RootElement.ScaleTransform = new System.Drawing.SizeF(1F, 1F);
            this.radImageEditor1.Size = new System.Drawing.Size(537, 446);
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
            // rlv_Images
            // 
            this.rlv_Images.AllowArbitraryItemHeight = true;
            this.rlv_Images.AllowArbitraryItemWidth = true;
            this.rlv_Images.AllowColumnReorder = false;
            this.rlv_Images.AllowColumnResize = false;
            this.rlv_Images.AllowEdit = false;
            this.rlv_Images.AllowRemove = false;
            this.rlv_Images.Dock = System.Windows.Forms.DockStyle.Left;
            this.rlv_Images.FullRowSelect = false;
            this.rlv_Images.ItemSize = new System.Drawing.Size(100, 200);
            this.rlv_Images.Location = new System.Drawing.Point(0, 110);
            this.rlv_Images.Name = "rlv_Images";
            this.rlv_Images.SelectLastAddedItem = false;
            this.rlv_Images.ShowColumnHeaders = false;
            this.rlv_Images.Size = new System.Drawing.Size(203, 446);
            this.rlv_Images.TabIndex = 8;
            this.rlv_Images.ViewType = Telerik.WinControls.UI.ListViewType.IconsView;
            this.rlv_Images.SelectedItemChanged += new System.EventHandler(this.radListView1_SelectedItemChanged);
            // 
            // treeviewData
            // 
            this.treeviewData.BackColor = System.Drawing.SystemColors.Control;
            this.treeviewData.Cursor = System.Windows.Forms.Cursors.Default;
            this.treeviewData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeviewData.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.treeviewData.ForeColor = System.Drawing.Color.Black;
            this.treeviewData.Location = new System.Drawing.Point(0, 0);
            this.treeviewData.MinimumSize = new System.Drawing.Size(0, 25);
            this.treeviewData.Name = "treeviewData";
            this.treeviewData.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.treeviewData.RootElement.MinSize = new System.Drawing.Size(0, 25);
            this.treeviewData.ShowDragHint = false;
            this.treeviewData.ShowDropHint = false;
            this.treeviewData.ShowNodeToolTips = true;
            this.treeviewData.Size = new System.Drawing.Size(209, 230);
            this.treeviewData.SpacingBetweenNodes = -1;
            this.treeviewData.TabIndex = 9;
            // 
            // pnl_ArticleCall
            // 
            this.pnl_ArticleCall.Controls.Add(this.pnl_ActivityIndicator);
            this.pnl_ArticleCall.Controls.Add(this.cbx_BaseURL);
            this.pnl_ArticleCall.Controls.Add(this.label7);
            this.pnl_ArticleCall.Controls.Add(this.btn_CallService);
            this.pnl_ArticleCall.Controls.Add(this.cbx_Channel);
            this.pnl_ArticleCall.Controls.Add(this.label6);
            this.pnl_ArticleCall.Controls.Add(this.pnl_ReadyPanel);
            this.pnl_ArticleCall.Controls.Add(this.btn_Abort);
            this.pnl_ArticleCall.Controls.Add(this.cbx_ArticleNum);
            this.pnl_ArticleCall.Controls.Add(this.label5);
            this.pnl_ArticleCall.Controls.Add(this.label4);
            this.pnl_ArticleCall.Controls.Add(this.num_ImageFullSize);
            this.pnl_ArticleCall.Controls.Add(this.label3);
            this.pnl_ArticleCall.Controls.Add(this.label1);
            this.pnl_ArticleCall.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_ArticleCall.Location = new System.Drawing.Point(0, 0);
            this.pnl_ArticleCall.Name = "pnl_ArticleCall";
            this.pnl_ArticleCall.Size = new System.Drawing.Size(949, 110);
            this.pnl_ArticleCall.TabIndex = 11;
            // 
            // pnl_ActivityIndicator
            // 
            this.pnl_ActivityIndicator.BackColor = System.Drawing.Color.Green;
            this.pnl_ActivityIndicator.Controls.Add(this.lbl_FormFlagBottom);
            this.pnl_ActivityIndicator.Controls.Add(this.lbl_FormFlagTop);
            this.pnl_ActivityIndicator.Location = new System.Drawing.Point(392, 7);
            this.pnl_ActivityIndicator.Name = "pnl_ActivityIndicator";
            this.pnl_ActivityIndicator.Size = new System.Drawing.Size(150, 55);
            this.pnl_ActivityIndicator.TabIndex = 21;
            this.pnl_ActivityIndicator.Visible = false;
            // 
            // lbl_FormFlagBottom
            // 
            this.lbl_FormFlagBottom.BackColor = System.Drawing.Color.Green;
            this.lbl_FormFlagBottom.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbl_FormFlagBottom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_FormFlagBottom.ForeColor = System.Drawing.Color.Yellow;
            this.lbl_FormFlagBottom.Location = new System.Drawing.Point(3, 27);
            this.lbl_FormFlagBottom.Name = "lbl_FormFlagBottom";
            this.lbl_FormFlagBottom.Size = new System.Drawing.Size(144, 24);
            this.lbl_FormFlagBottom.TabIndex = 7;
            this.lbl_FormFlagBottom.Text = "Service";
            this.lbl_FormFlagBottom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_FormFlagTop
            // 
            this.lbl_FormFlagTop.BackColor = System.Drawing.Color.Green;
            this.lbl_FormFlagTop.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lbl_FormFlagTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_FormFlagTop.ForeColor = System.Drawing.Color.Yellow;
            this.lbl_FormFlagTop.Location = new System.Drawing.Point(3, 5);
            this.lbl_FormFlagTop.Name = "lbl_FormFlagTop";
            this.lbl_FormFlagTop.Size = new System.Drawing.Size(144, 24);
            this.lbl_FormFlagTop.TabIndex = 6;
            this.lbl_FormFlagTop.Text = "Calling the";
            this.lbl_FormFlagTop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbx_BaseURL
            // 
            this.cbx_BaseURL.FormattingEnabled = true;
            this.cbx_BaseURL.Location = new System.Drawing.Point(96, 83);
            this.cbx_BaseURL.Name = "cbx_BaseURL";
            this.cbx_BaseURL.Size = new System.Drawing.Size(282, 21);
            this.cbx_BaseURL.TabIndex = 20;
            this.cbx_BaseURL.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbx_BaseURL_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(36, 86);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Base URL:";
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
            "CRM",
            "IPADRETAIL",
            "WEB"});
            this.cbx_Channel.Location = new System.Drawing.Point(96, 58);
            this.cbx_Channel.Name = "cbx_Channel";
            this.cbx_Channel.Size = new System.Drawing.Size(107, 21);
            this.cbx_Channel.TabIndex = 17;
            this.cbx_Channel.Text = "CRM";
            this.cbx_Channel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbx_Channel_KeyDown);
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
            // pnl_ReadyPanel
            // 
            this.pnl_ReadyPanel.Controls.Add(this.pb_BrandLogo);
            this.pnl_ReadyPanel.Controls.Add(this.tbx_CallMessage);
            this.pnl_ReadyPanel.Controls.Add(this.pb_Download);
            this.pnl_ReadyPanel.Controls.Add(this.lbl_ReadyIndicator);
            this.pnl_ReadyPanel.Controls.Add(this.lbl_ProgressBar);
            this.pnl_ReadyPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnl_ReadyPanel.Location = new System.Drawing.Point(549, 0);
            this.pnl_ReadyPanel.Name = "pnl_ReadyPanel";
            this.pnl_ReadyPanel.Size = new System.Drawing.Size(400, 110);
            this.pnl_ReadyPanel.TabIndex = 15;
            // 
            // pb_BrandLogo
            // 
            this.pb_BrandLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pb_BrandLogo.Location = new System.Drawing.Point(241, 40);
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
            this.tbx_CallMessage.Location = new System.Drawing.Point(3, 40);
            this.tbx_CallMessage.Multiline = true;
            this.tbx_CallMessage.Name = "tbx_CallMessage";
            this.tbx_CallMessage.ReadOnly = true;
            this.tbx_CallMessage.Size = new System.Drawing.Size(232, 56);
            this.tbx_CallMessage.TabIndex = 17;
            this.tbx_CallMessage.Visible = false;
            // 
            // pb_Download
            // 
            this.pb_Download.Location = new System.Drawing.Point(9, 8);
            this.pb_Download.Name = "pb_Download";
            this.pb_Download.Size = new System.Drawing.Size(296, 18);
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
            this.lbl_ReadyIndicator.Location = new System.Drawing.Point(315, 5);
            this.lbl_ReadyIndicator.Name = "lbl_ReadyIndicator";
            this.lbl_ReadyIndicator.Size = new System.Drawing.Size(79, 25);
            this.lbl_ReadyIndicator.TabIndex = 12;
            this.lbl_ReadyIndicator.Text = "Ready";
            this.lbl_ReadyIndicator.Visible = false;
            // 
            // lbl_ProgressBar
            // 
            this.lbl_ProgressBar.AutoSize = true;
            this.lbl_ProgressBar.ForeColor = System.Drawing.Color.Green;
            this.lbl_ProgressBar.Location = new System.Drawing.Point(94, 26);
            this.lbl_ProgressBar.Name = "lbl_ProgressBar";
            this.lbl_ProgressBar.Size = new System.Drawing.Size(106, 13);
            this.lbl_ProgressBar.TabIndex = 18;
            this.lbl_ProgressBar.Text = "Downloading Images";
            this.lbl_ProgressBar.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_ProgressBar.Visible = false;
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
            this.cbx_ArticleNum.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cbx_ArticleNum_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(479, 86);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "pixels";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(412, 68);
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
            this.num_ImageFullSize.Location = new System.Drawing.Point(415, 84);
            this.num_ImageFullSize.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.num_ImageFullSize.Name = "num_ImageFullSize";
            this.num_ImageFullSize.Size = new System.Drawing.Size(61, 20);
            this.num_ImageFullSize.TabIndex = 8;
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
            this.pnl_Footer.Controls.Add(this.cbx_LogTimings);
            this.pnl_Footer.Controls.Add(this.drp_Timings);
            this.pnl_Footer.Controls.Add(this.lbl_AppVersion);
            this.pnl_Footer.Controls.Add(this.checkBox1);
            this.pnl_Footer.Controls.Add(this.btn_Exit);
            this.pnl_Footer.Controls.Add(this.label2);
            this.pnl_Footer.Controls.Add(this.pictureBox1);
            this.pnl_Footer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnl_Footer.Location = new System.Drawing.Point(0, 556);
            this.pnl_Footer.Name = "pnl_Footer";
            this.pnl_Footer.Size = new System.Drawing.Size(949, 35);
            this.pnl_Footer.TabIndex = 15;
            // 
            // cbx_LogTimings
            // 
            this.cbx_LogTimings.AutoSize = true;
            this.cbx_LogTimings.Checked = true;
            this.cbx_LogTimings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbx_LogTimings.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbx_LogTimings.Location = new System.Drawing.Point(446, 17);
            this.cbx_LogTimings.Name = "cbx_LogTimings";
            this.cbx_LogTimings.Size = new System.Drawing.Size(64, 16);
            this.cbx_LogTimings.TabIndex = 7;
            this.cbx_LogTimings.Text = "Log Tmgs";
            this.cbx_LogTimings.UseVisualStyleBackColor = true;
            // 
            // drp_Timings
            // 
            this.drp_Timings.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.drp_Timings.FormattingEnabled = true;
            this.drp_Timings.Location = new System.Drawing.Point(512, 7);
            this.drp_Timings.Name = "drp_Timings";
            this.drp_Timings.Size = new System.Drawing.Size(355, 21);
            this.drp_Timings.TabIndex = 6;
            this.drp_Timings.Visible = false;
            // 
            // lbl_AppVersion
            // 
            this.lbl_AppVersion.AutoSize = true;
            this.lbl_AppVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_AppVersion.Location = new System.Drawing.Point(312, 18);
            this.lbl_AppVersion.Name = "lbl_AppVersion";
            this.lbl_AppVersion.Size = new System.Drawing.Size(56, 12);
            this.lbl_AppVersion.TabIndex = 5;
            this.lbl_AppVersion.Text = "App Version";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(446, 3);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 3;
            this.checkBox1.Text = "Menu";
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
            this.label2.Location = new System.Drawing.Point(313, 3);
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
            this.pictureBox1.Size = new System.Drawing.Size(312, 35);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // radSplitContainer1
            // 
            this.radSplitContainer1.Controls.Add(this.splitPanel1);
            this.radSplitContainer1.Controls.Add(this.splitPanel2);
            this.radSplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radSplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.radSplitContainer1.Name = "radSplitContainer1";
            this.radSplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // 
            // 
            this.radSplitContainer1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.radSplitContainer1.Size = new System.Drawing.Size(209, 446);
            this.radSplitContainer1.SplitterWidth = 8;
            this.radSplitContainer1.TabIndex = 16;
            this.radSplitContainer1.TabStop = false;
            // 
            // splitPanel1
            // 
            this.splitPanel1.Controls.Add(this.pnl_DataTreeview);
            this.splitPanel1.Controls.Add(this.pnl_DataSearch);
            this.splitPanel1.Location = new System.Drawing.Point(0, 0);
            this.splitPanel1.Name = "splitPanel1";
            // 
            // 
            // 
            this.splitPanel1.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel1.Size = new System.Drawing.Size(209, 295);
            this.splitPanel1.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, 0.173516F);
            this.splitPanel1.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, 76);
            this.splitPanel1.TabIndex = 0;
            this.splitPanel1.TabStop = false;
            this.splitPanel1.Text = "splitPanel1";
            // 
            // pnl_DataTreeview
            // 
            this.pnl_DataTreeview.Controls.Add(this.treeviewData);
            this.pnl_DataTreeview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_DataTreeview.Location = new System.Drawing.Point(0, 65);
            this.pnl_DataTreeview.Name = "pnl_DataTreeview";
            this.pnl_DataTreeview.Size = new System.Drawing.Size(209, 230);
            this.pnl_DataTreeview.TabIndex = 11;
            // 
            // pnl_DataSearch
            // 
            this.pnl_DataSearch.Controls.Add(this.btn_TreeLocateReset);
            this.pnl_DataSearch.Controls.Add(this.btn_LocateValue);
            this.pnl_DataSearch.Controls.Add(this.tbx_LocateValue);
            this.pnl_DataSearch.Controls.Add(this.cbx_DataSearchTokens);
            this.pnl_DataSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnl_DataSearch.Location = new System.Drawing.Point(0, 0);
            this.pnl_DataSearch.Name = "pnl_DataSearch";
            this.pnl_DataSearch.Size = new System.Drawing.Size(209, 65);
            this.pnl_DataSearch.TabIndex = 10;
            // 
            // btn_TreeLocateReset
            // 
            this.btn_TreeLocateReset.Enabled = false;
            this.btn_TreeLocateReset.Location = new System.Drawing.Point(159, 6);
            this.btn_TreeLocateReset.Name = "btn_TreeLocateReset";
            this.btn_TreeLocateReset.Size = new System.Drawing.Size(41, 21);
            this.btn_TreeLocateReset.TabIndex = 5;
            this.btn_TreeLocateReset.Text = "Clear";
            this.btn_TreeLocateReset.UseVisualStyleBackColor = true;
            this.btn_TreeLocateReset.Click += new System.EventHandler(this.btn_TreeLocateReset_Click);
            // 
            // btn_LocateValue
            // 
            this.btn_LocateValue.Enabled = false;
            this.btn_LocateValue.Location = new System.Drawing.Point(159, 31);
            this.btn_LocateValue.Name = "btn_LocateValue";
            this.btn_LocateValue.Size = new System.Drawing.Size(43, 23);
            this.btn_LocateValue.TabIndex = 4;
            this.btn_LocateValue.Text = "Go";
            this.btn_LocateValue.UseVisualStyleBackColor = true;
            this.btn_LocateValue.Click += new System.EventHandler(this.btn_LocateValue_Click);
            // 
            // tbx_LocateValue
            // 
            this.tbx_LocateValue.Enabled = false;
            this.tbx_LocateValue.Location = new System.Drawing.Point(6, 33);
            this.tbx_LocateValue.Name = "tbx_LocateValue";
            this.tbx_LocateValue.Size = new System.Drawing.Size(147, 20);
            this.tbx_LocateValue.TabIndex = 3;
            // 
            // cbx_DataSearchTokens
            // 
            this.cbx_DataSearchTokens.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbx_DataSearchTokens.Enabled = false;
            this.cbx_DataSearchTokens.FormattingEnabled = true;
            this.cbx_DataSearchTokens.Location = new System.Drawing.Point(6, 6);
            this.cbx_DataSearchTokens.Name = "cbx_DataSearchTokens";
            this.cbx_DataSearchTokens.Size = new System.Drawing.Size(147, 21);
            this.cbx_DataSearchTokens.TabIndex = 0;
            // 
            // splitPanel2
            // 
            this.splitPanel2.Controls.Add(this.treeviewBOM);
            this.splitPanel2.Location = new System.Drawing.Point(0, 303);
            this.splitPanel2.Name = "splitPanel2";
            // 
            // 
            // 
            this.splitPanel2.RootElement.MinSize = new System.Drawing.Size(25, 25);
            this.splitPanel2.Size = new System.Drawing.Size(209, 143);
            this.splitPanel2.SizeInfo.AutoSizeScale = new System.Drawing.SizeF(0F, -0.173516F);
            this.splitPanel2.SizeInfo.SplitterCorrection = new System.Drawing.Size(0, -76);
            this.splitPanel2.TabIndex = 1;
            this.splitPanel2.TabStop = false;
            this.splitPanel2.Text = "splitPanel2";
            // 
            // treeviewBOM
            // 
            this.treeviewBOM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeviewBOM.Location = new System.Drawing.Point(0, 0);
            this.treeviewBOM.MinimumSize = new System.Drawing.Size(0, 25);
            this.treeviewBOM.Name = "treeviewBOM";
            // 
            // 
            // 
            this.treeviewBOM.RootElement.MinSize = new System.Drawing.Size(0, 25);
            this.treeviewBOM.ShowDragHint = false;
            this.treeviewBOM.ShowDropHint = false;
            this.treeviewBOM.ShowNodeToolTips = true;
            this.treeviewBOM.Size = new System.Drawing.Size(209, 143);
            this.treeviewBOM.SpacingBetweenNodes = -1;
            this.treeviewBOM.TabIndex = 0;
            // 
            // pnl_Treeviews
            // 
            this.pnl_Treeviews.Controls.Add(this.radSplitContainer1);
            this.pnl_Treeviews.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnl_Treeviews.Location = new System.Drawing.Point(740, 110);
            this.pnl_Treeviews.Name = "pnl_Treeviews";
            this.pnl_Treeviews.Size = new System.Drawing.Size(209, 446);
            this.pnl_Treeviews.TabIndex = 17;
            // 
            // pnl_Editor
            // 
            this.pnl_Editor.Controls.Add(this.radImageEditor1);
            this.pnl_Editor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Editor.Location = new System.Drawing.Point(203, 110);
            this.pnl_Editor.Name = "pnl_Editor";
            this.pnl_Editor.Size = new System.Drawing.Size(537, 446);
            this.pnl_Editor.TabIndex = 18;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(949, 591);
            this.Controls.Add(this.pnl_Editor);
            this.Controls.Add(this.pnl_Treeviews);
            this.Controls.Add(this.rlv_Images);
            this.Controls.Add(this.pnl_ArticleCall);
            this.Controls.Add(this.pnl_Footer);
            this.MinimumSize = new System.Drawing.Size(965, 587);
            this.Name = "Form1";
            this.Text = "Richemont Product Catalog Viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.SizeChanged += new System.EventHandler(this.Form1_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.radImageEditor1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rlv_Images)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.treeviewData)).EndInit();
            this.pnl_ArticleCall.ResumeLayout(false);
            this.pnl_ArticleCall.PerformLayout();
            this.pnl_ActivityIndicator.ResumeLayout(false);
            this.pnl_ReadyPanel.ResumeLayout(false);
            this.pnl_ReadyPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pb_BrandLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_ImageFullSize)).EndInit();
            this.pnl_Footer.ResumeLayout(false);
            this.pnl_Footer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radSplitContainer1)).EndInit();
            this.radSplitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel1)).EndInit();
            this.splitPanel1.ResumeLayout(false);
            this.pnl_DataTreeview.ResumeLayout(false);
            this.pnl_DataSearch.ResumeLayout(false);
            this.pnl_DataSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitPanel2)).EndInit();
            this.splitPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeviewBOM)).EndInit();
            this.pnl_Treeviews.ResumeLayout(false);
            this.pnl_Editor.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadImageEditor radImageEditor1;
        private Telerik.WinControls.UI.RadListView rlv_Images;
        private Telerik.WinControls.UI.RadTreeView treeviewData;
        private Telerik.WinControls.RadThemeManager radThemeManager1;
        private System.Windows.Forms.Panel pnl_ArticleCall;
        private System.Windows.Forms.Panel pnl_Footer;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_FormFlagTop;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown num_ImageFullSize;
        private System.Windows.Forms.ComboBox cbx_ArticleNum;
        private System.Windows.Forms.Label lbl_ReadyIndicator;
        private System.Windows.Forms.Button btn_Abort;
        private System.Windows.Forms.ProgressBar pb_Download;
        private System.Windows.Forms.Panel pnl_ReadyPanel;
        private System.Windows.Forms.Label lbl_AppVersion;
        private System.Windows.Forms.ComboBox cbx_Channel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pb_BrandLogo;
        private System.Windows.Forms.Button btn_CallService;
        private System.Windows.Forms.TextBox tbx_CallMessage;
        private System.Windows.Forms.ComboBox cbx_BaseURL;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel pnl_ActivityIndicator;
        private System.Windows.Forms.Label lbl_FormFlagBottom;
        private System.Windows.Forms.Label lbl_ProgressBar;
        private System.Windows.Forms.ComboBox drp_Timings;
        private System.Windows.Forms.CheckBox cbx_LogTimings;
        private Telerik.WinControls.UI.RadSplitContainer radSplitContainer1;
        private Telerik.WinControls.UI.SplitPanel splitPanel1;
        private Telerik.WinControls.UI.SplitPanel splitPanel2;
        private Telerik.WinControls.UI.RadTreeView treeviewBOM;
        private System.Windows.Forms.Panel pnl_Treeviews;
        private System.Windows.Forms.Panel pnl_Editor;
        private System.Windows.Forms.Panel pnl_DataTreeview;
        private System.Windows.Forms.Panel pnl_DataSearch;
        private System.Windows.Forms.ComboBox cbx_DataSearchTokens;
        private System.Windows.Forms.Button btn_LocateValue;
        private System.Windows.Forms.TextBox tbx_LocateValue;
        private System.Windows.Forms.Button btn_TreeLocateReset;
    }
}

