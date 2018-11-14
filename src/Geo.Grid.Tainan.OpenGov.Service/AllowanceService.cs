using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Geo.Grid.Tainan.OpenGov.Dal;
using Geo.Grid.Tainan.OpenGov.Dal.Interface;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.Common;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel;
using Geo.Grid.Tainan.OpenGov.Service.Interface;
using System.Net.Http;
using System.Globalization;
using System.Web;
using NLog;

namespace Geo.Grid.Tainan.OpenGov.Service
{
    /// <summary>
    /// 弱勢補助 service
    /// </summary>
    public class AllowanceService : IAllowanceService
    {
        /// <summary>
        /// 資料集來源管理
        /// </summary>
        private readonly IRepository<AllowanceSource> allowanceSourceRepository;

        /// <summary>
        /// api service
        /// </summary>
        private readonly IApiService apiService;

        /// <summary>
        /// DbModel
        /// </summary>
        private readonly OpenGovContext dbModel;

        /// <summary>
        /// nlog
        /// </summary>
        private Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 弱勢補助 service 建構子
        /// </summary>
        /// <param name="allowanceSourceRepository"></param>
        /// <param name="iApiService"></param>
        public AllowanceService(IRepository<AllowanceSource> allowanceSourceRepository, IApiService iApiService)
        {
            this.dbModel = allowanceSourceRepository.Db;
            this.allowanceSourceRepository = allowanceSourceRepository;
            this.apiService = iApiService;
        }

        #region 補助資料
        /// <summary>
        /// 關鍵字搜尋
        /// </summary>
        /// <param name="queryVm">搜尋條件</param>
        /// <returns>補助 資料</returns>
        public PageResult<AllowanceVm> Query(AllowanceQueryVm queryVm)
        {
            var query = GetQuery();

            //關鍵字
            if (string.IsNullOrEmpty(queryVm.Keyword) == false)
            {
                query = query.Where(x => x.Docs.Contains(queryVm.Keyword) || x.Name.Contains(queryVm.Keyword));
            }

            return GetPageResult(query, queryVm);
        }

        /// <summary>
        /// 進階搜尋
        /// </summary>
        /// <param name="queryVm">搜尋條件</param>
        /// <returns>補助 資料</returns>
        public PageResult<AllowanceVm> AdvQuery(AllowanceQueryVm queryVm)
        {
            try
            {
                //驗證 AllowanceQueryVm 是否合法
                ValidateQueryVm(queryVm);

                var query = GetQuery();

                if (queryVm.LiveIn == "未設籍臺南市")
                {
                    query = query.Where(x => x.IsLiveIn == null || x.IsLiveIn == string.Empty || x.IsLiveIn == "否");
                    query = query.Where(x => x.IsLiveDays == null || x.IsLiveDays == string.Empty || x.IsLiveDays == "否");
                }
                else
                {
                    //query = query.Where(x => x.IsLiveIn == null || x.IsLiveIn == string.Empty || x.IsLiveIn == "否" || x.IsLiveIn == "是");

                    if (queryVm.IsLiveDays == "否")
                    {
                        query = query.Where(x => x.IsLiveDays == null || x.IsLiveDays == string.Empty || x.IsLiveDays == "否");
                    }
                }

                //年齡判斷
                int age = queryVm.Age.HasValue ? queryVm.Age.Value : 0;
                int childrenAge = queryVm.ChildrenAge.HasValue ? queryVm.ChildrenAge.Value : 0;
                int ageMin = queryVm.AgeMin.HasValue ? queryVm.AgeMin.Value : 0;
                int ageMax = queryVm.AgeMax.HasValue ? queryVm.AgeMax.Value : 0;

                query = query.Where(x =>
                    (x.AgeMin == null && x.AgeMax == null) ||
                    (x.AgeMin == null && (
                        queryVm.AnyChildren == false ? age <= x.AgeMax :
                        queryVm.AnyChildren == true ? (
                            queryVm.IsChildren == false ? (age <= x.AgeMax || childrenAge <= x.AgeMax) :
                            queryVm.IsChildren == true ? (age <= x.AgeMax || ageMin <= x.AgeMax || ageMax <= x.AgeMax) : false
                        ) : false
                    )) ||
                    (x.AgeMax == null && (
                        queryVm.AnyChildren == false ? x.AgeMin <= age :
                        queryVm.AnyChildren == true ? (
                            queryVm.IsChildren == false ? (x.AgeMin <= age || x.AgeMin <= childrenAge) :
                            queryVm.IsChildren == true ? (x.AgeMin <= age || x.AgeMin <= ageMin || x.AgeMin <= ageMax) : false
                        ) : false
                    )) ||
                    (
                        queryVm.AnyChildren == false ? age >= x.AgeMin && age <= x.AgeMax :
                        queryVm.AnyChildren == true ? (
                            queryVm.IsChildren == false ? ((age >= x.AgeMin && age <= x.AgeMax) || (childrenAge >= x.AgeMin && childrenAge <= x.AgeMax)) :
                            queryVm.IsChildren == true ? ((age >= x.AgeMin && age <= x.AgeMax) || (ageMin >= x.AgeMin && ageMin <= x.AgeMax) || (ageMax >= x.AgeMin && ageMax <= x.AgeMax)) : false
                        ) : false
                    )
                );

                //請問您是否具備下列特殊身分條件?
                if (queryVm.Identity1 == null || queryVm.Identity1.Any() == false)
                {
                    query = query.Where(x => (x.Identity1 == null || x.Identity1 == string.Empty) && (x.Identity2 == null || x.Identity2 == string.Empty));
                }
                else
                {
                    query = query.Where(x =>
                        (queryVm.Identity1.Contains(x.Identity1) || queryVm.Identity1.Contains(x.Identity2)) ||
                        (queryVm.Identity1.Contains(x.Identity1) && (x.Identity2 == null || x.Identity2 == string.Empty)) ||
                        ((x.Identity1 == null || x.Identity1 == string.Empty) && queryVm.Identity1.Contains(x.Identity2)) ||
                        ((x.Identity1 == null || x.Identity1 == string.Empty) && (x.Identity2 == null || x.Identity2 == string.Empty))
                    );
                }

                //請問您是否具備下列其他身分條件?
                if (queryVm.Identity2 == null || queryVm.Identity2.Any() == false)
                {
                    query = query.Where(x => x.Others == null || x.Others == string.Empty);
                }
                else
                {
                    query = query.Where(x => x.Others == null || x.Others == string.Empty || queryVm.Identity2.Contains(x.Others));
                }

                query = query.Where(x => queryVm.IncomeValue.Value <= x.IncomeValue || x.IncomeValue == null);

                query = query.Where(x => queryVm.MovableValue.Value <= x.MovableValue || x.MovableValue == null);

                query = query.Where(x => queryVm.ImmovableValue.Value <= x.ImmovableValue || x.ImmovableValue == null);

                return GetPageResult(query, queryVm);
            }
            catch (Exception e)
            {
                var result = new PageResult<AllowanceVm>();
                result.Message = e.Message;
                return result;
            }
        }

        /// <summary>
        /// 驗證 AllowanceQueryVm 是否合法
        /// </summary>
        /// <param name="queryVm">搜尋條件</param>
        private void ValidateQueryVm(AllowanceQueryVm queryVm)
        {
            string errorMessage = string.Empty;

            if (string.IsNullOrEmpty(queryVm.LiveIn))
            {
                errorMessage += @"問題：請問您的戶籍地位於臺南市嗎 未選擇! \n";
            }

            if (queryVm.LiveIn != "未設籍臺南市" && queryVm.IsLiveDays == null)
            {
                errorMessage += @"問題：請問是否在臺南市居住超過183天 未選擇! \n";
            }

            if (queryVm.Age.HasValue == false)
            {
                errorMessage += @"問題：您的年齡是 未填寫! \n";
            }

            if (queryVm.AnyChildren.HasValue == false)
            {
                errorMessage += @"問題：您是否有子女 未填寫! \n";
            }

            if (queryVm.AnyChildren == true && queryVm.IsChildren == null)
            {
                errorMessage += @"問題：您共養育幾名子女 未填寫! \n";
            }

            if (queryVm.IsChildren == false && queryVm.ChildrenAge == null)
            {
                errorMessage += @"問題：其年齡為? 未填寫! \n";
            }

            if (queryVm.IsChildren == true && (queryVm.AgeMin == null || queryVm.AgeMax == null))
            {
                errorMessage += @"問題：其年齡介於幾歲? 未填寫! \n";
            }

            //if (string.IsNullOrEmpty(queryVm.Identity1))
            //{
            //    errorMessage += @"問題：請問您是否具備下列特殊身分條件? 未填寫! \n";
            //}

            //if (string.IsNullOrEmpty(queryVm.Identity2))
            //{
            //    errorMessage += @"問題：請問您是否具備下列其他身分條件? 未填寫! \n";
            //}

            if (queryVm.IncomeValue.HasValue == false)
            {
                errorMessage += @"問題：請問您全家每人每月收入約多少? 未填寫! \n";
            }

            if (queryVm.MovableValue.HasValue == false)
            {
                errorMessage += @"問題：請問您的動產金額約多少? 未填寫! \n";
            }

            if (queryVm.ImmovableValue.HasValue == false)
            {
                errorMessage += @"問題：請問您全家不動產總值約多少? 未填寫! \n";
            }

            if (errorMessage.Length > 0)
            {
                throw new Exception(errorMessage);
            }
        }

        /// <summary>
        /// 取得 明細
        /// </summary>
        /// <param name="allowanceId">補助ID</param>
        /// <returns>補助 資料</returns>
        public Result<AllowanceVm> Detail(Guid allowanceId)
        {
            var result = new Result<AllowanceVm>();

            try
            {
                result.Data = GetQuery().Where(x => x.AllowanceId == allowanceId).Single();
                result.Success = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Success = false;
            }

            return result;
        }

        /// <summary>
        /// 區公所
        /// </summary>
        /// <returns></returns>
        public Result<List<DistrictOffice>> GetDistrictOffice()
        {
            var result = new Result<List<DistrictOffice>>();

            try
            {
                result.Data = dbModel.DistrictOffice.ToList();
                result.Success = true;
            }
            catch (Exception e)
            {
                result.Message = e.Message;
                result.Success = false;
            }

            return result;
        }

        /// <summary>
        /// 搜尋 [社福補助]
        /// </summary>
        /// <returns></returns>
        private IQueryable<AllowanceVm> GetQuery()
        {
            var query = dbModel.Allowance.Include(x=>x.AllowanceSource).AsNoTracking()
                .Where(x=>x.AllowanceSource.IsEnabled)
                .OrderBy(x => x.AllowanceCode)
                .Select(x => new AllowanceVm
                {
                    AllowanceId = x.AllowanceId,
                    AllowanceCode = x.AllowanceCode,
                    Name = x.Name,
                    AgeMin = x.AgeMin,
                    AgeMax = x.AgeMax,
                    LiveIn = x.LiveIn,
                    IsLiveIn = x.IsLiveIn,
                    LiveDays = x.LiveDays,
                    IsLiveDays = x.IsLiveDays,
                    Identity1 = x.Identity1,
                    Identity2 = x.Identity2,
                    IncomeValue = x.IncomeValue,
                    MovableValue = x.MovableValue,
                    ImmovableValue = x.ImmovableValue,
                    Others = x.Others,
                    Docs = x.Docs,
                    Receiver = x.Receiver,
                    Contact = x.Contact,
                    MoreInfo = x.MoreInfo,
                });

            return query;
        }

        /// <summary>
        /// 取得 PageResult
        /// </summary>
        /// <param name="query">query 條件</param>
        /// <param name="queryVm">搜尋條件</param>
        /// <returns></returns>
        private PageResult<AllowanceVm> GetPageResult(IQueryable<AllowanceVm> query, AllowanceQueryVm queryVm)
        {
            var result = new PageResult<AllowanceVm>();

            result.Data = query.Skip((queryVm.CurrentPage - 1) * queryVm.PageSize).Take(queryVm.PageSize).ToList();
            result.Total = query.Count();
            result.Success = true;
            result.PageSize = queryVm.PageSize;
            result.CurrentPage = queryVm.CurrentPage;
            result.TotalPage = result.Total / queryVm.PageSize + ((result.Total % queryVm.PageSize == 0) ? 0 : 1);
            return result;
        }


        /// <summary>
        /// 請問您是否具備下列其他身分條件
        /// </summary>
        /// <returns>其他身分條件</returns>
        public List<string> GetOthers()
        {
            return dbModel.Allowance.Include(x => x.AllowanceSource)
                .Where(x => x.AllowanceSource.IsEnabled)
                .Select(x => x.Others)
                .Where(x => x != null && x != "")
                .Distinct()
                .ToList();
        }

        /// <summary>
        /// 請問您是否具備下列特殊身分條件?
        /// </summary>
        /// <returns>特殊身分條件</returns>
        public List<string> GetIdentity()
        {
            List<AllowanceVm> allowanceVms = dbModel.Allowance.Include(x=>x.AllowanceSource)
                .Where(x=>x.AllowanceSource.IsEnabled)
                .Select(x => new AllowanceVm
                {
                    Identity1 = x.Identity1,
                    Identity2 = x.Identity2,
                }).ToList();

            List<string> identity1 = allowanceVms.Select(x => x.Identity1).Where(x => x != null && x != "").Distinct().ToList();

            List<string> identity2 = allowanceVms.Select(x => x.Identity2).Where(x => x != null && x != "").Distinct().ToList();

            identity1.AddRange(identity2);

            return identity1.Distinct().ToList();
        }
        #endregion

        #region 資料集來源管理

        /// <summary>
        /// 取得 資料集來源管理 清單
        /// </summary>
        /// <returns></returns>
        public IQueryable<AllowanceSource> GetAllowanceSourceList()
        {
            return dbModel.AllowanceSources.Include(x=>x.Allowances).Where(x => x.IsEnabled)
                .OrderByDescending(x => x.CreatedDate);
        }

        /// <summary>
        /// 取得 資料集來源管理
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AllowanceSource GetAllowanceSource(Guid id)
        {
            return allowanceSourceRepository.Get(x => x.IsEnabled && x.SourceId == id);
        }

        /// <summary>
        /// 更新 資料集來源管理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result<AllowanceSource> UpdateAllowanceSource(AllowanceSource model)
        {
            var result = new Result<AllowanceSource>(false);
            try
            {
                var allowanceSource = GetAllowanceSource(model.SourceId);

                if (allowanceSource == null)
                {
                    result.Message = "該資料不存在！";
                    return result;
                }

                allowanceSource.Name = model.Name;
                allowanceSource.Organization = model.Organization;
                allowanceSource.WebSite = model.WebSite;
                allowanceSource.ApiUrl = model.ApiUrl;
                allowanceSource.ResourceId = GetResourceId(model.ApiUrl);

                result.Success = allowanceSourceRepository.SaveChanges() > 0;
                if (result.Success)
                {
                    result.Data = allowanceSource;
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

        /// <summary>
        /// 由 url 取得 resourceId
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <returns></returns>
        private Guid GetResourceId(string apiUrl)
        {
            Guid resourceId = default(Guid);
            if (string.IsNullOrEmpty(apiUrl))
            {
                return resourceId;
            }

            Uri uri = new Uri(apiUrl);
            //var queryParams = uri.ParseQueryString();
            //var queryParams = HttpUtility.ParseQueryString(uri.Query, System.Text.Encoding.UTF8);
            var queryParams = HttpUtility.ParseQueryString(uri.Query);
            if (queryParams["resource_id"] != null)
            {
                resourceId = Guid.Parse(queryParams["resource_id"]);
            }

            return resourceId;
        }

        /// <summary>
        /// 刪除 資料集來源管理
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result<AllowanceSource> RemoveAllowanceSource(Guid id)
        {
            var result = new Result<AllowanceSource>(false);
            try
            {
                var allowanceSource = allowanceSourceRepository.Get(x => x.SourceId == id && x.IsEnabled);
                if (allowanceSource == null)
                {
                    return result;
                }
                allowanceSource.IsEnabled = false;
                result.Success = allowanceSourceRepository.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 新增 資料集來源管理
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result<AllowanceSource> CreateAllowanceSource(AllowanceSource model)
        {
            var result = new Result<AllowanceSource>(false);
            try
            {
                model.SourceId = Guid.NewGuid();
                model.IsEnabled = true;
                model.ResourceId = GetResourceId(model.ApiUrl);
                result.Success = allowanceSourceRepository.Create(model) == 1;
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

        /// <summary>
        /// 依據[資料集來源管理] 更新 [補助資料]
        /// </summary>
        /// <returns></returns>
        public Result<string> RefreshAllowanceList()
        {
            var errors = new List<string>();

            foreach (AllowanceSource allowanceSource in GetAllowanceSourceList().ToList())
            {
                try
                {
                    setAllowances(allowanceSource);
                }
                catch (Exception e)
                {
                    string msg = $"SourceId:{allowanceSource.SourceId}、Name:{allowanceSource.Name}  {e.Message}";
                    errors.Add(msg);
                    logger.Error(e, msg);
                }
            }

            Result<string> result = new Result<string>();
            if (errors.Any())
            {
                result.Message = string.Join("\n", errors);
            }
            else
            {
                result.Success = true;
            }

            return result;
        }

        /// <summary>
        /// 更新一筆 資料集來源
        /// </summary>
        /// <param name="sourceId"></param>
        /// <returns></returns>
        public Result<string> RefreshAllowanceBySourceId(Guid sourceId)
        {
            Result<string> result = new Result<string>();

            string allowanceSourceName = string.Empty;
            try
            {
                if (sourceId==default(Guid))
                {
                    throw new Exception("無此 資料集來源!!");
                }

                var allowanceSource = dbModel.AllowanceSources.Include(x=>x.Allowances)
                    .Where(x => x.SourceId == sourceId && x.IsEnabled).SingleOrDefault();
                
                if (allowanceSource == null)
                {
                    throw new Exception("無此 資料集來源!!");
                }

                allowanceSourceName = allowanceSource.Name;

                setAllowances(allowanceSource);

                result.Success = true;
            }
            catch (Exception e)
            {
                string msg = $"SourceId:{sourceId.ToString()}、Name:{allowanceSourceName}  {e.Message}";
                result.Message = msg;
                logger.Error(e, msg);
            }

            return result;
        }

        /// <summary>
        /// 新增、刪除、修改 allowance
        /// </summary>
        /// <param name="allowanceSource"></param>
        public void setAllowances(AllowanceSource allowanceSource)
        {
            if (string.IsNullOrEmpty(allowanceSource.ApiUrl) ||
                allowanceSource.ResourceId == default(Guid))
            {
                throw new Exception("ApiUrl不能為空。");
            }
            
            //目前[網址]先寫固定
            //http://data.tainan.gov.tw/api/action/datastore_search?resource_id=d28fb219-eaa9-4126-a45a-4a6f7297c717
            string webSite = string.Format(@"http://data.tainan.gov.tw/api/action/datastore_search?resource_id={0}", 
                allowanceSource.ResourceId.ToString());

            Dictionary<string, string> replaceStr = new Dictionary<string, string>();
            replaceStr.Add("\\ufeff\\u88dc\\u52a9\\u6d41\\u6c34\\u865f", "\\u88dc\\u52a9\\u6d41\\u6c34\\u865f");

            var apiPackage = apiService.GetDataFromWebApi<ApiPackage>(webSite, replaceStr);
            if (apiPackage.Result == null ||
                apiPackage.Result.Records == null ||
                apiPackage.Result.Records.Any() == false)
            {
                throw new Exception("API的資料為空");
            }
            
            var apiAllowances = apiPackage.Result.Records;
            List<int> dataIds = allowanceSource.Allowances.Select(x => x.DataId).ToList();

            updateAllowances(allowanceSource, apiAllowances);
            addAllowances(allowanceSource, apiAllowances);
            removeAllowances(allowanceSource, apiAllowances);

            dbModel.SaveChanges();
        }

        /// <summary>
        /// 更新 allowance
        /// </summary>
        /// <param name="allowanceSource"></param>
        /// <param name="apiAllowances"></param>
        private void updateAllowances(AllowanceSource allowanceSource, List<AllowanceJsonVm> apiAllowances)
        {
            var ids = apiAllowances.Select(x => x.DataId).ToList();

            foreach (var allowance in allowanceSource.Allowances.Where(x => ids.Contains(x.DataId)))
            {
                var apiAllowance = apiAllowances.Where(x => x.DataId == allowance.DataId).SingleOrDefault();

                if (apiAllowance == null)
                {
                    continue;
                }

                allowance.AllowanceCode = apiAllowance.AllowanceCode;
                allowance.Name = apiAllowance.Name;
                allowance.AgeMin = apiAllowance.AgeMin;
                allowance.AgeMax = apiAllowance.AgeMax;
                allowance.LiveIn = apiAllowance.LiveIn;
                allowance.IsLiveIn = apiAllowance.IsLiveIn;
                //allowance.LiveDays = apiAllowance.LiveDays;
                allowance.IsLiveDays = apiAllowance.IsLiveDays;
                allowance.Identity1 = apiAllowance.Identity1;
                allowance.Identity2 = apiAllowance.Identity2;
                allowance.IncomeValue = apiAllowance.IncomeValue;
                allowance.MovableValue = apiAllowance.MovableValue;
                allowance.ImmovableValue = apiAllowance.ImmovableValue;

                allowance.Others = apiAllowance.Others;
                allowance.Docs = apiAllowance.Docs;
                allowance.Receiver = apiAllowance.Receiver;
                allowance.Contact = apiAllowance.Contact;
                allowance.MoreInfo = apiAllowance.MoreInfo;
                allowance.DataId = apiAllowance.DataId;
                //allowance.SourceId = allowanceSource.SourceId;
            }
        }

        /// <summary>
        /// 新增 Allowance
        /// </summary>
        /// <param name="allowanceSource"></param>
        /// <param name="apiAllowances"></param>
        private void addAllowances(AllowanceSource allowanceSource, List<AllowanceJsonVm> apiAllowances)
        {
            var ids = allowanceSource.Allowances.Select(x => x.DataId).ToList();

            foreach (var apiAllowance in apiAllowances.Where(x => ids.Contains(x.DataId)==false))
            {
                Allowance allowance = new Allowance()
                {
                    AllowanceCode = apiAllowance.AllowanceCode,
                    Name = apiAllowance.Name,
                    AgeMin = apiAllowance.AgeMin,
                    AgeMax = apiAllowance.AgeMax,
                    LiveIn = apiAllowance.LiveIn,
                    IsLiveIn = apiAllowance.IsLiveIn,
                    IsLiveDays = apiAllowance.IsLiveDays,
                    Identity1 = apiAllowance.Identity1,
                    Identity2 = apiAllowance.Identity2,
                    IncomeValue = apiAllowance.IncomeValue,
                    MovableValue = apiAllowance.MovableValue,
                    ImmovableValue = apiAllowance.ImmovableValue,
                    Others = apiAllowance.Others,
                    Docs = apiAllowance.Docs,
                    Receiver = apiAllowance.Receiver,
                    Contact = apiAllowance.Contact,
                    MoreInfo = apiAllowance.MoreInfo,
                    SourceId = allowanceSource.SourceId,
                    DataId = apiAllowance.DataId,
                };

                allowanceSource.Allowances.Add(allowance);
            }
        }
        
        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="allowanceSource"></param>
        /// <param name="apiAllowances"></param>
        private void removeAllowances(AllowanceSource allowanceSource, List<AllowanceJsonVm> apiAllowances)
        {
            var allowances = dbModel.Allowance.Where(x=>x.AllowanceSource.SourceId == allowanceSource.SourceId);
            var ids = apiAllowances.Select(x => x.DataId).ToList();

            foreach (var allowance in allowances.Where(x => ids.Contains(x.DataId) == false))
            {
                dbModel.Allowance.Remove(allowance);
                allowanceSource.Allowances.Remove(allowance);
            }
        }

        #endregion
    }
}