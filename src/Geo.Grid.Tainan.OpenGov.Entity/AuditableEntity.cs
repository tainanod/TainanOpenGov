using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Geo.Grid.Tainan.OpenGov.Entity.Interface;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    public abstract class AuditableEntity : IAuditableEntity
    {
        [ScaffoldColumn(false)]
        [Column("IS_ENABLED")]
        public bool IsEnabled { get; set; }

        [ScaffoldColumn(false)]
        [Column("CREATED_BY")]
        [StringLength(100)]
        [Required]
        public string CreatedBy { get; set; }

        [ScaffoldColumn(false)]
        [Column("CREATED_DATE")]
        [Required]
        [DisplayFormat(DataFormatString ="{0:yyyy/MM/dd hh:mm:ss}")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [ScaffoldColumn(false)]
        [Column("UPDATE_BY")]
        [StringLength(100)]
        [Required]
        public string UpdatedBy { get; set; }

        [ScaffoldColumn(false)]
        [Column("UPDATE_DATE")]
        [Required]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
    }
}