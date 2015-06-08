var app = app || {};

(function ($) {
    'use strict';

    app.AppView = Backbone.View.extend({
        el: '#twitterApp',

        peopleTemplate: _.template($('#peopleTemplate').html()),

        events: {
            'click .js-follow-btn': 'followUser',
            'click .js-unfollow-btn': 'unfollowUser'
        },

        initialize: function () {
            this.$usersToFollowRoot = this.$('#usersToFollowRoot');

            this.listenTo(app.usersToFollow, 'reset', this.render);
            this.listenTo(app.usersToFollow, 'change', this.render);

            app.usersToFollow.fetch({ reset: true });
        },

        render: function () {
            this.$usersToFollowRoot.html(this.peopleTemplate({
                usersToFollow: app.usersToFollow.toJSON()
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
            var userId = $target.closest('.js-user-row').find('.js-user-id').val();
            return app.usersToFollow.get(userId);
        }

    });
})(jQuery);