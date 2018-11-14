using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Participation
{
    /// <summary>
    /// 台南開放政府Restful Api
    /// 取得討論區標籤設定之清單內容
    /// </summary>
    public class ApiParticipationDiscussVm
    {
        /// <summary>
        /// 討論編號
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 論壇編號
        /// </summary>
        public Guid ParticipationId { get; set; }
        /// <summary>
        /// 論壇主題
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 標籤內容
        /// </summary>
        public IEnumerable<string> Tags { get; set; }
    }
}
