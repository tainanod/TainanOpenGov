using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    [Table("Allowance", Schema = "dbo")]
    public class Allowance
    {
        public Allowance()
        {
            AllowanceId = Guid.NewGuid();
        }

        ///<summary>
        /// 補助ID
        ///</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(@"AllowanceId", Order = 1, TypeName = "uniqueidentifier")]
        [Index(@"PK_Allowance", 1, IsUnique = true, IsClustered = true)]
        [Required]
        [Key]
        [Display(Name = "Allowance ID")]
        public System.Guid AllowanceId { get; set; }

        ///<summary>
        /// 補助流水號
        ///</summary>
        [Column(@"AllowanceCode", Order = 2, TypeName = "nvarchar")]
        [Required]
        [MaxLength(10)]
        [StringLength(10)]
        [Display(Name = "Allowance code")]
        public string AllowanceCode { get; set; }

        ///<summary>
        /// 補助名稱
        ///</summary>
        [Column(@"Name", Order = 3, TypeName = "nvarchar(max)")]
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        ///<summary>
        /// 對象年齡
        ///</summary>
        [Column(@"Age", Order = 4, TypeName = "nvarchar(max)")]
        [Display(Name = "Age")]
        public string Age { get; set; }

        ///<summary>
        /// 對象年齡(下限)
        ///</summary>
        [Column(@"AgeMin", Order = 5, TypeName = "int")]
        [Display(Name = "Age min")]
        public int? AgeMin { get; set; }

        ///<summary>
        /// 對象年齡(上限)
        ///</summary>
        [Column(@"AgeMax", Order = 6, TypeName = "int")]
        [Display(Name = "Age max")]
        public int? AgeMax { get; set; }

        ///<summary>
        /// 設籍條件
        ///</summary>
        [Column(@"LiveIn", Order = 7, TypeName = "nvarchar(max)")]
        [Display(Name = "Live in")]
        public string LiveIn { get; set; }

        ///<summary>
        /// 設籍條件(行政區)，是否
        ///</summary>
        [Column(@"IsLiveIn", Order = 8, TypeName = "nvarchar(max)")]
        [Display(Name = "Is live in")]
        public string IsLiveIn { get; set; }

        ///<summary>
        /// 居住日數
        ///</summary>
        [Column(@"LiveDays", Order = 9, TypeName = "nvarchar(max)")]
        [Display(Name = "Live days")]
        public string LiveDays { get; set; }

        ///<summary>
        /// 居住日數(是否大於183日)，是否
        ///</summary>
        [Column(@"IsLiveDays", Order = 10, TypeName = "nvarchar(max)")]
        [Display(Name = "Is live days")]
        public string IsLiveDays { get; set; }

        ///<summary>
        /// 身份1
        ///</summary>
        [Column(@"Identity1", Order = 11, TypeName = "nvarchar(max)")]
        [Display(Name = "Identity 1")]
        public string Identity1 { get; set; }

        ///<summary>
        /// 身份2
        ///</summary>
        [Column(@"Identity2", Order = 12, TypeName = "nvarchar(max)")]
        [Display(Name = "Identity 2")]
        public string Identity2 { get; set; }

        ///<summary>
        /// 收入條件
        ///</summary>
        [Column(@"Income", Order = 13, TypeName = "nvarchar(max)")]
        [Display(Name = "Income")]
        public string Income { get; set; }

        ///<summary>
        /// 收入條件(上限)
        ///</summary>
        [Column(@"IncomeValue", Order = 14, TypeName = "int")]
        [Display(Name = "Income value")]
        public int? IncomeValue { get; set; }

        ///<summary>
        /// 動產條件
        ///</summary>
        [Column(@"Movable", Order = 15, TypeName = "nvarchar(max)")]
        [Display(Name = "Movable")]
        public string Movable { get; set; }

        ///<summary>
        /// 動產條件(上限)
        ///</summary>
        [Column(@"MovableValue", Order = 16, TypeName = "int")]
        [Display(Name = "Movable value")]
        public int? MovableValue { get; set; }

        ///<summary>
        /// 不動產條件
        ///</summary>
        [Column(@"Immovable", Order = 17, TypeName = "nvarchar(max)")]
        [Display(Name = "Immovable")]
        public string Immovable { get; set; }

        ///<summary>
        /// 不動產條件(上限)
        ///</summary>
        [Column(@"ImmovableValue", Order = 18, TypeName = "int")]
        [Display(Name = "Immovable value")]
        public int? ImmovableValue { get; set; }

        ///<summary>
        /// 其他條件
        ///</summary>
        [Column(@"Others", Order = 19, TypeName = "nvarchar(max)")]
        [Display(Name = "Others")]
        public string Others { get; set; }

        ///<summary>
        /// 應備文件
        ///</summary>
        [Column(@"Docs", Order = 20, TypeName = "nvarchar(max)")]
        [Display(Name = "Docs")]
        public string Docs { get; set; }

        ///<summary>
        /// 收件/洽辦單位
        ///</summary>
        [Column(@"Receiver", Order = 21, TypeName = "nvarchar(max)")]
        [Display(Name = "Receiver")]
        public string Receiver { get; set; }

        ///<summary>
        /// 聯繫方式
        ///</summary>
        [Column(@"Contact", Order = 22, TypeName = "nvarchar(max)")]
        [Display(Name = "Contact")]
        public string Contact { get; set; }

        ///<summary>
        /// 詳細資訊
        ///</summary>
        [Column(@"MoreInfo", Order = 23, TypeName = "nvarchar(max)")]
        [Display(Name = "More info")]
        public string MoreInfo { get; set; }

        ///<summary>
        /// 資料集來源，*待完成資料同步功能後應改為 NOT NULL
        ///</summary>
        [Column(@"SourceId", Order = 24, TypeName = "uniqueidentifier")]
        [Display(Name = "Source ID")]
        public Guid? SourceId { get; set; }

        ///<summary>
        /// 開放資料的唯一ID(_id)
        ///</summary>
        [Column(@"DataId", Order = 25, TypeName = "int")]
        [Required]
        [Display(Name = "Data ID")]
        public int DataId { get; set; }

        /// <summary>
        /// 公益臺南-資料集來源管理
        /// </summary>
        [ForeignKey("SourceId")]
        public AllowanceSource AllowanceSource { get; set; }

    }
}
