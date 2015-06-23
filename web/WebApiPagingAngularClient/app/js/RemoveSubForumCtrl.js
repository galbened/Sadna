(function () {
    'use strict';

    angular
        .module('app')
        .controller('RemoveSubForumCtrl', RemoveSubForumCtrl);

    RemoveSubForumCtrl.$inject = ['$scope', 'Forums', '$modalInstance','$routeParams'];

    function RemoveSubForumCtrl($scope, Forums, $modalInstance, $routeParams) {
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
            $scope.id = $scope.params.userId;
            console.log($scope.params);

            var queryArgsForum = {
                forumId: $routeParams.forumId
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
        }

        $scope.removeForum = function (subForumId) {
            var deleteSubForumParams = {
                'forumId': forumId,
                'userId': $scope.id,
                'subForumId': subForumId
            };

            return Forums.removeSubForum(deleteSubForumParams).$promise.then(
                function (result) {
                    return Forums.getForum({ 'forumId': forumId }).$promise.then(
                        function (result) {
                            $scope.forum = parseForum(result.data);
                            $modalInstance.close($scope.forum);
                        }, function (result) {
                            return $q.reject(result);
                        });

                }, function (result) {
                    console.log(result);
                    alert(result.data.message);
                    return $q.reject(result);
                });
        };
    }
})();
