using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.JsonViewModel;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;

namespace Geo.Grid.Tainan.OpenGov.Service.Interface
{
    public interface ITnodService
    {
        List<TnodDataset> GetTop5Dataset();

        List<TnodMayor> GetTop5Mayor();

        /// <summary>
        /// 行程公開
        /// </summary>
        /// <returns></returns>
        List<PublicScheduleVm> GetPublicSchedule();
    }
}