using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    [Table("ParticipationActivity")]
    public class ParticipationActivity : AuditableEntityNew
    {
        public ParticipationActivity()
        {
            ActivityId = Guid.NewGuid();
            ActivityType = 0;
            IsEnabled = true;
            ParticipationDocuments = new List<ParticipationDocument>();
        }
        
        ///<summary>
        /// 活動編號
        ///</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Key]
        [Display(Name = "活動編號")]
        public System.Guid ActivityId { get; set; }

        ///<summary>
        /// 市政參與編號
        ///</summary>
        [Required]
        [Display(Name = "市政參與編號")]
        public System.Guid ParticipationId { get; set; }

        ///<summary>
        /// 主題
        ///</summary>
        [Required]
        [MaxLength(300)]
        [Display(Name = "名稱")]
        public string Subject { get; set; }

        ///<summary>
        /// 描述
        ///</summary>
        [Required]
        [MaxLength(2000)]
        [Display(Name = "說明")]
        [UIHint("Info")]
        public string Description { get; set; }

        ///<summary>
        /// 日期與時間
        ///</summary>
        [Required]
        [Display(Name = "日期與時間")]
        public string HoldDate { get; set; }

        ///<summary>
        /// 地點
        ///</summary>
        [Required]
        [MaxLength(200)]
        [Display(Name = "地點")]
        public string Location { get; set; }

        ///<summary>
        /// 0: 論壇活動, 1: 工作坊
        ///</summary>
        [Required]
        [Display(Name = "Activity type")]
        public ActivityType ActivityType { get; set; }
        
        public ICollection<ParticipationDocument> ParticipationDocuments { get; set; }


        [ForeignKey("ParticipationId")] public Participation Participation { get; set; }
    }
}
