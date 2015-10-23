var awesm;
(function (awesm) {

    "use strict";

    var app = awesm.getModule();

    function service($window) {
        this.$window = $window;
        this.currentTitle = $window.document.title;
    }

    angular.extend(service.prototype, {
        setTitle: function (title) {
            var titleToSet = title + " | AWESM";
            this.$window.document.title = titleToSet;
            this.currentTitle = titleToSet;
        }
    });

    app.service("titleSvc", ["$window", service]);

})(awesm || (awesm = {}));