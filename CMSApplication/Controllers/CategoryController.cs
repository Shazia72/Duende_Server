using CMS.Service;
using CMS.Utilities;
using CMS.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CMSApplication.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        public IActionResult Index(int pageNumber=1, int pageSize=3)
        {
            var data = _service.GetAll().ToList();
            var category = PagedResult<CategoryViewModel>.Create(data, pageNumber, pageSize);
            return View(category);
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var viewModel = _service.GetById(Id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(CategoryViewModel model)
        {
            _service.Update(model);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Detail(int Id)
        {
            var viewModel = _service.GetById(Id);
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CategoryViewModel model)
        {
             _service.Insert(model);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int Id)
        {
             _service.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}
