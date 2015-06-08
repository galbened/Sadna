(function () {
    'use strict';

    angular
        .module('app')
        .controller('RemoveForumCtrl', RemoveForumCtrl);

    RemoveForumCtrl.$inject = ['$scope'];

    function RemoveForumCtrl($scope) {
        activate();

        function activate() {
        }

        $scope.superAdminLogin = function (username, password) {

        };
    }
})();
