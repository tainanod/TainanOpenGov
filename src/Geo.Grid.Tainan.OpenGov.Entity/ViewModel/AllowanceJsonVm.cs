using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    public class AllowanceJsonVm
    {
        ///<summary>
        /// 補助ID
        ///</summary>
        [JsonIgnore]
        public Guid AllowanceId { get; set; }

        ///<summary>
        /// 補助流水號
        ///</summary>
        [JsonProperty(PropertyName = "補助流水號")]
        public string AllowanceCode { get; set; }

        ///<summary>
        /// 補助名稱
        ///</summary>
        [JsonProperty(PropertyName = "補助名稱")]
        public string Name { get; set; }

        /////<summary>
        ///// 對象年齡
        /////</summary>
        //public string Age { get; set; }

        ///<summary>
        /// 對象年齡(下限)
        ///</summary>
        [JsonProperty(PropertyName = "補助對象年齡(下限)")]
        public int? AgeMin { get; set; }

        ///<summary>
        /// 對象年齡(上限)
        ///</summary>
        [JsonProperty(PropertyName = "補助對象年齡(上限)")]
        public int? AgeMax { get; set; }

        ///<summary>
        /// 設籍條件
        ///</summary>
        [JsonProperty(PropertyName = "設籍條件")]
        public string LiveIn { get; set; }

        ///<summary>
        /// 設籍條件(行政區)，是否
        ///</summary>
        [JsonIgnore]
        public string IsLiveIn { get; set; }

        ///<summary>
        /// 居住日數
        ///</summary>
        [JsonIgnore]
        public string LiveDays { get; set; }

        ///<summary>
        /// 居住日數(是否大於183日)，是否
        ///</summary>
        [JsonProperty(PropertyName = "居住日數是否需大於183日")]
        public string IsLiveDays { get; set; }

        ///<summary>
        /// 身份1
        ///</summary>
        [JsonProperty(PropertyName = "身份1")]
        public string Identity1 { get; set; }

        ///<summary>
        /// 身份2
        ///</summary>
        [JsonProperty(PropertyName = "身份2")]
        public string Identity2 { get; set; }

        /////<summary>
        ///// 收入條件
        /////</summary>
        //public string Income { get; set; }

        ///<summary>
        /// 收入條件(上限)
        ///</summary>
        [JsonProperty(PropertyName = "收入條件(每人每月上限)")]
        public int? IncomeValue { get; set; }

        /////<summary>
        ///// 動產條件
        /////</summary>
        //public string Movable { get; set; }

        ///<summary>
        /// 動產條件(上限)
        ///</summary>
        [JsonProperty(PropertyName = "動產條件(每人上限)")]
        public int? MovableValue { get; set; }

        /////<summary>
        ///// 不動產條件
        /////</summary>
        //public string Immovable { get; set; }

        ///<summary>
        /// 不動產條件(上限)
        ///</summary>
        [JsonProperty(PropertyName = "不動產條件(每戶上限)")]
        public int? ImmovableValue { get; set; }
        
        ///<summary>
        /// 其他條件
        ///</summary>
        [JsonProperty(PropertyName = "其他條件")]
        public string Others { get; set; }

        ///<summary>
        /// 應備文件
        ///</summary>
        [JsonProperty(PropertyName = "應備文件")]
        public string Docs { get; set; }

        ///<summary>
        /// 收件/洽辦單位
        ///</summary>
        [JsonProperty(PropertyName = "收件/洽辦單位")]
        public string Receiver { get; set; }

        ///<summary>
        /// 聯繫方式
        ///</summary>
        [JsonProperty(PropertyName = "聯繫方式")]
        public string Contact { get; set; }

        ///<summary>
        /// 詳細資訊
        ///</summary>
        [JsonProperty(PropertyName = "詳細資訊")]
        public string MoreInfo { get; set; }

        ///<summary>
        /// 資料集來源，*待完成資料同步功能後應改為 NOT NULL
        ///</summary>
        [JsonIgnore]
        public Guid? SourceId { get; set; }

        ///<summary>
        /// 開放資料的唯一ID(_id)
        ///</summary>
        [JsonProperty(PropertyName = "_id")]
        public int DataId { get; set; }
        
    }
}
