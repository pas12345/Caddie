'use strict';
angular
    .module("umbraco")
    .controller("CourseListController",
                    ["$scope"
                         , "$log"
                         , "$location"
                         , "$filter"
                         , "$routeParams"
                         , "courseResource"
                         , "notificationsService"
                         , "navigationService"
                        , CourseListController]);

function CourseListController($scope, $log, $location, $filter, $routeParams, courseResource, notificationsService, navigationService) {

    $log.debug('CourseListController loaded');
    $scope.club = {
        id: 0,
        name: null,
        courses: []
    };
    $scope.club.id = $routeParams.id;

    courseResource.getClub($scope.club.id).then(function (response) {
        $log.debug('courseResource.getClub');
        $scope.club.name = response.data.StringValue;
    }, function (reason) {
        $log.debug('courseResource.get getClub');
        $scope.message = reason;
    });

    courseResource.getCourses($scope.club.id).then(function (response) {
        $log.debug('courseResource.getCourses');
        $scope.club.courses = response.data;
    }, function (reason) {
        $log.debug('courseResource.getCourses failed');
        $scope.message = reason;
    });


    $scope.editCourse = function (courseId) {
        $log.debug('editCourse: ' + courseId);
        dialogService.closeAll();
        dialogService.open({
            template: '/app_plugins/Caddie/backoffice/CourseTree/dialogs/editCourse.html',
            dialogData: {
                courseId: courseId
            },
            closeOnSave: true,
            //tabFilter: ["Generic properties"],
            callback: function (data) {
                $scope.reloadView();
            }
        });
    };
    $scope.reloadView = function () {
        navigationService.syncTree({ tree: 'courseTree', path: [-1, -1], forceReload: true });
    }

};