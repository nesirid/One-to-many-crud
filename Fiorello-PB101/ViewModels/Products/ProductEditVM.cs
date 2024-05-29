namespace Fiorello_PB101.ViewModels.Products
{
    public class ProductEditVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public List<IFormFile> NewImages { get; set; } 
        public List<int> ImagesToDelete { get; set; } 
        public List<ImageVM> ExistingImages { get; set; }
        public int? MainImageId { get; set; }
    }

    public class ImageVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMain { get; set; }
    }
}
