(function () {
    'use strict';
    /*
     * 市政監督：工程地圖
     */
    function engineeringController($scope, $sce, $timeout, engineeringService) {
        $scope.vm = {
            //行政區
            townList: [],
            //工程類別
            categoryList: [],
            //狀態
            statusList: [],
            //工程進度
            progressTextList: [],
            //搜尋原始資料
            oriMapList: [],
            //地圖資料
            mapList: [],
            isOpenAdv: true,
            governmentAenciesCode: '',
            code: '',
            nowYear: new Date().getFullYear()
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
        $scope.dateConvert = function (d) {
            if (d) {
                debugger;
                if (typeof (d) !== 'string') d = d.toISOString();
                var result = Number(d.slice(0, 4)) + '-' + d.slice(5, 7) + '-' + d.slice(8, 10);
                return result;
            } else {
                return null;
            }
        }


        /*
         * 行政區名稱過濾
         */
        $scope.cityFormat = function (c) {
            if (c) {
                return c.match('區') ? c.split('市')[1] : c.replace(/臺南|台南/g, '全');
            } else {
                return '無';
            }
        }

        /*
         * query條件
         */
        $scope.query = {
            town: '台南市',
            category: '',
            progressText: '',
            status: '',
            amountMin: 5000,
            amountMax: null,
            startDate: new Date($scope.vm.nowYear + '-01-01'),
            endDate: new Date($scope.vm.nowYear + '-12-31'),
            keyword: ''
        }

        /*
         * 地圖初始化 
         */
        $scope.initMap = function (id, center) {
            var markers = [];
            var mapConfig = {
                center: center,
                mapTypeControl: false, //啟用/停用讓使用者切換地圖類型
                streetViewControl: false, //啟用/停用讓使用者啟用「街景服務」全景功能的「小黃人」控制項
                gestureHandling: 'greedy',  //單指滑動與兩指滑動都會使地圖平移
                zoomControl: false, //啟用/停用「縮放」控制項
                zoom: 16
            };

            id = id ? id : 'googleMap';
            center = center ? center : { lat: 23.311684, lng: 120.314736 };

            map = new google.maps.Map(document.getElementById(id), mapConfig);

            //監聽click事件
            map.data.addListener('click', function (e) {

                console.log(e.feature);

                infoWindow = $scope.addInfoWindow(e.feature);
                infoWindow.setPosition(e.feature.getGeometry().get());
                infoWindow.setOptions({ pixelOffset: new google.maps.Size(0, -35) });
                infoWindow.open(map, this);

                $scope.saveSearch();
            });

            //設定樣式
            map.data.setStyle(function (f) {
                var s = f.getProperty("Status"), result;
                result = (s === '驗收完成' || s === '保固中' || s === '已結案') ? 'finish' : 'notyet';
                return {
                    icon: {
                        url: window.appRoot.full + '/Content/geo/img/ic_marker_' + result + '.png',
                        scaledSize: new google.maps.Size(35, 35)
                    }
                }

            });

        };

        /*
         * 更新地圖資料
         */
        $scope.updateMap = function () {
            var bounds = new google.maps.LatLngBounds();

            //移除所有的 Features
            $scope.clearMap();
            //關閉所有 infowindow
            if (infoWindow) infoWindow.close(map);

            //點位處理
            function processPoints(geometry, callback, thisArg) {
                if (geometry instanceof google.maps.LatLng) {
                    callback.call(thisArg, geometry);
                } else if (geometry instanceof google.maps.Data.Point) {
                    callback.call(thisArg, geometry.get());
                } else {
                    geometry.getArray().forEach(function (g) {
                        processPoints(g, callback, thisArg);
                    });
                }
            }

            //監聽feature 調整地圖bounds
            map.data.addListener('addfeature', function (e) {
                processPoints(e.feature.getGeometry(), bounds.extend, bounds);
                map.fitBounds(bounds);
                if (map.getZoom() > 19) {
                    map.setZoom(19);
                }
                $('#imgLoading').hide();
            });

            map.data.addGeoJson($scope.vm.oriMapList);


        }

        /*
         * 詳細頁的地圖呈現
         */
        $scope.showMarkerInfo = function () {
            map.data.forEach(function (feature) {
                if (feature.getProperty("Code") === $scope.vm.code && feature.getProperty("GovernmentAenciesCode") === $scope.vm.governmentAenciesCode) {
                    google.maps.event.trigger(map.data, 'click', {
                        feature: feature
                    });
                } else {
                    map.data.remove(feature);
                }
            });
            $scope.vm.governmentAenciesCode = '';
            $scope.vm.code = '';
        }


        /*
        * 新增 infoWindow 
        */
        $scope.addInfoWindow = function (f) {
            //debugger;
            var contentString, infoWindow,
                s = f.getProperty("Status") === '保固中' ? '已結案' : f.getProperty("Status"),
                className = (s === '驗收完成' || s === '保固中' || s === '已結案') ? 'finish' : 'notyet';

            contentString = '<div id="infoContent" class="' + className + '">' +
                '<h1>' + s + '</h1>' +
                '<div class="detail-wrapper form-horizontal">' +
                '<label class="control-label col-xs-3">工程名稱</label>' +
                '<div class="col-xs-9">' +
                '<div class="text-control">' +
                f.getProperty("Name") +
                '</div>' +
                '</div>' +
                '<label class="control-label col-xs-3">行政區</label>' +
                '<div class="col-xs-9">' +
                '<div class="text-control">' +
                $scope.cityFormat(f.getProperty("CityTown")) +
                '</div>' +
                '</div>' +
                '<label class="control-label col-xs-3">工程類別</label>' +
                '<div class="col-xs-9">' +
                '<div class="text-control">' +
                f.getProperty("Category") +
                '</div>' +
                '</div>' +
                '<label class="control-label col-xs-3">工程進度</label>' +
                '<div class="col-xs-9">' +
                '<div class="text-control">' +
                f.getProperty("ProgressText") +
                '</div>' +
                '</div>' +
                '<label class="control-label col-xs-3">工程經費</label>' +
                '<div class="col-xs-9">' +
                '<div class="text-control">' +
                $scope.moneyFormat(f.getProperty("Amount") / 10) +
                ' (萬元)</div>' +
                '</div>' +
                //'<div class="col-xs-12 text-right">'+
                '<a class="qa-btn" href="' + window.appRoot.rel + 'Engineering/Detail/?gacode=' + f.getProperty("GovernmentAenciesCode") + '&code=' + f.getProperty("Code") + '">' +
                '<i class="fa fa-plus" aria-hidden="true"></i> 詳細資料' +
                '</a>' +
                //'</div>'+
                '</div>' +
                '</div>';


            infoWindow = new google.maps.InfoWindow({
                content: contentString
            });

            return infoWindow;
        };

        /*
         * 移除所有的 Features
         */
        $scope.clearMap = function () {
            map.data.forEach(function (f) {
                map.data.remove(f);
            });
        }




        /*
         * 地圖關鍵字搜尋
         */
        $scope.queryMap = function () {
            $('#imgLoading').show();
            $scope.clear(1);
            $scope.vm.isOpenAdv = false;
            engineeringService.queryMap($scope.query).success(function (res) {
                console.log(res);
                //debugger;
                if (res.features.length === 0) {
                    alert('查詢後沒有資料喔！');
                    $('#imgLoading').hide();
                }

                $scope.vm.oriMapList = res;
                $scope.updateMap();
                sessionStorage.clear();
            });
        }

        /*
         * 地圖進階搜尋
         */
        $scope.advQueryMap = function () {
            debugger;
            var param = {
                town: $scope.query.town,
                category: $scope.query.category,
                progressText: $scope.query.progressText,
                status: $scope.query.status,
                amountMin: $scope.query.amountMin,
                amountMax: $scope.query.amountMax,
                startDate: $scope.dateConvert($scope.query.startDate),
                endDate: $scope.dateConvert($scope.query.endDate),
                keyword: $scope.query.keyword
            };
            var checkOk = true,
                alertText = '';
            if (param.amountMin !== null && param.amountMax !== null && param.amountMin > param.amountMax) {
                checkOk = false;
                alertText += '工程經費區間金額有誤，請修正後查詢。\n';
            };

            if (param.startDate > param.endDate) {
                checkOk = false;
                alertText += '開工日期區間有誤，請修正後查詢。';
            }

            if (!checkOk) {
                alert(alertText);
                return;
            }

            $('#imgLoading').show();
            param.amountMin = param.amountMin ? param.amountMin * 10 : param.amountMin;
            param.amountMax = param.amountMax ? param.amountMax * 10 : param.amountMax;
            console.log($scope.query)
            console.log(param);

            engineeringService.advQueryMap(param).success(function (res) {
                console.log(res);
                //debugger;
                if (res.features.length === 0) {
                    alert('查詢後沒有資料喔！');
                    $('#imgLoading').hide();
                }

                $scope.vm.oriMapList = res;
                $scope.updateMap();
                if ($scope.vm.governmentAenciesCode) $scope.showMarkerInfo();
                sessionStorage.clear();
            });
        }


        /*
         * 儲存搜尋紀錄
         */
        $scope.saveSearch = function () {
            sessionStorage.town = $scope.query.town;
            sessionStorage.category = $scope.query.category;
            sessionStorage.progressText = $scope.query.progressText;
            sessionStorage.status = $scope.query.status;
            sessionStorage.amountMin = $scope.query.amountMin;
            sessionStorage.amountMax = $scope.query.amountMax;
            sessionStorage.startDate = $scope.dateConvert($scope.query.startDate);
            sessionStorage.endDate = $scope.dateConvert($scope.query.endDate);
            sessionStorage.keyword = $scope.query.keyword;
            sessionStorage.isOpenAdv = $scope.vm.isOpenAdv;
        };

        /*
         * 取得 行政區
         */
        $scope.getTown = function () {
            engineeringService.getTown().success(function (res) {
                $scope.vm.townList = res;
            })
        }

        /*
         * 取得 工程類別
         */
        $scope.getCategory = function () {
            engineeringService.getCategory().success(function (res) {
                $scope.vm.categoryList = res;
            })
        }

        /*
         * 取得 狀態
         */
        $scope.vm.statusList = [
            { Code: "未開工", Name: "未開工" },
            { Code: "準備開工中", Name: "準備開工中" },
            { Code: "施工中", Name: "施工中" },
            { Code: "停工中", Name: "停工中" },
            { Code: "完工待驗收", Name: "完工待驗收" },
            { Code: "驗收中", Name: "驗收中" },
            { Code: "驗收完成", Name: "驗收完成" },
            { Code: "已結案", Name: "已結案" },
        ];


        /*
         * 取得 工程進度
         */
        $scope.getProgressText = function () {
            engineeringService.getProgressText().success(function (res) {
                $scope.vm.progressTextList = res;
            })
        }

        /*
         * 預設載入資料
         */
        $scope.defaultLoadData = function () {
            debugger;
            if (sessionStorage.length) {
                $scope.query.town = (sessionStorage.town) ? sessionStorage.town : '台南市';
                $scope.query.category = (sessionStorage.category) ? sessionStorage.category : '';
                $scope.query.progressText = (sessionStorage.progressText) ? sessionStorage.progressText : '';
                $scope.query.status = (sessionStorage.status) ? sessionStorage.status : '';
                $scope.query.amountMin = sessionStorage.amountMin === 'null' || sessionStorage.amountMin === undefined ? 5000 : Number(sessionStorage.amountMin);
                $scope.query.amountMax = sessionStorage.amountMax === 'null' || sessionStorage.amountMax === undefined ? null : Number(sessionStorage.amountMax);
                $scope.query.startDate = sessionStorage.startDate === undefined ? new Date($scope.vm.nowYear + '-01-01') : sessionStorage.startDate === 'null' ? null : new Date(sessionStorage.startDate);
                $scope.query.endDate = sessionStorage.endDate === undefined ? new Date($scope.vm.nowYear + '-12-31') : sessionStorage.endDate === 'null' ? null : new Date(sessionStorage.endDate);
                $scope.query.keyword = (sessionStorage.keyword) ? sessionStorage.keyword : '';
                $scope.vm.isOpenAdv = sessionStorage.isOpenAdv === 'false' ? false : true;
                $scope.vm.governmentAenciesCode = sessionStorage.isShowMap === 'true' ? sessionStorage.governmentAenciesCode : '';
                $scope.vm.code = sessionStorage.isShowMap === 'true' ? sessionStorage.code : '';
            }

            $scope.advQueryMap();
        }

        /*
         * 控制進階搜尋panel開啟
         */
        $scope.toggleAdv = function () {
            $scope.vm.isOpenAdv = !$scope.vm.isOpenAdv;
        }

        /*
         * 清除條件
         */
        $scope.clear = function (isKeyworkSearch) {
            $scope.query.town = '台南市';
            $scope.query.category = '';
            $scope.query.progressText = '';
            $scope.query.status = '';
            $scope.query.amountMin = null;
            $scope.query.amountMax = null;
            $scope.query.startDate = null;
            $scope.query.endDate = null;
            $scope.query.keyword = isKeyworkSearch ? $scope.query.keyword : '';
        }


        /*
         * 頁面初始化：取得項目資料
         */
        function activate() {
            $scope.getTown();
            $scope.getCategory();
            //$scope.getStatus();
            $scope.getProgressText();
            $scope.initMap();
            $scope.defaultLoadData();
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
    });

    engineeringController.$inject = ["$scope", "$sce", "$timeout", "engineeringService"];
})();