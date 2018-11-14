using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Geo.Grid.Tainan.OpenGov.Dal.Interface;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Api;
using Geo.Grid.Tainan.OpenGov.Service.Interface;

namespace Geo.Grid.Tainan.OpenGov.Service
{
    public class VideoService : IVideoService
    {
        private readonly IRepository<Youtube> repository;

        public VideoService(IRepository<Youtube> repository)
        {
            this.repository = repository;
        }

        public Youtube GetVideo()
        {
            return repository.Where(x => x.IsOpened && x.IsEnabled && x.EndDate >= DateTime.Now)
                .OrderBy(x => x.StartDate).FirstOrDefault();
        }

        public IQueryable<Youtube> QueryVideo()
        {
            return repository.Where(x => x.IsEnabled).AsNoTracking();
        }

        public Result<Youtube> Create(Youtube model)
        {
            var result = new Result<Youtube>(false);
            try
            {
                model.IsEnabled = true;
                model.IsOpened = true;
                result.Success = repository.Create(model) == 1;
                if (result.Success)
                {
                    result.Data = model;
                }
                else
                {
                    result.Message = "新增失敗!";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Youtube GetYoutube(Guid id)
        {
            return repository.Get(x => x.YoutubeId == id && x.IsEnabled);
        }

        public Result<Youtube> Update(Youtube model)
        {
            var result = new Result<Youtube>(false);
            try
            {
                var youtube = GetYoutube(model.YoutubeId);
                if (youtube == null)
                {
                    result.Message = "該直播會議不存在！";
                    return result;
                }
                youtube.Name = model.Name;
                youtube.Description = model.Description;
                youtube.Url = model.Url;
                youtube.StartDate = model.StartDate;
                youtube.EndDate = model.EndDate;
                youtube.VideoTime = model.VideoTime;

                result.Success = repository.SaveChanges() > 0;
                if (result.Success)
                {
                    result.Data = youtube;
                }
                else
                {
                    result.Message = "更新失敗!";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<Youtube> Remove(Guid id)
        {
            var result = new Result<Youtube>(false);
            try
            {
                var act = repository.Get(x => x.YoutubeId == id && x.IsEnabled);
                if (act == null)
                {
                    result.Message = "該直播會議不存在!";
                    return result;
                }
                act.IsEnabled = false;
                result.Success = repository.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// Restful-Api用
        /// 取得直播列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ApiYoutubeVm> GetList()
        {
            return repository.Where(x => x.IsOpened && x.IsEnabled)
                             .OrderByDescending(x => x.StartDate)
                             .Select(x => new ApiYoutubeVm
                             {
                                 Name = x.Name,
                                 Url = x.Url,
                                 Description = x.Description,
                                 StartDate = x.StartDate,
                                 EndDate = x.EndDate,
                                 VideoTime = x.VideoTime
                             });
        }
    }
}