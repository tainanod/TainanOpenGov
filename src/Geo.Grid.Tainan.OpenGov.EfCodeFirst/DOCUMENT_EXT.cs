namespace Geo.Grid.Tainan.OpenGov.EfCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DOCUMENT_EXT
    {
        [Key]
        public Guid DOCUMENT_ID { get; set; }

        [Required]
        public byte[] ORIGINAL { get; set; }

        public virtual DOCUMENT DOCUMENT { get; set; }
    }
}
