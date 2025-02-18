﻿(function () {
    'use strict';

    var app = angular.module('app', ['ngResource', 'ngRoute','ui.bootstrap']);

    app.config(['$routeProvider',function ($routeProvider) {

        $routeProvider.when('/forums', {
            templateUrl: 'app/partials/forum-list.html',
            controller: 'ForumListCtrl',
            caseInsensitiveMatch: true
        });
        $routeProvider.when('/subForums/:forumId/:userId?', {
            templateUrl: 'app/partials/subforum-list.html',
            controller: 'SubForumListCtrl'
        });
        $routeProvider.when('/threads/:forumId/:subForumId/:userId?', {
            templateUrl: 'app/partials/thread-list.html',
            controller: 'ThreadListCtrl'
        });
        $routeProvider.otherwise({
            redirectTo: '/forums'
        });
    }]);

    app.run([function ($rootScope) {
    }]);
})();