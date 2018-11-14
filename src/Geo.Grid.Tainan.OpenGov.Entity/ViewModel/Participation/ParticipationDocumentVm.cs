using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Participation
{
    public class ParticipationDocumentVm
    {
        ///<summary>
        /// 附件編號
        ///</summary>
        public Guid DocumentId { get; set; }

        ///<summary>
        /// 附件檔案大小
        ///</summary>
        public long Size { get; set; }

        ///<summary>
        /// 附件檔名
        ///</summary>
        public string FileName { get; set; }

        ///<summary>
        /// 附件類型
        ///</summary>
        public string FileType { get; set; }

        ///<summary>
        /// 附件內容說明
        ///</summary>
        public string Alt { get; set; }

        ///<summary>
        /// 附件類別, 0:一般文件, 1:市府回應
        ///</summary>
        public int DocumentType { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        /// <summary>
        /// 是否已點擊
        /// </summary>
        public bool IsClick { get; set; } = false;

        public List<ParticipationVm> Participations { get; set; }
        public List<ParticipationActivityVm> ParticipationActivities { get; set; }
    }
}
