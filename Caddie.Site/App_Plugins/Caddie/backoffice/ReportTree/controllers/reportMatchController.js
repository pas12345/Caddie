'use strict';
angular
    .module("umbraco")
    .controller("ReportMatchController",
                    ["$scope"
                         , "$log"
                         , "$location"
                         , "$filter"
                         , "$routeParams"
                         , "matchResource"
                         , "reportResource"
                        , ReportMatchController]);

function ReportMatchController($scope, $log, $location, $filter, $routeParams, matchResource, reportResource) {

    $scope.match = {};
    $scope.matches = null;
    $scope.selectedMatchId = 0;

    matchResource.getMatchesAfter().then(function (response) {
        $scope.matches = response.data;
        $scope.match = $scope.matches[0];
    }, function (reason) {
        $log.debug('matchResource.get failed');
        $scope.message = reason;
    });
    
    $scope.MatchChange = function () {
        matchResource.getMatch($scope.match.Id).then(function (response) {
            $scope.match = response.data;
        }, function (reason) {
            $log.debug('matchResource.getMatch failed');
            $scope.message = reason;
        });
    }

    $scope.Print = function () {
        $scope.message = "Udskriften bliver nu dannet og downloadet";
        reportResource.printMatchRegistration($scope.match.Id).then(function (response) {
            var file = new Blob([response.data], {
                type: 'application/pdf'
            });
            //trick to download store a file having its URL
            var fileURL = URL.createObjectURL(file);
            var a = document.createElement('a');
            a.href = fileURL;
            $log.debug(fileURL);
            a.target = '_blank';
            a.download = 'MatchTilmelding.pdf';
            document.body.appendChild(a);
            a.click();
            $scope.message = "Så er udskriften downloadet";
        }, function (response) {
            $scope.message = "Udskrift fejlede: " + response;
        });
    }
};