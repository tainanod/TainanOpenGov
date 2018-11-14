(function () {
    'use strict';
    function gatherService($http) {
        return {
            getDetail: function (voteId) {
                return $http.get(window.appRoot.rel + "Api/Vote/Gather/" + voteId);
            },
            getBasic: function (voteId) {
                return $http.get(window.appRoot.rel + "Api/Vote/Gather/Basic/" + voteId);
            }

        };
    }

    angular.module("app").factory("gatherService", gatherService);
    gatherService.$inject = ["$http"];
})();