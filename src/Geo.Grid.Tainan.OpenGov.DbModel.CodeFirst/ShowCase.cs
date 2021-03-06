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

    [Table("ShowCase", Schema = "dbo")]
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.34.1.0")]
    public class ShowCase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(@"CaseId", Order = 1, TypeName = "uniqueidentifier")]
        [Index(@"PK_dbo.ShowCase", 1, IsUnique = true, IsClustered = true)]
        [Required]
        [Key]
        [Display(Name = "Case ID")]
        public System.Guid CaseId { get; set; }

        ///<summary>
        /// 標題
        ///</summary>
        [Column(@"Title", Order = 2, TypeName = "nvarchar")]
        [Required]
        [MaxLength(400)]
        [StringLength(400)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        ///<summary>
        /// 創作者
        ///</summary>
        [Column(@"UserName", Order = 3, TypeName = "nvarchar")]
        [Required]
        [MaxLength(255)]
        [StringLength(255)]
        [DataType(DataType.Text)]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        ///<summary>
        /// 簡介
        ///</summary>
        [Column(@"Contents", Order = 4, TypeName = "nvarchar(max)")]
        [Required]
        [Display(Name = "Contents")]
        public string Contents { get; set; }

        ///<summary>
        /// 代表圖
        ///</summary>
        [Column(@"PhotoId", Order = 5, TypeName = "uniqueidentifier")]
        [Required]
        [Display(Name = "Photo ID")]
        public System.Guid PhotoId { get; set; }

        ///<summary>
        /// 排序
        ///</summary>
        [Column(@"Sort", Order = 6, TypeName = "int")]
        [Required]
        [Display(Name = "Sort")]
        public int Sort { get; set; }

        ///<summary>
        /// 是否顯示
        ///</summary>
        [Column(@"IsEnabled", Order = 7, TypeName = "bit")]
        [Required]
        [Display(Name = "Is enabled")]
        public bool IsEnabled { get; set; }

        ///<summary>
        /// 是否刪除
        ///</summary>
        [Column(@"IsDeleted", Order = 8, TypeName = "bit")]
        [Required]
        [Display(Name = "Is deleted")]
        public bool IsDeleted { get; set; }

        ///<summary>
        /// 建立時間
        ///</summary>
        [Column(@"CreatedDate", Order = 9, TypeName = "datetime")]
        [Required]
        [Display(Name = "Created date")]
        public System.DateTime CreatedDate { get; set; }

        ///<summary>
        /// 建立者
        ///</summary>
        [Column(@"CreatedBy", Order = 10, TypeName = "nvarchar")]
        [Required]
        [MaxLength(128)]
        [StringLength(128)]
        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        ///<summary>
        /// 修改時間
        ///</summary>
        [Column(@"UpdatedDate", Order = 11, TypeName = "datetime")]
        [Required]
        [Display(Name = "Updated date")]
        public System.DateTime UpdatedDate { get; set; }

        ///<summary>
        /// 修改者
        ///</summary>
        [Column(@"UpdatedBy", Order = 12, TypeName = "nvarchar")]
        [Required]
        [MaxLength(128)]
        [StringLength(128)]
        [Display(Name = "Updated by")]
        public string UpdatedBy { get; set; }

        [Column(@"UserEmail", Order = 13, TypeName = "nvarchar")]
        [Required]
        [MaxLength(128)]
        [StringLength(128)]
        [Display(Name = "User email")]
        public string UserEmail { get; set; }

        public System.Collections.Generic.ICollection<DataSet> DataSets { get; set; }
        public System.Collections.Generic.ICollection<Photo> Photos { get; set; }
        public System.Collections.Generic.ICollection<ShowCaseUrl> ShowCaseUrls { get; set; }

        public ShowCase()
        {
            CaseId = System.Guid.NewGuid();
            Title = "";
            UserName = "";
            Contents = "";
            Sort = 0;
            IsEnabled = true;
            IsDeleted = false;
            CreatedDate = System.DateTime.Now;
            CreatedBy = "";
            UpdatedDate = System.DateTime.Now;
            UpdatedBy = "";
            UserEmail = "";
            ShowCaseUrls = new System.Collections.Generic.List<ShowCaseUrl>();
            DataSets = new System.Collections.Generic.List<DataSet>();
            Photos = new System.Collections.Generic.List<Photo>();
        }
    }

}
// </auto-generated>
