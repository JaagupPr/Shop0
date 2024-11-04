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
                string uploadsFolder = Path.Combine(_webHost.ContentRootPath, "multipleFileUpload");

                // Ensure the directory exists
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                foreach (var file in dto.Files)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream); // Copy file to the directory
                    }

                    // Create and add the new FileToApi entity
                    var path = new FileToApi
                    {
                        Id = Guid.NewGuid(),
                        ExistingFilePath = uniqueFileName,
                        SpaceshipId = spaceship.Id
                    };

                    _context.FileToApis.Add(path);
                }

                _context.SaveChanges(); // Save changes after all files are processed
            }
        }

        public async Task<FileToApi> RemoveImageFromApi(FileToApiDto dto)
        {
            var image = await _context.FileToApis
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (image != null)
            {
                var filePath = Path.Combine(_webHost.ContentRootPath, "multipleFileUpload", image.ExistingFilePath);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath); // Delete the file
                }

                _context.FileToApis.Remove(image);
                await _context.SaveChangesAsync(); // Save changes
            }

            return image; // Return the removed image
        }

        public async Task<List<FileToApi>> RemoveImagesFromApi(FileToApiDto[] dtos)
        {
            List<FileToApi> imagesToRemove = new List<FileToApi>();

            foreach (var dto in dtos)
            {
                var image = await _context.FileToApis
                    .FirstOrDefaultAsync(x => x.Id == dto.Id);

                if (image != null)
                {
                    var filePath = Path.Combine(_webHost.ContentRootPath, "multipleFileUpload", image.ExistingFilePath);
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath); // Delete the file
                    }

                    imagesToRemove.Add(image);
                }
            }

            _context.FileToApis.RemoveRange(imagesToRemove);
            await _context.SaveChangesAsync(); // Save changes
            return imagesToRemove; // Return the list of removed images
        }

        public void UploadFilesToData(KindergartenDto dto, Kindergarten domain)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                string uploadsFolder = Path.Combine(_webHost.ContentRootPath, "multipleFileUpload");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                foreach (var image in dto.Files)
                {
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(fileStream); // Copy file to the directory
                    }

                    var files = new FileToData()
                    {
                        Id = Guid.NewGuid(),
                        ImageTitles = image.FileName,
                        ExistingFilePath = uniqueFileName, // Store the path instead of image data
                        KindergartenId = domain.Id
                    };

                    _context.FileToDatas.Add(files); // Add to context
                }

                _context.SaveChanges(); // Save changes after all files are processed
            }
        }

        public async Task<FileToData> RemoveImageFromData(FileToDataDto dto)
        {
            var image = await _context.FileToDatas
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (image != null)
            {
                var filePath = Path.Combine(_webHost.ContentRootPath, "multipleFileUpload", image.ExistingFilePath);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath); // Delete the file
                }

                _context.FileToDatas.Remove(image);
                await _context.SaveChangesAsync(); // Save changes
            }

            return image; // Return the removed image
        }

        public void UploadFilesToDatabase(RealEstateDto dto, RealEstate domain)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                foreach (var image in dto.Files)
                {
                    using (var target = new MemoryStream())
                    {
                        image.CopyTo(target); // Copy file to memory stream

                        var files = new FileToDatabase()
                        {
                            Id = Guid.NewGuid(),
                            ImageTitle = image.FileName,
                            RealEstateId = domain.Id,
                            ImageData = target.ToArray() // Store image data in byte array
                        };

                        _context.FileToDatabases.Add(files); // Add to context
                    }
                }

                _context.SaveChanges(); // Save changes after all files are processed
            }
        }

        public async Task<FileToDatabase> RemoveImageFromDatabase(FileToDatabaseDto dto)
        {
            var image = await _context.FileToDatabases
                .FirstOrDefaultAsync(x => x.Id == dto.Id);

            if (image != null)
            {
                _context.FileToDatabases.Remove(image);
                await _context.SaveChangesAsync(); // Save changes
            }

            return image; // Return the removed image
        }

        public async Task<List<FileToDatabase>> RemoveImagesFromDatabase(FileToDatabaseDto[] dtos)
        {
            List<FileToDatabase> imagesToRemove = new List<FileToDatabase>();

            foreach (var dto in dtos)
            {
                var image = await _context.FileToDatabases
                    .FirstOrDefaultAsync(x => x.Id == dto.Id);

                if (image != null)
                {
                    imagesToRemove.Add(image); // Capture the image to be removed
                }
            }

            if (imagesToRemove.Count > 0)
            {
                _context.FileToDatabases.RemoveRange(imagesToRemove);
                await _context.SaveChangesAsync(); // Save changes
            }

            return imagesToRemove; // Return the list of removed images
        }

        public async Task<List<FileToData>> RemoveImagesFromData(FileToDataDto[] dtos)
        {
            List<FileToData> imagesToRemove = new List<FileToData>(); // Initialize a list to hold removed images

            foreach (var dataDto in dtos)
            {
                // Retrieve the image based on the provided ID
                var image = await _context.FileToDatas
                    .FirstOrDefaultAsync(x => x.Id == dataDto.Id);

                // If the image exists, proceed to remove it
                if (image != null)
                {
                    // Construct the file path for deletion
                    var filePath = Path.Combine(_webHost.ContentRootPath, "multipleFileUpload", image.ExistingFilePath);
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath); // Delete the physical file
                    }

                    _context.FileToDatas.Remove(image); // Remove the image from the context
                    imagesToRemove.Add(image); // Add the removed image to the list
                }
            }

            await _context.SaveChangesAsync(); // Save all changes to the database

            return imagesToRemove; // Return the list of removed images
        }

    }
}
