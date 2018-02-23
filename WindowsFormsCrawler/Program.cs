using Lucene.Net.Analysis;
using Lucene.Net.Analysis.MMSeg;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsCrawler
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        [STAThread]
        static void Main()
        {         
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            
            
            /*
            //AnalyzerTest("盤古分詞", "D:\\PanGuIndex", new PanGuAnalyzer());
            AnalyzerTest("標準分詞", "D:\\StdAnalyzerIndex",
                new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30));
            AnalyzerTest("MMSeg MaxWord", "D:\\MMSegIndex", new MMSegAnalyzer());
            AnalyzerTest("MMSeg Simple", "D:\\MMSegSimpIndex",
                new Lucene.Net.Analysis.MMSeg.SimpleAnalyzer());
            AnalyzerTest("MMSeg Complex", "D:\\MMSegCompIndex",
                new Lucene.Net.Analysis.MMSeg.ComplexAnalyzer());
            AnalyzerTest("CWSharp詞庫分詞", "D:\\CWStdIndex",
                new CwsAnalyzer(
                new Yamool.CWSharp.StandardTokenizer(
                    new FileStream("cwsharp.dawg", FileMode.Open))));
            AnalyzerTest("CWSharp一元分詞", "D:\\CWUniIndex",
                new CwsAnalyzer(new UnigramTokenizer()));
            AnalyzerTest("CWSharp二元分詞", "D:\\CWBiIndex",
                new CwsAnalyzer(new BigramTokenizer()));
            Console.Read();
            */
        }


        //http://blog.darkthread.net/post-2017-11-14-lucene-net-notes-3.aspx
        public static void AnalyzerTest(string title, string indexPath, Analyzer analyzer)
        {
            //指定索引資料儲存目錄
            var fsDir = FSDirectory.Open(indexPath);

            //建立IndexWriter
            using (var idxWriter = new IndexWriter(fsDir, //儲存目錄
                analyzer,
                true, //清除原有索引，重新建立
                IndexWriter.MaxFieldLength.UNLIMITED //不限定欄位內容長度
            ))

            {
                //示範為兩份文件建立索引
                var doc = new Document();
                //每份文件有兩個Field: Source、Word
                doc.Add(new Field("Word",
                    "生活就像一盒巧克力，你永遠也不會知道你將拿到什麼。",
                    Field.Store.YES, Field.Index.ANALYZED));
                idxWriter.AddDocument(doc);

                //建立索引
                idxWriter.Commit();
                idxWriter.Optimize();
            }


            var searcher = new IndexSearcher(fsDir, true);
            //指定欄位名傳入參數
            QueryParser qp = new QueryParser(Lucene.Net.Util.Version.LUCENE_30,"Word", analyzer);

            Action<string> testQuery = (kwd) =>
            {
                var q = qp.Parse(kwd);
                var hits = searcher.Search(q, 10);
                Console.WriteLine($"查詢「{kwd}」找到{hits.TotalHits}筆");
            };
            Console.WriteLine($"{title}測試");
            testQuery("魔物獵人");
            testQuery("要玩銃槍");
            testQuery("生活");
            testQuery("就像");
            testQuery("一盒");
            testQuery("巧克力");
            testQuery("永遠");
            testQuery("不會知道");
            testQuery("拿到");
            testQuery("什麼");
            Console.WriteLine("========================================");
        }
        



    }
}
