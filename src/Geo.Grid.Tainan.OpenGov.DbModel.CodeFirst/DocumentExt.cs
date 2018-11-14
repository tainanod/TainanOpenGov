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

    [Table("DOCUMENT_EXT", Schema = "dbo")]
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.34.1.0")]
    public class DocumentExt
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(@"DOCUMENT_ID", Order = 1, TypeName = "uniqueidentifier")]
        [Index(@"PK_dbo.DOCUMENT_EXT", 1, IsUnique = true, IsClustered = true)]
        [Index(@"IX_DOCUMENT_ID", 1, IsUnique = false, IsClustered = false)]
        [Required]
        [Key]
        [Display(Name = "Document ID")]
        [ForeignKey("Document")]
        public System.Guid DocumentId { get; set; }

        [Column(@"ORIGINAL", Order = 2, TypeName = "varbinary(max)")]
        [Required]
        [Display(Name = "Original")]
        public byte[] Original { get; set; }


        [ForeignKey("DocumentId")] public Document Document { get; set; }
    }

}
// </auto-generated>