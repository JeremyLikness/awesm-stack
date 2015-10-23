(function () {
    "use strict";

    var alertService;

    describe("alertService", function () {

        beforeEach(function () {

            module("awesm", function ($provide) {
                $provide.constant("baseUrl", "http://unittest/");
            });
        });

        beforeEach(inject(function (alertSvc, $rootScope) {
            alertService = alertSvc;            
        }));

        afterEach(function () {
        });

        it("is registered with the module.", function () {
            expect(alertService).not.toBeNull();
        });

        it("should expose an empty array", function() {
            expect(alertService.alerts).not.toBeNull();
            expect(alertService.alerts.length).toBe(0);
        });

        describe("addAlert", function () {

            beforeEach(function() {
                alertService.alerts = [];
            });

            it("should add the alert", function () {
                alertService.addAlert("test", "info");
                expect(alertService.alerts.length).toBe(1);
                expect(alertService.alerts[0].message).toBe("test");
                expect(alertService.alerts[0].level).toBe("info");                
            });

            it("should auto-generate ids", function () {
                alertService.addAlert("test", "info");
                alertService.addAlert("test", "info");
                expect(alertService.alerts.length).toBe(2);
                expect(alertService.alerts[1].id-alertService.alerts[0].id).toBe(1);                
            });
        });

        describe("removeAlert", function () {
            it("should remove the alert with the matching id", function () {
                var id1, id2;
                alertService.addAlert("test", "info");
                alertService.addAlert("test", "info");
                alertService.addAlert("test", "info");
                id1 = alertService.alerts[0].id;
                id2 = alertService.alerts[2].id;
                alertService.removeAlert(alertService.alerts[1].id);
                expect(alertService.alerts.length).toBe(2);
                expect(alertService.alerts[0].id).toBe(id1);
                expect(alertService.alerts[1].id).toBe(id2);
            });
        });
    });
})();