(function () {
    'use strict';

    angular
        .module('app')
        .controller('ThreadListCtrl', ThreadListCtrl);

    ThreadListCtrl.$inject = ['$scope', '$routeParams', 'Forums', '$http', '$q'];

    function ThreadListCtrl($scope, $routeParams, Forums, $http, $q) {
        activate();

        function activate() {
            var queryArgs = {
                forumId: $routeParams.forumId,
                subForumId: $routeParams.subForumId,
            };

            return Forums.getSubForum(queryArgs).$promise.then( 
                function (result) {
                    $scope.forums = result.data;
                    angular.forEach($scope.forums, function (forum) {
                        if (forum.id == $routeParams.forumId) {
                            $scope.forum = forum;
                            angular.forEach(forum.subForums, function (subForum) {
                                if (subForum.id == $routeParams.subForumId) {
                                    $scope.subForum = subForum;
                                }
                            })
                        }
                    })
                    return result.$promise;
                }, function (result) {
                    return $q.reject(result);
                });
        }
    }
})();