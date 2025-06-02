## Wie ist ein Datenpacket aufgebaut

### Das Format  

Als Format für das Datenpacket nehme ich JSON da es leserlich ist und ich schon weis 
wie man es serialisiert/deserialisiert. Anstelle von JSON hätte ich andere Formate wie 
CBOR, BSON, Protobuf diese sind jedoch binäre Formate und wesentlich komplizierter als 
Textbasierende und da ich schon einbisschen im Zeitdruck bin will ich nicht noch mehr 
Zeit in kleinere Ecken verwerfen. Ich hätte auch ein eigenes Format erstellen können 
also mit ASCII-Zeichen da würde aber auch wider viel Zeit darauf gehen die ich nicht 
habe, ich würde mich eher damit beschäftigen wenn ich keine Deadline hätte. Eine 
Andere Idee wäre noch eine RESTful API jedoch finde ich das nicht sehr sinnvoll da es 
weitere Komplikationen geben würde mit den ganzen session keys und weiteres und da 
das Projekt eine eher simplere Struktur haben sollte werde ich mit JSON arbeiten.

