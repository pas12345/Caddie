'use strict';
angular
    .module("umbraco")
    .controller("EditCompetitionResultController",
                    ["$scope"
                         , "$log"
                         , "$location"
                         , "$filter"
                         , "$routeParams"
                         , "resultResource"
                         , "playerResource"
                         , "settingResource"
                        , "navigationService"
                         , "notificationsService"
                        , EditCompetitionResultController]);

function EditCompetitionResultController($scope, $log, $location, $filter, $routeParams, resultResource,
    playerResource, settingResource, navigationService, notificationsService) {

    $log.debug('EditCompetitionResultController: ' + $scope.id);
    $scope.id = $routeParams.id;

    settingResource.getSeason().then(function (response) {
        $scope.season = response.data;
        $log.debug('season: ' + $scope.season);
        playerResource.getMemberNames($scope.season).then(function (response) {
            $scope.players = response.data;
        }, function (reason) {
            $log.debug('playerResource.getMemberNames failed');
            $scope.message = reason;
        });
    }, function (reason) {
        $log.debug('settingResource.getSeason() ');
        $scope.message = reason;
    });

    resultResource.getCompetitions().then(function (response) {
        $scope.competitions = response.data;
    }, function (reason) {
        $log.debug('resultResource.getCompetitions failed');
        $scope.message = reason;
    });

    resultResource.getCompetitionResultById($scope.id).then(function (response) {
        $log.debug("getCompetitionResultById(" + $scope.id + ")");
        $log.debug(response.data);
        $scope.result = response.data;
    },
    function (reason) {
        $scope.message = reason;
        notificationsService.error("Data blev ikke opdateret", error.Message);
    })

    $scope.GetCompetitionResult = function () {
        resultResource.getCompetitionResult($scope.result.MatchId, $scope.result.CompetitionId).then(function (response) {
            $log.debug(response.data);
            $scope.result = response.data;
        },
        function (reason) {
            $scope.result.MembershipId = response.data;
        })
    }

    $scope.competitionChange = function () {
        $scope.GetCompetitionResult();
    }

    $scope.submit = function () {
        $scope.message = null;
        $log.debug('submit: ');
        resultResource.saveCompetitionResult($scope.result).then(function (response) {
            $scope.message = "... resultatet er gemt";
            navigationService.syncTree({ tree: 'resultTree', path: [-1, -1], forceReload: true });
            notificationsService.success("Success", "Resultatet er blevet gemt");
        },
        function (reason) {
            $scope.message = reason;
            notificationsService.error("Data blev ikke opdateret", error.Message);
        })
    }

    $scope.close = function () {
        $location.path("/results");
    };
};