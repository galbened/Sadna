(function () {
    'use strict';

    angular
        .module('app')
        .factory('Forums', function ($resource) {
            return $resource("api/forums/",
                {},
                {
                    'getForums': {
                        method: 'GET',
                        url: '/api/forums/getForums',
                        params: {}
                    },
                    'getForum': {
                        method: 'GET',
                        url: '/api/forums/getForum/:forumId',
                        params: { forumId: '@forumId' }
                    },
                    'getSubForum': {
                        method: 'GET',
                        url: '/api/forums/getSubForum/:forumId/:subForumId',
                        params: { forumId: '@forumId',subForumId:'@subForumId' }
                    },
                });
        }).factory('Users', function ($resource) {
                return $resource("api/users/",
                    {},
                    {
                        'getUser': {
                            method: 'GET',
                            url: '/api/users/getUser',
                            params: {}
                        },
                        'signup': {
                            method: 'POST',
                            url: '/api/users/signup/:forumId',
                            params: { forumId: '@forumId' }
                        },
                    });
            });
})();



