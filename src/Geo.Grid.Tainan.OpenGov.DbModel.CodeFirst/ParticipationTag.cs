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

    [Table("ParticipationTag", Schema = "dbo")]
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.34.1.0")]
    public class ParticipationTag
    {

        ///<summary>
        /// 標籤編號
        ///</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(@"TagId", Order = 1, TypeName = "uniqueidentifier")]
        [Index(@"PK_ParticipationTag", 1, IsUnique = true, IsClustered = true)]
        [Required]
        [Key]
        [Display(Name = "Tag ID")]
        public System.Guid TagId { get; set; }

        ///<summary>
        /// 標籤名稱
        ///</summary>
        [Column(@"TagName", Order = 2, TypeName = "nvarchar")]
        [Required]
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Tag name")]
        public string TagName { get; set; }

        ///<summary>
        /// 排序
        ///</summary>
        [Column(@"Sort", Order = 3, TypeName = "int")]
        [Required]
        [Display(Name = "Sort")]
        public int Sort { get; set; }

        ///<summary>
        /// 市政參與編號
        ///</summary>
        [Column(@"ParticipationId", Order = 4, TypeName = "uniqueidentifier")]
        [Required]
        [Display(Name = "Participation ID")]
        public System.Guid ParticipationId { get; set; }

        ///<summary>
        /// 是否顯示
        ///</summary>
        [Column(@"IsEnabled", Order = 5, TypeName = "bit")]
        [Required]
        [Display(Name = "Is enabled")]
        public bool IsEnabled { get; set; }

        ///<summary>
        /// 建立人員
        ///</summary>
        [Column(@"CreatedBy", Order = 6, TypeName = "nvarchar")]
        [Required]
        [MaxLength(128)]
        [StringLength(128)]
        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        ///<summary>
        /// 建立時間
        ///</summary>
        [Column(@"CreatedDate", Order = 7, TypeName = "datetime")]
        [Required]
        [Display(Name = "Created date")]
        public System.DateTime CreatedDate { get; set; }

        ///<summary>
        /// 修改人員
        ///</summary>
        [Column(@"UpdatedBy", Order = 8, TypeName = "nvarchar")]
        [Required]
        [MaxLength(128)]
        [StringLength(128)]
        [Display(Name = "Updated by")]
        public string UpdatedBy { get; set; }

        ///<summary>
        /// 修改時間
        ///</summary>
        [Column(@"UpdatedDate", Order = 9, TypeName = "datetime")]
        [Required]
        [Display(Name = "Updated date")]
        public System.DateTime UpdatedDate { get; set; }

        public System.Collections.Generic.ICollection<ParticipationDiscuss> ParticipationDiscusses { get; set; }


        [ForeignKey("ParticipationId")] public Participation Participation { get; set; }

        public ParticipationTag()
        {
            TagId = System.Guid.NewGuid();
            Sort = 0;
            IsEnabled = true;
            CreatedBy = "";
            CreatedDate = System.DateTime.Now;
            UpdatedBy = "";
            UpdatedDate = System.DateTime.Now;
            ParticipationDiscusses = new System.Collections.Generic.List<ParticipationDiscuss>();
        }
    }

}
// </auto-generated>