'use strict';
angular
    .module("umbraco")
    .controller("TourController",
                    ["$scope"
                         , "$log"
                         , "$location"
                         , "$filter"
                         , "$routeParams"
                         , "tourResource"
                         , "notificationsService"
                         , "navigationService"
                        , TourController]);

function TourController($scope, $log, $location, $filter, $routeParams, tourResource, notificationsService, navigationService) {

    $log.debug('TourController loaded');

    $scope.tour = null;

    $scope.save = function () {
        $scope.message = null;
        $log.debug('save');
        tourResource.save($scope.tour).then(function (response) {
            navigationService.syncTree({ tree: 'tourTree', path: [-1, -1], forceReload: true });
            notificationsService.success("Success", "Turen er oprettet");
        },
        function (reason) {
            $scope.message = reason.data.ExceptionMessage;
            notificationsService.error("Data blev ikke opdateret");
        })
    };

    $scope.close = function () {
        $location.path("/tours");
    };
};