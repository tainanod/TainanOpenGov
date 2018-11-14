using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    [Table("ParticipationHyperlink")]
    public class ParticipationHyperlink : AuditableEntityNew
    {
        public ParticipationHyperlink()
        {
            HyperlinkId = Guid.NewGuid();
            IsEnabled = true;
            Participations = new List<Participation>();
        }

        ///<summary>
        /// 連結網址編號
        ///</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Key]
        [Display(Name = "Hyperlink ID")]
        public Guid HyperlinkId { get; set; }

        ///<summary>
        /// 連結名稱
        ///</summary>
        [Required]
        [MaxLength(200)]
        [Display(Name = "連結名稱")]
        public string Title { get; set; }

        ///<summary>
        /// 連結說明
        ///</summary>
        [MaxLength(500)]
        [Display(Name = "連結說明")]
        public string Description { get; set; }

        ///<summary>
        /// 連結網址
        ///</summary>
        [Required]
        [MaxLength(200)]
        //[Url]
        [Display(Name = "連結網址")]
        public string Url { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public ICollection<Participation> Participations { get; set; }

    }
}
