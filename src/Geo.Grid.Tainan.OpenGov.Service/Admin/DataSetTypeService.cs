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
    public class DataSetTypeService : IDataSetTypeService
    {
        private readonly IRepository<DataSetType> _dataSetType;
        private readonly OpenGovContext _db;

        /// <summary>
        /// service
        /// </summary>
        /// <param name="db">db</param>
        public DataSetTypeService(IRepository<DataSetType> dataSetType)
        {
            this._dataSetType = dataSetType;
            this._db = dataSetType.Db;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="key">查詢</param>
        /// <returns></returns>
        public IQueryable<DataSetTypeVm> GetList(SearchVm key)
        {
            var data = _db.DataSetType.OrderBy(x => x.Sort).ThenByDescending(x => x.CreatedDate)
                .Where(x => x.IsEnabled).Select(x => new DataSetTypeVm()
                {
                    TypeId = x.TypeId,
                    Title = x.Title,
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
        public Result<string> GetCreate(DataSetTypeVm vmD)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                int maxSort = _db.DataSetType.Max(x => x.Sort) + 1;

                var data = new DataSetType();
                data.Title = vmD.Title;
                data.Sort = maxSort;

                result.Success = _dataSetType.Create(data) > 0;
                result.ID = data.TypeId;
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
        public DataSetTypeVm GetDetail(Guid id)
        {
            var data = _db.DataSetType.Where(x => x.TypeId == id).Select(x => new DataSetTypeVm()
            {
                TypeId = x.TypeId,
                Title = x.Title,
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
        public Result<string> GetEdit(DataSetTypeVm vmD)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var data = _db.DataSetType.FirstOrDefault(x => x.TypeId == vmD.TypeId);
                if (data != null)
                {
                    data.Title = vmD.Title;

                    result.Success = _dataSetType.Update(data) > 0;
                    result.ID = data.TypeId;
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
                var data = _db.DataSetType.FirstOrDefault(x => x.TypeId == id);
                if (data != null)
                {
                    data.IsEnabled = false;

                    result.Success = _dataSetType.Update(data) > 0;
                    result.ID = id;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 排序-升
        /// </summary>
        /// <param name="id">tagId</param>
        /// <returns></returns>
        public Result<string> GetSortUp(Guid id)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var from = _db.DataSetType.Find(id);
                if (from != null)
                {
                    var query = _db.DataSetType.Where(x => x.Sort < from.Sort);

                    var to = query.OrderByDescending(x => x.Sort).FirstOrDefault();
                    if (to != null)
                    {
                        var tmp = from.Sort;
                        from.Sort = to.Sort;
                        to.Sort = tmp;
                        _db.SaveChanges();
                        result.Success = true;
                    }
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 排序降
        /// </summary>
        /// <param name="id">tagId</param>
        /// <returns></returns>
        public Result<string> GetSortDown(Guid id)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var from = _db.DataSetType.Find(id);
                if (from != null)
                {
                    var query = _db.DataSetType.Where(x => x.Sort > from.Sort);
                    var to = query.OrderBy(x => x.Sort).FirstOrDefault();
                    if (to != null)
                    {
                        var tmp = from.Sort;
                        from.Sort = to.Sort;
                        to.Sort = tmp;
                        _db.SaveChanges();
                        result.Success = true;
                    }
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
