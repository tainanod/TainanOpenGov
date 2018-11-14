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
    /// 投票管理-投票-基本資料
    /// </summary>
    [Table("VoteFillBasic")]
    public class VoteFillBasic
    {
        /// <summary>
        /// key-user
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public string UserId { get; set; }

        /// <summary>
        /// key-basic
        /// </summary>
        [Key]
        [Column(Order = 1)]
        public Guid BasicId { get; set; }

        /// <summary>
        /// key-vote
        /// </summary>
        [Key]
        [Column(Order = 2)]
        public Guid VoteId { get; set; }

        /// <summary>
        /// 回答內容
        /// </summary>
        [StringLength(500)]
        public string BasicDesc { get; set; } = string.Empty;

        /// <summary>
        /// 投票管理-個資-群組編號
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// 投票時間
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
