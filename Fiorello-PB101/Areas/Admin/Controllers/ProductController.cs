using Fiorello_PB101.Helpers;
using Fiorello_PB101.Services.Interfaces;
using Fiorello_PB101.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello_PB101.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var products = await _productService.GetAllPaginateAsync(page, 4);
            var mappedDatas = _productService.GetMappedDatas(products);
            int totalPage = await GetPageCountAsync(4);

            Paginate<ProductVM> paginateDatas = new(mappedDatas, totalPage, page);
            return View(paginateDatas);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts(int page = 1)
        {
            var products = await _productService.GetAllPaginateAsync(page, 4);
            var mappedDatas = _productService.GetMappedDatas(products);
            int totalPage = await GetPageCountAsync(4);

            foreach (var product in mappedDatas)
            {
                product.MainImage = Url.Content($"~/img/{product.MainImage}");
            }

            Paginate<ProductVM> paginateDatas = new(mappedDatas, totalPage, page);
            return Json(paginateDatas);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int productCount = await _productService.GetCountAsync();
            return (int)Math.Ceiling((decimal)productCount / take);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();

            var existProduct = await _productService.GetByIdWithAllDatasAsync((int)id);
            
            if (existProduct == null) return NotFound();

            List<ProductImageVM> images = new();

            foreach (var item in existProduct.ProductImages)
            {
                images.Add(new ProductImageVM
                {
                    Image = item.Name,
                    IsMain = item.IsMain
                });
            }
            ProductDetailVM response = new()
            {
                Name = existProduct.Name,
                Description = existProduct.Description,
                Category = existProduct.Category.Name,
                Price = existProduct.Price,
                Images = images

            };

            return View(response);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        { 
         return View();
        }

    }
}
