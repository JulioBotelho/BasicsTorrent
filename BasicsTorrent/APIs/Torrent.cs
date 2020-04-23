using Newtonsoft.Json;
using HtmlAgilityPack;
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
    /// Represents the Torrent Class
    /// </summary>
    public class Torrent
    {
        string nome;
        //string pageUrl;
        string torrentUrl = "";
        string magnetUrl = "";
        //string file;
        string origem = "";
        int seeders = 0;
        int leechers = 0;
        int minseeders = 0;
        int minleechers = 0;

        /// <summary>
        /// Represents the Torrent Name
        /// </summary>
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        }
        /// <summary>
        /// Represents the Origem do toreny
        /// </summary>
        public string Origem
        {
            get { return origem; }
            set { origem = value; }
        }

        ///// <summary>
        ///// Represents the Torrent Page
        ///// </summary>
        //public string PageUrl
        //{
        //    get { return pageUrl; }
        //    set { pageUrl = value; }
        //}

        /// <summary>
        /// Represents the Torrent Url
        /// </summary>
        public string TorrentUrl
        {
            get { return torrentUrl; }
            set { torrentUrl = value; }
        }

        /// <summary>
        /// Represents the Torrent Magnet
        /// </summary>
        public string MagnetUrl
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(magnetUrl))
                {
                    return magnetUrl;
                }
                else
                {
                    HttpClient hc = new HttpClient();
                    HtmlWeb web = new HtmlWeb();
                    HtmlDocument doc = new HtmlDocument();
                    //if (Origem.Equals("x1337"))
                    //{
                    // https://www.1377x.to/torrent/3265158/The-Neighborhood-S01E01-HDTV-x264-SVA-eztv/
                    Query mag = new Query();
                    mag.Origem = Origem;
                    string url = Query.UrlBase + TorrentUrl;
                    doc = web.Load(url);
                    IEnumerable<HtmlNode> hrefs = doc.DocumentNode.Descendants().Where(x => x.Name == "a");
                    foreach (HtmlNode href in hrefs)
                    {
                        foreach (HtmlAttribute t2 in href.Attributes)
                        {
                            if (t2.Name == "href")
                            {
                                if (t2.Value.StartsWith("magnet:"))
                                {
                                    magnetUrl = t2.Value;
                                    return magnetUrl;
                                }
                            }
                        }
                    }
                    //}
                    //else if (Origem.Equals("Kickass"))
                    //{
                    //    // https://www.1377x.to/torrent/3265158/The-Neighborhood-S01E01-HDTV-x264-SVA-eztv/
                    //    Query mag = new Query();
                    //    mag.Origem = Origem;
                    //    string url = Query.UrlBase + TorrentUrl;
                    //    doc = web.Load(url);
                    //    IEnumerable<HtmlNode> hrefs = doc.DocumentNode.Descendants().Where(x => x.Name == "a");
                    //    foreach (HtmlNode href in hrefs)
                    //    {
                    //        foreach (HtmlAttribute t2 in href.Attributes)
                    //        {
                    //            if (t2.Name == "href")
                    //            {
                    //                if (t2.Value.StartsWith("magnet:"))
                    //                {
                    //                    magnetUrl = t2.Value;
                    //                    return magnetUrl;
                    //                }
                    //            }
                    //        }
                    //    }
                //}

                return magnetUrl;
            }
        }

        set
            {
                magnetUrl = value;
            }
}

///// <summary>
///// Represents the Torrent File Name
///// </summary>
//public string File
//{
//    get
//    {
//        return file;
//    }

//    set
//    {
//        file = value;
//    }
//}

/// <summary>
/// Represents the Seeders
/// </summary>
public int Seeders
{
    get { return seeders; }
    set { seeders = value; }
}

/// <summary>
/// Represents the Seeders
/// </summary>
public int Leechers
{
    get { return leechers; }
    set { leechers = value; }
}

/// <summary>
/// Represents the Seeders Minimo
/// </summary>
public int MinSeeders
{
    get { return minseeders; }
    set { minseeders = value; }
}

/// <summary>
/// Represents the Seeders Minimo
/// </summary>
public int MinLeechers
{
    get { return minleechers; }
    set { minleechers = value; }
}


/// <summary>
/// Represents the Torrent Class as string
/// </summary>
public override string ToString()
{
    return JsonConvert.SerializeObject(this);
}
    }

}
