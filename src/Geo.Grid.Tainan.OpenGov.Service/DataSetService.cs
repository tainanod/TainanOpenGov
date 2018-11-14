using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geo.Grid.Tainan.OpenGov.Dal;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.DataSet;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Common;
using Geo.Grid.Tainan.OpenGov.Service.Common;
using Geo.Grid.Tainan.OpenGov.Service.Interface;
using Geo.Grid.Tainan.OpenGov.Dal.Interface;

namespace Geo.Grid.Tainan.OpenGov.Service
{
    /// <summary>
    /// 資料目錄
    /// </summary>
    public class DataSetService : IDataSetService
    {
        private readonly IRepository<DataSet> _rep;
        private readonly OpenGovContext _db;
        private string _urlName = new WebSite().GetWebSite();

        /// <summary>
        /// service
        /// </summary>
        /// <param name="db">db</param>
        public DataSetService(IRepository<DataSet> rep)
        {
            this._rep = rep;
            this._db = rep.Db;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public PageResult<DataSetVm> GetList(SearchVm key)
        {
            var result = new PageResult<DataSetVm>();
            var data = _db.DataSet.OrderByDescending(x => x.CreatedDate).Where(x => !x.IsDeleted && x.IsEnabled);

            result.Data = data.Skip((key.CurrentPage - 1) * key.PageSize).Take(key.PageSize)
                      .Select(x => new DataSetVm()
                      {
                          Setid = x.SetId,
                          TypeId = x.TypeId,
                          TypeName = x.DataSetType.Title,
                          Title = x.Title,
                          Info = x.Info,
                          ExtensionList = x.DataSetExtensionRel.Select(c => new PageNameVm()
                          {
                              PageGuid = c.ExtensionId,
                              PageName = c.Title
                          })
                      });
            result.Total = data.Count();
            result.Success = true;
            result.PageSize = key.PageSize;
            result.CurrentPage = key.CurrentPage;
            result.TotalPage = result.Total / key.PageSize + ((result.Total % key.PageSize == 0) ? 0 : 1);

            return result;
        }

        /// <summary>
        /// 單筆
        /// </summary>
        /// <param name="id">編號</param>
        /// <returns></returns>
        public DataSetDetailVm GetDetail(Guid id)
        {
            var data = _db.DataSet.Where(x => !x.IsDeleted && x.IsEnabled && x.SetId == id).ToList().Select(x => new DataSetDetailVm()
            {
                SetId = x.SetId,
                Title = x.Title,
                UntiName = x.DataSetUnit.Title,
                ContactName = x.ContactName,
                ContactTel = x.ContactTel,
                WebUrl = x.WebUrl,
                Contents = x.Contents,
                Info = x.Info,
                ExtensionArray = string.Join("、", x.DataSetExtensionRel.Select(c => c.Title).ToArray()),
                VersionName = x.DataSetHistory.FirstOrDefault() == null ? "v1.0" : x.DataSetHistory.OrderByDescending(c => c.CreatedDate).FirstOrDefault().Title,
                UpdatedDate = x.UpdatedDate,
                DataSetHistoryList = x.DataSetHistory.OrderByDescending(c => c.CreatedDate).Where(c => c.IsEnabled).Select(c => new DataSetHistoryVm()
                {
                    HistoryId = c.HistoryId,
                    Title = c.Title,
                    Info = c.Info,
                    Contents = c.Contents,
                    UpdatedDate = c.UpdatedDate
                }),
                ShowCaseList = x.ShowCaseRel.OrderByDescending(c => c.CreatedDate).Where(c => !c.IsDeleted && c.IsEnabled).Select(c => new ShowCaseVm()
                {
                    CaseId = c.CaseId,
                    Title = c.Title,
                    Contents = c.Contents,
                    PhotoUrl = _urlName + "/Photo/Detail/" + c.PhotoId
                })
            });
            return data.FirstOrDefault();
        }

        /// <summary>
        /// 類別
        /// </summary>
        /// <returns></returns>
        public IQueryable<DataSetTypeVm> GetTypeList()
        {
            var data = _db.DataSetType.OrderBy(x => x.Sort).Where(x => x.IsEnabled).Select(x => new DataSetTypeVm()
            {
                TypeId = x.TypeId,
                Title = x.Title,
                Counts = x.DataSet.Where(c => !c.IsDeleted && c.IsEnabled).Count()
            });
            return data;
        }
        /// <summary>
        /// Restful-Api用，取得列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ApiDataSetVm> GetList()
        {
            return _db.DataSet.OrderByDescending(x => x.CreatedDate).Where(x => !x.IsDeleted && x.IsEnabled)
                        .Select(x => new ApiDataSetVm
                        {
                            Id = x.SetId,
                            Title = x.Title,
                            UnitName = x.DataSetUnit.Title
                        }).ToList(); ;
        }

        /// <summary>
        /// Restful-Api用
        /// 取得單一資料目錄之欄位資料內容
        /// </summary>
        /// <param name="id">資料目錄編號</param>
        public ApiDataSetDetailVm GetDataSetDetail(Guid id)
        {
            return _db.DataSet.Where(x => !x.IsDeleted && x.IsEnabled && x.SetId == id).ToList()
                .Select(x => new ApiDataSetDetailVm {
                    Id= x.SetId,
                    Title = x.Title,
                    UnitName = x.DataSetUnit.Title,
                    ContactName = x.ContactName,
                    ContactTel = x.ContactTel,
                    WebUrl = x.WebUrl,
                    ExtensionArray = string.Join("、",x.DataSetExtensionRel.Select(e=>e.Title).ToArray()),
                    VersionName = x.DataSetHistory.FirstOrDefault()==null?"v1.0":x.DataSetHistory.OrderByDescending(c=>c.CreatedDate).FirstOrDefault().Title,
                    UpdatedDate = x.UpdatedDate,
                    Info = x.Info
                }).FirstOrDefault();
        }
    }
}
