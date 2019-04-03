using System;
using System.Web;
using System.Web.Mvc;

namespace FileManager
{
    interface FileController
    {
        // Получить список данных о файлах в папке в JSON строке
        String getFileList();

        // Удалить файл из папки
        String RemoveFile(int id);

        // Прочитать изображение
        ActionResult ReadPicture(int id);

        // Прочитать текстовый файл
        String ReadText(int id);

        // Добавить файл в папку
        String AddFile(HttpPostedFileBase file);
        
    }
}
