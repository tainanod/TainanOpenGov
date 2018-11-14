﻿(function () {
    'use strict';
    function gatherService($http) {
        return {
            getDetail: function (voteId) {
                return $http.get(window.appRoot.rel + "Api/Question/Gather/" + voteId);
            }

        };
    }

    angular.module("app").factory("gatherService", gatherService);
    gatherService.$inject = ["$http"];
})();