(function () {
    "use strict";

    var foodDescriptionService,
        httpBackend;

    describe("foodDescriptionService", function () {

        beforeEach(function () {

            module("awesm", function ($provide) {
                $provide.constant("baseUrl", "http://unittest/");
            });
        });

        beforeEach(inject(function ($httpBackend, foodDescriptionSvc) {
            httpBackend = $httpBackend;
            foodDescriptionService = foodDescriptionSvc;
        }));

        afterEach(function () {
            httpBackend.verifyNoOutstandingExpectation();
            httpBackend.verifyNoOutstandingRequest();
        });

        it("is registered with the module.", function () {
            expect(foodDescriptionService).not.toBeNull();
        });

        it("should set the proper URL for the service", function () {
            expect(foodDescriptionService.url).toBe("http://unittest/api/food");
        });

        describe("getFoodDescriptions", function () {
            it("should return the list of food items upon successful call", function () {
                foodDescriptionService.getFoodDescriptions('')
                    .then(function (result) {
                        expect(result).not.toBeNull();
                    }, function () {
                        expect(false).toBe(true);
                    });
                httpBackend.expectGET(foodDescriptionService.url+"?pattern=").respond(200, []);
                httpBackend.flush();
            });

            it("should reject the promise upon unsuccessful connection", function () {
                foodDescriptionService.getFoodDescriptions("test")
                    .then(function () {
                        expect(false).toEqual(true);
                    }, function (err) {
                        expect(err.data).toBe("error");
                    });
                httpBackend.expectGET(foodDescriptionService.url+"?pattern=test").respond(500, "error");
                httpBackend.flush();
            });
        });
    });
})();