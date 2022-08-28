
angular.module("umbraco.resources").factory("settingResource",

    function ($http, $cookieStore) {
        return {
            getSeason: function () {
                return $http.get(this.getApiPath() + "GetSeason");
            },
            getSetting: function (id) {
                return $http.get(this.getApiPath() + "GetSetting?id=" + id);
            },
            getApiPath: function () {
                return Umbraco.Sys.ServerVariables["caddie"]["SettingApiUrl"];
            }

        };
    });
