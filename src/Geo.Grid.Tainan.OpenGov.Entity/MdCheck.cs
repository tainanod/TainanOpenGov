using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    [Table("MdCheck")]
    public class MdCheck
    {
        /// <summary>
        /// voteId
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public Guid InfoId { get; set; }

        /// <summary>
        /// userEmail
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [StringLength(128)]
        public string UserEmail { get; set; } = string.Empty;

        /// <summary>
        /// 匿名登入ID
        /// </summary>
        [StringLength(128)]
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsEnabled { get; set; } = false;

        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
