using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Service.Interface
{
    public interface IApiService
    {
        T GetDataFromWebApi<T>(string url, Dictionary<string, string> replaceStr = null);
    }
}
