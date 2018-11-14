(function () {
    'use strict';

    function engineeringService($http) {
        return {
            //地圖關鍵字搜尋
            queryMap: function(param) {
                return $http.post(window.appRoot.rel + "Api/Engineering/Query", param, config);
            },
            //地圖進階搜尋
            advQueryMap: function(param) {
                return $http.post(window.appRoot.rel + "Api/Engineering/AdvQuery", param, config);
            },
            //取得工程詳細資料
            getDetail: function (governmentAenciesCode , code) {
                return $http.get(window.appRoot.rel + "Api/Engineering/Detail/"+ governmentAenciesCode + "/" + code);
            },
            // 取得 行政區
            getTown: function() {
                return $http.get(window.appRoot.rel + "Api/Engineering/GetTown")
            },
            //取得 工程類別
            getCategory: function() {
                return $http.get(window.appRoot.rel + "Api/Engineering/GetCategory")
            },
            // 取得 狀態
            getStatus: function() {
                return $http.get(window.appRoot.rel + "Api/Engineering/GetStatus")
            },
            // 取得 工程進度
            getProgressText: function() {
                return $http.get(window.appRoot.rel + "Api/Engineering/GetProgressText")
            },
        };
    }

    var config = {
        headers: {
            "Content-Type": "application/json"
        }
    };
    angular.module("app").factory("engineeringService", engineeringService);
    engineeringService.$inject = ["$http"];
})();