using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using RichemontApiLibrary;
using Telerik.WinControls.UI;

namespace CatalogViewer
{
    public partial class Form1 : Form
    {

        //================================================================
        //               *** KEEP THE VERSION HISTORY ***
        //================================================================
        public static string m_AppVersion_UI = "Version 1, Build 1, 11/20/2020";  //1. First release



        public static string m_CallMessage_Images = string.Empty;
        public static Boolean m_CallIsError_Images = false;

        public static TimeSpan m_tmeSpan_ServiceCall;
        public static TimeSpan m_tmeSpan_DLThread;
        public static TimeSpan m_tmeSpan_AppReady;

        public static DateTime dlTimeStart;
        public static Boolean m_TimerIsSec = false;

        public static int m_dlCountTotal = 0;
        public static int m_dlCount = 0;

        public static List<string> m_lstImageURLs;

        BindingList<ImageList> m_dsListView = new BindingList<ImageList>();
        BindingList<ImageData> m_dsImageData = new BindingList<ImageData>();

        //These delegates enable asynchronous calls
        public delegate void SetTimerLabelCallback(string strTimerMessage);
        public delegate void AppendListViewDatasetCallback(int idx, string strImageFilePathName_Thumb, string strImageFilePathName_Full, string strImageDownloadURL);
        public delegate void RefreshListViewCallback();
        public delegate void ListViewBeginUpdateCallback();
        public delegate void ListViewEndUpdateCallback();
        public delegate void UpdateCallMessageCallback(string strMessage, Boolean isError);
        public delegate void SetReadyIndicatorCallback(Boolean isVisible);
        public delegate void SetCallServiceControlsCallback(Boolean isCalling, Boolean bolAbortState);
        public delegate void UpdateDownloadProgressBarCallback(int value);

        DataSet ds_ImageDataDataset = new DataSet();

        public Thread _thread;

        public Form1()
        {
            InitializeComponent();

            lbl_AppVersion.Text = m_AppVersion_UI;
            SetupImageEditor();
            CreateImageDataset();
        }

        #region "Main Program ===================================================================================================================="

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

        private void btn_CallService_Click(object sender, EventArgs e)
        {
            //Check for Article Num entry =======================================================================

            string strArticleNum = string.Empty;
            if (cbx_ArticleNum.Text != string.Empty)
            {
                CallService(cbx_ArticleNum.Text);
            }
            else
            {
                cbx_ArticleNum.Focus();
                MessageBox.Show("A valid Article Number is required.", "Entry Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private void btn_Abort_Click(object sender, EventArgs e)
        {

            //Kill a previous thumb thread if still running =======================================================================
            try
            {
                if (_thread.ThreadState.Equals(ThreadState.Running))
                {
                    _thread.Abort();
                }
            }
            catch
            {
                //Do not throw - the thread doesn't exist
            }

            SetReadyIndicator(true); //Turn on the ready indicator
            SetCallingIndicator(false); //Turn off the calling indicator
            SetCallServiceControls(false, false); //Set the Call Service controls as Unlocked with Abort off
            UpdateCallMessage_ThreadedDone(cbx_ArticleNum.Text);
            UpdateTimers_ThreadedDone();
        }

    private void CallService(string strArticleNum)
        {

            //Kill a previous thumb thread if still running =======================================================================
            try
            {
                if(_thread != null)
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

            lbl_CallMessage.Text = string.Empty;
            lbl_CallMessage.ForeColor = Color.Green;
            lbl_Timings.Text = string.Empty;
            m_TimerIsSec = false;

            radTreeView1.Nodes.Clear();
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
            SetCallingIndicator(true); //Turn on the calling indicator

            if (CallService_AuxillaryData(cbx_ArticleNum.Text))
            {

                if (CallServiceImages())
                {
                    _thread = new Thread(() => CallServiceImages_Threaded(strArticleNum)) { IsBackground = false };

                    BindTreeview(0); //Before we start the thread, bind the Aux data treeview to the first image data which was loaded in CallService_Images sub

                    SetCallingIndicator(false); //Turn off the calling indicator (before starting the thread)

                    _thread.Start();
                }
                else
                {
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                }
            }
            else
            {
                //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            }

            DateTime tmeEnd = DateTime.Now;
            m_tmeSpan_AppReady = tmeEnd - tmeStart;

        }

        private void SetCallingIndicator(Boolean isVisible)
        {
            lbl_CallingFlag.Visible = isVisible;
            lbl_CallingFlag.Refresh();
        }

        private void SetCallServiceControls(Boolean isCalling, Boolean bolAbortState)
        {
            if (isCalling)
            {
                cbx_ArticleNum.Enabled = false;
                btn_CallService.Enabled = false;
                num_ImageFullSize.Enabled = false;
                pb_Download.Visible = true;
            }
            else
            {
                cbx_ArticleNum.Enabled = true;
                btn_CallService.Enabled = true;
                num_ImageFullSize.Enabled = true;
                pb_Download.Visible = false;
            }

            btn_Abort.Visible = bolAbortState;
        }

        private void SetReadyIndicator(Boolean isVisible)
        {
            lbl_ReadyIndicator.Visible = isVisible;
            lbl_ReadyIndicator.Refresh();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            var DialogResult = MessageBox.Show("Are you sure you want to exit Product Catalog Viewer?", "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (DialogResult == DialogResult.Yes)
            {
                this.Close();
            }
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
            if (radListView1.SelectedItems.Count >= 1)
            {
                string ImageFileNamePath = radListView1.Items[radListView1.SelectedIndex].Value.ToString();

                if (ImageFileNamePath != string.Empty)
                {
                    radImageEditor1.OpenImage(ImageFileNamePath);
                    BindTreeview(radListView1.SelectedIndex); //Filter to the selected image PID
                }
            }
        }
        private void lbl_Timings_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string strTimerMessage = string.Empty;

            if (m_TimerIsSec)
            {
                //Note: Converting double to integer lops off the fractional value
                int intServiceCall = Convert.ToInt32(m_tmeSpan_ServiceCall.TotalMilliseconds);
                int intDLThread = Convert.ToInt32(m_tmeSpan_DLThread.TotalMilliseconds);
                int intAppReady = Convert.ToInt32(m_tmeSpan_AppReady.TotalMilliseconds);
                strTimerMessage = String.Concat("Service Call:", intServiceCall.ToString("#,##0"), " ms, DL Thread:", intDLThread.ToString("#,##0"), " ms, App Ready:", intAppReady.ToString("#,##0"), " ms");
            }
            else
            {
                double dblServiceCall = m_tmeSpan_ServiceCall.TotalMilliseconds/1000;
                double dblDLThread = m_tmeSpan_DLThread.TotalMilliseconds/1000;
                double dblAppReady = m_tmeSpan_AppReady.TotalMilliseconds/1000;
                strTimerMessage = String.Concat("Service Call:", dblServiceCall.ToString("#.#"), " sec, DL Thread:", dblDLThread.ToString("#.#"), " sec, App Ready:", dblAppReady.ToString("#.#"), " sec");
            }

            m_TimerIsSec = !m_TimerIsSec; //Toggle m_TimerIsSec

            SetTimerLabelFromThread(strTimerMessage);
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

            //ImageAssetOrder
            DataColumn dc = new DataColumn(); dc.ColumnName = "AO_pid"; dc.DataType = typeof(int); dt_AO.Columns.Add(dc);
            DataColumn dc1 = new DataColumn(); dc1.ColumnName = "AO_Field"; dc1.DataType = typeof(string); dt_AO.Columns.Add(dc1);
            DataColumn dc2 = new DataColumn(); dc2.ColumnName = "AO_Value"; dc2.DataType = typeof(string); dt_AO.Columns.Add(dc2);
            ds_ImageDataDataset.Tables.Add(dt_AO);

            //ImageType
            DataColumn dc3 = new DataColumn(); dc3.ColumnName = "IT_id"; dc3.DataType = typeof(int); dt_IT.Columns.Add(dc3);
            DataColumn dc4 = new DataColumn(); dc4.ColumnName = "IT_Code"; dc4.DataType = typeof(string); dt_IT.Columns.Add(dc4);
            DataColumn dc5 = new DataColumn(); dc5.ColumnName = "IT_Field"; dc5.DataType = typeof(string); dt_IT.Columns.Add(dc5);
            DataColumn dc6 = new DataColumn(); dc6.ColumnName = "IT_Value"; dc6.DataType = typeof(string); dt_IT.Columns.Add(dc6);
            ds_ImageDataDataset.Tables.Add(dt_IT);

            //ImagePublicUrl
            DataColumn dc7 = new DataColumn(); dc7.ColumnName = "PU_id"; dc7.DataType = typeof(int); dt_PU.Columns.Add(dc7);
            DataColumn dc8 = new DataColumn(); dc8.ColumnName = "PU_Code"; dc8.DataType = typeof(string); dt_PU.Columns.Add(dc8);
            DataColumn dc9 = new DataColumn(); dc9.ColumnName = "PU_Field"; dc9.DataType = typeof(string); dt_PU.Columns.Add(dc9);
            DataColumn dc10 = new DataColumn(); dc10.ColumnName = "PU_Value"; dc10.DataType = typeof(string); dt_PU.Columns.Add(dc10);
            ds_ImageDataDataset.Tables.Add(dt_PU);

            //StandardizedArticleType
            DataColumn dc11 = new DataColumn(); dc11.ColumnName = "SAT_id"; dc11.DataType = typeof(int); dt_SAT.Columns.Add(dc11);
            DataColumn dc12 = new DataColumn(); dc12.ColumnName = "SAT_Code"; dc12.DataType = typeof(string); dt_SAT.Columns.Add(dc12);
            DataColumn dc13 = new DataColumn(); dc13.ColumnName = "SAT_Field"; dc13.DataType = typeof(string); dt_SAT.Columns.Add(dc13);
            DataColumn dc14 = new DataColumn(); dc14.ColumnName = "SAT_Value"; dc14.DataType = typeof(string); dt_SAT.Columns.Add(dc14);
            ds_ImageDataDataset.Tables.Add(dt_SAT);

            //ProductItemSourceMarket
            DataColumn dc15 = new DataColumn(); dc15.ColumnName = "PSM_id"; dc15.DataType = typeof(int); dt_PSM.Columns.Add(dc15);
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

            DataColumn parentColumn = ds_ImageDataDataset.Tables["dt_AO"].Columns["AO_pid"];
            DataColumn childColumn_IT = ds_ImageDataDataset.Tables["dt_IT"].Columns["IT_id"];
            DataColumn childColumn_PU = ds_ImageDataDataset.Tables["dt_PU"].Columns["PU_id"];
            DataColumn childColumn_SAT = ds_ImageDataDataset.Tables["dt_SAT"].Columns["SAT_id"];
            DataColumn childColumn_PSM = ds_ImageDataDataset.Tables["dt_PSM"].Columns["PSM_id"];

            // Create table Relations

            //Table AO is the parent table to all other tables

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

        private Boolean CallServiceImages()
        {

            // Set local variables =======================================================================

            string strImageFileNameOnly = String.Empty;
            string strImageFilePathName_Full = String.Empty;
            string strImageFilePathName_Thumb = String.Empty;
            string[] strImageFileNameSplit;
            string strImageDownloadURL = String.Empty;

            //Fetch the image thumbnail width from app.config
            string strThumbnailURLExtension = string.Concat("?impolicy=resize&width=", ConfigurationManager.AppSettings["MainProgramSettings.ImageThumbnailWidth"]);

            //Fetch the Image List from the Service =======================================================================

            DateTime tmeStart = DateTime.Now;
            m_lstImageURLs = GetProductImages(cbx_ArticleNum.Text); //Call the function that calls service to get the image URL's
            DateTime tmeEnd = DateTime.Now;
            m_tmeSpan_ServiceCall = tmeEnd - tmeStart;

            //Update the progressbar
            pb_Download.Value = 0;
            pb_Download.Maximum = m_lstImageURLs.Count();

            //Load the ListView with just the first image =======================================================================

            //Fetch the image download folder path/name from app.config
            string strImageDownloadFolder = ConfigurationManager.AppSettings["MainProgramSettings.ImageDownloadFolder"];

            if (m_lstImageURLs != null)
            {

                string strImageUrl = m_lstImageURLs[0]; //Note: Fetching image URL at index 0

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
                        //Derive a URL that includes the thumbnail resize command and download the resized image
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

                    //Download just the first full size image
                    try
                    {
                        client.DownloadFile(new Uri(strImageDownloadURL), strImageFilePathName_Full);
                    }
                    catch //Note: Any exception will do
                    {
                        strImageFilePathName_Full = GetNoImageAvailableImageFilePathName(false);
                    }

                    //We'll wait for the full size download to finish before updating the UI with the newly downloaded thumb
                    this.radListView1.BeginUpdate(); //Note: BeginUpdate and EndUpdate prevent auto scrolling as the full size image downloads
                    m_dsListView.Add(new ImageList(0, strImageFilePathName_Thumb, strImageFilePathName_Full, strImageDownloadURL));
                    this.radListView1.EndUpdate();

                    //Load the image Editor now to give the user something to look at
                    radImageEditor1.OpenImage(strImageFilePathName_Full);
                    radListView1.Refresh(); //Refresh the ListView after the full size image has completed download
                    radListView1.SelectedIndex = 0;

                    m_dlCount = 1;
                    pb_Download.Value = m_dlCount;

                }

                SetCallServiceControlsFromThread(true, true); //Set the Call Service controls as Locked with Abort On
                return true;
            }
            else
            {
                return false; //No images
            }
        }

        private void CallServiceImages_Threaded(string strArticleNum)
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

                for (int i = 1; i < m_lstImageURLs.Count - 1; i++) //Note: Starts FOR index at 1 to start after image URL 0
                {

                    string strImageUrl = m_lstImageURLs[i];

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

                    UpdateDownloadProgressBarFromThread(m_dlCount+1);

                }
            }

            DateTime tmeEnd = DateTime.Now; //End the time
            m_tmeSpan_DLThread = tmeEnd - tmeStart;

            UpdateCallMessage_ThreadedDone(strArticleNum);
            UpdateTimers_ThreadedDone();

            SetCallServiceControlsFromThread(false, false); //Set the Call Service controls as Unlocked with Abort off

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

        private void UpdateCallMessage_ThreadedDone(string strArticleNum)
        {
            //Update the call message - Note: We do this at the end of this thread because that's when all images are downloaded
            string strCallMessage = string.Empty;
            if (m_dlCount > 0)
            {
                int intCnt = m_dlCount;
                int intCntTotal = m_lstImageURLs.Count() - 1; //-1 because the image count is zero based
                strCallMessage = String.Concat("Returned ", intCnt.ToString(), " of ", intCntTotal.ToString(), " catalog images for article ", strArticleNum);
                UpdateCallMessageFromThread(strCallMessage, false);
            }
            else
            {
                strCallMessage = String.Concat("No catalog information is available for article ", strArticleNum);
                UpdateCallMessageFromThread(strCallMessage, true);
            }

            SetReadyIndicatorFromThread(true); //Turn on the ready indicator

        }

        private void UpdateTimers_ThreadedDone()
        {
            //Note: Converting double to integer lops off the fractional value
            int intServiceCall = Convert.ToInt32(m_tmeSpan_ServiceCall.TotalMilliseconds);
            int intDLThread = Convert.ToInt32(m_tmeSpan_DLThread.TotalMilliseconds);
            int intAppReady = Convert.ToInt32(m_tmeSpan_AppReady.TotalMilliseconds);
            string strTimerMessage = String.Concat("Service Call:", intServiceCall.ToString("#,##0"), " ms, DL Thread:", intDLThread.ToString("#,##0"), " ms, App Ready:", intAppReady.ToString("#,##0"), " ms");
            SetTimerLabelFromThread(strTimerMessage);
        }

        private void ListViewBeginUpdateFromThread()
        {
            if (this.radListView1.InvokeRequired)
            {
                ListViewBeginUpdateCallback d = new ListViewBeginUpdateCallback(ListViewBeginUpdateFromThread);
            }
            else
            {
                this.radListView1.BeginUpdate(); //Note: BeginUpdate and EndUpdate prevent auto scrolling as the image list builds
            }
        }

        private void AppendListViewDatasetFromThread(int idx, string strImageFilePathName_Thumb, string strImageFilePathName_Full, string strImageDownloadURL)
        {
            if (this.radListView1.InvokeRequired)
            {
                AppendListViewDatasetCallback d = new AppendListViewDatasetCallback(AppendListViewDatasetFromThread);
                this.Invoke(d, new object[] { idx, strImageFilePathName_Thumb, strImageFilePathName_Full, strImageDownloadURL });
            }
            else
            {
                this.radListView1.BeginUpdate(); //Note: BeginUpdate and EndUpdate prevent auto scrolling as the image list builds
                m_dsListView.Add(new ImageList(idx, strImageFilePathName_Thumb, strImageFilePathName_Full, strImageDownloadURL));
                this.radListView1.EndUpdate();
            }
        }

        private void ListViewEndUpdateFromThread()
        {
            if (this.radListView1.InvokeRequired)
            {
                ListViewEndUpdateCallback d = new ListViewEndUpdateCallback(ListViewEndUpdateFromThread);
            }
            else
            {
                this.radListView1.EndUpdate(); //Note: BeginUpdate and EndUpdate prevent auto scrolling as the image list builds
            }
        }

        private void RefreshListViewFromThread()
        {
            if (this.radListView1.InvokeRequired)
            {
                RefreshListViewCallback d = new RefreshListViewCallback(RefreshListViewFromThread);
                this.Invoke(d, new object[] { });
            }
            else
            {
                radListView1.Refresh();
            }
        }

        private void UpdateCallMessageFromThread(string strMessage, Boolean isError)
        {
            if (this.lbl_CallMessage.InvokeRequired)
            {
                UpdateCallMessageCallback d = new UpdateCallMessageCallback(UpdateCallMessageFromThread);
                this.Invoke(d, new object[] { strMessage, isError });
            }
            else
            {
                lbl_CallMessage.Text = strMessage;
                if (isError)
                {
                    lbl_CallMessage.ForeColor = Color.Red;
                }
                else
                {
                    lbl_CallMessage.ForeColor = Color.Green;
                }
            }
        }

        private void SetTimerLabelFromThread(string strTimerMessage)
        {
            if (this.lbl_Timings.InvokeRequired)
            {
                SetTimerLabelCallback d = new SetTimerLabelCallback(SetTimerLabelFromThread);
                this.Invoke(d, new object[] { strTimerMessage });
            }
            else
            {
                lbl_Timings.Text = strTimerMessage;
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

        private void UpdateDownloadProgressBarFromThread(int value)
        {
            if (this.pb_Download.InvokeRequired)
            {
                UpdateDownloadProgressBarCallback d = new UpdateDownloadProgressBarCallback(UpdateDownloadProgressBarFromThread);
                this.Invoke(d, new object[] { value });
            }
            else
            {
                pb_Download.Value = value;
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
                    btn_CallService.Enabled = false;
                    btn_Abort.Visible = true;
                    num_ImageFullSize.Enabled = false;
                    pb_Download.Visible = true;
                }
                else
                {
                    cbx_ArticleNum.Enabled = true;
                    btn_CallService.Enabled = true;
                    num_ImageFullSize.Enabled = true;
                    pb_Download.Visible = false;
                }

                btn_Abort.Visible = bolAbortState;

            }
        }

        private List<string> GetProductImages(string strArticleNum)
        {
            try
            {
                var apiService = new RichemontApiLibrary.ProductCatalogApi();
                var request = new RichemontApiLibrary.Models.ProductCatalog.Queries.StandardizedProduct.Request
                {
                    Article = strArticleNum,
                    Brand = "CAR",
                    Channel = "WEB",
                    Language = "en",
                    FromResult = 0,
                    PageSize = 10
                };

                var images = apiService.GetStandardizedProductImages(request);
                return images;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
                return null;
            }

        }

        private void radListView1_SelectedItemChanged(object sender, EventArgs e)
        {
            if (radListView1.SelectedItems.Count >= 1)
            {
                try
                {
                    string ImageFileNamePath = radListView1.Items[radListView1.SelectedIndex].Value.ToString();
                    radImageEditor1.OpenImage(ImageFileNamePath);

                    BindTreeview(radListView1.SelectedIndex); //Filter to the selected image PID
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
            radListView1.ItemDataBound += new Telerik.WinControls.UI.ListViewItemEventHandler(radListView1_ItemDataBound);
            radListView1.VisualItemFormatting += new Telerik.WinControls.UI.ListViewVisualItemEventHandler(radListView1_VisualItemFormatting);
            radListView1.CellFormatting += new Telerik.WinControls.UI.ListViewCellFormattingEventHandler(radListView1_CellFormatting);
            radListView1.ColumnCreating += new ListViewColumnCreatingEventHandler(radListView1_ColumnCreating);

            radListView1.ShowColumnHeaders = false;

            radListView1.AllowArbitraryItemWidth = true;
            radListView1.AllowArbitraryItemHeight = true;

            radListView1.ViewType = ListViewType.IconsView;
            //            ((IconListViewElement)this.radListView1.ListViewElement.ViewElement).Orientation = Orientation.Vertical;

            radListView1.AllowEdit = false;
            radListView1.AllowRemove = false;

            radTreeView1.TreeViewElement.ShowNodeToolTips = true;

            radListView1.DataSource = m_dsListView;
            radListView1.DisplayMember = "FilePathName_Thumb";
            radListView1.ValueMember = "FilePathName_Full";
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

        private Boolean CallService_AuxillaryData(string strArticleNum)
        {

            //ds_ImageData.Add(new ImageData(1, //ID
            //                               "ASSET_ORDER", "1", //ImageAssetOrder
            //                               "DAM_MTYP_IMAGE", "IMAGE_TYPE", "Image", //ImageType
            //                               "DAM_MTYP_IMAGE", "akamai:default", @"https://mediastorage.richemont.com/damdatastore/QuA/car/11/72/1172505.png") //ImagePublicUrl


            //Fetch the Auxillary Data and Load the Treeview =======================================================================
            ds_ImageDataDataset.Clear();

            try
            {
                var apiService = new RichemontApiLibrary.ProductCatalogApi();
                var request = new RichemontApiLibrary.Models.ProductCatalog.Queries.StandardizedProduct.Request
                {
                    Article = strArticleNum,
                    Brand = "CAR",
                    Channel = "WEB",
                    Language = "en",
                    FromResult = 0,
                    PageSize = 10
                };

                var result = apiService.GetStandardizedProduct(request);
                if (result == null)
                {
                    //If we get no data back bail out now - calling sub handles UI message
                    return false;
                }

                int id = 0;
                foreach (var HitItem in result.Hits.Hits)
                {
                    var ImageItem = HitItem.Source.Images;
                    foreach (var imgItem in ImageItem)
                    {

                        //ImageAssetOrder
                        DataRow dr_AO = ds_ImageDataDataset.Tables["dt_AO"].NewRow();
                        dr_AO[0] = id; //To "pid" datacolumn
                        dr_AO[1] = imgItem.AssetOrder.Field;
                        dr_AO[2] = imgItem.AssetOrder.Value;
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



                        id += 1;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
                //<<<<<<<<<<<<<<<<<<<<<<<<<<<<< UI message
                return false;
            }

            return true;

        }

        private Boolean BindTreeview(int intImagePID)
        {

            //Construct the TreeView from ds_ImageDataDataset ----------------------

            radTreeView1.Nodes.Clear();
            radTreeView1.DataSource = null;

            //ImageAssetOrder
            foreach (DataRow dbRow in ds_ImageDataDataset.Tables["dt_AO"].Rows)
            {
                if (!dbRow.IsNull("AO_pid"))
                {
                    if (dbRow["AO_Value"].ToString() == intImagePID.ToString())
                    {
                        RadTreeNode node = CreateNode(dbRow["AO_Value"].ToString(), true, dbRow["AO_pid"].ToString());
                        node.ToolTipText = "Asset Order";
                        radTreeView1.Nodes.Add(node);
                        RecursivelyPopulate(dbRow, node);
                    }
                }
            }

            return true;

        }

        private void RecursivelyPopulate(DataRow dbRow, RadTreeNode node)
        {
            //ImageTypes
            foreach (DataRow childRow in dbRow.GetChildRows("GlobalVar_IT"))
            {
                RadTreeNode childNode = CreateNode(childRow["IT_Value"].ToString(), true, childRow["IT_id"].ToString());
                childNode.ToolTipText = "Image Type";
                node.Nodes.Add(childNode);
            }

            //PublicUrl
            foreach (DataRow childRow in dbRow.GetChildRows("GlobalVar_PU"))
            {
                RadTreeNode childNode = CreateNode(childRow["PU_Value"].ToString(), false, childRow["PU_id"].ToString());
                childNode.ToolTipText = "Public URL";
                node.Nodes.Add(childNode);
            }

            //StandardizedArticleType
            foreach (DataRow childRow in dbRow.GetChildRows("GlobalVar_SAT"))
            {
                RadTreeNode childNode = CreateNode(childRow["SAT_Value"].ToString(), false, childRow["SAT_id"].ToString());
                childNode.ToolTipText = "Standardized Article Type";
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

        private RadTreeNode CreateNode(string text, bool expanded, string id)
        {
            RadTreeNode node = new RadTreeNode(text, expanded);
            return node;
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