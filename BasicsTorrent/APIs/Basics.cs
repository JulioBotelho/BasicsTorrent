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
    public class Basics
    {
        /// <summary>
        /// Retrieve new amount of Torrent from Kickass
        /// </summary>
        public static List<Torrent> Search(Query query)
        {
            // KATcr torrent/usearch ou katsearch
            // Ainda não consegui buscar exata no KATcr
            // https://katcr.co/katsearch/page/1/The.Neighborhood.S01E01%20
            query.Origem = "KATcr";
            query.Words = "The.Neighborhood.S01E01";
            List<Torrent> Result = KATcr.Search(query);

            // kickass torrent/usearch ou katsearch
            // https://kickass.sx/torrent/usearch/The.Neighborhood.S01E01/
            query.Origem = "Kickass";
            Result.AddRange(Kickass.Search(query));
            //List<Torrent> Result = Kickass.Search(query);

            // 1337X
            // https://www.1377x.to/search/The.Neighborhood.S01E01/1/
            query.Origem = "x1337";
            Result.AddRange(x1337.Search(query));
            //List<Torrent> Result = x1337.Search(query);

            // Lime
            //https://www.limetorrents.info/search/all/The-Neighborhood-S01E01/
            query.Origem = "Lime";
            Result.AddRange(Lime.Search(query));
            //List<Torrent> Result = Lime.Search(query);

            return Result.OrderByDescending(o => o.Seeders).ToList();
        }
    }

}
