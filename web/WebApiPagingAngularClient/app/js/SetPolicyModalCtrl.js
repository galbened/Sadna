(function () {
    'use strict';

    angular
        .module('app')
        .controller('SetPolicyModalCtrl', SetPolicyModalCtrl);

    SetPolicyModalCtrl.$inject = ['$scope', 'Forums', '$modalInstance','$routeParams'];

    function SetPolicyModalCtrl($scope, Forums, $modalInstance, $routeParams) {
        activate();

        var parseForum = function (data) {
            var forum = {
                'id': data.id,
                'title': data.title
            };
            return forum;
        }

        function activate() {
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
        }

        $scope.setPolicy = function (numOfModerators, uppercase, lowercase, numbers, symbols, minLength) {
            var queryParam = {
                "forumId": $routeParams.forumId,
                "userId":$scope.params.userId
            };
            var newForumParams = {
                'name': "",
                'numOfModerators': numOfModerators,
                'degreeOfEnsuring': '',
                'uppercase': uppercase,
                'lowercase': lowercase,
                'numbers': numbers,
                'symbols': symbols,
                'minLength': minLength
            };

            return Forums.setPolicy(queryParam, newForumParams).$promise.then(
                function (result) {
                    alert("policy was updated");
                    $modalInstance.close('success');
                }, function (result) {
                    console.log(result);
                    alert(result.data.message);
                    return $q.reject(result);
                });
        }
    }
})();
