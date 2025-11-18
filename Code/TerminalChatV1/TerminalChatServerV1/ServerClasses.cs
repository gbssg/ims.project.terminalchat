using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalChatServer
{
    public class Server
    {
        public string Ip { get; set; }
        public string Port { get; set; }
        public string Name { get; set; }
        public string description { get; set; }
        public Guid UUID { get; set; }
    }

    public class Channel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid UUID { get; set; }

    }
}
