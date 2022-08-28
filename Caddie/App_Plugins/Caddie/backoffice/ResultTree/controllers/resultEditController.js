angular
    .module("umbraco")
    .controller("ResultEditController",
                    ["$scope"
                         , "$log"
                         , "$filter"
                         , "$routeParams"
                         , "$window"
                         , "resultResource"
                         , "notificationsService"
                         , ResultEditController]);

function ResultEditController($scope, $log, $filter, $routeParams, $window, resultResource, notificationsService) {

    $log.debug('ResultEditController' + $routeParams.id);
    $scope.id = $routeParams.id;
    $scope.hcpText = "-";
    $scope.result = {};
    $scope.selectedVgcNo = 0;

    resultResource.getMatchResult($scope.id).then(function (response) {
        $scope.result = response.data;
        $scope.matchId = $scope.result.MatchId;
    }, function (reason) {
        $log.debug('resultResource.getMatchResult failed');
        $scope.message = reason;
    });

    $scope.submit = function () {
        $scope.message = null;
        $log.debug('submit: ' + $scope.result.Id);
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
                $log.debug('redirect: ' + $scope.matchId);
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