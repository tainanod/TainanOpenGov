using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Engineering
{
    public class EngineeringQueryVm
    {
        #region 進階搜尋

        ///<summary>
        /// 鄉鎮
        ///</summary>
        public string Town { get; set; }

        ///<summary>
        /// 標案類別
        ///</summary>
        public string Category { get; set; }

        ///<summary>
        /// 工程進度
        ///</summary>
        public string ProgressText { get; set; }

        ///<summary>
        /// 狀態
        ///</summary>
        public string Status { get; set; }

        ///<summary>
        /// 決標金額(最小值)
        ///</summary>
        public decimal? AmountMin { get; set; }
        
        ///<summary>
        /// 決標金額(最大值)
        ///</summary>
        public decimal? AmountMax { get; set; }

        ///<summary>
        /// 開工日期(最大值)
        ///</summary>
        public DateTime? StartDate { get; set; }

        ///<summary>
        /// 開工日期(最小值)
        ///</summary>
        public DateTime? EndDate { get; set; }

        #endregion

        #region 關鍵字

        /// <summary>
        /// 關鍵字
        /// </summary>
        public string Keyword { get; set; }

        #endregion

        #region 頁面資訊

        /// <summary>
        /// 目前page
        /// </summary>
        private int _currentPage = 1;

        /// <summary>
        /// 目前為第幾頁
        /// </summary>
        public int CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                if (value < 1)
                {
                    value = 1;
                }
                _currentPage = value;
            }
        }

        /// <summary>
        /// 每頁幾筆
        /// </summary>
        public int PageSize { get; set; } = 10;

        #endregion
    }
}
