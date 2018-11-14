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

    [Table("ViewQuestionExport", Schema = "dbo")]
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.34.1.0")]
    public class ViewQuestionExport
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(@"InfoId", Order = 1, TypeName = "uniqueidentifier")]
        [Required]
        [Key]
        [Display(Name = "Info ID")]
        public System.Guid InfoId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(@"MdFillUserId", Order = 2, TypeName = "uniqueidentifier")]
        [Required]
        [Key]
        [Display(Name = "Md fill user ID")]
        public System.Guid MdFillUserId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(@"UserName", Order = 3, TypeName = "nvarchar")]
        [Required]
        [MaxLength(256)]
        [StringLength(256)]
        [Key]
        [DataType(DataType.Text)]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(@"QstId", Order = 4, TypeName = "uniqueidentifier")]
        [Required]
        [Key]
        [Display(Name = "Qst ID")]
        public System.Guid QstId { get; set; }

        [Column(@"QstQuestion", Order = 5, TypeName = "nvarchar")]
        [MaxLength(300)]
        [StringLength(300)]
        [Display(Name = "Qst question")]
        public string QstQuestion { get; set; }

        [Column(@"SetDesc", Order = 6, TypeName = "nvarchar")]
        [MaxLength(100)]
        [StringLength(100)]
        [Display(Name = "Set desc")]
        public string SetDesc { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(@"SetId", Order = 7, TypeName = "uniqueidentifier")]
        [Required]
        [Key]
        [Display(Name = "Set ID")]
        public System.Guid SetId { get; set; }

        [Column(@"FillDesc", Order = 8, TypeName = "nvarchar")]
        [MaxLength(3000)]
        [StringLength(3000)]
        [Display(Name = "Fill desc")]
        public string FillDesc { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(@"FillScore", Order = 9, TypeName = "int")]
        [Required]
        [Key]
        [Display(Name = "Fill score")]
        public int FillScore { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(@"EditDate", Order = 10, TypeName = "datetime")]
        [Required]
        [Key]
        [Display(Name = "Edit date")]
        public System.DateTime EditDate { get; set; }
    }

}
// </auto-generated>