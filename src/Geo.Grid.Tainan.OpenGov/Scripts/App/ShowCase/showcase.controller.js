(function () {
    'use strict';

    function showcaseController($scope, $sce, $timeout, showcaseService) {
        $scope.vm = {
            list: [],
            tagList: [],
            optionModel: [10, 20, 50, 100],
            submitForm: {
                title: "",
                creator: "",
                introduce: "",
                userEmail:"",
                links: [],
                tags: [],
                appImg: null,
                demoImgs: null
            },
            linkKeyIn: { title: null, content: null },
            isValidLink: true,
            appImgFileRemind: '',
            demoImgsFileRemind: ''
        };

        $scope.page = {
            currentPage: 1,
            pageSize: 10,
            totalRecord: 10,
            totalPages: 1
        };

        $scope.submitted = false;

        //驗證
        $scope.caseFormSubmit = function () {
            //成果名稱、創作者、連絡e-mail、應用簡介、主題圖片
            var vmD = $scope.vm.submitForm;
            if (vmD.title == "" || vmD.creator == "" || vmD.userEmail == "" || vmD.introduce == "" || vmD.appImg == null) {
                $scope.submitted = true
            } else {
                $scope.getSubmit();
            }
        }

        //送出
        $scope.getSubmit = function () {
            var param = new FormData();
            param.append("Title", $scope.vm.submitForm.title);
            param.append("UserName", $scope.vm.submitForm.creator);
            param.append("UserEmail", $scope.vm.submitForm.userEmail);
            param.append("Info", $scope.vm.submitForm.introduce);
            param.append("AppImg", $scope.vm.submitForm.appImg);

            if ($scope.vm.submitForm.demoImgs != null) {
                for (var i = 0; i < $scope.vm.submitForm.demoImgs.length; i++) {
                    param.append("AppImgArray[" + i + "]", $scope.vm.submitForm.demoImgs[i]);
                }
            }
            if ($scope.vm.submitForm.links != null) {
                for (var i = 0; i < $scope.vm.submitForm.links.length; i++) {
                    var link = $scope.vm.submitForm.links[i];
                    param.append("LinkArray[" + i + "].Title", link.title);
                    param.append("LinkArray[" + i + "].Content", link.content);
                }
            }

            if ($scope.vm.selectedTag != null) {
                for (var i = 0; i < $scope.vm.selectedTag.length; i++) {
                    var tag = $scope.vm.selectedTag[i];
                    param.append("TagArray[" + i + "].PageGuid", tag.PageGuid);
                    param.append("TagArray[" + i + "].PageName", tag.PageName);
                }
            }

            showcaseService.getCreate(param).success(function (result) {
                if (result.Success) {
                    blockRemoveSubmit();
                    $('#submitPop').modal('hide');
                    window.location.href = window.appRoot.rel + 'ShowCase/Detail/' + result.ID;
                }                
            });
        }
        
        function blockRemoveSubmit() {
            $scope.submitted = false;
            $scope.vm.submitForm.title = "";
            $scope.vm.submitForm.creator = "";
            $scope.vm.submitForm.userEmail = "";
            $scope.vm.submitForm.introduce = "";
            $scope.vm.submitForm.links = [];
            $scope.vm.submitForm.tags = [];
            $scope.vm.submitForm.appImg = null;
            $scope.vm.submitForm.demoImgs = null;           
        }

        $scope.removeImg = function (target) {
            switch (target) {
                case 1:
                    $scope.vm.submitForm.appImg = null;
                    break;
                case 2:
                    $scope.vm.submitForm.demoImgs = null;
                    break;
                default:
                    break;
            }
        }

        $scope.addLink = function () {
            if (!$scope.vm.linkKeyIn.title || !$scope.vm.linkKeyIn.content) {
                $scope.vm.isValidLink = false;

            } else {
                $scope.vm.isValidLink = true;
                var linkTarget = $scope.vm.linkKeyIn;
                $scope.vm.submitForm.links.push(linkTarget);
                $scope.vm.linkKeyIn = { title: null, content: null };
            }
        }

        $scope.deleteLink = function (idx) {
            $scope.vm.submitForm.links.splice(idx, 1);
        }

        $scope.$watch('vm.submitForm.appImg', function () {
            if ($scope.vm.submitForm.appImg) {
                $scope.vm.appImgFileRemind = $scope.vm.submitForm.appImg.name;
            } else {
                $scope.vm.appImgFileRemind = '未選擇任何檔案 / 格式僅限.GIF檔、.JPG檔、.PNG檔'
            }
        })

        $scope.$watch('vm.submitForm.demoImgs', function () {
            if ($scope.vm.submitForm.demoImgs) {
                var fileLength = $scope.vm.submitForm.demoImgs.length;
                $scope.vm.demoImgsFileRemind = '已選擇 ' + (fileLength == undefined ? 1 : fileLength) + ' 張圖片';
            } else {
                $scope.vm.demoImgsFileRemind = '未選擇任何檔案 / 格式僅限.GIF檔、.JPG檔、.PNG檔'
            }
        })

        $scope.getCurrentList = function (currentPage, pageSize) {
            var param = {
                "CurrentPage": currentPage,
                "PageSize": pageSize
            }
            showcaseService.getList(param).success(function (res) {
                if (!res) {
                    $('#imgLoading').hide();
                    console.log('no data')
                    return;
                }

                $scope.vm.list = res.Data;
                $scope.page.currentPage = res.CurrentPage;
                $scope.page.pageSize = res.PageSize;
                $scope.page.totalRecord = res.Total;
                $scope.page.totalPages = res.TotalPage;
                //$scope.$apply();
                $('#imgLoading').hide();
            });

        };

        $scope.pageChange = function () {
            $scope.getCurrentList($scope.page.currentPage, $scope.page.pageSize);
        }

        $scope.blockPop = function () {
            showcaseService.getTags().success(function (tags) {
                $scope.vm.tagList = tags;
            });
        }

        function userDetail() {
            showcaseService.getUserDetail().success(function (result) {
                $scope.vm.submitForm.userEmail = result.UserEmail;
            });
        }

        function activate() {
            $scope.getCurrentList($scope.page.currentPage, $scope.page.pageSize);
            userDetail();
        }

        /*初始化 (延遲0.5秒)*/
        $timeout(function () {
            activate()
        }, 300);
    }

    angular.module("app").controller("showcaseController", showcaseController).directive("fileread", [function () {
        return {
            scope: {
                fileread: "="
            },
            link: function (scope, element, attributes) {
                element.bind("change", function (changeEvent) {
                    scope.$apply(function () {
                        var fileLength = changeEvent.target.files.length;
                        if (fileLength === 1) {
                            scope.fileread = changeEvent.target.files[0];
                        } else if (fileLength > 1) {
                            scope.fileread = changeEvent.target.files
                        }
                    });
                });
            }
        }
    }]);

    showcaseController.$inject = ["$scope", "$sce", "$timeout", "showcaseService"];
})();