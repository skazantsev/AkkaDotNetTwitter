var app = app || {};

// TODO: delegate responsibilities to inner views
(function ($) {
    'use strict';

    app.AppView = Backbone.View.extend({
        el: '#twitterApp',

        usersTemplate: _.template($('#usersTemplate').html()),
        tweetTemplate: _.template($('#tweetTemplate').html()),

        events: {
            'click .js-follow-btn': 'followUser',
            'click .js-unfollow-btn': 'unfollowUser'
        },

        initialize: function () {
            this.userHub = $.connection.userHub;

            this.userHub.client.userConnected = function (username) {
                app.users.add(new app.User({ username: username, isFollowed: false }));
            }.bind(this);

            this.userHub.client.messagePosted = function (username, message) {
                app.tweets.add(new app.Tweet({ username: username, text: message }));
            }.bind(this);

            $.connection.hub.start().done(function () {
                app.users.fetch({ reset: true });
            });

            this.$usersRoot = this.$('#users');
            this.$tweetsRoot = this.$('#tweets');

            this.listenTo(app.users, 'add', this.render);
            this.listenTo(app.users, 'change', this.render);
            this.listenTo(app.users, 'reset', this.render);

            this.listenTo(app.tweets, 'add', this.renderTweet);
        },
        
        render: function() {
            this.$usersRoot.html(this.usersTemplate({
                users: app.users.toJSON()
            }));
            return this;
        },

        renderTweet: function (tweet) {
            this.$tweetsRoot.prepend(this.tweetTemplate({
                username: tweet.get('username'),
                text: tweet.get('text')
            }));
        },

        followUser: function (e) {
            var userModel = this.getUserModel($(e.currentTarget));
            userModel.save({ 'isFollowed': true }, { wait: true });
        },

        unfollowUser: function (e) {
            var userModel = this.getUserModel($(e.currentTarget));
            userModel.save({ 'isFollowed': false }, { wait: true });
        },

        getUserModel: function ($target) {
            var username = $target.closest('.js-user-row').find('.js-username').val();
            return app.users.get(username);
        }
    });
})(jQuery);