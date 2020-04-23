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
 
