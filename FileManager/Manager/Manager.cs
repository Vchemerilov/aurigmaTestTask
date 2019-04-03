using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    interface Manager
    {
        // получить данные о файле из папки на сервере
        List<FileFolder> getFilesFolderData();

        // удалить файл в папке на сервере
        void removeFileFolderTo(int id, List<FileFolder> filesFolder);

        // проверить, есть ли в данной папке загружаемый на сервер файл 
        bool containsFileInFolder(String filename, List<FileFolder> filesFolder);

        // получить полный путь к файлу на сервере
        String getPathToFileFolder(int id, List<FileFolder> filesFolder);

        // получить директорию файла
        String getDirectory();

        // получить полное имя файла по id
        String getNameFromId(int id, List<FileFolder> filesFolder);     
    }
}
