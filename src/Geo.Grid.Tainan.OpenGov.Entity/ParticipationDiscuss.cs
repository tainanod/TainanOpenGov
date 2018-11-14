using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    [Table("ParticipationDiscuss")]

    public class ParticipationDiscuss :AuditableEntityNew
    {
        ///<summary>
        /// 討論區編號
        ///</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Key]
        [Display(Name = "Discuss ID")]
        public Guid DiscussId { get; set; }

        ///<summary>
        /// 市政參與編號
        ///</summary>
        [Required]
        [Display(Name = "Participation ID")]
        public Guid ParticipationId { get; set; }

        ///<summary>
        /// 使用者編號
        ///</summary>
        [Required]
        [MaxLength(128)]
        [Display(Name = "User ID")]
        public string UserId { get; set; }

        ///<summary>
        /// 上層討論區編號
        ///</summary>
        [Display(Name = "Upper ID")]
        public Guid? UpperId { get; set; }

        ///<summary>
        /// 討論內容
        ///</summary>
        [Required]
        [MaxLength(4000)]
        [Display(Name = "Message")]
        public string Message { get; set; }

        ///<summary>
        /// 置頂
        ///</summary>
        [Required]
        [Display(Name = "Is top")]
        public bool IsTop { get; set; }
        
        public ICollection<AspNetUsers> AspNetUsers { get; set; }
        public ICollection<Participation> Participations { get; set; }
        public ICollection<ParticipationTag> ParticipationTags { get; set; }
        
        [ForeignKey("UserId")] public AspNetUsers AspNetUser { get; set; }

        public ParticipationDiscuss()
        {
            DiscussId = Guid.NewGuid();
            IsTop = false;
            IsEnabled = true;
            AspNetUsers = new List<AspNetUsers>();
            ParticipationTags = new List<ParticipationTag>();
            Participations = new List<Participation>();
        }
    }
}
