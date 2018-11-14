//(function () {
//    'use strict';
//    /*
//     * 臺南市政府歷年預算資料
//     */
//    function budgetController($scope, $sce, $timeout, budgetService) {
//        $scope.vm = {
//            kindModal: [
//                {
//                    name: "歲入來源別預算表(經常門)",
//                    code: "tainan01"
//                },
//                {
//                    name: "歲入來源別預算表(資本門)",
//                    code: "tainan02"
//                },
//                {
//                    name: "歲出政事別預算表(經常門)",
//                    code: "tainan03"
//                },
//                {
//                    name: "歲出政事別預算表(資本門)",
//                    code: "tainan04"
//                },
//                {
//                    name: "歲出機關別預算表",
//                    code: "taiana05"
//                },
//            ],
//            yearModal: [
//                {
//                    name: "107年度",
//                    code: "107"
//                },
//                {
//                    name: "106年度",
//                    code: "106"
//                },
//                {
//                    name: "105年度",
//                    code: "105"
//                },
//                {
//                    name: "104年度",
//                    code: "104"
//                },
//                {
//                    name: "103年度",
//                    code: "103"
//                },
//            ],
//            chartModal: [
//                {
//                    name: "泡泡圖",
//                    code: "bubbletree"
//                },
//                {
//                    name: "鳥瞰圖",
//                    code: "treemap"
//                },
//                {
//                    name: "桑基圖",
//                    code: "sankey"
//                },
//            ],
//            optionModel: [10, 20, 50, 100],
//            currentChartName: "bubbletree",
//            currentYear: '107',
//            currentKind: 'tainan01',
//            currentChartUrl: null,
//            list: [],
//            iframeHeight: '600px'
//        };

//        $scope.getIframeHeight = function () {
//            switch ($scope.vm.currentChartName) {
//                case "treemap":
//                    $scope.vm.iframeHeight = '910px';
//                    break;
//                case "sankey":
//                    $scope.vm.iframeHeight = '750px';
//                    break;
//                default:
//                    $scope.vm.iframeHeight = '600px';
//                    break;
//            }
//        }

//        $scope.page = {
//            currentPage: 1,
//            pageSize: 10,
//            totalRecord: 10,
//            totalPages: 1,
//            orderby: '',
//            colName: ''
//        };

//        /*
//         * 金錢三位一撇
//         */
//        $scope.moneyFormat = function (money) {
//            // References: https://goo.gl/4XfhY1
//            if (money) {
//                // 取千元
//                money = Math.round(money / 1000);
//                return money.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
//            } else {
//                return 0;
//            }
//        };

//        /*
//         * 預算類型選擇觸發
//         */
//        $scope.changeCurrentKind = function () {
//            $scope.getChart();
//            $scope.getList();
//        }

//        /*
//         * 年度選擇觸發
//         */
//        $scope.changeCurrentYear = function () {
//            $scope.getChart();
//            $scope.getList();
//        }
        

//        /*
//         * 表格排序
//         */
//        $scope.setSort = function (colName, orderby) {
//            //debugger;
//            $scope.page.orderby = orderby;
//            $scope.page.colName = colName;
//            $scope.getList();
//        }

//        /*
//         * 表格分頁
//         */
//        $scope.pageChange = function () {
//            $scope.getList();
//        }

//        /*
//         * 切換圖表tab
//         */
//        $scope.switchChartName = function (chartName) {
//            $('#imgLoading').show();
//            $scope.vm.currentChartName = chartName;
//            $scope.getChart();
//        }

//        /*
//         * 取得圖表
//         */
//        $scope.getChart = function () {
//            $('#imgLoading').show();
//            $scope.getIframeHeight();
//            var group = $scope.vm.currentKind === 'tainan01' ? '%22sui_ru_lai_yuan_bian_hao.sui_ru_lai_yuan_bian_hao%22' : '%22zui_gao_ji_bian_hao.zui_gao_ji_bian_hao%22';
//            var url = 'http://openbudget.tainan.gov.tw/viewer/embed/' + $scope.vm.currentChartName + '/3ba74c66ee35cf7955bb6af4c86aecee:' + $scope.vm.currentKind + '?measure=%22ben_nian_du_yu_suan_shu.sum%22&groups%5B%5D=' + group +'&filters%5Bnian_du.nian_du%5D%5B%5D=' + $scope.vm.currentYear + '&order=%22ben_nian_du_yu_suan_shu.sum%7Cdesc%22&lang=en';

//            $scope.vm.currentChartUrl = url;
//            $('#imgLoading').hide();
//        }


//        /*
//         * 取得表單資料
//         */
//        $scope.getList = function () {
//            var currentPage = $scope.page.currentPage,
//                pageSize = $scope.page.pageSize,
//                orderby = $scope.page.orderby,
//                colName = $scope.page.colName,
//                year = $scope.vm.currentYear,
//                kind = $scope.vm.currentKind;

//            budgetService.getList(currentPage, pageSize, colName, orderby, year, kind).success(function (ret) {
//                if (!ret) {
//                    console.log('no data')
//                    return;
//                }
//                $scope.vm.list = ret.data;
//                $scope.page.totalRecord = ret.total_fact_count;
//                $scope.page.totalPages = Math.ceil($scope.page.totalRecord / $scope.page.pageSize);
//            });
//        }


//        /*
//         * 頁面初始化
//         */
//        function activate() {
//            $scope.getChart();
//            $scope.getList();
//        }

//        /*初始化 (延遲0.5秒)*/
//        $timeout(activate, 300);

//    }
//    angular.module("app").controller("budgetController", budgetController).filter("toLower", function () {
//        return function (str) {
//            return str.toLowerCase();
//        };
//    }).filter("trust", ['$sce', function ($sce) {
//        return function (htmlCode) {
//            if (htmlCode != null) {
//                htmlCode = htmlCode.replace(/(\r\n|\r|\n)/g, '<br />');
//            }
//            return $sce.trustAsHtml(htmlCode);
//        }
//    }]).filter("limitMax", function () {
//        return function (str) {
//            return str = str > 120 ? 120 : str;
//        };
//    }).directive('ngEnter', function () {
//        return function (scope, element, attrs) {
//            element.bind("keydown keypress", function (event) {
//                if (event.which === 13) {
//                    scope.$apply(function () {
//                        scope.$eval(attrs.ngEnter);
//                    });
//                    event.preventDefault();
//                }
//            });
//            };
//        }).filter("trustUrl", ['$sce', function ($sce) {
//            return function (url) {
//                if (url !== null) {
//                    return $sce.trustAsResourceUrl(url)
//                }
//            }
//        }]);

//    budgetController.$inject = ["$scope", "$sce", "$timeout", "budgetService"];
//})();
(function () {
    'use strict';
    /*
     * 臺南市政府歷年預算資料
     */
    function budgetController($scope, $sce, $timeout, budgetService) {
        $scope.vm = {
            kindModal: [
                {
                    name: "歲入來源別預算表(經常門)",
                    code: "taiana01"
                },
                {
                    name: "歲入來源別預算表(資本門)",
                    code: "taiana02"
                },
                {
                    name: "歲出政事別預算表(經常門)",
                    code: "tainan03"
                },
                {
                    name: "歲出政事別預算表(資本門)",
                    code: "taiana04"
                },
                {
                    name: "歲出機關別預算表",
                    code: "taiana05"
                },
            ],
            yearModal: [
                {
                    name: "107年度",
                    code: "107"
                },
                {
                    name: "106年度",
                    code: "106"
                },
                {
                    name: "105年度",
                    code: "105"
                },
                {
                    name: "104年度",
                    code: "104"
                },
                {
                    name: "103年度",
                    code: "103"
                },
            ],
            chartModal: [
                {
                    name: "泡泡圖",
                    code: "bubbletree"
                },
                {
                    name: "鳥瞰圖",
                    code: "treemap"
                },
                {
                    name: "桑基圖",
                    code: "sankey"
                },
            ],
            optionModel: [10, 20, 50, 100],
            currentChartName: "bubbletree",
            currentYear: '107',
            currentKind: 'taiana01',
            currentChartUrl: null,
            list: [],
            iframeHeight: '600px'
        };

        $scope.getIframeHeight = function () {
            switch ($scope.vm.currentChartName) {
                case "treemap":
                    $scope.vm.iframeHeight = '910px';
                    break;
                case "sankey":
                    $scope.vm.iframeHeight = '750px';
                    break;
                default:
                    $scope.vm.iframeHeight = '600px';
                    break;
            }
        }

        $scope.page = {
            currentPage: 1,
            pageSize: 10,
            totalRecord: 10,
            totalPages: 1,
            orderby: '',
            colName: ''
        };

        /*
         * 金錢三位一撇
         */
        $scope.moneyFormat = function (money) {
            // References: https://goo.gl/4XfhY1
            if (money) {
                // 取千元
                money = Math.round(money / 1000);
                return money.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            } else {
                return 0;
            }
        };

        /*
         * 預算類型選擇觸發
         */
        $scope.changeCurrentKind = function () {
            $scope.getChart();
            $scope.getList();
        }

        /*
         * 年度選擇觸發
         */
        $scope.changeCurrentYear = function () {
            $scope.getChart();
            $scope.getList();
        }


        /*
         * 表格排序
         */
        $scope.setSort = function (colName, orderby) {
            //debugger;
            $scope.page.orderby = orderby;
            $scope.page.colName = colName;
            $scope.getList();
        }

        /*
         * 表格分頁
         */
        $scope.pageChange = function () {
            $scope.getList();
        }

        /*
         * 切換圖表tab
         */
        $scope.switchChartName = function (chartName) {
            $('#imgLoading').show();
            $scope.vm.currentChartName = chartName;
            $scope.getChart();
        }

        /*
         * 取得圖表
         */
        $scope.getChart = function () {
            $('#imgLoading').show();
            $scope.getIframeHeight();
            var group = $scope.vm.currentKind === 'taiana01' ? '%22sui_ru_lai_yuan_bian_hao.sui_ru_lai_yuan_bian_hao%22' : '%22zui_gao_ji_bian_hao.zui_gao_ji_bian_hao%22';
            var url = 'https://openspending.org/viewer/embed/' + $scope.vm.currentChartName + '/3ba74c66ee35cf7955bb6af4c86aecee:' + $scope.vm.currentKind + '?measure=%22ben_nian_du_yu_suan_shu.sum%22&groups%5B%5D=' + group + '&filters%5Bnian_du.nian_du%5D%5B%5D=' + $scope.vm.currentYear + '&order=%22ben_nian_du_yu_suan_shu.sum%7Cdesc%22&lang=en';

            $scope.vm.currentChartUrl = url;
            $('#imgLoading').hide();
        }

        /*
         * 取得表單資料
         */
        $scope.getList = function () {
            //debugger;
            var currentPage = $scope.page.currentPage,
                pageSize = $scope.page.pageSize,
                orderby = $scope.page.orderby,
                colName = $scope.page.colName,
                year = $scope.vm.currentYear,
                kind = $scope.vm.currentKind;

            budgetService.getList(currentPage, pageSize, colName, orderby, year, kind).success(function (ret) {
                if (!ret) {
                    console.log('no data')
                    return;
                }
                $scope.vm.list = ret.data;
                $scope.page.totalRecord = ret.total_fact_count;
                $scope.page.totalPages = Math.ceil($scope.page.totalRecord / $scope.page.pageSize);
            });
        }


        /*
         * 頁面初始化
         */
        function activate() {
            $scope.getChart();
            $scope.getList();
        }

        /*初始化 (延遲0.5秒)*/
        $timeout(activate, 300);

    }
    angular.module("app").controller("budgetController", budgetController).filter("toLower", function () {
        return function (str) {
            return str.toLowerCase();
        };
    }).filter("trust", ['$sce', function ($sce) {
        return function (htmlCode) {
            if (htmlCode != null) {
                htmlCode = htmlCode.replace(/(\r\n|\r|\n)/g, '<br />');
            }
            return $sce.trustAsHtml(htmlCode);
        }
    }]).filter("limitMax", function () {
        return function (str) {
            return str = str > 120 ? 120 : str;
        };
    }).directive('ngEnter', function () {
        return function (scope, element, attrs) {
            element.bind("keydown keypress", function (event) {
                if (event.which === 13) {
                    scope.$apply(function () {
                        scope.$eval(attrs.ngEnter);
                    });
                    event.preventDefault();
                }
            });
        };
    }).filter("trustUrl", ['$sce', function ($sce) {
        return function (url) {
            if (url !== null) {
                return $sce.trustAsResourceUrl(url)
            }
        }
    }]);

    budgetController.$inject = ["$scope", "$sce", "$timeout", "budgetService"];
})();