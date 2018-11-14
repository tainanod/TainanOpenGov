(function () {
    'use strict';
    var config = { header: { "content-type": "application/json" } };

    function discussService($http) {
        return {
            getQuestionList: function (forumId) {                
                return $http.get(window.appRoot.rel + "Api/Question/Info/" + forumId);
            },

            getVoteList: function (forumId) {
                return $http.get(window.appRoot.rel + "Api/Vote/" + forumId);
            },

            getForum: function (param) {
                return $http.get(window.appRoot.rel + "api/forum/get/" + param);
            },
            getDiscuss: function (param) {
                return $http.post(window.appRoot.rel + "Api/Discuss/GetDiscuss", param, config);
            },
            //getDiscuss: function (forumId, currentPage, pageSize) {                
            //    return $http.post(window.appRoot.rel + "Api/Discuss/GetDiscuss/" + forumId + "?currentPage=" + currentPage + "&pageSize=" + pageSize, param, config);
            //},

            getSubForum: function (param) {
                return $http.get(window.appRoot.rel + "Api/Discuss/SubForum/" + param);
            },

            getUser: function () {
                return $http.get(window.appRoot.rel + "Api/Discuss/User");
            },
            getTags: function (id) {
                return $http.get(window.appRoot.rel + "Api/Discuss/Tags/" + id);
            },

            pushDiscuss: function (discussId, pushUserId) {
                return $http.put(window.appRoot.rel + "Api/Discuss/PushMessage?discussId=" + discussId + "&pushUserId=" + pushUserId);
            },

            getReply: function (param) {
                return $http.get(window.appRoot.rel + "Api/Discuss/GetReply/" + param);
            },

            sendReply: function (param) {
                return $http.post(window.appRoot.rel + "Api/Discuss/CreateDiscuss", param, config);
            },
        };
    }

    angular.module("app").factory("discussService", discussService);
    discussService.$inject = ["$http"];
})();