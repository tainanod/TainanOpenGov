using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Participation
{
    public class ParticipationTagVm
    {
        ///<summary>
        /// 標籤編號
        ///</summary>
        public Guid TagId { get; set; }

        ///<summary>
        /// 標籤名稱
        ///</summary>
        public string TagName { get; set; }

        ///<summary>
        /// 排序
        ///</summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否顯示
        /// </summary>
        [DisplayName("是否顯示")]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 建立時間
        /// </summary>
        [DisplayName("建立時間")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        
        ///<summary>
        /// 市政參與編號
        ///</summary>
        public Guid ParticipationId { get; set; }

        /// <summary>
        /// 論壇主題
        /// </summary>
        public string ForumTitle { get; set; } = string.Empty;
        
        public List<ParticipationDiscussVm> ParticipationDiscusses { get; set; }
        
        public ParticipationVm Participation { get; set; }

    }
}
