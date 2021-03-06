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

    [Table("VoteFillOption", Schema = "dbo")]
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.34.1.0")]
    public class VoteFillOption
    {

        ///<summary>
        /// 投票人
        ///</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(@"UserId", Order = 1, TypeName = "nvarchar")]
        [Index(@"PK_dbo.VoteFillOption", 1, IsUnique = true, IsClustered = true)]
        [Required]
        [MaxLength(128)]
        [StringLength(128)]
        [Key]
        [Display(Name = "User ID")]
        public string UserId { get; set; }

        ///<summary>
        /// 選項編號
        ///</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(@"OptionId", Order = 2, TypeName = "uniqueidentifier")]
        [Index(@"PK_dbo.VoteFillOption", 2, IsUnique = true, IsClustered = true)]
        [Required]
        [Key]
        [Display(Name = "Option ID")]
        public System.Guid OptionId { get; set; }

        ///<summary>
        /// 投票編號
        ///</summary>
        [Column(@"VoteId", Order = 3, TypeName = "uniqueidentifier")]
        [Required]
        [Display(Name = "Vote ID")]
        public System.Guid VoteId { get; set; }

        ///<summary>
        /// 投票時間
        ///</summary>
        [Column(@"CreatedDate", Order = 4, TypeName = "datetime")]
        [Required]
        [Display(Name = "Created date")]
        public System.DateTime CreatedDate { get; set; }

        public VoteFillOption()
        {
            CreatedDate = System.DateTime.Now;
        }
    }

}
// </auto-generated>
