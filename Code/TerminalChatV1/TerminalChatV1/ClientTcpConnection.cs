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
        public void ListenToServer(string hostname, int port)
        {
            try
            {
                using (TcpClient client = new TcpClient(hostname, port))
                {
                    Console.WriteLine($"Verbunden mit {hostname}:{port}");
                    NetworkStream stream = client.GetStream();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Fehler: " + e.Message);
                Console.ReadLine();
            }
        }

        public static void TcpReciveThread(NetworkStream _stream)
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
                }
            });
            receiveThread.Start();
            
        }        
    }
}
