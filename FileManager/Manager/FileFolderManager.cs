using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.Linq;

namespace FileManager
{
    public class FileFolderManager : Manager
    {
        static String pathToDirectory = ConfigurationManager.AppSettings["pathToFolder"];

        // получить данные о файле из папки на сервере
        public List<FileFolder> getFilesFolderData()
           {

               List<FileFolder> filesFolder = new List<FileFolder>();

               DirectoryInfo info = new DirectoryInfo(pathToDirectory);
               FileInfo[] files = info.GetFiles();

               for (int i = 0; i < files.Length; i++)
               {
                   if (files[i].Exists)
                       filesFolder.Add(new FileFolder(i, files[i].Name, files[i].Extension, files[i].Length, files[i].LastWriteTime.ToString()));
               }

               return filesFolder;
           }
       
        // удалить файл в папке на сервере
        public void removeFileFolderTo(int id, List<FileFolder> filesFolder)
        {
            FileInfo fileInf = new FileInfo(getPathToFileFolder(id, filesFolder));

            if (fileInf.Exists)
            {
                fileInf.Delete();
            }
        }
      
        // получить полный путь к файлу на сервере
        public String getPathToFileFolder(int id, List<FileFolder> filesFolder)
        {
            return Path.Combine(pathToDirectory, getNameFromId(id, filesFolder));
        }

        // получить директорию файла
        public String getDirectory()
        {
            return pathToDirectory;
        }

        // получить полное имя файла по id
        public String getNameFromId(int id, List<FileFolder> filesFolder)
        {
            FileFolder fileFolder = filesFolder.Find(e => e.id == id);
            return fileFolder.name;
        }

        // проверить, есть ли в данной папке загружаемый на сервер файл 
        public bool containsFileInFolder(String filename, List<FileFolder> filesFolder)
        {
            return filesFolder.Any(e => e.name == filename);
        }
    }
}