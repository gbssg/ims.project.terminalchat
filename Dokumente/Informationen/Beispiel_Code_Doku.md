### Beispiel Code IRC Client:
```c#
    using System.Net.Sockets; /*?*/
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
                    Thread receiveThread = new Thread(() => /*?*/
                    {
                        byte[] buffer = new byte[1024];
                        while (true)
                        {
                            int bytesRead = stream.Read(buffer, 0, buffer.Length);
                            if (bytesRead == 0) break;
                            string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead); /*?*/
                            Console.WriteLine("\nServer: " + receivedMessage);
                            Console.Write("Du: ");
                        }
                    });
                    receiveThread.Start(); /*?*/

                    while (true)
                    {
                        Console.Write("Du: ");
                        string message = Console.ReadLine() ?? "";

                        byte[] buffer = Encoding.UTF8.GetBytes(message); /*?*/
                        stream.Write(buffer, 0, buffer.Length); /*?*/

                        if (message.ToLower() == "exit") break;
                    }
                }
            }
            catch (Exception e) /*?*/
            {
                Console.WriteLine("Fehler: " + e.Message);
                Console.ReadLine();
            }
        }

    }
```
### Beispiel Code IRC Server
```c#
    using System.Net; /*?*/
    using System.Net.Sockets; /*?*/
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
                TcpClient client = listener.AcceptTcpClient(); /*?*/
                lock (lockObj) /*?*/
                {
                    clients.Add(client);
                }

                Console.WriteLine("Neuer Client verbunden!");
                Thread clientThread = new Thread(HandleClient);
                clientThread.Start(client); /*?*/
            }
        }

        static void HandleClient(object obj)
        {
            TcpClient client = (TcpClient)obj; /*?*/
            NetworkStream stream = client.GetStream(); /*?*/
            byte[] buffer = new byte[1024];

            try
            {
                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length); /*?*/
                    if (bytesRead == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead); /*?*/
                    Console.WriteLine("Client: " + message);

                    BroadcastMessage(message, client); /*?*/

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
                            client.GetStream().Write(buffer, 0, buffer.Length); /*?*/
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
            foreach (var item in Dns.GetHostEntry(Dns.GetHostName()).AddressList) /*?*/
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
### Knowhow- Komponenten
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

### Codezeilen erklärt

```using System.Net.Sockets;```

Importiert den Namespace für Low-Level-Netzwerkprogrammierung, insbesondere die Klassen ```TcpClient```, ```TcpListener```, ```NetworkStream```, ```Socket```, etc.Diese bieten direkten Zugriff auf Transport Layer-Funktionalität (Layer 4) im OSI-Modell, also z. B. TCP-Verbindungen.  

```Thread receiveThread = new Thread(() => { ... });```  
Erstellt einen neuen Thread über einen Lambda-Ausdruck.
Technisch ist dies eine Instanz der Klasse ```System.Threading.Thread```, die Code asynchron ausführt. Dieser Thread lauscht auf dem Stream, um eingehende TCP-Daten (also Netzwerkpakete) parallel zur UI/Benutzereingabe zu verarbeiten.  

```string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);```  
Diese Zeile dekodiert ein empfangenes Byte-Array in einen String mit dem Standard UTF-8-Zeichensatz.
Dies ist nötig, weil Netzwerkprotokolle Daten als Binärdaten (Bytes) transportieren – die Umwandlung stellt sicher, dass Text korrekt dargestellt wird.
  
```receiveThread.Start();```  
Initialisiert den Scheduler des .NET-Threads und übergibt die Kontrolle an den Lambda-Code.
Die Thread-Ausführung beginnt, typischerweise durch eine native OS-Thread-Routine (unter Windows: ```CreateThread```, intern durch CLR verwaltet).

```byte[] buffer = Encoding.UTF8.GetBytes(message);```  
Konvertiert einen ```string``` in ein Byte-Array im UTF-8-Format.
Dies ist notwendig, da die ```NetworkStream.Write()```-Methode rohe Byte-Daten erwartet – keine Strings oder komplexe Objekte.  

```stream.Write(buffer, 0, buffer.Length);```  
Sendet Daten über den ```NetworkStream```, der mit einem TCP-Socket verbunden ist.
Dies ist ein synchroner, blockierender Schreibvorgang, der den Datenpuffer an das Betriebssystem-Socket-API übergibt.

```catch (Exception e)```  
Fängt Laufzeitfehler ab, z. B. Netzwerkfehler, ungültige IPs, Verbindungsabbrüche.
```Exception``` ist die Oberklasse aller Fehler im .NET-Framework, inkl. ```SocketException```, ```IOException```, ```ObjectDisposedException``` usw.

```using System.Net;```  
Importiert den Namespace, der Zugriff auf IP-Adressen, DNS-Resolver, Hostnamen, etc. bietet.
Wird z. B. für ```Dns.GetHostEntry()``` oder ```IPAddress.Parse()``` genutzt, um IP-Adressen und Hostinformationen zu verwalten.  

```TcpClient client = listener.AcceptTcpClient();```  
```AcceptTcpClient()``` ist eine Methode von ```TcpListener```, die blockiert, bis ein Verbindungsversuch eingeht.
Sie kapselt einen Low-Level-Socket in ein ```TcpClient```-Objekt, was die Handhabung von Streams vereinfacht.

```lock (lockObj)```  
Erzeugt eine synchronisierte Code-Sektion, um Thread-Race-Conditions zu verhindern.
Dies ist nötig, da mehrere Threads gleichzeitig auf die ```clients```-Liste zugreifen können.

```clientThread.Start(client);```  
Startet einen Thread und übergibt als Parameter das ```TcpClient```-Objekt für den verbundenen Client.
Im Thread wird dieses Objekt dann per Cast als ```TcpClient``` verwendet.

```TcpClient client = (TcpClient)obj;```  
Konvertiert ein ```object``` zurück zu ```TcpClient```, weil ```Thread.Start()``` nur ```object``` als Parameter akzeptiert.
Dies ist typischer Boxing/Unboxing-Mechanismus im .NET Type-System.

```NetworkStream stream = client.GetStream();```  
Gibt den zugehörigen TCP-Datenstrom zurück, über den mit dem Client kommuniziert wird.
Dieser Stream basiert auf dem darunterliegenden Socket, ist aber einfacher zu verwenden.

```int bytesRead = stream.Read(buffer, 0, buffer.Length);```  
Liest Daten blockierend vom Stream in ein Byte-Array.
bytesRead enthält die tatsächliche Anzahl der empfangenen Bytes. Die Methode wartet, bis Daten eintreffen, was Netzwerklatenz verursachen kann.  

```string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);```  
Dekodiert das empfangene Byte-Array zu einem lesbaren UTF-8-String.
Dies ist nötig, da TCP/IP-Protokolle selbst keine Informationen über Textkodierung enthalten.
  
```BroadcastMessage(message, client);```  
Ruft eine Methode auf, die die empfangene Nachricht an alle anderen verbundenen Clients weiterleitet (Multicast-ähnlich, aber softwarebasiert).
Dabei wird der Sender ausgenommen, um Echo-Effekte zu vermeiden.

```client.GetStream().Write(buffer, 0, buffer.Length);```  
Sendet die Nachricht an einen spezifischen Client, über seinen ```NetworkStream```.
Wird in ```BroadcastMessage()``` verwendet, um Nachrichten an andere Clients weiterzugeben.

```foreach (var item in Dns.GetHostEntry(Dns.GetHostName()).AddressList)```  
Ruft die lokale Netzwerkinformation vom DNS-System ab.
Dies ist der interne Name-Resolver von Windows oder Linux – mit ```GetHostEntry()``` erhältst du eine Liste aller zugewiesenen IP-Adressen des Geräts.
Du suchst dann explizit nach IPv4-Adressen (```AddressFamily.InterNetwork```).
