using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    [Table("DISCUSS")]
    public partial class Discuss : AuditableEntity
    {
        public Discuss()
        {
            Tag = new HashSet<Tag>();
            PushUser = new HashSet<AspNetUsers>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("DISCUSS_ID")]
        public Guid DiscussId { get; set; }

        [Column("FORUM_ID")]
        public Guid ForumId { get; set; }

        [Required]
        [StringLength(128)]
        [Column("USER_ID")]
        public string UserId { get; set; }

        [Column("UPPER_ID")]
        public Guid? UpperId { get; set; }

        [Required]
        [StringLength(4000)]
        [Column("MESSAGE")]
        public string Message { get; set; }

        /// <summary>
        /// 置頂
        /// </summary>
        [Column("IS_TOP")]
        public bool IsTop { get; set; } = false;

        public virtual Forum Forum { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual ICollection<Tag> Tag { get; set; }

        public virtual ICollection<AspNetUsers> PushUser { get; set; }
    }
}