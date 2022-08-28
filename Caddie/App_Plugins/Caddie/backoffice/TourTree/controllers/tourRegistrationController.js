'use strict';
angular
    .module("umbraco")
    .controller("TourRegistrationController",
                    ["$scope"
                        , "$log"
                        , "$location"
                        , "$filter"
                        , "$routeParams"
                        , "playerResource"
                        , "tourResource"
                        , "notificationsService"
                    , TourRegistrationController]);

function TourRegistrationController($scope, $log, $location, $filter, $routeParams, playerResource, tourResource, notificationsService) {

    $scope.tourId = $scope.currentNode.id;
    $scope.registration = {};
    $scope.btnText = "Tilmeld";
    $scope.IsOpenText = "Åben for tilmelding";

    playerResource.getMemberList().then(function (response) {
        $scope.players = response.data;
    }, function (reason) {
        $log.debug('playerResource.getMemberList failed');
        $scope.message = reason;
    });

    $scope.PlayerChange = function () {
        $scope.VgcNo = $scope.registration.VgcNo;
        $scope.GetRegistration($scope.registration.VgcNo);
    }

    $scope.GetRegistration = function (vgcNo) {
        $scope.message = null;
        tourResource.getRegistration($scope.tourId, vgcNo)
            .then(function (response) {
                $scope.registration = response.data;
                $scope.IsOpenText = $scope.registration.Open ? "Åben for tilmelding" : "Lukket for tilmelding";

                $log.debug($scope.registration);
                $scope.SetButtonText();
            }, function (reason) {
                $log.debug("registration failed " + reason);
                $scope.message = reason;
                notificationsService.error("Kunne ikke finde oplysninger til result registrering", error.Message);
            })
    };

    $scope.Register = function () {
        if (!$scope.registration.Open && !$scope.registration.IsRegistered)
        {
            notificationsService.error("Der er lukket for tilmelding", error.Message);
            return;
        }

        tourResource.register($scope.registration)
            .then(function (response) {
                $scope.registration.IsRegistered = !$scope.registration.IsRegistered;
                $scope.SetButtonText();
                if ($scope.registration.IsRegistered) {
                    notificationsService.success("success", "Registreringen er udført");
                } else {
                    notificationsService.error("Afmelding udført", "");
                }
            }, function (reason) {
                $log.debug("registration failed " + reason);
                $scope.message = reason;
                notificationsService.error("Tilmeldingen fejlede", error.Message);
            })
    };

    $scope.SetButtonText = function () {
        if ($scope.registration.IsRegistered) {
            $scope.btnText = "Afmeld";
        }
        else {
            $scope.btnText = "Tilmeld";
        }
    };

};