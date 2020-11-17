using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RichemontApiLibrary;
using Telerik.WinControls.UI;

namespace CatalogViewer
{
    public partial class Form1 : Form
    {

        public static string m_CallMessage_Images = string.Empty;
        public static Boolean m_CallIsError_Images = false;

        BindingList<ImageList> ds_ListView = new BindingList<ImageList>();
        BindingList<ImageData> ds_ImageData = new BindingList<ImageData>();

        DataSet ds_ImageDataDataset = new DataSet();

        public Form1()
        {
            InitializeComponent();
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

            //Prep the Controls =======================================================================

            lbl_CallMessage.Text = string.Empty;
            lbl_CallMessage.ForeColor = Color.Black;

            radTreeView1.Nodes.Clear();
            radImageEditor1.CurrentBitmap = null;
            radImageEditor1.Refresh();

            if (CallService_Images())
            {
                radListView1.SelectedIndex = 0;
                if (CallService_Images_Delayed())
                {
                    if (CallService_AuxillaryData(tbx_ArticleNum.Text))
                    {
                        if (BindTreeview(0)) //Filter to the first image
                        {
                            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                        }
                    }
                }
            }
            else
            {
                //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            }

            //Display the image call message
            if (m_CallIsError_Images)
            {
                lbl_CallMessage.ForeColor = Color.Red;
            }
            lbl_CallMessage.Text = m_CallMessage_Images;
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            var DialogResult = MessageBox.Show("Are you sure you want to exit this application?", "Confirm", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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

        #endregion

        #region "Data Structures ===================================================================================================================="
        private void CreateImageDataset()
        {

            DataTable dt_AO = new DataTable();
            dt_AO.TableName = "dt_AO";

            DataTable dt_IT = new DataTable();
            dt_IT.TableName = "dt_IT";

            DataTable dt_PU = new DataTable();
            dt_PU.TableName = "dt_PU";

            DataTable dt_SAT = new DataTable();
            dt_SAT.TableName = "dt_SAT";

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

            ////StandardizedArticleType
            //DataColumn dc11 = new DataColumn(); dc11.ColumnName = "SAT_id"; dc11.DataType = typeof(int); dt_SAT.Columns.Add(dc11);
            //DataColumn dc12 = new DataColumn(); dc12.ColumnName = "SAT_Code"; dc12.DataType = typeof(string); dt_SAT.Columns.Add(dc12);
            //DataColumn dc13 = new DataColumn(); dc13.ColumnName = "SAT_Field"; dc13.DataType = typeof(string); dt_SAT.Columns.Add(dc13);
            //DataColumn dc14 = new DataColumn(); dc14.ColumnName = "SAT_Value"; dc14.DataType = typeof(string); dt_SAT.Columns.Add(dc14);
            //ds_ImageDataDataset.Tables.Add(dt_SAT);

            DataColumn parentColumn = ds_ImageDataDataset.Tables["dt_AO"].Columns["AO_pid"];
            DataColumn childColumn_IT = ds_ImageDataDataset.Tables["dt_IT"].Columns["IT_id"];
            DataColumn childColumn_PU = ds_ImageDataDataset.Tables["dt_PU"].Columns["PU_id"];
            //DataColumn childColumn_SAT = ds_ImageDataDataset.Tables["dt_SAT"].Columns["SAT_id"];

            // Create table Relations

            //Table AO is the parent table to all other tables

            //Table IT is a child to table AO
            DataRelation drGlobalVar_IT;
            drGlobalVar_IT = new DataRelation("GlobalVar_IT", parentColumn, childColumn_IT);
            ds_ImageDataDataset.Relations.Add(drGlobalVar_IT);

            //Table PU is a child to table AO
            DataRelation drGlobalVar_PU;
            drGlobalVar_PU = new DataRelation("GlobalVar_PU", parentColumn, childColumn_PU);
            ds_ImageDataDataset.Relations.Add(drGlobalVar_PU);

            ////Table SAT is a child to table AO
            //DataRelation drGlobalVar_SAT;
            //drGlobalVar_SAT = new DataRelation("GlobalVar_SAT", parentColumn, childColumn_SAT);
            //ds_ImageDataDataset.Relations.Add(drGlobalVar_SAT);

        }

        #endregion

        #region "Images ===================================================================================================================="

        private Boolean CallService_Images()
        {

            string strImageFileNameOnly = String.Empty;
            string strImageFilePathName_Full = String.Empty;
            string strImageFilePathName_Thumb = String.Empty;
            string[] strImageFileNameSplit;
            string strImageDownloadURL = String.Empty;

            //Fetch the image download folder from app.config
            string strImageDownloadFolder = ConfigurationManager.AppSettings["MainProgramSettings.ImageDownloadFolder"];

            //Check if image download folder exists and if not create the folder
            if (!Directory.Exists(strImageDownloadFolder))
            {
                Directory.CreateDirectory(strImageDownloadFolder);
            }

            //Fetch the image thumbnail width from app.config as integer
            string strThumbnailURLExtension = string.Concat("?impolicy=resize&width=", ConfigurationManager.AppSettings["MainProgramSettings.ImageThumbnailWidth"]);

            ds_ListView.Clear();

            //Fetch the Image List and Load the ListView =======================================================================

            lbl_CallingFlag.Visible = true;
            List<string> lstImageURLs = GetProductImages(tbx_ArticleNum.Text); //Call the function that calls service to get the image URL's
            lbl_CallingFlag.Visible = false;
            lbl_CallingFlag.Refresh();

            if (lstImageURLs != null)
            {

                LoadListView();

                int idx = 0;
                foreach (string strImageUrl in lstImageURLs)
                {
                    using (WebClient client = new WebClient())
                    {
                        //Derive file names for the local copies
                        strImageFileNameOnly = Path.GetFileName(strImageUrl); //Full size file name only
                        strImageFilePathName_Full = String.Concat(strImageDownloadFolder, strImageFileNameOnly); //Full size file path/name

                        strImageFileNameSplit = strImageFileNameOnly.Split('.');
                        strImageFilePathName_Thumb = String.Concat(strImageDownloadFolder, strImageFileNameSplit[0], "_Thumb.", strImageFileNameSplit[1]); //Thumbnail file path/name

                        //Thumbnail image download------------------------

                        //Derive a URL that includes the thumbnail resise command and download the resized image
                        client.DownloadFile(new Uri(string.Concat(strImageUrl, strThumbnailURLExtension)), strImageFilePathName_Thumb);

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

                        //Full Size image download ------------------------
                        if (idx == 1)
                        {
                            //Download just the first full size image
                            client.DownloadFile(new Uri(strImageDownloadURL), strImageFilePathName_Full);

                            //Load the image Editor now to give the user something to do
                            radImageEditor1.OpenImage(strImageFilePathName_Full);
                        }

                        this.radListView1.BeginUpdate(); //Note: BeginUpdate and EndUpdate prevent auto scrolling as the image list builds
                        ds_ListView.Add(new ImageList(idx, strImageFilePathName_Thumb, strImageFilePathName_Full, strImageDownloadURL));
                        this.radListView1.EndUpdate();

                        radListView1.Refresh();
                    }

                    int msgIdx = (idx + 1);
                    m_CallMessage_Images = String.Concat(msgIdx.ToString(), " Catalog Images Were Returned for Article Number ", tbx_ArticleNum.Text);
                    m_CallIsError_Images = false;

                    idx += 1;

                }

                return true;
            }
            else
            {
                m_CallMessage_Images = String.Concat("No Catalog Information is Available for Article Number ", tbx_ArticleNum.Text);
                m_CallIsError_Images = true;

                return false; //No images
            }

        }

        private Boolean CallService_Images_Delayed()
        {
            int idx = 0;
            foreach (ImageList item in ds_ListView)
            {
                using (WebClient client = new WebClient())
                {
                    if (idx > 0)
                    {
                        //Note: Async doesn't lock up the UI
                        client.DownloadFileAsync(new Uri(item.ImageURL), item.FilePathName_Full);
                    }
                }
                idx += 1;
            }

            return true;
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

        private void radListView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (radListView1.SelectedItems.Count >= 1)
            {
                string ImageFileNamePath = radListView1.Items[radListView1.SelectedIndex].Value.ToString();
                radImageEditor1.OpenImage(ImageFileNamePath);

                BindTreeview(radListView1.SelectedIndex); //Filter to the selected image PID
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
            ((IconListViewElement)this.radListView1.ListViewElement.ViewElement).Orientation = Orientation.Vertical;

            radListView1.AllowEdit = false;
            radListView1.AllowRemove = false;

            radTreeView1.TreeViewElement.ShowNodeToolTips = true;

            radListView1.DataSource = ds_ListView;
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

            //xxxds_ImageData.Clear();
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
                    //If we get no data back bail out now
                    //<<<<<<<<<<<<<<<<<<<<<<<<<<<<< UI message
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
            foreach (DataRow childRow in dbRow.GetChildRows("GlobalVar_IT"))
            {
                RadTreeNode childNode = CreateNode(childRow["IT_Value"].ToString(), true, childRow["IT_id"].ToString());
                childNode.ToolTipText = "Image Type";
                node.Nodes.Add(childNode);
            }

            foreach (DataRow childRow in dbRow.GetChildRows("GlobalVar_PU"))
            {
                RadTreeNode childNode = CreateNode(childRow["PU_Value"].ToString(), false, childRow["PU_id"].ToString());
                childNode.ToolTipText = "Public URL";
                node.Nodes.Add(childNode);
            }
        }

        private RadTreeNode CreateNode(string text, bool expanded, string id)
        {
            RadTreeNode node = new RadTreeNode(text, expanded);
            return node;
        }


    }

    #endregion

}

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
                     string PU_Field, string PU_Value)//,
                     //string SAT_Code, string SAT_Field, string SAT_Value)
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

        ////StandardizedArticleType
        //this._SAT_Code = SAT_Code;
        //this._SAT_Field = SAT_Field;
        //this._SAT_Value = SAT_Value;
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

    ////StandardizedArticleType
    //public string SAT_Code { get { return this._SAT_Code; } set { this._SAT_Code = value; } }
    //public string SAT_Field { get { return this._SAT_Field; } set { this._SAT_Field = value; } }
    //public string SAT_Value { get { return this._SAT_Value; } set { this._SAT_Value = value; } }

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

    ////StandardizedArticleType
    //private string _SAT_Code;
    //private string _SAT_Field;
    //private string _SAT_Value;

}

#endregion