'use strict';
angular
    .module("umbraco")
    .controller("CourseController",
                    ["$scope"
                         , "$log"
                         , "$location"
                         , "$filter"
                         , "$routeParams"
                         , "courseResource"
                         , "dialogService"
                         , "notificationsService"
                        , CourseController]);

function CourseController($scope, $log, $location, $filter, $routeParams, courseResource, dialogService, notificationsService) {

    $log.debug('CourseController loaded');
    $log.debug($routeParams);
    $scope.id = $routeParams.id;
    $scope.courseName = "";

    $scope.course = {
        Id: -1,
        StringValue: ""
    };

    $scope.courseDetail = {
        ClubId: $scope.id,
        Par: 72,
        CourseRating: 72.1,
        Slope: 125
    };

    $scope.getCourses = function () {
        courseResource.getCourseList($scope.id).then(function (response) {
            $log.debug('courseResource.getCourseList: ' + $scope.id);
            $scope.courses = response.data;
        }, function (reason) {
            $log.debug('courseResource.getCourse failed' + $scope.id);
            $scope.message = reason;
        })
    };
    $scope.getCourses();

    courseResource.getTees().then(function (response) {
        $log.debug('courseResource.getTees: ');
        $scope.tees = response.data;
    }, function (reason) {
        $log.debug('courseResource.getTees failed');
        $scope.message = reason;
    });

    $scope.submit = function () {
        $scope.message = "submit";
        $log.debug($scope.courseDetail);
        courseResource.updateCourseDetail($scope.courseDetail).then(function (response) {
            notificationsService.success("Success", "Ændringerne er gemt");
        },
        function (reason) {
            $scope.message = reason.data.Message;
            $log.debug(reason.data);
            notificationsService.error("Data blev ikke opdateret", error.Message);
        })
    };

    $scope.createCourseName = function () {
        $scope.message = "submit";
        $log.debug($scope.courseName);
        dialogService.closeAll();
        dialogService.open({
            template: '/app_plugins/Caddie/backoffice/CourseTree/dialogs/createCourseName.html',
            dialogData: {
                name: $scope.courseName
            },
            closeOnSave: true,
            //tabFilter: ["Generic properties"],
            callback: function (data) {
                $scope.course.Id = $scope.id;
                $scope.course.StringValue = data.name;
                courseResource.updateCourse($scope.course).then(function (response) {
                    $scope.getCourses();
                    $scope.courseDetail.CourseId = $scope.course.Id;
                },
                function (reason) {
                    $scope.message = reason.data.Message;
                    $log.debug(reason.data);
                    notificationsService.error("Data blev ikke opdateret", error.Message);
                })
                //$scope.reloadView();
            }
        });
    };

    

};