using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TerminalChatServerV1
{
    public static class Setup
    {
        static List<TcpClient> clients = new List<TcpClient>();
        static object lockObj = new object();
        public static void StartListener()
        {
            string localIP = GetLocalIPAddress();
            int port = 5000;

            TcpListener listener = new TcpListener(IPAddress.Parse(localIP), port);
            listener.Start();
            Console.WriteLine($"Starting server on {localIP}:{port}. Waiting for conection...");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                lock (lockObj)
                {
                    clients.Add(client);
                }

                Console.WriteLine("New Client connected!"); //ToDo: use client name from datapackage
                Thread clientThread = new Thread(HandleClient);
                clientThread.Start(client);
            }
        }

        public static void HandleClient(object Obj)
        {
            TcpClient client = (TcpClient)Obj;
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];

            try
            {
                while (true)
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        break;
                    }
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine("Client:" + message); //ToDo: use client name from datapackage

                    BroadcastMessage(message, client);

                    if(message.ToLower() == "exit")
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error:" + e.Message);
            }
            finally
            {
                lock (lockObj)
                {
                    clients.Remove(client);
                }
                client.Close();
                Console.WriteLine("client disconnected"); //ToDo: use client name from datapackage
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

                        }
                    }
                }
            }

        }

        static string GetLocalIPAddress()
        {
            foreach (var item in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (item.AddressFamily == AddressFamily.InterNetwork)
                {
                    return item.ToString();
                }
            }
            throw new Exception("No Networkconnection found!");
        }
    }
}
