using CMS.Model;
using CMS.Utilities;
using CMS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Service
{
    public interface IApplicationUserService
    {
        ApplicationUser GetById(int UserId);
        ApplicationUser GetByEmail(string email);
        void Update(ApplicationUser applicationUser);
        void Delete(int CategoryId);
        void Insert(ApplicationUser applicationUser);
        IEnumerable<ApplicationUser> GetAll();

    }
}
