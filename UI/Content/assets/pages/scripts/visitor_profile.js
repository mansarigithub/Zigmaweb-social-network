$(function () {
    zw.page.initialize();
})

zw.page = {
    strings: {
    },
    followBtn: null,
    initialize: function () {
        zw.page.setFollowClickHandler()
        zw.page.setSendMessageClickHandler()
    },

    setFollowClickHandler() {
        $('.follow-btn').click(function (e) {
            var btn = $(this);
            var userId = $('#ProfileId').val();

            if (btn.attr('data-role') == 'unfollow')
                zw.page.unfollowUser(userId);
            else
                zw.page.followUser(userId);
        });
    },

    setSendMessageClickHandler() {
        $('#sendMessageBtn').click(function (e) {
            var msg = $('#messageText').val();
            if (msg == null || msg.trim() == '') return;

            $.ajax('/profile/sendmessage', {
                method: 'POST',
                data: {
                    userId: $('#ProfileId').val(),
                    text: msg
                },
                beforeSend: function () {
                    $('#messageModal button').addClass('disabled');
                },
                success: function (response) {
                    $('#messageModal').modal('hide');
                    $('#messageText').val('');
                    $('#messageModal .error-msg').hide();
                },
                error: function (response) {
                    $('#messageModal .error-msg').show();
                },
                complete: function (response) {
                    $('#messageModal button').removeClass('disabled');
                },
            });

        });
    },

    followUser: function (userId) {
        var btn = $('.follow-btn');
        $.ajax('/profile/followuser', {
            method: 'POST',
            data: { userId: userId },
            beforeSend: function () {
                btn.addClass('disabled');
            },
            success: function (response) {
                btn.attr('data-role', 'unfollow');
                btn.removeClass('btn-default').addClass('btn-success');
                btn.children('i').removeClass('fa-user-plus').addClass('fa-check');
            },
            complete: function (response) {
                btn.removeClass('disabled');
            },
        });
    },

    unfollowUser: function (userId) {
        var btn = $('.follow-btn');
        $.ajax('/profile/unfollowuser', {
            method: 'POST',
            data: { userId: userId },
            beforeSend: function () {
                btn.addClass('disabled');
            },
            success: function (response) {
                btn.attr('data-role', 'follow');
                btn.removeClass('btn-success').addClass('btn-default');
                btn.children('i').removeClass('fa-check').addClass('fa-user-plus');
            },
            complete: function (response) {
                btn.removeClass('disabled');
            },
        });
    },
}