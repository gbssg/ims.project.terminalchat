# Informations Dokument

## Der Aufbau eines IRC's *Inspiriert von HexChat*
### Das Interface
Hex Chat is simple aufgebaut, es besteht aus einerside bar, einem chat, einer 
userbar, einem textfeld und tabs für einstellungen, um server/kanäle zu wechseln, 
um das erscheinungsbild zu ändern.  


### Server und Kanäle
Ein Server hat normalerweise mehrere Kanäle mit verschiedenen Topics, oftmals 
haust in einem Kanal ein Bot der Täglich ein neues Gesprächsthema ansagt.

### Login
Im Login fenster konte man einen Username festlegen, dabei musste man 4 
verschiedene angeben. Da jeder User uinque sein muss werden 4 Namen angegeben um 
3 auswege zu haben. Ein Passwort wird nich benötigt das führt jedoch dazu das man 
nicht sicher sein kann mit wen man spricht, also komplett anonym. 

## Das interface meines IRC's
Ich muss das terminal auf eine fixe fenstergrösse setzen dammit mein ASCII-art nicht verstertched wird. 1464 x 716

## Client zu Client kommunikation
Ich habe ein bisschen recherchiert und bin zur Erkentniss gekommen,   
dass ich die Komunikation von Client zu Client via Peer2Peer Netzwerk einleiten 
werde.

#
## Peer to Peer
https://de.wikipedia.org/wiki/Peer-to-Peer

## ToDo-List
### Planung

- [ ] IRC-Client planen
  - [ ] CtC-Kommunikation planen
    - [ ] Diagramme erstellen
      - [x] CtC direct communication Diagramm
      - [ ] Programm runtime Diagramm
    - [ ] Programmstrucktur entwerfen
- [ ] IRC-Server planen
  - [ ] CtS/StC-Kommunikation planen
    - [ ] Diagramme erstellen
    - [ ] Serverstrucktur planen
- [ ] Nachrichtenübertragungsprotokoll erstellen
  - [ ] Nachrichten Layout erstellen
  - [ ] Nachrichtenadressierung definieren


## Links:

### Code Beispiel:
- Library doc:
  - https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.tcplistener?view=net-9.0
  - https://learn.microsoft.com/en-us/dotnet/api/system.net.sockets.tcpclient?view=net-9.0

### IRC-Client

- Wikipedia:   
    - https://de.wikipedia.org/wiki/IRC-Client  
    - https://de.wikipedia.org/wiki/Internet_Relay_Chat  
  
- Libera.Chat: https://libera.chat/guides/basics 
- KiwiIRC Documentation: https://github.com/kiwiirc/kiwiirc/wiki/
- HexChat Documentation: https://hexchat.readthedocs.io/en/latest/getting_started.html
- irssi Client im Terminal: https://github.com/irssi/irssi

### Nachrichtenverschlüsselung

- Wikipedia: 
  - https://de.wikipedia.org/wiki/Transport_Layer_Security

### IRC-Server

- Wikipedia:
  - IRC-Server: https://de.wikipedia.org/wiki/IRC-Server
  - IRC-Network: https://de.wikipedia.org/wiki/IRC-Netzwerk

- Ubuntu Documentation: https://help.ubuntu.com/community/InternetRelayChat

### Server-Hosting

- ionos: https://www.ionos.com/digitalguide/server/know-how/irc-server/
