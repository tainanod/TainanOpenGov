using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    [Table("Participation")]
    public class Participation : AuditableEntityNew
    {

        public Participation()
        {
            ParticipationId = Guid.NewGuid();
            EnableDiscuss = false;
            IsMothball = false;
            IsEnabled = true;
            ParticipationActivities = new List<ParticipationActivity>();
            ParticipationTags = new List<ParticipationTag>();
            ParticipationDiscusses = new List<ParticipationDiscuss>();
            ParticipationDocuments = new List<ParticipationDocument>();
            ParticipationHyperlinks = new List<ParticipationHyperlink>();
            Photos = new List<Photo>();
            ParticipationAnonymousClicks = new List<ParticipationAnonymousClick>();
        }

        ///<summary>
        /// 市政參與編號
        ///</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Key]
        [Display(Name = "Participation ID")]
        public Guid ParticipationId { get; set; }

        ///<summary>
        /// 主題類別
        ///</summary>
        [Required]
        [MaxLength(100)]
        [Display(Name = "主題類別")]
        [UIHint("String-W-320")]
        public string Category { get; set; }

        ///<summary>
        /// 主辦單位編號
        ///</summary>
        [Required]
        [UIHint("SelectList")]
        [Display(Name = "主辦單位")]
        public Guid DataSetUnitId { get; set; }

        ///<summary>
        /// 主題
        ///</summary>
        [Required]
        [MaxLength(300)]
        [Display(Name = "主題")]
        public string Subject { get; set; }

        ///<summary>
        /// 描述
        ///</summary>
        [Required]
        [MaxLength(4000)]
        [UIHint("Info")]
        [Display(Name = "描述")]
        public string Description { get; set; }

        ///<summary>
        /// 討論區公告
        ///</summary>
        [MaxLength(4000)]
        [StringLength(4000)]
        [UIHint("Info-5-300")]
        [Display(Name = "討論區公告")]
        public string Announcement { get; set; }

        ///<summary>
        /// 是否開放討論區
        ///</summary>
        [Required]
        public bool EnableDiscuss { get; set; }

        ///<summary>
        /// 是否開放討論區
        ///</summary>
        [Display(Name = "是否開放討論區")]
        [UIHint("RadioButtonList")]
        [NotMapped]
        public string EnableDiscussStr {
            get
            {
                return EnableDiscuss ? "true" : "false";
            }
            set
            {
                EnableDiscuss = Boolean.Parse(value);
            }
        }

        ///<summary>
        /// 發佈時間
        ///</summary>
        [Required]
        [Display(Name = "發佈時間")]
        [UIHint("DateTimePicker-W-200")]
        public DateTime OpenDate { get; set; }

        ///<summary>
        /// 截止時間
        ///</summary>
        [Required]
        [Display(Name = "截止時間")]
        [UIHint("DateTimePicker-W-200")]
        public DateTime CloseDate { get; set; }

        ///<summary>
        /// 是否封存
        ///</summary>
        [Required]
        [Display(Name = "是否封存")]
        public bool IsMothball { get; set; }
        
        public ICollection<ParticipationActivity> ParticipationActivities { get; set; }
        public ICollection<ParticipationDiscuss> ParticipationDiscusses { get; set; }
        public ICollection<ParticipationDocument> ParticipationDocuments { get; set; }
        public ICollection<ParticipationHyperlink> ParticipationHyperlinks { get; set; }
        public ICollection<ParticipationTag> ParticipationTags { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<ParticipationAnonymousClick> ParticipationAnonymousClicks { get; set; }
        
    }
}
