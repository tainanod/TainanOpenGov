namespace Geo.Grid.Tainan.OpenGov.EfCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PHOTO")]
    public partial class PHOTO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHOTO()
        {
            FORUM = new HashSet<FORUM>();
        }

        [Key]
        public Guid PHOTO_ID { get; set; }

        public long SIZE { get; set; }

        [Required]
        [StringLength(255)]
        public string FILE_NAME { get; set; }

        [Required]
        [StringLength(255)]
        public string FILE_TYPE { get; set; }

        [StringLength(255)]
        public string ALT { get; set; }

        public bool IS_ENABLED { get; set; }

        [Required]
        [StringLength(100)]
        public string CREATED_BY { get; set; }

        public DateTime CREATED_DATE { get; set; }

        [Required]
        [StringLength(100)]
        public string UPDATE_BY { get; set; }

        public DateTime UPDATE_DATE { get; set; }

        public virtual PHOTO_EXT PHOTO_EXT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FORUM> FORUM { get; set; }
    }
}
