(function () {
    'use strict';
    function voteService($http) {
        return {
            getDetail: function (voteId) {
                return $http.get(window.appRoot.rel + "Api/Vote/Detail/" + voteId);
            },
            getCreate:function(data){
                return $http.post(window.appRoot.rel + "Vote/CreateSave", data, config);
            },
            //getCreate:function(data){
            //    return $http.post(window.appRoot.rel + "Api/Vote/Create", data, config);
            //},
            getCityList: function () {
                return $http.get(window.appRoot.rel + "Api/Vote/City");
            },
            getTownList: function () {
                return $http.get(window.appRoot.rel + "Api/Vote/CityTown");
            }

        };
    }

    var config = {
        headers: {
            "Content-Type": "application/json"
        }
    };
    angular.module("app").factory("voteService", voteService);
    voteService.$inject = ["$http"];
})();