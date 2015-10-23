var awesm;
(function (awesm) {

    "use strict";

    var app = awesm.getModule();

    function service($timeout) {
        this.$timeout = $timeout;
    }

    angular.extend(service.prototype, {

        alerts: [],
        alertId: 1,
        addAlert: function (message, level, timeout) {
            var alert = {
                message: message,
                level: level,
                id: this.alertId
            },
                that = this;
            this.alertId += 1;
            this.alerts.push(alert);
            if (timeout) {
                (function (id) {
                    that.$timeout(function () {
                        that.removeAlert(id);
                    }, timeout);
                })(alert.id);
            }
        },
        addError: function (message, timeout) {
            this.addAlert(message, "danger", timeout);
        },
        addWarning: function (message, timeout) {
            this.addAlert(message, "warning", timeout);
        },
        addTemporarySuccess: function (message) {
            this.addAlert(message, "success", 3000);
        },
        addSuccess: function (message, timeout) {
            this.addAlert(message, "success", timeout);
        },
        removeAlert: function (alertId) {
            var i;
            for (i = this.alerts.length - 1; i >= 0; i -= 1) {
                if (this.alerts[i].id === alertId) {
                    this.alerts.splice(i, 1);
                    break;
                }
            }
        }
    });

    app.service("alertSvc", ["$timeout", service]);

})(awesm || (awesm = {}));