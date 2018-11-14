using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    /// <summary>
    /// 問卷結果-填寫檢視
    /// </summary>
    public class QuestGatherExamineVm
    {
        /// <summary>
        /// infoId
        /// </summary>
        public Guid InfoId { get; set; }

        /// <summary>
        /// 填寫人
        /// </summary>
        public Guid MdFillUserId { get; set; }

        /// <summary>
        /// 填寫人-email
        /// </summary>
        public string MdFillUserEmail { get; set; } = string.Empty;

        /// <summary>
        /// 填寫時間
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 暱稱
        /// </summary>
        public string UserNickName { get; set; } = string.Empty;

        /// <summary>
        /// 電子郵件
        /// </summary>
        public string UserEmail { get; set; } = string.Empty;
    }
}
