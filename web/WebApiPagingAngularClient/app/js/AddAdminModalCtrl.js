(function () {
    'use strict';

    angular
        .module('app')
        .controller('AddAdminModalCtrl', AddAdminModalCtrl);

    AddAdminModalCtrl.$inject = ['$scope', 'Forums', '$modalInstance', '$routeParams', '$q'];

    function AddAdminModalCtrl($scope, Forums, $modalInstance, $routeParams, $q) {
        activate();

        var parseAdmins = function (data) {
            var admins = [];
            for (var i = 0; i < data.usernames.length; i++) {
                var admin = {
                    'id': data.ids[i],
                    'username':data.usernames[i]
                }
                admins.push(admin);
            }
            return admins;
        };

        function activate() {
            return Forums.getNotAdmins({ 'forumId': $routeParams.forumId }).$promise.then(
                function (result) {
                    $scope.admins = parseAdmins(result.data);
                }, function (result) {
                    console.log(result);
                    alert(result.data.message);
                });
        }

        $scope.addAdmin = function (adminId) {
            var newAdminParams = {
                'forumId': $routeParams.forumId,
                'userId':$scope.params.userId,
                'adminId': adminId
            };

            return Forums.addAdmin(newAdminParams).$promise.then(
                function (result) {
                    alert("admin added successfully");
                    $modalInstance.close('success');
                }, function (result) {
                    console.log(result);
                    alert(result.data.message);
                });
        };
    }
})();
