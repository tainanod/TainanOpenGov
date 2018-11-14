using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Admin
{
    public class RoleVm
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        [UIHint("SelectList")]
        [DisplayName("角色")]
        public string RoleName { get; set; }

        [UIHint("SelectList")]
        [DisplayName("局處")]
        public Guid? UnitId { get; set; }
    }
}
