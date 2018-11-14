using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    [Table("ParticipationDocumentExt")]
    public class ParticipationDocumentExt
    {
        ///<summary>
        /// 附件編號
        ///</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Key]
        [Display(Name = "Document ID")]
        [ForeignKey("ParticipationDocument")]
        public Guid DocumentId { get; set; }

        ///<summary>
        /// 檔案內容
        ///</summary>
        [Required]
        [Display(Name = "Original")]
        public byte[] Original { get; set; }
        
        [ForeignKey("DocumentId")] public ParticipationDocument ParticipationDocument { get; set; }
    }
}
