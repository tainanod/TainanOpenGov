using System;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    /// <summary>
    /// 填寫人回覆
    /// </summary>
    public class QuestGatherExcelFillVm
    {
        /// <summary>
        /// infoId
        /// </summary>
        public Guid InfoId { get; set; }

        /// <summary>
        /// userId
        /// </summary>
        public Guid MdFillUserId { get; set; }

        /// <summary>
        /// 填寫人-暱稱
        /// </summary>
        public string UserNickName { get; set; } = string.Empty;

        /// <summary>
        /// 填寫人-email
        /// </summary>
        public string UserEmail { get; set; } = string.Empty;

        /// <summary>
        /// qstId
        /// </summary>
        public Guid QstId { get; set; }

        /// <summary>
        /// 題型
        /// </summary>
        public int QstAnsType { get; set; }

        /// <summary>
        /// setId
        /// </summary>
        public Guid SetId { get; set; }

        /// <summary>
        /// 項目說明
        /// </summary>
        public string SetDesc { get; set; } = string.Empty;

        /// <summary>
        /// 回覆內容
        /// </summary>
        public string FillDesc { get; set; } = string.Empty;
    }
}
