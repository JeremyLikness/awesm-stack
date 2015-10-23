var awesm;
(function (awesm) {
    "use strict";

    var app = angular.module("awesm", ["ngRoute"]);

    app.constant("baseUrl", awesm.baseUrl);
   
    app.config([
        "$provide", "$httpProvider", function (provide, httpProvider) {

            provide.decorator("$exceptionHandler", ["$delegate", "$injector", function ($delegate, $injector) {
                return function (exception, cause) {
                    var alertSvc = $injector.get("alertSvc");
                    alertSvc.addError(exception.toString());
                    $delegate(exception, cause);
                };
            }]);

            provide.factory("interceptor", ["$q", "$injector", "$location", function (q, injector, $location) {
                var getService = function () {
                    return injector.get("busySvc");
                },
                    getAlertService = function () {
                        return injector.get("alertSvc");
                    };
                return {
                    request: function (config) {
                        config.headers["Angular-Request"] = "awesm";
                        getService().setBusy();
                        return config || q.when(config);
                    },
                    response: function (response) {
                        getService().resetBusy();
                        return response || q.when(response);
                    },
                    responseError: function (rejection) {
                        getService().resetBusy();
                        if (rejection.status && rejection.status == 401) {
                            getAlertService().addError("You are not authorized to access " + $location.path());
                            $location.path("/");
                            return q.when({ data: null });
                        } else {
                            return q.reject(rejection);
                        }
                    }
                };
            }
            ]);

            httpProvider.interceptors.push("interceptor");
        }
    ]);

    awesm.getModule = function () {
        return angular.module("awesm");
    };

})(awesm || (awesm = {}));