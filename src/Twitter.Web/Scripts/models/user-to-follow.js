var app = app || {};

(function () {
    'use strict';
    app.UserToFollow = Backbone.Model.extend({
        idAttribute: 'userId',

        defaults: {
            nickname: '',
            isFollowed: false
        }
    });
})();