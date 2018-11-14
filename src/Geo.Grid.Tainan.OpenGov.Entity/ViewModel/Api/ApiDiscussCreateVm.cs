using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api
{
    public class ApiDiscussCreateVm
    {
        /// <summary>
        /// 論壇編號
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 使用者編號
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 使用者Email
        /// </summary>
        public string UserEmail { get; set; }
        /// <summary>
        /// 發佈日期
        /// </summary>
        public DateTime PublishDate { get; set; }
        /// <summary>
        /// 留言訊息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 標籤編號
        /// </summary>
        public List<Guid> TagIds { get; set; }
        /// <summary>
        /// Jwt Token
        /// </summary>
        public string Token { get; set; }
    }
}
