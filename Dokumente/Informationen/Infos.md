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

### Beispiel Code IRC Client:
```c#
    using System.Net.Sockets;
    using System.Text;

    internal class Program
    {
        static void Main()
        {
            Console.Write("Gib die Server-IP ein: ");
            string serverIp = Console.ReadLine() ?? "127.0.0.1"; // Standard ist localhost

            int port = 5000;

            try
            {
                using (TcpClient client = new TcpClient(serverIp, port))
                {
                    Console.WriteLine($"Verbunden mit {serverIp}:{port}");
                    NetworkStream stream = client.GetStream();

                    // Empfängt Nachrichten vom Server
                    Thread receiveThread = new Thread(() =>
                    {
                        byte[] buffer = new byte[1024];
                        while (true)
                        {
                            int bytesRead = stream.Read(buffer, 0, buffer.Length);
                            if (bytesRead == 0) break;
                            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                            Console.WriteLine("\nServer: " + receivedMessage);
                            Console.Write("Du: ");
                        }
                    });
                    receiveThread.Start();

                    while (true)
                    {
                        Console.Write("Du: ");
                        string message = Console.ReadLine() ?? "";

                        byte[] buffer = Encoding.UTF8.GetBytes(message);
                        stream.Write(buffer, 0, buffer.Length);

                        if (message.ToLower() == "exit") break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Fehler: " + e.Message);
                Console.ReadLine();
            }
        }

    }
```
### Beispiel Code IRC Server
```c#
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    internal class Program
    {
        static List<TcpClient> clients = new List<TcpClient>();
        static object lockObj = new object();

        static void Main()
        {
            // Lokale IP-Adresse des Servers herausfinden
            string localIP = GetLocalIPAddress();
            int port = 5000;

            TcpListener listener = new TcpListener(IPAddress.Parse(localIP), port);
            listener.Start();
            Console.WriteLine($"Server gestartet auf {localIP}:{port}. Warte auf Verbindungen...");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                lock (lockObj)
                {
                    clients.Add(client);
                }

                Console.WriteLine("Neuer Client verbunden!");
                Thread clientThread = new Thread(HandleClient);
                clientThread.Start(client);
            }
        }

        static void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj;
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];

            try
            {
                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Client: " + message);

                    BroadcastMessage(message, client);

                    if (message.ToLower() == "exit") break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Fehler: " + e.Message);
            }
            finally
            {
                lock (lockObj)
                {
                    clients.Remove(client);
                }
                client.Close();
                Console.WriteLine("Client getrennt.");
            }
        }

        static void BroadcastMessage(string message, TcpClient sender)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            lock (lockObj)
            {
                foreach (TcpClient client in clients)
                {
                    if (client != sender)
                    {
                        try
                        {
                            client.GetStream().Write(buffer, 0, buffer.Length);
                        }
                        catch
                        {
                            // Falls ein Client nicht mehr erreichbar ist, ignorieren
                        }
                    }
                }
            }
        }

        // Diese Methode holt die lokale IP-Adresse des Servers
        static string GetLocalIPAddress()
        {
            foreach (var item in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (item.AddressFamily == AddressFamily.InterNetwork)
                {
                    return item.ToString();
                }
            }
            throw new Exception("Keine Netzwerkverbindung gefunden!");
        }
    }
```
### Begriffs-/ Komponentenerklärung
#### Try - Catch / - finally
```c# 
    try
    {
        int x = 10, y = 0;
        int result = x / y; // This will throw a DivideByZeroException
    }
    catch (DivideByZeroException ex)
    {
        Console.WriteLine("Cannot divide by zero!");
    }
    finally (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }
```
Der Try-Block wird ausgeführt, wenn dabei ein definierter fehler auftritt, 
in diesem fall ein "DivideByZeroExeption" dann löst das den Catch-Block aus,
der Finally-Block fürt es immer aus auch wenn es im Try- oder Catch-Block ein return oä. hat.

#### Lock
```c#
    class Counter
    {
        private int count = 0;
        private object lockObj = new object(); // Lock object

        public void Increment()
        {
            lock (lockObj) // Only one thread at a time can execute this
            {
                count++;
                Console.WriteLine($"Count: {count}");
            }
        }
    }
```
**Für was wird *lock* genutzt?**
Verhindert Datenkorruption, Gemeinsame Variablen werden sicher verändert.  
Vermeidet Race Conditions, Kein unerwartetes Verhalten durch konkurrierende Threads.  
Einfach zu verwenden, Viel leichter als ```Monitor.Enter``` und ```Monitor.Exit```.  
lock sollte immer als privates readonly objekt initialisiert werden:  
```private readonly object lockObj = new object();```

#### Throw
```c# 
using System;

class Program
{
    static void Main()
    {
        try
        {
            ÜberprüfeAlter(15);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler: {ex.Message}");
        }
    }

    static void ÜberprüfeAlter(int alter)
    {
        if (alter < 18)
        {
            throw new ArgumentException("Das Alter muss mindestens 18 sein.");
        }
        Console.WriteLine("Zugang gewährt.");
    }
}
```
**Wie funktioniert **```throw```** eigentlich?**  
Die Methode ```ÜberprüfeAlter(int alter)``` überprüft, ob das Alter mindestens 18 ist.  
Falls nicht, wird mit ```throw new ArgumentException("Das Alter muss mindestens 18 sein.");``` eine Ausnahme geworfen.  
Der try-catch-Block in ```Main()``` fängt die Ausnahme ab und gibt die Fehlermeldung aus.  
```throw``` wird in den meisten fällen in einer ```if```-Abfrage einer Methode verwendet,   
dabei beendet throw die Methode und "wirft" eine selbst definierte Fehlermeldung zurück.




### Peer to Peer
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
