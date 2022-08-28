'use strict';
angular
    .module("umbraco")
    .controller("ResultListController",
                    ["$scope"
                         , "$log"
                         , "$location"
                         , "$filter"
                         , "$routeParams"
                         , "resultResource"
                         , "matchResource"
                         , "notificationsService"
                        , ResultListController]);

function ResultListController($scope, $log, $location, $filter, $routeParams, resultResource, matchResource, notificationsService) {

    $log.debug('ResultListController' + $routeParams.id);

    $scope.matchId = $routeParams.id;
    $scope.results = {};

    matchResource.getMatch($scope.matchId).then(function (response) {
        $scope.match = response.data;
        $scope.matchText = $scope.match.CourseText + ', Par: ' + $scope.match.Par + ', ' + $scope.match.Matchform;
    }, function (reason) {
        $log.debug('matchResource.get failed');
        $scope.message = reason;
    });
    
    resultResource.getMatchResults($scope.matchId).then(function (response) {
        $scope.results = response.data;
    }, function (reason) {
        $log.debug('resultResource.getMatchResults failed');
        $scope.message = reason;
    });

};
