(function () {
    'use strict';
    function discussController($scope, $sce, $timeout, discussService) {
        $scope.vm = {
            imgList: [],
            tagList: [],
            sortList: [
                { name: '依發言時間', value: 1 },
                { name: '依推文次數', value: 2 }
            ]
        };

        $scope.Today = new Date();

        $scope.forum = {
        };

        $scope.subForum = [];

        $scope.page = {};

        $scope.style = {
            cursorNA: { cursor: 'not-allowed' }
        };

        $scope.replys = {};

        $scope.showAnnounceTop = 'block';
        $scope.showAnnounce = 'none';
        $scope.getTag = 0;
        $scope.getKeyWord = '';
        $scope.getSort = 0;

        function toTrustHtml(input) {
            if (input == undefined) {
                return undefined;
            }

            return $sce.trustAsHtml(input.replace("\\n", "<br>"))
        }

        function activate() {
            if ($scope.forumId == undefined) {
                location.href = window.appRoot.rel + "home/index";
            }

            /*取得登入資訊*/
            discussService.getUser().success(function (ret) {
                if (ret.Success) {
                    $scope.user = ret.Data;
                }
            });

            /*取得討論頁面資訊*/
            discussService.getForum($scope.forumId).success(function (ret) {
                if (ret.Success) {
                    ret.Data.Description = toTrustHtml(ret.Data.Description);

                    $scope.showAnnounce = ret.Data.Announcement ? 'block' : 'none';

                    $scope.forum = ret.Data;
                    for (var i in ret.Data.Photos) {
                        $scope.vm.imgList.push(window.appRoot.rel + "resource/photo/" + ret.Data.Photos[i].PhotoId + "?size=4");
                    }
                }
                else {
                    alert(ret.Message);
                }
            }).error(function () { alert("Invoke Service Error"); });
            
            /*取得標籤下拉選單資訊*/
            discussService.getTags($scope.forumId).success(function (tags) {
                $scope.vm.tagList = tags;
            });

            /*取得子討論資訊*/
            discussService.getSubForum($scope.forumId).success(function (data) {
                //debugger;
                $scope.subForum = data;
                
            }).error(function () { alert("Invoke Service Error"); });

            /*呼叫取得討論串*/
            $scope.GetDiscuss($scope.forumId);

            /*取得投票清單*/
            discussService.getVoteList($scope.forumId).success(function (res) {
                $scope.voteList = res;
            })

            /*取得問卷清單*/
            discussService.getQuestionList($scope.forumId).success(function (res) {
                $scope.questionList = res;
            })
        }
               
        /*取得討論串*/
        $scope.GetDiscuss = function (forumId, currentPage, pageSize) {            
            if (forumId == undefined) {
                return;
            }
            var param = { 'ForumId': forumId, 'KeyWord': $scope.getKeyWord, 'TagId': $scope.getTag, 'SortId': $scope.getSort, "CurrentPage": currentPage };
            
            discussService.getDiscuss(param).success(function (ret) {                
                if (ret.Success) {
                   
                    for (var i in ret.Data) {
                        var data = ret.Data[i];
                        data.TagNames = [];
                        for (var y in data.Tags) {
                            data.TagNames.push(data.Tags[y].TagName);
                        }
                        data.Message = toTrustHtml(data.Message);
                        data.PushAmt = data.PushUserIds.length;
                        data.RefusePush = $scope.user == undefined || $scope.user.Id == data.AspNetUser.Id || $.inArray($scope.user.Id, data.PushUserIds) >= 0;
                        data.RefuseReply = $scope.user == undefined;
                    }
                    $scope.page.currentPage = ret.CurrentPage;
                    $scope.page.pageSize = ret.PageSize;
                    $scope.page.totalRecord = ret.Total;
                    $scope.page.totalPages = ret.TotalPage;
                    $scope.discuss = ret;
                }
                else {
                    alert(ret.Message);
                }
            }).error(function () { alert("Invoke Service Error"); });
        };

        /*換頁動作*/
        $scope.pageChange = function () {
            $scope.GetDiscuss($scope.forumId, $scope.page.currentPage, $scope.page.pageSize);
        }

        /*搜尋-標籤*/
        $scope.getTagSearch = function () {
            $scope.GetDiscuss($scope.forumId, $scope.page.currentPage, $scope.page.pageSize);
        }

        $scope.getKeyWordSearch = function () {
            $scope.GetDiscuss($scope.forumId, $scope.page.currentPage, $scope.page.pageSize);
        }

        /*搜尋-排序*/
        $scope.getSortSearch = function () {
            $scope.GetDiscuss($scope.forumId, $scope.page.currentPage, $scope.page.pageSize);
        }

        /*未登入時按下登入後參與討論*/
        $scope.loginClick = function () {
            var url = window.appRoot.rel + 'Account/Login?ReturnUrl=' + location.pathname + location.search;           
            location.href = url;
        };

        /*推文*/
        $scope.pushDiscuss = function (discussId, pushUserId) {
            
            discussService.pushDiscuss(discussId, pushUserId).success(function (ret) {
                if (ret.Success) {
                    var discuss = $scope.discuss.Data;
                    for (var i in discuss) {
                        if (discuss[i].DiscussId == discussId) {
                            discuss[i].PushAmt += 1;
                            discuss[i].RefusePush = true;
                            return;
                        }
                    }
                } else {
                    alert(ret.Message);
                }
            }).error(function () { alert("Invoke Service Error"); });
            
        }
        
        /*討論串回復列按鈕*/
        $scope.replyClick = function (discussId) {

            discussService.getReply(discussId).success(function (ret) {
                for (var i in ret.Data) {
                    ret.Data[i].Message = toTrustHtml(ret.Data[i].Message);
                }

                $scope.replys[discussId] = ret.Data;

            }).error(function () { alert("Invoke Service Error"); })

        };

        /*回復送出*/
        $scope.replySubmit = function (upperDiscuss) {

            var message = upperDiscuss.replyTextArea;

            if (message == undefined || message == "") {
                toastr.error("回覆內容不可為空白");
                return;
            }


            var data = { ForumId: $scope.forumId, UserId: $scope.user.Id, Message: message, UpperId: upperDiscuss.DiscussId };

            discussService.sendReply(data).success(function (ret) {
                if (ret.Success) {
                    ret.Data.Message = toTrustHtml(ret.Data.Message);
                    $scope.replys[upperDiscuss.DiscussId].unshift(ret.Data);
                    upperDiscuss.replyTextArea = undefined;
                    upperDiscuss.ReplyAmount += 1;
                } else {
                    alert(ret.Message);
                }
            }).error(function () { alert("Invoke Service Error"); });
        }

        /*Discuss 送出*/
        $scope.discussSubmit = function () {

            var discuss= $scope.vm.DiscussTextArea;

            if (discuss == undefined || discuss == "") {
                toastr.error("回覆內容不可為空白");
                return;
            }

            var tagIds = [];
            for (var i in $scope.vm.selectedTag)
            {
                tagIds.push($scope.vm.selectedTag[i].TagId);
            }

            var data = { ForumId: $scope.forumId, UserId: $scope.user.Id, Message: $scope.vm.DiscussTextArea, TagIds: tagIds };
            discussService.sendReply(data).success(function (ret) {
                if (ret.Success) {
                    $scope.vm.DiscussTextArea = undefined;
                    $scope.vm.selectedTag = [];
                    $scope.GetDiscuss($scope.forumId);
                } else {
                    alert(ret.Message);
                }
            }).error(function () { alert("Invoke Service Error"); });
        }
        
        /*點擊時 【取消new圖示】*/
        $scope.getClick = function () {
            /*取得討論頁面資訊*/
            discussService.getForum($scope.forumId).success(function (ret) {
                if (ret.Success) {
                    $scope.forum = ret.Data;
                }
            });
        }

        /*初始化 (延遲0.5秒)*/
        $timeout(activate, 300);

        //錨點
        function gotoHeader() {
            $.getUrlParam = function (name) {
                var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
                var r = window.location.search.substr(1).match(reg);
                if (r != null) return unescape(r[2]);
                return null;
            }
            var nid = $.getUrlParam('did');
            if (nid != null) {
                $('html,body').animate({
                    scrollTop: $('#' + nid).offset().top
                }, 800);
            }
        }

        $timeout(gotoHeader, 1000);
    }
    angular.module("app").controller("discussController", discussController);
    discussController.$inject = ["$scope", "$sce", "$timeout", "discussService"];
})();