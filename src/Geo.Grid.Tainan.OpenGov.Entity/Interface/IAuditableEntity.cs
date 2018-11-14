using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geo.Grid.Tainan.OpenGov.Entity.Interface
{
    public interface IAuditableEntity
    {
        bool IsEnabled { get; set; }

        string CreatedBy { get; set; }

        DateTime CreatedDate { get; set; }

        string UpdatedBy { get; set; }

        DateTime UpdatedDate { get; set; }
    }
}