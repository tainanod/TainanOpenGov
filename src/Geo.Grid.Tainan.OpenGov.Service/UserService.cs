using System;
using System.Data.Entity;
using System.Linq;
using Geo.Grid.Tainan.OpenGov.Dal.Interface;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<AspNetUsers> repository;
        private readonly IRepository<AspNetRoles> roleRepository;

        public UserService(IRepository<AspNetUsers> repository, IRepository<AspNetRoles> roleRepository)
        {
            this.repository = repository;
            this.roleRepository = roleRepository;
        }

        public IQueryable<AspNetUsers> QueryUser(SysRoles role, string keyword)
        {
            var query = repository.GetAll().Include(x => x.AspNetRoles).AsNoTracking();

            if (string.IsNullOrEmpty(keyword) == false)
            {
                query = query.Where(x => x.UserName.Contains(keyword) || x.Email.Contains(keyword));
            }

            switch (role)
            {
                case SysRoles.系統管理者:
                    return query.Where(x => x.AspNetRoles.Any(r => r.Name == "Admin"));

                case SysRoles.一般民眾:
                    return query.Where(x => !x.AspNetRoles.Any());

                case SysRoles.局處管理員:
                    return query.Where(x => x.AspNetRoles.Any(r => r.Name == "Office"));

                default:
                    return query;
            }
        }
        public Result<AspNetUsers> AddAdminRole(string id)
        {
            return AddAdminRole(id, "Admin");
        }
        public Result<AspNetUsers> AddAdminRole(string id, string roleName)
        {
            var result = new Result<AspNetUsers>(false);
            try
            {
                var user = repository.Get(x => x.Id == id);
                if (user == null)
                {
                    result.Message = "該使用者不存在!";
                    return result;
                }
                var admin = roleRepository.Get(x => x.Name == roleName);
                user.AspNetRoles.Add(admin);
                result.Success = repository.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<AspNetUsers> RemoveAdminRole(string id)
        {
            var result = new Result<AspNetUsers>(false);
            try
            {
                var user = repository.Get(x => x.Id == id);
                if (user == null)
                {
                    result.Message = "該使用者不存在!";
                    return result;
                }
                var roles = user.AspNetRoles.ToList();
                foreach (var role in roles)
                {
                    if (role.Name == "Admin")
                    {
                        user.AspNetRoles.Remove(role);
                    }

                    if (role.Name == "Office")
                    {
                        user.AspNetRoles.Remove(role);
                        user.DataSetUnitId = null;
                    }
                }

                result.Success = repository.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<AspNetUsers> UpdateUserNickNameImageUrl(string id, string nickName, string imageUrl)
        {
            var result = new Result<AspNetUsers>(false);
            try
            {
                var user = repository.Get(x => x.Id == id);
                if (user == null)
                {
                    result.Message = "該使用者不存在!";
                    return result;
                }

                if (!string.IsNullOrWhiteSpace(nickName))
                {
                    user.NickName = nickName;
                }

                if (!string.IsNullOrWhiteSpace(imageUrl))
                {
                    user.ImageUrl = imageUrl;
                }

                repository.Update(user);
                result.Success = repository.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public IQueryable<AspNetRoles> GetRoles()
        {
            return roleRepository.GetAll();
        }

        public AspNetUsers GetUser(string userId)
        {
            return repository.Get(x => x.Id == userId);
        }

        public AspNetUsers GetUserByName(string name)
        {
            return repository.Get(x => x.UserName == name);
        }

        public Result<AspNetUsers> UpdateUserDataSetUnitId(string userId, string unitId)
        {
            var result = new Result<AspNetUsers>(false);
            try
            {
                var user = repository.Get(x => x.Id == userId);
                if (user == null)
                {
                    result.Message = "該使用者不存在!";
                    return result;
                }
                user.DataSetUnitId = new Guid(unitId);

                result.Success = repository.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}