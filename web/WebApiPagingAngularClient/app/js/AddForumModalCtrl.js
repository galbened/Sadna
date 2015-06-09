(function () {
    'use strict';

    angular
        .module('app')
        .controller('AddForumModalCtrl', AddForumModalCtrl);

    AddForumModalCtrl.$inject = ['$scope', 'Forums', '$modalInstance'];

    function AddForumModalCtrl($scope, Forums, $modalInstance) {
        activate();

        function activate() {
            $scope.notAdmin = true;
            $scope.incorrectPaswword = false;
        }

        $scope.superAdminLogin = function (username , password) {
            if (username === 'admin' && password === 'admin') {
                $scope.notAdmin = false;
                $scope.incorrectPaswword = false;
            } else {
                $scope.incorrectPaswword = true;
            }
        };

        $scope.createForum = function (name, numOfModerators, uppercase, lowercase, numbers, symbols, minLength) {
            var newForumParams = {
                'name' : name, 
                'numOfModerators': numOfModerators,
                'degreeOfEnsuring':'',
                'uppercase' :uppercase,
                'lowercase':lowercase,
                'numbers':numbers,
                'symbols':symbols,
                'minLength': minLength
            };

            return Forums.createForum({}, newForumParams).$promise.then(
                function (result) {
                    $modalInstance.dismiss('cancel');
                    return result.$promise;
                }, function (result) {
                    return $q.reject(result);
                });
        }
    }
})();
