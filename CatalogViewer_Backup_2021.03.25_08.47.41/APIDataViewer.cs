using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CatalogViewer
{
    public partial class APIDataViewer : Form
    {
        public APIDataViewer()
        {
            InitializeComponent();
        }

        private void btn_Sunmit_Click(object sender, EventArgs e)
        {
            ReadJSON();
        }

        private async void ReadJSON()
        {

            using (var httpClient = new HttpClient())
            {

                //var json = wc.DownloadString(@"https://rlg-ric-test-qua.apigee.net/dms-search-v1/query/standardized-product/result/3c293c7e1daa3c3cd98be476df21d877?article=CRW69011Z4&channel=IPADRETAIL&brand=CAR&language=en&apikey=F4VJUH9JZiXV5v2hfRM5PwmYUVdatDB0");
                //var json = wc.DownloadString(url);

                var json = await httpClient.GetStringAsync("https://rlg-ric-test-qua.apigee.net/dms-search-v1/query/standardized-product/result/3c293c7e1daa3c3cd98be476df21d877?article=CRW69011Z4&channel=IPADRETAIL&brand=CAR&language=en&apikey=F4VJUH9JZiXV5v2hfRM5PwmYUVdatDB0");

                //var json = wc.DownloadString(tbx_URL.Text);

                JObject myJSON = JObject.Parse(json);

                // get JSON result objects into a list
                IList<JToken> results = myJSON["hits"]["hits"][0]["_source"]["images"][0].Children().ToList();

                // serialize JSON results into .NET objects
                IList<SearchResult> searchResults = new List<SearchResult>();
                foreach (JToken result in results)
                {
                    //SearchResult searchResult = JsonConvert.DeserializeObject<SearchResult>(result.ToString());
                    //searchResults.Add(searchResult);

                    //tbx_Data.AppendText(String.Concat(JsonConvert.DeserializeObject<SearchResult>(result.ToString()), "\r\n"));
                    tbx_Data.AppendText(String.Concat(result.ToString(), "\r\n"));


                }

                //            string json = @"{
                //   'CPU': 'Intel',
                //   'PSU': '500W',
                //   'Drives': [
                //     'DVD read/writer'
                //     /*(broken)*/,
                //     '500 gigabyte hard drive',
                //     '200 gigabyte hard drive'
                //   ]
                //}";

                //JsonTextReader reader = new JsonTextReader(new StringReader(json));
                //while (reader.Read())
                //{
                //    if (reader.Value != null)
                //    {
                //        //tbx_Data.AppendText(String.Concat("Token:", reader.TokenType, ", Value:", reader.Value, "\r\n"));
                //        //tbx_Data.AppendText(String.Concat(reader.TokenType, "=", reader.Value, "\r\n"));
                //        tbx_Data.AppendText(String.Concat(reader.Value, "\r\n"));
                //    }
                //    else
                //    {
                //        //Console.WriteLine("Token: {0}", reader.TokenType);
                //    }
                //}
            }





        }

    }

    public class SearchResult
    {
        public string field { get; set; }
        public string value { get; set; }
        //public string Url { get; set; }
    }


}
