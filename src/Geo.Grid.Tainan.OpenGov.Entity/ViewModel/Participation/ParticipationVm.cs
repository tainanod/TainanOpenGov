using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Participation
{
    public class ParticipationVm
    {
        ///<summary>
        /// 市政參與編號
        ///</summary>
        public Guid ParticipationId { get; set; }

        ///<summary>
        /// 主題類別
        ///</summary>
        public string Category { get; set; }

        ///<summary>
        /// 主辦單位編號
        ///</summary>
        public Guid DataSetUnitId { get; set; }

        ///<summary>
        /// 主辦單位 名稱
        ///</summary>
        public string DataSetUnitName { get; set; }

        ///<summary>
        /// 主題
        ///</summary>
        public string Subject { get; set; }

        ///<summary>
        /// 描述
        ///</summary>
        public string Description { get; set; }

        ///<summary>
        /// 討論區公告
        ///</summary>
        public string Announcement { get; set; }

        ///<summary>
        /// 是否開放討論區
        ///</summary>
        public bool EnableDiscuss { get; set; }

        ///<summary>
        /// 發佈時間
        ///</summary>
        public DateTime OpenDate { get; set; }

        ///<summary>
        /// 截止時間
        ///</summary>
        public DateTime CloseDate { get; set; }

        ///<summary>
        /// 是否封存
        ///</summary>
        public bool IsMothball { get; set; }
        
        public List<ParticipationActivityVm> ParticipationActivities { get; set; }
        public List<ParticipationDiscussVm> ParticipationDiscusses { get; set; }
        public List<ParticipationDocumentVm> ParticipationDocuments { get; set; }
        public List<ParticipationHyperlinkVm> ParticipationHyperlinks { get; set; }
        public List<ParticipationTagVm> ParticipationTags { get; set; }
        public List<PhotoVm> Photos { get; set; }

        public List<ParticipationActivityVm> WorkActivity { get; set; }

        public List<ParticipationDocumentVm> NormalDocuments { get; set; }

        public List<ParticipationDocumentVm> GovReplyDocuments { get; set; }

        public List<ParticipationHyperlinkVm> HyperLinks { get; set; }

    }
}
