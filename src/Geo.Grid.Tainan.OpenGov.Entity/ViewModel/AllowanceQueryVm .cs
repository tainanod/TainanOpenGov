using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Geo.Grid.Tainan.OpenGov.Entity
{
    public class AllowanceQueryVm
    {
        #region 進階搜尋

        ///<summary>
        /// 請問您的戶籍地位於臺南市嗎 (未設籍臺南市、中西區、東區、安平區、..)
        ///</summary>
        public string LiveIn { get; set; }

        ///<summary>
        /// 請問是否在臺南市居住超過183天? (是、否)
        ///</summary>
        public string IsLiveDays { get; set; }

        ///<summary>
        /// 您的年齡是?
        ///</summary>
        public int? Age { get; set; }

        /// <summary>
        /// 您是否有子女? (是: True、否: False)
        /// </summary>
        public bool? AnyChildren { get; set; }

        /// <summary>
        /// 您共養育幾名子女?  (1名: False 、 超過1名: True)
        /// </summary>
        public bool? IsChildren { get; set; }

        /// <summary>
        /// (問題1)其年齡為?
        /// </summary>
        public int? ChildrenAge { get; set; }

        ///<summary>
        /// (問題2)其年齡介於幾歲? (最小值)
        ///</summary>
        public int? AgeMin { get; set; }
        
        ///<summary>
        /// (問題2)其年齡介於幾歲? (最大值)
        ///</summary>
        public int? AgeMax { get; set; }

        ///<summary>
        /// 請問您是否具備下列特殊身分條件? (無、低收入戶、中低收入戶)
        /// !important 日後改名為 Identity
        ///</summary>
        public List<string> Identity1 { get; set; }

        ///<summary>
        /// 請問您是否具備下列其他身分條件? (無、經濟狀況不佳致生活陷於困境者、急難事由生活陷困者)
        /// !important 日後改名為 Others
        ///</summary>
        public List<string> Identity2 { get; set; }

        ///<summary>
        /// 請問您全家每人每月收入約多少?(元)
        ///</summary>
        public int? IncomeValue { get; set; }

        ///<summary>
        /// 請問您的動產金額約多少？(元)
        ///</summary>
        public int? MovableValue { get; set; }

        ///<summary>
        /// 請問您全家不動產總值約多少?(元)
        ///</summary>
        public int? ImmovableValue { get; set; }

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
