using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    public partial class AspNetRoles
    {
        public AspNetRoles()
        {
            AspNetUsers = new HashSet<AspNetUsers>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }
        public virtual ICollection<Menu> Menu { get; set; }
    }
}