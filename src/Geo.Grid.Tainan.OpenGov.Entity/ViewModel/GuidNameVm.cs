using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    /// <summary>
    /// guid & name
    /// </summary>
    public class GuidNameVm
    {
        /// <summary>
        /// key
        /// </summary>
        public Guid PageGuid { get; set; }

        /// <summary>
        /// name
        /// </summary>
        public string PageName { get; set; } = string.Empty;        
    }
}
