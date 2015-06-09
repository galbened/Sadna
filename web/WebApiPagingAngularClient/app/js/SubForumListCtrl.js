(function () {
    'use strict';

    angular
        .module('app')
        .controller('SubForumListCtrl', SubForumListCtrl);

    SubForumListCtrl.$inject = ['$scope', '$routeParams', '$modal', 'Forums','Users', '$http', '$q','$rootScope'];

    function SubForumListCtrl($scope, $routeParams, $modal, Forums, Users, $http, $q, $rootScope) {
        activate();

        function activate() {
            var queryArgs = {
                forumId: $routeParams.forumId,
            };

            $rootScope.forumId = $routeParams.forumId;

            return Forums.getForum(queryArgs).$promise.then(
                function (result) {
                    $scope.forum = result.data;
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
