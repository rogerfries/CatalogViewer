using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public static Boolean m_UserImageViewerDetailPanalState;
        public static int m_UserImageViewerSplitterDistance;

        BindingList<ImageList> ds_ListView = new BindingList<ImageList>();
        BindingList<ImageData> ds_ImageData = new BindingList<ImageData>();

        DataSet ds_ImageDataDataset = new DataSet();

        public Form1()
        {
            InitializeComponent();

            SetupImageEditor();

            CreateImageDataset();
        }

        private void SetupImageEditor()
        {
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

        private void LoadListView()
        { 
            radListView1.ItemDataBound += new Telerik.WinControls.UI.ListViewItemEventHandler(radListView1_ItemDataBound);
            radListView1.VisualItemFormatting += new Telerik.WinControls.UI.ListViewVisualItemEventHandler(radListView1_VisualItemFormatting);
            //radListView1.CellFormatting += new Telerik.WinControls.UI.ListViewCellFormattingEventHandler(radListView1_CellFormatting);
            radListView1.ColumnCreating += new ListViewColumnCreatingEventHandler(radListView1_ColumnCreating);
            radListView1.ViewTypeChanged += new EventHandler(radListView1_ViewTypeChanged);
            radListView1.ShowColumnHeaders = false;

            radListView1.AllowArbitraryItemHeight = true;
            radListView1.AllowArbitraryItemWidth = true;
            radListView1.ViewType = ListViewType.IconsView;

            radListView1.AllowEdit = false;
            radListView1.AllowRemove = false;
            radListView1.DataSource = ds_ListView;
            radListView1.DisplayMember = "Image";
            radListView1.ValueMember = "FilePath";
        }

        void radListView1_ItemDataBound(object sender, Telerik.WinControls.UI.ListViewItemEventArgs e)
        {
            try
            {
                string ImagePath = ((ImageList)e.Item.DataBoundItem).ImagePath;

                using (MemoryStream ms = new MemoryStream())
                using (FileStream file = new FileStream(ImagePath, FileMode.Open, FileAccess.Read))
                {
                    byte[] bytes = new byte[file.Length];
                    file.Read(bytes, 0, (int)file.Length);
                    e.Item.Image = Image.FromStream(new MemoryStream(bytes)).GetThumbnailImage(135, 185, null, new IntPtr());
                }
        }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }

        }

        void radListView1_VisualItemFormatting(object sender, Telerik.WinControls.UI.ListViewVisualItemEventArgs e)
        {
            //if (e.VisualItem.Data.Image != null)
            //{
            //    e.VisualItem.Image = e.VisualItem.Data.Image.GetThumbnailImage(32, 32, null, IntPtr.Zero);
            //    e.VisualItem.Layout.RightPart.Margin = new Padding(2, 0, 0, 0);
            //}
            //if (this.radListView1.ViewType == Telerik.WinControls.UI.ListViewType.IconsView && e.VisualItem.Data.DataBoundItem != null)
            //{
            //    string albumName = ((MusicCollectionDataSet.SongsDataTableRow)(((System.Data.DataRowView)(e.VisualItem.Data.DataBoundItem)).Row)).AlbumName;
            //    string artisName = ((MusicCollectionDataSet.SongsDataTableRow)(((System.Data.DataRowView)(e.VisualItem.Data.DataBoundItem)).Row)).ArtistName;
            //    string songName = ((MusicCollectionDataSet.SongsDataTableRow)(((System.Data.DataRowView)(e.VisualItem.Data.DataBoundItem)).Row)).SongName;
            //    e.VisualItem.Text = "<html> " + songName + "<br><span style=\"color:#999999\"> " + artisName + "<br> " + albumName + "</span>";
            //}
        }

        void radListView1_CellFormatting(object sender, ListViewCellFormattingEventArgs e)
        {
            //if (e.CellElement.Image != null)
            //{
            //    e.CellElement.Image = e.CellElement.Image.GetThumbnailImage(32, 32, null, IntPtr.Zero);
            //}
        }

        void radListView1_ColumnCreating(object sender, ListViewColumnCreatingEventArgs e)
        {
            //if (e.Column.FieldName == "ID" || e.Column.FieldName == "FilePath")
            //{
            //    e.Column.Visible = false;
            //}
            //if (e.Column.FieldName == "Image")
            //{
            //    e.Column.HeaderText = "Image";
            //    e.Column.Visible = true;
            //}
        }

        private void SetupDetailsView()
        {
            //this.radListView1.AllowArbitraryItemHeight = true;
        }

        private void SetupIconsView()
        {
            //this.radListView1.ItemSize = new Size(200, 64);
            //this.radListView1.ItemSpacing = 5;
            //this.radListView1.AllowArbitraryItemHeight = true;
        }

        private void SetupSimpleListView()
        {
            this.radListView1.AllowArbitraryItemHeight = true;
        }

        void radListView1_ViewTypeChanged(object sender, EventArgs e)
        {
            //switch (radListView1.ViewType)
            //{
            //    case ListViewType.ListView:
            //        SetupSimpleListView();
            //        break;
            //    case ListViewType.IconsView:
            //        SetupIconsView();
            //        break;
            //    case ListViewType.DetailsView:
            //        SetupDetailsView();
            //        break;
            //}
        }

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
            DataColumn dc = new DataColumn(); dc.ColumnName = "pid"; dc.DataType = typeof(int); dt_AO.Columns.Add(dc);
            DataColumn dc1 = new DataColumn(); dc1.ColumnName = "AO_Field"; dc1.DataType = typeof(string); dt_AO.Columns.Add(dc1);
            DataColumn dc2 = new DataColumn(); dc2.ColumnName = "AO_Value"; dc2.DataType = typeof(string); dt_AO.Columns.Add(dc2);
            ds_ImageDataDataset.Tables.Add(dt_AO);

            //ImageType
            DataColumn dc3 = new DataColumn(); dc3.ColumnName = "id"; dc3.DataType = typeof(int); dt_IT.Columns.Add(dc3);
            DataColumn dc4 = new DataColumn(); dc4.ColumnName = "IT_Code"; dc4.DataType = typeof(string); dt_IT.Columns.Add(dc4);
            DataColumn dc5 = new DataColumn(); dc5.ColumnName = "IT_Field"; dc5.DataType = typeof(string); dt_IT.Columns.Add(dc5);
            DataColumn dc6 = new DataColumn(); dc6.ColumnName = "IT_Value"; dc6.DataType = typeof(string); dt_IT.Columns.Add(dc6);
            ds_ImageDataDataset.Tables.Add(dt_IT);

            //ImagePublicUrl
            DataColumn dc7 = new DataColumn(); dc7.ColumnName = "id"; dc7.DataType = typeof(int); dt_PU.Columns.Add(dc7);
            DataColumn dc8 = new DataColumn(); dc8.ColumnName = "PU_Code"; dc8.DataType = typeof(string); dt_PU.Columns.Add(dc8);
            DataColumn dc9 = new DataColumn(); dc9.ColumnName = "PU_Field"; dc9.DataType = typeof(string); dt_PU.Columns.Add(dc9);
            DataColumn dc10 = new DataColumn(); dc10.ColumnName = "PU_Value"; dc10.DataType = typeof(string); dt_PU.Columns.Add(dc10);
            ds_ImageDataDataset.Tables.Add(dt_PU);

            //StandardizedArticleType
            DataColumn dc11 = new DataColumn(); dc11.ColumnName = "id"; dc11.DataType = typeof(int); dt_SAT.Columns.Add(dc11);
            DataColumn dc12 = new DataColumn(); dc12.ColumnName = "SAT_Code"; dc12.DataType = typeof(string); dt_SAT.Columns.Add(dc12);
            DataColumn dc13 = new DataColumn(); dc13.ColumnName = "SAT_Field"; dc13.DataType = typeof(string); dt_SAT.Columns.Add(dc13);
            DataColumn dc14 = new DataColumn(); dc14.ColumnName = "SAT_Value"; dc14.DataType = typeof(string); dt_SAT.Columns.Add(dc14);
            ds_ImageDataDataset.Tables.Add(dt_SAT);

            DataColumn parentColumn = ds_ImageDataDataset.Tables["dt_AO"].Columns["pid"];
            DataColumn childColumn_IT = ds_ImageDataDataset.Tables["dt_IT"].Columns["id"];
            DataColumn childColumn_PU = ds_ImageDataDataset.Tables["dt_PU"].Columns["id"];
            DataColumn childColumn_SAT = ds_ImageDataDataset.Tables["dt_SAT"].Columns["id"];

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

            //Table SAT is a child to table AO
            DataRelation drGlobalVar_SAT;
            drGlobalVar_SAT = new DataRelation("GlobalVar_SAT", parentColumn, childColumn_SAT);
            ds_ImageDataDataset.Relations.Add(drGlobalVar_SAT);

        }

        private void btn_CallService_Click(object sender, EventArgs e)
        {
            //Prep the Controls =======================================================================

            lbl_Working.Visible = true;

            radImageEditor1.Enabled = false;
            radImageEditor1.CurrentBitmap = null;

            ds_ListView.Clear();

            //Load Image List =======================================================================

            List<string> data = GetProductImages(tbx_ArticleNum.Text);
            if (data != null)
            {
                int idx = 0;
                foreach (var imageUrl in data)
                {
                    idx += 1;
                    using (WebClient client = new WebClient())
                    {
                        string strImageFileName = String.Concat(@"c:\temp\", Path.GetFileName(imageUrl));
                        client.DownloadFile(new Uri(imageUrl), strImageFileName);
                        //client.DownloadFileAsync(new Uri(imageUrl), strImageFileName);

                        ds_ListView.Add(new ImageList(idx, strImageFileName, strImageFileName));

                    }

                    //Bail out for development
                    if (numericUpDown1.Value > 0)
                    {
                        if (idx >= numericUpDown1.Value)
                        {
                            break;
                        }
                    }

                }

                LoadListView();
            }
            else
            {
                //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
            }

            //Fetch Auxillary Data =======================================================================

            ds_ImageData.Clear();
            ds_ImageData.Add(new ImageData(1, //ID
                                           "ASSET_ORDER", "1", //ImageAssetOrder
                                           "DAM_MTYP_IMAGE", "IMAGE_TYPE", "Image", //ImageType
                                           "DAM_MTYP_IMAGE", "akamai:default", @"https://mediastorage.richemont.com/damdatastore/QuA/car/11/72/1172505.png", //ImagePublicUrl
                                           "CA01", "PIM:CARTIER:CRE_ARTICLE_TYPE", "CA Finished goods")); //StandardizedArticleType


            ds_ImageDataDataset.Clear();

            //Populate ds_ImageDataDataset
            int id = 0;
            foreach (ImageData obj in ds_ImageData)
            {
                id += 1;

                DataRow dr_AO = ds_ImageDataDataset.Tables["dt_AO"].NewRow();
                dr_AO[0] = id;
                dr_AO[1] = obj.AO_Field;
                dr_AO[2] = obj.AO_Value;
                ds_ImageDataDataset.Tables["dt_AO"].Rows.Add(dr_AO);

                DataRow dr_IT = ds_ImageDataDataset.Tables["dt_IT"].NewRow();
                dr_IT[0] = id;
                dr_IT[1] = obj.IT_Code;
                dr_IT[2] = obj.IT_Field;
                dr_IT[3] = obj.IT_Value;
                ds_ImageDataDataset.Tables["dt_IT"].Rows.Add(dr_IT);

                DataRow dr_PU = ds_ImageDataDataset.Tables["dt_PU"].NewRow();
                dr_PU[0] = id;
                dr_PU[1] = obj.PU_Code;
                dr_PU[2] = obj.PU_Field;
                dr_PU[3] = obj.PU_Value;
                ds_ImageDataDataset.Tables["dt_PU"].Rows.Add(dr_PU);

                DataRow dr_SAT = ds_ImageDataDataset.Tables["dt_SAT"].NewRow();
                dr_SAT[0] = id;
                dr_SAT[1] = obj.SAT_Code;
                dr_SAT[2] = obj.SAT_Field;
                dr_SAT[3] = obj.SAT_Value;
                ds_ImageDataDataset.Tables["dt_SAT"].Rows.Add(dr_SAT);

            }

            //Bind the TreeView to ds_ImageDataDataset
            radTreeView1.Nodes.Clear();

            radTreeView1.DataSource = ds_ImageDataDataset.Tables["dt_AO"];

            radTreeView1.DisplayMember = "AO_Value";
            radTreeView1.ValueMember = "AO_Value";

            //radTreeView1.ParentMember = "pid";
            //radTreeView1.ChildMember = "id";

            //DisplayMember / ParentMember /  ChildMember / ValueMember
            //radTreeView1.RelationBindings.Add(new RelationBinding(ds_ImageDataDataset.Tables["dt_AO"], "AO_Value", "id_AO", "id_IT", "AO_Value"));
            radTreeView1.RelationBindings.Add(new RelationBinding(ds_ImageDataDataset.Tables["dt_IT"], "IT_Value", "pid", "id", "IT_Value"));
            radTreeView1.RelationBindings.Add(new RelationBinding(ds_ImageDataDataset.Tables["dt_PU"], "PU_Value", "id", "id", "PU_Value"));
            radTreeView1.RelationBindings.Add(new RelationBinding(ds_ImageDataDataset.Tables["dt_SAT"], "SAT_Value", "id", "id", "SAT_Value"));

            radTreeView1.ExpandAll();

            //foreach (DataRow dbRow in ds_ImageDataDataset.Tables["dt_AO"].Rows)
            //{
            //    if (! dbRow.IsNull("id_AO"))
            //    {
            //        RadTreeNode node = CreateNode(dbRow["AO_Value"].ToString(), true, dbRow["id_AO"].ToString());
            //        radTreeView1.Nodes.Add(node);
            //        RecursivelyPopulate(dbRow, node);
            //    }
            //}

            lbl_Working.Visible = false;

        }

        //private void RecursivelyPopulate(DataRow dbRow, RadTreeNode node)
        //{
        //    foreach (DataRow childRow in dbRow.GetChildRows("NodeRelation"))
        //    {
        //        RadTreeNode childNode = CreateNode(childRow["AO_Value"].ToString(), true, childRow["id_AO"].ToString());
        //        node.Nodes.Add(childNode);
        //        RecursivelyPopulate(childRow, childNode);
        //    }
        //}

        //private RadTreeNode CreateNode(string text, bool expanded, string id)
        //{
        //    RadTreeNode node = new RadTreeNode(text);
        //    node.Expanded = true;
        //    return node;
        //}

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
            {
                if (radListView1.SelectedItems.Count >= 1)
                {
                    var val = radListView1.SelectedItem.Value;
                    radImageEditor1.OpenImage(val.ToString());

                    if (!radImageEditor1.Enabled)
                    {
                        radImageEditor1.Enabled = true;
                    }

                }
            }
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}

#region Data Classes ================================================================================================================================================

public class ImageList
{
    // Methods
    public ImageList(int ID, //ID
                     string ImagePath, //Image File Path
                     string FilePath) //FilePath
    {
        this._ID = ID; //ID
        this._ImagePath = ImagePath; //Image Path
        this._FilePath = FilePath; //FilePath
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

    public string ImagePath
    {
        get
        {
            return this._ImagePath;
        }
        set
        {
            this._ImagePath = value;
        }
    }

    public string FilePath
    {
        get
        {
            return this._FilePath;
        }
        set
        {
            this._FilePath = value;
        }
    }

    private int _ID; //ID
    private string _ImagePath; //Image Path
    private string _FilePath; //FilePath
}

public class ImageData
{
    public ImageData(int pid,
                     string AO_Field, string AO_Value,
                     string IT_Code, string IT_Field, string IT_Value,
                     string PU_Code, string PU_Field, string PU_Value,
                     string SAT_Code, string SAT_Field, string SAT_Value)
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
        this._PU_Code = PU_Code;
        this._PU_Field = PU_Field;
        this._PU_Value = PU_Value;

        //StandardizedArticleType
        this._SAT_Code = SAT_Code;
        this._SAT_Field = SAT_Field;
        this._SAT_Value = SAT_Value;
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
    public string PU_Code { get { return this._PU_Code; } set { this._PU_Code = value; } }
    public string PU_Field { get { return this._PU_Field; } set { this._PU_Field = value; } }
    public string PU_Value { get { return this._PU_Value; } set { this._PU_Value = value; } }

    //StandardizedArticleType
    public string SAT_Code { get { return this._SAT_Code; } set { this._SAT_Code = value; } }
    public string SAT_Field { get { return this._SAT_Field; } set { this._SAT_Field = value; } }
    public string SAT_Value { get { return this._SAT_Value; } set { this._SAT_Value = value; } }

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
    private string _PU_Code;
    private string _PU_Field;
    private string _PU_Value;

    //StandardizedArticleType
    private string _SAT_Code;
    private string _SAT_Field;
    private string _SAT_Value;

}

#endregion