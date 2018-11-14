namespace Geo.Grid.Tainan.OpenGov.EfCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DISCUSS")]
    public partial class DISCUSS
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DISCUSS()
        {
            TAG = new HashSet<TAG>();
            AspNetUsers1 = new HashSet<AspNetUsers>();
        }

        [Key]
        public Guid DISCUSS_ID { get; set; }

        public Guid FORUM_ID { get; set; }

        [Required]
        [StringLength(128)]
        public string USER_ID { get; set; }

        public Guid? UPPER_ID { get; set; }

        [Required]
        [StringLength(4000)]
        public string MESSAGE { get; set; }

        public bool IS_ENABLED { get; set; }

        [Required]
        [StringLength(100)]
        public string CREATED_BY { get; set; }

        public DateTime CREATED_DATE { get; set; }

        [Required]
        [StringLength(100)]
        public string UPDATE_BY { get; set; }

        public DateTime UPDATE_DATE { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual FORUM FORUM { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TAG> TAG { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUsers> AspNetUsers1 { get; set; }
    }
}
