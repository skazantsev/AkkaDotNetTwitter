var app = app || {};

(function ($) {
    'use strict';

    app.AppView = Backbone.View.extend({
        el: '#twitterApp',

        usersTemplate: _.template($('#usersTemplate').html()),

        events: {
            'click .js-follow-btn': 'followUser',
            'click .js-unfollow-btn': 'unfollowUser'
        },

        initialize: function () {
            this.userHub = $.connection.userHub;
            this.userHub.client.userConnected = function (username) {
                app.users.add(new app.User({ username: username, isFollowed: false }));
            };

            $.connection.hub.start().done(function () {
                app.users.fetch({ reset: true });
            });

            this.$usersRoot = this.$('#users');

            this.listenTo(app.users, 'add', this.render);
            this.listenTo(app.users, 'change', this.render);
            this.listenTo(app.users, 'reset', this.render);
        },

        render: function() {
            this.$usersRoot.html(this.usersTemplate({
                users: app.users.toJSON()
            }));
            return this;
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