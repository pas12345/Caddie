'use strict';
angular
    .module("umbraco")
    .controller("MatchplayEditMatchController",
                    ["$scope"
                         , "$log"
                         , "$location"
                         , "$filter"
                         , "$routeParams"
                         , "matchplayResource"
                         , "dialogService"
                         , "notificationsService"
                        , MatchplayEditMatchController]);

function MatchplayEditMatchController($scope, $log, $location, $filter, $routeParams, matchplayResource, dialogService, notificationsService) {

    $scope.id = $routeParams.id;
    //$scope.id = $routeParams.id.split("?")[0];

    $log.debug('MatchplayEditMatchController called with league id: ' + $scope.id);

    $scope.match = {};

    $scope.match.LeagueId = $scope.id;
    $scope.teams1 = {};
    $scope.teams2 = {};

    $scope.leagues = [
        { Id: 1, StringValue: 'Række: A' },
        { Id: 2, StringValue: 'Række: B' },
        { Id: 3, StringValue: 'Række: Par' }
    ];

    $scope.getTeams = function (id) {
        $log.debug('getTeams');
        matchplayResource.getTeams(id).then(function (response) {
            $scope.teams1 = response.data;
            $scope.teams2 = response.data;
        }, function (reason) {
            $log.debug('matchplayResource.getTeams failed');
            $scope.message = reason.data.ExceptionMessage;
        })
        $scope.matchText = 'Række: A';
        if (id == 2) {
            $scope.matchText = 'Række: B';
        }
        else if (id == 3) {
            $scope.matchText = 'Række: Par';
        }
    };

    $scope.LeagueChange = function () {
        $log.debug('leagueChanged');
        $log.debug($scope.leagueId);
        $scope.getTeams($scope.leagueId);
    };

    if ($routeParams.id > 0)
    {
        $log.debug('getMatch');
        matchplayResource.getMatch($routeParams.id).then(function (response) {
            $scope.match = response.data;
            $scope.leagueId = $scope.match.LeagueId;
            $scope.getTeams($scope.leagueId);
        }, function (reason) {
            $log.debug('matchplayResource.getMatch failed k');
            $scope.message = reason.data.ExceptionMessage;
        });
    }

    $scope.submit = function () {
        $scope.message = null;
        $scope.match.LeagueId = $scope.leagueId;
        matchplayResource.saveMatch($scope.match).then(function (response) {
            notificationsService.success("Success", "Ændringerne er gemt");
        },
        function (reason) {
            $scope.message = reason;
            notificationsService.error("Data blev ikke opdateret", error.Message);
        })
    };
};