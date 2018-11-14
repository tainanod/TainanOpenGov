(function () {
    'use strict';
    // Initialize Firebase
    var config = {
        apiKey: "AIzaSyDfpp9CgL9FhL_TAAxxBMWPF1cEXcwKYF0",
        authDomain: "apitest-fe10a.firebaseapp.com",
        databaseURL: "https://apitest-fe10a.firebaseio.com",
        storageBucket: "apitest-fe10a.appspot.com",
        messagingSenderId: "920238903964"
    };
    firebase.initializeApp(config);

    function datasetService($http) {
        return {
            //getList: function (param) {
            //    return firebase.database().ref("/dataSet/List/");
            //},
            //getDetail: function (datasetId) {
            //    return firebase.database().ref("/dataSet/Detail/" + datasetId);
            //},
            //getType: function () {
            //    return firebase.database().ref("/dataSet/TypeSeq/");
            //},
            getList: function (param) {
                return $http.post(window.appRoot.rel + "Api/DataSet/List", param, config);
            },
            getDetail: function (datasetId) {
                return $http.get(window.appRoot.rel + "Api/DataSet/Detail/" + datasetId);
            },
            getType: function () {
                return $http.get(window.appRoot.rel + "Api/DataSet/Type");
            },
            getTags: function () {
                return $http.get(window.appRoot.rel + "Api/Discuss/Tags");
            }
        };
    }

    var config = {
        headers: {
            "Content-Type": "application/json"
        }
    };
    angular.module("app").factory("datasetService", datasetService);
    datasetService.$inject = ["$http"];
})();