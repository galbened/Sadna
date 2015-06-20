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
            var queryArgs = {
                forumId: $routeParams.forumId,
            };

            $rootScope.forumId = $routeParams.forumId;

            if ($rootScope.user) {
                $scope.user = $rootScope.user;
            }

            return Forums.getForum(queryArgs).$promise.then(
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
            $modal.open({
                templateUrl: 'app/partials/signup-modal.html',
                size: 'sm',
                controller: 'SignupModalCtrl'
            });
        }

        $scope.addNewSubForum = function () {
            $modal.open({
                templateUrl: 'app/partials/AddSubForumModal.html',
                size: 'sm',
                controller: 'AddSubForumModalCtrl'
            });
        }


    }
})();
