using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    [Table("DOCUMENT_EXT")]
    public partial class DocumentExt
    {
        [Key]
        [Column("DOCUMENT_ID")]
        public Guid DocumentId { get; set; }

        [Required]
        [Column("ORIGINAL")]
        public byte[] Original { get; set; }

        public virtual Document Document { get; set; }
    }
}