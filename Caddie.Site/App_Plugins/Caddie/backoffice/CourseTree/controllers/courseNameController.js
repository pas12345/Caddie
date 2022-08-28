'use strict';
angular
    .module("umbraco")
    .controller("CourseNameController",
                    ["$scope"
                         , "$log"
                        , CourseNameController]);

function CourseNameController($scope, $log) {

    $scope.model = {
        name : $scope.dialogData.name,
    }    	    	
};