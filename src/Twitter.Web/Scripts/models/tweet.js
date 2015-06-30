var app = app || {};

(function () {
    'use strict';
    app.Tweet = Backbone.Model.extend({
        defaults: {
            from: '',
            message: ''
        }
    });
})();