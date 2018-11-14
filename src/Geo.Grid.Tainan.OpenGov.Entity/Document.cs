using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    [Table("DOCUMENT")]
    public partial class Document : AuditableEntity
    {
        public Document()
        {
            Activity = new HashSet<Activity>();
            Forum = new HashSet<Forum>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("DOCUMENT_ID")]
        public Guid DocumentId { get; set; }

        [Column("SIZE")]
        public long Size { get; set; }

        [Required]
        [StringLength(255)]
        [Column("FILE_NAME")]
        public string FileName { get; set; }

        [Required]
        [StringLength(255)]
        [Column("FILE_TYPE")]
        public string FileType { get; set; }

        [StringLength(255)]
        [Column("ALT")]
        public string Alt { get; set; }

        /// <summary>
        /// DOCUMENT_TYPE. 文件類別,0:一般文件,1:市府回應
        /// </summary>
        [Column("DOCUMENT_TYPE")]
        public DocType DocumentType { get; set; } 

        public virtual DocumentExt DocumentExt { get; set; }

        public virtual ICollection<Forum> Forum { get; set; }

        public virtual ICollection<Activity> Activity { get; set; }
    }
}