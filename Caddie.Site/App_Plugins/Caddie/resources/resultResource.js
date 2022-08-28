
angular.module("umbraco.resources").factory("resultResource",

    function ($http, $cookieStore) {
        return {
            getRegistrations: function (matchId) {
                return $http.get(this.getApiPath() + "GetMatchResultListForRegistration?matchId=" + matchId);
            },
            getPlayerResults: function (matchId, vgcNo) {
                return $http.get(this.getApiPath() + "GetMatchResultForRegistration?matchId=" + matchId + "&vgcNo=" + vgcNo);
            },
            getMatchResult: function (id) {
                return $http.get(this.getApiPath() + "GetMatchResult?matchResultId=" + id);
            },
            getMatchResults: function (matchId) {
                return $http.get(this.getApiPath() + "GetMatchResults?matchId=" + matchId);
            },
            save: function (object) {
                return $http.post(this.getApiPath() + "SaveResult", object);
            },
            delete: function (id) {
                return $http.post(this.getApiPath() + "DeleteMatchResult", id);
            },
            settleMatch: function (object) {
                return $http.post(this.getApiPath() + "SettleMatch", object);
            },

            getCompetitions: function () {
                return $http.get(this.getApiPath() + "GetCompetitions");
            },
            getCompetitionResult: function (matchId, competitionId) {
                return $http.get(this.getApiPath() + "GetCompetitionResult?matchId=" + matchId + "&competitionId=" + competitionId);
            },
            getCompetitionResultById: function (id) {
                return $http.get(this.getApiPath() + "GetCompetitionResultById?id=" + id);
            },
            saveCompetitionResult: function (object) {
                return $http.post(this.getApiPath() + "SaveCompetitionResult", object);
            },

            getPinResultById: function (id) {
                return $http.get(this.getApiPath() + "GetNearestPinResult?id=" + id);
            },
            savePinResult: function (object) {
                return $http.post(this.getApiPath() + "SavePinResult", object);
            },
            getApiPath: function () {
                return Umbraco.Sys.ServerVariables["caddie"]["ResultApiUrl"];
            }

        };
    });
