var app = app || {};

(function () {
    'use strict';
    app.User = Backbone.Model.extend({
        idAttribute: 'username',
        urlRoot: '/api/people',
        url: function () {
            return this.urlRoot + '/' + encodeURIComponent(this.id);
        },
        defaults: {
            isFollowed: false
        }
    });
})();