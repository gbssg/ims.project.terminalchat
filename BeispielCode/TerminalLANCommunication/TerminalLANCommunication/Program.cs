using System.Net.Sockets;
using System.Net;
using System.Text;

namespace TerminalLANCommunication
{
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

}
