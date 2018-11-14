using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    /// <summary>
    /// 信件管理
    /// </summary>
    [Table("WaitingMail")]
    public class WaitingMail
    {
        /// <summary>
        /// key
        /// </summary>
        [Key]
        public int MailSeq { get; set; }

        /// <summary>
        /// 寄發系統
        /// </summary>
        [StringLength(400)]
        public string TypeName { get; set; } = string.Empty;

        /// <summary>
        /// 收件人姓名
        /// </summary>
        [StringLength(50)]
        public string ToName { get; set; } = string.Empty;

        /// <summary>
        /// 收件人Email
        /// </summary>
        [StringLength(400)]
        public string ToEmail { get; set; } = string.Empty;

        /// <summary>
        /// 寄件人姓名
        /// </summary>
        [StringLength(50)]
        public string FromName { get; set; } = string.Empty;

        /// <summary>
        /// 寄件人Email
        /// </summary>
        [StringLength(400)]
        public string FromEmail { get; set; } = string.Empty;

        /// <summary>
        /// 密件Email
        /// </summary>
        [StringLength(800)]
        public string BccEmail { get; set; } = string.Empty;

        /// <summary>
        /// 郵件主旨
        /// </summary>
        [StringLength(256)]
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// 郵件內容
        /// </summary>
        [StringLength(8000)]
        public string MailContent { get; set; } = string.Empty;

        /// <summary>
        /// 是否已發送
        /// </summary>
        public bool IsSend { get; set; } = false;

        /// <summary>
        /// 新增日期
        /// </summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 實際發送日期
        /// </summary>
        public DateTime? SendTime { get; set; }
    }
}
