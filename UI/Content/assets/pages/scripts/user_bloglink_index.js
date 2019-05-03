$(function () {
    zw.page.initialize();
});

zw.page = {
    strings: {
        selectedRowsDeleted: 'آیتم های انتخاب شده حذف شدند',
        name: 'نام',
        address: 'ادرس'
    },

    initialize: function () {
        this.initialGrid();
        $('#addFriendButton').click(this.onAddFriendClick);
        $('.delete-items').click(this.deletePosts);
    },

    initialGrid: function () {
        $('.dataTable').DataTable({
            language: {
                url: '/content/assets/global/plugins/datatables/localization/persian.js'
            },
            ajax: {
                url: '/User/BlogLink/ReadFriends',
                type: 'GET'
            },
            columns: [
                {
                    data: 'Id',
                    width: '50px',
                    render: function (data, type, full, meta) {
                        return '<label class="mt-checkbox mt-checkbox-single mt-checkbox-outline"><input type="checkbox" class="checkboxes"><span></span></label>'
                    }
                },
                {
                    data: 'Name',
                    title: zw.page.strings.name,
                },
                {
                    data: 'Url',
                    title: zw.page.strings.address,
                    //render: function (data, type, full, meta) {
                    //    var labelClass = data ? 'label-info' : 'label-danger';
                    //    var labelText = data ? zw.strings.yes : zw.strings.no;
                    //    return "<span class='label label-md {0}'>{1}</span>"
                    //        .replace('{0}', labelClass)
                    //        .replace('{1}', labelText);
                    //}
                }
            ]
        });
    },

    onAddFriendClick: function (e) {
        var form = $('#linksForm');
        var validator = form.validate();
        validator.form();
        if (!validator.valid()) return false;

        var grid = $('.dataTable').DataTable();
        $.ajax('/User/BlogLink/Add', {
            type: 'POST',
            data: form.serialize(),
            success: function (response) {
                $('#Link_Name').val('');
                $('#Link_Url').val('');
                grid.ajax.reload();
                zw.ui.showMessage(zw.strings.saved);
            },
        });
    },

    deletePosts: function (e) {
        var grid = $('.dataTable').DataTable();
        var selectedRows = [], selectedRowsIds = [];
        $('tbody > tr > td:nth-child(1) input[type="checkbox"]:checked').each(function () {
            var row = $(this).parent().parent().parent();
            selectedRows.push(row);
            selectedRowsIds.push(grid.row(row).data().Id);
        });

        $.ajax('/User/BlogLink/Delete', {
            type: 'POST',
            data: { ids: selectedRowsIds },
            success: function (response) {
                grid.rows(selectedRows).remove().draw(false);
                zw.ui.showMessage(zw.page.strings.selectedRowsDeleted);
            },
        });
    }

}
