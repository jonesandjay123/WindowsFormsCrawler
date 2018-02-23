using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsCrawler
{
    public partial class Form1 : Form
    {
        int progress = 0;
        Thread Crawler = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Text = "測試文字";

            if (string.IsNullOrEmpty(textBox1.Text)) {
                //先自動做成魔物獵人
                textBox1.Text = "https://forum.gamer.com.tw/B.php?bsn=5786";
            }
            string URL = textBox1.Text.Trim();
            /*
            # region=== 進度條的顯示===
            int maxCount = 100;

            this.Invoke((MethodInvoker)delegate
            {
                progressBar1.Maximum = maxCount;

            });


            for (int i = 0; i<= maxCount; i++)
            {
                Console.WriteLine("=========i:" + i + "=============");

                progress++;
                progressBar1.Value = (int)((float) i / progressBar1.Maximum * 100);
                this.Text = "目前進度... " + (int)((float) i / progressBar1.Maximum * 100) + " %";

                //停一秒
                Thread.Sleep(100);
            }
            #endregion
            */


            #region ===HtmlAgilityPack爬蟲===
            //HtmlAgilityPack官方教學: http://html-agility-pack.net/?z=codeplex
            HtmlWeb webClient = new HtmlWeb();
            var doc = webClient.Load(URL);

            //var head = doc.DocumentNode.SelectSingleNode("//head/title");
            //Console.WriteLine("Node Name: " + head.Name + "\n" + head.OuterHtml);
       
            var table = doc.DocumentNode.SelectSingleNode("//body//table");
            //Console.WriteLine("table.ChildNodes.Count: " + table.FirstChild.NextSibling.Attributes["class"]);

            var tr = doc.DocumentNode.SelectNodes("//body//tr[contains(@class,'b-list__row b-list__row--sticky')]").First(); //找到符合條件的第一個tr
            Console.WriteLine("tr: " + tr.ChildNodes.Count);

            var tr0 = doc.DocumentNode.SelectNodes("//body//td[contains(@class,'b-list__main')]").First(); //找到符合條件的第一個tr
            Console.WriteLine("tr0: " + tr0.InnerHtml);
            var hrefstring = tr0.FirstChild.Attributes["href"].Value;
            Console.WriteLine("hrefstring: " + hrefstring);


            //Console.WriteLine("td: " + td.Attributes["class"].Value);

            /*
            var t1 = doc.DocumentNode.SelectSingleNode("//div[@id='BH-master']").InnerText;
            Console.WriteLine("body: " + t1);
            */

            /*
            var link = doc.DocumentNode.SelectSingleNode("//body//a[contains(@class,'data-gtm')][0]");
            var hrefstring = link.Attributes["href"].Value;
            */

            /*
            //根據HTML節點NODE的ID獲取節點
            HtmlNode navNode = doc.GetElementbyId("BH-master");
            //根據XPATH來索引節點
            //div[2]表示文章鏈接a位於post_list裡面第3個div節點中
            HtmlNode navNode2 = navNode.SelectSingleNode("//table[@class='b-list']");
            HtmlNode navNode3 = navNode.SelectSingleNode("//table[@id='loads']//tbody//tr");

            string articleTitle = navNode.InnerText;
            */

            /*
            //獲取文章鏈接地址
            string articleTitle = navNode3.Attributes["href"].Value.ToString();
            //獲取文章標題
            string articleName = navNode2.InnerText;
            */


            //Console.WriteLine("href: " + articleTitle);

            //doc.DocumentNode.SelectSingleNode("//body//a[contains(@class,'b-list')][2]");
            #endregion

            //c# 分詞索引: http://blog.darkthread.net/post-2017-11-14-lucene-net-notes-3.aspx

            MessageBox.Show("程式結束!");
        }
    }
}
