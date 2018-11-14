using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geo.Grid.Tainan.OpenGov.Entity.Interface
{
    public interface IResult
    {
        Exception Exception { get; set; }

        Guid ID { get; set; }

        string Message { get; set; }

        bool Success { get; set; }
    }
}