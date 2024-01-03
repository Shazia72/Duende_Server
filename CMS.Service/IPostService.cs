using CMS.Utilities;
using CMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service
{
    public interface IPostService
    {
        PagedResult<PostViewModel> GetAll(int PageNumber, int PageSize);
        PostViewModel GetById(int PostId);
        void Update(PostViewModel postViewModel);
        void Delete(int PostId);
        void Insert(PostViewModel postViewModel);
        IEnumerable<PostViewModel> GetAll();
    }
}
