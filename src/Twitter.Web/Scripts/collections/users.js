var app = app || {};

(function () {
    'use strict';

    var Users = Backbone.Collection.extend({
        model: app.User,
        url: '/api/people'
    });

    app.users = new Users();
})();