using EntityLayer.GameCart;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CORE_HBKSOFTWARE.Interfaces
{
    public interface IFileUploader
    {
        Task<FileRepo> FileUploadToDatabase(List<IFormFile> files);
        Task<FileRepo> FileUploadToPath(List<IFormFile> files);
        Task<bool> FileDeleteFromPath(int id);
    }
}