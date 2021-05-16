using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CORE_HBKSOFTWARE.Models
{
    public class FileModel
    {
        public int FileID { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FileExtension { get; set; }
        public string FilePath { get; set; }
        public byte[] Data { get; set; }
        //public List<IFormFile> files { get; set; }
        //Bu model entity layer'da base classdan kalıtılacak.
    }
}