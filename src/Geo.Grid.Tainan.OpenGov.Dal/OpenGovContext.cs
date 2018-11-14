using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading;
using Geo.Grid.Tainan.OpenGov.Dal.Migrations;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Interface;

namespace Geo.Grid.Tainan.OpenGov.Dal
{
    public class OpenGovContext : DbContext
    {
        public OpenGovContext()
            : base("name=OpenGovDB")
        {
        }

        public OpenGovContext(string cs)
            : base(cs)
        {
            Database.SetInitializer<OpenGovContext>(null);
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<OpenGovContext, Configuration>());
        }

        public virtual DbSet<Activity> Activity { get; set; }

        public virtual DbSet<CityTown> CityTown { get; set; }

        public virtual DbSet<Discuss> Discuss { get; set; }

        public virtual DbSet<Document> Document { get; set; }

        public virtual DbSet<DocumentExt> DocumentExt { get; set; }

        public virtual DbSet<Engineering> Engineerings { get; set; }

        public virtual DbSet<EngineeringLog> EngineeringLogs { get; set; }
        
        public virtual DbSet<Forum> Forum { get; set; }

        public virtual DbSet<Hyperlink> Hyperlink { get; set; }

        public virtual DbSet<Photo> Photo { get; set; }

        public virtual DbSet<PhotoExt> PhotoExt { get; set; }

        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }

        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }

        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }

        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }

        public virtual DbSet<Youtube> Youtube { get; set; }

        public virtual DbSet<MdFillQuest> MdFillQuest { get; set; }

        public virtual DbSet<MdQuestInfo> MdQuestInfo { get; set; }

        public virtual DbSet<MdQuestion> MdQuestion { get; set; }

        public virtual DbSet<MdQuestionGroup> MdQuestionGroup { get; set; }

        public virtual DbSet<MdQuestSetItem> MdQuestSetItem { get; set; }

        public virtual DbSet<MdQuestNecessaryRel> MdQuestNecessaryRel { get; set; }

        public virtual DbSet<Card> Card { get; set; }

        /// <summary>
        /// 問卷管理-第二階段驗證
        /// </summary>
        public virtual DbSet<MdCheck> MdCheck { get; set; }

        #region 投票管理

        /// <summary>
        /// 投票管理-主表
        /// </summary>
        public virtual DbSet<Vote> Vote { get; set; }

        /// <summary>
        /// 投票管理-選項
        /// </summary>
        public virtual DbSet<VoteOption> VoteOption { get; set; }

        /// <summary>
        /// 投票管理-投票(選項)
        /// </summary>
        public virtual DbSet<VoteFillOption> VoteFillOption { get; set; }

        /// <summary>
        /// 投票管理-個資-群組
        /// </summary>
        public virtual DbSet<VoteBasicGroup> VoteBasicGroup { get; set; }

        /// <summary>
        /// 投票管理-個資
        /// </summary>
        public virtual DbSet<VoteBasic> VoteBasic { get; set; }

        /// <summary>
        /// 投票管理-投票-基本資料
        /// </summary>
        public virtual DbSet<VoteFillBasic> VoteFillBasic { get; set; }

        /// <summary>
        /// 投票管理-有效投票
        /// </summary>
        public virtual DbSet<VoteCheck> VoteCheck { get; set; }

        #endregion

        #region 野生台南-資料目錄

        /// <summary>
        /// 野生台南-資料目錄
        /// </summary>
        public virtual DbSet<DataSet> DataSet { get; set; }

        /// <summary>
        /// 野生台南-資料目錄-類別
        /// </summary>
        public virtual DbSet<DataSetType> DataSetType { get; set; }

        /// <summary>
        /// 野生台南-資料目錄-業管單位
        /// </summary>
        public virtual DbSet<DataSetUnit> DataSetUnit { get; set; }

        /// <summary>
        /// 野生台南-資料目錄-格式
        /// </summary>
        public virtual DbSet<DataSetExtension> DataSetExtension { get; set; }

        /// <summary>
        /// 野生台南-資料目錄-歷程管理
        /// </summary>
        public virtual DbSet<DataSetHistory> DataSetHistory { get; set; }

        #endregion

        #region 野生台南-應用展示

        /// <summary>
        /// 野生台南-應用展示
        /// </summary>
        public virtual DbSet<ShowCase> ShowCase { get; set; }

        /// <summary>
        /// 野生台南-應用展示-連結
        /// </summary>
        public virtual DbSet<ShowCaseUrl> ShowCaseUrl { get; set; }

        #endregion

        /// <summary>
        /// 信件管理
        /// </summary>
        public virtual DbSet<WaitingMail> WaitingMail { get; set; }

        /// <summary>
        /// 論壇點擊用(new語法)
        /// </summary>
        public virtual DbSet<AnonymousClick> AnonymousClick { get; set; }

        /// <summary>
        /// 標籤管理
        /// </summary>
        public virtual DbSet<Tag> Tag { get; set; }

        /// <summary>
        /// 形象圖
        /// </summary>
        public virtual DbSet<Banner> Banner { get; set; }

        #region 公益台南
        /// <summary>
        /// 社福補助
        /// </summary>
        public virtual DbSet<Allowance> Allowance { get; set; }

        ///<summary>
        /// 公益臺南-資料集來源管理
        ///</summary>
        public virtual DbSet<AllowanceSource> AllowanceSources { get; set; }

        /// <summary>
        /// 區公所座標
        /// </summary>
        public virtual DbSet<DistrictOffice> DistrictOffice { get; set; }
        #endregion

        #region 市政參與

        /// <summary>
        /// 市政參與
        /// </summary>
        public DbSet<Participation> Participations { get; set; }

        /// <summary>
        /// 市政參與活動
        /// </summary>
        public DbSet<ParticipationActivity> ParticipationActivities { get; set; }

        /// <summary>
        /// 市政參與討論
        /// </summary>
        public DbSet<ParticipationDiscuss> ParticipationDiscusses { get; set; }

        /// <summary>
        /// 市政參與附件
        /// </summary>
        public DbSet<ParticipationDocument> ParticipationDocuments { get; set; }

        /// <summary>
        /// 市政參與 相關連結
        /// </summary>
        public DbSet<ParticipationHyperlink> ParticipationHyperlinks { get; set; }

        /// <summary>
        /// 市政參與 標籤
        /// </summary>
        public DbSet<ParticipationTag> ParticipationTags { get; set; }

        /// <summary>
        /// 點擊紀錄
        /// </summary>
        public DbSet<ParticipationAnonymousClick> ParticipationAnonymousClicks { get; set; }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Moved all Student related configuration to StudentEntityConfiguration class
            //modelBuilder.Configurations.Add(new OpenGovEntityConfiguration());

            modelBuilder.Entity<AnonymousClick>()
                .HasRequired(x => x.Forum)
                .WithMany(x => x.AnonymousClick)
                .HasForeignKey(x => x.ForumId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tag>()
                .HasRequired(x => x.Forum)
                .WithMany(x => x.Tag)
                .HasForeignKey(x => x.ForumId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MdQuestionGroup>()
                .HasRequired(a => a.MdQuestInfo)
                .WithMany(b => b.MdQuestionGroups)
                .HasForeignKey(c => c.InfoId)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<MdQuestion>()
                .HasRequired(a => a.MdQuestionGroup)
                .WithMany(b => b.MdQuestions)
                .HasForeignKey(c => c.GroupId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MdQuestSetItem>()
                .HasRequired(a => a.MdQuestion)
                .WithMany(b => b.MdQuestSetItems)
                .HasForeignKey(c => c.QstId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MdFillQuest>()
                .HasRequired(a => a.MdQuestion)
                .WithMany(b => b.MdFillQuests)
                .HasForeignKey(c => c.QstId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Activity>()
                .HasMany(e => e.Document)
                .WithMany(e => e.Activity)
                .Map(m => m.ToTable("REL_ACTIVITY_DOCUMENT").MapLeftKey("ACTIVITY_ID").MapRightKey("DOCUMENT_ID"));

            modelBuilder.Entity<Document>()
                .HasOptional(e => e.DocumentExt)
                .WithRequired(e => e.Document)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Document>()
                .HasMany(e => e.Forum)
                .WithMany(e => e.Document)
                .Map(m => m.ToTable("REL_FORUM_DOCUMENT").MapLeftKey("DOCUMENT_ID").MapRightKey("FORUM_ID"));

            modelBuilder.Entity<Forum>()
                .HasMany(e => e.Activity)
                .WithRequired(e => e.Forum)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Forum>()
                .HasMany(e => e.Discuss)
                .WithRequired(e => e.Forum)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Forum>()
                .HasMany(e => e.Hyperlink)
                .WithMany(e => e.Forum)
                .Map(m => m.ToTable("REL_FORUM_HYPERLINK").MapLeftKey("FORUM_ID").MapRightKey("HYPERLINK_ID"));

            modelBuilder.Entity<Forum>()
                .HasMany(e => e.Photo)
                .WithMany(e => e.Forum)
                .Map(m => m.ToTable("REL_FORUM_PHOTO").MapLeftKey("FORUM_ID").MapRightKey("PHOTO_ID"));

            modelBuilder.Entity<Forum>()
                .HasMany(e => e.MdQuestionInfo)
                .WithRequired(e => e.Forum)
                .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Forum>()
            //    .HasRequired(e => e.DataSetUnit)
            //    .WithMany(e => e.Forum)
            //    .HasForeignKey(e => e.DataSetUnitId)
            //    .WillCascadeOnDelete(false);

            modelBuilder.Entity<Forum>()
                .HasRequired(e => e.DataSetUnit)
                .WithMany(e => e.Forum)
                .HasForeignKey(e => e.DataSetUnitId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Photo>()
                .HasOptional(e => e.PhotoExt)
                .WithRequired(e => e.Photo)
                .WillCascadeOnDelete();

            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.Disucss)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.Menu)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("REL_AspNetRoles_Menu").MapLeftKey("RoleId").MapRightKey("MenuId"));

            modelBuilder.Entity<Discuss>()
                .HasMany(e => e.Tag)
                .WithMany(e => e.Discuss)
                .Map(m => m.ToTable("REL_DISCUSS_TAG").MapLeftKey("DiscussId").MapRightKey("TagId"));

            //modelBuilder.Entity<AspNetUsers>()
            //    .HasMany(e => e.Disucss)
            //    .WithMany(e => e.PushUser)
            //    .Map(m => m.ToTable("REL_USER_PUSH").MapLeftKey("Id").MapRightKey("DiscussId"));
            modelBuilder.Entity<Discuss>()
                .HasMany(e => e.PushUser)
                .WithMany(e => e.PushDisucss)
                .Map(m => m.ToTable("REL_USER_PUSH").MapLeftKey("DiscussId").MapRightKey("Id"));

            modelBuilder.Entity<MdQuestNecessaryRel>()
                .HasRequired(x => x.MdQuestSetItem)
                .WithOptional(x => x.MdQuestNecessaryRel)
                .WillCascadeOnDelete(false);

            #region 投票管理

            modelBuilder.Entity<Vote>()
                .HasMany(x => x.VoteBasicGroups)
                .WithMany(x => x.Votes)
                .Map(x =>
                {
                    x.ToTable("VoteRelBasicGroup");
                    x.MapLeftKey("VoteId");
                    x.MapRightKey("GroupId");
                });

            #endregion

            #region 野生台南-資料目錄

            //野生台南-資料目錄-格式
            modelBuilder.Entity<DataSet>()
                .HasMany(x => x.DataSetExtensionRel)
                .WithMany(x => x.DataSetRel)
                .Map(x =>
                {
                    x.ToTable("DataSetRelExtension");
                    x.MapLeftKey("SetId");
                    x.MapRightKey("ExtensionId");
                });
            #endregion

            #region 野生台南-應用展示

            //野生台南-應用展示-多圖
            modelBuilder.Entity<ShowCase>()
                .HasMany(x => x.PhotoRel)
                .WithMany(x => x.ShowCaseRel)
                .Map(x =>
                {
                    x.ToTable("ShowCaseRelPhoto");
                    x.MapLeftKey("CaseId");
                    x.MapRightKey("PhotoId");
                });

            //野生台南-應用展示-資料目錄
            modelBuilder.Entity<ShowCase>()
                .HasMany(x => x.DataSetRel)
                .WithMany(x => x.ShowCaseRel)
                .Map(x =>
                {
                    x.ToTable("ShowCaseRelDataSet");
                    x.MapLeftKey("CaseId");
                    x.MapRightKey("SetId");
                });

            #endregion

            #region 市政參與

            modelBuilder.Entity<ParticipationHyperlink>()
                .HasMany(t => t.Participations).WithMany(t => t.ParticipationHyperlinks).Map(m =>
                {
                    m.ToTable("RelParticipationHyperlink", "dbo");
                    m.MapLeftKey("HyperlinkId");
                    m.MapRightKey("ParticipationId");
                });

            modelBuilder.Entity<ParticipationDocument>()
                .HasMany(t => t.Participations).WithMany(t => t.ParticipationDocuments).Map(m =>
                {
                    m.ToTable("RelParticipationDocument", "dbo");
                    m.MapLeftKey("DocumentId");
                    m.MapRightKey("ParticipationId");
                });
            
            modelBuilder.Entity<ParticipationDiscuss>()
                .HasMany(t => t.AspNetUsers).WithMany(t => t.PushParticipationDiscuss).Map(m =>
                {
                    m.ToTable("RelPushParticipationDiscussUser", "dbo");
                    m.MapLeftKey("DiscussId");
                    m.MapRightKey("Id");
                });

            modelBuilder.Entity<ParticipationDiscuss>()
                .HasMany(t => t.ParticipationTags).WithMany(t => t.ParticipationDiscusses).Map(m =>
                {
                    m.ToTable("RelParticipationDiscussTag", "dbo");
                    m.MapLeftKey("DiscussId");
                    m.MapRightKey("TagId");
                });

            modelBuilder.Entity<ParticipationDiscuss>()
                .HasMany(t => t.Participations).WithMany(t => t.ParticipationDiscusses).Map(m =>
                {
                    m.ToTable("RelParticipationDiscuss", "dbo");
                    m.MapLeftKey("DiscussId");
                    m.MapRightKey("ParticipationId");
                });

            modelBuilder.Entity<ParticipationActivity>()
                .HasMany(t => t.ParticipationDocuments).WithMany(t => t.ParticipationActivities).Map(m =>
                {
                    m.ToTable("RelParticipationActivity", "dbo");
                    m.MapLeftKey("ActivityId");
                    m.MapRightKey("DocumentId");
                });

            modelBuilder.Entity<Participation>().
                HasMany(t => t.Photos).WithMany(t => t.Participations).Map(m =>
                {
                    m.ToTable("RelParticipationPhoto", "dbo");
                    m.MapLeftKey("ParticipationId");
                    m.MapRightKey("PHOTO_ID");
                });

            #endregion

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            var modifiedEntries = ChangeTracker.Entries()
              .Where(x => x.Entity is IAuditableEntity
                  && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                var entity = entry.Entity as IAuditableEntity;
                if (entity == null)
                {
                    continue;
                }

                var identityName = Thread.CurrentPrincipal.Identity.Name;
                var now = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedBy = string.IsNullOrEmpty(identityName) ? "service@geo.com.tw" : identityName;
                    entity.CreatedDate = now;
                    //entity.IsEnabled = true;
                }
                else
                {
                    base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                    base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                }

                entity.UpdatedBy = string.IsNullOrEmpty(identityName) ? "service@geo.com.tw" : identityName;
                entity.UpdatedDate = now;
            }
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} 驗證失敗！", failure.Entry.Entity.GetType());

                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }
                // Add the original exception as the innerException
                throw new DbEntityValidationException("發生錯誤 - 原因如下：" + sb.ToString(), ex);
            }
        }
    }
}