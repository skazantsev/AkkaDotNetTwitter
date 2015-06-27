var app = app || {};

(function () {
    'use strict';
    app.User = Backbone.Model.extend({
        idAttribute: 'username',

        defaults: {
            isFollowed: false
        }
    });
})();