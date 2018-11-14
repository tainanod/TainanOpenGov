using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;

namespace Geo.Grid.Tainan.OpenGov.Entity.ViewModel.Engineering
{
    public class EngineeringCsvMap : ClassMap<EngineeringCsvVm>
    {
        public EngineeringCsvMap()
        {
            Map(x => x.GovernmentAenciesCode).Name("執行機關代碼");
            Map(x => x.Code).Name("編號");
            Map(x => x.GovernmentAencies).Name("執行機關");
            Map(x => x.Name).Name("標案名稱");
            Map(x => x.Amount).Name("決標金額");
            Map(x => x.Supervision).Name("監造單位");
            Map(x => x.Factory).Name("得標廠商");
            Map(x => x.Category).Name("標案類別");
            Map(x => x.CityTown).Name("縣市鄉鎮");
            Map(x => x.X).Name("X座標");
            Map(x => x.Y).Name("Y座標");
            Map(x => x.Address).Name("詳細地點");
            Map(x => x.Summary).Name("工程概要");
            Map(x => x.ActualStartDate).Name("實際開工日期");
            Map(x => x.TargetCompleteDate).Name("原合約預定完工日");
            Map(x => x.ChangeCompleteDate).Name("變更後預定完工日");
            Map(x => x.ProgressDate).Name("進度月份");
            Map(x => x.TargetProgress).Name("預定進度%");
            Map(x => x.ActualProgress).Name("實際進度%");
            Map(x => x.Discrepancy).Name("差異%");
            Map(x => x.Status).Name("狀態");
            Map(x => x.BehindReason).Name("落後因素");
            Map(x => x.Analysis).Name("原因分析");
            Map(x => x.Solution).Name("解決辦法");
            Map(x => x.ImproveTermDate).Name("改進期限");
            Map(x => x.ActualCompleteDate).Name("實際完工日期");
        }
    }
}
