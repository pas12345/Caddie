'use strict';
angular
    .module("umbraco")
    .controller("PinResultController",
                    ["$scope"
                         , "$log"
                         , "$location"
                         , "$filter"
                         , "$routeParams"
                         , "resultResource"
                         , "playerResource"
                        , "settingResource"
                        , "navigationService"
                         , "notificationsService"
                        , PinResultController]);

function PinResultController($scope, $log, $location, $filter, $routeParams, resultResource,
    playerResource, settingResource, navigationService, notificationsService) {

    $log.debug('PinResultController: ' + $routeParams.id);
    $scope.matchId = $routeParams.id;
    $scope.pin = {};
    $scope.pin.matchId = $scope.matchId
    settingResource.getSeason().then(function (response) {
        $scope.season = response.data;
        playerResource.getMemberNames($scope.season).then(function (response) {
            $scope.players = response.data;
            $log.debug('season: ' + $scope.season);
            $log.debug($scope.players);
        }, function (reason) {
            $log.debug('playerResource.getMemberNames failed');
            $scope.message = reason;
        });
    }, function (reason) {
        $log.debug('settingResource.getSeason()');
        $scope.message = reason;
    });

    $scope.submit = function () {
        $scope.message = null;
        $log.debug('submit: ');
        resultResource.savePinResult($scope.pin).then(function (response) {
            $scope.message = "... resultatet er gemt";
            navigationService.syncTree({ tree: 'resultTree', path: [-1, , $routeParams.id], forceReload: true, activate: true });
            $scope.pin = {};
            $scope.pin.matchId = $scope.matchId
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