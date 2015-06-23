(function () {
    'use strict';

    angular
        .module('app')
        .controller('AddSubForumModalCtrl', AddSubForumModalCtrl);

    AddSubForumModalCtrl.$inject = ['$scope', '$rootScope', 'Forums', '$modalInstance', '$http', '$q'];

    function AddSubForumModalCtrl($scope, $rootScope, Forums, $modalInstance, $http, $q) {
        activate();

        function activate() {
            $scope.id = $scope.params.userId;
            console.log($scope.params);
        }

        $scope.createSubForum = function (topic) {
            var queryArgs = {
                userId: $scope.id,
                forumId: $rootScope.forumId,
            };
            return Forums.createSubForum(queryArgs, { 'topic': topic }).$promise.then(
                function (result) {
                    $scope.newSubForum = result.data;
                    $modalInstance.close($scope.newSubForum);
                    return result.$promise;
                }, function (result) {
                    alert(result.data.message);
                    return $q.reject(result);
                });
        }
    }
})();
