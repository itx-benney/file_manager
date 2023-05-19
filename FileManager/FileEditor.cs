using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;

namespace FileManager;

public class FileEditor
{
	private string LocalPath { get; set; }
	public string CurrentDirectory { get; private set; }
	public string CurrentFullPath { get; private set; }

	public FileEditor(string localPath, string currentDirectory = "")
	{
		LocalPath = localPath;
		CurrentDirectory = currentDirectory;
		CurrentFullPath = $"{LocalPath}{CurrentDirectory}";
	}

	//CREATE A NEW FOLDER WITH ENTERED NAME
	public void CreateFolder(string folderName)
	{
		if (IsFolderOk(folderName))
		{
			try
			{
				DirectoryInfo df = Directory.CreateDirectory($"{CurrentFullPath}{folderName}/");
				CurrentDirectory = folderName + "/";
				CurrentFullPath = df.FullName;
				Console.WriteLine(folderName + " successfully created");
			}
			catch (Exception e)
			{
				Console.WriteLine("Error : " + e.Message);
			}
		}
		else
		{
			Console.WriteLine("\t\t\tAlready exists");
		}
	}

	//MOVE FOLDER TO ANOTHR FOLER
	public void MoveFolder(string from, string to)
	{
		to = CurrentFullPath + to + "/" + from;
		from = CurrentFullPath + "/";
		if (!(IsFolderOk(from) && IsFolderOk(to)))
		{
			try
			{
				Directory.Move(from, to);
				Console.WriteLine("\t\t\tMoved Successfully");
			}
			catch (Exception e)
			{
				Console.WriteLine("Error : " + e.Message);
			}
		}
		else
		{
			Console.WriteLine((IsFolderOk(from)) ? "Destination folder doesn\'t exist" : "Source folder doesn\'t exist");
		}
	}

	//DELETE THE WHOLE FOLDER
	public void DeleteFolder(string folderName)
	{
		if (!IsFolderOk(folderName))
		{
			try
			{
				folderName = CurrentFullPath + folderName + "";
				Directory.Delete(folderName);
				Console.WriteLine("\t\t\tDeleted successfully");
			}
			catch (Exception e)
			{
				Console.WriteLine("Error : " + e.Message);
			}
		}
		else
		{
			Console.WriteLine("\t\t\tFolder doesn\'t exist");
		}
	}

	//CREATE A NEW FILE IN CURRENT DIRECTORY
	public void CreateFile(string fileName)
	{
		if (IsFileOk(fileName))
		{
			try
			{
				fileName = CurrentFullPath + fileName;
				FileStream fs = File.Create(fileName);
				Console.WriteLine("\t\t\t" + fs.Name + " successfully created");
				fs.Close();
				EditFile(fileName);
			}
			catch (Exception e)
			{
				Console.WriteLine("Error : " + e.Message);
			}
		}
		else
		{
			Console.WriteLine("Already exists");
		}
	}

	//MOVE FILE FROM SOURCE TO ENTERED DESTINATION
	public void MoveFile(string from, string to)
	{
		if (!(IsFileOk(from) && IsFolderOk(to)))
		{
			try
			{
				to = CurrentFullPath + to + "/" + from;
				from = CurrentFullPath + from;
				File.Move(from, to, true);
				Console.WriteLine("\t\t\tMoved successfully");
			}
			catch (Exception e)
			{
				Console.WriteLine("Error : " + e.Message);
			}
		}
		else
		{
			Console.WriteLine((IsFileOk(from)) ? "Destination folder doesn\'t exist" : "Source file doesn\'t exist");
		}
	}

	//COPY FILE FROM SOURCE TO ENTERED DESTINATION
	public void CopyFile(string from, string to)
	{
		if (!(IsFileOk(from) && IsFolderOk(to)))
		{
			try
			{
				to = CurrentFullPath + to + "/" + from;
				from = CurrentFullPath + from;
				File.Copy(from, to, true);
				Console.WriteLine("\t\t\tSuccessfully copied");
			}
			catch (Exception e)
			{
				Console.WriteLine("Error : " + e.Message);
			}
		}
		else
		{
			Console.WriteLine((IsFileOk(from)) ? "Destination folder doesn\'t exist" : "Source file doesn\'t exist");
		}
	}

	//DELETE THE FILE
	public void DeleteFile(string fileName)
	{
		if (!IsFileOk(fileName))
		{
			try
			{
				fileName = CurrentFullPath + fileName;
				File.Delete(fileName);
				Console.WriteLine("\t\t\tDeleted successfully");
			}
			catch (Exception e)
			{
				Console.WriteLine("Error : " + e.Message);
			}
		}
		else
		{
			Console.WriteLine("File doesn\'t exist");
		}
	}

	//OPEN FILE AND SHOW FILE INFO
	public void OpenFile(string fileName)
	{
		FileStream fs;
		if (!IsFileOk(fileName))
		{
			//FileInfo fi = new FileInfo(fileName);
			//Console.WriteLine($"\t\tName = {fi.Name}\n\t\tExtension = {fi.Extension}\n\t\tCreation Date = {fi.CreationTime}");
			//Console.WriteLine($"\t\tLast Accessed = {fi.LastAccessTimeUtc}\n\t\tLast Wrote = {fi.LastWriteTimeUtc}");
			ReadFile(fileName);
			fileName = CurrentFullPath + fileName;
			fs = File.OpenWrite(fileName);
			fs.Close();
			EditFile(fileName);
		}
		else
		{
			Console.WriteLine("\t\t\tFile doesn\'t exist");
		}
	}

	//SEARCH FILES WITH KEYWORD
	public void SearchFiles(string keyword)
	{
		string[] currentAllFiles = Directory.GetFiles($"{CurrentFullPath}", "*", SearchOption.AllDirectories);

		foreach (string file in currentAllFiles)
		{
			if (file.Contains(keyword))
			{
				Console.WriteLine("\t\t\t" + new FileInfo(file).Name);
			}
		}
	}

	//WRITE NEW INFO WHEN CREATE FILES OR OPEN EXISTING FILES
	private void EditFile(string sourceFile)
	{
		try
		{
			Console.Write("\t\t\tWrite >>");
			string newInfo = Console.ReadLine();
			File.AppendAllText(sourceFile, newInfo);
			Console.WriteLine("\t\t\tFile Saved");
		}
		catch (Exception)
		{
			Console.WriteLine("\t\t\tError : file can't be saved");
		}
	}

	//READ CONTENTS OF A FILE
	public void ReadFile(string fileName)
	{
		if (!IsFileOk(fileName))
		{
			try
			{
				fileName = CurrentFullPath + fileName;
				Console.WriteLine("\t\t\t" + File.ReadAllText(fileName));
			}
			catch (Exception)
			{
				Console.WriteLine("\t\t\tError : file can't be read");
			}
		}
		else
		{
			Console.WriteLine("\t\t\tFile doesn't exist");
		}
	}

	//GO TO THE ENTERED DIRECTORY
	public void GotoFolder(string folderName)
	{
		if (!IsFolderOk(folderName))
		{
			CurrentDirectory = folderName + "/";
			CurrentFullPath = $"{CurrentFullPath}{CurrentDirectory}";
		}
		else
		{
			Console.WriteLine(folderName + " doesn't exist");
		}
	}

	//GO BACK TO PREVIOUS DIRECTORY
	public void ToBack()
	{
		if (CurrentFullPath != "/storage/emulated/0/")
		{
			DirectoryInfo di = new DirectoryInfo(CurrentFullPath + CurrentDirectory).Parent;
			CurrentFullPath = di.FullName.Remove(di.FullName.LastIndexOf('/'), (di.FullName.Length - di.FullName.LastIndexOf('/'))) + "/";
		}
	}

	//To check whether folder is already exist or not
	private bool IsFolderOk(string folderName)
	{
		folderName = CurrentFullPath + folderName + "/";
		if (!Directory.Exists(folderName))
		{
			return true;
		}
		return false;
	}
	//To check whether file is already exist or not
	private bool IsFileOk(string fileName)
	{
		fileName = CurrentFullPath + fileName;
		if (!File.Exists(fileName))
		{
			return true;
		}
		return false;
	}

	//HERE ARE THE CODES TO MANIPULATE THE ARCHIVES
	public void Extract(string zipName, string destination)
	{
		if (!(IsFileOk(zipName) && IsFolderOk(destination)))
		{
			try
			{
				zipName = CurrentFullPath + zipName;
				destination = CurrentFullPath + destination + "/";
				ZipFile.ExtractToDirectory(zipName, destination, true);
				Console.WriteLine("\t\t\tExtracted successfully");
			}
			catch (Exception e)
			{
				Console.WriteLine($"Error : {e.Message}");
			}
		}
		else
		{
			Console.WriteLine((IsFileOk(zipName)) ? "Zip file does\'t found" : "Destination path does\'t found");
		}
	}

	//ARCHIVES A DIRECTORY TO .ZIP
	public void Archive(string folderName)
	{
		if (!IsFolderOk(folderName))
		{
			try
			{
				folderName = CurrentFullPath + folderName;
				string resultPath = CurrentFullPath + new DirectoryInfo(folderName).Name + ".zip";
				ZipFile.CreateFromDirectory(folderName, resultPath);
				Console.WriteLine($"Archive saved to {resultPath}");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error : {ex.Message}");
			}
		}
		else
		{
			Console.WriteLine("Folder does not exist");
		}
	}
}