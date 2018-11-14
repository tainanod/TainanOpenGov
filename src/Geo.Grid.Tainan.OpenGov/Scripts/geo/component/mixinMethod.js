/*
* vue mixin 共用的方法
*/
(function (geo) {
    "use strict";

    if (typeof geo === "undefined") {
        geo = {};
    }
    var exports = {
        data: function () {
            return {
                showLoading: false,
                urls: {
                     photoUrl: "https://smartcurator.blob.core.windows.net/sc-photo/"
                }
            };
        },
        created: function () {
            //console.log('mixinMethod loaded!');
        },
        methods: {
            pageSizeChanged: function (size) {
                this.pagination.pageSize = size;
                if (this.paras.keyword !== "") {
                    this.query();
                } else {
                    this.query(1);
                }
            },
            changedPage: function (idx) {
                this.pagination.currentPage = idx;
                if (this.paras.keyword !== "") {
                    this.query();
                } else {
                    this.query(1);
                }
            },
            computePagination: function ( total) {
                var p = this.pagination;
                p.total = total;
                p.pageNo = Math.ceil(p.total / p.pageSize);
                if (p.total <= 0) {
                    p.total = 0;
                    p.startIndex = 0;
                } else {
                    p.startIndex = 1 + ((p.currentPage - 1) * p.pageSize);
                }
                p.endIndex = ((p.currentPage) * p.pageSize);
                if (p.endIndex > total) {
                    p.endIndex = total;
                }
            },
            validateForm: function (form) {
                this.$validator.validateAll(form);

                if (this.errors.any(form)) {
                    return false;
                }

                for (var ref in app.$refs) {
                    if (this.$refs[ref].errors.any()) {
                        return false;
                    }
                }
                return true;
            },
            /*
            * 判斷發文日期是否顯示'X'
            */
            setRemoveTag: function () {
                if (this.detail.isDisableBelow) {
                    $('.tagsinput').find('a').remove();
                }
            },
            /*
             * 頁簽篩選
             */
            tabChanged: function (idx) {
                this.paras.filterType = idx;
                if (this.paras.isAdvance) {
                    this.query(1);
                } else {
                    this.query();
                }
            },

            /*
             * 顯示完整地址
             */
            fullAddress: function (com) {
                return com.cityName + com.townName + com.address;
            },
            /*
             * 格式化日期 YYYY-MM-DD
             */
            dateConvert: function (date, template) {
                if (date === null) {
                    return "";
                }
                if (template === undefined) {
                    template = 'YYYY-MM-DD';
                }
                return moment(date).format(template);
            },
            /**
             * 取得Url上的Id
             */
            getUrlId: function () {
                var arr = location.pathname.split("/");
                return arr[arr.length - 1].split("?")[0];
            },
            getPhotoUrl: function (photoId, imageSize) {
                //console.log("getPhotoUrl");
                return this.urls.photoUrl + imageSize + '/' + photoId;
            },
            /*
             * 計算分頁的 該資料為第幾筆
             */
            computeIndex: function (list, p) {
                // 先計算，使用computeIndex method 會同步更新資料!!
                list.forEach(function (d, i) {
                    d.index = ((p.currentPage - 1) * p.pageSize) + i + 1;
                });
            },
            /*
             * 補0或補字符
             */
            fillCharFormat: function (ori, fillChar, totalLength) {
                var fillLength = totalLength - (ori + '').length;
                if (fillLength < 1) {
                    return ori;
                }

                for (var i = 0; i < fillLength; i++) {
                    ori = fillChar + ori;
                }
                return ori;
            },
            /*
             * 數字金額 三位一撇
             */
            moneyFormat: function (money) {
                if (money === undefined || money === null) {
                    return "";
                }
                money = money.toString();
                if (money.length <= 3) {
                    return money;
                } else {
                    var index = money.length - 3;
                    return this.moneyFormat(money.substr(0, index)) + "," + (money.substr(index));
                }
            },
            /*
             * 從陣列中取出某個Key值的Value Ex:範例陣列 myArr=[{k:'s',v:1},{k:'q',v:2}]
             * 呼叫方法 this.getValueFromKeyValuePairArry(myArr,"k","v","s") => got 1
             */
            getValueFromKeyValuePairArry: function (arr, keyName, valueName, key) {
                var tmpAry = $.grep(arr, function (x) { return x[keyName].toUpperCase() === key.toUpperCase(); });
                if (tmpAry.length === 0) {
                    return "";
                }
                return tmpAry[0][valueName];
            },
            //欄位排序
            sorting: function (col) {
                if (this.paras.sorting === col) {
                    this.paras.sortingDesc = !this.paras.sortingDesc;
                } else {
                    this.paras.sorting = col;
                }

                if (this.paras.searchType !== 1) {
                    this.query();
                } else {
                    this.query(1);
                }
            },
            /**
             * 取得Url的參數
             */
            getUrlParams: function () {
                var ret = {};
                var query = window.location.search.substring(1);
                var vars = query.split("&");
                for (var i = 0; i < vars.length; i++) {
                    var pair = vars[i].split("=");
                    // If first entry with this name
                    if (typeof ret[pair[0]] === "undefined") {
                        ret[pair[0]] = decodeURIComponent(pair[1]);
                        // If second entry with this name
                    } else if (typeof ret[pair[0]] === "string") {
                        var arr = [ret[pair[0]], decodeURIComponent(pair[1])];
                        ret[pair[0]] = arr;
                        // If third or later entry with this name
                    } else {
                        ret[pair[0]].push(decodeURIComponent(pair[1]));
                    }
                }
                return ret;
            },
            /*
             * 建立GUID
             */
            createGuid: function () {
                function S4() {
                    return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
                }

                return (S4() + S4() + "-" + S4() + "-" + S4() + "-" + S4() + "-" + S4() + S4() + S4());
            },
            /*
             * Email 驗證
             */
            validateEmail: function (email) {
                var re =
                    /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
                return re.test(email);
            },
            /*
             * 關鍵字 亮響
             */
            highLight: function (query) {
                var k = this.paras.keyword;
                if (query == null || k.length === 0) {
                    return query;
                }
                var keywords = k.split(" ");

                keywords.forEach(function (d) {
                    query = query.replace(d, '<span class="bg-green">' + d + '</span>');
                });
                return query;
            },
            /*
             * Guid Conpare
             */
            guidComparer: function (id1, id2) {
                if (typeof id1 === "undefined" || typeof id2 === "undefined") {
                    return false;
                }

                return id1.toUpperCase() === id2.toUpperCase();
            }
        },
        filters: {
            dateConvert: function (date, template) {
                if (date === null) {
                    return "";
                }
                if (template === undefined) {
                    template = "YYYY-MM-DD";
                }
                return moment(date).format(template);
            },
            fillCharFormat: function (ori, fillChar, totalLength) {
                var fillLength = totalLength - ori.length;
                if (fillLength < 1) {
                    return ori;
                }

                for (var i = 0; i < fillLength; i++) {
                    ori = fillChar + ori;
                }
                return ori;
            },
            substring: function (str, start, end) {
                var etc = "";
                if (str === undefined || str === null) {
                    return "";
                }
                if (str.length > end) {
                    etc = "...";
                }
                return str.substr(start, end) + etc;
            },
            removeExtension: function (ori) {
                var tmp = ori.split('.');

                if (tmp.length > 1) {
                    tmp.pop();
                }
                return tmp.join('.');
            }
        },
        computed: {
        }
    };

    geo.mixinMethod = exports;
})(geo);