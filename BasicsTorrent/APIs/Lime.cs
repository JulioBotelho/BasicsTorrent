using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BasicsTorrent
{
    /// <summary>
    /// Retrieve new amount of Torrent from Kickass
    /// </summary>
    public static class Lime
    {
        /// <summary>
        /// Retrieve new amount of Torrent from Lime
        /// </summary>
        public static List<Torrent> Search(Query para)
        {
            List<Torrent> result = new List<Torrent>();
            Torrent torrent = new Torrent();
            torrent.Origem = para.Origem;
            Debug.WriteLine(para.ToString());
            HttpClient hc = new HttpClient();
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = new HtmlDocument();
            doc = web.Load(para.ToString());
            IEnumerable<HtmlNode> tabela = doc.DocumentNode.Descendants().Where(x => x.Name == "table");
            foreach (HtmlNode tb in tabela)
            {
                foreach (HtmlAttribute tt in tb.Attributes)
                {
                    if (tt.Name == "class" && tt.Value == "table2")
                    {
                        IEnumerable<HtmlNode> rows = tb.Descendants().Where(x => x.Name == "tr");
                        foreach (HtmlNode tr in rows)
                        {
                            bool achou = false;
                            //Debug.WriteLine("TR=" + tr.OuterHtml.Substring(0, 100).Replace("\n", "").Replace("  ", " "));
                            IEnumerable<HtmlNode> tds = tr.Descendants().Where(x => x.Name == "td");
                            foreach (HtmlNode td1 in tds)
                            {
                                //Debug.WriteLine("TD=" + td1.OuterHtml.Replace("\n", "").Replace("  ", " "));
                                foreach (HtmlAttribute t1 in td1.Attributes)
                                {
                                    if (t1.Name == "class" && t1.Value == "tdleft")
                                    {
                                        IEnumerable<HtmlNode> aref = td1.Descendants().Where(x => x.Name == "a");
                                        foreach (HtmlNode href in aref)
                                        {
                                            //Debug.WriteLine("HREF=" + href.OuterHtml.Replace("\n", "").Replace("  ", " "));
                                            foreach (HtmlAttribute t2 in href.Attributes)
                                            {
                                                if(t2.Name == "href")
                                                {
                                                    if (!string.IsNullOrWhiteSpace(href.InnerText.Replace("\n", "").Trim()))
                                                    {
                                                        torrent.Nome = href.InnerText.Replace("\n", "").Trim();
                                                        torrent.TorrentUrl = t2.Value;
                                                        achou = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (t1.Name == "class" && t1.Value == "tdseed")
                                    {
                                        torrent.Seeders = int.Parse(td1.InnerText.Replace("\n", "").Replace(",", "").Replace(".", "").Trim());
                                    }
                                    else if (t1.Name == "class" && t1.Value == "tdleech")
                                    {
                                        torrent.Leechers = int.Parse(td1.InnerText.Replace("\n", "").Replace(",", "").Replace(".", "").Trim());
                                    }
                                }
                            }
                            if (achou &&
                                   torrent.Seeders > torrent.MinSeeders
                                && torrent.Leechers > torrent.MinLeechers)
                            {
                                string a = torrent.Nome.ToLower().Replace("+", ".").Replace(" ", ".");
                                string b = para.Words.ToLower().Replace("+", ".").Replace(" ", ".");
                                if (a.StartsWith(b))
                                {
                                    result.Add(torrent);
                                    torrent = new Torrent();
                                    torrent.Origem = para.Origem;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
