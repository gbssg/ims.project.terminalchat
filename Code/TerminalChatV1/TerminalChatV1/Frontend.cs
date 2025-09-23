using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalChatClient
{
    //Size and positions need to be fitted, future implementation of responsive sizes planned
    internal class InfoBox : AbstComponent
    {
        public Position position { get; set; } = new Position(0,50);
        public Size size { get; set; } = new Size(50, 10);
        public Sprites spriteSet { get; set; } = new Sprites();
        public Boolean inFocus { get; set; }

        //Implementation after dataclasses/management are implemented

        //public void PrintServerlist(ServerProfiles[] serverlist) { }
        //public void PrintChannellist(ServerProfile serverprofile, int amount) { }
        //public void PrintUserList(Serverprofile serverprofile) { }

    }

    internal class MessageBox : AbstComponent
    {
        public Position position { get; set; } = new Position(0, 50);
        public Size size { get; set; } = new Size(50, 10);
        public Boolean inFocus { get; set; }
        public int serverId { get; set; }
        public int channelId { get; set; }

        //Implementation after dataclasses/management are implemented

        //public void PrintMessages(int serverId, int channelId) { }



    }
    internal class  TextBox : AbstComponent
    {
        public Position position { get; set; } = new Position(0, 50);
        public Size size { get; set; } = new Size(50, 10);
        public Boolean inFocus { get; set; }
        public string message { get; set; }

        //Implementation after dataclasses/management are implemented

        //public void InputMessage() { }
    }
}
