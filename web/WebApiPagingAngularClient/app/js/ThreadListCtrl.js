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

            if ($routeParams.userId) {
                var queryArgsUser = {
                    forumId: $routeParams.forumId,
                    userId: $routeParams.userId
                };
                return Forums.getUser(queryArgsUser).$promise.then(
                    function (result) {
                        console.log(result.data);
                        $scope.forum = parseForum(result.data);
                        console.log($scope.forum);
                        return result.$promise;
                    }, function (result) {
                        return $q.reject(result);
                    });
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

                return Forums.addThread({}, queryArgs).$promise.then(
                    function (result) {
                        return Forums.getSubForum(queryArgs).$promise.then(
                            function (result) {
                                $scope.forum = result.data;
                                $scope.subForum = $scope.forum.subForum;
                                return result.$promise;
                            }, function (result) {
                                return $q.reject(result);
                            });
                    }, function (result) {
                        return $q.reject(result);
                    });
            } else {
                alert("log in first");
            }
        }

        $scope.addComment = function (threadId) {
            if ($scope.user) {
                var queryArgs = {
                    firstMessageId: threadId,
                    publisherID: $scope.user.id,
                    publisherName: $scope.user.username,
                    title: 'snsacksc',
                    body: 'sacijbsacjksa sakcbskjac asskacnsak'
                };
            } else {
                var queryArgs = {
                    firstMessageId: threadId,
                    publisherID: 0,
                    publisherName: 'amit romem',
                    title: 'sjicbsjcbsajck',
                    body: 'sacjibscjajcsnac sldkvklsac'
                };
            }

            return Forums.addComment({}, queryArgs).$promise.then(
                function (result) {
                    return result.$promise;
                }, function (result) {
                    return $q.reject(result);
                });
        }
    }
})();