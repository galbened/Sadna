(function () {
    'use strict';

    angular
        .module('app')
        .controller('LoginModalCtrl', LoginModalCtrl);

    LoginModalCtrl.$inject = ['$scope'];

    function LoginModalCtrl($scope) {
        activate();

        function activate() {
        }

        $scope.login = function () {

        };
    }
})();
