using System;
using System.Collections.Generic;
using System.Linq;
using Geo.Grid.Tainan.OpenGov.Dal.Interface;
using Geo.Grid.Tainan.OpenGov.Service;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Dal;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Service
{
    /// <summary>
    /// 鄉鎮市區資料Service
    /// </summary>
    public class CityTownService : ICityTownService
    {
        private readonly IRepository<CityTown> cityTown;
        private readonly OpenGovContext db;

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="cityTown"></param>
        public CityTownService(IRepository<CityTown> cityTown)
        {
            this.cityTown = cityTown;
            this.db = cityTown.Db;
        }

        public List<CityVm> GetCities()
        {
            return db.CityTown.Where(x => x.IsEnable).ToList().Distinct(new CityCompare()).Select(x => new CityVm
            {
                CitySeq = x.CitySeq,
                CityName = x.CityName
            }).OrderBy(x => x.CitySeq).ToList();
        }

        private class CityCompare : IEqualityComparer<CityTown>
        {
            public bool Equals(CityTown x, CityTown y)
            {
                return x.CitySeq == y.CitySeq;
            }

            public int GetHashCode(CityTown obj)
            {
                return obj.CitySeq;
            }
        }

        /// <summary>
        /// 由縣市序號取得鄉鎮資料
        /// </summary>
        /// <param name="citySeq"></param>
        /// <returns></returns>
        public List<TownVm> GetTowns(int? citySeq = null)
        {
            var query = cityTown.Where(x => x.IsEnable);

            if (citySeq.HasValue)
            {
                query = query.Where(x => x.CitySeq == citySeq);
            }

            return query.OrderBy(x => x.TownSeq).Select(x => new TownVm
            {
                CityTownId = x.CityTownId,
                TownName = x.TownName,
                TownSeq = x.TownSeq
            }).ToList();
        }

        #region 共用

        /// <summary>
        /// 縣市
        /// </summary>
        /// <returns></returns>
        public IQueryable<SeqNameVm> GetCity()
        {
            var data = db.CityTown.OrderBy(x => x.TownSeq).Where(x => x.IsEnable)
                .GroupBy(x => new { citySeq = x.CitySeq, cityName = x.CityName })
                .Select(x => new SeqNameVm()
                {
                    PageSeq = x.Key.citySeq,
                    PageName = x.Key.cityName
                });
            return data;
        }

        /// <summary>
        /// 縣市/鄉鎮區
        /// </summary>
        /// <param name="cityId">cityId</param>
        /// <returns></returns>
        public IQueryable<CityTownVm> GetCityTown(int cityId)
        {
            var data = db.CityTown.OrderBy(x => x.TownSeq).Where(x => x.IsEnable).Select(x => new CityTownVm()
            {
                CityId = x.CitySeq,
                CityName = x.CityName,
                TownId = x.TownSeq,
                TownName = x.TownName
            });

            if (cityId > 0)
            {
                data = data.Where(x => x.CityId == cityId);
            }
            return data;
        }

        #endregion
    }
}
