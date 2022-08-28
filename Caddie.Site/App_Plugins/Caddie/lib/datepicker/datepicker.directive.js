angular.module("umbraco.directives")
.directive('datepicker', function ($window) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelCtrl) {
            var dpElement = $(element).find('.datetime-picker');
            dpElement.datetimepicker({
                format: "dd.mm.yyyy hh:ii",
                autoclose: true,
                language: "dk",
                startDate: new Date(),
                minuteStep: 10
            });
            dpElement.on('dp.change', function (event) {
                //need to run digest cycle for applying bindings
                scope.$apply(function () {
                    ngModelCtrl.$setViewValue(event.date);
                });
            });
        }
    };

}); 