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

    [Table("CityTown", Schema = "dbo")]
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.34.1.0")]
    public class CityTown
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(@"CityTownId", Order = 1, TypeName = "uniqueidentifier")]
        [Index(@"PK_dbo.CityTown", 1, IsUnique = true, IsClustered = true)]
        [Required]
        [Key]
        [Display(Name = "City town ID")]
        public System.Guid CityTownId { get; set; }

        [Column(@"TownSeq", Order = 2, TypeName = "int")]
        [Required]
        [Display(Name = "Town seq")]
        public int TownSeq { get; set; }

        [Column(@"TownName", Order = 3, TypeName = "nvarchar")]
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Town name")]
        public string TownName { get; set; }

        [Column(@"CitySeq", Order = 4, TypeName = "int")]
        [Required]
        [Display(Name = "City seq")]
        public int CitySeq { get; set; }

        [Column(@"CityName", Order = 5, TypeName = "nvarchar")]
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "City name")]
        public string CityName { get; set; }

        [Column(@"IsEnable", Order = 6, TypeName = "bit")]
        [Required]
        [Display(Name = "Is enable")]
        public bool IsEnable { get; set; }

        public CityTown()
        {
            IsEnable = false;
        }
    }

}
// </auto-generated>