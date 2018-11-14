using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    [Table("PHOTO")]
    public partial class Photo : AuditableEntity
    {
        public Photo()
        {
            PhotoId = Guid.NewGuid();
            Forum = new HashSet<Forum>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("PHOTO_ID")]
        public Guid PhotoId { get; set; }

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

        public virtual PhotoExt PhotoExt { get; set; }

        public virtual ICollection<Forum> Forum { get; set; }

        /// <summary>
        /// 野生台南-應用展示
        /// </summary>
        public virtual ICollection<ShowCase> ShowCaseRel { get; set; }

        /// <summary>
        /// 市政參與
        /// </summary>
        public virtual ICollection<Participation> Participations { get; set; }

    }
}