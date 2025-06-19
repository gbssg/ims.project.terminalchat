using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalChatClient
{
    internal class Packages
    {
        public abstract class Package
        {
            public string packageType { get; set; }
            public DateTime timeStamp { get; set; } = DateTime.Now;
        }

        public class MessagePackage : Package
        {
            private int message_id { get; set; }
            public int sender_id { get; set; }
            public string receiver_id { get; set; }
            public string channel_id { get; set; }
            public int chat_id { get; set; }
            public string message { get; set; }
        }
    }
}
