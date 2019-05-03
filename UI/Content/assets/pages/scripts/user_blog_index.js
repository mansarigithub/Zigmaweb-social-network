$(function () {
    zw.page.initialize();
});

zw.page = {
    strings: {
        enterContent: 'متن اصلی خالی است',
        postsTrashed: 'پست های انتخاب شده به زباله‌دان منتقل شدند',
    },

    initialize: function () {
        this.initialContentsGrid();
        $('.delete-posts').click(this.deletePosts);
    },

    initialContentsGrid: function () {
        $('.contents-grid').DataTable({
            language: {
                url: '/content/assets/global/plugins/datatables/localization/persian.js'
            },
            ajax: {
                url: '/user/blog/readuserposts',
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
                    data: 'Title',
                    title: zw.strings.title,
                    width: '50%'
                },
                {
                    data: 'CommentsCount',
                    title: zw.strings.comment,
                    render: function (data, type, full, meta) {
                        return "<span class='label label-info number-fa'>{0}</span>"
                            .replace('{0}', data);
                    }
                },
                {
                    data: 'Published',
                    title: zw.strings.published,
                    render: function (data, type, full, meta) {
                        var labelClass = data ? 'label-info' : 'label-danger';
                        var labelText = data ? zw.strings.yes : zw.strings.no;
                        return "<span class='label label-md {0}'>{1}</span>"
                            .replace('{0}', labelClass)
                            .replace('{1}', labelText);
                    }
                },
                {
                    data: 'PublishDateString',
                    title: zw.strings.publishDate
                },
                {
                    data: 'Id',
                    title: '',
                    render: function (data, type, full, meta) {
                        return ('<div><a  target="_blank" href="{0}" class="btn btn-sm btn-outline grey-salsa">' +
                         '<i class="fa fa-search"></i> {1}' +
                         '</a>').replace('{0}', '/articles/' + full.Id).replace('{1}', zw.strings.view) +

                         ('<a href="{0}" class="btn btn-sm blue">' +
                         '<i class="fa fa-pencil"></i> {1}' +
                         '</a>').replace('{0}', '/user/blog/edit?id=' + full.Id).replace('{1}', zw.strings.edit);// +

                    }
                },
            ]
        });
    },

    deletePosts: function (e) {
        var grid = $('.contents-grid').DataTable();
        var selectedRows = [], selectedRowsIds = [];
        $('tbody > tr > td:nth-child(1) input[type="checkbox"]:checked').each(function () {
            var row = $(this).parent().parent().parent();
            selectedRows.push(row);
            selectedRowsIds.push(grid.row(row).data().Id);
        });

        $.ajax('/user/blog/delete', {
            type: 'POST',
            data: { ids: selectedRowsIds },
            success: function (response) {
                grid.rows(selectedRows).remove().draw(false);
                zw.ui.showMessage(zw.page.strings.postsTrashed, '');
            },
        });
    }
}
