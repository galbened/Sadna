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
                    'createForum': {
                        method: 'POST',
                        url: '/api/forums/createForum',
                        params: {}
                    },
                    'createSubForum': {
                        method: 'POST',
                        url: '/api/forums/createSubForum/:userId/:forumId',
                        params: { userId:'@userId', forumId: '@forumId' }
                    },
                    'signup': {
                        method: 'POST',
                        url: '/api/forums/signup/:forumId',
                        params: { forumId: '@forumId' }
                    },
                    'addThread': {
                        method: 'POST',
                        url: '/api/forums/addThread',
                        params: {}
                    },
                    'addComment': {
                        method: 'POST',
                        url: '/api/forums/addComment',
                        params: {}
                    },
                    'getUser': {
                        method: 'GET',
                        url: '/api/forums/getUser/:forumId/:userId',
                        params: { forumId: '@forumId', userId: '@userId' }
                    },
                    'login': {
                        method: 'POST',
                        url: '/api/forums/login/:forumId',
                        params: { forumId: '@forumId'}
                    },
                    'logout': {
                        method: 'GET',
                        url: '/api/forums/logout/:forumId/:userId',
                        params: { forumId: '@forumId', userId: '@userId' }
                    }
                });
        }).factory('Users', function ($resource) {
                return $resource("api/users/",
                    {},
                    {
                        'getUser': {
                            method: 'GET',
                            url: '/api/users/getUser',
                            params: {}
                        } 
                    });
            });
})();



