using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.ShowCase
{
    /// <summary>
    /// page & name
    /// </summary>
    public class PageNameVm
    {
        /// <summary>
        /// guid
        /// </summary>
        public Guid PageGuid { get; set; }

        /// <summary>
        /// name
        /// </summary>
        public string PageName { get; set; } = string.Empty;

        /// <summary>
        /// url
        /// </summary>
        public string PageUrl { get; set; } = string.Empty;
    }
}
