namespace Geo.Grid.Tainan.OpenGov.EfCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("YOUTUBE")]
    public partial class YOUTUBE
    {
        [Key]
        public Guid YOUTUBE_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string NAME { get; set; }

        [StringLength(4000)]
        public string URL { get; set; }

        public DateTime START_DATE { get; set; }

        [StringLength(50)]
        public string VIDEO_TIME { get; set; }

        public bool IS_OPENED { get; set; }

        [StringLength(4000)]
        public string DESCRIPTION { get; set; }

        public DateTime END_DATE { get; set; }

        public bool IS_ENABLED { get; set; }

        [Required]
        [StringLength(100)]
        public string CREATED_BY { get; set; }

        public DateTime CREATED_DATE { get; set; }

        [Required]
        [StringLength(100)]
        public string UPDATE_BY { get; set; }

        public DateTime UPDATE_DATE { get; set; }
    }
}
