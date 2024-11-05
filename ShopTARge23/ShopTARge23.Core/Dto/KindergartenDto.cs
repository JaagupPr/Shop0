﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace ShopTARge23.Core.Dto
{
    public class KindergartenDto
    {
        public Guid? Id { get; set; }
        public string? GroupName { get; set; }
        public int? ChildrenCount { get; set; }
        public string? KindergartenName { get; set; }
        public string? Teacher { get; set; }

        public List<IFormFile> Files { get; set; }

        public IEnumerable<FileToDataDto> Image { get; set; }
            = new List<FileToDataDto>();

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }  
    }
}
