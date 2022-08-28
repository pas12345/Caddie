
angular.module("umbraco.resources").factory("tourResource",

    function ($http, $cookieStore) {
        return {
            getTour: function (tourId) {
                return $http.get(this.getApiPath() + "GetTour?tourId=" + tourId);
            },
            getTours: function () {
                return $http.get(this.getApiPath() + "GetAllTourItems");
            },
            getRegistration: function (tourId, vgcNo) {
                return $http.get(this.getApiPath() + "GetRegistration?tourId=" + tourId + "&vgcno=" + vgcNo);
            },
            register: function (object) {
                return $http.post(this.getApiPath() + "TourRegistration", object);
            },
            save: function (object) {
                return $http.post(this.getApiPath() + "SaveTour", object);
            },
            getApiPath: function () {
                return Umbraco.Sys.ServerVariables["caddie"]["TourApiUrl"];
            }

        };
    });
