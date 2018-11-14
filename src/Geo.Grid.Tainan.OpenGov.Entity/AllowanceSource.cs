using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    [Table("AllowanceSource")]
    public class AllowanceSource: AuditableEntityNew
    {
        /// <summary>
        /// 建構子
        /// </summary>
        public AllowanceSource()
        {
            //Allowances = new List<Allowance>();
        }
        
        ///<summary>
        /// 公益臺南-資料集來源管理
        ///</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        [Key]
        public Guid SourceId { get; set; }

        ///<summary>
        /// 資料集名稱
        ///</summary>
        [Required]
        [MaxLength(500)]
        [DisplayName("資料集名稱")]
        public string Name { get; set; }

        ///<summary>
        /// 管理組織
        ///</summary>
        [Required]
        [MaxLength(500)]
        [DisplayName("管理組織")]
        public string Organization { get; set; }

        ///<summary>
        /// 網頁連結
        ///</summary>
        [Required]
        [MaxLength(500)]
        [DisplayName("網頁連結")]
        public string WebSite { get; set; }

        ///<summary>
        /// API
        ///</summary>
        [Required]
        [MaxLength(500)]
        [DisplayName("API")]
        public string ApiUrl { get; set; }
        
        ///<summary>
        /// 資料來源的唯一編碼，由ApiUrl取得
        ///</summary>
        [Required]
        public Guid ResourceId { get; set; }

        /// <summary>
        /// 補助 資料
        /// </summary>
        public ICollection<Allowance> Allowances { get; set; }
    }
}
