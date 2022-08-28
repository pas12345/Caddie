'use strict';
angular
    .module("umbraco")
    .controller("MatchplayController",
                    ["$scope"
                         , "$log"
                         , "$location"
                         , "$filter"
                         , "$routeParams"
                         , "matchplayResource"
                         , "notificationsService"
                        , MatchplayController]);

function MatchplayController($scope, $log, $location, $filter, $routeParams, matchplayResource, notificationsService) {

    $log.debug('MatchplayController loaded');
    $scope.league = $routeParams.id;

    $scope.match = {};
    $scope.teams = {};
    $scope.matches = {};
    $scope.matchText = 'Række: A';
    if ($scope.league == 2) {
        $scope.matchText = 'Række: B';
    }
    else if ($scope.league == 3) {
        $scope.matchText = 'Række: Par';
    }

    matchplayResource.getMatches($scope.league).then(function (response) {
        $scope.matches = response.data;
        $log.debug('League: ' + $scope.league);
    }, function (reason) {
        $log.debug('matchplayResource.get failed');
        $scope.message = reason;
    });
    
    $scope.MatchChange = function () {
        $log.debug('MatchChange xd');
        $log.debug($scope.match);
        matchplayResource.getMatch($scope.match.Id).then(function (response) {
            $scope.match = response.data;
            $scope.winners = [
                { Id: 0, StringValue: 'Ikke spillet' },
                { Id: 1, StringValue: $scope.match.TeamName1 },
                { Id: 2, StringValue: $scope.match.TeamName2 }
            ];
        }, function (reason) {
            $log.debug(' matchplayResource.getMatch failed g');
            $scope.message = reason;
        });
    }


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

    $scope.close = function () {
        $location.path("/results");
    };
};