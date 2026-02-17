using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TerminalChatServer
{
    public class ServerTcpConnection
    {
        public void RunServer(Server _server)
        {
            
            IPAddress ipAddr = IPAddress.Parse( _server.Ip );
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, _server.Port);

            Socket listener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(localEndPoint);
            listener.Listen(1000);

        }
    }
}
