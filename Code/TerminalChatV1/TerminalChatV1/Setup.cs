using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalChatClient
{
    public class Setup
    {
        public ReadWriteData rwd { get; set; } = new ReadWriteData();
        public string name {  get; set; }


        // displays the first 5 Users to choose from (0-4).
        public SetupUser UserSetupPrompt()
        {
            SetupUserList sul = rwd.ReadSetupUserlist();
            string answer;
            do
            {
                Console.Clear();
                Console.WriteLine("Choose a username or option:");
                Console.WriteLine($"0: {sul.users[0]}");
                Console.WriteLine($"1: {sul.users[1]}");
                Console.WriteLine($"2: {sul.users[2]}");
                Console.WriteLine($"3: {sul.users[3]}");
                Console.WriteLine($"4: {sul.users[4]}");
                Console.WriteLine("5: create new username");

                answer = Console.ReadLine();

            } while (answer != "0" || answer != "1" || answer != "2" || answer != "3" || answer != "4" || answer != "5");

            switch (answer)
            {
                case "0":
                    return sul.users[0];
                    break;
                case "1":
                    return sul.users[1];
                    break;
                case "2":
                    return sul.users[2];
                    break;
                case "3":
                    return sul.users[3];
                    break;
                case "4":
                    return sul.users[4];
                    break;
                default:
                    return CreateSetupUser();
                    break;
            }
        }
        public SetupUser CreateSetupUser()
        {
            SetupUser user = new SetupUser();
            string? name;
            bool loop = true;
            do {
                Console.Clear();
                Console.WriteLine("Enter your new Username:");

                name = Console.ReadLine();

                if (name.Length <= 0 || name == null)
                {
                    Console.WriteLine("the name must be longer than zero");
                }else
                {
                    
                    user.name = name;
                    loop = false;
                }
            } while (loop);


            return user;
        }
    }
}
