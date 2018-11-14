// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable EmptyNamespace
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 4.5
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geo.Grid.Tainan.OpenGov.DbModel.CodeFirst
{

    [Table("Engineering", Schema = "dbo")]
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.34.1.0")]
    public class Engineering
    {

        ///<summary>
        /// 執行機關代碼
        ///</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(@"GovernmentAenciesCode", Order = 1, TypeName = "nvarchar")]
        [Index(@"PK_Engineering", 1, IsUnique = true, IsClustered = true)]
        [Required]
        [MaxLength(100)]
        [StringLength(100)]
        [Key]
        [Display(Name = "Government aencies code")]
        public string GovernmentAenciesCode { get; set; }

        ///<summary>
        /// 編號
        ///</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(@"Code", Order = 2, TypeName = "nvarchar")]
        [Index(@"PK_Engineering", 2, IsUnique = true, IsClustered = true)]
        [Required]
        [MaxLength(100)]
        [StringLength(100)]
        [Key]
        [Display(Name = "Code")]
        public string Code { get; set; }

        ///<summary>
        /// 執行機關
        ///</summary>
        [Column(@"GovernmentAencies", Order = 3, TypeName = "nvarchar")]
        [Required]
        [MaxLength(200)]
        [StringLength(200)]
        [Display(Name = "Government aencies")]
        public string GovernmentAencies { get; set; }

        ///<summary>
        /// 標案名稱
        ///</summary>
        [Column(@"Name", Order = 4, TypeName = "nvarchar")]
        [Required]
        [MaxLength(300)]
        [StringLength(300)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        ///<summary>
        /// 決標金額(仟元)
        ///</summary>
        [Column(@"Amount", Order = 5, TypeName = "decimal")]
        [Required]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }

        ///<summary>
        /// 監造單位
        ///</summary>
        [Column(@"Supervision", Order = 6, TypeName = "nvarchar")]
        [Required]
        [MaxLength(200)]
        [StringLength(200)]
        [Display(Name = "Supervision")]
        public string Supervision { get; set; }

        ///<summary>
        /// 得標廠商
        ///</summary>
        [Column(@"Factory", Order = 7, TypeName = "nvarchar")]
        [Required]
        [MaxLength(200)]
        [StringLength(200)]
        [Display(Name = "Factory")]
        public string Factory { get; set; }

        ///<summary>
        /// 標案類別
        ///</summary>
        [Column(@"Category", Order = 8, TypeName = "nvarchar")]
        [Required]
        [MaxLength(100)]
        [StringLength(100)]
        [Display(Name = "Category")]
        public string Category { get; set; }

        ///<summary>
        /// 縣市鄉鎮
        ///</summary>
        [Column(@"CityTown", Order = 9, TypeName = "nvarchar")]
        [Required]
        [MaxLength(100)]
        [StringLength(100)]
        [Display(Name = "City town")]
        public string CityTown { get; set; }

        ///<summary>
        /// X座標、Y座標
        ///</summary>
        [Column(@"Geom", Order = 10, TypeName = "geometry")]
        [Required]
        [Display(Name = "Geom")]
        public System.Data.Entity.Spatial.DbGeometry Geom { get; set; }

        ///<summary>
        /// 詳細地點
        ///</summary>
        [Column(@"Address", Order = 11, TypeName = "nvarchar(max)")]
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        ///<summary>
        /// 工程概要
        ///</summary>
        [Column(@"Summary", Order = 12, TypeName = "nvarchar(max)")]
        [Display(Name = "Summary")]
        public string Summary { get; set; }

        ///<summary>
        /// 實際開工日期
        ///</summary>
        [Column(@"ActualStartDate", Order = 13, TypeName = "datetime")]
        [Required]
        [Display(Name = "Actual start date")]
        public System.DateTime ActualStartDate { get; set; }

        ///<summary>
        /// 原合約預定完工日
        ///</summary>
        [Column(@"TargetCompleteDate", Order = 14, TypeName = "datetime")]
        [Display(Name = "Target complete date")]
        public System.DateTime? TargetCompleteDate { get; set; }

        ///<summary>
        /// 變更後預定完工日
        ///</summary>
        [Column(@"ChangeCompleteDate", Order = 15, TypeName = "datetime")]
        [Display(Name = "Change complete date")]
        public System.DateTime? ChangeCompleteDate { get; set; }

        ///<summary>
        /// 進度月份
        ///</summary>
        [Column(@"ProgressDate", Order = 16, TypeName = "datetime")]
        [Required]
        [Display(Name = "Progress date")]
        public System.DateTime ProgressDate { get; set; }

        ///<summary>
        /// 預定進度(百分比)
        ///</summary>
        [Column(@"TargetProgress", Order = 17, TypeName = "decimal")]
        [Required]
        [Display(Name = "Target progress")]
        public decimal TargetProgress { get; set; }

        ///<summary>
        /// 實際進度(百分比)
        ///</summary>
        [Column(@"ActualProgress", Order = 18, TypeName = "decimal")]
        [Required]
        [Display(Name = "Actual progress")]
        public decimal ActualProgress { get; set; }

        ///<summary>
        /// 差異(百分比)
        ///</summary>
        [Column(@"Discrepancy", Order = 19, TypeName = "decimal")]
        [Required]
        [Display(Name = "Discrepancy")]
        public decimal Discrepancy { get; set; }

        ///<summary>
        /// 狀態
        ///</summary>
        [Column(@"Status", Order = 20, TypeName = "nvarchar")]
        [Required]
        [MaxLength(100)]
        [StringLength(100)]
        [Display(Name = "Status")]
        public string Status { get; set; }

        ///<summary>
        /// 落後因素
        ///</summary>
        [Column(@"BehindReason", Order = 21, TypeName = "nvarchar(max)")]
        [Display(Name = "Behind reason")]
        public string BehindReason { get; set; }

        ///<summary>
        /// 原因分析
        ///</summary>
        [Column(@"Analysis", Order = 22, TypeName = "nvarchar(max)")]
        [Display(Name = "Analysis")]
        public string Analysis { get; set; }

        ///<summary>
        /// 解決辦法
        ///</summary>
        [Column(@"Solution", Order = 23, TypeName = "nvarchar(max)")]
        [Display(Name = "Solution")]
        public string Solution { get; set; }

        ///<summary>
        /// 改進期限
        ///</summary>
        [Column(@"ImproveTermDate", Order = 24, TypeName = "datetime")]
        [Display(Name = "Improve term date")]
        public System.DateTime? ImproveTermDate { get; set; }

        ///<summary>
        /// 實際完工日期
        ///</summary>
        [Column(@"ActualCompleteDate", Order = 25, TypeName = "datetime")]
        [Display(Name = "Actual complete date")]
        public System.DateTime? ActualCompleteDate { get; set; }

        ///<summary>
        /// 建立人員
        ///</summary>
        [Column(@"CreatedBy", Order = 26, TypeName = "nvarchar")]
        [Required]
        [MaxLength(128)]
        [StringLength(128)]
        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        ///<summary>
        /// 建立日期
        ///</summary>
        [Column(@"CreatedDate", Order = 27, TypeName = "datetime")]
        [Required]
        [Display(Name = "Created date")]
        public System.DateTime CreatedDate { get; set; }

        ///<summary>
        /// 最後更新人員
        ///</summary>
        [Column(@"UpdatedBy", Order = 28, TypeName = "nvarchar")]
        [Required]
        [MaxLength(128)]
        [StringLength(128)]
        [Display(Name = "Updated by")]
        public string UpdatedBy { get; set; }

        ///<summary>
        /// 最後更新日期
        ///</summary>
        [Column(@"UpdatedDate", Order = 29, TypeName = "datetime")]
        [Required]
        [Display(Name = "Updated date")]
        public System.DateTime UpdatedDate { get; set; }

        ///<summary>
        /// 是否啟用
        ///</summary>
        [Column(@"IsEnabled", Order = 30, TypeName = "bit")]
        [Required]
        [Display(Name = "Is enabled")]
        public bool IsEnabled { get; set; }

        public Engineering()
        {
            CreatedDate = System.DateTime.Now;
            UpdatedDate = System.DateTime.Now;
            IsEnabled = true;
        }
    }

}
// </auto-generated>
