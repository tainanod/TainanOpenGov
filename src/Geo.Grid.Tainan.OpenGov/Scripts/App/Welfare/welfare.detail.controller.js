(function () {
    'use strict';

    /*
     * 公益台南：弱勢補助 Detail 頁
     */
    function welfareController($scope, $sce, $timeout, welfareService) {
        $scope.vm = {
            isAll: sessionStorage.isAll === 'true' ? true : false,
            detail: null,
            selectOfficeId: null,
            districtOffice: null
        };

        /*
         * 金錢三位一撇
         */
        $scope.moneyFormat = function (money) {
            // References: https://goo.gl/4XfhY1
            if (money) {
                return money.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
            }
        };
        /*
         * 地圖初始化 
         */
        $scope.initMap = function () {
            var mapConfig = {
                target: "googleMap",
                center: {
                    lon: 120.314736,
                    lat: 23.311684,
                    zoom: 17
                },
            };
            var layerOption = [
                { type: 'nlsc', name: '通用版', active: true },
            ];
            
            map = new MapFusion(mapConfig, layerOption);
        };

        /*
         * 收件單位變更時，將地標移至收件單位位置
         */
        $scope.setMarkerCenter = function (officeId) {
            debugger;
            var target = $scope.vm.districtOffice.filter(function (el) {
                return el.OfficeId === officeId;
            })[0];
            var geom = map.theMarker.getGeometry();
            var coor = ol.proj.fromLonLat([target.Lng, target.Lat]);
            geom.setCoordinates(coor);

            var v = map.olmap.getView();
            v.setCenter(coor);
        };

        /*
         * 取得收件單位下拉資料
         */
        $scope.getDistrictOffice = function() {
            welfareService.getDistrictOffice().success(function (res) {
                $scope.vm.districtOffice = res.Data.sort(function (a, b) {
                    return b.Name.length - a.Name.length;
                });
            })
        }

        /*
         * 取得 Detail 內容
         */
        $scope.getDetail = function(param) {
            welfareService.getDetail(param).success(function (res) {
                if (!res) {
                    console.log('no data')
                    return;
                }
                $scope.vm.detail = res.Data;
                $('#imgLoading').hide();
            })
        };

        $scope.goBack = function () {
            window.history.back();
        }

        function activate() {
            $scope.getDetail($scope.allowanceId);
            $scope.getDistrictOffice();
            $scope.initMap();
        }


        /*初始化 (延遲0.5秒)*/
        $timeout(activate, 300);

    }

    angular.module("app").controller("welfareController", welfareController).filter("toLower", function () {
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

    welfareController.$inject = ["$scope", "$sce", "$timeout", "welfareService"];
})();