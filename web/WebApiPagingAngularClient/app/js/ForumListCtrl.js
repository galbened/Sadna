(function () {
    'use strict';

    angular
        .module('app')
        .controller('ForumListCtrl', ForumListCtrl);

    ForumListCtrl.$inject = ['$scope', '$rootScope', 'Forums', '$http', '$q','$modal'];

    function ForumListCtrl($scope, $rootScope, Forums, $http, $q, $modal) {
        activate();

        var parseForums = function (data) {
            var forums = [];
            console.log(data);
            for (var i = 0; i < data.ids.length; i++) {
                var forum = {
                    'title': data.names[i],
                    'id': data.ids[i]
                }
                forums.push(forum);
            }
            console.log(forums);
            return forums;
        }

        function activate() {

            return Forums.getForums().$promise.then(
                function (result) {
                    $scope.forums = parseForums(result.data);
                    return result.$promise;
                }, function (result) {
                    return $q.reject(result);
                });
        }

        $scope.addNewForum = function () {
            $scope.modalInstance = $modal.open({
                templateUrl: 'app/partials/AddForumModal.html',
                size: 'sm',
                controller: 'AddForumModalCtrl',
            });

            $scope.modalInstance.result.then(function (result) {
                console.log(result);
                $scope.forums.push(result);
            });

        }

        $scope.removeForums = function () {
            $scope.modalInstance = $modal.open({
                templateUrl: 'app/partials/remove-forum-modal.html',
                size: 'sm',
                controller: 'RemoveForumCtrl'
            });

            $scope.modalInstance.result.then(function (result) {
                $scope.forums = result;
            });
        }

    }
})();
