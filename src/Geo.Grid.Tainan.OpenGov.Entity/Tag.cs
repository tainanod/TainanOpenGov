using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    [Table("TAG")]
    public class Tag: AuditableEntity
    {
        public Tag()
        {
            Discuss = new HashSet<Discuss>();
        }

        [Key]
        [Column("TAG_ID")]
        public Guid TagId { get; set; } // TAG_ID (Primary key)
        
        [Column("FORUM_ID")]
        public Guid ForumId { get; set; }

        [Column("SORT")]
        public int Sort { get; set; }

        [Required]
        [StringLength(50)]
        [Column("TAG_NAME")]
        [DisplayName("標籤名稱")]
        public string TagName { get; set; } // TAG_NAME

        /// <summary>
        /// 公民論壇
        /// </summary>
        public virtual Forum Forum { get; set; }

        public virtual ICollection<Discuss> Discuss { get; set; } // Many to many mapping
        
    }
}
