'use strict';
angular
    .module("umbraco")
    .controller("MatchplayEditTeamController",
                    ["$scope"
                         , "$log"
                         , "$routeParams"
                         , "matchplayResource"
                         , "playerResource"
                         , "notificationsService"
                        , MatchplayEditTeamController]);

function MatchplayEditTeamController($scope, $log, $routeParams, matchplayResource, playerResource, notificationsService) {

    $log.debug("MatchplayEditTeamController");
    $scope.teamId = $routeParams.id;
    $log.debug($scope.teamId);
    $scope.team = {
        LeagueId: 1
    };
    $scope.editing = ($scope.teamId > 0);
    $scope.matchText = '';

    $scope.leagues = [
            { Id: 1, StringValue: 'Række: A' },
            { Id: 2, StringValue: 'Række: B' },
            { Id: 3, StringValue: 'Række: Par' }
    ];

    $scope.LeagueChange = function () {
        $log.debug($scope.team.LeagueId);
        $scope.isSingle = ($scope.team.LeagueId < 3);
    }

    if ($scope.teamId > 0) {
        matchplayResource.getTeam($scope.teamId).then(function (response) {
            $scope.team = response.data;
            $scope.LeagueId = $scope.team.LeagueId;
            $log.debug($scope.team);
            $log.debug('League: ' + $scope.LeagueId);
            if ($scope.LeagueId == 1) {
                $scope.matchText = 'Række: A';
            }
            else if ($scope.LeagueId == 2) {
                $scope.matchText = 'Række: B';
            }
            else if ($scope.LeagueId == 3) {
                $scope.matchText = 'Række: Par';
                $scope.isSingle = false;
            }
        }, function (reason) {
            $log.debug(' matchplayResource.getTeam failed');
            $scope.message = reason.data.Message;
        });
    }
    $scope.LeagueChange();
    
    $scope.players = {};
    playerResource.getMemberList().then(function (response) {
        $scope.players = response.data;
    }, function (reason) {
        $log.debug('playerResource.getMemberList failed');
        $scope.message = reason.data, Message;
    });

    $scope.PlayerChange = function () {
        if ($scope.isSingle)
        {
            matchplayResource.singleTeamExists($scope.team.VgcNo).then(function (response) {
                if (response.status != 204)
                {
                    $scope.team = response.data;
                }
            }, function (reason) {
                $log.debug(' matchplayResource.singleTeamExists failed');
                $scope.message = 'Spilleren er ikke registreret til hulspil';
            });
        }
        else {
            matchplayResource.parTeamExists($scope.team.VgcNo, $scope.team.VgcNoPartner).then(function (response) {
                if (response.status != 204)
                {
                    $scope.team = response.data;
                }
            }, function (reason) {
                $scope.message = 'Holdet er ikke oprettet';
            });
        }
        $log.debug($scope.team);
    }

    $scope.PartnerChange = function () {
        matchplayResource.parTeamExists($scope.team.VgcNo, $scope.team.VgcNoPartner).then(function (response) {
            if (response.status != 204)
            {
                $scope.team = response.data;
            }
        }, function (reason) {
            $scope.message = 'Holdet er ikke oprettet';
        });
        $log.debug($scope.team);
    }


    $scope.submit = function () {
        $scope.message = null;
        matchplayResource.saveTeam($scope.team).then(function (response) {
            notificationsService.success("Success", "Ændringerne er gemt");
        },
        function (reason) {
            $scope.message = reason;
            notificationsService.error("Data blev ikke opdateret", error.Message);
        })
    };
};