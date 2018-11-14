(function () {
    'use strict';
    var config = { header: { "content-type": "application/json" } };

    function discussService($http) {
        return {
            getForum: function (param) {
                return $http.get(window.appRoot.rel + "api/Participation/get/" + param);
            },
            getDiscuss: function (param) {
                return $http.post(window.appRoot.rel + "Api/ParticipationDiscuss/GetDiscuss", param, config);
            },
            
            getUser: function () {
                return $http.get(window.appRoot.rel + "Api/ParticipationDiscuss/User");
            },
            getTags: function (id) {
                return $http.get(window.appRoot.rel + "Api/ParticipationDiscuss/Tags/" + id);
            },

            pushDiscuss: function (discussId, pushUserId) {
                return $http.put(window.appRoot.rel + "Api/ParticipationDiscuss/PushMessage?discussId=" + discussId + "&pushUserId=" + pushUserId);
            },

            getReply: function (param) {
                return $http.get(window.appRoot.rel + "Api/ParticipationDiscuss/GetReply/" + param);
            },

            sendReply: function (param) {
                return $http.post(window.appRoot.rel + "Api/ParticipationDiscuss/CreateDiscuss", param, config);
            },
        };
    }

    angular.module("app").factory("discussService", discussService);
    discussService.$inject = ["$http"];
})();