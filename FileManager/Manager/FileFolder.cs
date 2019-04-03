using System;

namespace FileManager
{
    public class FileFolder
    {
        // id файла
        public int id {get; set;}

        // полное имя файла
        public String name {get; set;}

        // расширение файла
        public String extension { get; set;}

        // размер файла
        public long size {get; set;}

        // дата последней модификации
        public String lastModification {get; set;}

        public FileFolder(int fid, String fname, String fextension, long fsize, String flastModification)
        {
            id = fid;
            name = fname;
            extension = fextension;
            size = fsize;
            lastModification = flastModification;
        }
    }
}