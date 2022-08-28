'use strict';
angular
    .module("umbraco")
    .service('stateService', function () {
        var matchId = -1;
        this.setMatchId = function (id) {
            matchId = id;
        }
        this.getMatchId = function () {
            return matchId;
        }
    });