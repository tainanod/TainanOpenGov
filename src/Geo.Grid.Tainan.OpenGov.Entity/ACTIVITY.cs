using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    [Table("ACTIVITY")]
    public partial class Activity : AuditableEntity
    {
        public Activity()
        {
            Document = new HashSet<Document>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ACTIVITY_ID")]
        public Guid ActivityId { get; set; }

        [Column("FORUM_ID")]
        public Guid ForumId { get; set; }

        [Required]
        [StringLength(300)]
        [Column("SUBJECT"), DisplayName("名稱")]
        public string Subject { get; set; }

        [Required]
        [UIHint("Info")]
        [StringLength(2000)]
        [Column("DESCRIPTION"), DisplayName("說明")]
        public string Description { get; set; }

        [Required]
        [UIHint("String-W-200")]
        [Column("HOLD_DATE"), DisplayName("日期與時間")]
        public string HoldDate { get; set; }

        [Required]
        [StringLength(200)]
        [Column("LOCATION"), DisplayName("地點")]
        public string Location { get; set; }

        [Column("ACTIVITY_TYPE")]
        public ActivityType ActivityType { get; set; }

        public virtual Forum Forum { get; set; }

        public virtual ICollection<Document> Document { get; set; }
    }
}