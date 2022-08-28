
angular.module("umbraco.resources").factory("matchResource",

    function ($http, $cookieStore) {
        return {
            getMatch: function (matchId) {
                return $http.get(this.getApiPath() + "GetMatch?matchId=" + matchId);
            },
            matchDates: function () {
                return $http.get(this.getApiPath() + "matchDates");
            },
            getMatchesBefore: function () {
                return $http.get(this.getApiPath() + "GetMatchesBefore");
            },
            getMatchesAfter: function () {
                return $http.get(this.getApiPath() + "GetMatchesAfter");
            },
            getMatchforms: function () {
                return $http.get(this.getApiPath() + "GetMatchforms");
            },
            lastMatch: function () {
                return $http.get(this.getApiPath() + "lastMatch");
            },
            save: function (object) {
                return $http.post(this.getApiPath() + "SaveMatch", object);
            },
            getApiPath: function() {
                return Umbraco.Sys.ServerVariables["caddie"]["MatchApiUrl"];
            }

        };
    });
