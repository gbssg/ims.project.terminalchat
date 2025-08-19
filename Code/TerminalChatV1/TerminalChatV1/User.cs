using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TerminalChatClient
{
    internal class User
    {
        public string? name;
        public DateTime creationTime;
    
        public static User SetUser()
        {
            string tempname = "";
            while (string.IsNullOrWhiteSpace(tempname))
            {
                Console.WriteLine("Please Set a Username");
                tempname = Console.ReadLine();
                Console.Clear();
            }

            var user = new User
            {
                name = tempname,
                creationTime = DateTime.Now
            };

            string fileName2 = Path.Combine(
                Directory.GetParent(AppContext.BaseDirectory).Parent.Parent.Parent.FullName);
            string fileName = @"C:\Users\yvesj\source\ims.project.terminalchat\Code\TerminalChatV1\TerminalChatV1\Data\user.json";
            string jsonString = JsonSerializer.Serialize(user);
            File.WriteAllText(fileName, jsonString);

            Console.WriteLine($"User saved at: {Path.GetFullPath(fileName)}");
            Console.ReadKey();

            return user;
        }
    }

}
