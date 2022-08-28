'use strict';
angular
    .module("umbraco")
    .controller("MatchController",
                    ["$scope"
                         , "$log"
                         , "$location"
                         , "$filter"
                         , "$routeParams"
                         , "courseResource"
                         , "matchResource"
                         , "notificationsService"
                         , "navigationService"
                        , MatchController]);

function MatchController($scope, $log, $location, $filter, $routeParams, courseResource, matchResource, notificationsService, navigationService) {

    $log.debug('MatchController called');
    var dateFormat = "YYYY-MM-DD";

    $scope.match = {
        MatchText: "TorsdagsMatch",
        Sponsor: "Mens Section"
    };

    $scope.matchforms = {};
    $scope.courses = {};
    $scope.model = {};
    $scope.model.config = {
        pickDate: true,
        pickTime: false,
        useSeconds: false,
        format: dateFormat,
        icons: {
            time: "icon-time",
            date: "icon-calendar",
            up: "icon-chevron-up",
            down: "icon-chevron-down"
        }
    };

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
        $log.debug($scope.match.MatchDate);
        $log.debug('save');
        matchResource.save($scope.match).then(function (response) {
            navigationService.syncTree({ tree: 'matchTree', path: [-1, -1], forceReload: true });
            notificationsService.success("Success", "Ændringerne er gemt");
        },
        function (reason) {
            $scope.message = reason.data.ExceptionMessage;
            notificationsService.error("Data blev ikke opdateret");
        })
    };

    $scope.close = function () {
        $location.path("/matches");
    };

};