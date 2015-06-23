(function () {
    'use strict';

    angular
        .module('app')
        .controller('RemoveForumCtrl', RemoveForumCtrl);

    RemoveForumCtrl.$inject = ['$scope','Forums','$modalInstance'];

    function RemoveForumCtrl($scope, Forums, $modalInstance) {
        activate();

        var parseForums = function (data) {
            var forums = [];
            console.log(data);
            for (var i = 0; i < data.ids.length; i++) {
                var forum = {
                    'title': data.names[i],
                    'id': data.ids[i]
                }
                forums.push(forum);
            }
            console.log(forums);
            return forums;
        }

        function activate() {
            $scope.notAdmin = true;
            $scope.incorrectPaswword = false;
        }

        $scope.superAdminLogin = function (username, password) {
            if (username === 'admin' && password === 'admin') {
                $scope.notAdmin = false;
                $scope.incorrectPaswword = false;
                return Forums.getForums().$promise.then(
                function (result) {
                    $scope.forums = parseForums(result.data);
                    return result.$promise;
                }, function (result) {
                    return $q.reject(result);
                });
            } else {
                $scope.incorrectPaswword = true;
            }

        };

        $scope.removeForum = function (forumId) {
            var deleteForumParams = {
                'forumId': forumId
            };

            return Forums.removeForum(deleteForumParams).$promise.then(
                function (result) {
                    return Forums.getForums().$promise.then(
                        function (result) {
                            $scope.forums = parseForums(result.data);
                            $modalInstance.close($scope.forums);
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
