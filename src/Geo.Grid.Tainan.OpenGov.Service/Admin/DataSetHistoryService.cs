using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geo.Grid.Tainan.OpenGov.Dal;
using Geo.Grid.Tainan.OpenGov.Dal.Interface;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Admin;
using Geo.Grid.Tainan.OpenGov.Entity.Dictionary;
using Geo.Grid.Tainan.OpenGov.Service.Common;
using Geo.Grid.Tainan.OpenGov.Service.Interface.Admin;

namespace Geo.Grid.Tainan.OpenGov.Service.Admin
{
    /// <summary>
    /// 野生台南-資料目錄-類別
    /// </summary>
    public class DataSetHistoryService : IDataSetHistoryService
    {
        private readonly IRepository<DataSetHistory> _dataSetHistory;
        private readonly OpenGovContext _db;

        /// <summary>
        /// service
        /// </summary>
        /// <param name="db">db</param>
        public DataSetHistoryService(IRepository<DataSetHistory> dataSetHistory)
        {
            this._dataSetHistory = dataSetHistory;
            this._db = dataSetHistory.Db;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="key">查詢</param>
        /// <returns></returns>
        public IQueryable<DataSetHistoryVm> GetList(SearchVm key)
        {
            var data = _db.DataSetHistory.OrderBy(x => x.Sort).ThenByDescending(x => x.CreatedDate)
                .Where(x => x.IsEnabled).Select(x => new DataSetHistoryVm()
                {
                    HistoryId = x.HistoryId,
                    Title = x.Title,
                    Info = x.Info,
                    Sort = x.Sort,
                    CreatedDate = x.CreatedDate
                });
            return data;
        }

        /// <summary>
        /// 新增-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        public Result<string> GetCreate(DataSetHistoryVm vmD)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var data = new DataSetHistory();
                int i = _db.DataSetHistory.Where(x => x.SetId == vmD.SetId).Count();
                vmD.Title = (i == 0 ? "v1.0" : "v" + (i + 1) + ".0");

                data.Title = vmD.Title;
                data.SetId = vmD.SetId;
                data.Contents = vmD.Contents;
                data.Info = vmD.Info;

                result.Success = _dataSetHistory.Create(data) > 0;
                result.ID = data.HistoryId;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 單筆
        /// </summary>
        /// <param name="id">編號</param>
        /// <returns></returns>
        public DataSetHistoryVm GetDetail(Guid id)
        {
            var data = _db.DataSetHistory.Where(x => x.HistoryId == id).Select(x => new DataSetHistoryVm()
            {
                HistoryId = x.HistoryId,
                SetId = x.SetId,
                Title = x.Title,
                Contents = x.Contents,
                Info = x.Info,
                Sort = x.Sort,
                IsEnabled = x.IsEnabled,
                CreatedDate = x.CreatedDate
            });
            return data.FirstOrDefault();
        }

        /// <summary>
        /// 編輯-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        public Result<string> GetEdit(DataSetHistoryVm vmD)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var data = _db.DataSetHistory.FirstOrDefault(x => x.HistoryId == vmD.HistoryId);
                if (data != null)
                {
                    //data.Title = vmD.Title;
                    data.Contents = vmD.Contents;
                    data.Info = vmD.Info;                    

                    result.Success = _dataSetHistory.Update(data) > 0;
                    result.ID = data.HistoryId;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 刪除-儲存
        /// </summary>
        /// <param name="id">參數</param>
        /// <returns></returns>
        public Result<string> GetRemove(Guid id)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var data = _db.DataSetHistory.FirstOrDefault(x => x.HistoryId == id);
                if (data != null)
                {
                    data.IsEnabled = false;

                    result.Success = _dataSetHistory.Update(data) > 0;
                    result.ID = id;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }
    }
}
