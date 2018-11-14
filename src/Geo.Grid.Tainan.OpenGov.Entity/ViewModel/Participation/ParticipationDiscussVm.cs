using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Participation
{

    public class ParticipationDiscussVm
    {
        public ParticipationDiscussVm()
        {
            TagIds = new List<Guid>();
            //ParticipationTags = new List<ParticipationTagVm>();
            //PushUserIds = new List<string>();
            //AspNetUser = new AspNetUserVm();
        }

        ///<summary>
        /// 討論區編號
        ///</summary>
        public Guid DiscussId { get; set; }

        ///<summary>
        /// 市政參與編號
        ///</summary>
        public Guid ParticipationId { get; set; }

        ///<summary>
        /// 使用者編號
        ///</summary>
        public string UserId { get; set; }

        /// <summary>
        /// 留言者-匿稱
        /// </summary>
        public string UserNickName { get; set; } = string.Empty;

        ///<summary>
        /// 上層討論區編號
        ///</summary>
        public Guid? UpperId { get; set; }

        ///<summary>
        /// 討論內容
        ///</summary>
        public string Message { get; set; }

        ///<summary>
        /// 置頂
        ///</summary>
        public bool IsTop { get; set; }

        /// <summary>
        /// 是否顯示
        /// </summary>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public int ReplyAmount { get; set; }

        public List<Guid> TagIds { get; set; }

        public IEnumerable<string> PushUserIds { get; set; }



        public List<AspNetUserVm> AspNetUsers { get; set; }
        public List<ParticipationVm> Participations { get; set; }
        public List<ParticipationTagVm> ParticipationTags { get; set; }
        
        public AspNetUserVm AspNetUser { get; set; }
    }
}
