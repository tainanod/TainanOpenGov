namespace Geo.Grid.Tainan.OpenGov.EfCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TAG")]
    public partial class TAG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TAG()
        {
            DISCUSS = new HashSet<DISCUSS>();
        }

        [Key]
        public Guid TAG_ID { get; set; }

        [Required]
        [StringLength(50)]
        public string TAG_NAME { get; set; }

        public DateTime UPDATE_DATE { get; set; }

        public bool IS_ENABLED { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DISCUSS> DISCUSS { get; set; }
    }
}
