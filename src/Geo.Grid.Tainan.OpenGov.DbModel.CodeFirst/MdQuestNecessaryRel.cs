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

    [Table("MdQuestNecessaryRel", Schema = "dbo")]
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.34.1.0")]
    public class MdQuestNecessaryRel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(@"SetId", Order = 1, TypeName = "uniqueidentifier")]
        [Index(@"PK_dbo.MdQuestNecessaryRel", 1, IsUnique = true, IsClustered = true)]
        [Index(@"IX_SetId", 1, IsUnique = false, IsClustered = false)]
        [Required]
        [Key]
        [Display(Name = "Set ID")]
        [ForeignKey("MdQuestSetItem")]
        public System.Guid SetId { get; set; }

        ///<summary>
        /// 目標類型 1:題組 2:題目
        ///</summary>
        [Column(@"TargetType", Order = 2, TypeName = "int")]
        [Required]
        [Display(Name = "Target type")]
        public int TargetType { get; set; }

        [Column(@"TargetId", Order = 3, TypeName = "uniqueidentifier")]
        [Required]
        [Display(Name = "Target ID")]
        public System.Guid TargetId { get; set; }


        [ForeignKey("SetId")] public MdQuestSetItem MdQuestSetItem { get; set; }
    }

}
// </auto-generated>
