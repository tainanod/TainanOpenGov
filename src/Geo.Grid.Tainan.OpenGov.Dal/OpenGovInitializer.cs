using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace Geo.Grid.Tainan.OpenGov.Dal
{
    //public class OpenGovInitializer : DropCreateDatabaseAlways<OpenGovContext>
    public class OpenGovInitializer : CreateDatabaseIfNotExists<OpenGovContext>
    {
    }
}