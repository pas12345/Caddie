
angular.module("umbraco.resources").factory("playerResource",

    function ($http, $cookieStore) {
        return {
            getMember: function (id) {
                return $http.get(this.getApiPath() + "GetMember?id=" + id);
            },
            getPlayer: function (id) {
                return $http.get(this.getApiPath() + "GetPlayer?vgcNo=" + id);
            },
            getMemberList: function () {
                return $http.get(this.getApiPath() + "GetMemberList");
            },
            getMemberNames: function (season) {
                return $http.get(this.getApiPath() + "GetMemberNames?season=" + season);
            },
            getNonMemberList: function () {
                return $http.get(this.getApiPath() + "GetNonMemberList");
            },
            getUpdatedHcp: function () {
                return $http.get(this.getApiPath() + "GetUpdatedHcp");
            },
            save: function (object) {
                return $http.post(this.getApiPath() + "Save", object);
            },
            getApiPath: function () {
                return Umbraco.Sys.ServerVariables["caddie"]["PlayerApiUrl"];
            }
        };
    });
