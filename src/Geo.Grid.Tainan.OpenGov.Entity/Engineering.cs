using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    [Table("Engineering")]
    public class Engineering: AuditableEntityNew
    {
        public Engineering()
        {

            IsEnabled = true;
        }

        ///<summary>
        /// 執行機關代碼
        ///</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [MaxLength(100)]
        [Key]
        [Column(Order = 1)]
        public string GovernmentAenciesCode { get; set; }

        ///<summary>
        /// 編號
        ///</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [MaxLength(100)]
        [Key]
        [Column(Order = 2)]
        public string Code { get; set; }

        ///<summary>
        /// 執行機關
        ///</summary>
        [Required]
        [MaxLength(200)]
        public string GovernmentAencies { get; set; }

        ///<summary>
        /// 標案名稱
        ///</summary>
        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        ///<summary>
        /// 決標金額(仟元)
        ///</summary>
        [Required]
        public decimal Amount { get; set; }

        ///<summary>
        /// 監造單位
        ///</summary>
        [Required]
        [MaxLength(200)]
        public string Supervision { get; set; }

        ///<summary>
        /// 得標廠商
        ///</summary>
        [Required]
        [MaxLength(200)]
        public string Factory { get; set; }

        ///<summary>
        /// 標案類別
        ///</summary>
        [Required]
        [MaxLength(100)]
        public string Category { get; set; }

        ///<summary>
        /// 縣市鄉鎮
        ///</summary>
        [Required]
        [MaxLength(100)]
        public string CityTown { get; set; }

        ///<summary>
        /// X座標、Y座標
        ///</summary>
        [Required]
        public System.Data.Entity.Spatial.DbGeometry Geom { get; set; }

        ///<summary>
        /// 詳細地點
        ///</summary>
        [Required]
        public string Address { get; set; }

        ///<summary>
        /// 工程概要
        ///</summary>
        public string Summary { get; set; }

        ///<summary>
        /// 實際開工日期
        ///</summary>
        [Required]
        public DateTime ActualStartDate { get; set; }

        ///<summary>
        /// 原合約預定完工日
        ///</summary>
        public DateTime? TargetCompleteDate { get; set; }

        ///<summary>
        /// 變更後預定完工日
        ///</summary>
        public DateTime? ChangeCompleteDate { get; set; }

        ///<summary>
        /// 進度月份
        ///</summary>
        [Required]
        public DateTime ProgressDate { get; set; }

        ///<summary>
        /// 預定進度(百分比)
        ///</summary>
        [Required]
        public decimal TargetProgress { get; set; }

        ///<summary>
        /// 實際進度(百分比)
        ///</summary>
        [Required]
        public decimal ActualProgress { get; set; }

        ///<summary>
        /// 差異(百分比)
        ///</summary>
        [Required]
        public decimal Discrepancy { get; set; }

        ///<summary>
        /// 狀態
        ///</summary>
        [Required]
        [MaxLength(100)]
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
        public DateTime? ImproveTermDate { get; set; }

        ///<summary>
        /// 實際完工日期
        ///</summary>
        public DateTime? ActualCompleteDate { get; set; }
    }
}
