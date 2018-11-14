using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Geo.Grid.Tainan.OpenGov.Dal;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.ShowCase;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Common;
using Geo.Grid.Tainan.OpenGov.Service.Common;
using Geo.Grid.Tainan.OpenGov.Service.Interface;
using Geo.Grid.Tainan.OpenGov.Dal.Interface;
using System.Web;
using System.Threading;

namespace Geo.Grid.Tainan.OpenGov.Service
{
    public class ShowCaseService : IShowCaseService
    {
        private readonly IRepository<ShowCase> _rep;
        private readonly IRepository<Photo> _repPhoto;
        private readonly IRepository<Forum> _repForum;
        private readonly OpenGovContext _db;
        private string _urlName = new WebSite().GetWebSite();

        /// <summary>
        /// service
        /// </summary>
        /// <param name="db">db</param>
        public ShowCaseService(IRepository<ShowCase> rep, IRepository<Photo> repPhoto, IRepository<Forum> repForum)
        {
            this._rep = rep;
            this._repPhoto = repPhoto;
            this._repForum = repForum;
            this._db = rep.Db;
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="key">search</param>
        /// <returns></returns>
        public PageResult<ShowCaseVm> GetList(SearchVm key)
        {
            var result = new PageResult<ShowCaseVm>();
            var data = _db.ShowCase.Where(x => !x.IsDeleted && x.IsEnabled);

            result.Data = data.OrderByDescending(x => x.CreatedDate).Skip((key.CurrentPage - 1) * key.PageSize).Take(key.PageSize)
                      .Select(x => new ShowCaseVm()
                      {
                          CaseId = x.CaseId,
                          Title = x.Title,
                          Contents = x.Contents,
                          PhotoUrl = _urlName + "/Photo/Detail/" + x.PhotoId
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
        /// <param name="id">guid</param>
        /// <returns></returns>
        public ShowCaseDetailVm GetDetail(Guid id)
        {
            var data = _db.ShowCase.Where(x => !x.IsDeleted && x.CaseId == id).Select(x => new ShowCaseDetailVm()
            {
                CaseId = x.CaseId,
                Title = x.Title,
                UserName = x.UserName,
                Contents = x.Contents,
                PhotoUrl = _urlName + "/Photo/Detail/" + x.PhotoId,
                PhotoList = x.PhotoRel.OrderByDescending(c => c.CreatedDate).Where(c => c.IsEnabled).Select(c => new PageNameVm()
                {
                    PageGuid = c.PhotoId,
                    PageName = c.FileName,
                    PageUrl = _urlName + "/Photo/Detail/" + c.PhotoId
                }),
                DataSetList = x.DataSetRel.OrderByDescending(c => c.CreatedDate).Where(c => !c.IsDeleted && c.IsEnabled).Select(c => new PageNameVm()
                {
                    PageGuid = c.SetId,
                    PageName = c.Title,
                    PageUrl = _urlName + "/DataSet/Detail/" + c.SetId
                }),
                UrlList = x.ShowCaseUrl.OrderBy(c => c.Sort).Where(c => c.IsEnabled).Select(c => new PageNameVm()
                {
                    PageGuid = c.UrlId,
                    PageName = c.Title,
                    PageUrl = c.WebUrl
                })
            });
            return data.FirstOrDefault();
        }

        /// <summary>
        /// 資料目錄
        /// </summary>
        /// <returns></returns>
        public IQueryable<PageNameVm> GetDataSetList()
        {
            var data = _db.DataSet.OrderByDescending(x => x.CreatedDate).Where(x => !x.IsDeleted && x.IsEnabled).Select(x => new PageNameVm()
            {
                PageGuid = x.SetId,
                PageName = x.Title
            });
            return data;
        }

        /// <summary>
        /// 投稿-儲存
        /// </summary>
        /// <param name="vmD">參數</param>
        /// <returns></returns>
        public Result<string> GetCreate(ShowCaseCreateVm vmD)
        {
            Result<string> result = new Result<string>(false);
            try
            {
                var data = new ShowCase();
                data.Title = vmD.Title;
                data.UserName = vmD.UserName;
                data.UserEmail = vmD.UserEmail;
                data.Contents = vmD.Info;
                data.IsEnabled = false;

                data.PhotoId = GetPhotoCreate(vmD.AppImg, "ShowCase").ID;

                if (vmD.LinkArray != null)
                {
                    int i = 1;
                    var link = vmD.LinkArray.Where(x => !string.IsNullOrEmpty(x.Title) && !string.IsNullOrEmpty(x.Content)).Select(x => new ShowCaseUrl()
                    {
                        Title = x.Title,
                        WebUrl = x.Content,
                        Sort = i++
                    }).ToList();
                    data.ShowCaseUrl = link;
                }

                if (vmD.TagArray != null)
                {
                    var guid = vmD.TagArray.Select(x => x.PageGuid).ToList();
                    var dataSet = _db.DataSet.Where(x => guid.Contains(x.SetId)).ToList();
                    data.DataSetRel = dataSet;
                }

                if (vmD.AppImgArray != null)
                {
                    List<Guid> imgGuid = new List<Guid>();
                    foreach (var item in vmD.AppImgArray)
                    {
                        Guid photoId = GetPhotoCreate(item, "ShowCase").ID;
                        imgGuid.Add(photoId);
                    }
                    var photo = _db.Photo.Where(x => imgGuid.Contains(x.PhotoId)).ToList();
                    data.PhotoRel = photo;
                }

                result.Success = _rep.Create(data) > 0;
                result.ID = data.CaseId;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 圖片上傳
        /// </summary>
        /// <param name="file">檔案</param>
        /// <param name="alt">路徑</param>
        /// <returns></returns>
        private Result<Photo> GetPhotoCreate(HttpPostedFileBase file, string alt)
        {
            return new PhotoService(_repPhoto, _repForum).GetCreate(file, alt);
        }

        /// <summary>
        /// 使用者資料
        /// </summary>
        /// <returns></returns>
        public UserVm GetUserDetail()
        {
            var name = Thread.CurrentPrincipal.Identity.Name;
            var data = _db.AspNetUsers.Where(x => x.Email == name).Select(x => new UserVm()
            {
                UserId = x.Id,
                UserEmail = x.Email,
                UserName = x.NickName
            });
            return data.FirstOrDefault();
        }

        /// <summary>
        /// Restful-Api用
        /// 取得列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ApiShowCaseVm> GetList()
        {
            return _db.ShowCase.Where(x => !x.IsDeleted && x.IsEnabled).ToList()
                      .Select(x => new ApiShowCaseVm {
                          Title = x.Title,
                          UserName = x.UserName,
                          Contents = x.Contents,
                          DataSetList = x.DataSetRel.Select(d=>new PageNameVm {
                              PageGuid = d.SetId,
                              PageName = d.Title,
                              PageUrl = $"{_urlName}/DataSet/Detail/{d.SetId}"
                          })
                      });
        }
    }
}
