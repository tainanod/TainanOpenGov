(function () {
    'use strict';

    function homeController($scope, $sce, $timeout, homeService) {
        $scope.vm = {
            target: [],
            bannerList: []
        };

        $scope.page = {
            currentPage: 1,
            pageSize: 10,
            totalRecord: 10,
            totalPages: 1
        };




        $scope.getCurrentList = function (currentPage, pageSize) {
            var param = {
                "CurrentPage": currentPage,
                "PageSize": pageSize
            }
            homeService.getShowcaseList(param).success(function (res) {
                if (!res || !res.Data[0]) {
                    $('#ulShowcase .li-loading').hide();
                    $('#ulShowcase .home-showcase-li').hide();
                    $('#ulShowcase .no-li').show();
                    console.log('no data')
                    return;
                }
                $scope.vm.target = res.Data[0];
                $('#ulShowcase .li-loading').hide();
            });


        };


        function activate() {
            $scope.getCurrentList(1, 10);
            homeService.getBannerList().success(function (res) {
                $scope.vm.bannerList = res;
                if ($scope.vm.bannerList.length) {
                    var options = {
                        dots: false,
                        arrows: true,
                        infinite: true,
                        speed: 400,
                        slidesToShow: 1,
                        slidesToScroll: 1,
                        autoplay: false
                    };
                    $scope.vm.bannerList.length > 1 ? (options.autoplay = true) : (options.autoplay = false);
                    $timeout(function () {
                        $('#homePhotoWrapper').slick(options);
                    }, 3000);
                } else {
                    $('#homePhotoWrapper').hide();
                }
            });
        }

        /*初始化 (延遲0.5秒)*/
        $timeout(function () {
            activate()
        }, 300);
    }

    angular.module("app").controller("homeController", homeController);

    homeController.$inject = ["$scope", "$sce", "$timeout", "homeService"];
})();