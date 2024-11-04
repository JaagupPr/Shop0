using Microsoft.EntityFrameworkCore;
using ShopTARge23.Core.Domain;
using ShopTARge23.Core.Dto;
using ShopTARge23.Core.ServiceInterface;
using ShopTARge23.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopTARge23.ApplicationServices.Services
{
    public class KindergartensServices : IKindergartensServices
    {
        private readonly ShopTARge23Context _context;
        private readonly IFileServices _fileServices;

        public KindergartensServices(ShopTARge23Context context, IFileServices fileServices)
        {
            _context = context;
            _fileServices = fileServices;
        }

        public async Task<Kindergarten> Create(KindergartenDto dto)
        {
            Kindergarten kindergarten = new()
            {
                Id = Guid.NewGuid(),
                GroupName = dto.GroupName,
                ChildrenCount = dto.ChildrenCount,
                KindergartenName = dto.KindergartenName,
                Teacher = dto.Teacher,
                UpdatedAt = DateTime.Now,
                CreatedAt = DateTime.Now
            };

            if (dto.Files != null)
            {
                _fileServices.UploadFilesToData(dto, kindergarten);
            }

            await _context.Kindergartens.AddAsync(kindergarten);
            await _context.SaveChangesAsync();

            return kindergarten;
        }

        public async Task<Kindergarten> DetailAsync(Guid id)
        {
            return await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Kindergarten> Update(KindergartenDto dto)
        {
            Kindergarten domain = new()
            {
                Id = dto.Id,
                GroupName = dto.GroupName,
                ChildrenCount = dto.ChildrenCount,
                KindergartenName = dto.KindergartenName,
                Teacher = dto.Teacher,
                UpdatedAt = DateTime.Now,
                CreatedAt = dto.CreatedAt
            };

            if (dto.Files != null)
            {
                _fileServices.UploadFilesToData(dto, domain);
            }

            _context.Kindergartens.Update(domain);
            await _context.SaveChangesAsync();

            return domain;
        }

        public async Task<Kindergarten> Delete(Guid id)
        {
            var result = await _context.Kindergartens
                .FirstOrDefaultAsync(x => x.Id == id);

            if (result != null)
            {
                var images = await _context.FileToDatas
                    .Where(x => x.KindergartenId == id)
                    .Select(y => new FileToDataDto
                    {
                        Id = y.Id,
                        ImageTitles = y.ImageTitles,
                        ExistingFilePath = y.ExistingFilePath,
                        KindergartenId = y.KindergartenId
                    }).ToArrayAsync();

                await _fileServices.RemoveImagesFromData(images);
                _context.Kindergartens.Remove(result);
                await _context.SaveChangesAsync();
            }

            return result;
        }
    }
}
