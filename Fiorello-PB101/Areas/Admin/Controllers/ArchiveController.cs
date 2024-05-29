using Fiorello_PB101.Helpers;
using Fiorello_PB101.Services.Interfaces;
using Fiorello_PB101.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello_PB101.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArchiveController : Controller
    {
        private readonly ICategoryService _categoryService;
        private const int pageSize = 3;

        public ArchiveController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> CategoryArchive(int page = 1)
        {
            var categories = await _categoryService.GetAllArchiveAsync();
            var totalCategories = categories.Count();
            var totalPages = (int)Math.Ceiling(totalCategories / (double)pageSize);

            var paginatedCategories = categories
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var model = new Paginate<CategoryArchiveVM>(paginatedCategories, totalPages, page);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories(int page = 1)
        {
            var categories = await _categoryService.GetAllArchiveAsync();
            var totalCategories = categories.Count();
            var totalPages = (int)Math.Ceiling(totalCategories / (double)pageSize);

            var paginatedCategories = categories
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var model = new Paginate<CategoryArchiveVM>(paginatedCategories, totalPages, page);

            return Json(model);
        }
    }
}
