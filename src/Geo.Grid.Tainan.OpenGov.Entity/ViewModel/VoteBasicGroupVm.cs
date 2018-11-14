using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    /// <summary>
    /// 個資-群組
    /// </summary>
    public class VoteBasicGroupVm
    {
        /// <summary>
        /// key
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 個資
        /// </summary>
        public IEnumerable<VoteBasicVm> VoteBasicArray { get; set; }
    }

    /// <summary>
    /// 個資
    /// </summary>
    public class VoteBasicVm
    {
        /// <summary>
        /// key
        /// </summary>
        public Guid BasicId { get; set; }

        /// <summary>
        /// name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 是否需填寫文字
        /// </summary>
        public bool IsExplain { get; set; } = false;
                
    }
}
