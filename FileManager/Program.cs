/*
   Author : @itx-benney
   Project name : Android File Manager
   Project version : 1.0
   Updated on : 19 May 2023 1:12 Am
   Description : This file manager is mainly built for android devices.
                 Features are not completed yet, and I'm trying to add more.
				 If you have any suggestions for this program, feel free to say.
				 Hope you will love it :)
   ## WARNING ##
   This file manager is for android devices and if you try to run this in a Computer,
   IT WILL CATCH MANY ERRORS: USE AN ANDROID DEVICE OR EMULATOR, OR CHANGE THE PATH.
                 
 */
using System;

namespace FileManager;

public static class Program
{
	public static void Main()
	{
		FileEditor fe = new FileEditor("/storage/emulated/0/");
		Parser ps = new Parser();
		bool exit = false;
		
		Console.WriteLine("××××× FileManger ×××××\nType `help` for command guide.\n\n");
		
		while(!exit)
		{
			Console.Write($"{fe.CurrentFullPath}>> #");
			string command = Console.ReadLine();
			ps.Parse(fe, command, ref exit);
		}
	}
}
