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
		public void UserSetupPrompt()
		{
			// read setupuserlist form user.json
			SetupUserList sul = rwd.ReadSetupUserlist();

			var returnValue = sul.setupUsers[0];
			bool loopRun = true;

			do
			{
				//Menue section needs to be revamped
				Console.Clear();
				Console.WriteLine("Choose a username or option:");
				Console.WriteLine($"0: {sul.setupUsers[0].name}");
				Console.WriteLine($"1: {sul.setupUsers[1].name}");
				Console.WriteLine($"2: {sul.setupUsers[2].name}");
				Console.WriteLine($"3: {sul.setupUsers[3].name}");
				Console.WriteLine($"4: {sul.setupUsers[4].name}");
				Console.WriteLine("5: create new username");

				ConsoleKey answer = Console.ReadKey().Key;

				switch (answer)
				{
					case ConsoleKey.D0:
						returnValue = sul.setupUsers[0];
                        loopRun = false;
                        break;

					case ConsoleKey.D1:
						returnValue = sul.setupUsers[1];
                        loopRun = false;
                        break;

					case ConsoleKey.D2:
						returnValue = sul.setupUsers[2];
                        loopRun = false;
                        break;

					case ConsoleKey.D3:
						returnValue = sul.setupUsers[3];
                        loopRun = false;
                        break;

					case ConsoleKey.D4:
						returnValue = sul.setupUsers[4];
                        loopRun = false;
                        break;

					case ConsoleKey.D5:
						CreateSetupUser();
                        loopRun = false;
                        break;

				}
				Console.Clear();
				Console.WriteLine("Please select one of the given option, to enter press the number next to the option.");
			} while (loopRun);
		}
		public void CreateSetupUser()
		{
			SetupUser user = new SetupUser();
			string? name;
			bool loop = true;
			do
			{
				Console.Clear();
				Console.WriteLine("Enter your new Username:");

				name = Console.ReadLine();

				if (name.Length <= 0 || name == null)
				{
					Console.WriteLine("the name must be longer than zero");
				}
				else
				{

					user.name = name;
					loop = false;
				}
			} while (loop);
			rwd.UpdateSetupUserlist(user);
		}
	}
}
