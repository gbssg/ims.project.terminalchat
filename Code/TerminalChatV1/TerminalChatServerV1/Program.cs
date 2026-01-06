using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TerminalChatServer;

namespace TerminalChatServerV1
{
    internal class Program
    {
        static List<TcpClient> clients = new List<TcpClient>();
        static object lockObj = new object();
        static ServerSetup setup = new ServerSetup();
        static ServerDataCrud datacrud = new ServerDataCrud();

        static void Main()
        {
            datacrud.SetupAppDir();
            Console.WriteLine(datacrud.GetServersAsJson());
            Console.WriteLine(datacrud.GetServers());
            datacrud.AddServer(setup.ServerSetupPrompt());
            Console.WriteLine(datacrud.GetServers().Servers.Count);
            //var currentServer = datacrud.GetServers().Servers[0];
            /*

            TcpListener listener = new TcpListener(IPAddress.Parse(currentServer.Ip), currentServer.Port);
            listener.Start();
            Console.WriteLine($"Server gestartet auf {currentServer.Ip}:{currentServer.Port}. Warte auf Verbindungen...");

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
            */
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
    }
}
