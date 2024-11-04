using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using ShopTARge23.Core.Domain;
using ShopTARge23.Core.Dto;
using ShopTARge23.Core.ServiceInterface;
using ShopTARge23.Data;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ShopTARge23.ApplicationServices.Services
{
    public class FileServices : IFileServices
    {
        private readonly IHostEnvironment _webHost;
        private readonly ShopTARge23Context _context;

        public FileServices(IHostEnvironment webHost, ShopTARge23Context context)
        {
            _webHost = webHost;
            _context = context;
        }

        public void FilesToApi(SpaceshipDto dto, Spaceship spaceship)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                if (!Directory.Exists(_webHost.ContentRootPath + "\\multipleFileUpload\\"))
            if (!Directory.Exists(Path.Combine(_webHost.ContentRootPath, "multipleFileUpload")))
            {
                Directory.CreateDirectory(Path.Combine(_webHost.ContentRootPath, "multipleFileUpload"));
            }

            foreach (var file in dto.Files)
            {
                string uploadsFolder = Path.Combine(_webHost.ContentRootPath, "multipleFileUpload");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Directory.CreateDirectory(_webHost.ContentRootPath + "\\multipleFileUpload\\");
                }

                foreach (var file in dto.Files)
                {
                    string uploadsFolder = Path.Combine(_webHost.ContentRootPath, "multipleFileUpload");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                        FileToApi path = new FileToApi
                        {
                            Id = Guid.NewGuid(),
                            ExistingFilePath = uniqueFileName,
                            SpaceshipId = spaceship.Id
                        };

                        _context.FileToApis.AddAsync(path);
                    }
                    _context.FileToApis.Add(path);
                }
            }
        }

        public async Task<FileToApi> RemoveImageFromApi(FileToApiDto dto)
        {
            var imageId = await _context.FileToApis
                .FirstOrDefaultAsync(x => x.Id == dto.Id);
            var filePath = _webHost.ContentRootPath + "\\multipleFileUpload\\"
                + imageId.ExistingFilePath;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            _context.FileToApis.Remove(imageId);
            await _context.SaveChangesAsync();
            return null;
        }

        public async Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos)
        {
            List<FileToApi> imagesToRemove = new List<FileToApi>();

            foreach (var dto in dtos)
            {
                var image = await _context.FileToApis
                    .FirstOrDefaultAsync(x => x.ExistingFilePath == dto.ExistingFilePath);
                var filePath = _webHost.ContentRootPath + "\\multipleFileUpload\\" + imageId.ExistingFilePath;


                if (image != null)
                {
                    var filePath = Path.Combine(_webHost.ContentRootPath, "multipleFileUpload", image.ExistingFilePath);
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }

                    imagesToRemove.Add(image);
                }
            }

            _context.FileToApis.RemoveRange(imagesToRemove);
            await _context.SaveChangesAsync();

            return imagesToRemove;
        }

        public void UploadFilesToData(KindergartenDto dto, Kindergarten domain)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                foreach (var image in dto.Files)
                {
                    string uploadsFolder = Path.Combine(_webHost.ContentRootPath, "multipleFileUpload");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(fileStream);

                        FileToData files = new FileToData()
                        {
                            Id = Guid.NewGuid(),
                            ImageTitles = image.FileName,
                            ExistingFilePath = uniqueFileName, // Store the path instead of image data
                            KindergartenId = domain.Id
                        };

                        _context.FileToDatas.Add(files);
                    }
                }
            }
        }

        public async Task<FileToData> RemoveImageFromData(FileToDataDto dto)
        {
            var image = await _context.FileToDatas
                .Where(x => x.Id == dto.Id)
                .FirstOrDefaultAsync();

            if (image != null)
            {
                var filePath = Path.Combine(_webHost.ContentRootPath, "multipleFileUpload", image.ExistingFilePath);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                _context.FileToApis.Remove(imageId);
                await _context.SaveChangesAsync();
            }

            return null;
        }

        public void UploadFilesToDatabase(RealEstateDto dto, RealEstate domain)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                foreach (var image in dto.Files)
                {
                    using (var target = new MemoryStream())
                    {
                        FileToDatabase files = new FileToDatabase()
                        {
                            Id = Guid.NewGuid(),
                            ImageTitle = image.FileName,
                            RealEstateId = domain.Id
                        };
                        image.CopyTo(target);
                        files.ImageData = target.ToArray();

                        _context.FileToDatabases.Add(files);
                    }
                }
            }
        }
        public async Task<FileToDatabase> RemoveImageFromDatabase(FileToDatabaseDto dto)
        {
            var image = await _context.FileToDatabases

                .Where(x => x.Id == dto.Id)
                .FirstOrDefaultAsync();

            _context.FileToDatabases.Remove(image);
            await _context.SaveChangesAsync();

            return image;
        }

        public async Task<FileToDatabase> RemoveImagesFromDatabase(FileToDatabaseDto[] dtos)
        {
            foreach (var dto in dtos)
            {
                var image = await _context.FileToDatabases
                    .Where(x => x.Id == dto.Id)
                    .FirstOrDefaultAsync();

                _context.FileToDatabases.Remove(image);
                await _context.SaveChangesAsync();
            }
            return null;
                _context.FileToDatas.Remove(image);
                await _context.SaveChangesAsync();
            }

            return image; // Consider returning null if the image is not found
        }

        public async Task<FileToData> RemoveImagesFromData(FileToDataDto[] dtos)
        {
            List<FileToData> imagesToRemove = new List<FileToData>(); // Initialize a list to hold removed images

            foreach (var dataDto in dtos)
            {
                var image = await _context.FileToDatas
                    .Where(x => x.Id == dataDto.Id)
                    .FirstOrDefaultAsync();

                if (image != null)
                {
                    var filePath = Path.Combine(_webHost.ContentRootPath, "multipleFileUpload", image.ExistingFilePath);
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath); // Delete the physical file
                    }

                    _context.FileToDatas.Remove(image);
                    imagesToRemove.Add(image); // Capture the removed image
                }
            }

            await _context.SaveChangesAsync();

            return imagesToRemove.Count > 0 ? imagesToRemove.Last() : null; // Return the last removed image, or null if none were removed
        }
    }
}
