﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Admin
{
    /// <summary>
    /// 投票管理-User回答
    /// </summary>
    public class VoteFillUserExcelVm
    {
        /// <summary>
        /// userId
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        /// nickName
        /// </summary>
        public string UserNickName { get; set; } = string.Empty;

        /// <summary>
        /// email
        /// </summary>
        public string UserEmail { get; set; } = string.Empty;

        /// <summary>
        /// 填寫時間
        /// </summary>
        public DateTime UserCreate { get; set; }

        /// <summary>
        /// 選項-回答
        /// </summary>
        public IEnumerable<GuidNameVm> OptionArray { get; set; }

        /// <summary>
        /// 個資-回答
        /// </summary>
        public IEnumerable<IdNameVm> BasicArray { get; set; }
    }
}
