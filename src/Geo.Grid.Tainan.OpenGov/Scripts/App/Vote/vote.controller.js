(function () {
    'use strict';

    function voteController($scope, $sce, $timeout, voteService) {
        $scope.vm = {
            citys: [],
            towns: [],
            jobs: [],
            getUserMail: ''
        };

        $scope.getVoteContent = [];
        $scope.getInfoContent = [];

        $scope.getInfoContent[7] = { getCitySeq: '台南市', getTownSeq: null };

        $scope.submitted = false;

        //submit
        $scope.getSubmit = function () {

            //vote
            if ($scope.vote.SelectNumber > 1) {
                var origVoteContent = $scope.getVoteContent.map(function (val, idx, arr) {
                    if (val === true) {
                        return { 'OptionId': $scope.vote.OptionArray[idx].PageGuid };
                    };
                });

                $scope.voteContent = origVoteContent.filter(function (val, idx, arr) {
                    return val != undefined;
                })

                $scope.postList.OptionArray = $scope.voteContent;

            } else {
                var origVoteContent = $scope.getVoteContent.map(function (val, idx, arr) {

                    return { 'OptionId': val };

                });

                $scope.voteContent = origVoteContent.filter(function (val, idx, arr) {
                    return val != undefined;
                })

                
                $scope.postList.OptionArray = $scope.voteContent;
            }

            $scope.postList.UserMail = $scope.vm.getUserMail;

            //info
            var origInfoContent = $scope.getInfoContent.map(function (val, idx, arr) {
                if (idx == 2 || idx == 8) {

                    var targetArray = $scope.basicArray.filter(function (val) {

                        return val.GroupId == idx;
                    });

                    var targetId = targetArray[0].VoteBasicArray[0].BasicId;

                    return {
                        'BasicId': targetId,
                        'BasicDesc': val.BasicDesc
                    };

                } else if (idx == 6 || idx == 7) {
                    if (arr[idx].getTownSeq != null && arr[idx].getCitySeq != undefined) {
                        var targetArray = $scope.basicArray.filter(function (val) {
                            return val.GroupId == idx;
                        });

                        var targetId = targetArray[0].VoteBasicArray[0].BasicId;

                        return {
                            'BasicId': targetId,
                            'BasicDesc': arr[idx].getCitySeq + arr[idx].getTownSeq + ''
                        };
                    }

                } else {
                    return {
                        'BasicId': val.BasicId,
                        'BasicDesc': ''
                    };
                };
            });

            $scope.infoContent = origInfoContent.filter(function (val, idx, arr) {
                return val != undefined;
            })            

            $scope.postList.BasicArray = $scope.infoContent;

            voteService.getCreate($scope.postList).success(function (result) {                
                window.location.href = window.appRoot.rel + 'Vote/Message?id=' + result.ID;
            });
        }

        $scope.voteFormSubmit = function () {
            var basicLength = $scope.basicArray.length,            
                fillLength = 0;
            for (var key in $scope.getInfoContent) {
                fillLength += 1;
                if ($scope.getInfoContent[key].BasicDesc == '' || $scope.getInfoContent[key].getTownSeq === null || $scope.getInfoContent[key].getCitySeq === null) {
                    fillLength -= 1;
                }
            }

            if ($scope.vote.VerifyType == 2) {
                if ($scope.vm.getUserMail && $scope.optionRequired && fillLength === basicLength) {
                    $scope.getSubmit();
                } else {
                    $scope.voteForm.submitted = true;
                    alert('資料填寫不完整 或 有誤。');
                    $("html,body").animate({ scrollTop: 0 }, 900);
                }
            } else if ($scope.optionRequired && fillLength === basicLength) {
                
                $scope.getSubmit();
            } else {
                $scope.voteForm.submitted = true;
                alert('資料填寫不完整 或 有誤。');
                $("html,body").animate({ scrollTop: 0 }, 900);
            }
        }

        $scope.voteFormReset = function () {
            $scope.optionRequired = false;
            $scope.vm.getUserMail = '';
            $scope.getVoteContent = [];
            $scope.getInfoContent = [];
            $scope.getInfoContent[7] = { getCitySeq: '台南市', getTownSeq: null };
        }

        $scope.onSelected = function (n, target) {
            var selectedAmount = 0;
            for (var key in target) {
                
                if (target[key] === true || n == 1) {
                    selectedAmount += 1;
                }
            }

            if (selectedAmount > 0 && selectedAmount <= n) {
                $scope.optionRequired = true;
            } else {
                $scope.optionRequired = false;
            }
        }
        
        function activate() {
            voteService.getDetail($scope.voteId).success(function (res) {
                if (res.IsVote) {
                    window.location.href = window.appRoot.rel + 'Vote/Message';
                    return;
                }
                
                $scope.vote = res;
                $scope.basicArray = res.BasicArray;               

                $scope.postList = {
                    VoteId: $scope.voteId,
                    UserMail: $scope.vm.getUserMail,
                    OptionArray: [],
                    BasicArray: []
                };
                $scope.infoContent = [];
                $scope.voteContent = [];
            });

            voteService.getCityList().success(function (res) {
                $scope.vm.citys = res;
            });

            voteService.getTownList().success(function (res) {
                $scope.vm.towns = res;
            });
        }

        /*初始化 (延遲0.5秒)*/
        $timeout(activate, 300);

    }
    angular.module("app").controller("voteController", voteController);
    voteController.$inject = ["$scope", "$sce", "$timeout", "voteService"];
})();