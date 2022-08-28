'use strict';
angular
    .module("umbraco")
    .controller("MatchplayMatchListController",
                    ["$scope"
                         , "$log"
                         , "$location"
                         , "$filter"
                         , "$routeParams"
                         , "matchplayResource"
                         , "notificationsService"
                        , MatchplayMatchListController]);

function MatchplayMatchListController($scope, $log, $location, $filter, $routeParams, matchplayResource, notificationsService) {

    $scope.league = -1 * $routeParams.id;
    $log.debug($scope.league);

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
    }, function (reason) {
        $log.debug('matchplayResource.getMatches failed');
        $scope.message = reason;
    });
    
    //$scope.MatchChange = function () {
    //    matchplayResource.getMatch($scope.match.LeagueMatchId).then(function (response) {
    //        $scope.match = response.data;
    //        $scope.winners = [
    //            { Id: 0, StringValue: 'Ikke spillet' },
    //            { Id: 1, StringValue: $scope.match.TeamName1 },
    //            { Id: 2, StringValue: $scope.match.TeamName2 }
    //        ];
    //    }, function (reason) {
    //        $log.debug(' matchplayResource.getMatch failed');
    //        $scope.message = reason;
    //    });
    //}


    //$scope.submit = function () {
    //    $scope.message = null;
    //    matchplayResource.save($scope.match).then(function (response) {
    //        notificationsService.success("Success", "Ændringerne er gemt");
    //    },
    //    function (reason) {
    //        $scope.message = reason;
    //        notificationsService.error("Data blev ikke opdateret", error.Message);
    //    })
    //};

    //$scope.close = function () {
    //    $location.path("/results");
    //};
};