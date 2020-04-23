using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BasicsTorrent;

namespace BasicsTorrentTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Query query = new Query();
            query.Words = "The.Neighborhood.S01E01";
            List<Torrent> Result = Basics.Search(query);
            //query.Origem = "Lime";
            //List<Torrent> Result = Lime.Search(query);

            foreach (Torrent x in Result)
                Console.WriteLine("Origem=" + x.Origem
                    + " Seeders=" + x.Seeders.ToString()
                    + " Nome=" + x.Nome.Substring(0, Math.Min(40, x.Nome.Length))
                    + " Magnet=" + x.MagnetUrl.Substring(0, Math.Min(40, x.MagnetUrl.Length))
                    + " Torrent=" + x.TorrentUrl.Substring(0, Math.Min(40, x.TorrentUrl.Length)));
            Console.ReadLine();
        }
    }
}
