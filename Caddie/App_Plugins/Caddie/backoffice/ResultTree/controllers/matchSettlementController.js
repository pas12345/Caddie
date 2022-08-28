angular
    .module("umbraco")
    .controller("matchSettlementController",
                    ["$scope"
                         , "$log"
                         , "$filter"
                         , "$routeParams"
                         , "resultResource"
                         , "notificationsService"
                         , matchSettlementController]);

function matchSettlementController($scope, $log, $filter, $routeParams, resultResource, notificationsService) {
    $log.debug('matchSettlementController');
    $scope.matchId = $routeParams.id;

    $scope.settleMatch = function (id) {
        $log.debug('SettleMatch:' + id);
        resultResource.settleMatch(id).then(function (response) {
            notificationsService.success("Succes", "Matchen er opgjort og point er tildelt");
        }, function (reason) {
            $log.debug('SettleMatch failed: ' + id + ' - ' + reason);
            notificationsService.error("Det var ikke muligt at lukke matchen", error.Message);
        });
    }

};