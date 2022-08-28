'use strict';
angular
    .module("umbraco")
    .controller("SettingsController",
                    ["$scope"
                         , "$log"
                         , "$location"
                         , "$filter"
                         , "$routeParams"
                         , "dialogService"
                         , "courseResource"
                        , SettingsController]);

function SettingsController($scope, $log, $location, $filter, $routeParams, dialogService, courseResource) {

    $log.debug('SettingsController loaded');

};