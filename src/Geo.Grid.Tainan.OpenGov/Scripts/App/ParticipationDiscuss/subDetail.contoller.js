(function () {
    "use strict";

    function subDetailContoller($scope, $timeout, $sce, discussService) {

        $scope.forum = {};
        //$scope.vm = {};
        $scope.replys = {};
        $scope.page = {};
        //$scope.page = {
        //    currentPage: 1,
        //    pageSize: 10
        //};

        $scope.vm = {
            imgList: [],
            tagList: [],
            sortList: [
                { name: '依發言時間', value: 1 },
                { name: '依推文次數', value: 2 }
            ]
        };

        $scope.replys = {};
        $scope.showAnnounceTop = 'block';
        $scope.showAnnounce = 'none';
        $scope.getTag = 0;
        $scope.getKeyWord = '';
        $scope.getSort = 0;

        $scope.style = {
            cursorNA: { cursor: 'not-allowed' }
        };

        function toTrustHtml(input) {
            if (input == undefined) {
                return undefined;
            }

            return $sce.trustAsHtml(input.replace("\\n", "<br>"))
        }

        /*初始化*/
        function activate() {

            /*取得登入資訊*/
            discussService.getUser().success(function (ret) {
                if (ret.Success) {
                    $scope.user = ret.Data;
                }
            });

            /*取得forum和parent forum的資訊*/
            discussService.getForum($scope.forumId).success(function (ret) {
                if (ret.Success) {
                    ret.Data.Discription = toTrustHtml(ret.Data.Description);                    
                    $scope.showAnnounce = ret.Data.Announcement ? 'block' : 'none';                    

                    $scope.forum = ret.Data;

                    $scope.upperForumId = ret.Data.UpperId;

                    /*取得討論頁面資訊*/
                    discussService.getForum($scope.upperForumId).success(function (ret) {
                        if (ret.Success) {
                            $scope.vm.pSubject = ret.Data.Subject;
                        }
                        else {
                            alert(ret.Message);
                        }
                    }).error(function () { alert('Invoke Service Error') });

                    /*取得標籤下拉選單資訊*/
                    discussService.getTags($scope.upperForumId).success(function (tags) {
                        $scope.vm.tagList = tags;
                    });

                    
                }
                else {
                    alert(ret.Message);
                }
            }).error(function () { alert('Invoke Service Error') });

            /*呼叫取得討論串*/
            $scope.GetDiscuss($scope.forumId);
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
                            var item = ret.Data[i];
                            item.TagNames = [];
                            for (var y in item.Tags) {
                                item.TagNames.push(item.Tags[y].TagName);
                            }
                            item.Message = toTrustHtml(item.Message);
                            item.PushAmt = item.PushUserIds.length;
                            item.RefusePush = $scope.user == undefined || $scope.user.Id == item.AspNetUser.Id || $.inArray($scope.user.Id, item.PushUserIds) >= 0;
                            item.RefuseReply = $scope.user == undefined;
                        }
                        $scope.page.currentPage = ret.CurrentPage;
                        $scope.page.pageSize = ret.PageSize;
                        $scope.page.totalRecord = ret.Total;
                        $scope.page.totalPages = ret.TotalPage;
                        $scope.discuss = ret;
                    } else {
                        alert(ret.Message);
                    }
                }).error(function () { alert('Invoke Service Error') });
        };

        /*換頁*/
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

        /*按下回復展開回復Div後讀取該推文的所有回復*/
        $scope.replyClick = function (discussId) {

            discussService.getReply(discussId)
                .success(function (ret) {
                    for (var i in ret.Data) {
                        ret.Data[i].Message = toTrustHtml(ret.Data[i].Message);
                    }

                    $scope.replys[discussId] = ret.Data;
                }).error(function () { alert('Invoke Service Error') });

        }               

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

            var discuss = $scope.vm.DiscussTextArea;

            if (discuss == undefined || discuss == "") {
                toastr.error("回覆內容不可為空白");
                return;
            }

            var tagIds = [];
            for (var i in $scope.vm.selectedTag) {
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

        /* 延遲執行初始化*/
        $timeout(activate, 300);
    }
    angular.module("app").controller("subDetailContoller", subDetailContoller);

    subDetailContoller.$inject = ["$scope", "$timeout", "$sce", "discussService"];
})();