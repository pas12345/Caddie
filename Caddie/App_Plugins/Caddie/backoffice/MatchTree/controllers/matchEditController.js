'use strict';
angular
    .module("umbraco")
    .controller("MatchEditController",
                    ["$scope"
                         , "$log"
                         , "$location"
                         , "$filter"
                         , "$routeParams"
                         , "courseResource"
                         , "matchResource"
                         , "notificationsService"
                         , "navigationService"
                        , MatchEditController]);

function MatchEditController($scope, $log, $location, $filter, $routeParams, courseResource, matchResource, notificationsService, navigationService) {

    $log.debug('MatchEditController called');
    $scope.matchId = $routeParams.id;

    $scope.match = {};
    $scope.matchforms = {};
    $scope.courses = {};
    $scope.tees = {};

    matchResource.getMatch($scope.matchId).then(function (response) {
        $scope.match = response.data;
        $log.debug($scope.match);
        $scope.matchText = $scope.match.CourseText + ', Par: ' + $scope.match.Par + ', ' + $scope.match.Matchform;
    }, function (reason) {
        $log.debug('matchResource.get failed');
        $scope.message = reason;
    });
    
    matchResource.getMatchforms().then(function (response) {
        $scope.matchforms = response.data;
    }, function (reason) {
        $log.debug('getMatchforms.get failed');
        $scope.message = reason;
    });

    courseResource.getCourseItems().then(function (response) {
        $scope.courses = response.data;
    }, function (reason) {
        $log.debug('courseResource.getCourseItems failed');
        $scope.message = reason;
    });

    $scope.courseChange = function (id) {
        $scope.message = id;
        courseResource.getCourse(id).then(function (response) {
            $scope.match.Par = response.data.Par;
        },
        function (reason) {
            $scope.message = reason;
            $log.debug('courseResource.getCourse failed');
        })
    }

    $scope.save = function () {
        $scope.message = null;
        $log.debug('saved');
        matchResource.save($scope.match).then(function (response) {
            navigationService.syncTree({ tree: 'matchTree', path: [-1, -1], forceReload: true });
            notificationsService.success("Success", "Ændringerne er gemt");
        },
        function (reason) {
            $scope.message = reason;
            notificationsService.error("Data blev ikke opdateret", error.Message);
        })
    };

    $scope.close = function () {
        $location.path("/matches");
    };
};