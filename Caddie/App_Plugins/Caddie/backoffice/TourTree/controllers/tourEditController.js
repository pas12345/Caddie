'use strict';
angular
    .module("umbraco")
    .controller("TourEditController",
                    ["$scope"
                         , "$log"
                         , "$location"
                         , "$filter"
                         , "$routeParams"
                         , "tourResource"
                         , "notificationsService"
                         , "navigationService"
                        , TourEditController]);

function TourEditController($scope, $log, $location, $filter, $routeParams, tourResource, notificationsService, navigationService) {

    $log.debug('TourEditController loaded');

    $scope.tourId = $routeParams.id;
    $scope.tour = null;

    tourResource.getTour($scope.tourId).then(function (response) {
        $scope.tour = response.data;
        $log.debug($scope.tour);
        $scope.tourText = $scope.tour.Description + ', deb : ' + $scope.tour.TourDate;
    }, function (reason) {
        $log.debug('tourResource.get failed');
        $scope.message = reason;
    });

    $scope.save = function () {
        $scope.message = null;
        $log.debug('save');
        tourResource.save($scope.tour).then(function (response) {
            navigationService.syncTree({ tree: 'tourTree', path: [-1, -1], forceReload: true });
            notificationsService.success("Success", "Turen er opdateret");
        },
        function (reason) {
            $scope.message = reason.data.ExceptionMessage;
            notificationsService.error("Data blev ikke opdateret");
        })
    };
};