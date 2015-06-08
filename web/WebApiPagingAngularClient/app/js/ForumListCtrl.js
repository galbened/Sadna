(function () {
    'use strict';

    angular
        .module('app')
        .controller('ForumListCtrl', ForumListCtrl);

    ForumListCtrl.$inject = ['$scope', '$rootScope', 'Forums', '$http', '$q','$modal'];

    function ForumListCtrl($scope, $rootScope, Forums, $http, $q, $modal) {
        activate();

        function activate() {

            return Forums.getForums().$promise.then(
                function (result) {
                    $scope.forums = result.data;
                    return result.$promise;
                }, function (result) {
                    return $q.reject(result);
                });
        }

        $scope.addNewForum = function () {
            $modal.open({
                templateUrl: 'app/partials/AddForumModal.html',
                size: 'sm',
                controller: 'AddForumModalCtrl'
            });
        }

        $scope.removeForums = function () {
            $modal.open({
                templateUrl: 'app/partials/remove-forum-modal.html',
                size: 'sm',
                controller: 'RemoveForumCtrl'
            });
        }
    }
})();
