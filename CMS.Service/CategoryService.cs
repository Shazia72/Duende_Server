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
    public class CategoryService : ICategoryService
    {
        private IUnitOfWork _unitOfWork;
        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Delete(int CategoryId)
        {
           var model=_unitOfWork.GenericRepository<Category>().GetById(CategoryId);
            _unitOfWork.GenericRepository<Category>().Delete(model);
            _unitOfWork.Save();
        }

        public IEnumerable<CategoryViewModel> GetAll()
        {
            var modelList = _unitOfWork.GenericRepository<Category>().GetAll().ToList();
            var vmList = ConvertModelToViewModelList(modelList);
            return vmList;
        }

        public CategoryViewModel GetById(int CategoryId)
        {
            var model = _unitOfWork.GenericRepository<Category>().GetById(CategoryId);
            var vm = new CategoryViewModel(model);
            return vm;
        }

        public void Insert(CategoryViewModel categoryViewModel)
        {
            var model = new CategoryViewModel().ConvertViewModel(categoryViewModel);
            _unitOfWork.GenericRepository<Category>().Add(model);
            _unitOfWork.Save();
        }

        public void Update(CategoryViewModel categoryViewModel)
        {
            var category = new CategoryViewModel().ConvertViewModel(categoryViewModel);
            var categoryById = _unitOfWork.GenericRepository<Category>().GetById(category.Id);
            categoryById.Title = category.Title;
            categoryById.CreatedDate = category.CreatedDate;

            _unitOfWork.GenericRepository<Category>().Update(categoryById);
            _unitOfWork.Save();
        }
        public PagedResult<CategoryViewModel> GetAll(int pageNumber, int pageSize)
        {
            List<CategoryViewModel> vmList = new List<CategoryViewModel>();

            var modelList = _unitOfWork.GenericRepository<Category>().GetAll().ToList();
            vmList = ConvertModelToViewModelList(modelList);

            var result = new PagedResult<CategoryViewModel>( vmList, vmList.Count, pageNumber,pageSize);
            return result;
        }

        private List<CategoryViewModel> ConvertModelToViewModelList(List<Category> modelList)
        {
            return modelList.Select(x => new CategoryViewModel(x)).ToList();
        }
    }
}
