
angular.module("umbraco.resources").factory("matchplayResource",

    function ($http, $cookieStore) {
        return {
            getMatch: function (matchId) {
                return $http.get(this.getApiPath() + "GetMatch?matchId=" + matchId);
            },
            getMatches: function (id) {
                return $http.get(this.getApiPath() + "GetMatches?leagueId=" + id);
            },
            getTeams: function (id) {
                return $http.get(this.getApiPath() + "GetTeams?leagueId=" + id);
            },
            singleTeamExists: function (id) {
                return $http.get(this.getApiPath() + "GetPlayerTeam?vgcNo=" + id);
            },
            parTeamExists: function (id1, id2) {
                return $http.get(this.getApiPath() + "GetParTeam?vgcNo=" + id1 + "&VgcNoPartner=" + id2);
            },
            getTeam: function (id) {
                return $http.get(this.getApiPath() + "GetTeam?id=" + id);
            },
            save: function (object) {
                return $http.post(this.getApiPath() + "SaveMatchplayResult", object);
            },
            saveTeam: function (object) {
                return $http.post(this.getApiPath() + "UpdateTeam", object);
            },
            deleteTeam: function (id) {
                return $http.delete(this.getApiPath() + "DeleteTeam?id=" + id);
            },
            saveMatch: function (object) {
                return $http.post(this.getApiPath() + "UpdateMatch", object);
            },
            getApiPath: function () {
                return Umbraco.Sys.ServerVariables["caddie"]["MatchplayApiUrl"];
            }

        };
    });
