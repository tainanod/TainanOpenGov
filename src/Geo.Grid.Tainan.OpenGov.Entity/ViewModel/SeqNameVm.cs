using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    /// <summary>
    /// seq & name
    /// </summary>
    public class SeqNameVm
    {
        /// <summary>
        /// id
        /// </summary>
        public int PageSeq { get; set; }

        /// <summary>
        /// name
        /// </summary>
        public string PageName { get; set; } = string.Empty;
    }
}
