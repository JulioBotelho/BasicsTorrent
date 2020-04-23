# BasicsTorrent
 API to search manyTorrents engine to get TV Series Magnet link
 
 This is a very simple API warapper to torrents engine.
 
 At this moment searchs:
      - Kickass
      - KATcr (yes, this has the same visual, but are diferent engine)
      - Limetorrents
      - x1337
      
The Pirates Bay was excluded in april/2020 due last engine update changes the way torrents are publised and there no API enpoint exposed

USAGE

You can choose to use engine especific or all in a roll.

Basic usage

	Query query = new Query();
	query.Words = "The Neighborhood S01E01";
	List<Torrent> Result = Basics.Search(query);
			
 This will search all engines suporteds and return a list of torrents, descendent ordered by seeds
 
 Individual usage
 
	Query query = new Query();
	query.Words = "The Neighborhood S01E01";
	query.Origem = "Lime";
	List<Torrent> Result = Lime.Search(query);
			
This will search especific engine suporteds and return a list of torrents, descendent ordered by seeds

Torrent Data Structura

	Nome - torrent file name
        TorrentUrl - URL to open torrent
        MagnetUrl - URL to download Magnet
        Origem - ID to supported engine - Valids Lime, KATcr, x1337 and Kickass
        Seeders - Number of torrent seeders
        Leechers - Number of torrent leechers
        MinSeeders - Filter to get only torrent with Seeders greater than 
        MinLeechers - Filter to get only torrent with Leechers greater than 

 All selected torrents has Nome starting with query Words
 
 There are some constantsexposed to make possible dinamic fixing in case of engine changes.
 
        Constants.KatBase = "https://katcr.co";
        Constants.KatMask = "{0}/katsearch/page/1/{1}";
        Constants.KikBase = "https://kickass.sx";
        Constants.KikMask = "{0}/torrent/usearch/{1}";
        Constants.x13Base = "https://www.1377x.to";
        Constants.x13Mask = "{0}/search/{1}/1/";
        Constants.LimBase = "https://www.limetorrents.info";
        Constants.LimMask = "{0}/search/all/{1}/";
	
These is usefull if you need use a proxy or if the engine changes url format

