namespace Geo.Grid.Tainan.OpenGov.EfCodeFirst
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OpenModel : DbContext
    {
        public OpenModel()
            : base("name=OpenModel")
        {
        }

        public virtual DbSet<ACTIVITY> ACTIVITY { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<DISCUSS> DISCUSS { get; set; }
        public virtual DbSet<DOCUMENT> DOCUMENT { get; set; }
        public virtual DbSet<DOCUMENT_EXT> DOCUMENT_EXT { get; set; }
        public virtual DbSet<FORUM> FORUM { get; set; }
        public virtual DbSet<HYPERLINK> HYPERLINK { get; set; }
        public virtual DbSet<PHOTO> PHOTO { get; set; }
        public virtual DbSet<PHOTO_EXT> PHOTO_EXT { get; set; }
        public virtual DbSet<TAG> TAG { get; set; }
        public virtual DbSet<YOUTUBE> YOUTUBE { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ACTIVITY>()
                .HasMany(e => e.DOCUMENT)
                .WithMany(e => e.ACTIVITY)
                .Map(m => m.ToTable("REL_ACTIVITY_DOCUMENT").MapLeftKey("ACTIVITY_ID").MapRightKey("DOCUMENT_ID"));

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
                .HasMany(e => e.DISCUSS)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.USER_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.DISCUSS1)
                .WithMany(e => e.AspNetUsers1)
                .Map(m => m.ToTable("REL_USER_PUSH").MapLeftKey("Id").MapRightKey("DISCUSS_ID"));

            modelBuilder.Entity<DISCUSS>()
                .HasMany(e => e.TAG)
                .WithMany(e => e.DISCUSS)
                .Map(m => m.ToTable("REL_DISCUSS_TAG").MapLeftKey("DISCUSS_ID").MapRightKey("TAG_ID"));

            modelBuilder.Entity<DOCUMENT>()
                .HasOptional(e => e.DOCUMENT_EXT)
                .WithRequired(e => e.DOCUMENT)
                .WillCascadeOnDelete();

            modelBuilder.Entity<DOCUMENT>()
                .HasMany(e => e.FORUM)
                .WithMany(e => e.DOCUMENT)
                .Map(m => m.ToTable("REL_FORUM_DOCUMENT").MapLeftKey("DOCUMENT_ID").MapRightKey("FORUM_ID"));

            modelBuilder.Entity<FORUM>()
                .HasMany(e => e.ACTIVITY)
                .WithRequired(e => e.FORUM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FORUM>()
                .HasMany(e => e.DISCUSS)
                .WithRequired(e => e.FORUM)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FORUM>()
                .HasMany(e => e.HYPERLINK)
                .WithMany(e => e.FORUM)
                .Map(m => m.ToTable("REL_FORUM_HYPERLINK").MapLeftKey("FORUM_ID").MapRightKey("HYPERLINK_ID"));

            modelBuilder.Entity<FORUM>()
                .HasMany(e => e.PHOTO)
                .WithMany(e => e.FORUM)
                .Map(m => m.ToTable("REL_FORUM_PHOTO").MapLeftKey("FORUM_ID").MapRightKey("PHOTO_ID"));

            modelBuilder.Entity<PHOTO>()
                .HasOptional(e => e.PHOTO_EXT)
                .WithRequired(e => e.PHOTO)
                .WillCascadeOnDelete();
        }
    }
}
