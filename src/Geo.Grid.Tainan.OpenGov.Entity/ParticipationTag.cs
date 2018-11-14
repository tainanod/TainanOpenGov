using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    [Table("ParticipationTag")]
    public class ParticipationTag : AuditableEntityNew
    {
        public ParticipationTag()
        {
            TagId = Guid.NewGuid();
            IsEnabled = true;
            ParticipationDiscusses = new List<ParticipationDiscuss>();
        }

        ///<summary>
        /// 標籤編號
        ///</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Key]
        [Display(Name = "Tag ID")]
        public Guid TagId { get; set; }

        ///<summary>
        /// 標籤名稱
        ///</summary>
        [Required]
        [MaxLength(50)]
        [Display(Name = "Tag name")]
        public string TagName { get; set; }

        ///<summary>
        /// 排序
        ///</summary>
        [Required]
        [Display(Name = "Sort")]
        public int Sort { get; set; }

        ///<summary>
        /// 市政參與編號
        ///</summary>
        [Required]
        [Display(Name = "Participation ID")]
        public Guid ParticipationId { get; set; }
        
        public ICollection<ParticipationDiscuss> ParticipationDiscusses { get; set; }
        
        [ForeignKey("ParticipationId")] public Participation Participation { get; set; }

    }
}
