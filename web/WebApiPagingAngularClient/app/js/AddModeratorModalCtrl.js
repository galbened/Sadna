(function () {
    'use strict';

    angular
        .module('app')
        .controller('AddModeratorModalCtrl', AddModeratorModalCtrl);

    AddModeratorModalCtrl.$inject = ['$scope', 'Forums', '$modalInstance', '$routeParams', '$q'];

    function AddModeratorModalCtrl($scope, Forums, $modalInstance, $routeParams, $q) {
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
            return Forums.getNotModerators({ 'forumId': $routeParams.forumId, 'subForumId': $routeParams.subForumId }).$promise.then(
                function (result) {
                    $scope.moderators = parseModerators(result.data);
                }, function (result) {
                    console.log(result);
                    alert(result.data.message);
                });
        }

        $scope.addModerator = function (moderatorId) {
            var newModeratorParams = {
                'forumId': $routeParams.forumId,
                'subForumId': $routeParams.subForumId,
                'userId': $scope.params.userId,
                'moderatorId': moderatorId
            };

            return Forums.addModerator(newModeratorParams).$promise.then(
                function (result) {
                    alert("moderator added successfully");
                    $modalInstance.close('success');
                }, function (result) {
                    console.log(result);
                    alert(result.data.message);
                });
        };
    }
})();