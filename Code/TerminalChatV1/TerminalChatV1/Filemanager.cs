using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalChatClient
{
    internal class Filemanager
    {
        public string appPath {  get; set; } = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public string directoryPath {  get; set; } 
        public string userPath { get; set; }
        public string serverProfilePath { get; set; }
        public string messagePath { get; set; }
        public void SetupAppDir()
        {
            
        }
        public void CreateServerProfile()
        {

        }
        public string GetLocalUserPath()
        {
            return "to be added";
        }
        public string GetServerProfilePath(int serverIndex) 
        {
            return "to be added";
        }
        public string GetMessagelogPath(int channelId)
        {
            return "to be added";
        }
    }
}
