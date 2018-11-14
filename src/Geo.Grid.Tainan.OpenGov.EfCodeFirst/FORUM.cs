namespace Geo.Grid.Tainan.OpenGov.EfCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FORUM")]
    public partial class FORUM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FORUM()
        {
            ACTIVITY = new HashSet<ACTIVITY>();
            DISCUSS = new HashSet<DISCUSS>();
            DOCUMENT = new HashSet<DOCUMENT>();
            HYPERLINK = new HashSet<HYPERLINK>();
            PHOTO = new HashSet<PHOTO>();
        }

        [Key]
        public Guid FORUM_ID { get; set; }

        [Required]
        [StringLength(100)]
        public string CATEGORY { get; set; }

        [Required]
        [StringLength(300)]
        public string SUBJECT { get; set; }

        [Required]
        [StringLength(100)]
        public string HOLDER { get; set; }

        [Required]
        [StringLength(4000)]
        public string DESCRIPTION { get; set; }

        public DateTime OPEN_DATE { get; set; }

        public DateTime CLOSE_DATE { get; set; }

        public bool IS_ENABLED { get; set; }

        [Required]
        [StringLength(100)]
        public string CREATED_BY { get; set; }

        public DateTime CREATED_DATE { get; set; }

        [Required]
        [StringLength(100)]
        public string UPDATE_BY { get; set; }

        public DateTime UPDATE_DATE { get; set; }

        public int FORUM_TYPE { get; set; }

        public Guid? UPPER_ID { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACTIVITY> ACTIVITY { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DISCUSS> DISCUSS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DOCUMENT> DOCUMENT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HYPERLINK> HYPERLINK { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHOTO> PHOTO { get; set; }
    }
}
