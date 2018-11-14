(function () {
    'use strict';

    function showcaseController($scope, $sce, $timeout, showcaseService) {
        $scope.vm = {
            detail: null
        };

        $scope.getCurrentDetail = function (param) {
            showcaseService.getDetail(param).success(function (res) {
                $scope.vm.detail = res;
                if (res.PhotoList.length) {
                    $timeout(function () {
                        $('#slickWrapper').slick({
                            dots: false,
                            arrows: true,
                            infinite: true,
                            speed: 300,
                            slidesToShow: 4,
                            slidesToScroll: 1,
                            autoplay: true,
                            responsive: [{
                                breakpoint: 1024,
                                settings: {
                                    slidesToShow: 3,
                                    slidesToScroll: 1,
                                    infinite: true,
                                }
                            }, {
                                breakpoint: 600,
                                settings: {
                                    slidesToShow: 2,
                                    slidesToScroll: 2,
                                    dots: true,
                                    arrows: false
                                }
                            }, {
                                breakpoint: 480,
                                settings: {
                                    slidesToShow: 1,
                                    slidesToScroll: 1,
                                    dots: true,
                                    arrows: false
                                }
                            }]
                        })
                    }, 3000);
                }

                $('#detailTemplate').show();
                $('#imgLoading').hide();
            });
        }

        function activate() {
            $scope.getCurrentDetail($scope.caseId);
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
    }]).filter("trust", ['$sce', function ($sce) {
        return function (htmlCode) {
            if (htmlCode != null) {
                htmlCode = htmlCode.replace(/(\r\n|\r|\n)/g, '<br />');
            }
            return $sce.trustAsHtml(htmlCode);
        }
    }]);
    showcaseController.$inject = ["$scope", "$sce", "$timeout", "showcaseService"];
})();