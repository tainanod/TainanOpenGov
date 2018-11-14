(function () {
    var app = angular.module("app", ['ui.bootstrap', 'oi.select']).filter('to_trusted', ['$sce', function($sce){
        return function(text) {
            return $sce.trustAsHtml(text);
        };
    }]);
})();