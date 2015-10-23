(function () {
    "use strict";

    var busyService,
        timeout,
        rs;

    describe("busyService", function () {

        beforeEach(function () {

            module("awesm", function ($provide) {
                $provide.constant("baseUrl", "http://unittest/");                
            });
        });

        beforeEach(inject(function (busySvc, $rootScope, $timeout) {
            busyService = busySvc;
            timeout = $timeout;
            rs = $rootScope;
        }));

        afterEach(function () {
        });

        it("is registered with the module.", function () {
            expect(busyService).not.toBeNull();
        });

        it("should set the initial count to null and the $rootScope busy to false", function () {
            expect(busyService.busyCount).toBe(0);
            expect(rs.isBusy).toBe(false);
        });

        describe("setBusy", function () {
            it("should increment the busy count and set busy to true", function () {
                busyService.setBusy();
                timeout.flush();
                expect(busyService.busyCount).toBe(1);
                expect(rs.isBusy).toBe(true);
            });
        });

        describe("resetBusy", function () {
            it("should decrement the busy count and set busy to false when the count reaches 0", function () {
                busyService.setBusy();
                busyService.setBusy();
                busyService.resetBusy();
                timeout.flush();
                expect(busyService.busyCount).toBe(1);
                expect(rs.isBusy).toBe(true);
                busyService.resetBusy();                
                expect(busyService.busyCount).toBe(0);
                expect(rs.isBusy).toBe(false);
            });
        });

        
    });
})();