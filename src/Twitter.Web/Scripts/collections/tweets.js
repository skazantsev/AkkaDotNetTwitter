var app = app || {};

(function () {
    'use strict';

    var Tweets = Backbone.Collection.extend({
        model: app.Tweet
    });

    app.tweets = new Tweets();
})();