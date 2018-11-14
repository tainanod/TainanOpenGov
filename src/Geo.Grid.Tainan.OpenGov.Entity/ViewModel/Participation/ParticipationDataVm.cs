using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Participation
{
    public class ParticipationDataVm
    {
        /// <summary>
        /// key
        /// </summary>
        public Guid ParticipationId { get; set; }

        /// <summary>
        /// 主旨
        /// </summary>
        public string Subject { get; set; } = string.Empty;
    }
}
