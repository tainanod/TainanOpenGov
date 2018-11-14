using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Admin
{
    /// <summary>
    /// 公民論壇
    /// </summary>
    public class ForumVm
    {
        /// <summary>
        /// key
        /// </summary>
        public Guid ForumId { get; set; }

        /// <summary>
        /// 主旨
        /// </summary>
        public string Subject { get; set; } = string.Empty;
    }
}
