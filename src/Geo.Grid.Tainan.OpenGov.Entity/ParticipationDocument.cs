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
    [Table("ParticipationDocument")]
    public class ParticipationDocument : AuditableEntityNew
    {
        public ParticipationDocument()
        {
            DocumentId = Guid.NewGuid();
            DocumentType = 0;
            IsEnabled = true;
            ParticipationActivities = new List<ParticipationActivity>();
            Participations = new List<Participation>();
        }

        ///<summary>
        /// 附件編號
        ///</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Key]
        [Display(Name = "Document ID")]
        public Guid DocumentId { get; set; }

        ///<summary>
        /// 附件檔案大小
        ///</summary>
        [Required]
        [Display(Name = "Size")]
        public long Size { get; set; }

        ///<summary>
        /// 附件檔名
        ///</summary>
        [Required]
        [MaxLength(255)]
        [Display(Name = "File name")]
        public string FileName { get; set; }

        ///<summary>
        /// 附件類型
        ///</summary>
        [Required]
        [MaxLength(255)]
        [Display(Name = "File type")]
        public string FileType { get; set; }

        ///<summary>
        /// 附件內容說明
        ///</summary>
        [MaxLength(255)]
        [Display(Name = "Alt")]
        public string Alt { get; set; }

        ///<summary>
        /// 附件類別, 0:一般文件, 1:市府回應
        ///</summary>
        [Required]
        [Display(Name = "Document type")]
        public DocType DocumentType { get; set; }

        public ParticipationDocumentExt ParticipationDocumentExt { get; set; }
        public ICollection<Participation> Participations { get; set; }
        public ICollection<ParticipationActivity> ParticipationActivities { get; set; }
    }
}
