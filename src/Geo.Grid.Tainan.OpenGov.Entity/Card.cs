using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geo.Grid.Tainan.OpenGov.Entity.Enumeration;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    /// <summary>
    /// 九宮格卡片
    /// </summary>
    [Table("Card")]
    public class Card : AuditableEntityNew
    {
        public Card()
        {
            this.CardId = Guid.NewGuid();
            this.IconId = new Guid("696993C5-85D1-E611-80B6-F4CE4686931A");
            this.IsDelete = true;
            this.IsPublish = false;
        }
        /// <summary>
        /// 卡片ID
        /// </summary>
        [Key]
        public Guid CardId { get; set; }

        /// <summary>
        /// 單元名稱
        /// </summary>
        [MaxLength(150)]
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// 卡片內容HtmlTag
        /// </summary>
        public string Contents { get; set; }

        /// <summary>
        /// 單元icon的PhotoId
        /// </summary>
        [Required]
        public Guid IconId { get; set; }

        /// <summary>
        /// 卡片顏色
        /// </summary>
        [Required]
        public CardColor Color { get; set; }


        /// <summary>
        /// 相關連結
        /// </summary>
        [MaxLength(500)]
        public string WebUrl { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        public int Sort { get; set; }

        /// <summary>
        /// 0：其它 1：政府資料 2：行程公開 3：警報告示 4：Open1999 5：管線圖資 6：野生台南 7：重大會議 8：公民論壇 9：市政提案
        /// </summary>
        public CardType Type { get; set; }

        /// <summary>
        /// 是否可以刪除
        /// </summary>
        [Required]
        public bool IsDelete { get; set; }

        ///<summary>
        /// 是否公開
        ///</summary>
        [Required]
        public bool IsPublish { get; set; }
    }
}
