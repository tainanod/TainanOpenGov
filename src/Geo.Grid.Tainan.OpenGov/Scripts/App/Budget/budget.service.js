//(function () {
//    'use strict';

//    function budgetService($http) {
//        return {
//            getList: function (currentPage, pageSize, colName, orderby, year, kind) {
//                var drilldown = kind === "tainan01" ? "sui_ru_lai_yuan_bian_hao.sui_ru_lai_yuan_bian_hao" : "zui_gao_ji_bian_hao.zui_gao_ji_bian_hao";
//                var url = "http://openbudget.tainan.gov.tw/api/3/cubes/3ba74c66ee35cf7955bb6af4c86aecee:" + kind + "/facts/?aggregates=ben_nian_du_yu_suan_shu.sum&";
//                if (colName && orderby) url += "order=" + colName + ":" + orderby + "&";
//                url += "page=" + currentPage + "&pagesize=" + pageSize + "&cut=nian_du.nian_du:" + year + "&drilldown=" + drilldown;
//                return $http.get(url);
//            },
//        };
//    }

//    var config = {
//        headers: {
//            "Content-Type": "application/json"
//        }
//    };
//    angular.module("app").factory("budgetService", budgetService);
//    budgetService.$inject = ["$http"];
//})();
(function () {
    'use strict';

    function budgetService($http) {
        return {
            getList: function (currentPage, pageSize, colName, orderby, year, kind) {
                var drilldown = kind === "taiana01" ? "sui_ru_lai_yuan_bian_hao.sui_ru_lai_yuan_bian_hao" : "zui_gao_ji_bian_hao.zui_gao_ji_bian_hao";
                var url = "https://openspending.org/api/3/cubes/3ba74c66ee35cf7955bb6af4c86aecee:" + kind + "/facts/?";
                if (colName && orderby) url += "order=" + colName + ":" + orderby + "&";
                url += "page=" + currentPage + "&pagesize=" + pageSize + "&cut=nian_du.nian_du:" + year + "&drilldown=" + drilldown;
                return $http.get(url);
            },
        };
    }

    var config = {
        headers: {
            "Content-Type": "application/json"
        }
    };
    angular.module("app").factory("budgetService", budgetService);
    budgetService.$inject = ["$http"];
})();