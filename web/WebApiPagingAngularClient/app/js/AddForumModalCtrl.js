(function () {
    'use strict';

    angular
        .module('app')
        .controller('AddForumModalCtrl', AddForumModalCtrl);

    AddForumModalCtrl.$inject = ['$scope'];

    function AddForumModalCtrl($scope) {
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
    }
})();
