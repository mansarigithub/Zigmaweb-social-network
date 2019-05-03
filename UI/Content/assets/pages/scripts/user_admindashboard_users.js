$(function() {
    zw.page.initialize();
});

zw.page = {
    strings: {
        isApproved: 'تایید عضویت',
        isLockedOut: 'مسدود است',
        registerDate: 'تاریخ عضویت',
        lastLoginDateString: 'آخرین ورود',
        selectedUsersBlocked: 'کاربران انتخاب شده بلاک شدند',
        selectedUsersUnblocked: 'کاربران انتخاب شده از بلاک خارج شدند'
    },

    initialize: function () {
        this.initialContentsGrid();
        $('#block-users').click(this.blockUsers);
        $('#unblock-users').click(this.unblockUsers);
    },

    initialContentsGrid: function () {
        $('.contents-grid').DataTable({
            language: {
                url: '/content/assets/global/plugins/datatables/localization/persian.js'
            },
            ajax: {
                url: '/User/AdminDashboard/GetAllUsers',
                type: 'GET'
            },
            columns: [
                {
                    data: 'Id',
                    render: function (data, type, full, meta) {
                        return '<label class="mt-checkbox mt-checkbox-single mt-checkbox-outline"><input type="checkbox" class="checkboxes"><span></span></label>'
                    }
                },
                {
                    data: 'FullName',
                    title: zw.strings.name,
                    width: '25%'
                },
                {
                    data: 'Email',
                    title: zw.strings.email
                },
                {
                    data: 'IsApproved',
                    title: zw.page.strings.isApproved,
                    render: function (data, type, full, meta) {
                        var labelClass = data ? 'label-info' : 'label-danger';
                        var labelText = data ? zw.strings.yes : zw.strings.no;
                        return "<span class='label label-md {0}'>{1}</span>"
                            .replace('{0}', labelClass)
                            .replace('{1}', labelText);
                    }
                },
                 {
                     data: 'IsLockedOut',
                     title: zw.page.strings.isLockedOut,
                     render: function (data, type, full, meta) {
                         var labelClass = data ? 'label-info' : 'label-danger';
                         var labelText = data ? zw.strings.yes : zw.strings.no;
                         return "<span class='label label-md {0}'>{1}</span>"
                             .replace('{0}', labelClass)
                             .replace('{1}', labelText);
                     }
                 },
                { data: 'RegisterDateString', title: zw.page.strings.registerDate },
                { data: 'LastLoginDateString', title: zw.page.strings.lastLoginDateString },
                {
                    data: 'Id',
                    title: '',
                    render: function (data, type, full, meta) {
                        return ('<div><a  target="_blank" href="{0}" class="btn btn-sm btn-outline grey-salsa">' +
                         '<i class="fa fa-search"></i> {1}' +
                         '</a>').replace('{0}', '/articles/' + full.Id).replace('{1}', zw.strings.view) +

                         ('<a href="{0}" class="btn btn-sm blue">' +
                         '<i class="fa fa-pencil"></i> {1}' +
                         '</a>').replace('{0}', '/user/article/edit/' + full.Id).replace('{1}', zw.strings.edit);// +

                    }
                }
            ]
        });
    },

    blockUsers: function (e) {
        var grid = $('.contents-grid').DataTable();
        var selectedRows = [], selectedRowsIds = [];
        $('tbody > tr > td:nth-child(1) input[type="checkbox"]:checked').each(function () {
            var row = $(this).parent().parent().parent();
            selectedRows.push(row);
            selectedRowsIds.push(grid.row(row).data().Id);
        });

        $.ajax('/user/admindashboard/blockUsers', {
            type: 'POST',
            data: { ids: selectedRowsIds },
            success: function (response) {
           //     zw.ui.showMessage(zw.page.strings.selectedUsersBlocked, '');
                $('.contents-grid').DataTable().ajax.reload();
            }
        });
    },
    unblockUsers: function (e) {
        var grid = $('.contents-grid').DataTable();
        var selectedRows = [], selectedRowsIds = [];
        $('tbody > tr > td:nth-child(1) input[type="checkbox"]:checked').each(function () {
            var row = $(this).parent().parent().parent();
            selectedRows.push(row);
            selectedRowsIds.push(grid.row(row).data().Id);
        });

        $.ajax('/user/admindashboard/unblockUsers', {
            type: 'POST',
            data: { ids: selectedRowsIds },
            success: function (response) {
            $('.contents-grid').DataTable().ajax.reload();
            //    zw.ui.showMessage(zw.page.strings.selectedUsersUnblocked, '');
            }
        });
    }
}
