using System;
using System.IO;
using System.Collections.Generic;
namespace FileManager;

public class Parser
{
	private string space = "\t\t\t";

	public void Parse(FileEditor fe, string command, ref bool exit)
	{
		command = command.Trim();
		string[] commandParts = command.Split(" ");
		/*
		   If commandParts.Length is equal to two, command can be:
		   to create, or delete, or go, or search, or open, or createFd, or deleteFd, or read or, archive.
		   
		   If commandParts.Length is equal to four, command can be:
		   to move, or copy, or moveFd or, extract.
		   
		   If commandParts.Length is equal to one, command can be:
		   to exit, or get help, or clear or to go back.
		   
		   If commandParts equals none of above(1 or 2 or 4), command is invalid.
		 */
		if (commandParts.Length == 2)
		{
			switch (commandParts[0])
			{
				case "create":
					fe.CreateFile(commandParts[1]);
					break;
				case "delete":
					fe.DeleteFile(commandParts[1]);
					break;
				case "go":
					fe.GotoFolder(commandParts[1]);
					break;
				case "search":
					fe.SearchFiles(commandParts[1]);
					break;
				case "open":
					fe.OpenFile(commandParts[1]);
					break;
				case "createFd":
					fe.CreateFolder(commandParts[1]);
					break;
				case "deleteFd":
					fe.DeleteFolder(commandParts[1]);
					break;
				case "read":
					fe.ReadFile(commandParts[1]);
					break;
				case "archive":
					fe.Archive(commandParts[1]);
					break;
				default:
					Console.WriteLine($"{space}Error : Command is invalid");
					break;
			}
		}
		else if (commandParts.Length == 4)
		{
			switch (commandParts[0])
			{
				case "move":
					if (commandParts[2] == "to")
					{
						fe.MoveFile(commandParts[1], commandParts[3]);
					}
					else
					{
						Console.WriteLine($"{space}Error : Command is invalid");
					}
					break;
				case "copy":
					if (commandParts[2] == "to")
					{
						fe.CopyFile(commandParts[1], commandParts[3]);
					}
					else
					{
						Console.WriteLine($"{space}Error : Command is invalid");
					}
					break;
				case "moveFd":
					if (commandParts[2] == "to")
					{
						fe.MoveFolder(commandParts[1], commandParts[3]);
					}
					else
					{
						Console.WriteLine($"{space}Error : Command is invalid");
					}
					break;
				case "extract":
					if (commandParts[2] == "to")
					{
						fe.Extract(commandParts[1], commandParts[3]);
					}
					else
					{
						Console.WriteLine($"{space}Error : Command is invalid");
					}
					break;
				default:
					Console.WriteLine($"{space}Error : Command is invalid");
					break;
			}
		}
		else if (commandParts.Length == 1)
		{
			switch (commandParts[0])
			{
				case "exit":
					exit = true;
					break;
				case "help":
					Console.WriteLine(File.ReadAllText("help.txt"));
					break;
				case "clear":
					Console.Clear();
					break;
				case "back":
					fe.ToBack();
					break;
				default:
					Console.WriteLine($"{space}Error: Command is invalid");
					break;
			}
		}
		else
		{
			//Command is invalid
			Console.WriteLine($"{space}Error : Command is invalid");
		}
	}

}
