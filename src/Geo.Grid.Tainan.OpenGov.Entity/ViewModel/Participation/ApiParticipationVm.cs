using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Participation
{
    public class ApiParticipationVm
    {
        /// <summary>
        /// 論壇編號
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 主題
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 主辦單位
        /// </summary>
        public string UnitName { get; set; }
        /// <summary>
        /// 發佈時間
        /// </summary>
        public DateTime OpenDate { get; set; }
    }
}
