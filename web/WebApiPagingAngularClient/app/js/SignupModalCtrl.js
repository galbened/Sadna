(function () {
    'use strict';

    angular
        .module('app')
        .controller('SignupModalCtrl', SignupModalCtrl);

    SignupModalCtrl.$inject = ['$scope', 'Users', '$rootScope'];

    function SignupModalCtrl($scope, Users, $rootScope) {
        activate();

        function activate() {
        }

        $scope.signup = function (username, email, password) {
            var queryArgs = {
                forumId: $rootScope.forumId
            };

            console.log($scope.forum);

            var signup = {
                username: username,
                email: email,
                password: password
            };

            return Users.signup(queryArgs,signup).$promise.then(
                function (result) {
                    $scope.kash = result.data;
                    return result.$promise;
                }, function (result) {
                    $scope.kash = { username: "kashkasha" };
                    return $q.reject(result);
                });

        };
    }
})();