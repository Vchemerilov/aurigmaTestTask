using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Script.Serialization;


namespace FileManager
{
    public class FileFolderController : Controller, FileController
    {
        static private List<FileFolder> filesFolder = new List<FileFolder>();

        static private Manager getManager()
        {
            return new FileFolderManager();
        }

        public String getFileList()
        {
            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
            Manager fileFolderManager = getManager();
            filesFolder = fileFolderManager.getFilesFolderData();

            var TheJson = TheSerializer.Serialize(filesFolder);

            return TheJson;
        }

        public String RemoveFile(int id)
        {
            Manager fileFolderManager = getManager();
            fileFolderManager.removeFileFolderTo(id, filesFolder);

            return fileFolderManager.getNameFromId(id, filesFolder);
        }

        public ActionResult ReadPicture(int id)
        {
            Manager fileFolderManager = getManager();
            String pathtoFileFolder = fileFolderManager.getPathToFileFolder(id, filesFolder);

            return File(System.IO.File.ReadAllBytes(pathtoFileFolder), "application/octet-stream");

        }

        public String ReadText(int id)
        {
            Manager fileFolderManager = getManager();
            String pathToFileFolder = fileFolderManager.getPathToFileFolder(id, filesFolder);

            return System.IO.File.ReadAllText(pathToFileFolder);

        }

        public String AddFile(HttpPostedFileBase file)
        {
            string result = "Файл не сохранен";
            try
            {
                Manager fileFolderManager = getManager();
                string filename = file.FileName;

                if (!fileFolderManager.containsFileInFolder(filename, filesFolder))
                {
                    if (file != null)
                    {
                        var fileName = string.Format("{0}\\{1}", fileFolderManager.getDirectory(), file.FileName);
                        file.SaveAs(fileName);
                        result = "Файл сохранен";
                    }
                }
                else
                    result = "Данный файл уже присутствует в папке";
            }
            catch (IOException)
            {
                result = "Ошибка при обработке файла";
            }

            return result;
        }
    }
}