using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerminalChatServer;

namespace TerminalChatServer
{
    internal class Server
    {
        public string ip { get; set; }
        public string port { get; set; }
        public string name { get; set; }
        public int NumOfChannels { get; } 
    }
}
