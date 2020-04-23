using System;
using System.Text;

namespace BasicsTorrent
{
    /// <summary>
    /// Represents the query text to submit
    /// </summary>
    public class Query
    {
        internal int page = 1;
        internal string words;
        internal string origem;
        /// <summary>
        /// All these URL
        /// </summary>
        public static string UrlBase;
        /// <summary>
        /// All these URL
        /// </summary>
        public static string UrlMask;

        /// <summary>
        /// All these words
        /// Represents the query text to submit
        /// </summary>
        public string Words
        {
            get { return words; }
            set { words = value; }
        }
        /// <summary>
        /// All these words
        /// Represents the query text to submit
        /// </summary>
        public string Origem
        {
            get { return origem; }
            set
            {
                origem = value;
                if (origem.Equals("KATcr"))
                {
                    UrlBase = "https://katcr.co";
                    UrlMask = "{0}/katsearch/page/1/{1}";
                }
                else if (origem.Equals("Kickass"))
                {
                    UrlBase = "https://kickass.sx";
                    UrlMask = "{0}/torrent/usearch/{1}";
                }
                else if (origem.Equals("x1337"))
                {
                    UrlBase = "https://www.1377x.to";
                    UrlMask = "{0}/search/{1}/1/";
                }
                else if (origem.Equals("Lime"))
                {
                    UrlBase = "https://www.limetorrents.info";
                    UrlMask = "{0}/search/all/{1}/";
                }
            }
        }

        /// <summary>
        /// Represents the query text to submit as string
        /// </summary>
        public override string ToString()
        {
            return String.Format(UrlMask, UrlBase, Words.Replace(" ", "+").Replace(".", "+"));
        }
    }
}
