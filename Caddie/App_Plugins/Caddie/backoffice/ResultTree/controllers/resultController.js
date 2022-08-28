'use strict';
angular
    .module("umbraco")
    .controller("ResultController",
                    ["$scope"
                         , "$log"
                         , "$location"
                         , "$filter"
                         , "$routeParams"
                         , "resultResource"
                         , "matchResource"
                         , "notificationsService"
                        , ResultController]);

function ResultController($scope, $log, $location, $filter, $routeParams, resultResource, matchResource, notificationsService) {

    $log.debug('ResultController, matchId:  ' + $routeParams.id);
    $scope.matchId = $routeParams.id;
    $scope.hcpText = "-";
    $scope.result = {};
    $scope.selectedVgcNo = 0;

    matchResource.getMatch($scope.matchId).then(function (response) {
        $scope.match = response.data;
        $scope.matchText = $scope.match.CourseText + ', Par: ' + $scope.match.Par + ', ' + $scope.match.Matchform;
        $scope.showPut = $scope.match.IsStrokeplay;
        $scope.showHallington = $scope.match.IsHallington;
        $scope.showNetto = $scope.match.IsStrokeplay;
        $scope.showBrutto = $scope.match.IsStrokeplay;
    }, function (reason) {
        $log.debug('matchResource.get failed');
        $scope.message = reason;
    });
    
    resultResource.getRegistrations($scope.matchId).then(function (response) {
        $scope.results = response.data;
    }, function (reason) {
        $log.debug('resultResource.registrations failed');
        $scope.message = reason;
    });

    $scope.PlayerChange = function () {
        $scope.VgcNo = $scope.result.VgcNo;
        $scope.GetResult($scope.result.VgcNo);
    }

    $scope.GetResult = function (vgcNo) {
        $log.debug('GetResult: ' + vgcNo);
        $scope.message = null;
        resultResource.getPlayerResults($scope.match.Id, vgcNo)
            .then(function (response) {
                $scope.result = response.data;
                $log.debug($scope.result);
                $scope.originalresult = $scope.result;
                $scope.hcpText = "Hcp: " + $scope.result.Hcp + " (" + $scope.result.HcpIndex + ")";
            },
            function (reason) {
                $log.debug("Not found");
            })
    };

    $scope.submit = function () {
        $scope.message = null;
        $log.debug('submit: ' + $scope.matchId);
        $scope.result.matchId = $scope.matchId;
        if ($scope.result.Id) {
            $log.debug('submit update: ' + $scope.result.Id);
            resultResource.save($scope.result).then(function (response) {
                $scope.message = "... resultatet er gemt";
                notificationsService.success("Success", "Resultatet er blevet gemt");
                $log.debug(document.getElementById("selectPlayer"));
                document.getElementById("selectPlayer").focus();
            },
                function (reason) {
                    $scope.message = reason;
                    notificationsService.error("Data blev ikke opdateret", error.Message);
                })
        }
        else {
            $log.debug('submit save: ');
            resultResource.save($scope.result).then(function (response) {
                $scope.originalresult = angular.copy(response.data);
                    $scope.message = "... resultatet er opdateret";
                    notificationsService.success("Success", "Resultatet er blevet opdateret");
            },
                function (reason) {
                    $scope.message = reason;
                    notificationsService.error("Data blev ikke gemt", error.Message);
                })
        }
    };

    $scope.clear = function () {
        $scope.message = null;
        $log.debug('clear: ' + $scope.result.Id + '*' + $scope.matchId);
        if ($scope.result.Id) {
            resultResource.delete($scope.result.Id).then(function (response) {
                $scope.result = {};
                notificationsService.success("Success", "Resultatet er slettet");
                //$log.debug(document.getElementById("selectPlayer"));
                //document.getElementById("selectPlayer").focus();
                $window.location.href = "#/caddie/resultTree/resultlist/" + $scope.matchId;
             },
            function (reason) {
                $scope.message = reason;
                notificationsService.error("Data blev ikke opdateret", error.Message);
            })
        }
    };

    $scope.onBruttoChanged = function () {
        if ($scope.result.isHallington)
            $scope.result.netto = $scope.result.brutto == null ? null : $scope.result.brutto + $scope.result.hcp;
        if ($scope.result.isStrokePlay)
            $scope.result.netto = $scope.result.brutto == null ? null : $scope.result.brutto - $scope.result.hcp;
    }
    $scope.close = function () {
        $location.path("/results");
    };
};