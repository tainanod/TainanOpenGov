using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Geo.Grid.Common.Models;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Engineering;
using GeoJSON.Net.Feature;

namespace Geo.Grid.Tainan.OpenGov.Service.Interface
{
    public interface IEngineeringService
    {
        /// <summary>
        /// 更新 Engineering
        /// </summary>
        /// <param name="csvMessage"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        int UpgradeEngineering(string fileName, string csvMessage, string userId);

        /// <summary>
        /// 關鍵字搜尋
        /// </summary>
        /// <param name="queryVm">搜尋條件</param>
        /// <returns>工程資料</returns>
        List<EngineeringVm> Query(EngineeringQueryVm queryVm);

        /// <summary>
        /// 進階搜尋
        /// </summary>
        /// <param name="queryVm">搜尋條件</param>
        /// <returns>工程資料</returns>
        List<EngineeringVm> AdvQuery(EngineeringQueryVm queryVm);

        /// <summary>
        /// 將搜尋結果轉換成GeoJSON 
        /// </summary>
        /// <param name="queryVm"></param>
        /// <param name="advQuery"></param>
        /// <returns></returns>
        FeatureCollection GetGeoJson(EngineeringQueryVm queryVm, bool advQuery);

        /// <summary>
        /// 取得 行政區
        /// </summary>
        /// <returns></returns>
        List<CodeName> GetTown();

        /// <summary>
        /// 取得 工程類別
        /// </summary>
        /// <returns></returns>
        List<CodeName> GetCategory();
        
        /// <summary>
        /// 取得 工程進度
        /// </summary>
        /// <returns></returns>
        List<CodeName> GetProgressText();

        ///// <summary>
        ///// 取得 工程狀態
        ///// </summary>
        ///// <returns></returns>
        //List<CodeName> GetStatus();
        
        /// <summary>
        /// 取得 工程明細資料
        /// </summary>
        /// <returns></returns>
        EngineeringDetailVm Detail(string governmentAenciesCode, string code);

        /// <summary>
        /// 取得 清單
        /// </summary>
        /// <returns></returns>
        IQueryable<EngineeringLogVm> GetEngineeringLogs();

        /// <summary>
        /// 刪除 EngineeringLog
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Grid.Common.Models.Result<string> Remove(long id);

        /// <summary>
        /// 下載原始的 CSV資料
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        EngineeringLogVm DownloadCsv(int logId);

        /// <summary>
        /// 下載 市政監督 工程標案
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        byte[] DownloadEngineering();
    }
}
