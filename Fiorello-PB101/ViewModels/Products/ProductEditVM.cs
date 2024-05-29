namespace Fiorello_PB101.ViewModels.Products
{
    public class ProductEditVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public List<IFormFile> Images { get; set; }

    }
}
