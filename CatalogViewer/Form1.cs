using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CatalogViewer.Properties;
using RichemontApiLibrary;
using Telerik.WinControls.UI;

namespace CatalogViewer
{
    public partial class Form1 : Form
    {

        //================================================================
        //               *** KEEP THE VERSION HISTORY ***
        //================================================================
        //public static string m_AppVersion_UI = "Version 1, Build 1, 11/20/2020"; //1. First release
        //public static string m_AppVersion_UI = "Version 1, Build 2, 11/23/2020"; //1. Deriving brand from Article Number. 2. Added user selectable Channel. 3. UI improvements.
        //public static string m_AppVersion_UI = "Version 1, Build 3, 11/24/2020"; //1. Added logging with some try/catches. 2. Added BOM data. 3. Added treeview node icons.
        //public static string m_AppVersion_UI = "Version 1, Build 4, 11/30/2020"; //1. Added error and no data hardening, 2. Handle NULL BOM, 3. Form activity indicators, 4) Added Environment selection, 5) UI Improvements
        //public static string m_AppVersion_UI = "Version 1, Build 5, 12/02/2020"; //1. Corrected missing last image download, 2) Added Enter key detect to UI combobox controls, 3) Minor improvement to ProgressBar count display
        //public static string m_AppVersion_UI = "Version 1, Build 6, 12/02/2020"; //1. Reduced to one service call, 2. Improved form processing indicatations, 2. Added timings, 3. Loggable timings
        public static string m_AppVersion_UI = "Version 1, Build 7, 12/18/2020"; //1. Revised to use the dynamic API service result, 2. Added BOM treeview, 3. Added Image Data search for testing/review

        public static string m_CallMessage_Images = string.Empty;
        public static Boolean m_CallIsError_Images = false;
        public static Boolean m_UserAborted = false;

        public static string m_UserArticleNumber = string.Empty;
        public static string m_UserChannel = string.Empty;
        public static string m_UserBaseURL = string.Empty;

        public static TimeSpan m_tmeSpan_ServiceCall;
        public static TimeSpan m_tmeSpan_ProcessingData;
        public static TimeSpan m_tmeSpan_FirstThumbImage;
        public static TimeSpan m_tmeSpan_FirstFullImage;
        public static TimeSpan m_tmeSpan_DLThread;
        public static TimeSpan m_tmeSpan_AppReady;

        public static DateTime dlTimeStart;
        public static Boolean m_TimerIsSec = false;

        public static int m_dlCountTotal = 0;
        public static int m_dlCount = 0;
        public static string m_strBrandName = string.Empty;

        public static List<string> m_lstImageURLs = new List<string>();

        BindingList<ImageList> m_dsListView = new BindingList<ImageList>();
        BindingList<ImageData> m_dsImageData = new BindingList<ImageData>();

        public static dynamic dynAPIFullDataResult = null;

        //These delegates enable asynchronous calls
        public delegate void SetTimerLabelCallback(string strTimerMessage);
        public delegate void AppendListViewDatasetCallback(int idx, string strImageFilePathName_Thumb, string strImageFilePathName_Full, string strImageDownloadURL);
        public delegate void RefreshListViewCallback();
        public delegate void ListViewBeginUpdateCallback();
        public delegate void ListViewEndUpdateCallback();
        public delegate void UpdateCallMessageCallback(string strMessage, Boolean isError);
        public delegate void SetReadyIndicatorCallback(Boolean isVisible);
        public delegate void SetCallServiceControlsCallback(Boolean isCalling, Boolean bolAbortState);
        public delegate void SetCallingIndicatorCallback(Boolean isCalling, Boolean isFetchingData, Boolean isDownloadingFirstThumb, Boolean isDownloadingFirstFull, Boolean isThreadDownloading, Boolean isVisible);
        public delegate void UpdateDownloadProgressBarCallback(Boolean bolClearLabel, int valCount, int valMax);
        public delegate void WriteLoggerCallback(string strLogEntry);
        public delegate void UpdateTimersThreadedDoneCallback();

        DataSet ds_ImageDataDataset = new DataSet();

        public Thread _thread;

        //Treeview Icons
        public static Bitmap m_imgFolder;
        public static Bitmap m_imgFlag_red;
        public static Bitmap m_imgFlag_green;
        public static Bitmap m_imgFlag_blue;
        public static Bitmap m_imgKeyHS;
        public static Bitmap m_imgDocViewHS;

        public static Dictionary<string, string> dictFields = new Dictionary<string, string>();
        public const string cnstAllFieldsDropdownText = "--All Fields--";

        public Form1()
        {
            InitializeComponent();

            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);

            lbl_AppVersion.Text = m_AppVersion_UI;
            LoadBaseURLList();
            SetupImageEditor();
            CreateImageDataset();

            ResourceManager rm = Resources.ResourceManager;
            m_imgFolder = (Bitmap)rm.GetObject("FolderHS.png");
            m_imgFlag_red = (Bitmap)rm.GetObject("Flag_redHS.png");
            m_imgFlag_green = (Bitmap)rm.GetObject("Flag_greenHS.png");
            m_imgFlag_blue = (Bitmap)rm.GetObject("Flag_blueHS.png");
            m_imgKeyHS = (Bitmap)rm.GetObject("KeyHS.png");
            m_imgDocViewHS = (Bitmap)rm.GetObject("DocViewHS.png");

            cbx_DataSearchTokens.Text = cnstAllFieldsDropdownText;

            //Populate the Fields dictionary
            dictFields.Add("_image", "Image");
            dictFields.Add("ASSET_ORDER", "Asset Order");
            dictFields.Add("_imagetypes", "Image Types");
            dictFields.Add("IMAGE_TYPE", "Image Type");
            dictFields.Add("IMAGE_TYPE/code", "Image Type/Code");
            dictFields.Add("IMAGE_TYPE/field", "Image Type/Field");
            dictFields.Add("IMAGE_TYPE/value", "Image Type/value");
            dictFields.Add("akamai:default", "Public URL");
            dictFields.Add("NAME", "Name");
            dictFields.Add("_imagebackgrounds", "Image Backgrounds");
            dictFields.Add("IMAGE_BACKGROUND", "Image Background");
            dictFields.Add("_productviews", "Product Views");
            dictFields.Add("PRODUCT_VIEW", "Product View");
            dictFields.Add("CREDIT", "Credit");
        }

        static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;

            Logger _logger = new Logger();
            _logger.Log(String.Concat("UNHANDLED EXEPTION, AN:", m_UserArticleNumber, ", CN:", m_UserChannel, ", BaseURL:", m_UserBaseURL, ", ex.Message:", e.Message));
        }

        #region "Main Program ===================================================================================================================="

        private void LoadBaseURLList()
        {
            //First BaseURL key value
            string strFirstBaseURL = string.Empty;

            //Get the AppSettings
            var appSettings = ConfigurationManager.AppSettings;

            //Loop all keys
            foreach (var key in appSettings.AllKeys)
            {
                //If the key is a BaseURL
                if (key.Contains("BaseURL"))
                {
                    //Add the Key value to the combobox dropdown collection
                    if (appSettings[key] != string.Empty)
                    {
                        cbx_BaseURL.Items.Add(appSettings[key]);

                        //Get the first key of the stack
                        if (strFirstBaseURL == string.Empty)
                        {
                            strFirstBaseURL = appSettings[key];
                        }
                    }
                }

                //Set the combobox to the first key
                cbx_BaseURL.SelectedIndex = cbx_BaseURL.Items.IndexOf(strFirstBaseURL); ;
            }

        }

        private void SetupImageEditor()
        {
            //Keep for future reference
            //foreach (var item in this.radImageEditor1.ImageEditorElement.CommandsElement.CommandsStackElement.Children)
            //{
            //    RadMenuItem menuItem = item as RadMenuItem;
            //    if (menuItem != null && (menuItem.Text == "Resize" || menuItem.Text == "Crop" || menuItem.Text == "Hue" || menuItem.Text == "Saturation" || menuItem.Text == "Contrast" || menuItem.Text == "InvertColors"))
            //    {
            //        menuItem.Visibility = Telerik.WinControls.ElementVisibility.Collapsed;
            //    }
            //}

            radImageEditor1.ImageEditorElement.CommandsElement.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
        }

        private void cbx_ArticleNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CallService_UIEvent();
                e.Handled = true;
            }
        }

        private void cbx_Channel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CallService_UIEvent();
                e.Handled = true;
            }
        }

        private void cbx_BaseURL_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                CallService_UIEvent();
                e.Handled = true;
            }
        }

        private void btn_CallService_Click(object sender, EventArgs e)
        {
            CallService_UIEvent();
        }

        private void CallService_UIEvent()
        {
            //Check for Article Num entry
            string strArticleNum = string.Empty;
            if (cbx_ArticleNum.Text != string.Empty & cbx_Channel.Text != string.Empty)
            {
                if (cbx_BaseURL.Text != string.Empty)
                {
                    m_UserArticleNumber = cbx_ArticleNum.Text.Trim();
                    m_UserChannel = cbx_Channel.Text.Trim();
                    m_UserBaseURL = cbx_BaseURL.Text.Trim();

                    CallService();
                }
                else
                {
                    cbx_BaseURL.Focus();
                    MessageBox.Show("A valid Base URL is required.", "Entry Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else
            {
                cbx_ArticleNum.Focus();
                MessageBox.Show("A valid Article Number and Channel are required.", "Entry Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
        private void btn_Abort_Click(object sender, EventArgs e)
        {

            //Kill a previous thumb thread if still running =======================================================================
            try
            {
                if (_thread != null & _thread.ThreadState.Equals(ThreadState.Running))
                {
                    _thread.Abort();
                    m_UserAborted = true;
                }
            }
            catch
            {
                //Do not throw - the thread doesn't exist
            }

            SetReadyIndicator(true); //Turn on the ready indicator
            SetCallingIndicatorFromThread(true, false, false, false, false, false); //Turn o the "Calling" indicator
            SetCallServiceControls(false, false); //Set the Call Service controls as Unlocked with Abort off
            UpdateCallMessage_ThreadedDone(m_UserArticleNumber, m_UserChannel);
            UpdateDownloadProgressBarFromThread(true, 0, 0);
            LoadPerformanceAnalysisDropdown();
        }

        private void CallService()
        {

            //Kill a previous thumb thread if still running =======================================================================
            try
            {
                if (_thread != null)
                {
                    if (_thread.ThreadState.Equals(ThreadState.Running))
                    {
                        _thread.Abort();
                    }
                }
            }
            catch
            {
                //Do not throw - the thread doesn't exist
            }

            //Prep variables and controls =======================================================================

            m_dlCount = 0;
            m_UserAborted = false;

            cbx_DataSearchTokens.SelectedItem = null;
            tbx_LocateValue.Text = string.Empty;

            tbx_CallMessage.Text = string.Empty;
            tbx_CallMessage.ForeColor = Color.Green;
            tbx_CallMessage.Visible = false;

            m_TimerIsSec = false;

            treeviewData.Nodes.Clear();
            radImageEditor1.CurrentBitmap = null;
            radImageEditor1.Refresh();

            SetCallServiceControls(true, false); //Set the Call Service controls as Locked with Abort off

            LoadListView(); //This effectelty clears the ListView since there are no thumbs downlaoded yet
            m_dsListView.Clear(); //Clear the dataset that contains the Image URLs

            //Let's do this =======================================================================

            DateTime tmeStart = DateTime.Now;

            //Fetch the image download folder from app.config
            string strImageDownloadFolder = ConfigurationManager.AppSettings["MainProgramSettings.ImageDownloadFolder"];

            //Check if image download folder exists and if not create the folder
            if (!Directory.Exists(strImageDownloadFolder))
            {
                Directory.CreateDirectory(strImageDownloadFolder);
            }

            SetReadyIndicator(false); //Turn off the ready indicator
            pb_BrandLogo.Image = null; //Clear the brand picturebox

            //Derive the brand data
            Tuple<string, string, string> strBrandData = GetBrandByArticlePrefix(m_UserArticleNumber);
            if (strBrandData.Item3 != string.Empty)
            {
                ResourceManager rm = Resources.ResourceManager;
                Bitmap myImage = (Bitmap)rm.GetObject(strBrandData.Item3);
                pb_BrandLogo.BackgroundImage = myImage;
            }
            else
            {
                pb_BrandLogo.BackgroundImage = null;
            }

            m_strBrandName = strBrandData.Item2;



            if (CallService_AsDynamic(m_UserArticleNumber, strBrandData.Item1, m_UserChannel, m_UserBaseURL))
            {
                if (CallServiceImages_AsDynamic(m_UserArticleNumber, strBrandData.Item1, m_UserChannel, m_UserBaseURL))
                {
                    _thread = new Thread(() => CallServiceImages_Threaded(m_UserArticleNumber, m_UserChannel)) { IsBackground = false };
                    //xxxxxxxxxxxxxBindTreeview(0); //Before we start the thread, bind the Aux data treeview to the first image data which was loaded in CallService_Images sub
                    _thread.Start();
                }
            }
            else
            {
                //No Aux data returned, so reset the UI
                //Kill a previous thumb thread if still running
                try
                {
                    if (_thread != null & _thread.ThreadState.Equals(ThreadState.Running))
                    {
                        _thread.Abort();
                    }
                }
                catch
                {
                    //Do not throw - the thread doesn't exist
                }

                SetReadyIndicator(false); //Turn off the ready indicator
                SetCallServiceControls(false, false); //Set the Call Service controls as Unlocked with Abort off
                UpdateCallMessage_ThreadedDone(cbx_ArticleNum.Text, cbx_Channel.Text);
            }

            //if (CallService_AuxillaryData(m_UserArticleNumber, strBrandData.Item1, m_UserChannel, m_UserBaseURL))
            //{
            //    if (CallServiceImages(m_UserArticleNumber, strBrandData.Item1, m_UserChannel, m_UserBaseURL))
            //    {
            //        _thread = new Thread(() => CallServiceImages_Threaded(m_UserArticleNumber, m_UserChannel)) { IsBackground = false };
            //        BindTreeview(0); //Before we start the thread, bind the Aux data treeview to the first image data which was loaded in CallService_Images sub
            //        _thread.Start();
            //    }
            //}
            //else
            //{
            //    //No Aux data returned, so reset the UI
            //    //Kill a previous thumb thread if still running
            //    try
            //    {
            //        if (_thread != null & _thread.ThreadState.Equals(ThreadState.Running))
            //        {
            //            _thread.Abort();
            //        }
            //    }
            //    catch
            //    {
            //        //Do not throw - the thread doesn't exist
            //    }

            //    SetReadyIndicator(false); //Turn off the ready indicator
            //    SetCallServiceControls(false, false); //Set the Call Service controls as Unlocked with Abort off
            //    UpdateCallMessage_ThreadedDone(cbx_ArticleNum.Text, cbx_Channel.Text);
            //}

            m_tmeSpan_AppReady = DateTime.Now - tmeStart; //End the time and calculate the timespan value

        }

        private void SetCallingIndicatorFromThread(Boolean isCalling, Boolean isProcessingData, Boolean isDownloadingFirstThumb, Boolean isDownloadingFirstFull, Boolean isThreadDownloading, Boolean isVisible)
        {
            if (this.pnl_ActivityIndicator.InvokeRequired)
            {
                SetCallingIndicatorCallback d = new SetCallingIndicatorCallback(SetCallingIndicatorFromThread);
                this.Invoke(d, new object[] { isCalling, isProcessingData, isDownloadingFirstThumb, isDownloadingFirstFull, isThreadDownloading, isVisible });
            }
            else
            {
                if (isCalling)
                {
                    lbl_FormFlagTop.Text = "Calling the";
                    lbl_FormFlagBottom.Text = "Service";
                }
                else if (isProcessingData)
                {
                    lbl_FormFlagTop.Text = "Processing";
                    lbl_FormFlagBottom.Text = "the Data";
                }
                else if (isDownloadingFirstThumb)
                {
                    lbl_FormFlagTop.Text = "Downloading";
                    lbl_FormFlagBottom.Text = "1st Thumb Image";
                }
                else if (isDownloadingFirstFull)
                {
                    lbl_FormFlagTop.Text = "Downloading";
                    lbl_FormFlagBottom.Text = "1st Full Image";
                }
                else if (isThreadDownloading)
                {
                    lbl_FormFlagTop.Text = "Downloading";
                    lbl_FormFlagBottom.Text = "Rest of Images";
                }

                pnl_ActivityIndicator.Visible = isVisible;
                lbl_FormFlagTop.Refresh();
            }
        }

        private void SetCallServiceControls(Boolean isCalling, Boolean bolAbortState)
        {
            if (isCalling)
            {
                cbx_ArticleNum.Enabled = false;
                cbx_Channel.Enabled = false;
                cbx_BaseURL.Enabled = false;
                btn_CallService.Enabled = false;
                num_ImageFullSize.Enabled = false;
                drp_Timings.Text = "";
                drp_Timings.Visible = false;
                UpdateDownloadProgressBarFromThread(true, 0, 0);

                cbx_DataSearchTokens.Enabled = false;
                tbx_LocateValue.Enabled = false;
                btn_TreeLocateReset.Enabled = false;
                btn_LocateValue.Enabled = false;
                
            }
            else
            {
                cbx_ArticleNum.Enabled = true;
                cbx_Channel.Enabled = true;
                cbx_BaseURL.Enabled = true;
                btn_CallService.Enabled = true;
                num_ImageFullSize.Enabled = true;
                UpdateDownloadProgressBarFromThread(true, 0, 0);

                cbx_DataSearchTokens.Enabled = true;
                tbx_LocateValue.Enabled = true;
                btn_TreeLocateReset.Enabled = true;
                btn_LocateValue.Enabled = true;

            }

            btn_Abort.Visible = bolAbortState;
        }

        private void SetReadyIndicator(Boolean isVisible)
        {
            lbl_ReadyIndicator.Visible = isVisible;
            lbl_ReadyIndicator.Refresh();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                radImageEditor1.ImageEditorElement.CommandsElement.Visibility = Telerik.WinControls.ElementVisibility.Visible;
            }
            else
            {
                radImageEditor1.ImageEditorElement.CommandsElement.Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            }
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            if (rlv_Images.SelectedItems.Count >= 1)
            {
                string ImageFileNamePath = rlv_Images.Items[rlv_Images.SelectedIndex].Value.ToString();

                if (ImageFileNamePath != string.Empty)
                {
                    radImageEditor1.OpenImage(ImageFileNamePath);
                    //xxxxBindTreeview(rlv_Images.SelectedIndex); //Filter to the selected image PID
                }
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close(); //Note: Form1_FormClosing event will fire where dialog is displayed to the user
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            var DialogResult = MessageBox.Show("Are you sure you want to exit Product Catalog Viewer?", "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (DialogResult == DialogResult.Yes)
            {
                //Kill a previous thumb thread if still running =======================================================================
                try
                {
                    if (_thread != null & _thread.ThreadState.Equals(ThreadState.Running))
                    {
                        _thread.Abort();
                    }
                }
                catch
                {
                    //Do not throw - the thread doesn't exist
                }
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void LoadPerformanceAnalysisDropdown()
        {

            //Note: Converting double (TotalMilliseconds) to integer lops off the fractional value
            int intServiceCall = Convert.ToInt32(m_tmeSpan_ServiceCall.TotalMilliseconds);
            double dblProcessingDataMS = m_tmeSpan_ProcessingData.TotalMilliseconds;
            int intFirstThumbImage = Convert.ToInt32(m_tmeSpan_FirstThumbImage.TotalMilliseconds);
            int intFirstFullImage = Convert.ToInt32(m_tmeSpan_FirstFullImage.TotalMilliseconds);
            int intDLThread = Convert.ToInt32(m_tmeSpan_DLThread.TotalMilliseconds);
            int intAppReady = Convert.ToInt32(m_tmeSpan_AppReady.TotalMilliseconds);

            double dblServiceCall = m_tmeSpan_ServiceCall.TotalSeconds;
            double dblProcessingDataSec = m_tmeSpan_ProcessingData.TotalSeconds;
            double dblFirstThumbImage = m_tmeSpan_FirstThumbImage.TotalSeconds;
            double dblFirstFullImage = m_tmeSpan_FirstFullImage.TotalSeconds;
            double dblDLThread = m_tmeSpan_DLThread.TotalSeconds;
            double dblAppReady = m_tmeSpan_AppReady.TotalSeconds;

            drp_Timings.Items.Clear();

            if (m_UserAborted)
            {
                drp_Timings.Items.Add(String.Concat("Performance Analysis for ", m_UserArticleNumber, " / ", m_UserChannel, " (User Aborted)"));
            }
            else
            {
                drp_Timings.Items.Add(String.Concat("Performance Analysis for ", m_UserArticleNumber, " / ", m_UserChannel));
            }

            string strLogEntry = String.Concat("PerfAnal:", m_UserArticleNumber, "/", m_UserChannel);

            drp_Timings.Items.Add(String.Concat("Service Call: ", intServiceCall.ToString("#,##0"), " ms, ", dblServiceCall.ToString("#,##0.0"), " sec"));
            strLogEntry = String.Concat(strLogEntry, ", SrvCall: ", intServiceCall.ToString("#,##0"), " ms, ", dblServiceCall.ToString("#,##0.0"), " sec");

            drp_Timings.Items.Add(String.Concat("Processing Data: ", dblProcessingDataMS.ToString("#,##0.0"), " ms, ", dblProcessingDataSec.ToString("#,##0.00"), " sec"));
            strLogEntry = string.Concat(strLogEntry, String.Concat(", PrcData: ", dblProcessingDataMS.ToString("#,##0.0"), " ms, ", dblProcessingDataSec.ToString("#,##0.00"), " sec"));

            drp_Timings.Items.Add(String.Concat("First Thumb Image Download: ", intFirstThumbImage.ToString("#,##0"), " ms, ", dblFirstThumbImage.ToString("#,##0.0"), " sec"));
            strLogEntry = string.Concat(strLogEntry, String.Concat(", 1stThumbDL: ", intFirstThumbImage.ToString("#,##0"), " ms, ", dblFirstThumbImage.ToString("#,##0.0"), " sec"));

            if (num_ImageFullSize.Value == 0)
            {
                drp_Timings.Items.Add(String.Concat("First Image Download: ", intFirstFullImage.ToString("#,##0"), " ms, ", dblFirstFullImage.ToString("#,##0.0"), " sec"));
                strLogEntry = string.Concat(strLogEntry, String.Concat(", 1stImageDL: ", intFirstFullImage.ToString("#,##0"), " ms, ", dblFirstFullImage.ToString("#,##0.0"), " sec"));

                drp_Timings.Items.Add(String.Concat("Image and Thumb Downloads After First: ", intDLThread.ToString("#,##0"), " ms, ", dblDLThread.ToString("#,##0.0"), " sec"));
                strLogEntry = string.Concat(strLogEntry, String.Concat(", Img/ThumbDL: ", intDLThread.ToString("#,##0"), " ms, ", dblDLThread.ToString("#,##0.0"), " sec"));
            }
            else
            {
                drp_Timings.Items.Add(String.Concat("First Image Download (User-Sized): ", intFirstFullImage.ToString("#,##0"), " ms, ", dblFirstFullImage.ToString("#,##0.0"), " sec"));
                strLogEntry = string.Concat(strLogEntry, String.Concat(", 1stImgDL(US): ", intFirstFullImage.ToString("#,##0"), " ms, ", dblFirstFullImage.ToString("#,##0.0"), " sec"));

                drp_Timings.Items.Add(String.Concat("Image and Thumb Downloads After First (User-Sized): ", intDLThread.ToString("#,##0"), " ms, ", dblDLThread.ToString("#,##0.0"), " sec"));
                strLogEntry = string.Concat(strLogEntry, String.Concat(", Img/ThumbDL(US): ", intDLThread.ToString("#,##0"), " ms, ", dblDLThread.ToString("#,##0.0"), " sec"));
            }

            drp_Timings.Items.Add(String.Concat("Post Service Call Ready: ", intAppReady.ToString("#,##0"), " ms, ", dblAppReady.ToString("#,##0.0"), " sec"));
            strLogEntry = string.Concat(strLogEntry, String.Concat(", PostSrvCallReady: ", intAppReady.ToString("#,##0"), " ms, ", dblAppReady.ToString("#,##0.0"), " sec"));

            drp_Timings.SelectedIndex = 0;
            drp_Timings.Visible = true;

            if (cbx_LogTimings.Checked)
            {
                Logger _logger = new Logger();
                _logger.Log(strLogEntry);
            }

            //Clear the TimeSpan vars for next use
            m_tmeSpan_ServiceCall = System.TimeSpan.Zero;
            m_tmeSpan_ProcessingData = System.TimeSpan.Zero;
            m_tmeSpan_FirstThumbImage = System.TimeSpan.Zero;
            m_tmeSpan_FirstFullImage = System.TimeSpan.Zero;
            m_tmeSpan_DLThread = System.TimeSpan.Zero;
            m_tmeSpan_AppReady = System.TimeSpan.Zero;

        }

        #endregion

        #region "Data Structures ===================================================================================================================="
        private void CreateImageDataset()
        {

            //ImageAssetOrder
            DataTable dt_AO = new DataTable(); dt_AO.TableName = "dt_AO";

            //ImageType
            DataTable dt_IT = new DataTable(); dt_IT.TableName = "dt_IT";

            //ImagePublicUrl
            DataTable dt_PU = new DataTable(); dt_PU.TableName = "dt_PU";

            //StandardizedArticleType
            DataTable dt_SAT = new DataTable(); dt_SAT.TableName = "dt_SAT";

            //ProductItemSourceMarket
            DataTable dt_PSM = new DataTable(); dt_PSM.TableName = "dt_PSM";

            //ProductItemSourceMarketValues
            //ProductItemSourceMarketAvailability
            //ProductItemSourceMarketAvailabilityWeb
            //ProductItemSourceStandardizedSpecialOrder
            //ProductItemSourceWorldWideDistribution
            //ProductItemSourceStandardizedCountryDistribution
            //ProductItemSourceStandardizedCountryLaunchDate
            //ProductItemSourceChange

            //Bill of Material (BOM)
            DataTable dt_BOM = new DataTable(); dt_BOM.TableName = "dt_BOM";

            //ImageAssetOrder
            DataColumn dc = new DataColumn(); dc.ColumnName = "AO_pid"; dc.DataType = typeof(Int32); dt_AO.Columns.Add(dc);
            DataColumn dc1 = new DataColumn(); dc1.ColumnName = "AO_Field"; dc1.DataType = typeof(string); dt_AO.Columns.Add(dc1);
            DataColumn dc2 = new DataColumn(); dc2.ColumnName = "AO_Value"; dc2.DataType = typeof(string); dt_AO.Columns.Add(dc2);
            ds_ImageDataDataset.Tables.Add(dt_AO);

            //ImageType
            DataColumn dc3 = new DataColumn(); dc3.ColumnName = "IT_id"; dc3.DataType = typeof(Int32); dt_IT.Columns.Add(dc3);
            DataColumn dc4 = new DataColumn(); dc4.ColumnName = "IT_Code"; dc4.DataType = typeof(string); dt_IT.Columns.Add(dc4);
            DataColumn dc5 = new DataColumn(); dc5.ColumnName = "IT_Field"; dc5.DataType = typeof(string); dt_IT.Columns.Add(dc5);
            DataColumn dc6 = new DataColumn(); dc6.ColumnName = "IT_Value"; dc6.DataType = typeof(string); dt_IT.Columns.Add(dc6);
            ds_ImageDataDataset.Tables.Add(dt_IT);

            //ImagePublicUrl
            DataColumn dc7 = new DataColumn(); dc7.ColumnName = "PU_id"; dc7.DataType = typeof(Int32); dt_PU.Columns.Add(dc7);
            DataColumn dc8 = new DataColumn(); dc8.ColumnName = "PU_Code"; dc8.DataType = typeof(string); dt_PU.Columns.Add(dc8);
            DataColumn dc9 = new DataColumn(); dc9.ColumnName = "PU_Field"; dc9.DataType = typeof(string); dt_PU.Columns.Add(dc9);
            DataColumn dc10 = new DataColumn(); dc10.ColumnName = "PU_Value"; dc10.DataType = typeof(string); dt_PU.Columns.Add(dc10);
            ds_ImageDataDataset.Tables.Add(dt_PU);

            //StandardizedArticleType
            DataColumn dc11 = new DataColumn(); dc11.ColumnName = "SAT_id"; dc11.DataType = typeof(Int32); dt_SAT.Columns.Add(dc11);
            DataColumn dc12 = new DataColumn(); dc12.ColumnName = "SAT_Code"; dc12.DataType = typeof(string); dt_SAT.Columns.Add(dc12);
            DataColumn dc13 = new DataColumn(); dc13.ColumnName = "SAT_Field"; dc13.DataType = typeof(string); dt_SAT.Columns.Add(dc13);
            DataColumn dc14 = new DataColumn(); dc14.ColumnName = "SAT_Value"; dc14.DataType = typeof(string); dt_SAT.Columns.Add(dc14);
            ds_ImageDataDataset.Tables.Add(dt_SAT);

            //ProductItemSourceMarket
            DataColumn dc15 = new DataColumn(); dc15.ColumnName = "PSM_id"; dc15.DataType = typeof(Int32); dt_PSM.Columns.Add(dc15);
            DataColumn dc16 = new DataColumn(); dc16.ColumnName = "PSM_Code"; dc16.DataType = typeof(string); dt_PSM.Columns.Add(dc16);
            DataColumn dc17 = new DataColumn(); dc17.ColumnName = "PSM_Value"; dc17.DataType = typeof(string); dt_PSM.Columns.Add(dc17);
            ds_ImageDataDataset.Tables.Add(dt_PSM);

            //ProductItemSourceMarketValues
            //ProductItemSourceMarketAvailability
            //ProductItemSourceMarketAvailabilityWeb
            //ProductItemSourceStandardizedSpecialOrder
            //ProductItemSourceWorldWideDistribution
            //ProductItemSourceStandardizedCountryDistribution
            //ProductItemSourceStandardizedCountryLaunchDate
            //ProductItemSourceChange

            //Biil of Material (BOM)
            DataColumn dc94 = new DataColumn(); dc94.ColumnName = "BOM_pid"; dc94.DataType = typeof(Int32); dt_BOM.Columns.Add(dc94);
            DataColumn dc95 = new DataColumn(); dc95.ColumnName = "BOM_Children"; dc95.DataType = typeof(string); dt_BOM.Columns.Add(dc95);
            DataColumn dc96 = new DataColumn(); dc96.ColumnName = "BOM_Component"; dc96.DataType = typeof(string); dt_BOM.Columns.Add(dc96);
            DataColumn dc97 = new DataColumn(); dc97.ColumnName = "BOM_Id"; dc97.DataType = typeof(string); dt_BOM.Columns.Add(dc97);
            DataColumn dc98 = new DataColumn(); dc98.ColumnName = "BOM_Label"; dc98.DataType = typeof(string); dt_BOM.Columns.Add(dc98);
            DataColumn dc99 = new DataColumn(); dc99.ColumnName = "BOM_Quantity"; dc99.DataType = typeof(Int32); dt_BOM.Columns.Add(dc99);

            ds_ImageDataDataset.Tables.Add(dt_BOM);

            DataColumn parentColumn = ds_ImageDataDataset.Tables["dt_AO"].Columns["AO_pid"];
            DataColumn childColumn_IT = ds_ImageDataDataset.Tables["dt_IT"].Columns["IT_id"];
            DataColumn childColumn_PU = ds_ImageDataDataset.Tables["dt_PU"].Columns["PU_id"];
            DataColumn childColumn_SAT = ds_ImageDataDataset.Tables["dt_SAT"].Columns["SAT_id"];
            DataColumn childColumn_PSM = ds_ImageDataDataset.Tables["dt_PSM"].Columns["PSM_id"];

            // Create table Relations

            //Table AO is the parent table to all other tables
            //Note: There is no child relationship for the BOM data

            //ImageType - child to table AO
            DataRelation drGlobalVar_IT;
            drGlobalVar_IT = new DataRelation("GlobalVar_IT", parentColumn, childColumn_IT);
            ds_ImageDataDataset.Relations.Add(drGlobalVar_IT);

            //ImagePublicUrl - child to table AO
            DataRelation drGlobalVar_PU;
            drGlobalVar_PU = new DataRelation("GlobalVar_PU", parentColumn, childColumn_PU);
            ds_ImageDataDataset.Relations.Add(drGlobalVar_PU);

            //StandardizedArticleType - child to table AO
            DataRelation drGlobalVar_SAT;
            drGlobalVar_SAT = new DataRelation("GlobalVar_SAT", parentColumn, childColumn_SAT);
            ds_ImageDataDataset.Relations.Add(drGlobalVar_SAT);

            //ProductItemSourceMarket  - child to table AO
            DataRelation drGlobalVar_PSM;
            drGlobalVar_PSM = new DataRelation("GlobalVar_PSM", parentColumn, childColumn_PSM);
            ds_ImageDataDataset.Relations.Add(drGlobalVar_PSM);

            //ProductItemSourceMarketValues
            //ProductItemSourceMarketAvailability
            //ProductItemSourceMarketAvailabilityWeb
            //ProductItemSourceStandardizedSpecialOrder
            //ProductItemSourceWorldWideDistribution
            //ProductItemSourceStandardizedCountryDistribution
            //ProductItemSourceStandardizedCountryLaunchDate
            //ProductItemSourceChange
        }

        #endregion

        #region "Images ===================================================================================================================="

        private Boolean CallServiceImages_AsDynamic(string strArticleNum, string strBrand, string strChannel, string strBaseURL)
        {

            // Set local variables =======================================================================
            Logger _logger = new Logger();

            string strImageFileNameOnly = String.Empty;
            string strImageFilePathName_Full = String.Empty;
            string strImageFilePathName_Thumb = String.Empty;
            string[] strImageFileNameSplit;
            string strImageDownloadURL = String.Empty;

            //Fetch the Image List from the DataSet =======================================================================
            try
            {
                //For developer reference only --------------------------------
                //PublicUrl
                //DataRow dr_PU = ds_ImageDataDataset.Tables["dt_PU"].NewRow();
                //dr_PU[0] = id; //To "id" datacolumn
                //dr_PU[2] = imgItem.PublicUrl.Field;
                //dr_PU[3] = imgItem.PublicUrl.Value;
                //ds_ImageDataDataset.Tables["dt_PU"].Rows.Add(dr_PU);
                //--------------------------------------------------------------

                try
                {

                    //Clear the list collection (just to be safe)
                    m_lstImageURLs.Clear();

                    //Get the iamge URLs into a list for further processing
                    dynamic dynImageData = (IEnumerable<dynamic>)dynAPIFullDataResult.hits.hits[0]._source.images;
                    foreach (var imageItem in dynImageData)
                    {
                        m_lstImageURLs.Add(imageItem.publicUrl.value.ToString());
                    }

                    //DataTable tablePU = ds_ImageDataDataset.Tables["dt_PU"];
                    //var query = tablePU.AsEnumerable().
                    //    Select(product => new
                    //    {
                    //        PublicUrl_id = product.Field<int>("PU_id"),
                    //        PublicUrl_Code = product.Field<string>("PU_Code"),
                    //        PublicUrl_Field = product.Field<string>("PU_Field"),
                    //        PublicUrl_Value = product.Field<string>("PU_Value"),
                    //    }); //Note: We don't need a WHERE clause because we want all of the Public URL values in the data result

                    ////Clear the list collection (just to be safe)
                    //m_lstImageURLs.Clear();

                    //foreach (var product in query)
                    //{
                    //    m_lstImageURLs.Add(product.PublicUrl_Value);
                    //}

                }
                catch (Exception ex)
                {
                    return false; //No images
                }

                if (m_lstImageURLs != null & m_lstImageURLs.Count() > 0) //If we have image URLs
                {

                    //Load the ListView with just the first image =======================================================================

                    //Fetch the image download folder path/name from app.config
                    string strImageDownloadFolder = ConfigurationManager.AppSettings["MainProgramSettings.ImageDownloadFolder"];

                    //Fetch the image thumbnail width from app.config
                    string strThumbnailURLExtension = string.Concat("?impolicy=resize&width=", ConfigurationManager.AppSettings["MainProgramSettings.ImageThumbnailWidth"]);

                    //Update the progressbar
                    UpdateDownloadProgressBarFromThread(false, 1, m_lstImageURLs.Count());

                    string strImageUrl = m_lstImageURLs[0]; //Note: Fetch the FIRST image URL from m_lstImageURLs
                    using (WebClient client = new WebClient())
                    {
                        //Derive file names for the local copies
                        strImageFileNameOnly = Path.GetFileName(strImageUrl); //Full size file name only
                        strImageFilePathName_Full = String.Concat(strImageDownloadFolder, strImageFileNameOnly); //Full size file path/name

                        strImageFileNameSplit = strImageFileNameOnly.Split('.');
                        strImageFilePathName_Thumb = String.Concat(strImageDownloadFolder, strImageFileNameSplit[0], "_Thumb.", strImageFileNameSplit[1]); //Thumbnail file path/name

                        //Thumbnail image download------------------------
                        try
                        {
                            SetCallingIndicatorFromThread(false, false, true, false, false, true); //Turn on the "Downloading Thumb" indicator

                            DateTime tmeStart = DateTime.Now;
                            //Derive a URL that includes the thumbnail resize command and download the resized image
                            client.DownloadFile(new Uri(string.Concat(strImageUrl, strThumbnailURLExtension)), strImageFilePathName_Thumb);
                            m_tmeSpan_FirstThumbImage = DateTime.Now - tmeStart; //End the time and calculate the timespan value

                        }
                        catch //Note: Any exception will do
                        {
                            strImageFilePathName_Thumb = GetNoImageAvailableImageFilePathName(true);
                        }

                        //Full Size image download ------------------------
                        strImageFilePathName_Full = String.Concat(strImageDownloadFolder, strImageFileNameOnly); //Full size file path/name

                        //Fetch the user's width size value and derive a URL that includes the user resize command if applicable
                        if (num_ImageFullSize.Value > 0) //Did the user's specify a width value?
                        {
                            strImageDownloadURL = string.Concat(strImageUrl, "?impolicy=resize&width=", num_ImageFullSize.Value.ToString());
                        }
                        else
                        {
                            strImageDownloadURL = string.Concat(strImageUrl);
                        }

                        //Download just the first full size image
                        try
                        {
                            SetCallingIndicatorFromThread(false, false, false, true, false, true); //Turn on the "Downloading Full" indicator

                            DateTime tmeStart = DateTime.Now;
                            client.DownloadFile(new Uri(strImageDownloadURL), strImageFilePathName_Full);
                            m_tmeSpan_FirstFullImage = DateTime.Now - tmeStart; //End the time and calculate the timespan value
                        }
                        catch //Note: Any exception will do
                        {
                            strImageFilePathName_Full = GetNoImageAvailableImageFilePathName(false);
                        }

                        //We'll wait for the full size download to finish before updating the UI with the newly downloaded thumb
                        this.rlv_Images.BeginUpdate(); //Note: BeginUpdate and EndUpdate prevent auto scrolling as the full size image downloads
                        m_dsListView.Add(new ImageList(0, strImageFilePathName_Thumb, strImageFilePathName_Full, strImageDownloadURL));
                        this.rlv_Images.EndUpdate();

                        //Load the image Editor now to give the user something to look at
                        radImageEditor1.OpenImage(strImageFilePathName_Full);
                        rlv_Images.Refresh(); //Refresh the ListView after the full size image has completed download
                        rlv_Images.SelectedIndex = 0;

                        m_dlCount = 1;

                    }

                    SetCallServiceControlsFromThread(true, true); //Set the Call Service controls as Locked with Abort On
                    return true;
                }
                else
                {
                    return false; //No images
                }

            }
            catch (Exception ex)
            {
                //If we error, log the error and bail out now
                _logger.Log(String.Concat("ERROR at CallServiceImages, AN:", m_UserArticleNumber, ", CN:", m_UserChannel, ", BaseURL:", m_UserBaseURL, ", ex.Message:", ex.Message));
                return false; //Error
            }
        }

        private void CallServiceImages_Threaded(string strArticleNum, string strChannel)
        {

            string strImageFileNameOnly = String.Empty;
            string strImageFilePathName_Full = String.Empty;
            string strImageFilePathName_Thumb = String.Empty;
            string[] strImageFileNameSplit;
            string strImageDownloadURL = String.Empty;

            //Fetch the image download folder path/name from app.config
            string strImageDownloadFolder = ConfigurationManager.AppSettings["MainProgramSettings.ImageDownloadFolder"];

            //Fetch the image thumbnail width from app.config as integer
            string strThumbnailURLExtension = string.Concat("?impolicy=resize&width=", ConfigurationManager.AppSettings["MainProgramSettings.ImageThumbnailWidth"]);

            DateTime tmeStart = DateTime.Now; //Start the timer

            //Fetch the Image List and Load the ListView =======================================================================
            if (m_lstImageURLs != null & m_lstImageURLs.Count > 1)
            {
                SetCallingIndicatorFromThread(false, false, false, false, true, true); //Turn on the "Thread Downloading Images" indicator
                for (int i = 1; i < m_lstImageURLs.Count; i++) //Note: Starts FOR index at 1 to start after image URL 0 of first downloaded image
                {

                    string strImageUrl = m_lstImageURLs[i];
                    UpdateDownloadProgressBarFromThread(false, m_dlCount, m_lstImageURLs.Count);

                    using (WebClient client = new WebClient())
                    {
                        //Derive file names for the local copies
                        strImageFileNameOnly = Path.GetFileName(strImageUrl); //Full size file name only
                        strImageFilePathName_Full = String.Concat(strImageDownloadFolder, strImageFileNameOnly); //Full size file path/name

                        strImageFileNameSplit = strImageFileNameOnly.Split('.');
                        strImageFilePathName_Thumb = String.Concat(strImageDownloadFolder, strImageFileNameSplit[0], "_Thumb.", strImageFileNameSplit[1]); //Thumbnail file path/name

                        //Thumbnail image download------------------------
                        try
                        {
                            //Derive a URL that includes the thumbnail resise command and download the resized image
                            client.DownloadFile(new Uri(string.Concat(strImageUrl, strThumbnailURLExtension)), strImageFilePathName_Thumb);
                        }
                        catch //Note: Any exception will do
                        {
                            strImageFilePathName_Thumb = GetNoImageAvailableImageFilePathName(true);
                        }

                        //Full Size image download ------------------------
                        strImageFilePathName_Full = String.Concat(strImageDownloadFolder, strImageFileNameOnly); //Full size file path/name
                        //Fetch the user's width size value and derive a URL that includes the user resize command if applicable
                        if (num_ImageFullSize.Value > 0) //Did the user's specify a width value?
                        {
                            strImageDownloadURL = string.Concat(strImageUrl, "?impolicy=resize&width=", num_ImageFullSize.Value.ToString());
                        }
                        else
                        {
                            strImageDownloadURL = string.Concat(strImageUrl);
                        }

                        try
                        {
                            client.DownloadFile(new Uri(strImageDownloadURL), strImageFilePathName_Full); //Download the full size image
                        }
                        catch //Note: Any exception will do
                        {
                            strImageFilePathName_Full = GetNoImageAvailableImageFilePathName(false);
                        }

                        //We'll wait for the full size download to finish before updating the UI with the newly downloaded thumb
                        ListViewBeginUpdateFromThread();
                        AppendListViewDatasetFromThread(i, strImageFilePathName_Thumb, strImageFilePathName_Full, strImageDownloadURL);

                        ListViewEndUpdateFromThread();
                        //Note: No need to call ListView Refresh with the Begin/End Update calls

                    }

                    m_dlCount += 1;

                }
            }

            m_tmeSpan_DLThread = DateTime.Now - tmeStart; //End the time and calculate the timespan value

            UpdateCallMessage_ThreadedDone(strArticleNum, strChannel);
            UpdateTimers_ThreadedDone();
            SetCallServiceControlsFromThread(false, false); //Set the Call Service controls as Unlocked with Abort off
            SetCallingIndicatorFromThread(false, false, false, false, false, false); //Turn off the calling indicator

        }
        private string GetNoImageAvailableImageFilePathName(Boolean isThumb)
        {
            //Derive the no image exists image file/path name 
            string strNoImageExistsImage = string.Empty;
            if (isThumb)
            {
                strNoImageExistsImage = String.Concat(ConfigurationManager.AppSettings["MainProgramSettings.ImageDownloadFolder"], "noimageavailable_100.png");
                if (!File.Exists(strNoImageExistsImage))
                {
                    Properties.Resources.noimageavailable_100_png.Save(strNoImageExistsImage);
                }
            }
            else
            {
                strNoImageExistsImage = String.Concat(ConfigurationManager.AppSettings["MainProgramSettings.ImageDownloadFolder"], "noimageavailable.png");
                if (!File.Exists(strNoImageExistsImage))
                {
                    Properties.Resources.noimageavailable_png.Save(strNoImageExistsImage);
                }
            }
            return strNoImageExistsImage;
        }

        private void UpdateCallMessage_ThreadedDone(string strArticleNum, string strChannel)
        {
            //Update the call message - Note: We do this at the end of this thread because that's when all images are downloaded
            string strCallMessage = string.Empty;
            if (m_dlCount > 0)
            {
                int intCnt = m_dlCount;
                int intCntTotal = m_lstImageURLs.Count() + 1; //+1 because m_lstImageURLs is zero based
                strCallMessage = String.Concat("Returned ", intCnt.ToString(), " of ", intCntTotal.ToString(), " ", m_strBrandName, " catalog images for article '", strArticleNum, "' and Channel '", strChannel, "'.");
                UpdateCallMessageFromThread(strCallMessage, false);
            }
            else
            {
                strCallMessage = String.Concat("No ", m_strBrandName, " catalog information is available for article '", strArticleNum, "' and Channel '", strChannel, "'.");
                UpdateCallMessageFromThread(strCallMessage, true);
            }

            if (m_dlCount > 0)
            {
                SetReadyIndicatorFromThread(true); //Turn on the ready indicator
            }
            else
            {
                SetReadyIndicatorFromThread(false); //Turn off the ready indicator
            }

        }

        private void UpdateTimers_ThreadedDone()
        {
            if (this.drp_Timings.InvokeRequired)
            {
                UpdateTimersThreadedDoneCallback d = new UpdateTimersThreadedDoneCallback(UpdateTimers_ThreadedDone);
                this.Invoke(d, new object[] { });
            }
            else
            {
                LoadPerformanceAnalysisDropdown();
            }
        }

        private void ListViewBeginUpdateFromThread()
        {
            if (this.rlv_Images.InvokeRequired)
            {
                ListViewBeginUpdateCallback d = new ListViewBeginUpdateCallback(ListViewBeginUpdateFromThread);
                this.Invoke(d, new object[] { });
            }
            else
            {
                this.rlv_Images.BeginUpdate(); //Note: BeginUpdate and EndUpdate prevent auto scrolling as the image list builds
            }
        }

        private void AppendListViewDatasetFromThread(int idx, string strImageFilePathName_Thumb, string strImageFilePathName_Full, string strImageDownloadURL)
        {
            if (this.rlv_Images.InvokeRequired)
            {
                AppendListViewDatasetCallback d = new AppendListViewDatasetCallback(AppendListViewDatasetFromThread);
                this.Invoke(d, new object[] { idx, strImageFilePathName_Thumb, strImageFilePathName_Full, strImageDownloadURL });
            }
            else
            {
                this.rlv_Images.BeginUpdate(); //Note: BeginUpdate and EndUpdate prevent auto scrolling as the image list builds
                m_dsListView.Add(new ImageList(idx, strImageFilePathName_Thumb, strImageFilePathName_Full, strImageDownloadURL));
                this.rlv_Images.EndUpdate();
            }
        }

        private void ListViewEndUpdateFromThread()
        {
            if (this.rlv_Images.InvokeRequired)
            {
                ListViewEndUpdateCallback d = new ListViewEndUpdateCallback(ListViewEndUpdateFromThread);
                this.Invoke(d, new object[] { });
            }
            else
            {
                this.rlv_Images.EndUpdate(); //Note: BeginUpdate and EndUpdate prevent auto scrolling as the image list builds
            }
        }

        private void RefreshListViewFromThread()
        {
            if (this.rlv_Images.InvokeRequired)
            {
                RefreshListViewCallback d = new RefreshListViewCallback(RefreshListViewFromThread);
                this.Invoke(d, new object[] { });
            }
            else
            {
                rlv_Images.Refresh();
            }
        }

        private void UpdateCallMessageFromThread(string strMessage, Boolean isError)
        {
            if (this.tbx_CallMessage.InvokeRequired)
            {
                UpdateCallMessageCallback d = new UpdateCallMessageCallback(UpdateCallMessageFromThread);
                this.Invoke(d, new object[] { strMessage, isError });
            }
            else
            {
                tbx_CallMessage.Text = strMessage;
                if (isError)
                {
                    tbx_CallMessage.ForeColor = Color.Red;
                }
                else
                {
                    tbx_CallMessage.ForeColor = Color.Green;
                }
                tbx_CallMessage.Visible = true;
            }
        }

        private void WriteLoggerFromThread(string strLogEntry)
        {
            if (this.tbx_CallMessage.InvokeRequired)
            {
                WriteLoggerCallback d = new WriteLoggerCallback(WriteLoggerFromThread);
                this.Invoke(d, new object[] { strLogEntry });
            }
            else
            {
                Logger _logger = new Logger();
                _logger.Log(strLogEntry);
            }
        }

        private void SetReadyIndicatorFromThread(Boolean isVisible)
        {
            if (this.lbl_ReadyIndicator.InvokeRequired)
            {
                SetReadyIndicatorCallback d = new SetReadyIndicatorCallback(SetReadyIndicatorFromThread);
                this.Invoke(d, new object[] { isVisible });
            }
            else
            {
                lbl_ReadyIndicator.Visible = isVisible;
            }
        }

        private void UpdateDownloadProgressBarFromThread(Boolean bolClearLabel, int valCount, int valMax)
        {
            if (this.pb_Download.InvokeRequired)
            {
                UpdateDownloadProgressBarCallback d = new UpdateDownloadProgressBarCallback(UpdateDownloadProgressBarFromThread);
                this.Invoke(d, new object[] { bolClearLabel, valCount, valMax });
            }
            else
            {
                try
                {
                    if (bolClearLabel)
                    {
                        pb_Download.Maximum = 0;
                        pb_Download.Value = 0;
                        pb_Download.Visible = false;

                        lbl_ProgressBar.Text = String.Concat("Downloading Images.");
                        lbl_ProgressBar.Visible = false;
                    }
                    else
                    {
                        pb_Download.Maximum = valMax;
                        pb_Download.Value = valCount;
                        pb_Download.Visible = true;

                        valMax += 1; //+1 because the numeric value is zero-based
                        valCount += 1; //+1 because the numeric value is zero-based
                        lbl_ProgressBar.Text = String.Concat("Downloading ", valCount.ToString(), " of ", valMax.ToString(), " Images.");
                        lbl_ProgressBar.Visible = true;
                    }
                }
                catch
                {
                    //Do not throw. This is not an error.
                    //After the last image downloaded, the image download loop will call this sub with
                    //a value that is over the max value, which throws an error, but is not an error.
                }
            }
        }

        private void SetCallServiceControlsFromThread(Boolean isCalling, Boolean bolAbortState)
        {
            if (this.btn_CallService.InvokeRequired)
            {
                SetCallServiceControlsCallback d = new SetCallServiceControlsCallback(SetCallServiceControls);
                this.Invoke(d, new object[] { isCalling, bolAbortState });
            }
            else
            {
                if (isCalling)
                {
                    cbx_ArticleNum.Enabled = false;
                    cbx_Channel.Enabled = false;
                    cbx_BaseURL.Enabled = false;
                    btn_CallService.Enabled = false;
                    btn_Abort.Visible = true;
                    num_ImageFullSize.Enabled = false;
                    drp_Timings.Text = "";
                    drp_Timings.Visible = false;
                    UpdateDownloadProgressBarFromThread(true, 0, 0);
                }
                else
                {
                    cbx_ArticleNum.Enabled = true;
                    cbx_Channel.Enabled = true;
                    cbx_BaseURL.Enabled = true;
                    btn_CallService.Enabled = true;
                    num_ImageFullSize.Enabled = true;
                    UpdateDownloadProgressBarFromThread(true, 0, 0);
                }

                btn_Abort.Visible = bolAbortState;
            }
        }

        private Tuple<string, string, string> GetBrandByArticlePrefix(string strArticleNum)
        {
            string strBrand = string.Empty;
            string strBrandName = string.Empty;
            string strBrandLogoResourceName = string.Empty;

            //Obtain the brand prefix from the Article Number
            string strPrefix = strArticleNum.Substring(0, 2);

            //Assemble what the app.config should look like
            string strConfigKey = string.Concat("BrandCrossReference.", strPrefix);

            try
            {
                //Fetch the brand value from app.config
                string strAppConfigValue = ConfigurationManager.AppSettings[strConfigKey];

                string[] strAppConfigValueSplit = strAppConfigValue.Split(';');
                strBrand = strAppConfigValueSplit[0];
                strBrandName = strAppConfigValueSplit[1];
                strBrandLogoResourceName = strAppConfigValueSplit[2];

                if (strBrand != string.Empty)
                {
                    return Tuple.Create(strBrand, strBrandName, strBrandLogoResourceName);
                }
                else
                {
                    return Tuple.Create(strBrand, string.Empty, string.Empty); //Return the Artcile Number prefix if the App.config key fetch fails - better than nothing
                }

            }
            catch //This is not an exception - the strArticleNum argument could not be split
            {
                return Tuple.Create(strBrand, string.Empty, string.Empty); //Return the Artcile Number prefix if the App.config key fetch fails - better than nothing
            }
        }

        private void radListView1_SelectedItemChanged(object sender, EventArgs e)
        {
            if (rlv_Images.SelectedItems.Count >= 1)
            {
                try
                {
                    string ImageFileNamePath = rlv_Images.Items[rlv_Images.SelectedIndex].Value.ToString();
                    radImageEditor1.OpenImage(ImageFileNamePath);

                    LoadDataTreeview(rlv_Images.SelectedIndex);

                    //xxxxxxxxxxxxxxBindTreeview(rlv_Images.SelectedIndex); //Filter to the selected image PID
                }
                catch
                {
                    MessageBox.Show("There was a problem displaying the selected product image. Please try again in a few minutes.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void RadImageEditor1_ImageLoaded(object sender, AsyncCompletedEventArgs e)
        {
            float factor = this.GetAutoZoomFactor();
            radImageEditor1.ImageEditorElement.ZoomFactor = new SizeF(factor, factor);
        }

        private float GetAutoZoomFactor()
        {
            float factorX = (radImageEditor1.ImageEditorElement.Size.Width - radImageEditor1.ImageEditorElement.CommandsElementWidth) / (float)System.Convert.ToSingle(radImageEditor1.ImageEditorElement.CurrentBitmap.Width);
            float factorY = (radImageEditor1.ImageEditorElement.Size.Height - radImageEditor1.ImageEditorElement.ZoomElementHeight) / (float)System.Convert.ToSingle(radImageEditor1.ImageEditorElement.CurrentBitmap.Height);
            return Math.Min(factorX, factorY);
        }

        private void LoadListView()
        {
            rlv_Images.ItemDataBound += new Telerik.WinControls.UI.ListViewItemEventHandler(radListView1_ItemDataBound);
            rlv_Images.VisualItemFormatting += new Telerik.WinControls.UI.ListViewVisualItemEventHandler(radListView1_VisualItemFormatting);
            rlv_Images.CellFormatting += new Telerik.WinControls.UI.ListViewCellFormattingEventHandler(radListView1_CellFormatting);
            rlv_Images.ColumnCreating += new ListViewColumnCreatingEventHandler(radListView1_ColumnCreating);

            rlv_Images.ShowColumnHeaders = false;

            rlv_Images.AllowArbitraryItemWidth = true;
            rlv_Images.AllowArbitraryItemHeight = true;

            rlv_Images.ViewType = ListViewType.IconsView;
            ((IconListViewElement)this.rlv_Images.ListViewElement.ViewElement).Orientation = Orientation.Vertical;

            rlv_Images.AllowEdit = false;
            rlv_Images.AllowRemove = false;

            rlv_Images.DataSource = m_dsListView;
            rlv_Images.DisplayMember = "FilePathName_Thumb";
            rlv_Images.ValueMember = "FilePathName_Full";
        }

        private void radListView1_ItemDataBound(object sender, Telerik.WinControls.UI.ListViewItemEventArgs e)
        {
            try
            {
                string ImagePath = ((ImageList)e.Item.DataBoundItem).FilePathName_Thumb;

                using (MemoryStream ms = new MemoryStream())
                using (FileStream file = new FileStream(ImagePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] bytes = new byte[file.Length];
                    file.Read(bytes, 0, (int)file.Length);
                    e.Item.Image = Image.FromStream(new MemoryStream(bytes)); //Note: We use the Service to fetch reduced sized images
                    //e.Item.Image = Image.FromStream(new MemoryStream(bytes)).GetThumbnailImage(135, 185, null, new IntPtr()); <--- not used, but keep for reference
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

            //Note: Next two lines are just a cheap workaround to hid the icon text
            e.Item.ForeColor = Color.Transparent;
            e.Item.Font = new Font(new FontFamily(System.Drawing.Text.GenericFontFamilies.Serif), 1);

        }

        private void radListView1_VisualItemFormatting(object sender, Telerik.WinControls.UI.ListViewVisualItemEventArgs e)
        {
            if (e.VisualItem.Data.Image != null)
            {
                e.VisualItem.Layout.RightPart.Margin = new Padding(0, 2, 0, 2);
            }
        }

        private void radListView1_CellFormatting(object sender, ListViewCellFormattingEventArgs e)
        {
            //Keep for future use
        }

        private void radListView1_ColumnCreating(object sender, ListViewColumnCreatingEventArgs e)
        {
            if (e.Column.FieldName == "ID")
            {
                e.Column.Visible = false;
            }

            if (e.Column.FieldName == "FilePathName_Thumb")
            {
                e.Column.Visible = true;
            }

            if (e.Column.FieldName == "FilePathName_Full")
            {
                e.Column.Visible = false;
            }

            if (e.Column.FieldName == "ImageURL")
            {
                e.Column.Visible = false;
            }
        }

        #endregion

        #region "Auxillary Data ===================================================================================================================================="

        private Boolean CallService_AuxillaryData(string strArticleNum, string strBrand, string strChannel, string m_UserBaseURL)
        {
            Logger _logger = new Logger();

            SetCallingIndicatorFromThread(true, false, false, false, false, true); //Turn on the "Calling Service" indicator

            //API library 18+ accepts the host URL
            var host = new RichemontApiLibrary.Models.Host
            {
                //HostUri = "https://rlg-ric-test-qua.apigee.net/dms-search-v1",
                HostUri = m_UserBaseURL,
                AuthenticationType = "ApiKey",
                ApiKeyAuthentication = new RichemontApiLibrary.Models.ApiKeyAuthentication
                {
                    KeyName = "x-apikey",
                    Value = "F4VJUH9JZiXV5v2hfRM5PwmYUVdatDB0"
                }
            };

            //API library 18+ accepts the host URL as an argument to ProductCatalogApi
            var apiService = new RichemontApiLibrary.ProductCatalogApi(host);

            var result = new RichemontApiLibrary.Models.ProductCatalog.Queries.StandardizedProduct.Response();
            var request = new RichemontApiLibrary.Models.ProductCatalog.Queries.StandardizedProduct.Request
            {
                Article = strArticleNum,
                Brand = strBrand,
                Channel = strChannel,
                Language = "en",
                FromResult = 0,
                PageSize = 10
            };

            try
            {
                DateTime tmeStart = DateTime.Now;
                result = apiService.GetStandardizedProduct(request);
                m_tmeSpan_ServiceCall = DateTime.Now - tmeStart; //End the time and calculate the timespan value

                if (result == null)
                {
                    //If we get no data back bail out now - calling sub handles UI message
                    return false;
                }
            }
            catch (Exception ex)
            {
                //If we error, log the error and bail out now
                _logger.Log(String.Concat("ERROR at CallService_AuxillaryData, AN:", m_UserArticleNumber, ", CN:", m_UserChannel, ", BaseURL:", m_UserBaseURL, ", ex.Message:", ex.Message));
                return false;
            }

            //Fetch the Auxillary Data and Load the Treeview =======================================================================
            ds_ImageDataDataset.Clear();
            int id = 0;

            try
            {
                SetCallingIndicatorFromThread(false, true, false, false, false, true); //Turn on the "Processing Data" indicator

                DateTime tmeStart = DateTime.Now;
                foreach (var HitItem in result.Hits.Hits)
                {
                    var ImageItem = HitItem.Source.Images;
                    //ImageItem = null; //for Dev testing
                    if (ImageItem != null)
                    {
                        foreach (var imgItem in ImageItem)
                        {

                            //ImageAssetOrder
                            DataRow dr_AO = ds_ImageDataDataset.Tables["dt_AO"].NewRow();
                            dr_AO[0] = id; //To "pid" datacolumn
                            if (imgItem.AssetOrder == null)
                            {
                                dr_AO[1] = "ImageAssetOrder"; //Field
                                dr_AO[2] = id; //Value
                            }
                            else
                            {
                                dr_AO[1] = imgItem.AssetOrder.Field;
                                dr_AO[2] = imgItem.AssetOrder.Value;
                            }
                            ds_ImageDataDataset.Tables["dt_AO"].Rows.Add(dr_AO);

                            foreach (var imgTypes in imgItem.ImageTypes)
                            {
                                //ImageTypes
                                DataRow dr_IT = ds_ImageDataDataset.Tables["dt_IT"].NewRow();
                                dr_IT[0] = id; //To "id" datacolumn
                                dr_IT[1] = imgTypes.Code;
                                dr_IT[2] = imgTypes.Field;
                                dr_IT[3] = imgTypes.Value;
                                ds_ImageDataDataset.Tables["dt_IT"].Rows.Add(dr_IT);
                            }

                            //PublicUrl
                            DataRow dr_PU = ds_ImageDataDataset.Tables["dt_PU"].NewRow();
                            dr_PU[0] = id; //To "id" datacolumn
                            dr_PU[2] = imgItem.PublicUrl.Field;
                            dr_PU[3] = imgItem.PublicUrl.Value;
                            ds_ImageDataDataset.Tables["dt_PU"].Rows.Add(dr_PU);

                            ////StandardizedArticleType
                            //DataRow dr_SAT = ds_ImageDataDataset.Tables["dt_SAT"].NewRow();
                            //dr_SAT[0] = id; //To "id" datacolumn
                            //dr_SAT[2] = imgItem.PublicUrl.Field;
                            //dr_SAT[3] = imgItem.PublicUrl.Value;
                            //ds_ImageDataDataset.Tables["dt_SAT"].Rows.Add(dr_SAT);

                            ////ProductItemSourceMarket
                            //DataRow dr_PSM = ds_ImageDataDataset.Tables["dt_PSM"].NewRow();
                            //dr_PSM[0] = id; //To "id" datacolumn
                            //dr_PSM[2] = imgItem.PublicUrl.Code;
                            //dr_PSM[3] = imgItem.PublicUrl.Value;
                            //ds_ImageDataDataset.Tables["dt_PSM"].Rows.Add(dr_PSM);

                            //ProductItemSourceMarketValues
                            //ProductItemSourceMarketAvailability
                            //ProductItemSourceMarketAvailabilityWeb
                            //ProductItemSourceStandardizedSpecialOrder
                            //ProductItemSourceWorldWideDistribution
                            //ProductItemSourceStandardizedCountryDistribution
                            //ProductItemSourceStandardizedCountryLaunchDate
                            //ProductItemSourceChange

                            var BomItem = HitItem.Source.Bom;
                            //BomItem = null; //for Dev testing
                            if (BomItem != null)
                            {
                                foreach (var bomItem in BomItem)
                                {
                                    //Bill of Material (BOM)
                                    DataRow dr_BOM = ds_ImageDataDataset.Tables["dt_BOM"].NewRow();
                                    dr_BOM[0] = id; //To "pid" datacolumn
                                    dr_BOM[1] = bomItem.Children;
                                    dr_BOM[2] = bomItem.Component;
                                    dr_BOM[3] = bomItem.Id;
                                    dr_BOM[4] = bomItem.Label;
                                    dr_BOM[5] = bomItem.Quantity;
                                    ds_ImageDataDataset.Tables["dt_BOM"].Rows.Add(dr_BOM);
                                }
                            }
                            else
                            {
                                //Log that the BOM data was missing
                                _logger.Log(String.Concat("NOTICE - BOM Data Was NULL, AN:", m_UserArticleNumber, ", CN:", m_UserChannel, ", BaseURL:", m_UserBaseURL, ", ImageItem:", imgItem.PublicUrl.Value));
                            }

                            id += 1;
                        } //foreach (var imgItem in ImageItem)
                    }
                    else
                    {
                        //Log that the Image data was missing
                        _logger.Log(String.Concat("NOTICE - Image Data Was NULL, AN:", m_UserArticleNumber, ", CN:", m_UserChannel, ", BaseURL:", m_UserBaseURL));
                    } //if (ImageItem != null)
                } //foreach (var HitItem in result.Hits.Hits)

                m_tmeSpan_ProcessingData = DateTime.Now - tmeStart; //End the time and calculate the timespan value

            }
            catch (Exception ex)
            {
                //If we error, log the error and bail out now
                _logger.Log(String.Concat("ERROR at CallService_AuxillaryData, AN:", m_UserArticleNumber, ", CN:", m_UserChannel, ", BaseURL:", m_UserBaseURL, ", ex.Message:", ex.Message));
                return false;
            }
            return true;
        }

        private Boolean CallService_AsDynamic(string strArticleNum, string strBrand, string strChannel, string m_UserBaseURL)
        {
            Logger _logger = new Logger();

            SetCallingIndicatorFromThread(true, false, false, false, false, true); //Turn on the "Calling Service" indicator

            //API library 18+ accepts the host URL
            var host = new RichemontApiLibrary.Models.Host
            {
                //HostUri = "https://rlg-ric-test-qua.apigee.net/dms-search-v1",
                HostUri = m_UserBaseURL,
                AuthenticationType = "ApiKey",
                ApiKeyAuthentication = new RichemontApiLibrary.Models.ApiKeyAuthentication
                {
                    KeyName = "x-apikey",
                    Value = "F4VJUH9JZiXV5v2hfRM5PwmYUVdatDB0"
                }
            };

            //API library 18+ accepts the host URL as an argument to ProductCatalogApi
            var apiService = new RichemontApiLibrary.ProductCatalogApi(host);

            var result = new RichemontApiLibrary.Models.ProductCatalog.Queries.StandardizedProduct.Response();
            var request = new RichemontApiLibrary.Models.ProductCatalog.Queries.StandardizedProduct.Request
            {
                Article = strArticleNum,
                Brand = strBrand,
                Channel = strChannel,
                Language = "en",
                FromResult = 0,
                PageSize = 10
            };

            try
            {
                DateTime tmeStart = DateTime.Now;

                dynamic dynResult = apiService.GetStandardizedProductAsDynamic(request); //<----- "dynamic"
                if (dynResult != null)
                {
                    dynAPIFullDataResult = dynResult; //Store the full result in dynamic variable 'dynAPIFullDataResult'

                    dynamic imagesData = dynAPIFullDataResult.hits.hits[0]._source.images;
                    if (imagesData != null)
                    {
                        //Note: The LoadDataTreeview() is called when the image listview selection changes, which occurs on listview loading
                        //LoadDataSearchControls(0);
                    }

                    var BOMData = dynResult.hits.hits[0]._source.BOM;
                    if (BOMData != null)
                    {
                        LoadBOMTreeview();
                    }
                }

                m_tmeSpan_ServiceCall = DateTime.Now - tmeStart; //End the time and calculate the timespan value

            }
            catch (Exception ex)
            {
                //If we error, log the error and bail out now
                _logger.Log(String.Concat("ERROR at CallService_AuxillaryData_AsDynamic, AN:", m_UserArticleNumber, ", CN:", m_UserChannel, ", BaseURL:", m_UserBaseURL, ", ex.Message:", ex.Message));
                return false;
            }

            return true;
        }

        private Boolean xxxxxxxxxxxxxxxxxxxxxLoadDataSearchControls(int intImageIndex)
        {

            Logger _logger = new Logger();

            dynamic imagesData = dynAPIFullDataResult.hits.hits[0]._source.images[intImageIndex];

            try
            {
                cbx_DataSearchTokens.Items.Clear();

                foreach (dynamic imageItem in imagesData)
                {
                    cbx_DataSearchTokens.Items.Add(imageItem.Name.ToString());
                }

                return true;
            }
            catch (Exception ex)
            {
                //If we error, log the error and bail out now
                _logger.Log(String.Concat("ERROR at LoadDataSearchControls, AN:", m_UserArticleNumber, ", CN:", m_UserChannel, ", BaseURL:", m_UserBaseURL, ", ex.Message:", ex.Message));
                return false;
            }
        }

        private Boolean LoadDataTreeview(int intImageIndex)
        {
            Logger _logger = new Logger();

            try
            {
                dynamic imagesData = dynAPIFullDataResult.hits.hits[0]._source.images[intImageIndex];

                treeviewData.Nodes.Clear(); //Toss the data treeview nodes
                cbx_DataSearchTokens.Items.Clear(); //Clear the Search dropdown
                cbx_DataSearchTokens.Items.Add(cnstAllFieldsDropdownText); //Add to the Search dropdown

                string strNodeName = dictFields["_image"];
                RadTreeNode nodeTop = CreateNode(strNodeName, true);
                nodeTop.Name = strNodeName;
                nodeTop.ToolTipText = strNodeName;
                nodeTop.Image = m_imgFolder;
                treeviewData.Nodes.Add(nodeTop);

                //AssetOrder
                strNodeName = dictFields[imagesData.assetOrder.field.ToString()];
                RadTreeNode childNode = CreateNode(imagesData.assetOrder.value.ToString(), false);
                childNode.ToolTipText = strNodeName;
                childNode.Name = strNodeName;
                childNode.Image = m_imgFlag_red;
                nodeTop.Nodes.Add(childNode);
                cbx_DataSearchTokens.Items.Add(strNodeName); //Add to the Search dropdown

                //ImageTypes
                strNodeName = dictFields["_imagetypes"];
                RadTreeNode nodeIT = CreateNode(strNodeName, false);
                nodeIT.Name = strNodeName;
                nodeIT.ToolTipText = strNodeName;
                nodeIT.Image = m_imgFolder;
                treeviewData.Nodes.Add(nodeIT);

                foreach (dynamic imageItem_Types in imagesData.imageTypes)
                {
                    strNodeName = dictFields[imageItem_Types.field.ToString()]; //IMAGE_TYPE"

                    RadTreeNode childnodeIT = CreateNode(strNodeName, false);
                    nodeIT.Name = strNodeName;
                    nodeIT.ToolTipText = strNodeName;
                    nodeIT.Nodes.Add(childnodeIT);

                    childNode = CreateNode(imageItem_Types.code.ToString(), false);
                    childNode.Name = string.Concat(strNodeName,"/code");
                    childNode.ToolTipText = string.Concat(strNodeName, "/code");
                    childnodeIT.Nodes.Add(childNode);

                    childNode = CreateNode(imageItem_Types.field.ToString(), false);
                    childNode.Name = string.Concat(strNodeName, "/field");
                    childNode.ToolTipText = string.Concat(strNodeName, "/field");
                    childnodeIT.Nodes.Add(childNode);

                    childNode = CreateNode(imageItem_Types.value.ToString(), false);
                    childNode.Name = string.Concat(strNodeName, "/value");
                    childNode.ToolTipText = string.Concat(strNodeName, "/value");
                    childnodeIT.Nodes.Add(childNode);
                }

                cbx_DataSearchTokens.Items.Add(string.Concat(strNodeName, "/code")); //Add to the Search dropdown
                cbx_DataSearchTokens.Items.Add(string.Concat(strNodeName, "/field")); //Add to the Search dropdown
                cbx_DataSearchTokens.Items.Add(string.Concat(strNodeName, "/value")); //Add to the Search dropdown

                //Public URL
                strNodeName = dictFields[imagesData.publicUrl.field.ToString()]; //akamai:default
                childNode = CreateNode(imagesData.publicUrl.value.ToString(), false);
                childNode.Name = strNodeName;
                childNode.ToolTipText = strNodeName;
                childNode.Image = m_imgFlag_red;
                nodeTop.Nodes.Add(childNode);
                cbx_DataSearchTokens.Items.Add(strNodeName); //Add to the Search dropdown

                //Name
                strNodeName = dictFields[imagesData.name.field.ToString()]; //NAME
                childNode = CreateNode(imagesData.name.value.ToString(), false);
                childNode.Name = strNodeName;
                childNode.ToolTipText = strNodeName;
                childNode.Image = m_imgFlag_red;
                nodeTop.Nodes.Add(childNode);
                cbx_DataSearchTokens.Items.Add(strNodeName); //Add to the Search dropdown

                //ImageBackgrounds
                strNodeName = dictFields["_imagebackgrounds"]; //_imagebackgrounds
                RadTreeNode nodeBG = CreateNode(strNodeName, false);
                nodeBG.Name = strNodeName;
                nodeBG.ToolTipText = strNodeName;
                nodeBG.Image = m_imgFolder;
                treeviewData.Nodes.Add(nodeBG);

                foreach (dynamic imageItem_Backgrounds in imagesData.imageBackgrounds)
                {
                    strNodeName = dictFields[imageItem_Backgrounds.field.ToString()]; //IMAGE_BACKGROUND
                    childNode = CreateNode(imageItem_Backgrounds.value.ToString(), false);
                    childNode.Name = strNodeName;
                    childNode.ToolTipText = strNodeName;
                    childNode.Image = m_imgFlag_red;
                    nodeBG.Nodes.Add(childNode);
                }
                cbx_DataSearchTokens.Items.Add(strNodeName); //Add to the Search dropdown

                //ProductViews
                strNodeName = dictFields["_productviews"]; //ProductViews
                RadTreeNode nodePV = CreateNode(strNodeName, false);
                nodePV.Name = strNodeName;
                nodePV.ToolTipText = strNodeName;
                nodePV.Image = m_imgFolder;
                treeviewData.Nodes.Add(nodePV);

                foreach (dynamic imageItem_ProductViews in imagesData.productViews)
                {
                    strNodeName = dictFields[imageItem_ProductViews.field.ToString()]; //PRODUCT_VIEW
                    childNode = CreateNode(imageItem_ProductViews.value.ToString(), false);
                    childNode.Name = strNodeName;
                    childNode.ToolTipText = strNodeName;
                    childNode.Image = m_imgFlag_red;
                    nodePV.Nodes.Add(childNode);
                }
                cbx_DataSearchTokens.Items.Add(strNodeName); //Add to the Search dropdown

                //Credit
                strNodeName = dictFields[imagesData.credit.field.ToString()]; //CREDIT

                                                                              //childNode = CreateNode(imagesData.credit.value.ToString(), false);
                                                                              //childNode.Name = strNodeName;
                                                                              //childNode.ToolTipText = strNodeName;
                                                                              //childNode.Image = m_imgFlag_red;
                                                                              //nodeTop.Nodes.Add(childNode);

                RadTreeNode nodeCR = CreateNode(strNodeName, false);
                nodeCR.Text = imagesData.credit.value.ToString();
                nodeCR.Name = strNodeName;
                nodeCR.ToolTipText = strNodeName;
                nodeCR.Image = m_imgFolder;
                treeviewData.Nodes.Add(nodeCR);

                cbx_DataSearchTokens.Items.Add(strNodeName); //Add to the Search dropdown

                cbx_DataSearchTokens.Text = cnstAllFieldsDropdownText;

                return true;
            }
            catch (Exception ex)
            {
                //If we error, log the error and bail out now
                _logger.Log(String.Concat("ERROR at LoadDataTreeview, AN:", m_UserArticleNumber, ", CN:", m_UserChannel, ", BaseURL:", m_UserBaseURL, ", ex.Message:", ex.Message));
                return false;
            }
        }

        private Boolean LoadBOMTreeview()
        {

            Logger _logger = new Logger();

            dynamic BOMData = dynAPIFullDataResult.hits.hits[0]._source.BOM;

            try
            {
                treeviewBOM.Nodes.Clear();

                RadTreeNode nodeTop = CreateNode("Bill of Material", true);
                nodeTop.ToolTipText = "Bill of Material";
                nodeTop.Image = m_imgFolder;
                treeviewBOM.Nodes.Add(nodeTop);

                int idx = 0;
                foreach (dynamic BOMItem in BOMData)
                {

                    string strItemIndex = string.Concat("Item ", idx.ToString());

                    RadTreeNode childnodeBM = CreateNode(strItemIndex, false);
                    childnodeBM.ToolTipText = strItemIndex;
                    nodeTop.Nodes.Add(childnodeBM);

                    RadTreeNode childNode = CreateNode(BOMItem.component.ToString(), false);
                    childNode.ToolTipText = "Component";
                    childnodeBM.Nodes.Add(childNode);

                    childNode = CreateNode(BOMItem.quantity.ToString(), false);
                    childNode.ToolTipText = "Quantity";
                    childnodeBM.Nodes.Add(childNode);

                    childNode = CreateNode(BOMItem.label.ToString(), false);
                    childNode.ToolTipText = "Label";
                    childnodeBM.Nodes.Add(childNode);

                    childNode = CreateNode(BOMItem.id.ToString(), false);
                    childNode.ToolTipText = "ID";
                    childnodeBM.Nodes.Add(childNode);

                    idx += 1;
                }

                return true;
            }
            catch (Exception ex)
            {
                //If we error, log the error and bail out now
                _logger.Log(String.Concat("ERROR at LoadBOMTreeview, AN:", m_UserArticleNumber, ", CN:", m_UserChannel, ", BaseURL:", m_UserBaseURL, ", ex.Message:", ex.Message));
                return false;
            }
        }

        private Boolean XXXXXXXXXXXXXXXXXXXXXXBindTreeview(int intImagePID)
        {

            //Construct the TreeView from ds_ImageDataDataset ----------------------

            treeviewData.Nodes.Clear();
            treeviewData.DataSource = null;

            //ImageAssetOrder
            foreach (DataRow dbRow in ds_ImageDataDataset.Tables["dt_AO"].Rows)
            {
                if (!dbRow.IsNull("AO_pid"))
                {
                    if (dbRow["AO_Value"].ToString() == intImagePID.ToString()) //Only display tree data for the currently selected image
                    {
                        RadTreeNode node = CreateNode("Product Information", true);
                        node.ToolTipText = "Product Information";
                        node.Image = m_imgFolder;
                        treeviewData.Nodes.Add(node);
                        //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxRecursivelyPopulate(dbRow, node);
                    }
                }
            }

            //Bill of Material (BOM) - Parent Treeview Node
            foreach (DataRow dbRow_p in ds_ImageDataDataset.Tables["dt_BOM"].Rows)
            {
                if (dbRow_p["BOM_pid"].ToString() == intImagePID.ToString()) //Only display tree data for the currently selected image
                {
                    RadTreeNode node = CreateNode("Bill of Materials", true);
                    node.ToolTipText = "Bill of Materials";
                    node.Image = m_imgFolder;
                    treeviewData.Nodes.Add(node);

                    foreach (DataRow dbRow_c in ds_ImageDataDataset.Tables["dt_BOM"].Rows)
                    {
                        if (dbRow_c["BOM_pid"].ToString() == intImagePID.ToString()) //Only display tree data for the currently selected image
                        {
                            RadTreeNode childNode = CreateNode(dbRow_c["BOM_Children"].ToString(), false);
                            childNode.ToolTipText = "Children";
                            childNode.Image = m_imgFlag_red;
                            node.Nodes.Add(childNode);

                            childNode = CreateNode(dbRow_c["BOM_Component"].ToString(), false);
                            childNode.ToolTipText = "Component";
                            childNode.Image = m_imgFlag_green;
                            node.Nodes.Add(childNode);

                            childNode = CreateNode(dbRow_c["BOM_Id"].ToString(), false);
                            childNode.ToolTipText = "Id";
                            childNode.Image = m_imgFlag_blue;
                            node.Nodes.Add(childNode);

                            childNode = CreateNode(dbRow_c["BOM_Label"].ToString(), false);
                            childNode.ToolTipText = "Label";
                            childNode.Image = m_imgKeyHS;
                            node.Nodes.Add(childNode);

                            childNode = CreateNode(dbRow_c["BOM_Quantity"].ToString(), false);
                            childNode.ToolTipText = "Quantity";
                            childNode.Image = m_imgDocViewHS;
                            node.Nodes.Add(childNode);
                        }
                    }
                    break;
                }
            }

            return true;

        }

        private void xxxxxxxxxxxxxxxxxxxxRecursivelyPopulate(DataRow dbRow, RadTreeNode node)
        {

            //ImageTypes
            foreach (DataRow childRow in dbRow.GetChildRows("GlobalVar_IT"))
            {
                RadTreeNode childNode = CreateNode(childRow["IT_Value"].ToString(), true);
                childNode.ToolTipText = "Image Type";
                childNode.Image = m_imgFlag_red;
                node.Nodes.Add(childNode);
            }

            //PublicUrl
            foreach (DataRow childRow in dbRow.GetChildRows("GlobalVar_PU"))
            {
                RadTreeNode childNode = CreateNode(childRow["PU_Value"].ToString(), false);
                childNode.ToolTipText = "Public URL";
                childNode.Image = m_imgFlag_green;
                node.Nodes.Add(childNode);
            }

            //StandardizedArticleType
            foreach (DataRow childRow in dbRow.GetChildRows("GlobalVar_SAT"))
            {
                RadTreeNode childNode = CreateNode(childRow["SAT_Value"].ToString(), false);
                childNode.ToolTipText = "Standardized Article Type";
                childNode.Image = m_imgFlag_blue;
                node.Nodes.Add(childNode);
            }

            //ProductItemSourceMarket
            //ProductItemSourceMarketValues
            //ProductItemSourceMarketAvailability
            //ProductItemSourceMarketAvailabilityWeb
            //ProductItemSourceStandardizedSpecialOrder
            //ProductItemSourceWorldWideDistribution
            //ProductItemSourceStandardizedCountryDistribution
            //ProductItemSourceStandardizedCountryLaunchDate
            //ProductItemSourceChange

        }

        private RadTreeNode CreateNode(string text, bool expanded)
        {
            RadTreeNode node = new RadTreeNode(text, expanded);
            return node;
        }

        private void btn_LocateValue_Click(object sender, EventArgs e)
        {

            //<<<<<<<<<<<<<<<<<<<< TO DO  check user entry

            string strLocateField = cbx_DataSearchTokens.Text;
            string strLocateValue = tbx_LocateValue.Text;

            Telerik.WinControls.UI.RadTreeNodeCollection nodes = treeviewData.Nodes;
            treeviewData.SelectedNode = null;
            ClearDataTreeRecursive(nodes, false);
            SearchDataTreeRecursive(nodes, strLocateField, strLocateValue);
        }

        private bool SearchDataTreeRecursive(IEnumerable nodes, string strField, string strValue)
        {
            foreach (Telerik.WinControls.UI.RadTreeNode node in nodes)
            {
                if (node.Text.ToLower().Contains(strValue.ToLower()))
                {
                    if (cbx_DataSearchTokens.Text == cnstAllFieldsDropdownText)
                    {
                        treeviewData.SelectedNode = node;
                        node.BackColor = Color.Yellow;
                        node.Expanded = true;
                    }
                    else
                    {
                        if (node.Name.ToLower() == strField.ToLower())
                        {
                            treeviewData.SelectedNode = node;
                            node.BackColor = Color.Yellow;
                            node.Expanded = true;
                        }
                    }
                }

                if (SearchDataTreeRecursive(node.Nodes, strField, strValue))
                        return true;
             
            }
            return false;
        }

        private void btn_TreeLocateReset_Click(object sender, EventArgs e)
        {
            cbx_DataSearchTokens.Text = cnstAllFieldsDropdownText;
            tbx_LocateValue.Text = string.Empty;

            Telerik.WinControls.UI.RadTreeNodeCollection nodes = treeviewData.Nodes;
            ClearDataTreeRecursive(nodes, false);
        }

        private bool ClearDataTreeRecursive(IEnumerable nodes, Boolean bolCollapseAllNoes)
        {
            foreach (Telerik.WinControls.UI.RadTreeNode node in nodes)
            {
                node.BackColor = Color.White;
                node.BorderColor = Color.White;

                if (bolCollapseAllNoes)
                {
                    node.Expanded = false;
                };

            if (ClearDataTreeRecursive(node.Nodes, bolCollapseAllNoes))
                return true;
        }
            return false;
        }
    
    }
}
    #endregion

#region Data Classes ================================================================================================================================================

public class ImageList
{
    // Methods
    public ImageList(int ID, //ID
                     string FilePathName_Thumb, //Thumbnail Image File Path
                     string FilePathName_Full, //FilePathName
                     string ImageURL) //Image URL
    {
        this._ID = ID; //ID
        this._FilePathName_Thumb = FilePathName_Thumb; //Thumbnail Image Path
        this._FilePathName_Full = FilePathName_Full; //Full File Path Name
        this._ImageURL = ImageURL; //Image URL
    }

    // Properties 
    public int ID
    {
        get
        {
            return this._ID;
        }
        set
        {
            this._ID = value;
        }
    }

    public string FilePathName_Thumb
    {
        get
        {
            return this._FilePathName_Thumb;
        }
        set
        {
            this._FilePathName_Thumb = value;
        }
    }

    public string FilePathName_Full
    {
        get
        {
            return this._FilePathName_Full;
        }
        set
        {
            this._FilePathName_Full = value;
        }
    }

    public string ImageURL
    {
        get
        {
            return this._ImageURL;
        }
        set
        {
            this._ImageURL = value;
        }
    }

    private int _ID; //ID
    private string _FilePathName_Thumb; //Thumbnail Image Path
    private string _FilePathName_Full; //Full File Path Name
    private string _ImageURL; //Image URL
}

public class ImageData
{
    public ImageData(int pid,
                     string AO_Field, string AO_Value,
                     string IT_Code, string IT_Field, string IT_Value,
                     string PU_Field, string PU_Value,
                     string SAT_Code, string SAT_Field, string SAT_Value,
                     string PSM_Code, string PSM_Value)
    {
        //ImageAssetOrder
        this._pid = pid;
        this._AO_Field = AO_Field;
        this._AO_Value = AO_Value;

        //ImageType
        this._IT_Code = IT_Code;
        this._IT_Field = IT_Field;
        this._IT_Value = IT_Value;

        //ImagePublicUrl
        this._PU_Field = PU_Field;
        this._PU_Value = PU_Value;

        //StandardizedArticleType
        this._SAT_Code = SAT_Code;
        this._SAT_Field = SAT_Field;
        this._SAT_Value = SAT_Value;

        //ProductItemSourceMarket
        this._PSM_Code = PSM_Code;
        this._PSM_Value = PSM_Value;

        //ProductItemSourceMarket
        //ProductItemSourceMarketValues
        //ProductItemSourceMarketAvailability
        //ProductItemSourceMarketAvailabilityWeb
        //ProductItemSourceStandardizedSpecialOrder
        //ProductItemSourceWorldWideDistribution
        //ProductItemSourceStandardizedCountryDistribution
        //ProductItemSourceStandardizedCountryLaunchDate
        //ProductItemSourceChange

    }

    //ImageAssetOrder
    public int pid {get {return this._pid;} set {this._pid = value;}}
    public string AO_Field {get {return this._AO_Field;} set {this._AO_Field = value;}}
    public string AO_Value { get { return this._AO_Value; } set { this._AO_Value = value; } }

    //ImageType
    public string IT_Code { get { return this._IT_Code; } set { this._IT_Code = value; } }
    public string IT_Field { get { return this._IT_Field; } set { this._IT_Field = value; } }
    public string IT_Value { get { return this._IT_Value; } set { this._IT_Value = value; } }

    //ImagePublicUrl
    public string PU_Field { get { return this._PU_Field; } set { this._PU_Field = value; } }
    public string PU_Value { get { return this._PU_Value; } set { this._PU_Value = value; } }

    //StandardizedArticleType
    public string SAT_Code { get { return this._SAT_Code; } set { this._SAT_Code = value; } }
    public string SAT_Field { get { return this._SAT_Field; } set { this._SAT_Field = value; } }
    public string SAT_Value { get { return this._SAT_Value; } set { this._SAT_Value = value; } }

    //ProductItemSourceMarket
    public string PSM_Code { get { return this._PSM_Code; } set { this._PSM_Code = value; } }
    public string PSM_Value { get { return this._PSM_Value; } set { this._PSM_Value = value; } }

    //ProductItemSourceMarket
    //ProductItemSourceMarketValues
    //ProductItemSourceMarketAvailability
    //ProductItemSourceMarketAvailabilityWeb
    //ProductItemSourceStandardizedSpecialOrder
    //ProductItemSourceWorldWideDistribution
    //ProductItemSourceStandardizedCountryDistribution
    //ProductItemSourceStandardizedCountryLaunchDate
    //ProductItemSourceChange

    //public string xxx { get { return this._xxx; } set { this._xxx = value; } }

    //ImageAssetOrder
    private int _pid;
    private string _AO_Field;
    private string _AO_Value;

    //ImageType
    private string _IT_Code;
    private string _IT_Field;
    private string _IT_Value;

    //ImagePublicUrl
    private string _PU_Field;
    private string _PU_Value;

    //StandardizedArticleType
    private string _SAT_Code;
    private string _SAT_Field;
    private string _SAT_Value;

    //ProductItemSourceMarket
    private string _PSM_Code;
    private string _PSM_Value;

    //ProductItemSourceMarket
    //ProductItemSourceMarketValues
    //ProductItemSourceMarketAvailability
    //ProductItemSourceMarketAvailabilityWeb
    //ProductItemSourceStandardizedSpecialOrder
    //ProductItemSourceWorldWideDistribution
    //ProductItemSourceStandardizedCountryDistribution
    //ProductItemSourceStandardizedCountryLaunchDate
    //ProductItemSourceChange

}

#endregion