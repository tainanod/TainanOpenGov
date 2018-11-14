namespace Geo.Grid.Tainan.OpenGov.EfCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HYPERLINK")]
    public partial class HYPERLINK
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HYPERLINK()
        {
            FORUM = new HashSet<FORUM>();
        }

        [Key]
        public Guid HYPERLINK_ID { get; set; }

        [Required]
        [StringLength(200)]
        public string TITLE { get; set; }

        [StringLength(500)]
        public string DESCRIPTION { get; set; }

        [Required]
        [StringLength(200)]
        public string URL { get; set; }

        public bool IS_ENABLED { get; set; }

        [Required]
        [StringLength(100)]
        public string CREATED_BY { get; set; }

        public DateTime CREATED_DATE { get; set; }

        [Required]
        [StringLength(100)]
        public string UPDATE_BY { get; set; }

        public DateTime UPDATE_DATE { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FORUM> FORUM { get; set; }
    }
}
