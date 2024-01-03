using CMS.Model;
using CMS.Repository;
using CMS.Service;
using CMS.Utilities;
using CMS.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Common;
using System.Text;

namespace CMSApplication.Controllers
{
    //[AuthorizationFilter]
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly IApplicationUserService _user;
        public PostController(IPostService postService, ICategoryService categoryService, IApplicationUserService user)
        {
            _postService = postService;
            _categoryService = categoryService;
            _user = user;
        }

        [HttpGet]
        public IActionResult Index(int pageNumber = 1, int pageSize = 2)
        {
            var data = _postService.GetAll().ToList();
            var posts = PagedResult<PostViewModel>.Create(data, pageNumber, pageSize);
            return View(posts);
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            ViewBag.category = new SelectList(_categoryService.GetAll(), "Id", "Title");
            var viewModel = _postService.GetById(Id);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(PostViewModel model)
        {
            model.CreatedDate = DateTime.Now;
            _postService.Update(model);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Detail(int Id)
        {
            ViewBag.category = new SelectList(_categoryService.GetAll(), "Id", "Title");
            var viewModel = _postService.GetById(Id);
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.category = new SelectList(_categoryService.GetAll(), "Id", "Title");
            var vm = new PostViewModel();
            vm.CreatedDate = DateTime.Now;

            return View(vm);
        }
        private ApplicationUser getUser()
        {
            string email = HttpContext.Session.GetString("Email");
            return _user.GetByEmail(email);

        }
        [HttpPost]
        public IActionResult Create(PostViewModel model)
        {
            model.ApplicationUserId = getUser().Id;
            _postService.Insert(model);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int Id)
        {
            _postService.Delete(Id);
            return RedirectToAction("Index");
        }
    }
}
