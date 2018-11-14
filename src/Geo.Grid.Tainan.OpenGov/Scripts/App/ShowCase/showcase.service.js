(function () {
    'use strict';

    function showcaseService($http) {
        return {
            getList: function (param) {                
                return $http.post(window.appRoot.rel + "Api/ShowCase/List", param, config);
            },
            getDetail: function (param) {
                return $http.get(window.appRoot.rel + "Api/ShowCase/Detail/" + param);
            },
            getTags: function () {
                return $http.get(window.appRoot.rel + "Api/ShowCase/DataSet");
            },
            getCreate: function (param) {
                return $http.post(window.appRoot.rel + "ShowCase/Create", param, configImg);
            },
            getUserDetail:function(){
                return $http.post(window.appRoot.rel + "Api/ShowCase/UserDetail", config);
            }
        };
    }

    var config = {
        headers: {
            "Content-Type": "application/json"
        }
    };
    var configImg = {
        headers: {
            "Content-Type": undefined
        }
    };
    angular.module("app").factory("showcaseService", showcaseService);
    showcaseService.$inject = ["$http"];
})();