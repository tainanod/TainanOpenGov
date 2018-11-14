using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Participation
{
    public class ParticipationHyperlinkVm
    {
        ///<summary>
        /// 連結網址編號
        ///</summary>
        public Guid HyperlinkId { get; set; }

        ///<summary>
        /// 連結名稱
        ///</summary>
        public string Title { get; set; }

        ///<summary>
        /// 連結說明
        ///</summary>
        public string Description { get; set; }

        ///<summary>
        /// 連結網址
        ///</summary>
        public string Url { get; set; }

        /// <summary>
        /// 是否已點擊
        /// </summary>
        public bool IsClick { get; set; } = false;


        /// <summary>
        /// 
        /// </summary>
        public List<ParticipationVm> Participations { get; set; }

    }
}
