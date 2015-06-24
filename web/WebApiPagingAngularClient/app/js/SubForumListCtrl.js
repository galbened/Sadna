(function () {
    'use strict';

    angular
        .module('app')
        .controller('SubForumListCtrl', SubForumListCtrl);

    SubForumListCtrl.$inject = ['$scope', '$routeParams', '$modal', 'Forums','Users', '$http', '$q','$rootScope'];

    function SubForumListCtrl($scope, $routeParams, $modal, Forums, Users, $http, $q, $rootScope) {
        activate();

        var parseForum = function (data) {
            var subForums = [];
            for (var i = 0; i < data.subForumIds.length; i++) {
                var subForum = {
                    'title': data.subForumTopics[i],
                    'id': data.subForumIds[i]
                }
                subForums.push(subForum);
            }
            var forum = {
                'id': data.id,
                'title': data.title,
                'subForums': subForums
            };
            return forum;
        }

        function activate() {
            var queryArgsForum = {
                forumId: $routeParams.forumId,
            };

            $rootScope.forumId = $routeParams.forumId;

            if ($routeParams.userId) {
                $rootScope.userId = $routeParams.userId;
                var queryArgsUser = {
                    forumId: $routeParams.forumId,
                    userId: $routeParams.userId
                };
                return Forums.getUser(queryArgsUser).$promise.then(
                    function (result) {
                        $scope.user = result.data;
                        return Forums.getForum(queryArgsForum).$promise.then(
                            function (result) {
                                console.log(result.data);
                                $scope.forum = parseForum(result.data);
                                console.log($scope.forum);
                                return result.$promise;
                            }, function (result) {
                                return $q.reject(result);
                            });
                    }, function (result) {
                        return $q.reject(result);
                    });
            } else {
                return Forums.getForum(queryArgsForum).$promise.then(
                    function (result) {
                        console.log(result.data);
                        $scope.forum = parseForum(result.data);
                        console.log($scope.forum);
                        return result.$promise;
                    }, function (result) {
                        return $q.reject(result);
                    });
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
                $rootScope.userId = $scope.user.userId;
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
                $rootScope.userId = $scope.user.userId;
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

        $scope.addNewSubForum = function () {

            var scope = $rootScope.$new();
            scope.params = { userId: $scope.user.id };

            $scope.modalInstance = $modal.open({
                scope: scope,
                templateUrl: 'app/partials/AddSubForumModal.html',
                size: 'sm',
                controller: 'AddSubForumModalCtrl'
            });

            $scope.modalInstance.result.then(function (result) {
                console.log(result);
                $scope.forum.subForums.push(result);
            });
        }

        $scope.removeSubForum = function () {

            var scope = $rootScope.$new();
            scope.params = { userId: $scope.user.id };

            $scope.modalInstance = $modal.open({
                scope: scope,
                templateUrl: 'app/partials/remove-subforum-modal.html',
                size: 'sm',
                controller: 'RemoveSubForumCtrl'
            });

            $scope.modalInstance.result.then(function (result) {
                var queryArgsForum = {
                    forumId: $routeParams.forumId,
                };
                return Forums.getForum(queryArgsForum).$promise.then(
                    function (result) {
                        console.log(result.data);
                        $scope.forum = parseForum(result.data);
                        console.log($scope.forum);
                        return result.$promise;
                    }, function (result) {
                        return $q.reject(result);
                    });
            });
        }

        $scope.addAdmin = function () {
            var scope = $rootScope.$new();
            scope.params = { userId: $scope.user.id };
            $scope.modalInstance = $modal.open({
                scope: scope,
                templateUrl: 'app/partials/add-admin-modal.html',
                size: 'sm',
                controller: 'AddAdminModalCtrl'
            });
        }


    }
})();
