using System.Collections.Generic;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Admin;

namespace Geo.Grid.Tainan.OpenGov.Service.Interface.Admin
{
    public interface ISharedService
    {
        List<MenuVm> GetSetMenu();
        List<MenuVm> GetMenu(string userId);
        Result<MenuVm> AddToOffice(string id);
        Result<MenuVm> RemoveFromOffice(string id);
    }
}