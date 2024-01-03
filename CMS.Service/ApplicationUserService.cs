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
    public class ApplicationUserService : IApplicationUserService
    {
        private IUnitOfWork _unitOfWork;
        public ApplicationUserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void Delete(int CategoryId)
        {
           var model=_unitOfWork.GenericRepository<ApplicationUser>().GetById(CategoryId);
            _unitOfWork.GenericRepository<ApplicationUser>().Delete(model);
            _unitOfWork.Save();
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _unitOfWork.GenericRepository<ApplicationUser>().GetAll().ToList();
        }

        public ApplicationUser GetByEmail(string email)
        {
            var users = _unitOfWork.GenericRepository<ApplicationUser>().GetAll().ToList();
            foreach(var user in users)
            {
                if (user.Email == email) return user;
            }
                return null; 
        }

        public ApplicationUser GetById(int userId)
        {
            return _unitOfWork.GenericRepository<ApplicationUser>().GetById(userId);
        }

        public void Insert(ApplicationUser applicationUser)
        {
            _unitOfWork.GenericRepository<ApplicationUser>().Add(applicationUser);
            _unitOfWork.Save();
        }

        public void Update(ApplicationUser applicationUser)
        {
            var userById = _unitOfWork.GenericRepository<ApplicationUser>().GetById(applicationUser.Id);
            userById.UserName = applicationUser.UserName;
            
            _unitOfWork.GenericRepository<ApplicationUser>().Update(userById);
            _unitOfWork.Save();
        }
       
    }
}
