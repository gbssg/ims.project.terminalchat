using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalChatClient
{
    internal class Tab
    {
        public int id;
        public string? name { get; set; }
        public int posH { get; set; }
        public int posV { get; set; }
        public int size { get; set; }
        public bool selected { get; set; }
    }

}
