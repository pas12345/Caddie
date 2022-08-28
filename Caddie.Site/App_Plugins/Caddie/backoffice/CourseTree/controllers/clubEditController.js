'use strict';
angular
    .module("umbraco")
    .controller("ClubEditController",
                    ["$scope"
                         , "$log"
                         , "$location"
                         , "$filter"
                         , "$routeParams"
                         , "courseResource"
                         , "notificationsService"
                         , "navigationService"
                        , ClubEditController]);

function ClubEditController($scope, $log, $location, $filter, $routeParams, courseResource, notificationsService, navigationService) {

    $log.debug('ClubEditController loaded');
    $scope.club = {};

    courseResource.getClub($routeParams.id).then(function (response) {
        $log.debug(response.data);
        $scope.club = response.data;
    }, function (reason) {
        $log.debug('courseResource.get getClub failed: ' + $routeParams.id);
        $scope.message = reason;
    });

    $scope.submit = function () {
        $scope.message = null;
        $log.debug( $scope.club);
        courseResource.updateClub($scope.club).then(function (response) {
            notificationsService.success("Success", "Ændringerne er gemt");
        },
        function (reason) {
            $scope.message = reason.data.Message;
            $log.debug(reason.data);
            notificationsService.error("Data blev ikke opdateret", error.Message);
            navigationService.syncTree({ tree: 'courseTree', path: [-1, -1], forceReload: true });
        })
    };

};