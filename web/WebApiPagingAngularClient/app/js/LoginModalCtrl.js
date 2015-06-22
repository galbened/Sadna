(function () {
    'use strict';

    angular
        .module('app')
        .controller('LoginModalCtrl', LoginModalCtrl);

    LoginModalCtrl.$inject = ['$scope', 'Forums', '$modalInstance','$rootScope'];

    function LoginModalCtrl($scope, Forums, $modalInstance, $rootScope) {
        activate();

        function activate() {
        }

        $scope.login = function (username,password) {
            var queryArgs = {
                forumId: $rootScope.forumId
            };

            var login = {
                username: username,
                password: password
            };

            return Forums.login(queryArgs, login).$promise.then(
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
