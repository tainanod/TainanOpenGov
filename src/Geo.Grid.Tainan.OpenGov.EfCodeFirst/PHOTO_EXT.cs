namespace Geo.Grid.Tainan.OpenGov.EfCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PHOTO_EXT
    {
        [Key]
        public Guid PHOTO_ID { get; set; }

        [Column(TypeName = "image")]
        [Required]
        public byte[] ORIGINAL { get; set; }

        [Column(TypeName = "image")]
        [Required]
        public byte[] WIDTH1024 { get; set; }

        [Column(TypeName = "image")]
        [Required]
        public byte[] WIDTH768 { get; set; }

        [Column(TypeName = "image")]
        [Required]
        public byte[] WIDTH320 { get; set; }

        [Column(TypeName = "image")]
        [Required]
        public byte[] WIDTH120 { get; set; }

        public virtual PHOTO PHOTO { get; set; }
    }
}
