using System.Collections.Generic;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using System.Linq;

namespace Geo.Grid.Tainan.OpenGov.Service.Interface
{
    public interface ICityTownService
    {
        List<TownVm> GetTowns(int? citySeq = null);

        List<CityVm> GetCities();

        #region 共用

        /// <summary>
        /// 縣市
        /// </summary>
        /// <returns></returns>
        IQueryable<SeqNameVm> GetCity();

        /// <summary>
        /// 縣市/鄉鎮區
        /// </summary>
        /// <param name="cityId">cityId</param>
        /// <returns></returns>
        IQueryable<CityTownVm> GetCityTown(int cityId);

        #endregion
    }
}