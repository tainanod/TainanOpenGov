(function () {
    'use strict';

    function datasetController($scope, $sce, $timeout, datasetService) {
        $scope.vm = {
            optionModel: [10, 20, 50, 100],
            detail: null,
            popGuide: null
        };

        $scope.getPopGuide = function (guideId) {
            var target = $.grep($scope.vm.detail.DataSetHistoryList, function (x) {
                return x.HistoryId === guideId;
            })

            $scope.vm.popGuide = target[0].Contents;
            markdownToHtml("dvMarkdownPop", $scope.vm.popGuide);
            $timeout(function () {
                $('#guidePop').modal({
                    show: true,
                    backdrop: 'static'
                });
            }, 100)
        }

        $scope.getDetail = function (param) {
            datasetService.getDetail(param).success(function (res) {
                if (!res) {
                    console.log('no data')
                    return;
                }
                $scope.vm.detail = res;
                markdownToHtml("dvMarkdown", res.Contents);
                $('#imgLoading').hide();
            })
        };

        function activate() {
            $scope.getDetail($scope.datasetId);
        }

        function markdownToHtml(myId, contents) {
            $('#' + myId).html('');
            var testEditormdView2 = editormd.markdownToHTML(myId, {
                markdown: contents,
                htmlDecode: "style,script,iframe",
                emoji: true,
                taskList: true,
                tex: true,
                flowChart: false,
                sequenceDiagram: false,
            });
        }

        /*初始化 (延遲0.5秒)*/
        $timeout(activate, 300);

    }

    angular.module("app").controller("datasetController", datasetController).filter("toLower", function () {
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
    }]);

    datasetController.$inject = ["$scope", "$sce", "$timeout", "datasetService"];
})();