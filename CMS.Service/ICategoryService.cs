using CMS.Utilities;
using CMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service
{
    public interface ICategoryService
    {
        PagedResult<CategoryViewModel> GetAll(int PageNumber, int PageSize);
        CategoryViewModel GetById(int CategoryId);
        void Update(CategoryViewModel categoryViewModel);
        void Delete(int CategoryId);
        void Insert(CategoryViewModel categoryViewModel);
        IEnumerable<CategoryViewModel> GetAll();

    }
}
