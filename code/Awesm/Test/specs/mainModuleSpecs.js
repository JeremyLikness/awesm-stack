(function () {

    "use strict";

    describe("awesm module", function () {

        beforeEach(function () {
            module("awesm");
        });

        it("has a $http service.", inject(function ($http) {
            expect($http).not.toBeNull();
        }));
    });
})();