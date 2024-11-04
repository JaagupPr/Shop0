namespace ShopTARge23.Models.Kindergarten
{
    public class KindergartenImageViewModel
    {
        public Guid ImageId { get; set; }
        public string FilePath { get; set; }
        public string ExistingFilePath { get; set; }
        public string? ImageTitles { get; set; }
        public string? Image { get; set; }
        public Guid? KindergartenId { get; set; }
    }
}
