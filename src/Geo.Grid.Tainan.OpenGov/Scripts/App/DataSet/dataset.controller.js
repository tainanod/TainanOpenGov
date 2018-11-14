(function () {
    'use strict';

    function datasetController($scope, $sce, $timeout, datasetService) {
        $scope.vm = {
            list: [],
            tagList: [],
            optionModel: [10, 20, 50, 100],
            types: [],
            detail: null,
            popGuide: null
        };

        $scope.page = {
            currentPage: 1,
            pageSize: 10,
            totalRecord: 10,
            totalPages: 1
        };

        //add類別查詢
        $scope.getTypeSeq = function (typeSeq) {            
            if ($scope.vm.filteredType) {
                if ($scope.vm.filteredType[0].TypeId === typeSeq) {
                    $scope.resetTypeSeq();
                    return;
                } else {
                    $scope.vm.filteredType = $.grep($scope.vm.types, function (t) {
                        return t.TypeId === typeSeq
                    })
                }
            } else {
                $scope.vm.filteredType = $.grep($scope.vm.types, function (t) {
                    return t.TypeId === typeSeq
                })
            }
        }

        //取消類別查詢
        $scope.resetTypeSeq = function () {
            $scope.vm.filteredType = null;
        }

        //列表
        $scope.getCurrentList = function (currentPage, pageSize) {
            var param = {
                "CurrentPage": currentPage,
                "PageSize": pageSize
            }

            datasetService.getList(param).success(function (res) {
                if (!res) {
                    $('#imgLoading').hide();
                    console.log('no data')
                    return;
                }
                $scope.vm.list = res.Data;
                $scope.page.currentPage = res.CurrentPage;
                $scope.page.pageSize = res.PageSize;
                $scope.page.totalRecord = res.Total;
                $scope.page.totalPages = res.TotalPage;
                $('#imgLoading').hide();
            });
        };

        //分頁
        $scope.pageChange = function () {
            $scope.getCurrentList($scope.page.currentPage, $scope.page.pageSize);
        }

        function activate() {
            datasetService.getType().success(function (res) {                
                $scope.vm.types = res;
            })

            //datasetService.getTags().success(function (tags) {
            //    console.log(tags);
            //    $scope.vm.tagList = tags;
            //});

            $scope.getCurrentList();
        }

        /*初始化 (延遲0.5秒)*/
        $timeout(activate, 300);

    }
    angular.module("app").controller("datasetController", datasetController).filter("toLower", function () {
        return function (str) {
            return str.toLowerCase();
        };
    }).filter("trust", ['$sce', function ($sce) { return function (htmlCode) { return $sce.trustAsHtml(htmlCode); } }]);

    datasetController.$inject = ["$scope", "$sce", "$timeout", "datasetService"];
})();