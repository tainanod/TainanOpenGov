using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;

namespace Geo.Grid.Tainan.OpenGov.Service.Interface
{
    public interface IRssService
    {
        List<RssEntity> GetTop5Rss();
    }
}