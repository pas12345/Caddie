'use strict';
angular
    .module("umbraco")
    .controller("MatchplayResultController",
                    ["$scope"
                         , "$log"
                         , "$location"
                         , "$filter"
                         , "$routeParams"
                         , "matchplayResource"
                         , "notificationsService"
                        , MatchplayResultController]);

function MatchplayResultController($scope, $log, $location, $filter, $routeParams, matchplayResource, notificationsService) {

    $log.debug('MatchplayResultController loaded');
    $scope.id = $routeParams.id;
    $log.debug($scope.id);

    $scope.match = {};

    matchplayResource.getMatch($scope.id).then(function (response) {
        $scope.match = response.data;
        $scope.winners = [
            { Id: 0, StringValue: 'Ikke spillet' },
            { Id: 1, StringValue: $scope.match.TeamName1 },
            { Id: 2, StringValue: $scope.match.TeamName2 }
        ];
    }, function (reason) {
        $log.debug(' matchplayResource.getMatch failed v');
        $scope.message = reason;
    });

    $scope.submit = function () {
        $scope.message = null;
        matchplayResource.save($scope.match).then(function (response) {
            notificationsService.success("Success", "Ændringerne er gemt");
        },
        function (reason) {
            $scope.message = reason.data.Message;
            $log.debug(reason.data);
            notificationsService.error("Data blev ikke opdateret", error.Message);
        })
    };

    $scope.close = function () {
        $location.path("/results");
    };
};