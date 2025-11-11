using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerminalChatClient
{
	public class SetupLocalUser
	{
		public ReadWriteData readWriteData { get; set; } = new ReadWriteData();

		// displays the first 5 Users to choose from (0-4).
		public void UserSetupPrompt()
		{
			// read setupuserlist form user.json
			LocalUsers localUsers = readWriteData.ReadSetupUserlist();

			bool loopRun = true;

			do
			{
				//Menue section needs to be revamped
				Console.Clear();
				Console.WriteLine("Choose a username or option:");
				Console.WriteLine($"1: {localUsers.SetupUsers[0].Name}");
				Console.WriteLine("2: create new username");

				ConsoleKey answer = Console.ReadKey().Key;

				switch (answer)
				{
					case ConsoleKey.D1:
						Console.WriteLine(localUsers.SetupUsers[0]);
						loopRun = false;
						break;

					case ConsoleKey.D2:
						CreateSetupUser();
						loopRun = false;
						break;

				}
				Console.Clear();
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
					Console.WriteLine("the name must be longer than zero!");
				}
				else if (name.Length >= 32)
				{
					Console.WriteLine("the name must be shorter than 32 characters!");
				} else
				{
					user.Name = name;
					loop = false;
				}
			} while (loop);
			readWriteData.UpdateSetupUserlist(user);
		}
	}
}
