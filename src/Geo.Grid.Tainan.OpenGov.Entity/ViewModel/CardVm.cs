using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel
{
    /// <summary>
    /// 九宮格卡片
    /// </summary>
    public class CardVm
    {
        /// <summary>
        /// 卡片ID
        /// </summary>
        public Guid CardId { get; set; }

        /// <summary>
        /// 單元名稱
        /// </summary>
        
        public string Title { get; set; }

        /// <summary>
        /// 卡片內容HtmlTag
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// 單元icon的PhotoId
        /// </summary>
        public Guid IconId { get; set; }

        /// <summary>
        /// 卡片顏色
        /// </summary>
        public CardColor Color { get; set; }


        /// <summary>
        /// 相關連結
        /// </summary>
        public string WebUrl { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        
        public int Sort { get; set; }

        /// <summary>
        /// 0：其它 1：政府資料 2：行程公開 3：警報告示 4：Open1999 5：管線圖資 6：野生台南 7：重大會議 8：公民論壇 9：市政提案
        /// </summary>
        public CardType Type { get; set; }


        /// <summary>
        /// 是否可以刪除
        /// </summary>
        public bool IsDelete { get; set; }

        ///<summary>
        /// 是否公開
        ///</summary>
        public bool IsPublish { get; set; }
    }
}
