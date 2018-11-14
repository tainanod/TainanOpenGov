(function () {
    'use strict';
    /*
     * 公益台南：弱勢條件篩選 / 弱勢補助項目一覽表
     */
    function welfareController($scope, $sce, $timeout, welfareService) {
        $scope.vm = {
            others: [],
            identity: [],
            //條件篩選資料
            list: [],
            //一覽表資料
            listAll: [],
            //每頁顯示筆數
            optionModel: [10, 20, 50, 100],
            //判斷所在的 Tab
            isAll: sessionStorage.isAll === 'true' ? true : false,
            townList: [
                "未設籍臺南市",
                "中西區",
                "東區",
                "南區",
                "北區",
                "安平區",
                "安南區",
                "永康區",
                "歸仁區",
                "新化區",
                "左鎮區",
                "玉井區",
                "楠西區",
                "南化區",
                "仁德區",
                "關廟區",
                "龍崎區",
                "官田區",
                "麻豆區",
                "佳里區",
                "西港區",
                "七股區",
                "將軍區",
                "學甲區",
                "北門區",
                "新營區",
                "後壁區",
                "白河區",
                "東山區",
                "六甲區",
                "下營區",
                "柳營區",
                "鹽水區",
                "善化區",
                "大內區",
                "山上區",
                "新市區",
                "安定區"
            ],
            detail: null
        };

        /*
         * 條件篩選的query條件
         */
        $scope.query = {
            liveIn: '',
            isLiveDays: '',
            age: null,
            anyChildren: null,
            isChildren: null,
            childrenAge: null,
            ageMin: null,
            ageMax: null,
            identity1: [],
            identity2: [],
            incomeValue: 0,
            movableValue: 0,
            immovableValue: 0
        }

        /*
         * 身份條件checkbox modal
         */
        $scope.identity = [];
        $scope.others = [];

        /*
         * 一覽表的query條件
         */
        $scope.queryAll = {
            keyword: ''
        }

        /*
         * 條件篩選的分頁
         */
        $scope.page = {
            currentPage: 1,
            pageSize: 10,
            totalRecord: 0,
            totalPages: 1
        };

        /*
         * 一覽表的分頁
         */
        $scope.pageAll = {
            currentPage: 1,
            pageSize: 10,
            totalRecord: 0,
            totalPages: 1
        };

        /*
         * 判斷是否已送出查詢過
         */
        $scope.submitted = false;

        /*
         * 根據使用者填寫的設籍地址、養育幾名子女的問題後，判斷其相關問題是否必填
         */
        $scope.required = {
            isLiveDays: false,
            isChildren: false,
            childrenAge: false,
            ageMin: false
        }

        /*
         * 弱勢篩選表單送出(驗證)
         */
        $scope.formSubmit = function () {
            //清除上次的資料
            $scope.vm.list = $scope.vm.list !== [] ? [] : $scope.vm.list;

            // 如果沒有子女，則清除子女的相關問題
            if ($scope.query.anyChildren === 'false') {
                $scope.query.isChildren = null;
                $scope.query.childrenAge = null;
                $scope.query.ageMin = null;
                $scope.query.ageMax = null;
            }

            //根據使用者填寫的設籍地址、養育幾名子女的問題後，判斷其相關問題是否必填
            $scope.required.isLiveDays = ($scope.query.liveIn && $scope.query.liveIn !== '未設籍臺南市') ? true : false;
            $scope.required.isChildren = ($scope.query.anyChildren === 'true') ? true : false;
            $scope.required.childrenAge = ($scope.query.isChildren === 'false') ? true : false;
            $scope.required.ageMin = ($scope.query.isChildren === 'true') ? true : false;

            var isLiveDaysInvalid = $scope.required.isLiveDays ? $scope.query.isLiveDays === '' : false
            var isChildrenInvalid = $scope.required.isChildren ? $scope.query.isChildren === null : false;
            var childrenAgeInvalid = $scope.required.childrenAge ? $scope.query.childrenAge === null : false;
            var ageMinInvalid = $scope.required.ageMin ? $scope.query.ageMin == null : false;
            var ageMaxInvalid = $scope.required.ageMin ? $scope.query.ageMax == null : false;

            if (!$scope.query.liveIn || !$scope.query.age || !$scope.query.anyChildren || $scope.query.incomeValue === null || $scope.query.movableValue === null || $scope.query.immovableValue === null || isLiveDaysInvalid || isChildrenInvalid || childrenAgeInvalid || ageMinInvalid || ageMaxInvalid) {
                $scope.submitted = true;
                alert('請填寫必填欄位，並確認資料是否正確。');
            } else {
                //debugger;
                $scope.getList($scope.page.currentPage
                    , $scope.page.pageSize, $scope.query);
            }

        };

        /*
         * 取得弱勢條件篩選列表
         */
        $scope.getList = function (currentPage, pageSize, query) {

            var ageMin = !query.ageMin ? null : query.ageMin <= query.ageMax ? query.ageMin : query.ageMax;
            var ageMax = !query.ageMin ? null : query.ageMin <= query.ageMax ? query.ageMax : query.ageMin;
            var param = {
                "CurrentPage": currentPage,
                "PageSize": pageSize,
                "LiveIn": query.liveIn,
                "IsLiveDays": query.isLiveDays,
                "Age": query.age,
                "AnyChildren": query.anyChildren === 'true' ? true : false,
                "IsChildren": query.isChildren === null ? null : query.isChildren === 'true' ? true : false,
                "ChildrenAge": query.childrenAge,
                "AgeMin": ageMin,
                "AgeMax": ageMax,
                "Identity1": query.identity1,
                "Identity2": query.identity2,
                "IncomeValue": query.incomeValue,
                "MovableValue": query.movableValue,
                "ImmovableValue": query.immovableValue
            }

            console.log(param);

            welfareService.getList(param).success(function (res) {
                //console.log(res);
                debugger;
                if (!res.Success) {
                    $('#imgLoading').hide();
                    console.log('請先填寫您的戶籍地、年齡、子女條件、特殊身分、財產條件等資訊');
                    alert(res.Message);
                    return;
                } else if (res.Total === 0) {
                    alert('查詢後沒有資料喔！')
                }

                $scope.vm.list = res.Data;
                $scope.page.currentPage = res.CurrentPage;
                $scope.page.pageSize = res.PageSize;
                $scope.page.totalRecord = res.Total;
                $scope.page.totalPages = res.TotalPage;

                $('#imgLoading').hide();
                sessionStorage.clear();
            });
        }

        /*
         * 取得一覽表的列表
         */
        $scope.getListAll = function (currentPage, pageSize, query) {
            var param = {
                "CurrentPage": currentPage,
                "PageSize": pageSize,
                "Keyword": query ? query.keyword : ''
            }

            welfareService.getListAll(param).success(function (res) {
                if (!res.Success) {
                    $('#imgLoading').hide();
                    console.log(res.Message)
                    alert(res.Message);
                    return;
                } else if (res.Data.length === 0) {
                    alert('查詢後沒有資料喔！')
                }
                //debugger;
                var reg = new RegExp(param.Keyword, 'g');
                var temp = param.Keyword ? res.Data.map(function (el, idx, arr) {
                    arr[idx].Name = arr[idx].Name.replace(reg, "<span class='point'>" + param.Keyword + "</span>");
                    arr[idx].Docs = arr[idx].Docs.replace(reg, "<span class='point'>" + param.Keyword + "</span>");
                    return el;
                }) : res.Data;
                //console.log(temp);
                $scope.vm.listAll = temp;
                $scope.pageAll.currentPage = res.CurrentPage;
                $scope.pageAll.pageSize = res.PageSize;
                $scope.pageAll.totalRecord = res.Total;
                $scope.pageAll.totalPages = res.TotalPage;

                $('#imgLoading').hide();
                sessionStorage.clear();
            });
        }

        /*
         * 上方 tab 切換功能
         */
        $scope.switchTab = function (isAll) {
            $scope.vm.isAll = isAll;
            sessionStorage.isAll = $scope.vm.isAll;
        }

        /*
         * 弱勢條件篩選分頁切換
         */
        $scope.pageChange = function () {
            //$scope.getList($scope.page.currentPage, $scope.page.pageSize, $scope.query);
            $scope.formSubmit();
        }

        /*
         * 一覽表分頁切換
         */
        $scope.pageChangeAll = function () {
            $scope.getListAll($scope.pageAll.currentPage, $scope.pageAll.pageSize, $scope.queryAll);
        }

        /*
         * 前往 Detail 頁，並且儲存搜尋紀錄
         */
        $scope.goDetail = function (pageId) {
            if (!$scope.vm.isAll) {
                //條件篩選 sessionStorage
                sessionStorage.isAll = $scope.vm.isAll;
                sessionStorage.currentPage = $scope.page.currentPage;
                sessionStorage.pageSize = $scope.page.pageSize;
                sessionStorage.liveIn = $scope.query.liveIn;
                sessionStorage.isLiveDays = $scope.query.isLiveDays;
                sessionStorage.age = $scope.query.age;
                sessionStorage.anyChildren = $scope.query.anyChildren;
                sessionStorage.isChildren = $scope.query.isChildren;
                sessionStorage.childrenAge = $scope.query.childrenAge;
                sessionStorage.ageMin = $scope.query.ageMin;
                sessionStorage.ageMax = $scope.query.ageMax;
                sessionStorage.identityLength = $scope.vm.identity.length;
                $scope.vm.identity.forEach(function (el, idx) {
                    sessionStorage['identityVal' + idx] = el;
                });
                $scope.identity.forEach(function (el, idx) {
                    sessionStorage['identity' + idx] = el;
                });
                sessionStorage.othersLength = $scope.vm.others.length;
                $scope.vm.others.forEach(function (el, idx) {
                    sessionStorage['othersVal' + idx] = el;
                });
                $scope.others.forEach(function (el, idx) {
                    sessionStorage['others' + idx] = el;
                });
                sessionStorage.incomeValue = $scope.query.incomeValue;
                sessionStorage.movableValue = $scope.query.movableValue;
                sessionStorage.immovableValue = $scope.query.immovableValue;
            } else {
                //一覽表 sessionStorage
                sessionStorage.isAll = $scope.vm.isAll;
                sessionStorage.currentPageAll = $scope.pageAll.currentPage;
                sessionStorage.pageSizeAll = $scope.pageAll.pageSize;
                sessionStorage.keyword = $scope.queryAll ? $scope.queryAll.keyword : '';
            }
            window.location.href = window.appRoot.rel + "Welfare/Detail/" + pageId;
        };

        /*
         * 取得特殊身分條件
         */
        $scope.getIdentity = function () {
            welfareService.getIdentity().success(function (res) {
                $scope.vm.identity = res;
            })
        }

        /*
         * 取得其他身分條件
         */
        $scope.getOthers = function () {
            welfareService.getOthers().success(function (res) {
                $scope.vm.others = res;
            })
        }

        /*
         * 身份條件checkbox變動時觸發
         */
        $scope.changeIdentity = function (val, isOthers) {
            var idx = isOthers ? $scope.query.identity2.indexOf(val) : $scope.query.identity1.indexOf(val);
            if (idx === -1) {
                if (isOthers) {
                    $scope.query.identity2.push(val);
                } else {
                    $scope.query.identity1.push(val);
                }
            } else {
                if (isOthers) {
                    $scope.query.identity2.splice(idx, 1);
                } else {
                    $scope.query.identity1.splice(idx, 1);
                }
            }
        }


        /*
         * 頁面初始化：取得項目資料
         */
        function activate() {
            $scope.getOthers();
            $scope.getIdentity();
            //在條件篩選的頁面且有 sessionStorage 的狀況下
            if (sessionStorage.currentPage && !$scope.vm.isAll) {
                $scope.page.currentPage = parseInt(sessionStorage.currentPage);
                $scope.page.pageSize = parseInt(sessionStorage.pageSize);
                $scope.query.liveIn = sessionStorage.liveIn;
                $scope.query.isLiveDays = sessionStorage.isLiveDays;
                $scope.query.age = parseInt(sessionStorage.age);
                $scope.query.anyChildren = sessionStorage.anyChildren;
                $scope.query.isChildren = sessionStorage.isChildren === 'null' ? null : sessionStorage.isChildren;
                $scope.query.childrenAge = sessionStorage.childrenAge === 'null' ? null : parseInt(sessionStorage.childrenAge);
                $scope.query.ageMin = sessionStorage.ageMin === 'null' ? null : parseInt(sessionStorage.ageMin);
                $scope.query.ageMax = sessionStorage.ageMax === 'null' ? null : parseInt(sessionStorage.ageMax);
                for (var i = 0; i < sessionStorage.identityLength; i++) {
                    //debugger;
                    if (sessionStorage['identity' + i]) {
                        if (sessionStorage['identity' + i] === 'true') $scope.query.identity1.push(sessionStorage['identityVal' + i]);
                        $scope.identity[i] = (sessionStorage['identity' + i]) === 'true' ? true : false;
                    };
                };
                for (var i = 0; i < sessionStorage.othersLength; i++) {
                    //debugger;
                    if (sessionStorage['others' + i]) {
                        if (sessionStorage['others' + i] === 'true') $scope.query.identity2.push(sessionStorage['othersVal' + i]);
                        $scope.others[i] = (sessionStorage['others' + i]) === 'true' ? true : false;
                    };
                };
                $scope.query.incomeValue = parseInt(sessionStorage.incomeValue);
                $scope.query.movableValue = parseInt(sessionStorage.movableValue);
                $scope.query.immovableValue = parseInt(sessionStorage.immovableValue);

                $scope.formSubmit();
            }

            //在一覽表的頁面且有 sessionStorage 的狀況下
            if (sessionStorage.currentPageAll && $scope.vm.isAll) {
                $scope.pageAll.currentPage = parseInt(sessionStorage.currentPageAll);
                $scope.pageAll.pageSize = parseInt(sessionStorage.pageSizeAll);
                $scope.queryAll.keyword = sessionStorage.keyword;

                $scope.getListAll($scope.pageAll.currentPage, $scope.pageAll.pageSize, $scope.queryAll);
            } else {
                $scope.getListAll();
            }

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

    welfareController.$inject = ["$scope", "$sce", "$timeout", "welfareService"];
})();