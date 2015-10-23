var awesm;
(function (awesm) {

    "use strict";

    var app = awesm.getModule();

    function service($rootScope, $timeout) {
        this.$rootScope = $rootScope;
        this.$timeout = $timeout;
        this.$rootScope.isBusy = false;
        this.busyCount = 0;
    }

    angular.extend(service.prototype, {

        setBusy: function () {
            var that = this;
            this.busyCount += 1;
            this.$timeout(function () {
                that.$rootScope.isBusy = !!that.busyCount;
            }, 500);
        },

        resetBusy: function () {
            this.busyCount -= 1;
            this.$rootScope.isBusy = !!this.busyCount;
        }

    });

    app.service("busySvc", ["$rootScope", "$timeout", service]);

})(awesm || (awesm = {}));