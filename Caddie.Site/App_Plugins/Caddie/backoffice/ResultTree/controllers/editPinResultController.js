'use strict';
angular
    .module("umbraco")
    .controller("EditPinResultController",
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
                        , EditPinResultController]);

function EditPinResultController($scope, $log, $location, $filter, $routeParams, resultResource,
    playerResource, settingResource, navigationService, notificationsService) {

    $log.debug('EditPinResultController: ' + $routeParams.id);
    $scope.id = $routeParams.id;

    settingResource.getSeason().then(function (response) {
        $scope.season = response.data;
        $log.debug('season: ' + $scope.season);
        playerResource.getMemberNames($scope.season).then(function (response) {
            $scope.players = response.data;
            $log.debug($scope.players);
        }, function (reason) {
            $log.debug('playerResource.getMemberNames failed');
            $scope.message = reason;
        });
    }, function (reason) {
        $log.debug('settingResource.getSeason()');
        $scope.message = reason;
    });


    resultResource.getPinResultById($scope.id).then(function (response) {
        $log.debug("getPinResult(" + $scope.id + ")");
        $log.debug(response.data);
        $scope.pin = response.data;
    },
    function (reason) {
        $scope.message = reason;
        notificationsService.error("Data blev ikke hentet", error.Message);
    });

    $scope.submit = function () {
        $scope.message = null;
        $log.debug('submit: ');
        resultResource.savePinResult($scope.pin).then(function (response) {
            $scope.message = "... resultatet er gemt";
            navigationService.syncTree({ tree: 'resultTree', path: [-1, , $routeParams.id], forceReload: true, activate: true });
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