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
    /// 野生台南-應用展示
    /// </summary>
    public class ShowCaseService : IShowCaseService
    {
        private readonly IRepository<ShowCase> _showCase;
        private readonly OpenGovContext _db;

        /// <summary>
        /// service
        /// </summary>
        /// <param name="db">db</param>
        public ShowCaseService(IRepository<ShowCase> showCase)
        {
            this._showCase = showCase;
            this._db = showCase.Db;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="key">查詢</param>
        /// <returns></returns>
        public IQueryable<ShowCaseVm> GetList(SearchVm key)
        {
            var data = _db.ShowCase.OrderByDescending(x => x.CreatedDate).Where(x => !x.IsDeleted).Select(x => new ShowCaseVm()
            {
                CaseId = x.CaseId,
                Title = x.Title,
                UserName = x.UserName,
                Sort = x.Sort,
                IsEnabled = x.IsEnabled,
                PhotoId = x.PhotoId,
                CreatedDate = x.CreatedDate
            });
            return data;
        }

        /// <summary>
        /// 新增-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        public Result<string> GetCreate(ShowCaseVm vmD)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var data = new ShowCase();
                data.Title = vmD.Title;
                data.UserName = vmD.UserName;
                data.UserEmail = vmD.UserEmail;
                data.Contents = vmD.Contents;
                data.PhotoId = vmD.PhotoId;
                data.IsEnabled = vmD.IsEnabled;

                if (vmD.DataSetList != null)
                {
                    var dataSet = _db.DataSet.Where(x => vmD.DataSetList.Contains(x.SetId)).ToList();
                    data.DataSetRel = dataSet;
                }

                if (vmD.PhotoList != null)
                {
                    var photo = _db.Photo.Where(x => vmD.PhotoList.Contains(x.PhotoId.ToString())).ToList();
                    data.PhotoRel = photo;
                }

                if (vmD.UrlList != null)
                {
                    int i = 1;
                    var url = vmD.UrlList.Where(x => !string.IsNullOrEmpty(x.PageName) && !string.IsNullOrEmpty(x.PageUrl)).Select(x => new ShowCaseUrl()
                    {
                        Title = x.PageName,
                        WebUrl = x.PageUrl,
                        Sort = i++
                    }).ToList();

                    data.ShowCaseUrl = url;
                }

                result.Success = _showCase.Create(data) > 0;
                result.ID = data.CaseId;
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
        public ShowCaseVm GetDetail(Guid id)
        {
            var data = _db.ShowCase.Where(x => x.CaseId == id).Select(x => new ShowCaseVm()
            {
                CaseId = x.CaseId,
                Title = x.Title,
                UserName = x.UserName,
                UserEmail = x.UserEmail,
                Contents = x.Contents,
                PhotoId = x.PhotoId,
                Sort = x.Sort,
                IsEnabled = x.IsEnabled,
                DataSetList = x.DataSetRel.Select(c => c.SetId),
                PhotoList = x.PhotoRel.Select(c => c.PhotoId.ToString()),
                UrlList = x.ShowCaseUrl.OrderBy(c => c.Sort).Select(c => new PageNameVm()
                {
                    PageId = c.UrlId.ToString(),
                    PageName = c.Title,
                    PageUrl = c.WebUrl
                })
            });
            return data.FirstOrDefault();
        }

        /// <summary>
        /// 編輯-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        public Result<string> GetEdit(ShowCaseVm vmD)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var data = _db.ShowCase.Find(vmD.CaseId);
                if (data != null)
                {
                    data.DataSetRel.Clear();
                    data.PhotoRel.Clear();
                    _db.ShowCaseUrl.RemoveRange(data.ShowCaseUrl);

                    data.Title = vmD.Title;
                    data.UserName = vmD.UserName;
                    data.UserEmail = vmD.UserEmail;
                    data.Contents = vmD.Contents;
                    data.PhotoId = vmD.PhotoId;
                    data.IsEnabled = vmD.IsEnabled;

                    if (vmD.DataSetList != null)
                    {
                        var dataSet = _db.DataSet.Where(x => vmD.DataSetList.Contains(x.SetId)).ToList();
                        data.DataSetRel = dataSet;
                    }

                    if (vmD.PhotoList != null)
                    {
                        var photo = _db.Photo.Where(x => vmD.PhotoList.Contains(x.PhotoId.ToString())).ToList();
                        data.PhotoRel = photo;
                    }

                    if (vmD.UrlList != null)
                    {
                        int i = 1;
                        var url = vmD.UrlList.Where(x => !string.IsNullOrEmpty(x.PageName) && !string.IsNullOrEmpty(x.PageUrl)).Select(x => new ShowCaseUrl()
                        {
                            Title = x.PageName,
                            WebUrl = x.PageUrl,
                            Sort = i++
                        }).ToList();
                        
                        data.ShowCaseUrl = url;
                    }

                    result.Success = _showCase.SaveChanges() > 0;
                    result.ID = data.CaseId;
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
                var data = _db.ShowCase.Find(id);
                if (data != null)
                {
                    data.IsDeleted = true;
                    result.Success = _showCase.SaveChanges() > 0;
                    result.ID = data.CaseId;
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
        /// 野生台南-資料目錄
        /// </summary>
        /// <returns></returns>
        public IQueryable<GuidNameVm> GetDataSetList()
        {
            var data = _db.DataSet.OrderByDescending(x => x.CreatedDate)
                .Where(x => !x.IsDeleted && x.IsEnabled).Select(x => new GuidNameVm()
                {
                    PageGuid = x.SetId,
                    PageName = x.Title
                });
            return data;
        }

        #endregion

    }
}
