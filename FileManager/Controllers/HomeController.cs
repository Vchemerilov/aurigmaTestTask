using System;
using System.Web;
using System.Web.Mvc;

namespace FileManager
{
    public class HomeController : Controller , FileController
    {
        static private FileController ActiveController = new FileFolderController();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public String getFileList()
        {
            return ActiveController.getFileList();
        }

        [HttpPost]
        public String RemoveFile(int id)
        {
            return ActiveController.RemoveFile(id);
        }
        
        [HttpGet]
        public ActionResult ReadPicture(int id)
        {
            return ActiveController.ReadPicture(id); 
        }
        
        [HttpGet]
        public String ReadText(int id)
        {
            return ActiveController.ReadText(id);
        }

        [HttpPost]
        public String AddFile(HttpPostedFileBase file)
        {
            return ActiveController.AddFile(file);
        }
    }
}