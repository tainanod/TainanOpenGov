using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    [Table("PHOTO_EXT")]
    public partial class PhotoExt
    {
        [Key]
        [Column("PHOTO_ID")]
        public Guid PhotoId { get; set; }

        [Column("ORIGINAL", TypeName = "image")]
        [Required]
        public byte[] Original { get; set; }

        [Column("WIDTH1024", TypeName = "image")]
        [Required]
        public byte[] Width1024 { get; set; }

        [Column("WIDTH768", TypeName = "image")]
        [Required]
        public byte[] Width768 { get; set; }

        [Column("WIDTH320", TypeName = "image")]
        [Required]
        public byte[] Width320 { get; set; }

        [Column("WIDTH120", TypeName = "image")]
        [Required]
        public byte[] Width120 { get; set; }

        public virtual Photo Photo { get; set; }
    }
}