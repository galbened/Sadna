(function () {
    'use strict';

    angular
        .module('app')
        .controller('SignupModalCtrl', SignupModalCtrl);

    SignupModalCtrl.$inject = ['$scope', 'Forums', '$rootScope','$modalInstance'];

    function SignupModalCtrl($scope, Forums, $rootScope, $modalInstance) {
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

            return Forums.signup(queryArgs, signup).$promise.then(
                function (result) {
                    $scope.user = result.data;
                    $modalInstance.close($scope.user);
                }, function (result) {
                    alert(result.data.message);
                    return $q.reject(result);
                });

        };
    }
})();