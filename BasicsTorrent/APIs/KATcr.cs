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
    /// Retrieve new amount of Torrent from KATcr
    /// </summary>
    public static class KATcr
    {
        /// <summary>
        /// Retrieve Torrent from KATcr
        /// </summary>
        public static List<Torrent> Search(Query para)
        {
            //bool err = false;
            //string errMessage = "";
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
                IEnumerable<HtmlNode> rows = tb.Descendants().Where(x => x.Name == "tr");
                foreach (HtmlNode tr in rows)
                {
                    //Debug.WriteLine("TR=" + tr.OuterHtml.Substring(0, 100).Replace("\n", "").Replace("  ", " "));
                    IEnumerable<HtmlNode> tds = tr.Descendants().Where(x => x.Name == "td");
                    foreach (HtmlNode td1 in tds)
                    {
                        //Debug.WriteLine("TD=" + td1.OuterHtml.Replace("\n", "").Replace("  ", " "));
                        if (!td1.Attributes.Any())
                        {
                            IEnumerable<HtmlNode> aref = td1.Descendants().Where(x => x.Name == "a");
                            foreach (HtmlNode href in aref)
                            {
                                //Debug.WriteLine("HREF=" + href.OuterHtml.Replace("\n", "").Replace("  ", " "));
                                bool achou = false;
                                if (string.IsNullOrWhiteSpace(torrent.TorrentUrl))
                                {
                                    foreach (HtmlAttribute t1 in href.Attributes)
                                    {
                                        if (t1.Name == "class" && t1.Value == "torrents_table__torrent_title")
                                        {
                                            torrent.Nome = href.InnerText.Replace("\n", "").Trim(); ;
                                            foreach (HtmlAttribute t2 in href.Attributes)
                                            {
                                                if (t2.Name == "href")
                                                {
                                                    torrent.TorrentUrl = t2.Value;
                                                    achou = true;
                                                    break;
                                                }
                                            }
                                            if (achou)
                                                break;
                                        }
                                        if (achou)
                                            break;
                                    }
                                }
                                //if (string.IsNullOrWhiteSpace(torrent.PageUrl))
                                //{
                                //    foreach (HtmlAttribute t1 in href.Attributes)
                                //    {
                                //        if (t1.Name == "title" && t1.Value == "Download with privacy")
                                //        {
                                //            foreach (HtmlAttribute t2 in href.Attributes)
                                //            {
                                //                if (t2.Name == "href")
                                //                {
                                //                    torrent.PageUrl = t2.Value;
                                //                    achou = true;
                                //                    break;
                                //                }
                                //            }
                                //            if (achou)
                                //                break;
                                //        }
                                //        if (achou)
                                //            break;
                                //    }
                                //}
                                if (string.IsNullOrWhiteSpace(torrent.MagnetUrl))
                                {
                                    foreach (HtmlAttribute t1 in href.Attributes)
                                    {
                                        if (t1.Name == "title" && t1.Value == "Torrent magnet link")
                                        {
                                            foreach (HtmlAttribute t2 in href.Attributes)
                                            {
                                                if (t2.Name == "href")
                                                {
                                                    torrent.MagnetUrl = t2.Value;
                                                    achou = true;
                                                    break;
                                                }
                                            }
                                            if (achou)
                                                break;
                                        }
                                        if (achou)
                                            break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            foreach (HtmlAttribute t1 in td1.Attributes)
                            {
                                if (t1.Name == "class" && t1.Value == "text--nowrap text--center text--success")
                                {
                                    torrent.Seeders = int.Parse(td1.InnerText.Replace("\n", "").Replace(",", "").Replace(".", "").Trim());
                                }
                                else if (t1.Name == "class" && t1.Value == "text--nowrap text--center text--error")
                                {
                                    torrent.Leechers = int.Parse(td1.InnerText.Replace("\n", "").Replace(",", "").Replace(".", "").Trim());
                                }
                            }
                        }
                    }
                    if (torrent.Seeders > torrent.MinSeeders
                        && torrent.Leechers > torrent.MinLeechers)
                    {
                        string a = torrent.Nome.ToLower().Replace("+", ".").Replace(" ", ".");
                        string b = para.Words.ToLower().Replace("+", ".").Replace(" ", ".");
                        if(a.StartsWith(b))
                        {
                            result.Add(torrent);
                            torrent = new Torrent();
                            torrent.Origem = para.Origem;
                        }
                    }
                }
            }
            return result;
        }
    }
}
