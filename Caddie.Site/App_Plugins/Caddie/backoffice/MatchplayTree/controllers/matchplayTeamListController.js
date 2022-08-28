'use strict';
angular
    .module("umbraco")
    .controller("MatchplayTeamListController",
                    ["$scope"
                         , "$log"
                         , "$location"
                         , "$filter"
                         , "$routeParams"
                         , "matchplayResource"
                         , "notificationsService"
                        , MatchplayTeamListController]);

function MatchplayTeamListController($scope, $log, $location, $filter, $routeParams, matchplayResource, notificationsService) {

    $scope.league = -1 * $routeParams.id;
    $log.debug($scope.league);

    $scope.match = {};
    $scope.teams = {};
    $scope.matches = {};
    $scope.isSingle = true;
    $scope.matchText = 'Række: A';
    if ($scope.league == 2) {
        $scope.matchText = 'Række: B';
    }
    else if ($scope.league == 3) {
        $scope.matchText = 'Række: Par';
        $scope.isSingle = false;
    }

    matchplayResource.getTeams($scope.league).then(function (response) {
        $scope.teams = response.data;
    }, function (reason) {
        $log.debug('matchplayResource.getTeams failed');
        $scope.message = reason;
    });
    
    $scope.delete = function (id) {
        if (confirm("Vil du slette holdet ?")) {
            matchplayResource.deleteTeam(id).then(function (response) {
                matchplayResource.getTeams($scope.league).then(function (response) {
                    $scope.teams = response.data;
                }, function (reason) {
                    $log.debug('matchplayResource.getTeams failed');
                    $scope.message = reason;
                });
                notificationsService.success("Success", "Holdet er slettet");
            },
             function (reason) {
                 $scope.message = reason;
                 notificationsService.error("Data blev ikke opdateret", error.Message);
            })
        }
    };

    $scope.submit = function () {
        $scope.message = null;
        matchplayResource.save($scope.match).then(function (response) {
            notificationsService.success("Success", "Ændringerne er gemt");
        },
        function (reason) {
            $scope.message = reason;
            notificationsService.error("Data blev ikke opdateret", error.Message);
        })
    };
};