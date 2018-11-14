using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    [Table("FORUM")]
    public partial class Forum : AuditableEntity
    {
        public Forum()
        {
            Activity = new HashSet<Activity>();
            Discuss = new HashSet<Discuss>();
            Document = new HashSet<Document>();
            Hyperlink = new HashSet<Hyperlink>();
            Photo = new HashSet<Photo>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("FORUM_ID")]
        public Guid ForumId { get; set; }

        [Required]
        [UIHint("String-W-320")]
        [StringLength(100)]
        [Column("CATEGORY"), DisplayName("主題類別")]
        public string Category { get; set; }

        [Required]
        [StringLength(300)]
        [Column("SUBJECT"), DisplayName("主題")]
        public string Subject { get; set; }

        //[Required]
        //[UIHint("String-W-320")]
        [StringLength(100)]
        [UIHint("SelectList")]
        [Column("HOLDER"), DisplayName("主辦單位")]
        public string Holder { get; set; }

        [Required]
        [UIHint("Info")]
        [StringLength(4000)]
        [Column("DESCRIPTION"), DisplayName("描述")]
        public string Description { get; set; }

        [UIHint("DateTimePicker-W-200")]
        [Column("OPEN_DATE"), DisplayName("發佈時間")]
        [DisplayFormat(DataFormatString = " {0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime OpenDate { get; set; }

        [UIHint("DateTimePicker-W-200")]
        [Column("CLOSE_DATE"), DisplayName("截止時間")]
        [DisplayFormat(DataFormatString = " {0:yyyy/MM/dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime CloseDate { get; set; }

        [Column("FORUM_TYPE")]
        public ForumType ForumType { get; set; }

        [Column("UPPER_ID")]
        public Guid? UpperId { get; set; }

        [UIHint("Info-5-300")]
        [StringLength(4000)]
        [Column("ANNOUNCEMENT"), DisplayName("討論區公告")]
        public string Announcement { get; set; }

        [Required]
        [UIHint("SelectList")]
        [Column("DataSetUnitId"), DisplayName("主辦單位")]
        public Guid? DataSetUnitId { get; set; }

        public virtual ICollection<Activity> Activity { get; set; }

        public virtual ICollection<Discuss> Discuss { get; set; }

        public virtual ICollection<Document> Document { get; set; }

        public virtual ICollection<Hyperlink> Hyperlink { get; set; }

        public virtual ICollection<Photo> Photo { get; set; }

        public virtual ICollection<MdQuestInfo> MdQuestionInfo { get; set; }

        /// <summary>
        /// 投票管理
        /// </summary>
        public virtual ICollection<Vote> Vote { get; set; }

        /// <summary>
        /// 匿名點擊
        /// </summary>
        public virtual ICollection<AnonymousClick> AnonymousClick { get; set; }

        /// <summary>
        /// 標籤管理
        /// </summary>
        public virtual ICollection<Tag> Tag { get; set; }

        /// <summary>
        /// 主辦單位
        /// </summary>
        public virtual DataSetUnit DataSetUnit { get; set; }
    }
}