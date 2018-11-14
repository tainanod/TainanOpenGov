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
    /// 投票管理-投票(選項)
    /// </summary>
    [Table("VoteFillOption")]
    public class VoteFillOption
    {
        /// <summary>
        /// key-user
        /// </summary>
        [Key]
        [Column(Order = 0)]
        public string UserId { get; set; }

        /// <summary>
        /// key-option
        /// </summary>
        [Key]
        [Column(Order = 1)]
        public Guid OptionId { get; set; }

        /// <summary>
        /// key-主表
        /// </summary>
        public Guid VoteId { get; set; }

        /// <summary>
        /// 投票時間
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
