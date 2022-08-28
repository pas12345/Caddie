'use strict';
angular
    .module("umbraco")
    .controller("ClubController",
                    ["$scope"
                         , "$log"
                         , "$location"
                         , "$filter"
                         , "$routeParams"
                         , "dialogService"
                         , "courseResource"
                         , "notificationsService"
                         , "navigationService"
                        , ClubController]);

function ClubController($scope, $log, $location, $filter, $routeParams, dialogService, courseResource, notificationsService, navigationService) {

    $log.debug('ClubController loaded');

    $scope.club = {
        Id: -1,
        StringValue: ""
    };

    $scope.submit = function () {
        $scope.message = null;
        $log.debug($scope.club);
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

