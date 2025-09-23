using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalChatClient
{
    internal abstract class AbstComponent
    {
        public Position position { get; set; }
        public Size size { get; set; }
        public Sprites spriteSet { get; set; } = new Sprites();
        public Boolean inFocus { get; set; } = false;
        public void drawComponent(Boolean inFocus)
        {
            Console.SetCursorPosition(position.col, position.row);
            //draw top border
            for (int i = 0; i < size.width; i++)
            {
                if (i == 0)
                {
                    Console.Write(spriteSet.nw);
                }
                else if (i == size.width)
                {
                    Console.Write(spriteSet.ne);
                }
                else
                {
                    Console.Write(spriteSet.sh);
                }
            }

            //draw sides
            for (int i = 0; i < size.height - 2; i++)
            {
                Console.SetCursorPosition(position.col, position.row + 1 + i);
                Console.Write(spriteSet.sv);
                Console.SetCursorPosition(position.col + size.width - 1, position.row + 1 + i);
                Console.Write(spriteSet.sv);
            }
            //draw bottom border
            Console.SetCursorPosition(position.col, position.row + size.height - 1);
            for (int i = 0; i < size.width; i++)
            {
                if (i == 0)
                {
                    Console.Write(spriteSet.sw);
                }
                else if (i == size.width)
                {
                    Console.Write(spriteSet.se);
                }
                else
                {
                    Console.Write(spriteSet.sh);
                }
            }
        }
    }
}
