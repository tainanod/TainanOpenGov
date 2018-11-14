(function () {
    "use strict";
        var app = new Vue({
            el: "#app",
            data: {
                urls: {
                    banner: window.appRoot.rel + "Api/Banner/List",
                    query: window.appRoot.rel + "api/card/cards",
                    updateCards: window.appRoot.rel + "api/card/UpdateCards",
                    createCard: window.appRoot.rel + "api/card/CreateCard",
                    editCard: window.appRoot.rel + "api/card/EditCard",
                    updateContents: window.appRoot.rel + "api/card/UpdateContents",
                    deleteCard: window.appRoot.rel + "api/card/Delete/",
                    uploadPhoto: window.appRoot.rel + "Photo/Upload",
                    photo: window.appRoot.rel + "resource/photo/",

                    tnod: window.appRoot.rel + "api/tnod/get", //政府資料
                    top5Mayor: window.appRoot.rel + "api/tnod/Top5Mayor", //行程公開
                    top5Rss: window.appRoot.rel + "api/Rss/get", //警報告示
                    showCase: window.appRoot.rel + "api/ShowCase/List", //野生台南
                    forum: window.appRoot.rel + "api/card/Forum", //公民論壇
                    suggest: window.appRoot.rel + "api/card/Suggest", //市政提案
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
                newData: {
                    cardId: null,
                    title: '',
                    iconId: '',
                    color: 1,
                    webUrl: ''
                },
                gmap: {},
                banners: [],
                querys: [],
                brown: [],
                red: [],
                yellow: [],
                tnods: [],
                top5Mayors: [],
                top5Rss: [],
                showCase: {},
                forum: [],
                suggest: [],
                editCard: {},
                checkCount: 0,
                totalCnt: 0,
                isCreate: true,
                groupDivMinHeight: 291
            },
            computed: {
                newDataIconUrl: function () {
                    if (this.newData.iconId) {
                        return this.urls.photo + this.newData.iconId + "?size=0";
                    } else {
                        return window.appRoot.rel + "Content/geo/img/icon1.png"
                    }
                }
            },
        mounted: function () {
            
            this.mapInit();
            this.query();
            this.getTnod();
            this.getTop5Mayor();
            this.getTop5Rss();
            this.getShowcaseList();
            this.getForum();
            this.getSuggest();
        },
        updated: function(e) {
            this.onUpdate();
        },
        methods: {
            query: function() {
                this.$http.get(this.urls.query).then(function(res) {
                        if (res.ok) {
                            this.querys = res.data;
                            var allGroupHeight = [];
                            this.querys.forEach(function (el, idx, arr) {
                                allGroupHeight.push(el.Cards.length * 291);
                            })
                            this.groupDivMinHeight = Math.max.apply(null, allGroupHeight);
                            //this.brown = this.querys[0].Cards;
                            //this.red = this.querys[1].Cards;
                            //this.yellow = this.querys[2].Cards;
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
            onMove: function() {
                //console.log('move');
                //this.onUpdate();
            },
            getShowcaseList: function() {
                this.$http.post(this.urls.showCase, this.pagination)
                   .then(function (res) {
                       //console.log(res);
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
            getSuggest: function () {
                this.$http.get(this.urls.suggest).then(function (res) {
                    if (res.ok) {
                        this.suggest = res.data;
                    }
                }, errorHandler);
            },
            getCardClass: function (color) {
                color = Number(color);
                return color === 1 ? "brown" : color === 2 ? "red" : color === 3 ? "yellow" : "";
            },
            getTnodUrl: function(name) {
                return "http://data.tainan.gov.tw/dataset/" + name;
            },
            getForumUrl: function(f) {
                return window.appRoot.rel + 'Forum/Detail/' + f.ForumId;
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
                //console.log(filter);
                if (filter) {
                    var data = filter[0];
                    this.markerCenter(data.坐標Y, data.坐標X);
                    $('#mayorTitle').html(data.標題);
                    $('#mayorEvent').html(data.行程事由);
                    $('#mayorTime').html(moment(data.開始時間).format("YYYY年MMMDo a h:mm") +
                        " ~ " +
                        moment(data.結束時間).format("a h:mm"));
                    $('#mayorHolder').html(data.主辦單位);
                    $('#mayorLocation').html(data.行程地點);
                }
            },
            markerCenter: function (lat, lng) {
                var gmap = this.gmap;
                var p = new google.maps.LatLng(lat, lng);
                gmap.marker.setPosition(p);
                setTimeout(function () {
                    google.maps.event.trigger(gmap.map, "resize");
                    gmap.map.setCenter(p);
                }, 250);
            },
            mapInit: function () {
                var gmap = this.gmap;
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
            getShowCaseUrl: function(id) {
                return window.appRoot.rel + "ShowCase/Detail/" + id;
            },
            onCreate: function() {
                this.isCreate = true;
            },
            onCreateSave: function () {
                this.$http.post(this.urls.createCard, this.newData)
                   .then(function (res) {
                       //console.log(res);
                       if (res.ok) {

                           //console.log(res);
                           if (res.data.Success) {
                               this.query();
                               $.fancybox.close();
                           } else {

                              
                           }


                       }
                   }, errorHandler);
            },
            onEdit: function (card) {
                //console.log(card);
                this.editCard = card;
                this.isCreate = false;
                
            },
            editCardSave: function() {
                this.$http.post(this.urls.editCard, this.editCard)
                    .then(function (res) {
                        if (res.ok) {
                            //console.log(res);
                            if (res.data.Success) {
                                this.query();
                                $.fancybox.close();
                            } else {


                            }
                        }
                    }, errorHandler);
            },
           
            //更新卡片排序
            onUpdate: function () {
                this.$http.post(this.urls.updateCards, this.querys)
                   .then(function (res) {
                       //console.log(res);
                       if (res.ok) {
                           if (res.data.Success) {
                               this.query();
                               $.notify("更新成功", "success");
                           }
                       }
                   }, errorHandler);
            },
            onDelete: function (id) {
                if (!confirm('確定刪除?')) {
                    return;
                }
                console.log(id);
                this.$http.get(this.urls.deleteCard + id).then(function (ret) {
                    console.log(ret);
                    if (ret.data.Success) {
                        this.query();
                    } else {
                        alert(ret.data.Message);
                    }
                },errorHandler);
            },
            updateDescription: function(c) {
                this.editCard.Contents = c;
            },
            onEditContent: function (card) {
                //console.log(card);
                var vm = this;
               
                this.editCard = card;
                if (this.editCard.Contents==null) {
                    this.editCard.Contents = "";
                }
            },
            //更新卡片內文
            onEditCardContentSave: function() {
                this.$http.post(this.urls.updateContents, this.editCard)
                  .then(function (res) {
                      //console.log(res);
                      if (res.ok) {
                          if (res.data.Success) {
                              this.query();
                              //$.fancybox.close();
                              $('#popUpEditContetn').modal('hide');
                          }
                      }
                  }, errorHandler);
            },
            fileSubmit: function (file, note) {
                if (!file || file.size === 0) {
                    alert('請選擇上傳的檔案');
                    return;
                }

                var data = new FormData();
                //data.append('id', this.caseId);
                data.append('file', file);
                data.append('alt', note);


                this.$http.post(this.urls.uploadPhoto, data).then(function (ret) {
                    //console.log(ret);
                    if (ret.ok) {
                        //this.photos.push(ret.data);
                        var photo = ret.data;

                        //var p = {
                        //    createdDate: photo.CreatedDate,
                        //    extension: photo.Extension,
                        //    isDefault: false,
                        //    photoId: photo.PhotoId,
                        //    updatedDate: photo.UpdatedDate
                        //};
                        //console.log('photo=');
                        //console.log(photo);
                        if (this.isCreate) {
                            this.newData.iconId = photo.ID;
                        } else {
                            this.editCard.IconId = photo.ID;
                        }

                        
                        $('.fileUpload-pop').modal('hide');
                    }
                }, errorHandler);
            },
            getIconUrl: function (card) {
                var iconId = card.IconId;
                var iconUrl=window.appRoot.rel + "Content/geo/img/";

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
            //geoLoading: geo.loading,
            //geoDatepicker: geo.geoDatepicker,
            //geoPagination: geo.pagination,
            //geoPageInfo: geo.pageInfo,
            //geoPageSize: geo.pageSize
            'geo-file-uploader': geo.fileUploader,
            'geo-tinymce': geo.geoTinymce,
        }
    });
    window.app = app;
})();