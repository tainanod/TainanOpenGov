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

    [Table("PHOTO", Schema = "dbo")]
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.34.1.0")]
    public class Photo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(@"PHOTO_ID", Order = 1, TypeName = "uniqueidentifier")]
        [Index(@"PK_dbo.PHOTO", 1, IsUnique = true, IsClustered = true)]
        [Required]
        [Key]
        [Display(Name = "Photo ID")]
        public System.Guid PhotoId { get; set; }

        [Column(@"SIZE", Order = 2, TypeName = "bigint")]
        [Required]
        [Display(Name = "Size")]
        public long Size { get; set; }

        [Column(@"FILE_NAME", Order = 3, TypeName = "nvarchar")]
        [Required]
        [MaxLength(255)]
        [StringLength(255)]
        [Display(Name = "File name")]
        public string FileName { get; set; }

        [Column(@"FILE_TYPE", Order = 4, TypeName = "nvarchar")]
        [Required]
        [MaxLength(255)]
        [StringLength(255)]
        [Display(Name = "File type")]
        public string FileType { get; set; }

        [Column(@"ALT", Order = 5, TypeName = "nvarchar")]
        [MaxLength(255)]
        [StringLength(255)]
        [Display(Name = "Alt")]
        public string Alt { get; set; }

        [Column(@"IS_ENABLED", Order = 6, TypeName = "bit")]
        [Required]
        [Display(Name = "Is enabled")]
        public bool IsEnabled { get; set; }

        [Column(@"CREATED_BY", Order = 7, TypeName = "nvarchar")]
        [Required]
        [MaxLength(100)]
        [StringLength(100)]
        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        [Column(@"CREATED_DATE", Order = 8, TypeName = "datetime")]
        [Required]
        [Display(Name = "Created date")]
        public System.DateTime CreatedDate { get; set; }

        [Column(@"UPDATE_BY", Order = 9, TypeName = "nvarchar")]
        [Required]
        [MaxLength(100)]
        [StringLength(100)]
        [Display(Name = "Update by")]
        public string UpdateBy { get; set; }

        [Column(@"UPDATE_DATE", Order = 10, TypeName = "datetime")]
        [Required]
        [Display(Name = "Update date")]
        public System.DateTime UpdateDate { get; set; }

        public PhotoExt PhotoExt { get; set; }
        public System.Collections.Generic.ICollection<Forum> Fora { get; set; }
        public System.Collections.Generic.ICollection<Participation> Participations { get; set; }
        public System.Collections.Generic.ICollection<ShowCase> ShowCases { get; set; }

        public Photo()
        {
            PhotoId = System.Guid.NewGuid();
            Participations = new System.Collections.Generic.List<Participation>();
            Fora = new System.Collections.Generic.List<Forum>();
            ShowCases = new System.Collections.Generic.List<ShowCase>();
        }
    }

}
// </auto-generated>