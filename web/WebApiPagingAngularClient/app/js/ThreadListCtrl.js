(function () {
    'use strict';

    angular
        .module('app')
        .controller('ThreadListCtrl', ThreadListCtrl);

    ThreadListCtrl.$inject = ['$scope', '$routeParams', 'Forums', '$http', '$q', '$rootScope'];

    function ThreadListCtrl($scope, $routeParams, Forums, $http, $q, $rootScope) {
        activate();

        function activate() {
            var queryArgs = {
                forumId: $routeParams.forumId,
                subForumId: $routeParams.subForumId,
            };

            if ($rootScope.user) {
                $scope.user = $rootScope.user;
            }

            return Forums.getSubForum(queryArgs).$promise.then( 
                function (result) {
                    $scope.forum = result.data;
                    $scope.subForum = $scope.forum.subForum;
                    return result.$promise;
                }, function (result) {
                    return $q.reject(result);
                });
        }

        $scope.addThread = function (title, content) {
            if ($scope.user) {
                var queryArgs = {
                    forumId: $routeParams.forumId,
                    subForumId: $routeParams.subForumId,
                    publisherID: $scope.user.id,
                    publisherName: $scope.user.username,
                    title: title,
                    body: content
                };
            } else {
                var queryArgs = {
                    forumId: $routeParams.forumId,
                    subForumId: $routeParams.subForumId,
                    publisherID: 0,
                    publisherName: 'amit romem',
                    title: title,
                    body: content
                };
            }

            return Forums.addThread({},queryArgs).$promise.then(
                function (result) {
                    return result.$promise;
                }, function (result) {
                    return $q.reject(result);
                });
        }
    }
})();