'use strict';
angular
    .module("umbraco")
    .controller("PlayerEditController",
                    ["$scope"
                    , "$log"
                    , "$location"
                    , "$filter"
                    , "$routeParams"
                    , "playerResource"
                    , "navigationService"
                    , "notificationsService"
                        , PlayerEditController]);

function PlayerEditController($scope, $log, $location, $filter, $routeParams, playerResource, navigationService, notificationsService) {

    $log.debug('PlayerEditController loaded');
    $scope.loaded = false;
    $scope.editing = false;

    $scope.season = 2016;

    $scope.playerId = $routeParams.id;

    playerResource.getMember($scope.playerId).then(function (response) {
        $log.debug('playerResource.getMember');
        $scope.player = response.data;
        if ($scope.player.Season < 1)
            $scope.player.Season = $scope.season;

    }, function (reason) {
        $log.debug('playerResource.getMember failed');
        $scope.message = reason;
        notificationsService.error("Kan ikke finde oplysningerne", error.Message);
    });
    $scope.loaded = true;

    $scope.save = function () {
        $log.debug('save called');
        $log.debug($scope.player);
        playerResource.save($scope.player).then(function (response) {
            $scope.player = response.data;
            $scope.editing = true;
            $scope.objectForm.$dirty = false;
            navigationService.syncTree({ tree: 'playerTree', path: [-1, -1], forceReload: true });
            notificationsService.success("Success", "Oplysningerne er blevet opdateret");
        }, function (reason) {
            $log.debug('matchResource.get failed');
            $scope.message = reason;
            notificationsService.error("Der opstod en fejl i opdateringen: ", reason);
        });
    }
};
