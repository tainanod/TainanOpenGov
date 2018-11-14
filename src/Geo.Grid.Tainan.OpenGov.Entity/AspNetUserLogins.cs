using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    public partial class AspNetUserLogins
    {
        [Key]
        [Column(Order = 0)]
        public string LoginProvider { get; set; }

        [Key]
        [Column(Order = 1)]
        public string ProviderKey { get; set; }

        [Key]
        [Column(Order = 2)]
        public string UserId { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }
    }
}