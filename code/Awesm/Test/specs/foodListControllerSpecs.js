(function () {
    "use strict";

    var controllerSvc,
        foodListController,
        foodDescriptionSvcMock,
        foodList,
        flushPromises;

    describe("foodListCtrl", function () {

        beforeEach(function () {

            foodList = [{Number:'001',Name:'Test Food',Description:'Awesome Test Food'},
                { Number: '999', Name: 'More Test Food', Description: 'More Test Food' }];

            foodDescriptionSvcMock = {
                $q: null,               
                queryCount: 0,
                lastPattern: '',
                getFoodDescriptions: function (filter) {
                    this.lastPattern = filter;
                    this.queryCount += 1;
                    return this.$q.when(foodList);
                }
            };

            module("awesm", function ($provide) {
                $provide.value("baseUrl", "http://unittest/");
                $provide.value("titleSvc", {
                    setTitle: angular.noop
                });
                $provide.value("foodDescriptionSvc", foodDescriptionSvcMock);                
            });
        });

        beforeEach(inject(function ($controller, $q, $rootScope) {
            controllerSvc = $controller;
            foodDescriptionSvcMock.$q = $q;
            flushPromises = function () {
                $rootScope.$apply();
            }
        }));

        afterEach(function () {
        });

        it("is registered with the module.", function () {
            foodListController = controllerSvc("foodListCtrl");
            expect(foodListController).not.toBeNull();
        });

        it("loads the food items when constructed.", function () {
            foodListController = controllerSvc("foodListCtrl");
            flushPromises();
            expect(foodListController.foodList).toBe(foodList);
        });

        it("sets the loading flag.", function () {
            foodListController = controllerSvc("foodListCtrl");
            expect(foodListController.loading).toBe(true);
            flushPromises();
            expect(foodListController.loading).toBe(false);
        });

        it("sets the default form values.", function () {
            foodListController = controllerSvc("foodListCtrl");
            flushPromises();
            expect(foodListController.searchText).toBe('awesome');
        });

        describe("reset", function () {

            it("resets the default form values.", function () {
                foodListController = controllerSvc("foodListCtrl");
                flushPromises();
                foodListController.searchText = 'a';
                foodListController.reset();
                flushPromises
                expect(foodListController.searchText).toBe('');
            });

            it("calls load after the form values are reset.", function () {
                var count;
                foodListController = controllerSvc("foodListCtrl");
                flushPromises();
                count = foodDescriptionSvcMock.queryCount;
                foodListController.searchText = 'a';
                foodListController.reset();
                flushPromises();
                expect(foodDescriptionSvcMock.queryCount).toBeGreaterThan(count);
            });

        });

        describe("refresh", function () {

            it("sets the loading flag, then resets it once loaded", function () {
                foodListController = controllerSvc("foodListCtrl");
                flushPromises();
                expect(foodListController.loading).toBe(false);
                foodListController.refresh();
                expect(foodListController.loading).toBe(true);
                flushPromises();
                expect(foodListController.loading).toBe(false);
            });
        });

        describe("edit", function () {

            it("navigates to the view", function () {
                foodListController = controllerSvc("foodListCtrl");
                flushPromises();
                foodListController.edit(1);
                expect(foodListController.$location.path()).toBe("/food/edit/1");
            });
        });
    });
})();