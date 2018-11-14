(function () {
    var app = angular.module("app", ['ui.bootstrap', 'oi.select']);

    toastr.options = {
        "positionClass": "toast-bottom-right",
        "timeOut": "3000",
        "showDuration": "0",
        "hideDuration": "0",
        "preventDuplicates": true,
    };


    $('body').addClass('detail');

})();