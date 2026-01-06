using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

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
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Fehler: " + e.Message);
                Console.ReadLine();
            }
        }

        public void TcpReciveThread(NetworkStream _stream)
        {
            // Empfängt Nachrichten vom Server
            Thread receiveThread = new Thread(() =>
            {
                byte[] buffer = new byte[1024];
                while (true)
                {
                    int bytesRead = _stream.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0) break;
                    
                    string receivedString = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    //process package()
                }
            });
            receiveThread.Start();
            
        }
        public iRecivable ProcessPackage(string _receivedString)
        {
            try
            {
                Message messsage = JsonSerializer.Deserialize<Message>( _receivedString );
            }
            catch
            {
                // to debug
                Console.WriteLine("received message did not fit message class!");
                try
                {
                    Server server = JsonSerializer.Deserialize<Server>( _receivedString );
                }
                catch
                {
                    // to debug
                    Console.WriteLine("received message did not fit message or server class!");
                }
            }

            return null; // null is temporary
        }
        
    }
}
