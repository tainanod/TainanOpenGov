(function () {
    'use strict';

    function welfareService($http) {
        return {
            getList: function (param) {
                return $http.post(window.appRoot.rel + "Api/Allowance/AdvQuery", param, config);
            },
            getListAll: function (param) {
                return $http.post(window.appRoot.rel + "Api/Allowance/Query", param, config);
            },
            getDetail: function (allowanceId) {
                return $http.get(window.appRoot.rel + "Api/Allowance/Detail/" + allowanceId);
            },
            getDistrictOffice: function () {
                return $http.get(window.appRoot.rel + "Api/Allowance/GetDistrictOffice");
            },
            getOthers: function () {
                return $http.get(window.appRoot.rel + "Api/Allowance/GetOthers");
            },
            getIdentity: function () {
                return $http.get(window.appRoot.rel + "Api/Allowance/GetIdentity");
            }
        };
    }

    var config = {
        headers: {
            "Content-Type": "application/json"
        }
    };
    angular.module("app").factory("welfareService", welfareService);
    welfareService.$inject = ["$http"];
})();