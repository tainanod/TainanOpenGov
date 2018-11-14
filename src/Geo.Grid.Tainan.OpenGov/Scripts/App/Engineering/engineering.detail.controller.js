(function () {
    'use strict';

    /*
     * 市政監督：Detail 頁
     */
    function engineeringController($scope, $sce, $timeout, engineeringService) {
        $scope.vm = {
            detail: null,
            mainClass: '',
            realStatus: ''
        };

        /*
         * 金錢三位一撇
         */
        $scope.moneyFormat = function (money) {
            // References: https://goo.gl/4XfhY1
            if (money) {
                return money.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            } else {
                return '無';
            }
        };

        /*
         * 日期轉換
         */
        $scope.dateConvert = function(d, onlyMonth) {
            if(d) {
                var result = onlyMonth ? (Number(d.slice(0, 4)) - 1911) + '年' + d.slice(5, 7) + '月' : (Number(d.slice(0, 4)) - 1911) + '年' + d.slice(5, 7) + '月' + d.slice(8, 10) + '日';
                return result;
            } else {
                return '無';
            }
        }

        /*
         * geometry 取出座標
         */
        $scope.geomConvert = function(geom) {
            if(geom) {
                return geom.Geometry.WellKnownText.split('(')[1].split(')')[0].replace(/ /,", ");
            } else {
                return '無';
            }
        }

        /*
         * 行政區名稱過濾
         */
        $scope.cityFormat = function(c) {
            if(c) {
                return c.match('區') ? c.split('市')[1] : c.replace(/臺南|台南/g, '全');
            } else {
                return '無';
            }
        }

        /*
         * 取得工程狀態
         */
        $scope.getRealStatus = function() {
            var result;
            switch($scope.vm.detail.Status) {
                case '停工中':
                    result = '停工中';
                    break; 
                case '解約重新發包':
                    result = '解約';
                    break; 
                default:
                    result = $scope.vm.detail.Discrepancy >= 0 ? '進度正常無落後' : '進度落後';
                    break; 
            };
            $scope.vm.realStatus = result;
        }

        $scope.setMainClass = function() {
            var result;
            switch($scope.vm.realStatus) {
                case '進度正常無落後':
                    result = 'progress-normal';
                    break;
                case '進度落後':
                    result = 'progress-delay';
                    break;
                case '停工中':
                    result = 'progress-stop';
                    break;
                case '解約':
                    result = 'progress-dismissal';
                    break;
                default:
                    result = 'progress-normal';
                    break;
            }
            $scope.vm.mainClass = result;
        }


        /*
         * 取得 Detail 內容
         */
        $scope.getDetail = function() {
            debugger;
            //?gacode=301021300G&code=05-C106-0201-6737-0-1
            if (window.location.search) {
                var tempPath = window.location.search.split('?gacode=')[1].split('&code='),
                    governmentAenciesCode = tempPath[0],
                    code = tempPath[1];
                sessionStorage.governmentAenciesCode = governmentAenciesCode;
                sessionStorage.code = code;
            }
            engineeringService.getDetail(sessionStorage.governmentAenciesCode, sessionStorage.code).success(function (res) {
                if (!res) {
                    console.log('no data')
                    return;
                }
                console.log(res);
                $scope.vm.detail = res;
                $scope.getRealStatus();
                $scope.setMainClass();
                $('#imgLoading').hide();
            })
        };

        $scope.goBack = function (isShowMap) {
            sessionStorage.isShowMap = isShowMap ? true : false;
            //window.history.back();
            window.location.href = window.appRoot.rel + 'Engineering';
        }

        function activate() {
            sessionStorage.isShowMap = false;
            $scope.getDetail($scope.allowanceId);
        }


        /*初始化 (延遲0.5秒)*/
        $timeout(activate, 300);

    }

    angular.module("app").controller("engineeringController", engineeringController).filter("toLower", function () {
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
    }]);

    engineeringController.$inject = ["$scope", "$sce", "$timeout", "engineeringService"];
})();