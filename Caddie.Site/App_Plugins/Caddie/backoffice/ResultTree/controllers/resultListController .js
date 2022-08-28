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

    //$scope.PlayerChange = function () {
    //    $log.debug($scope.result.VgcNo);
    //    $scope.VgcNo = $scope.result.VgcNo;
    //    $scope.GetResult($scope.result.VgcNo);
    //}

    //$scope.GetResult = function (vgcNo) {
    //    $log.debug('GetResult: ' + vgcNo);
    //    $scope.message = null;
    //    resultResource.getPlayerResults($scope.matchId, vgcNo)
    //        .then(function (response) {
    //            $scope.result = response.data;
    //            $log.debug($scope.result);
    //            $scope.originalresult = $scope.result;
    //            $scope.hcpText = "Hcp: " + $scope.result.Hcp + " (" + $scope.result.HcpIndex + ")";
    //        }, function (reason) {
    //            $log.debug("registration failed " + reason);
    //            $scope.message = reason;
    //            notificationsService.error("Kunne ikke finde oplysninger til result registrering", error.Message);
    //        })
    //};

    //$scope.submit = function () {
    //    $scope.message = null;
    //    $log.debug('submit: ' + $scope.result.Id);
    //    if ($scope.result.Id) {
    //        $log.debug('submit update: ' + $scope.result.Id);
    //        resultResource.save($scope.result).then(function (response) {
    //            $scope.message = "... resultatet er gemt";
    //            notificationsService.success("Success", "Resultatet er blevet gemt");
    //            $log.debug(document.getElementById("selectPlayer"));
    //            document.getElementById("selectPlayer").focus();
    //        },
    //            function (reason) {
    //                $scope.message = reason;
    //                notificationsService.error("Data blev ikke opdateret", error.Message);
    //            })
    //    }
    //    else {
    //        $log.debug('submit save: ');
    //        resultResource.save($scope.result).then(function (response) {
    //            $scope.originalresult = angular.copy(response.data);
    //                $scope.message = "... resultatet er opdateret";
    //                notificationsService.success("Success", "Resultatet er blevet opdateret");
    //        },
    //            function (reason) {
    //                $scope.message = reason;
    //                notificationsService.error("Data blev ikke gemt", error.Message);
    //            })
    //    }
    //};

    //$scope.clear = function () {
    //    $scope.message = null;
    //    $log.debug('clear: ' + $scope.result.Id);
    //    if ($scope.result.Id) {
    //        resultResource.delete($scope.result.Id).then(function (response) {
    //            $scope.result = {};
    //            notificationsService.success("Success", "Resultatet er slettet");
    //            $log.debug(document.getElementById("selectPlayer"));
    //            document.getElementById("selectPlayer").focus();
    //        },
    //        function (reason) {
    //            $scope.message = reason;
    //            notificationsService.error("Data blev ikke opdateret", error.Message);
    //        })
    //    }
    //};

    //$scope.onBruttoChanged = function () {
    //    if ($scope.result.isHallington)
    //        $scope.result.netto = $scope.result.brutto == null ? null : $scope.result.brutto + $scope.result.hcp;
    //    if ($scope.result.isStrokePlay)
    //        $scope.result.netto = $scope.result.brutto == null ? null : $scope.result.brutto - $scope.result.hcp;
    //}
    //$scope.close = function () {
    //    $location.path("/results");
    //};
