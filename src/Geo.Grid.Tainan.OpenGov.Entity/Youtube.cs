using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    [Table("YOUTUBE")]
    public partial class Youtube : AuditableEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("YOUTUBE_ID", Order = 0)]
        public Guid YoutubeId { get; set; }

        [Required]
        [StringLength(50)]
        [Column("NAME", Order = 1), DisplayName("會議名稱")]
        public string Name { get; set; }

        [UIHint("Info-3-300")]
        [StringLength(4000)]
        [Column("DESCRIPTION"), DisplayName("影片描述")]
        public string Description { get; set; }

        [StringLength(4000)]
        [Column("URL", Order = 2), DisplayName("直播位址")]
        public string Url { get; set; }

        [Required]
        [UIHint("DateTimePicker-W-200")]
        [Column("START_DATE", Order = 3), DisplayName("播放時間")]
        [DisplayFormat(DataFormatString = " {0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required]
        [UIHint("DateTimePicker-W-200")]
        [Column("END_DATE"), DisplayName("結束時間")]
        [DisplayFormat(DataFormatString = " {0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [UIHint("String-W-320")]
        [StringLength(50)]
        [Column("VIDEO_TIME", Order = 4), DisplayName("影片長度")]
        public string VideoTime { get; set; }

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column("IS_OPENED", Order = 5)]
        public bool IsOpened { get; set; }
    }
}