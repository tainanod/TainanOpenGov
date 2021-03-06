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

    [Table("Vote", Schema = "dbo")]
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.34.1.0")]
    public class Vote
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(@"VoteId", Order = 1, TypeName = "uniqueidentifier")]
        [Index(@"PK_dbo.Vote", 1, IsUnique = true, IsClustered = true)]
        [Required]
        [Key]
        [Display(Name = "Vote ID")]
        public System.Guid VoteId { get; set; }

        ///<summary>
        /// 公民論壇編號
        ///</summary>
        [Column(@"ForumId", Order = 2, TypeName = "uniqueidentifier")]
        [Index(@"IX_ForumId", 1, IsUnique = false, IsClustered = false)]
        [Required]
        [Display(Name = "Forum ID")]
        public System.Guid ForumId { get; set; }

        ///<summary>
        /// 標題
        ///</summary>
        [Column(@"Title", Order = 3, TypeName = "nvarchar")]
        [Required]
        [MaxLength(255)]
        [StringLength(255)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        ///<summary>
        /// 說明
        ///</summary>
        [Column(@"Info", Order = 4, TypeName = "nvarchar(max)")]
        [Required]
        [Display(Name = "Info")]
        public string Info { get; set; }

        ///<summary>
        /// 投票開始日
        ///</summary>
        [Column(@"StartDate", Order = 5, TypeName = "date")]
        [Required]
        [Display(Name = "Start date")]
        public System.DateTime StartDate { get; set; }

        ///<summary>
        /// 投票結束日
        ///</summary>
        [Column(@"EndDate", Order = 6, TypeName = "date")]
        [Required]
        [Display(Name = "End date")]
        public System.DateTime EndDate { get; set; }

        ///<summary>
        /// 是否開放投票結果
        ///</summary>
        [Column(@"CanVote", Order = 7, TypeName = "bit")]
        [Required]
        [Display(Name = "Can vote")]
        public bool CanVote { get; set; }

        ///<summary>
        /// 至少需投幾項
        ///</summary>
        [Column(@"SelectNumber", Order = 8, TypeName = "int")]
        [Required]
        [Display(Name = "Select number")]
        public int SelectNumber { get; set; }

        ///<summary>
        /// 是否開放
        ///</summary>
        [Column(@"IsPublish", Order = 9, TypeName = "bit")]
        [Required]
        [Display(Name = "Is publish")]
        public bool IsPublish { get; set; }

        ///<summary>
        /// 是否已刪除
        ///</summary>
        [Column(@"IsEnabled", Order = 10, TypeName = "bit")]
        [Required]
        [Display(Name = "Is enabled")]
        public bool IsEnabled { get; set; }

        ///<summary>
        /// 建立時間
        ///</summary>
        [Column(@"CreatedDate", Order = 11, TypeName = "datetime")]
        [Required]
        [Display(Name = "Created date")]
        public System.DateTime CreatedDate { get; set; }

        ///<summary>
        /// 建立人員
        ///</summary>
        [Column(@"CreatedBy", Order = 12, TypeName = "nvarchar")]
        [Required]
        [MaxLength(128)]
        [StringLength(128)]
        [Display(Name = "Created by")]
        public string CreatedBy { get; set; }

        ///<summary>
        /// 修改時間
        ///</summary>
        [Column(@"UpdatedDate", Order = 13, TypeName = "datetime")]
        [Required]
        [Display(Name = "Updated date")]
        public System.DateTime UpdatedDate { get; set; }

        ///<summary>
        /// 修改人員
        ///</summary>
        [Column(@"UpdatedBy", Order = 14, TypeName = "nvarchar")]
        [Required]
        [MaxLength(128)]
        [StringLength(128)]
        [Display(Name = "Updated by")]
        public string UpdatedBy { get; set; }

        ///<summary>
        /// 驗證方式
        ///</summary>
        [Column(@"VerifyType", Order = 15, TypeName = "int")]
        [Required]
        [Display(Name = "Verify type")]
        public int VerifyType { get; set; }

        public System.Collections.Generic.ICollection<VoteBasicGroup> VoteBasicGroups { get; set; }
        public System.Collections.Generic.ICollection<VoteOption> VoteOptions { get; set; }


        [ForeignKey("ForumId")] public Forum Forum { get; set; }

        public Vote()
        {
            VoteId = System.Guid.NewGuid();
            Title = "";
            Info = "";
            StartDate = System.DateTime.Now;
            EndDate = System.DateTime.Now;
            CanVote = false;
            SelectNumber = 2;
            IsPublish = true;
            IsEnabled = false;
            CreatedDate = System.DateTime.Now;
            CreatedBy = "";
            UpdatedDate = System.DateTime.Now;
            UpdatedBy = "";
            VerifyType = 0;
            VoteOptions = new System.Collections.Generic.List<VoteOption>();
            VoteBasicGroups = new System.Collections.Generic.List<VoteBasicGroup>();
        }
    }

}
// </auto-generated>
