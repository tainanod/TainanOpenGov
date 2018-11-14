using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    [Table("HYPERLINK")]
    public partial class Hyperlink : AuditableEntity
    {
        public Hyperlink()
        {
            Forum = new HashSet<Forum>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("HYPERLINK_ID")]
        public Guid HyperlinkId { get; set; }

        [Required]
        [StringLength(200)]
        [Column("TITLE"), DisplayName("連結名稱")]
        public string Title { get; set; }

        [StringLength(500)]
        [Column("DESCRIPTION"), DisplayName("連結說明")]
        public string Description { get; set; }

        [Required]
        [StringLength(200)]
        [Column("URL"), DisplayName("連結位址")]
        public string Url { get; set; }

        public virtual ICollection<Forum> Forum { get; set; }
    }
}