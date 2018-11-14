using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    /// <summary>
    /// 投票管理-儲存
    /// </summary>
    public class VoteSaveVm
    {
        /// <summary>
        /// voteId
        /// </summary>
        public Guid VoteId { get; set; }

        /// <summary>
        /// userMail
        /// </summary>
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string UserMail { get; set; } = string.Empty;

        /// <summary>
        /// 投票管理
        /// </summary>
        public IEnumerable<VoteSaveOptionVm> OptionArray { get; set; }

        /// <summary>
        /// 個資
        /// </summary>
        public IEnumerable<VoteSaveBasicVm> BasicArray { get; set; }
    }

    /// <summary>
    /// 投票管理-儲存-選項用
    /// </summary>
    public class VoteSaveOptionVm
    {
        /// <summary>
        /// optionId
        /// </summary>
        public Guid OptionId { get; set; }
    }

    /// <summary>
    /// 投票管理-儲存-個資用
    /// </summary>
    public class VoteSaveBasicVm
    {
        /// <summary>
        /// basicId
        /// </summary>
        public Guid BasicId { get; set; }

        /// <summary>
        /// basicDesc
        /// </summary>
        public string BasicDesc { get; set; } = string.Empty;
    }

}
