(function () {
    'use strict';

    function gatherController($scope, $sce, $timeout, gatherService) {
        $scope.vm = {
            gatherArr: [],
            basicArr: []
        };


        $scope.getChartResult = function(result, $chartVal, $numVal) {
            result.forEach(function (jsonResult) {
                if (jsonResult.Categories.length > 0) {
                    var $categories = jsonResult.Categories,
                        $series = jsonResult.Series,
                    $total = jsonResult.Counts,
                    $counts = jsonResult.SetCounts;
                    //$counts = $series.reduce(function (a, b) { return a + b.y; }, 0);
                    $('#blockChart_' + jsonResult.PageId).highcharts({
                        chart: {
                            type: $chartVal
                        },
                        title: {
                            text: ''
                        },
                        subtitle: {
                            text: ''
                        },
                        xAxis: {
                            categories: $categories
                        },
                        yAxis: {
                            title: {
                                text: ''
                            }
                        },
                        legend: {
                            enabled: false
                        },
                        //plotOptions: {
                        //    column: {
                        //        dataLabels: {
                        //            enabled: true,
                        //            formatter: function () {
                        //                return getChartResultFormatter($chartVal, this.point.options.PageName, this.point.options.y, $numVal);
                        //            }
                        //        }
                        //    },
                        //    bar: {
                        //        dataLabels: {
                        //            enabled: true,
                        //            formatter: function () {
                        //                return getChartResultFormatter($chartVal, this.point.options.PageName, this.point.options.y, $numVal);
                        //            }
                        //        }
                        //    },
                        //    pie: {
                        //        dataLabels: {
                        //            enabled: true,
                        //            formatter: function () {
                        //                return getChartResultFormatter($chartVal, this.point.options.PageName, this.point.options.y, $numVal);
                        //            }
                        //        }
                        //    },
                        //    line: {
                        //        dataLabels: {
                        //            enabled: true,
                        //            formatter: function () {
                        //                return getChartResultFormatter($chartVal, this.point.options.PageName, this.point.options.y, $numVal);
                        //            }
                        //        }
                        //    }
                        //},
                        series: [{
                            name: '',
                            data: $series
                        }]
                    });
                }
            });
        }


        function activate() {
            gatherService.getDetail($scope.voteId).success(function (res) {

                $scope.vm.gatherArr = res;

                $timeout(function () {
                    $scope.getChartResult(res, 'column', 1);
                    $('#imgLoading').hide();
                    $('text.highcharts-credits').hide();
                }, 100)
                
            });

            gatherService.getBasic($scope.voteId).success(function (res) {
                $scope.vm.basicArr = res;
                $timeout(function () {
                    $scope.getChartResult(res, 'column', 1);
                    $('text.highcharts-credits').hide();
                }, 100)
            });
            
            
            


        }


        /*初始化 (延遲0.5秒)*/
        $timeout(function () {
            activate()
        }, 300);


    }
    angular.module("app").controller("gatherController", gatherController);
    gatherController.$inject = ["$scope", "$sce", "$timeout", "gatherService"];
})();