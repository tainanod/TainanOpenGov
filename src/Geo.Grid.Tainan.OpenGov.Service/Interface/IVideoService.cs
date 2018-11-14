using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api;

namespace Geo.Grid.Tainan.OpenGov.Service.Interface
{
    public interface IVideoService
    {
        IEnumerable<ApiYoutubeVm> GetList();
        Youtube GetVideo();

        IQueryable<Youtube> QueryVideo();

        Result<Youtube> Create(Youtube model);

        Youtube GetYoutube(Guid id);

        Result<Youtube> Update(Youtube model);

        Result<Youtube> Remove(Guid id);
    }
}