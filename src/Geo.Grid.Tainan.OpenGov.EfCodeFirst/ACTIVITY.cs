namespace Geo.Grid.Tainan.OpenGov.EfCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ACTIVITY")]
    public partial class ACTIVITY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ACTIVITY()
        {
            DOCUMENT = new HashSet<DOCUMENT>();
        }

        [Key]
        public Guid ACTIVITY_ID { get; set; }

        public Guid FORUM_ID { get; set; }

        [Required]
        [StringLength(300)]
        public string SUBJECT { get; set; }

        [Required]
        [StringLength(2000)]
        public string DESCRIPTION { get; set; }

        public DateTime HOLD_DATE { get; set; }

        [Required]
        [StringLength(200)]
        public string LOCATION { get; set; }

        public bool IS_ENABLED { get; set; }

        [Required]
        [StringLength(100)]
        public string CREATED_BY { get; set; }

        public DateTime CREATED_DATE { get; set; }

        [Required]
        [StringLength(100)]
        public string UPDATE_BY { get; set; }

        public DateTime UPDATE_DATE { get; set; }

        public int ACTIVITY_TYPE { get; set; }

        public virtual FORUM FORUM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DOCUMENT> DOCUMENT { get; set; }
    }
}
