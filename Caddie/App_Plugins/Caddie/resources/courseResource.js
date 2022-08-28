
angular.module("umbraco.resources").factory("courseResource",

    function ($http, $cookieStore) {
        return {
            getClub: function (id) {
                return $http.get(this.getApiPath() + "GetClub?clubId=" + id);
            },
            getCourses: function (id) {
                return $http.get(this.getApiPath() + "GetClubCourses?clubId=" + id);
            },
            getCourse: function (id) {
                return $http.get(this.getApiPath() + "GetCourse?courseId=" + id);
            },
            getCourseList: function (id) {
                return $http.get(this.getApiPath() + "GetCourseList?clubId=" + id);
            },
            getCourseItems: function () {
                return $http.get(this.getApiPath() + "GetCourseItems");
            },
            getTees: function () {
                return $http.get(this.getApiPath() + "GetTees");
            },
            updateClub: function (object) {
                return $http.post(this.getApiPath() + "UpdateClub", object);
            },
            updateCourse: function (object) {
                return $http.post(this.getApiPath() + "UpdateCourse", object);
            },
            updateCourseDetail: function (object) {
                return $http.post(this.getApiPath() + "UpdateCourseDetail", object);
            },
            getApiPath: function () {
                return Umbraco.Sys.ServerVariables["caddie"]["CourseApiUrl"];
            }
        };
    });
