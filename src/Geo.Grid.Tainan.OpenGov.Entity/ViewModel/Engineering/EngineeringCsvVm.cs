﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Engineering
{
    public class EngineeringCsvVm
    {
        ///<summary>
        /// 執行機關代碼
        ///</summary>
        public string GovernmentAenciesCode { get; set; }

        ///<summary>
        /// 編號
        ///</summary>
        public string Code { get; set; }

        ///<summary>
        /// 執行機關
        ///</summary>
        public string GovernmentAencies { get; set; }

        ///<summary>
        /// 標案名稱
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        /// 決標金額(仟元)
        ///</summary>
        public string Amount { get; set; }

        ///<summary>
        /// 監造單位
        ///</summary>
        public string Supervision { get; set; }

        ///<summary>
        /// 得標廠商
        ///</summary>
        public string Factory { get; set; }

        ///<summary>
        /// 標案類別
        ///</summary>
        public string Category { get; set; }

        ///<summary>
        /// 縣市鄉鎮
        ///</summary>
        public string CityTown { get; set; }

        ///<summary>
        /// X座標
        ///</summary>
        public string X { get; set; }

        ///<summary>
        /// Y座標
        ///</summary>
        public string Y { get; set; }

        ///<summary>
        /// 詳細地點
        ///</summary>
        public string Address { get; set; }

        ///<summary>
        /// 工程概要
        ///</summary>
        public string Summary { get; set; }

        ///<summary>
        /// 實際開工日期
        ///</summary>
        public string ActualStartDate { get; set; }

        ///<summary>
        /// 原合約預定完工日
        ///</summary>
        public string TargetCompleteDate { get; set; }

        ///<summary>
        /// 變更後預定完工日
        ///</summary>
        public string ChangeCompleteDate { get; set; }

        ///<summary>
        /// 進度月份
        ///</summary>
        public string ProgressDate { get; set; }

        ///<summary>
        /// 預定進度(百分比)
        ///</summary>
        public string TargetProgress { get; set; }

        ///<summary>
        /// 實際進度(百分比)
        ///</summary>
        public string ActualProgress { get; set; }

        ///<summary>
        /// 差異(百分比)
        ///</summary>
        public string Discrepancy { get; set; }

        ///<summary>
        /// 狀態
        ///</summary>
        public string Status { get; set; }

        ///<summary>
        /// 落後因素
        ///</summary>
        public string BehindReason { get; set; }

        ///<summary>
        /// 原因分析
        ///</summary>
        public string Analysis { get; set; }

        ///<summary>
        /// 解決辦法
        ///</summary>
        public string Solution { get; set; }

        ///<summary>
        /// 改進期限
        ///</summary>
        public string ImproveTermDate { get; set; }

        ///<summary>
        /// 實際完工日期
        ///</summary>
        public string ActualCompleteDate { get; set; }
    }
}
