angular.module("umbraco").controller("Caddie.DatepickerController",
    function ($scope, notificationsService, assetsService, angularHelper, userService, $element) {

        //lists the custom language files that we currently support
        //setup the default config
        var config = {
            pickDate: true,
            pickTime: false,
            pick12HourFormat: false,
            format: "YYYY-MM-DD"
        };

        //handles the date changing via the api
        function applyDate(e) {
            angularHelper.safeApply($scope, function () {
                // when a date is changed, update the model
                console.log('$scope.dateValue before', $scope.dateValue)
                var elementData = $element.find("div:first").data().DateTimePicker;
                if (e.localDate) {
                   $scope.dateValue = e.localDate.toString("YYYY-MM-DD");
                }
                console.log('$scope.dateValue after', $scope.dateValue)
                console.log($scope)
                elementData.setValue($scope.dateValue);
            });
        }

        //get the current user to see if we can localize this picker
        userService.getCurrentUser().then(function (user) {

            assetsService.loadCss('lib/datetimepicker/bootstrap-datetimepicker.min.css').then(function () {
                var filesToLoad = ["lib/datetimepicker/bootstrap-datetimepicker.js"];

                assetsService.load(filesToLoad).then(
                    function () {
                        //The Datepicker js and css files are available and all components are ready to use.
                        console.log('loaded')
                        // Open the datepicker and add a changeDate eventlistener

                        var element = $element.find("div:first");
                        // Open the datepicker and add a changeDate eventlistener
                        console.log(element);
                        console.log($scope.dateValue);

                        element.datetimepicker(config).on("changeDate", applyDate);
                        if ($scope.dateValue) {
                            //manually assign the date to the plugin
                            console.log('value assigned')
                            element.datetimepicker("setValue", $scope.dateValue);
                        }

                        //Ensure to remove the event handler when this instance is destroyted
                        $scope.$on('$destroy', function () {
                            $element.find("div:first").datetimepicker("destroy");
                        });
                    });
            });
        });
    });