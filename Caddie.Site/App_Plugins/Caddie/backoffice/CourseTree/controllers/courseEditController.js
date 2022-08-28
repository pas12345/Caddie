'use strict';
angular
    .module("umbraco")
    .controller("CourseEditController",
                    ["$scope"
                         , "$log"
                         , "$location"
                         , "$filter"
                         , "$routeParams"
                         , "courseResource"
                         , "notificationsService"
                        , CourseEditController]);

function CourseEditController($scope, $log, $location, $filter, $routeParams, courseResource, notificationsService) {

    $log.debug('CourseEditController loaded');
    $log.debug($routeParams);
    $scope.id = $routeParams.id;

    $scope.courseDetail = {};

    courseResource.getCourse($scope.id).then(function (response) {
        $log.debug('courseResource.getCourse: ' + $scope.id);
        $log.debug(response.data);
        $scope.courseDetail = response.data;
    }, function (reason) {
        $log.debug('courseResource.getCourse failed' + $scope.id);
        $scope.message = reason;
    });

    $scope.submit = function () {
        $scope.message = "submit";
        courseResource.updateCourseDetail($scope.courseDetail).then(function (response) {
            notificationsService.success("Success", "Ændringerne er gemt");
        },
        function (reason) {
            $scope.message = reason.data.Message;
            $log.debug(reason.data);
            notificationsService.error("Data blev ikke opdateret", error.Message);
        })
    };

};