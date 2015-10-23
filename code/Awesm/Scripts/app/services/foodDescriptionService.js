var awesm;
(function (awesm) {

    "use strict";

    var app = awesm.getModule();

    function service(baseUrl, $http, $q) {
        this.url = baseUrl + "api/food";
        this.$q = $q;
        this.$http = $http;
    }

    angular.extend(service.prototype, {

        getFoodDescriptions: function (pattern) {
            var defer = this.$q.defer();
            this.$http({
                url: this.url, 
                method: "GET",
                params: {pattern: pattern}
            }).then(function (result) {
                defer.resolve(result.data);
            }, function (err) {
                defer.reject(err);
            });
            return defer.promise;
        },

        deleteFood: function (item) {
            var defer = this.$q.defer();
            item.name = ("DELETED:" + (new Date())).substring(0, 59);
            this.$http({
                url: this.url + "/" + item.Id,
                method: "POST",
                data: item
            }).then(function (result) {
                defer.resolve();
            }, function (err) {
                defer.reject(err);
            });
            return defer.promise;
        }

    });

    app.service("foodDescriptionSvc", ["baseUrl", "$http", "$q", service]);

})(awesm || (awesm = {}));