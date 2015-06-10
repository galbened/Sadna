(function () {
    'use strict';

    angular
        .module('app')
        .controller('AddSubForumModalCtrl', AddSubForumModalCtrl);

    AddSubForumModalCtrl.$inject = ['$scope', '$rootScope', 'Forums', '$modalInstance', '$http', '$q'];

    function AddSubForumModalCtrl($scope, $rootScope, Forums, $modalInstance, $http, $q) {
        activate();

        function activate() {
        }

        $scope.createSubForum = function (topic) {
            var queryArgs = {
                forumId: $rootScope.forumId,
            };
            console.log(topic);
            return Forums.createSubForum(queryArgs, { 'topic': topic }).$promise.then(
                function (result) {
                    $scope.forum = result.data;
                    $modalInstance.dismiss('cancel');
                    return result.$promise;
                }, function (result) {
                    return $q.reject(result);
                });
        }
    }
})();
