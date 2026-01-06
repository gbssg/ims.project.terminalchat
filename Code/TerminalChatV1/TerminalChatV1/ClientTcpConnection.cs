using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace TerminalChatClient
{
    public class ClientTcpConnection
    {
        public void ListenToServer(Server _server)
        {
            try
            {
                using (TcpClient client = new TcpClient(_server.ServerIp, _server.Port))
                {
                    Console.WriteLine($"Verbunden mit {_server.ServerIp}:{_server.Port}");
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

                            Console.WriteLine("\nClient: " + receivedMessage);
                            Console.Write("Du: ");
                        }
                    });
                    receiveThread.Start();


                    //while (true)
                    //{
                    //    Console.Write("Du: ");
                    //    string message = Console.ReadLine() ?? "";

                    //    byte[] buffer = Encoding.UTF8.GetBytes(message);
                    //    stream.Write(buffer, 0, buffer.Length);

                    //    if (message.ToLower() == "exit") break;
                    //}
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Fehler: " + e.Message);
                Console.ReadLine();
            }
        }

        public void TcpReciveThread()
        {

        }
    }
}
