using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    [Table("MdQuestNecessaryRel")]
    public class MdQuestNecessaryRel
    {
        /// <summary>
        /// 選項GUID
        /// </summary>
        [Key]
        public Guid SetId { get; set; }

        /// <summary>
        /// 目標類型 1:題組 2:題目
        /// </summary>
        public int TargetType { get; set; }

        /// <summary>
        /// 目標ID
        /// </summary>
        public Guid TargetId { get; set; }

        public virtual MdQuestSetItem MdQuestSetItem { get; set; }
    }
}
