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

    // The table 'group' is not usable by entity framework because it
    // does not have a primary key. It is listed here for completeness.
    [NotMapped]
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.34.1.0")]
    public class Group
    {
        [Column(@"GroupId", Order = 1, TypeName = "varchar")]
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Group ID")]
        public string GroupId { get; set; }

        [Column(@"GroupTitle", Order = 2, TypeName = "varchar")]
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Group title")]
        public string GroupTitle { get; set; }

        [Column(@"InfoId", Order = 3, TypeName = "varchar")]
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Info ID")]
        public string InfoId { get; set; }

        [Column(@"GroupDesc", Order = 4, TypeName = "nvarchar(max)")]
        [Display(Name = "Group desc")]
        public string GroupDesc { get; set; }

        [Column(@"GroupSort", Order = 5, TypeName = "varchar")]
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Group sort")]
        public string GroupSort { get; set; }

        [Column(@"IsEnable", Order = 6, TypeName = "varchar")]
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Is enable")]
        public string IsEnable { get; set; }

        [Column(@"EditDate", Order = 7, TypeName = "varchar")]
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Edit date")]
        public string EditDate { get; set; }

        [Column(@"Editer", Order = 8, TypeName = "varchar")]
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Editer")]
        public string Editer { get; set; }
    }

}
// </auto-generated>
