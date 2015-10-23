var awesm;
(function (awesm) {
    "use strict";

    var app = awesm.getModule();

    app.config([
        "$routeProvider",
        "baseUrl",
        function ($routeProvider, baseUrl) {
            $routeProvider.when("/food", {
                templateUrl: baseUrl + "Home/FoodList",
                controller: "foodListCtrl",
                controllerAs: "foodListCtrl",
                reloadOnSearch: false
            });

            $routeProvider.otherwise("/food");
        }
    ]);

    function controller($location, $routeParams, alertSvc, titleSvc, foodDescriptionSvc) {
        var that = this;
        this.$location = $location;
        titleSvc.setTitle("Food Descriptions");
        this.foodDescriptionSvc = foodDescriptionSvc;
        
        if ($routeParams["searchText"]) {
            this.searchText = $routeParams["searchText"];
        }

        this.refresh();
    }

    angular.extend(controller.prototype, {
        searchText: 'awesome',
        loading: true,
        foodList: [],
        view: function (id) {
            this.$location.search({});
            this.$location.path("/food/view/" + id);
        },
        edit: function (id) {
            this.$location.search({});
            this.$location.path("/food/edit/" + id);
        },
        deleteFood: function (item) {
            var that = this;
            this.foodDescriptionSvc.deleteFood(item)
            .then(function () {
                that.refresh();
            }, function (err) {
                throw new Error(err);
            });
        },
        reset: function () {
            this.searchText = '';
            this.sortAscending = false;
            this.refresh();
        },
        refresh: function () {
            var that = this;
            this.loading = true;            
            this.$location.search("searchText", this.searchText);
            this.foodDescriptionSvc.getFoodDescriptions(this.searchText).then(function (data) {
                that.foodList = data;                
                that.loading = false;
            }, function (err) {
                throw new Error("Error loading food descriptions: " + err.statusText);
            });
        }
    });

    app.controller("foodListCtrl", ["$location", "$routeParams", "alertSvc", "titleSvc", "foodDescriptionSvc", controller]);

})(awesm || (awesm = {}));