using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    [Table("EngineeringLog")]
    public class EngineeringLog
    {
        public EngineeringLog()
        {
            CreatedDate = DateTime.Now;
            IsEnabled = true;
        }
        
        ///<summary>
        /// 工程標案匯入紀錄編號
        ///</summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [Key]
        public long LogId { get; set; }

        ///<summary>
        /// 原始資料,CSV檔案格式,不換行(;分隔)
        ///</summary>
        [Required]
        public string LogMessage { get; set; }

        ///<summary>
        /// 檔案名稱
        ///</summary>
        [Required]
        [MaxLength(100)]
        public string FileName { get; set; }

        ///<summary>
        /// 建立人員
        ///</summary>
        [MaxLength(128)]
        public string CreatedBy { get; set; }

        ///<summary>
        /// 建立日期
        ///</summary>
        [Required]
        public DateTime CreatedDate { get; set; }

        ///<summary>
        /// 是否啟用
        ///</summary>
        [Required]
        public bool IsEnabled { get; set; }
    }

}
