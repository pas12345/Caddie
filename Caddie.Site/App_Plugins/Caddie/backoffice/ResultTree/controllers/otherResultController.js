'use strict';
angular
    .module("umbraco")
    .controller("OtherResultController",
                    ["$scope"
                         , "$log"
                         , "$location"
                         , "$filter"
                         , "$routeParams"
                         , "resultResource"
                         , "playerResource"
                         , "notificationsService"
                        , OtherResultController]);

function OtherResultController($scope, $log, $location, $filter, $routeParams, resultResource, playerResource, notificationsService) {

    $log.debug('OtherResultController');
    $scope.matchId = $routeParams.id;
    $scope.result = {
        MatchId: $routeParams.id
    };

    playerResource.getMemberNames(2016).then(function (response) {
        $scope.players = response.data;
        $log.debug($scope.players);
    }, function (reason) {
        $log.debug('playerResource.getMemberList failed');
        $scope.message = reason;
    });
    
    resultResource.getCompetitions().then(function (response) {
        $scope.competitions = response.data;
        $log.debug($scope.competitions);
    }, function (reason) {
        $log.debug('resultResource.getCompetitions failed');
        $scope.message = reason;
    });

    $scope.submit = function () {
        $scope.message = null;
        $log.debug('submit: ');
        resultResource.saveOtherResult($scope.result).then(function (response) {
            $scope.message = "... resultatet er gemt";
            navigationService.syncTree({ tree: 'resultTree', path: [-1, 1], forceReload: true });
            notificationsService.success("Success", "Resultatet er blevet gemt");
        },
        function (reason) {
            $scope.message = reason;
            notificationsService.error("Data blev ikke opdateret", error.Message);
        })
        }

    $scope.close = function () {
        $location.path("/results");
    };
};