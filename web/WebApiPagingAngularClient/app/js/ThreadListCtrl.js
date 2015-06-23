(function () {
    'use strict';

    angular
        .module('app')
        .controller('ThreadListCtrl', ThreadListCtrl);

    ThreadListCtrl.$inject = ['$scope', '$routeParams', 'Forums', '$http', '$q', '$rootScope', '$modal'];

    function ThreadListCtrl($scope, $routeParams, Forums, $http, $q, $rootScope, $modal) {
        activate();

        function activate() {
            var queryArgs = {
                forumId: $routeParams.forumId,
                subForumId: $routeParams.subForumId,
            };

            return Forums.getSubForum(queryArgs).$promise.then(
                function (result) {
                    $scope.forum = result.data;
                    $scope.subForum = $scope.forum.subForum;

                    console.log($scope.subForum);
                    if ($routeParams.userId) {
                        var queryArgsUser = {
                            forumId: $routeParams.forumId,
                            userId: $routeParams.userId
                        };
                        return Forums.getUser(queryArgsUser).$promise.then(
                            function (result) {
                                $scope.user = result.data;
                                return result.$promise;
                            }, function (result) {
                                return $q.reject(result);
                            });
                    }
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
                        var queryArgsSubForum = {
                            forumId: $routeParams.forumId,
                            subForumId: $routeParams.subForumId,
                        };
                        return Forums.getSubForum(queryArgsSubForum).$promise.then(
                            function (result) {
                                $scope.forum = result.data;
                                $scope.subForum = $scope.forum.subForum;
                                return result.$promise;
                            }, function (result) {
                                alert(result.data.message);
                                return $q.reject(result);
                            });
                    }, function (result) {
                        alert(result.data.message);
                        return $q.reject(result);
                    });
            } else {
                alert("log in first");
            }
        }

        $scope.addComment = function (threadId, comment) {
            if ($scope.user) {
                var queryArgs = {
                    firstMessageId: threadId,
                    publisherID: $scope.user.id,
                    publisherName: $scope.user.username,
                    title: comment.title,
                    body: comment.content
                };
                return Forums.addComment({}, queryArgs).$promise.then(
                    function (result) {
                        var queryArgsSubForum = {
                            forumId: $routeParams.forumId,
                            subForumId: $routeParams.subForumId,
                        };
                        return Forums.getSubForum(queryArgsSubForum).$promise.then(
                            function (result) {
                                $scope.forum = result.data;
                                $scope.subForum = $scope.forum.subForum;
                                return result.$promise;
                            }, function (result) {
                                alert(result.data.message);
                                return $q.reject(result);
                            });
                    }, function (result) {
                        alert(result.data.message);
                        return $q.reject(result);
                    });
            } else {
                alert("log in first");
            }


        }
        
        $scope.openLoginModal = function () {
            $scope.modalInstance = $modal.open({
                templateUrl: 'app/partials/login-modal.html',
                size: 'sm',
                controller: 'LoginModalCtrl'
            });

            $scope.modalInstance.result.then(function (result) {
                console.log(result);
                $scope.user = result;
            });
        };

        $scope.openSignupModal = function () {
            $scope.modalInstance = $modal.open({
                templateUrl: 'app/partials/signup-modal.html',
                size: 'sm',
                controller: 'SignupModalCtrl'
            });

            $scope.modalInstance.result.then(function (result) {
                console.log(result);
                $scope.user = result;
            });
        };

        $scope.logout = function () {
            var queryArgs = {
                forumId: $rootScope.forumId,
                userId: $scope.user.id
            };

            return Forums.logout(queryArgs).$promise.then(
                function (result) {
                    $scope.user = undefined;
                }, function (result) {
                });
        };

        $scope.deleteMessage = function (messageId) {
            var conf = confirm("are you sure?");
            if (conf) {
                var queryArgs = {
                    userId: $scope.user.id,
                    messageId: messageId
                };

                return Forums.deleteMessage(queryArgs).$promise.then(
                    function (result) {
                        var queryArgsSubForum = {
                            forumId: $routeParams.forumId,
                            subForumId: $routeParams.subForumId,
                        };
                        return Forums.getSubForum(queryArgsSubForum).$promise.then(
                            function (result) {
                                $scope.forum = result.data;
                                $scope.subForum = $scope.forum.subForum;
                                return result.$promise;
                            }, function (result) {
                                alert(result.data.message);
                                return $q.reject(result);
                            });
                    }, function (result) {
                        alert(result.data.message);
                        return $q.reject(result);
                    });
            }
        };
    }
})();