(function () {
    'use strict';

    //var firebaseConfig = {
    //    apiKey: "AIzaSyDfpp9CgL9FhL_TAAxxBMWPF1cEXcwKYF0",
    //    authDomain: "apitest-fe10a.firebaseapp.com",
    //    databaseURL: "https://apitest-fe10a.firebaseio.com",
    //    storageBucket: "apitest-fe10a.appspot.com",
    //    messagingSenderId: "920238903964"
    //};
    //firebase.initializeApp(firebaseConfig);

    function homeService($http) {
        return {
            getShowcaseList: function (param) {                
                return $http.post(window.appRoot.rel + "Api/ShowCase/List", param, config);
            },
            getBannerList: function () {
                return $http.get(window.appRoot.rel + "Api/Banner/List");
                //return firebase.database().ref("/home/banner/");
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
    angular.module("app").factory("homeService", homeService);
    homeService.$inject = ["$http"];
})();