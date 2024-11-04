using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTARge23.Core.Dto
{
    public class FileToDataDto
    {
        public Guid Id { get; set; }
        public string ExistingFilePath { get; set; }
        public string? ImageTitles { get; set; }
        public Guid? KindergartenId { get; set; }
    }
}
