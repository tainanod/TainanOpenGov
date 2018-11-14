using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    /// <summary>
    /// 系統管理後台選單
    /// </summary>
    [Table("Menu")]
    public class Menu : AuditableEntityNew
    {
        public Menu()
        {
            AspNetRoles = new HashSet<AspNetRoles>();
        }
        /// <summary>
        /// 選單編號
        /// </summary>
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("MenuId")]
        public Guid MenuId { get; set; }

        /// <summary>
        /// 選單名稱
        /// </summary>
        [Required]
        [Column("Name")]
        public string Name { get; set; }

        /// <summary>
        /// 選單指向的Controller
        /// </summary>
        [Required]
        [Column("Controller")]
        public string Controller { get; set; }

        /// <summary>
        /// 選單所指定的Action
        /// </summary>
        [Required]
        [Column("Action")]
        public string Action { get; set; }

        /// <summary>
        /// 選單所屬的Area
        /// </summary>
        [Column("Area")]
        public string Area { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        [Column("Sort")]
        public int Sort { get; set; }

        public virtual ICollection<AspNetRoles> AspNetRoles { get; set; }
    }
}