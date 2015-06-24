(function () {
    'use strict';

    angular
        .module('app')
        .controller('RemoveModeratorModalCtrl', RemoveModeratorModalCtrl);

    RemoveModeratorModalCtrl.$inject = ['$scope', 'Forums', '$modalInstance', '$routeParams', '$q'];

    function RemoveModeratorModalCtrl($scope, Forums, $modalInstance, $routeParams, $q) {
        activate();

        var parseModerators = function (data) {
            var moderators = [];
            for (var i = 0; i < data.usernames.length; i++) {
                var moderator = {
                    'id': data.ids[i],
                    'username': data.usernames[i]
                }
                moderators.push(moderator);
            }
            return moderators;
        };

        function activate() {
            return Forums.getModerators({ 'forumId': $routeParams.forumId, 'subForumId': $routeParams.subForumId }).$promise.then(
                function (result) {
                    $scope.moderators = parseModerators(result.data);
                }, function (result) {
                    console.log(result);
                    alert(result.data.message);
                });
        }

        $scope.removeModerator = function (moderatorId) {
            var newModeratorParams = {
                'forumId': $routeParams.forumId,
                'subForumId': $routeParams.subForumId,
                'userId': $scope.params.userId,
                'moderatorId': moderatorId
            };

            var name;
            for (var i = 0; i < $scope.moderators.length; i++) {
                if (moderatorId == $scope.moderators[i].id) {
                    name = $scope.moderators[i].username;
                }
            }

            return Forums.removeModerator(newModeratorParams).$promise.then(
                function (result) {
                    alert(name + " was removed");
                    $modalInstance.close('success');
                }, function (result) {
                    console.log(result);
                    alert(result.data.message);
                });
        };
    }
})();