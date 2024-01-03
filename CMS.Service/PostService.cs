using CMS.Model;
using CMS.Repository.Interface;
using CMS.Utilities;
using CMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service
{
    public class PostService: IPostService
    {
        private IUnitOfWork _unitOfWork;
        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Delete(int PostId)
        {
            var model = _unitOfWork.GenericRepository<Post>().GetById(PostId);
            _unitOfWork.GenericRepository<Post>().Delete(model);
            _unitOfWork.Save();
        }

        public IEnumerable<PostViewModel> GetAll()
        {
            var modelList = _unitOfWork.GenericRepository<Post>().GetAll().ToList();
            var vmList = ConvertModelToViewModelList(modelList);
            return vmList;
        }

        public PostViewModel GetById(int PostId)
        {
            var model = _unitOfWork.GenericRepository<Post>().GetById(PostId);
            var vm = new PostViewModel(model);
            return vm;
        }

        public void Insert(PostViewModel postViewModel)
        {
            var model = new PostViewModel().ConvertViewModel(postViewModel);
            _unitOfWork.GenericRepository<Post>().Add(model);
            _unitOfWork.Save();
        }

        public void Update(PostViewModel postViewModel)
        {
            var post = new PostViewModel().ConvertViewModel(postViewModel);
            var postById = _unitOfWork.GenericRepository<Post>().GetById(post.Id);
            postById.Title = post.Title;
            postById.Description = post.Description;
            postById.CreatedDate = post.CreatedDate;
            postById.PublishedDate = post.PublishedDate;

            _unitOfWork.GenericRepository<Post>().Update(postById);
            _unitOfWork.Save();
        }
        public PagedResult<PostViewModel> GetAll(int pageNumber, int pageSize)
        {
            List<PostViewModel> vmList = new List<PostViewModel>();

            var modelList = _unitOfWork.GenericRepository<Post>().GetAll().ToList();
            vmList = ConvertModelToViewModelList(modelList);
         
            var result = new PagedResult<PostViewModel>(vmList, vmList.Count, pageNumber, pageSize);
            return result;
        }

        private List<PostViewModel> ConvertModelToViewModelList(List<Post> modelList)
        {
            return modelList.Select(x => new PostViewModel(x)).ToList();
        }
    }
}
