'use strict';
angular
    .module("umbraco")
    .controller("ReportTourController",
                    ["$scope"
                         , "$log"
                         , "$location"
                         , "$filter"
                         , "$routeParams"
                         , "tourResource"
                         , "reportResource"
                        , ReportTourController]);

function ReportTourController($scope, $log, $location, $filter, $routeParams, tourResource, reportResource) {

    $scope.tour = {};
    $scope.tours = null;
    $scope.tourText = 'Første text';
    $scope.selectedTourId = 0;

    tourResource.getTours().then(function (response) {
        $scope.tours = response.data;
        $scope.tour = $scope.tours[0];
    }, function (reason) {
        $log.debug('tourResource.getTours failed');
        $scope.message = reason;
    });

    $scope.tourChange = function () {
        matchResource.getTour($scope.tour.Id).then(function (response) {
            $scope.tour = response.data;
        }, function (reason) {
            $log.debug('matchResource.getTour failed');
            $scope.message = reason;
        });
    }

    $scope.Print = function () {
        $scope.message = "Udskriften bliver nu dannet og downloadet";
        reportResource.printTourRegistration($scope.tour.Id).then(function (response) {
            var file = new Blob([response.data], {
                type: 'application/pdf'
            });
            //trick to download store a file having its URL
            var fileURL = URL.createObjectURL(file);
            var a = document.createElement('a');
            a.href = fileURL;
            a.target = '_blank';
            a.download = 'TurTilmelding.pdf';
            document.body.appendChild(a);
            a.click();
            $scope.message = "Så er udskriften downloadet";
        }, function (reason) {
            $scope.message = "Udskrift fejlede: " + reason;
        });
    }
};