var app = app || {};

(function () {
    'use strict';

    var UsersToFollow = Backbone.Collection.extend({
        model: app.UserToFollow,
        url: '/api/people'
    });

    app.usersToFollow = new UsersToFollow();
})();