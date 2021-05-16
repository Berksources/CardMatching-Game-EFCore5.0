using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CORE_HBKSOFTWARE.Interfaces;
using EntityLayer.GameCart;
using Microsoft.AspNetCore.Http;

namespace CORE_HBKSOFTWARE.Classes
{
    public class FileUploader : IFileUploader
    {
        private FileRepo _fileRepo;
        public FileUploader(FileRepo fileRepo)
        {
            _fileRepo = fileRepo;
        }
        public async Task<FileRepo> FileUploadToDatabase(List<IFormFile> files)
        {
            foreach (var file in files)
            {
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var fileExtension = Path.GetExtension(file.FileName);
                _fileRepo = new FileRepo
                {
                    FileName = fileName,
                    FileExtension = fileExtension,
                    FileType = file.ContentType,
                    CreatedDate= DateTime.Now
                };
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    _fileRepo.FileData = dataStream.ToArray();
                }
            }
            return _fileRepo;
        }

        public async Task<FileRepo> FileUploadToPath(List<IFormFile> files)
        {
            foreach (var file in files)
            {
                var basePath = Path.Combine(Directory.GetCurrentDirectory() + "\\Files\\");
                bool basePathExists = Directory.Exists(basePath);
                if (!basePathExists) Directory.CreateDirectory(basePath);
                var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                var filePath = Path.Combine(basePath, file.FileName);
                var extension = Path.GetExtension(file.FileName);
                if (!File.Exists(filePath))
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    _fileRepo = new FileRepo
                    {
                        FileName = fileName,
                        FileType = file.ContentType,
                        FilePath = filePath
                    };
                }
            }
            return _fileRepo;
        }

        //Context'den kaynaklı ayarlamaları bitmedi.
        public Task<bool> FileDeleteFromPath(int id)
        {
            //var file = await context.FilesOnFileSystem.Where(x => x.Id == id).FirstOrDefaultAsync();
            //if (file == null) return null;
            //if (File.Exists(file.FilePath))
            //{
            //    File.Delete(file.FilePath);
            //}
            //context.FilesOnFileSystem.Remove(file);
            return Task.FromResult(true);
        }
    }
}