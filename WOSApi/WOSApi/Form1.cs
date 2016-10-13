using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WOSApi.Helpers;
using WOSApi.Model;

namespace WOSApi
{
    public partial class Form1 : Form
    {
        private BindingSource bs = new BindingSource();

        public Form1()
        {
            InitializeComponent();
        }

        public List<ArticleModel> ExecuteQuery(string author)
        {
            List<ArticleModel> articles = new List<ArticleModel>();
            XMLHelper xmlhelper = new XMLHelper();
            ApiHelper helper = new ApiHelper();
            try
            {
                string ses = helper.Login();
                try
                {

                    WOSSearch.fullRecordSearchResults results = new WOSSearch.fullRecordSearchResults();
                    int start = 1;
                    int count = 100;
                    int found = 999;
                    while (start < found)
                    {
                        results = helper.Search(start, count, author);
                        var recrod = xmlhelper.DesrializeXML<records>(results.records);
                          

                        foreach (var rec in recrod.REC)
                        {
                            articles.Add(rec);
                        }
                        found = results.recordsFound; 
                        start += count;
                    } 

                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                } 
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return articles;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var results = ExecuteQuery(txtQuery.Text);
            bs.DataSource = results;
            dgResults.DataSource = bs;
        }
    }
}
