
angular.module("umbraco.resources").factory("reportResource",

    function ($http, $cookieStore) {
        return {
            printMatchRegistration: function (matchId) {
                var data = { 'matchId': matchId };
                return $http({
                    method: 'get',
                    url: this.getApiPath() + "MatchRegistrationReport",
                    params: data,
                    headers: {
                        'Content-type': 'application/pdf',
                    },
                    responseType: 'arraybuffer'
                });
            },
            printMatchResult: function (matchId) {
                var data = { 'matchId': matchId };
                return $http({
                    method: 'get',
                    url: this.getApiPath() + "MatchResultReport",
                    params: data,
                    headers: {
                        'Content-type': 'application/pdf',
                    },
                    responseType: 'arraybuffer'
                });
            },
            printTourRegistration: function (tourId) {
                var data = { 'tourId': tourId };
                return $http({
                    method: 'get',
                    url: this.getApiPath() + "TourRegistrationReport",
                    params: data,
                    headers: {
                        'Content-type': 'application/pdf',
                    },
                    responseType: 'arraybuffer'
                });
            },
            getApiPath: function () {
                return Umbraco.Sys.ServerVariables["caddie"]["ReportApiUrl"];
            }

        };
    });
