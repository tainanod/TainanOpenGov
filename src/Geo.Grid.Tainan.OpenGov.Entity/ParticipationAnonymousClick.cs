using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    [Table("ParticipationAnonymousClick")]
    public class ParticipationAnonymousClick
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [MaxLength(128)]
        [Key, Column(Order = 1)]
        public string AnonymousId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [MaxLength(128)]
        [Key, Column(Order = 2)]
        public string ClickId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [MaxLength(128)]
        [Key, Column(Order = 3)]
        public string ClickType { get; set; }

        ///<summary>
        /// 市政參與編號
        ///</summary>
        [Required]
        public Guid ParticipationId { get; set; }
        
        [ForeignKey("ParticipationId")] public Participation Participation { get; set; }
    }
}
