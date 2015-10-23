var awesm;
(function (awesm) {

    "use strict";

    var app = awesm.getModule();

    function controller(alertSvc) {
        this.alertSvc = alertSvc;
    }

    angular.extend(controller.prototype, {
        dismiss: function (alertId) {
            this.alertSvc.removeAlert(alertId);
        }
    });

    Object.defineProperty(controller.prototype, "alerts", {
        configurable: false,
        enumerable: true,
        get: function () {
            return this.alertSvc.alerts;
        }
    });

    app.controller("alertCtrl", ["alertSvc", controller]);

})(awesm || (awesm = {}));