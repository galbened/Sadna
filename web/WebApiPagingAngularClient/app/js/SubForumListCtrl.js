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

        $scope.openLoginModal = function () {
            $modal.open({
                templateUrl: 'app/partials/login-modal.html',
                size: 'sm',
                controller: 'LoginModalCtrl'
            });
        };

        $scope.openSignupModal = function () {
            $scope.modalInstance =  $modal.open({
                templateUrl: 'app/partials/signup-modal.html',
                size: 'sm',
                controller: 'SignupModalCtrl'
            });

            $scope.modalInstance.result.then(function (result) {
                console.log(result);
                $scope.user = result;
            });
        }

        $scope.addNewSubForum = function () {
            $scope.modalInstance = $modal.open({
                templateUrl: 'app/partials/AddSubForumModal.html',
                size: 'sm',
                controller: 'AddSubForumModalCtrl'
            });

            $scope.modalInstance.result.then(function (result) {
                console.log(result);
                $scope.forum.subForums.push(result);
            });
        }


    }
})();
