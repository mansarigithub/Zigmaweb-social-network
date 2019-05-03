$(function () {
    zw.page.initialize();
})

zw.page = {
    strings: {
        sendDate: 'تاریخ ارسال',
        message: 'پیام',
        receiver: 'دریافت کننده',
        messagesDeleted: 'پیام‌های انتخاب شده حذف شدند',
    },

    initialize: function () {
        this.initialGrid();
        $('.delete-comments').click(this.deleteComments);
    },

    initialGrid: function () {
        $('.dataTable').DataTable({
            language: {
                url: '/content/assets/global/plugins/datatables/localization/persian.js'
            },
            ajax: {
                url: '/user/message/readsentmessages',
                type: 'GET'
            },
            initComplete: function (settings, json) {
                $('.number-fa').persiaNumber();
            },
            columns: [
                //{
                //    data: 'Id',
                //    render: function (data, type, full, meta) {
                //        return '<label class="mt-checkbox mt-checkbox-single mt-checkbox-outline"><input type="checkbox" class="checkboxes"><span></span></label>'
                //    }
                //},
                {
                    data: 'ReceiverFullName',
                    title: zw.page.strings.receiver,
                },
                {
                    data: 'Text',
                    title: zw.page.strings.message,
                    width: '60%',
                    render: function (data, type, full, meta) {
                        return full.Text.split('\n').join('<br/>');
                    }
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
        var grid = $('.dataTable').DataTable();
        var selectedRows = [], selectedRowsIds = [];
        $('tbody > tr > td:nth-child(1) input[type="checkbox"]:checked').each(function () {
            var row = $(this).parent().parent().parent();
            selectedRows.push(row);
            selectedRowsIds.push(grid.row(row).data().Id);
        });

        $.ajax('/user/message/delete', {
            type: 'POST',
            data: { ids: selectedRowsIds },
            success: function (response) {
                grid.rows(selectedRows).remove().draw(false);
                zw.ui.showMessage(zw.page.strings.messagesDeleted, '');
            },
        });
    }
}
