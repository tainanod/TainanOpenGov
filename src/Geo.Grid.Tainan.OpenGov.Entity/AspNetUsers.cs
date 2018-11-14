using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            Disucss = new HashSet<Discuss>();
            AspNetRoles = new HashSet<AspNetRoles>();
            PushDisucss = new HashSet<Discuss>();
        }

        [Key]
        public string Id { get; set; }

        [StringLength(256)]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public string NickName { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        [StringLength(256)]
        public string UserName { get; set; }

        public Guid? DataSetUnitId { get; set; }

        public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }

        public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }

        public virtual ICollection<Discuss> Disucss { get; set; }

        public virtual ICollection<AspNetRoles> AspNetRoles { get; set; }

        public virtual ICollection<Discuss> PushDisucss { get; set; }

        public virtual ICollection<ParticipationDiscuss> PushParticipationDiscuss { get; set; }
        public virtual ICollection<ParticipationDiscuss> ParticipationDiscuss { get; set; }

    }
}