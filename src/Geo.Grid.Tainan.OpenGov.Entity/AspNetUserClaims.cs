using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    public partial class AspNetUserClaims
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }
    }
}