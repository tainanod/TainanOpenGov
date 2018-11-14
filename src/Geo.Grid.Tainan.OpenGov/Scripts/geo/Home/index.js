(function () {
    "use strict";
    var app = new Vue({
        el: "#app",
        data: {
            urls: {
                banner: window.appRoot.rel + "Api/Banner/List",
                query: window.appRoot.rel + "api/card/ApiCards",

                tnod: window.appRoot.rel + "api/tnod/get", //政府資料

                //top5Mayor: window.appRoot.rel + "api/tnod/Top5Mayor", //行程公開
                top5Mayor: window.appRoot.rel + "api/tnod/PublicSchedule", //行程公開

                top5Rss: window.appRoot.rel + "api/Rss/get", //警報告示
                showCase: window.appRoot.rel + "api/ShowCase/List", //野生台南
                forum: window.appRoot.rel + "api/card/Forum", //公民論壇
                suggest: window.appRoot.rel + "api/card/Suggest", //市政提案

                photo: window.appRoot.rel + "resource/photo/",

                getParticipation: window.appRoot.rel + "api/card/Participation", //市政參與
            },
            paras: {
                keyword: "",
                sorting: "UpdatedDate",
                sortingDesc: false,
            },
            pagination: {
                currentPage: 1,
                pageSize: 10,
                total: 999,
                pageNo: 0,
                startIndex: 0,
                endIndex: 5
            },
            banners:[],
            querys: [],
            brown: [],
            red: [],
            yellow: [],
            tnods: [],
            top5Mayors: [],
            top5Rss: [],
            showCase: {},
            forum: [],
            participation: [],
            suggest:[],
            checkCount: 0,
            totalCnt: 0
        },
        mounted: function () {
            var vm = this;
            setTimeout(function () {
                vm.getBannerList();
            }, 300);
            this.getBannerList();
            this.mapInit();
            this.query();
            this.getTnod();
            this.getTop5Mayor();
            this.getTop5Rss();
            this.getShowcaseList();
            this.getForum();
            this.getParticipation();
            this.getSuggest();
        },
        updated: function(e) {

        },
        methods: {
            getBannerList: function() {
                this.$http.get(this.urls.banner).then(function (res) {
                    if (res.ok) {
                        this.banners = res.body;
                        if (this.banners.length) {
                            
                            var options = {
                                dots: false,
                                arrows: true,
                                infinite: true,
                                speed: 400,
                                slidesToShow: 1,
                                slidesToScroll: 1,
                                autoplay: false
                            };
                            this.banners.length > 1 ? (options.autoplay = true) : (options.autoplay = false);
                            setTimeout(function () {
                                $('#homePhotoWrapper').slick(options);
                            }, 3000);
                        } else {
                            $('#homePhotoWrapper').hide();
                        }
                    }
                },errorHandler);
            },
            query: function() {
                this.$http.get(this.urls.query).then(function(res) {
                        if (res.ok) {
                            this.querys = res.data;
                            this.brown = this.querys[0].Cards;
                            this.red = this.querys[1].Cards;
                            this.yellow = this.querys[2].Cards;
                            $('#imgLoading').hide();
                        }
                    },
                    errorHandler);
            },
            getTnod: function() {
                this.$http.get(this.urls.tnod).then(function(res) {
                        if (res.ok) {
                            this.tnods = res.data;
                        }
                    },
                    errorHandler);
            },
            getTop5Mayor: function() {
                this.$http.get(this.urls.top5Mayor).then(function(res) {
                    if (res.ok) {
                        this.top5Mayors = res.data;
                    }
                },
                errorHandler);
            },
            getTop5Rss: function () {
                this.$http.get(this.urls.top5Rss).then(function (res) {
                    if (res.ok) {
                        this.top5Rss = res.data;
                    }
                },errorHandler);
            },
            getShowcaseList: function() {
                this.$http.post(this.urls.showCase, this.pagination)
                   .then(function (res) {
                       console.log(res);
                       if (res.ok) {
                           if (!res || !res.data.Data[0]) {
                               $('#ulShowcase .no-li').show();
                               $('#ulShowcase .li-loading').hide();
                               $('#ulShowcase .li-loading').hide();
                               $('#ulShowcase .home-showcase-li').hide();
                               console.log('no data');
                           } else {
                               
                               $('#ulShowcase .li-loading').hide();
                               this.showCase = res.data.Data[0];
                           }
                       }
                   } , errorHandler);
            },
            getForum: function () {
                this.$http.get(this.urls.forum).then(function (res) {
                    if (res.ok) {
                        this.forum = res.data;
                    }
                }, errorHandler);
            },
            getParticipation: function () {
                this.$http.get(this.urls.getParticipation).then(function (res) {
                    if (res.ok) {
                        this.participation = res.data;
                    }
                }, errorHandler);
            },

            getSuggest: function () {
                this.$http.get(this.urls.suggest).then(function (res) {
                    if (res.ok) {
                        this.suggest = res.data;
                    }
                }, errorHandler);
            },
            getShowCaseUrl: function (id) {
                return window.appRoot.rel + "ShowCase/Detail/" + id;
            },
            getCardClass: function(color) {
                return color === 1 ? "brown" : color === 2 ? "red" : "yellow";
            },
            onCreate: function() {
                window.location.href = this.urls.create;
            },
            onEdit: function(id) {
                window.location.href = this.urls.create + id;
            },
            onDelete: function(id) {
                if (!confirm('確定刪除?')) {
                    return;
                }

                this.$http.get(this.urls.deleteDtl + id).then(function(ret) {
                        if (ret.data.success) {
                            this.query();
                        } else {
                            alert(ret.data.message);
                        }
                    },
                    errorHandler);
            },
            sorting: function(col) {
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
            getTnodUrl: function(name) {
                return "http://data.tainan.gov.tw/dataset/" + name;
            },
            getForumUrl: function(f) {
                return window.appRoot.rel + 'Forum/Detail/' + f.ForumId;
            },
            getParticipationUrl: function (f) {
                return window.appRoot.rel + 'Participation/Detail/' + f.ParticipationId;
            },
            getSuggestUrl: function (s) {
                return window.appRoot.rel + 'Suggest/Detail/' + s.ForumId;
            },
            getTnodClass: function(fileType) {
                return fileType.indexOf("X") == 0 ? "green" : "red";
            },
            onTop5MayorClick: function(id) {
                this.mayorDetail(id);
            },
            mayorDetail:function(id) {
                var filter = this.top5Mayors.filter(function (d) {
                    return d._id === id;
                });
                console.log(filter);
                if (filter) {
                    var data = filter[0];
                    this.markerCenter(data.坐標Y, data.坐標X);
                    $('#mayorTitle').html(data.標題);
                    $('#mayorEvent').html(data.行程事由);
                    //$('#mayorTime').html(moment(data.開始時間).format("YYYY年MMMDo a h:mm") +
                    //    " ~ " +
                    //    moment(data.結束時間).format("a h:mm"));
                    $('#startMayorTime').html(data.開始時間);
                    $('#endMayorTime').html(data.結束時間);
                    $('#mayorHolder').html(data.主辦單位);
                    $('#mayorLocation').html(data.行程地點);
                }
            },
            markerCenter: function (lat, lng) {
                console.log('進到vue.MarkerCenter');
                console.log('lat=' + lat);
                console.log('lng=' + lng);
                var p = new google.maps.LatLng(lat, lng);
                gmap.marker.setPosition(p);
                setTimeout(function () {
                    google.maps.event.trigger(gmap.map, "resize");
                    gmap.map.setCenter(p);
                }, 250);
            },
            mapInit: function() {
                var position = new google.maps.LatLng(22.991608, 120.184955);
                var myOptions = {
                    zoom: 16,
                    center: position,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };
                gmap.map = new google.maps.Map(document.getElementById("map_canvas"), myOptions);

                gmap.marker = new google.maps.Marker({
                    position: position,
                    map: gmap.map
                });
            },
            getIconUrl: function (card) {
                var iconId = card.IconId;
                var iconUrl = window.appRoot.rel + "Content/geo/img/";

                switch (card.Type) {
                    case 1:
                        return iconUrl + "icon1.png";
                    case 2:
                        return iconUrl + "icon2.png";
                    case 3:
                        return iconUrl + "icon3.png";
                    case 4:
                        return iconUrl + "icon4.png";
                    case 5:
                        return iconUrl + "icon5.png";
                    case 6:
                        return iconUrl + "icon6.png";
                    case 7:
                        return iconUrl + "icon7.png";
                    case 8:
                        return iconUrl + "icon8.png";
                    case 9:
                        return iconUrl + "icon9.png";
                    case 10:
                        return iconUrl + "icon8.png";
                    default:
                        if (iconId !== '00000000-0000-0000-0000-000000000000' && iconId !== undefined) {
                            return this.urls.photo + iconId + "?size=0";
                        } else {
                            return window.appRoot.rel + "Content/geo/img/icon1.png"
                        }
                }
            }
        },
        mixins: [geo.mixinMethod],
        components: {
            
        
        }
    });
    window.app = app;
})();