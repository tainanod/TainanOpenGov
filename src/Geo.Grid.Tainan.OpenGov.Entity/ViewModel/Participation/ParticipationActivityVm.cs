using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Participation
{
    public class ParticipationActivityVm
    {
        ///<summary>
        /// 活動編號
        ///</summary>
        public Guid ActivityId { get; set; }

        ///<summary>
        /// 市政參與編號
        ///</summary>
        public Guid ParticipationId { get; set; }

        ///<summary>
        /// 主題
        ///</summary>
        public string Subject { get; set; }

        ///<summary>
        /// 描述
        ///</summary>
        public string Description { get; set; }

        ///<summary>
        /// 日期與時間
        ///</summary>
        public string HoldDate { get; set; }

        ///<summary>
        /// 地點
        ///</summary>
        public string Location { get; set; }

        ///<summary>
        /// 0: 論壇活動, 1: 工作坊
        ///</summary>
        public ActivityType ActivityType { get; set; }

        public List<IdName> Attachments { get; set; }

        public DateTime CreatedDate { get; set; }

        public List<ParticipationDocumentVm> ParticipationDocuments { get; set; }
        
        public ParticipationVm Participation { get; set; }
    }
}
