angular.module("umbraco.directives")
.directive('simpledatepicker', function ($parse, $filter, assetsService) {
    return {
        restrict: 'E',
        scope: {
            field: '='
        },
        controller: function ($scope, $element) {
            assetsService.loadCss('/umbraco/lib/datetimepicker/bootstrap-datetimepicker.min.css').then(function () {
                var filesToLoad = ["/umbraco/lib/moment/moment-with-locales.js",
                                   "/umbraco/lib/datetimepicker/bootstrap-datetimepicker.js"];
                assetsService.load(filesToLoad, $scope).then(function () {
                    var utils = {
                        formatDate: function (value) {
                            var isAlreadyFormatted = /\d{4}-\d{2}-\d{2}$/.test(value);

                            if (isAlreadyFormatted) {
                                return value;
                            } if (value && value instanceof Date) {
                                return moment(value).format('YYYY-MM-DD');
                            } else if (value) {
                                var parsedDate = utils.parseDate(value);

                                if (parsedDate) {
                                    return moment(parsedDate).format('YYYY-MM-DD');
                                }
                            }
                        },
                        parseDate: function (value) {
                            if (value) {
                                console.log('parseDate', value);
                                return new Date(Date.parse(value));
                            }
                            //if (value && value.indexOf('0001-01-01') < 0) {
                            //    var parsedDate = value.replace('T', ' ').replace('Z', '');

                            //    return new Date(Date.parse(parsedDate));
                            //}
                        }
                    };
                    var container = $element.find("div.datepicker");

                    var existingDate = utils.parseDate($scope.field) || new Date();
                    $scope.field = utils.formatDate(existingDate);

                    $scope.$watch('field', function (newValue, oldValue) {
                        if (newValue) {
                            $scope._tempfield = utils.formatDate(newValue);
                        } else if (oldValue) {
                            $scope._tempfield = utils.formatDate(oldValue);
                        }
                    });

                    container.datetimepicker({
                        format: 'YYYY-MM-DD',
                        autoclose: true
                    })
                    .on('changeDate', function (ev) {
                        $scope.field = utils.formatDate(ev.date);
                        //$('.datepicker').hide();
                        //$(this).datepicker('hide');
                    });
                });
            });
        },
        templateUrl: '/App_Plugins/Caddie/lib/datepicker/DirectiveView.html'
    }
});