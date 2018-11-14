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
    public class DataSetService : IDataSetService
    {
        private readonly IRepository<DataSet> _dataSet;
        private readonly OpenGovContext _db;

        /// <summary>
        /// service
        /// </summary>
        /// <param name="db">db</param>
        public DataSetService(IRepository<DataSet> dataSet)
        {
            this._dataSet = dataSet;
            this._db = dataSet.Db;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="key">查詢</param>
        /// <returns></returns>
        public IQueryable<DataSetVm> GetList(SearchVm key)
        {
            var data = _db.DataSet.OrderBy(x => x.Sort).ThenByDescending(x => x.CreatedDate)
                .Where(x => !x.IsDeleted).Select(x => new DataSetVm()
                {
                    SetId = x.SetId,
                    TypeId = x.TypeId,
                    TypeName = x.DataSetType.Title,
                    Title = x.Title,
                    UnitId = x.UnitId,
                    UnitName = x.DataSetUnit.Title,
                    ContactName = x.ContactName,
                    ContactTel = x.ContactTel,
                    WebUrl = x.WebUrl,
                    ContactEmail = x.ContactEmail,
                    Sort = x.Sort,
                    IsEnabled = x.IsEnabled,
                    CreatedDate = x.CreatedDate
                });

            if (!string.IsNullOrEmpty(key.KeyWord))
            {
                data = data.Where(x => x.Title.Contains(key.KeyWord));
            }

            return data;
        }

        /// <summary>
        /// 新增-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        public Result<string> GetCreate(DataSetVm vmD)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var data = new DataSet();
                data.TypeId = vmD.TypeId;
                data.Title = vmD.Title;
                data.UnitId = vmD.UnitId;
                data.ContactName = vmD.ContactName;
                data.ContactTel = vmD.ContactTel;
                data.WebUrl = vmD.WebUrl;
                data.ContactEmail = vmD.ContactEmail;
                data.Contents = vmD.Contents;
                data.Info = vmD.Info;
                data.IsEnabled = vmD.IsEnabled;

                if (vmD.ExtensionList != null)
                {
                    var extension = _db.DataSetExtension.Where(x => vmD.ExtensionList.Contains(x.ExtensionId)).ToList();
                    data.DataSetExtensionRel = extension;
                }

                result.Success = _dataSet.Create(data) > 0;
                result.ID = data.SetId;
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
        public DataSetVm GetDetail(Guid id)
        {
            var data = _db.DataSet.Where(x => x.SetId == id).Select(x => new DataSetVm()
            {
                SetId = x.SetId,
                TypeId = x.TypeId,
                Title = x.Title,
                UnitId = x.UnitId,
                ContactName = x.ContactName,
                ContactTel = x.ContactTel,
                WebUrl = x.WebUrl,
                ContactEmail = x.ContactEmail,
                Contents = x.Contents,
                Info = x.Info,
                Sort = x.Sort,
                IsEnabled = x.IsEnabled,
                CreatedDate = x.CreatedDate,
                ExtensionList = x.DataSetExtensionRel.Select(c => c.ExtensionId)
            });
            return data.FirstOrDefault();
        }

        /// <summary>
        /// 編輯-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        public Result<string> GetEdit(DataSetVm vmD)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var data = _db.DataSet.FirstOrDefault(x => x.SetId == vmD.SetId);
                if (data != null)
                {
                    data.DataSetExtensionRel.Clear();

                    data.TypeId = vmD.TypeId;
                    data.Title = vmD.Title;
                    data.UnitId = vmD.UnitId;
                    data.ContactName = vmD.ContactName;
                    data.ContactTel = vmD.ContactTel;
                    data.WebUrl = vmD.WebUrl;
                    data.ContactEmail = vmD.ContactEmail;
                    data.Contents = vmD.Contents;
                    data.Info = vmD.Info;
                    data.IsEnabled = vmD.IsEnabled;

                    if (vmD.ExtensionList != null)
                    {
                        var extension = _db.DataSetExtension.Where(x => vmD.ExtensionList.Contains(x.ExtensionId)).ToList();
                        data.DataSetExtensionRel = extension;
                    }

                    result.Success = _dataSet.Update(data) > 0;
                    result.ID = data.SetId;
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
                var data = _db.DataSet.FirstOrDefault(x => x.SetId == id);
                if (data != null)
                {
                    data.IsDeleted = true;

                    result.Success = _dataSet.Update(data) > 0;
                    result.ID = id;
                }
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        #region 共用

        /// <summary>
        /// 類別
        /// </summary>
        /// <returns></returns>
        public IQueryable<GuidNameVm> GetTypeList()
        {
            var data = _db.DataSetType.OrderBy(x => x.Sort).ThenByDescending(x => x.CreatedDate)
                .Where(x => x.IsEnabled).Select(x => new GuidNameVm()
                {
                    PageGuid = x.TypeId,
                    PageName = x.Title
                });
            return data;
        }

        /// <summary>
        /// 資料類型
        /// </summary>
        /// <returns></returns>
        public IQueryable<GuidNameVm> GetExtensionList()
        {
            var data = _db.DataSetExtension.OrderBy(x => x.Sort).Where(x => x.IsEnabled).Select(x => new GuidNameVm()
            {
                PageGuid = x.ExtensionId,
                PageName = x.Title
            });
            return data;
        }

        /// <summary>
        /// 業管單位
        /// </summary>
        /// <returns></returns>
        public IQueryable<GuidNameVm> GetUnitList()
        {
            var data = _db.DataSetUnit.OrderBy(x => x.Sort).Where(x => x.IsEnabled).Select(x => new GuidNameVm()
            {
                PageGuid = x.UnitId,
                PageName = x.Title
            });
            return data;
        }
        #endregion

    }
}
