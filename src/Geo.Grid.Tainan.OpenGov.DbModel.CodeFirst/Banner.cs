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

    [Table("Banner", Schema = "dbo")]
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.34.1.0")]
    public class Banner
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(@"BannerId", Order = 1, TypeName = "uniqueidentifier")]
        [Index(@"PK_dbo.Banner", 1, IsUnique = true, IsClustered = true)]
        [Required]
        [Key]
        [Display(Name = "Banner ID")]
        public System.Guid BannerId { get; set; }

        ///<summary>
        /// 標題
        ///</summary>
        [Column(@"Title", Order = 2, TypeName = "nvarchar")]
        [Required]
        [MaxLength(255)]
        [StringLength(255)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        ///<summary>
        /// 連結網址
        ///</summary>
        [Column(@"WebUrl", Order = 3, TypeName = "nvarchar")]
        [Required]
        [MaxLength(400)]
        [StringLength(400)]
        [Display(Name = "Web url")]
        public string WebUrl { get; set; }

        ///<summary>
        /// 連結方式
        ///</summary>
        [Column(@"Target", Order = 4, TypeName = "bit")]
        [Required]
        [Display(Name = "Target")]
        public bool Target { get; set; }

        ///<summary>
        /// 置頂
        ///</summary>
        [Column(@"IsTopEnabled", Order = 5, TypeName = "bit")]
        [Required]
        [Display(Name = "Is top enabled")]
        public bool IsTopEnabled { get; set; }

        ///<summary>
        /// 是否刪除-0:不刪；1:刪
        ///</summary>
        [Column(@"IsDeleted", Order = 6, TypeName = "bit")]
        [Required]
        [Display(Name = "Is deleted")]
        public bool IsDeleted { get; set; }

        ///<summary>
        /// 圖片管理
        ///</summary>
        [Column(@"PhotoId", Order = 7, TypeName = "uniqueidentifier")]
        [Required]
        [Display(Name = "Photo ID")]
        public System.Guid PhotoId { get; set; }

        ///<summary>
        /// 是否顯示-0不顯；1:顯
        ///</summary>
        [Column(@"IsEnabled", Order = 8, TypeName = "bit")]
        [Required]
        [Display(Name = "Is enabled")]
        public bool IsEnabled { get; set; }

        ///<summary>
        /// 建立者
        ///</summary>
        [Column(@"CreatedBy", Order = 9, TypeName = "nvarchar")]
        [Required]
        [MaxLength(128)]
        [StringLength(128)]
        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        ///<summary>
        /// 建立時間
        ///</summary>
        [Column(@"CreatedDate", Order = 10, TypeName = "datetime")]
        [Required]
        [Display(Name = "Created date")]
        public System.DateTime CreatedDate { get; set; }

        ///<summary>
        /// 修改者
        ///</summary>
        [Column(@"UpdatedBy", Order = 11, TypeName = "nvarchar")]
        [Required]
        [MaxLength(128)]
        [StringLength(128)]
        [Display(Name = "Updated by")]
        public string UpdatedBy { get; set; }

        ///<summary>
        /// 修改時間
        ///</summary>
        [Column(@"UpdatedDate", Order = 12, TypeName = "datetime")]
        [Required]
        [Display(Name = "Updated date")]
        public System.DateTime UpdatedDate { get; set; }

        public Banner()
        {
            BannerId = System.Guid.NewGuid();
        }
    }

}
// </auto-generated>
