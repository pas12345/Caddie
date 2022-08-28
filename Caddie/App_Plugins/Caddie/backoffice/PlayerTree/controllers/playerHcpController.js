'use strict';
angular
    .module("umbraco")
    .controller("PlayerHcpController",
                    ["$scope"
                    , "$log"
                    , "playerResource"
                    , "notificationsService"
                    , PlayerHcpController]);

function PlayerHcpController($scope, $log, playerResource, notificationsService) {
    $scope.getUpdatedHcp = function () {
        playerResource.getUpdatedHcp().then(function (response) {
            $log.debug(response);
            $log.debug(response.data);
            $scope.message = "Opdateret hcp for antal spillere: " + response.data;
            notificationsService.success("Success", "Hcp opdateret");
        }, function (reason) {
            $log.debug('playerResource.getUpdatedHcp failed');
            $log.debug(reason);
            notificationsService.error("Der opstod en fejl i opdateringen: ", reason);
        });
    }
};