$(function () {
    zw.page.initialize();
})

zw.page = {
    strings: {
    },

    initialize: function () {
        
        $('#registerCommentButton').click(this.onRegisterCommentButtonClick);
    },

    getNewComment() {
        return {
            Text: $('.new-comment-area textarea').val().trim(),
            ContentId: $('#contentId').val(),
            IsPrivate: $('#isPrivateCheckbox').is(':checked')
        }
    },

    onRegisterCommentButtonClick: function () {
        var comment = zw.page.getNewComment();
        if (comment.Text == '') {
            $('.new-comment-area textarea').focus();
            return;
        }

        $.ajax('/blog/addcomment', {
            method: 'POST',
            data: comment,
            success: function (response) {
                if (!comment.IsPrivate) {
                    comment.Id = response.Id;
                    comment.CreateDate = response.CreateDate;
                    zw.page.addCommentToList(comment);
                }
                $('.new-comment-area textarea').val('');
            },
            error: function (response) {
            }
        });
    },

    addCommentToList(comment) {
        var newItem = $('#comment-template').clone();
        newItem.removeClass('item-template').removeAttr('id');
        $('.item-body', newItem).html(comment.Text.split('\n').join('<br/>'));
        $('.item-label', newItem).text(comment.CreateDate);
        $('.comment-area .general-item-list').append(newItem);
        newItem.hide().fadeIn();
        $('.comment-portlet').removeClass('hide');

        // increment 
        var hidden = $('#commentsCounter');
        var commentsCount = parseInt(hidden.val());
        commentsCount++;
        hidden.val(commentsCount);
        $('.comments-count').html(commentsCount);
    }
}