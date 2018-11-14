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

    [Table("DataSetType", Schema = "dbo")]
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.34.1.0")]
    public class DataSetType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(@"TypeId", Order = 1, TypeName = "uniqueidentifier")]
        [Index(@"PK_dbo.DataSetType", 1, IsUnique = true, IsClustered = true)]
        [Required]
        [Key]
        [Display(Name = "Type ID")]
        public System.Guid TypeId { get; set; }

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
        /// 排序
        ///</summary>
        [Column(@"Sort", Order = 3, TypeName = "int")]
        [Required]
        [Display(Name = "Sort")]
        public int Sort { get; set; }

        ///<summary>
        /// 是否顯示
        ///</summary>
        [Column(@"IsEnabled", Order = 4, TypeName = "bit")]
        [Required]
        [Display(Name = "Is enabled")]
        public bool IsEnabled { get; set; }

        ///<summary>
        /// 建立時間
        ///</summary>
        [Column(@"CreatedDate", Order = 5, TypeName = "datetime")]
        [Required]
        [Display(Name = "Created date")]
        public System.DateTime CreatedDate { get; set; }

        ///<summary>
        /// 建立者
        ///</summary>
        [Column(@"CreatedBy", Order = 6, TypeName = "nvarchar")]
        [Required]
        [MaxLength(128)]
        [StringLength(128)]
        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        ///<summary>
        /// 修改時間
        ///</summary>
        [Column(@"UpdatedDate", Order = 7, TypeName = "datetime")]
        [Required]
        [Display(Name = "Updated date")]
        public System.DateTime UpdatedDate { get; set; }

        ///<summary>
        /// 修改者
        ///</summary>
        [Column(@"UpdatedBy", Order = 8, TypeName = "nvarchar")]
        [Required]
        [MaxLength(128)]
        [StringLength(128)]
        [Display(Name = "Updated by")]
        public string UpdatedBy { get; set; }

        public System.Collections.Generic.ICollection<DataSet> DataSets { get; set; }

        public DataSetType()
        {
            TypeId = System.Guid.NewGuid();
            Title = "";
            Sort = 0;
            IsEnabled = true;
            CreatedDate = System.DateTime.Now;
            CreatedBy = "";
            UpdatedDate = System.DateTime.Now;
            UpdatedBy = "";
            DataSets = new System.Collections.Generic.List<DataSet>();
        }
    }

}
// </auto-generated>
