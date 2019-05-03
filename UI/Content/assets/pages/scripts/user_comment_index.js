$(function () {
    zw.page.initialize();
})

zw.page = {
    strings: {
        sendDate: 'تاریخ ارسال',
        inReplay: 'در پاسخ به',
        commentsDeleted: 'دیدگاه‌های انتخاب شده حذف شدند',
    },

    initialize: function () {
        this.initialCommentsGrid();
        $('.delete-comments').click(this.deleteComments);
    },

    initialCommentsGrid: function () {
        $('.comments-grid').DataTable({
            language: {
                url: '/content/assets/global/plugins/datatables/localization/persian.js'
            },
            ajax: {
                url: '/user/comment/readcomments',
                type: 'GET'
            },
            initComplete: function (settings, json) {
                $('.number-fa').persiaNumber();
            },
            columns: [
                {
                    data: 'Id',
                    render: function (data, type, full, meta) {
                        return '<label class="mt-checkbox mt-checkbox-single mt-checkbox-outline"><input type="checkbox" class="checkboxes"><span></span></label>'
                    }
                },
                {
                    data: 'SenderFullName',
                    title: zw.strings.sender,
                },
                {
                    data: 'Text',
                    title: zw.strings.comment,
                    width: '50%',
                    render: function (data, type, full, meta) {
                        var text = full.Text.split('\n').join('<br/>');
                        if (!full.IsPrivate)
                            return text;
                        return '<span class="badge badge-danger badge-roundless" style="float:left">خصوصی</span>' +
                            '<div class="font-red">' + text + '</div>';
                    }
                },
                {
                    data: 'ContentTitle',
                    title: zw.page.strings.inReplay,
                },
                {
                    data: 'CreateDateString',
                    title: zw.page.strings.sendDate,
                    render: function (data, type, full, meta) {
                        return '<span class="number-fa">' + data + '</span>';
                    }
                },

            ]
        });
    },

    deleteComments: function (e) {
        var grid = $('.comments-grid').DataTable();
        var selectedRows = [], selectedRowsIds = [];
        $('tbody > tr > td:nth-child(1) input[type="checkbox"]:checked').each(function () {
            var row = $(this).parent().parent().parent();
            selectedRows.push(row);
            selectedRowsIds.push(grid.row(row).data().Id);
        });

        $.ajax('/user/comment/delete', {
            type: 'POST',
            data: { ids: selectedRowsIds },
            success: function (response) {
                grid.rows(selectedRows).remove().draw(false);
                zw.ui.showMessage(zw.page.strings.articlesTrashed, '');
            },
        });
    }
}
