## OSI - schicht Nr. 4 - Transport

Für das Datentransportprotokoll habe ich diese vier möglichkeiten,

- TCP
- UDP
- SCTP
- SPX

Ich werde aber nur TCP benutzen also Transmission Controll Protocol da die anderen eher ungeeignet sind für mein anwendungsfall.

### SPX - Sequence Packet Exchange
Ist ein Protokoll wie TCP das in den 1980er - 1990er in LANs genutzt wurde, jedoch ist es jetzt hauptsächlich von TCP/IP abgelöst.

### SCTP - Stream Controll Transmission Protocol
SCTP ist eine mischung aus verschiedenen funktionen von TCP und UDP, jedoch wird es vorallem vor telekom services genutzt und ist "überqualifiziert" für mein Anwendung.

### UDP - User Datagram Protocol
Das UD-Protokoll wirt meistens in Onlinegames, Video-Streams, Sensorkomunikation (IoT) 
und DNS queries genutzt. UDP nutzt man wenn man Geschwindigkeit über Zuverlässigkeit 
stellt, und das eignet sich nicht gut für einen Chat indem möglichst keine Daten 
verloren gehen sollten.

### TCP - Transmission Controll Protocol
Übrig bleibt mir das TC-Protokoll, ich hätte dies sowieso gewählt jedoch ist es gut 
die anderen Möglichkeiten auch in betracht zu nehmen. TCP macht es einem einfach ganze 
nachrichten zu verschicken und empfangen, ausserdem sorgt das Protokoll dafür, dass 
bei datenverlust die nachrichten erneut gesendet werden ohne zusätzliche funktionen zu 
schreiben.

## Fazit
Ich werde für meine Applikation das TC-Protokoll nutzen da UDP durch den möglichen datenverlust ungeeignet ist, SCTP völlig "overkill" ist, SPX es am besten zu meinen 
bedingungen passt
