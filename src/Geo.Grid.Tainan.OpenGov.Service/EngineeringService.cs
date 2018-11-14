using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Spatial;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CsvHelper;
using Geo.Grid.Common.Models;
using Geo.Grid.Tainan.OpenGov.Dal;
using Geo.Grid.Tainan.OpenGov.Dal.Interface;
using Geo.Grid.Tainan.OpenGov.Entity;
using Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Engineering;
using Geo.Grid.Tainan.OpenGov.Service.Interface;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Microsoft.SqlServer.Types;
using GeoJSON.Net.Contrib.MsSqlSpatial;
using Geo.Grid.Common.Gis;
using System.Text;

namespace Geo.Grid.Tainan.OpenGov.Service
{
    /// <summary>
    /// 
    /// </summary>
    public class EngineeringService : IEngineeringService
    {
        /// <summary>
        /// dbModel
        /// </summary>
        private OpenGovContext dbModel;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="IEngineeringService"></param>
        public EngineeringService(IRepository<Engineering> repository)
        {
            this.dbModel = repository.Db;
        }

        /// <summary>
        /// 關鍵字搜尋
        /// </summary>
        /// <param name="queryVm">搜尋條件</param>
        /// <returns>工程資料</returns>
        public List<EngineeringVm> Query(EngineeringQueryVm queryVm)
        {
            var query = GetQuery(queryVm);
            
            return GetEngineeringVm(query).ToList();
        }

        /// <summary>
        /// 進階搜尋
        /// </summary>
        /// <param name="queryVm">搜尋條件</param>
        /// <returns>工程資料</returns>
        public List<EngineeringVm> AdvQuery(EngineeringQueryVm queryVm)
        {
            var query = GetEngineeringVm(GetQuery(queryVm));

            if (queryVm.AmountMin.HasValue && queryVm.AmountMax.HasValue && queryVm.AmountMin.Value > queryVm.AmountMax.Value)
            {
                throw new Exception("工程經費 輸入錯誤。");
            }

            if (queryVm.StartDate.HasValue && queryVm.EndDate.HasValue && queryVm.StartDate.Value.Date > queryVm.EndDate.Value.Date)
            {
                throw new Exception("開工日期 輸入錯誤。");
            }

            if (string.IsNullOrEmpty(queryVm.Town) == false)
            {
                query = query.Where(x => x.CityTown.Contains(queryVm.Town));
            }

            if (string.IsNullOrEmpty(queryVm.Category) == false)
            {
                query = query.Where(x => x.Category.Contains(queryVm.Category));
            }

            if (string.IsNullOrEmpty(queryVm.ProgressText) == false)
            {
                query = query.Where(x => x.ProgressText.Contains(queryVm.ProgressText));
            }

            if (string.IsNullOrEmpty(queryVm.Status) == false)
            {
                if (queryVm.Status.Contains("已結案"))
                {
                    query = query.Where(x => x.Status.Contains("保固中") || x.Status.Contains("已結案"));
                }
                else
                {
                    query = query.Where(x => x.Status.Contains(queryVm.Status));
                }
            }

            if (queryVm.AmountMin != null)
            {
                var amountMin = queryVm.AmountMin.Value;
                query = query.Where(x => amountMin <= x.Amount);
            }

            if (queryVm.AmountMax != null)
            {
                var amountMax = queryVm.AmountMax.Value;
                query = query.Where(x => amountMax >= x.Amount);
            }

            if (queryVm.StartDate != null)
            {
                var startDate = queryVm.StartDate.Value.Date;
                query = query.Where(x => startDate <= x.ActualStartDate);
            }

            if (queryVm.EndDate != null)
            {
                var endDate = queryVm.EndDate.Value.Date.AddDays(1);
                query = query.Where(x => endDate > x.ActualStartDate);
            }

            return query.ToList();
        }

        /// <summary>
        /// 將搜尋結果轉換成GeoJSON
        /// </summary>
        /// <param name="queryVm"></param>
        /// <param name="advQuery"></param>
        /// <returns></returns>
        public FeatureCollection GetGeoJson(EngineeringQueryVm queryVm, bool advQuery)
        {
            var data = advQuery ? AdvQuery(queryVm) : Query(queryVm);

            var model = new FeatureCollection();

            var list = data.Select(x => new Feature(GetIGeometry(x.Geom)
                                      , new Dictionary<string, object>
                                        {
                                            { "GovernmentAenciesCode", x.GovernmentAenciesCode},
                                            { "Code", x.Code },
                                            { "Name", x.Name },
                                            { "CityTown", x.CityTown},
                                            { "Category", x.Category},
                                            { "Discrepancy", x.Discrepancy },
                                            { "ProgressText", x.Discrepancy >= 0 ? "進度正常無落後" : "進度落後" },
                                            { "Amount", x.Amount },
                                            { "Geom", x.Geom },
                                            { "Status", x.Status },
                                            { "ActualStartDate", x.ActualStartDate },
                                            { "Address", x.Address }
                                        }
                                      , Guid.NewGuid().ToString()));
            model.Features.AddRange(list);
            return model;
        }

        /// <summary>
        /// 取得 Engineering Queryable
        /// </summary>
        /// <param name="queryVm">搜尋條件</param>
        /// <returns>Engineering Queryable</returns>
        private IQueryable<Engineering> GetQuery(EngineeringQueryVm queryVm = null)
        {
            var query = dbModel.Engineerings.Where(x => x.IsEnabled);

            if (queryVm != null && string.IsNullOrEmpty(queryVm.Keyword) == false)
            {
                query = query.Where(x => x.Name.Contains(queryVm.Keyword));
            }

            return query.OrderByDescending(x => x.UpdatedDate);
        }

        /// <summary>
        /// 取得 工程明細資料
        /// </summary>
        /// <returns></returns>
        public EngineeringDetailVm Detail(string governmentAenciesCode, string code)
        {
            var query = GetQuery()
                .Where(x => x.GovernmentAenciesCode == governmentAenciesCode && x.Code == code)
                .Select(x => new EngineeringDetailVm
                {
                    GovernmentAenciesCode = x.GovernmentAenciesCode,
                    Code = x.Code,
                    GovernmentAencies = x.GovernmentAencies,
                    Name = x.Name,
                    Amount = x.Amount,
                    Supervision = x.Supervision,
                    Factory = x.Factory,
                    Category = x.Category,
                    CityTown = x.CityTown,
                    Geom = x.Geom,
                    Address = x.Address,
                    Summary = x.Summary,
                    ActualStartDate = x.ActualStartDate,
                    CompleteDate = x.ChangeCompleteDate != null ? x.ChangeCompleteDate : x.TargetCompleteDate,
                    ProgressDate = x.ProgressDate,
                    TargetProgress = x.TargetProgress,
                    ActualProgress = x.ActualProgress,
                    Discrepancy = x.Discrepancy,
                    Status = x.Status,
                    BehindReason = x.BehindReason,
                    Analysis = x.Analysis,
                    Solution = x.Solution,
                    ImproveTermDate = x.ImproveTermDate,
                    ActualCompleteDate = x.ActualCompleteDate,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    UpdatedBy = x.UpdatedBy,
                    UpdatedDate = x.UpdatedDate,
                    ProgressText = x.Status == "停工中" ? "停工中" : x.Status == "解約" ? "解約" : x.Discrepancy >= 0 ? "進度正常無落後" : "進度落後",
                });

            return query.SingleOrDefault();
        }

        /// <summary>
        /// 取得 EngineeringVm
        /// </summary>
        /// <param name="query">Engineering Queryable</param>
        /// <returns>EngineeringVm</returns>
        private IQueryable<EngineeringVm> GetEngineeringVm(IQueryable<Engineering> query)
        {
            return query.Select(x => new EngineeringVm
            {
                GovernmentAenciesCode = x.GovernmentAenciesCode,
                Code = x.Code,
                Name = x.Name,
                CityTown = x.CityTown,
                Category = x.Category,
                Discrepancy = x.Discrepancy,
                ProgressText = x.Discrepancy >= 0 ? "進度正常無落後" : "進度落後",
                Amount = x.Amount,
                Geom = x.Geom,
                Status = x.Status,
                ActualStartDate = x.ActualStartDate,

                Address = x.Address,
            });


        }

        /// <summary>
        /// 取得 行政區
        /// </summary>
        /// <returns></returns>
        public List<CodeName> GetTown()
        {
            var query = GetQuery().Select(x => x.CityTown).Distinct().ToList();

            return query.Select(x => new CodeName
            {
                Code = x,
                Name = x == "台南市" ? "全市" : x.Replace("台南市", ""),
            }).ToList();
        }

        /// <summary>
        /// 取得 工程類別
        /// </summary>
        /// <returns></returns>
        public List<CodeName> GetCategory()
        {
            var query = GetQuery().Select(x => x.Category).Distinct().ToList();

            return query.Select(x => new CodeName
            {
                Code = x,
                Name = x,
            }).ToList();
        }

        /// <summary>
        /// 取得 工程進度
        /// </summary>
        /// <returns></returns>
        public List<CodeName> GetProgressText()
        {
            return new List<CodeName>
            {
                new CodeName{
                    Code = "",
                    Name = "全部",
                },
                new CodeName{
                    Code = "進度正常無落後",
                    Name = "進度正常無落後",
                },
                new CodeName{
                    Code = "進度落後",
                    Name = "進度落後",
                },
            };
        }

        ///// <summary>
        ///// 取得 工程狀態
        ///// </summary>
        ///// <returns></returns>
        //public List<CodeName> GetStatus()
        //{
        //    var query = GetQuery().Select(x => x.Status).Distinct().ToList();

        //    return query.Select(x => new CodeName
        //    {
        //        Code = x,
        //        Name = x,
        //    }).ToList();
        //}

        /// <summary>
        /// 更新 Engineering
        /// </summary>
        /// <param name="csvMessage"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public int UpgradeEngineering(string fileName, string csvMessage, string userId)
        {
            List<Engineering> engineerings = GetEngineeringByCsv(csvMessage, userId);

            int updateCount = 0;
            foreach (var engineering in engineerings)
            {
                var query = dbModel.Engineerings.Where(x => x.Code == engineering.Code && x.GovernmentAenciesCode == engineering.GovernmentAenciesCode);
                if (query.Any() == false)
                {
                    dbModel.Engineerings.Add(engineering);
                }
                else
                {
                    var dbEngineering = query.Single();
                    
                    dbEngineering.GovernmentAencies = engineering.GovernmentAencies;
                    dbEngineering.Name = engineering.Name;
                    dbEngineering.Amount = engineering.Amount;
                    dbEngineering.Supervision = engineering.Supervision;
                    dbEngineering.Factory = engineering.Factory;
                    dbEngineering.Category = engineering.Category;
                    dbEngineering.CityTown = engineering.CityTown;
                    dbEngineering.Geom = engineering.Geom;
                    dbEngineering.Address = engineering.Address;
                    dbEngineering.Summary = engineering.Summary;
                    dbEngineering.ActualStartDate = engineering.ActualStartDate;
                    dbEngineering.TargetCompleteDate = engineering.TargetCompleteDate;
                    dbEngineering.ChangeCompleteDate = engineering.ChangeCompleteDate;
                    dbEngineering.ProgressDate = engineering.ProgressDate;
                    dbEngineering.TargetProgress = engineering.TargetProgress;
                    dbEngineering.ActualProgress = engineering.ActualProgress;
                    dbEngineering.Discrepancy = engineering.Discrepancy;
                    dbEngineering.Status = engineering.Status;
                    dbEngineering.BehindReason = engineering.BehindReason;
                    dbEngineering.Analysis = engineering.Analysis;
                    dbEngineering.Solution = engineering.Solution;
                    dbEngineering.ImproveTermDate = engineering.ImproveTermDate;
                    dbEngineering.ActualCompleteDate = engineering.ActualCompleteDate;
                    dbEngineering.IsEnabled = engineering.IsEnabled;
                    dbEngineering.UpdatedBy = engineering.UpdatedBy;
                    dbEngineering.CreatedBy = engineering.CreatedBy;
                }

                updateCount += dbModel.SaveChanges();
            }

            dbModel.EngineeringLogs.Add(new EngineeringLog
            {
                LogMessage = csvMessage,
                FileName = fileName,
                CreatedBy = userId,
                CreatedDate = DateTime.Now,
                IsEnabled = true,
            });
            dbModel.SaveChanges();

            return updateCount;
        }

        /// <summary>
        /// 由 CSV 取得Engineering
        /// </summary>
        /// <param name="csvMessage"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private List<Engineering> GetEngineeringByCsv(string csvMessage, string userId)
        {
            using (var stringReader = new StringReader(csvMessage))
            using (var csvHelper = new CsvReader(stringReader))
            {
                csvHelper.Configuration.RegisterClassMap(new EngineeringCsvMap());
                var records = csvHelper.GetRecords<EngineeringCsvVm>().ToList();

                var engineerings = new List<Engineering>();

                foreach (var record in records)
                {
                    int index = records.IndexOf(record);
                    try
                    {
                        Engineering engineering = new Engineering();
                        engineering.GovernmentAenciesCode = record.GovernmentAenciesCode;
                        engineering.Code = record.Code;
                        engineering.GovernmentAencies = record.GovernmentAencies;
                        engineering.Name = record.Name;
                        engineering.Amount = decimal.Parse(record.Amount.Replace(",",""));
                        engineering.Supervision = record.Supervision;
                        engineering.Factory = record.Factory;
                        engineering.Category = record.Category;
                        engineering.CityTown = record.CityTown;
                        engineering.Geom = DbGeometry.FromText($"POINT({record.X} {record.Y})");
                        engineering.Address = record.Address;
                        engineering.Summary = record.Summary;
                        engineering.ActualStartDate = GetDate(record.ActualStartDate).Value;
                        engineering.TargetCompleteDate = string.IsNullOrEmpty(record.TargetCompleteDate) ? null : GetDate(record.TargetCompleteDate); 
                        engineering.ChangeCompleteDate = string.IsNullOrEmpty(record.ChangeCompleteDate) ? null : GetDate(record.ChangeCompleteDate);
                        engineering.ProgressDate = GetDate(record.ProgressDate).Value;
                        engineering.TargetProgress = decimal.Parse(record.TargetProgress);
                        engineering.ActualProgress = decimal.Parse(record.ActualProgress);
                        engineering.Discrepancy = decimal.Parse(record.Discrepancy);
                        engineering.Status = record.Status;
                        engineering.BehindReason = record.BehindReason;
                        engineering.Analysis = record.Analysis;
                        engineering.Solution = record.Solution;
                        engineering.ImproveTermDate = string.IsNullOrEmpty(record.ImproveTermDate) ? null : GetDate(record.ImproveTermDate);
                        engineering.ActualCompleteDate = string.IsNullOrEmpty(record.ActualCompleteDate) ? null : GetDate(record.ActualCompleteDate);

                        engineering.IsEnabled = true;
                        engineering.UpdatedBy = userId;
                        engineering.CreatedBy = userId;

                        engineerings.Add(engineering);
                    }
                    catch (Exception e)
                    {
                        throw new Exception($"第{index+2}列，{e.Message}");
                    }
                }

                return engineerings;
            }
        }

        /// <summary>
        /// 民國年轉西元年
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private DateTime? GetDate(string date)
        {
            date = date.Replace("/", "").Trim();
            if (Regex.IsMatch(date, @"^\d{5}$"))
            {
                date += "01";
            }
            date = date.PadLeft(8, '0');
            var ci = new CultureInfo("zh-TW", true);
            ci.DateTimeFormat.Calendar = new TaiwanCalendar();
            return DateTime.ParseExact(date, "yMMdd", ci);
        }
        
        private static IGeometryObject GetIGeometry(DbGeometry geom)
        {
            // 利用 Geo.Grid.Common.Transformation.ToWgs84 轉換成經緯度
            var wgs84Point = Transformation.ToWgs84(geom.XCoordinate.Value, geom.YCoordinate.Value);

            //var geog = SqlGeometry.STGeomFromWKB(new SqlBytes(geom.AsBinary())
            //                                            , geom.CoordinateSystemId);

            var geog = SqlGeometry.Point(wgs84Point.X, wgs84Point.Y, geom.CoordinateSystemId);

            return MsSqlSpatialConvert.ToGeoJSONGeometry(geog);
        }

        /// <summary>
        /// 取得 清單
        /// </summary>
        /// <returns></returns>
        public IQueryable<EngineeringLogVm> GetEngineeringLogs()
        {
            return dbModel.EngineeringLogs.Where(x => x.IsEnabled).Select(x=>new EngineeringLogVm {
                LogId = x.LogId,
                LogMessage = x.LogMessage,
                FileName = x.FileName,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
            }).OrderByDescending(x => x.CreatedDate);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Result<string> Remove(long id)
        {
            var entity = dbModel.EngineeringLogs.Where(x => x.IsEnabled && x.LogId == id).Single();
            entity.IsEnabled = false;
            Result<string> res = new Result<string>();
            res.Success = dbModel.SaveChanges() > 0;
            return res;
        }

        /// <summary>
        /// 下載原始的 CSV資料
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        public EngineeringLogVm DownloadCsv(int logId)
        {
            return GetEngineeringLogs().Where(x => x.LogId == logId).FirstOrDefault();
        }

        /// <summary>
        /// 下載 市政監督 工程標案
        /// </summary>
        /// <param name="filePath">檔案路徑</param>
        /// <returns></returns>
        public byte[] DownloadEngineering()
        {
            try
            {
                var engineeringCsv = GetEngineeringCsvVm();
                
                using (var stream = new MemoryStream())
                using (var streamWriter = new StreamWriter(stream))
                using (var csvWriter = new CsvWriter(streamWriter))
                {
                    csvWriter.Configuration.Delimiter = ",";
                    csvWriter.Configuration.HasHeaderRecord = true;
                    csvWriter.Configuration.RegisterClassMap(new EngineeringCsvMap());

                    csvWriter.WriteRecords<EngineeringCsvVm>(engineeringCsv);
                    csvWriter.Flush();
                    
                    return stream.ToArray();
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }

        /// <summary>
        /// 取得 工程標案 清單
        /// </summary>
        /// <returns></returns>
        private List<EngineeringCsvVm> GetEngineeringCsvVm()
        {
            var query = dbModel.Engineerings.AsEnumerable().Select(x => new EngineeringCsvVm()
            {
                GovernmentAenciesCode = x.GovernmentAenciesCode,
                Code = x.Code,
                GovernmentAencies = x.GovernmentAencies,
                Name = x.Name,
                Amount = x.Amount.ToString(),
                Supervision = x.Supervision,
                Factory = x.Factory,
                Category = x.Category,
                CityTown = x.CityTown,
                X = x.Geom == null ? "" : x.Geom.XCoordinate.ToString(),
                Y = x.Geom == null ? "" : x.Geom.YCoordinate.ToString(),
                Address = x.Address,
                Summary = x.Summary,
                ActualStartDate = x.ActualStartDate.ToString("yyyy-MM-dd"),
                TargetCompleteDate = x.TargetCompleteDate.HasValue ? x.TargetCompleteDate.Value.ToString("yyyy-MM-dd") : "",
                ChangeCompleteDate = x.ChangeCompleteDate.HasValue ? x.ChangeCompleteDate.Value.ToString("yyyy-MM-dd") : "",
                ProgressDate = x.ProgressDate.ToString("yyyy-MM-dd"),
                TargetProgress = x.TargetProgress.ToString(),
                ActualProgress = x.ActualProgress.ToString(),
                Discrepancy = x.Discrepancy.ToString(),
                Status = x.Status,
                BehindReason = x.BehindReason,
                Analysis = x.Analysis,
                Solution = x.Solution,
                ImproveTermDate = x.ImproveTermDate.HasValue ? x.ImproveTermDate.Value.ToString("yyyy-MM-dd") : "",
                ActualCompleteDate = x.ActualCompleteDate.HasValue ? x.ActualCompleteDate.Value.ToString("yyyy-MM-dd") : "",
            });

            return query.ToList();
        }
    }
}