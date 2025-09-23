using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalChatClient
{
    internal class Size
    {
        public int height { get; set; }
        public int width { get; set; }
        public Size(int height, int width)
        {
            this.height = height;
            this.width = width;
        }

    }
}
