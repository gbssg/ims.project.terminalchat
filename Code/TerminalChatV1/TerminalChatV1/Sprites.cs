using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalChatClient
{
    internal class Sprites
    {
        public char sv { get; set; } = '║';
        public char sh { get; set; } = '═';
        public char ne { get; set; } = '╗';
        public char nw { get; set; } = '╔';
        public char se { get; set; } = '╝';
        public char sw { get; set; } = '╚';
        public ConsoleColor backgroundColor { get; set; } = ConsoleColor.Black;
        public ConsoleColor foregroundColor { get; set; } = ConsoleColor.White;
        public ConsoleColor focusBackgroundColor { get; set; } = ConsoleColor.DarkBlue;
        public ConsoleColor focusForegroundColor { get; set; } = ConsoleColor.White;

        public void DrawFocused(Sprites spriteSet, Boolean inFocus)
        {
            if (inFocus)
            {
                Console.BackgroundColor = spriteSet.focusBackgroundColor;
                Console.ForegroundColor = spriteSet.focusForegroundColor;
            }
            else
            {
                Console.BackgroundColor = spriteSet.backgroundColor;
                Console.ForegroundColor = spriteSet.foregroundColor;
            }
        }

    }
}
