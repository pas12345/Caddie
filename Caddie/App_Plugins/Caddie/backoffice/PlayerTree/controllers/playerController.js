angular
    .module("umbraco")
    .controller("PlayerController",
                    ["$scope"
                    , "$log"
                    , "$location"
                    , "$filter"
                    , "playerResource"
                    , "settingResource"
                    , "navigationService"
                    , "notificationsService"
                    , PlayerController]);

function PlayerController($scope, $log, $location, $filter, playerResource, settingResource, navigationService, notificationsService) {
    $log.debug('PlayerController');

    $scope.loaded = false;
    $scope.selectedVgcNo = null;
    $scope.player = {};

    settingResource.getSeason().then(function (response) {
        $scope.season = response.data;
    }, function (reason) {
        $log.debug('settingResource.getSeason()');
        $scope.message = reason;
    });
    $log.debug('season: ' + $scope.season);

    playerResource.getNonMemberList().then(function (response) {
        $log.debug('playerResource.getNonMemberList');
        $scope.players = response.data;
    }, function (reason) {
        $log.debug('playerResource.getNonMemberList failed');
        $scope.message = reason;
    });
    $scope.loaded = true;

    $scope.selectVgcNoChange = function (id) {
        $log.debug('selectVgcNoChange: ' + id);
        playerResource.getPlayer(id).then(function (response) {
            $scope.player = response.data;
            $scope.player.Season = $scope.season;
        }, function (reason) {
            $log.debug('playerResource.getPlayer failed');
            $scope.message = reason;
        }
    )
    };

    $scope.inputVgcNoChange = function () {
        $log.debug('inputVgcNoChange');
        $scope.player.VgcNo = null;
    };

    $scope.cancel = function () {
        $scope.objectForm.$dirty = false;
        navigationService.hideNavigation();
    };

    $scope.save = function () {
        $scope.player.Season = $scope.season;
        $log.debug($scope.player);
        playerResource.save($scope.player).then(function (response) {
            $scope.player = response.data;
            $scope.editing = true;
            $scope.objectForm.$dirty = false;
            navigationService.syncTree({ tree: 'playerTree', path: [-1, -1], forceReload: true });
            notificationsService.success("Success", "Oplysningerne er blevet opdateret");
        }, function (reason) {
            $log.debug('matchResource.get failed');
            $scope.message = reason;
            notificationsService.error("Der opstod en fejl i opdateringen: ", reason);
        });
    }
};