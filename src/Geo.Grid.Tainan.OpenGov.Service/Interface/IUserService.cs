using System.Linq;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;

namespace Geo.Grid.Tainan.OpenGov.Service.Interface
{
    public interface IUserService
    {
        IQueryable<AspNetUsers> QueryUser(SysRoles role, string keyword);

        Result<AspNetUsers> AddAdminRole(string id);
        Result<AspNetUsers> AddAdminRole(string id,string roleName);

        Result<AspNetUsers> RemoveAdminRole(string id);

        Result<AspNetUsers> UpdateUserDataSetUnitId(string userId, string unitId);

        Result<AspNetUsers> UpdateUserNickNameImageUrl(string id, string nickName, string imageUrl);

        AspNetUsers GetUser(string userId);

        AspNetUsers GetUserByName(string name);

        IQueryable<AspNetRoles> GetRoles();
    }
}