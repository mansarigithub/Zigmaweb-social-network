$(function() {
    zw.page.initialize();
});

zw.page = {
    strings: {
        enterContent: 'متن اصلی خالی است',
        articlesTrashed: 'مقاله‌های انتخاب شده به زباله‌دان منتقل شدند',
    },

    initialize: function () {
        this.initialContentsGrid();
        $('.delete-articles').click(this.deleteArticles);
    },

    initialContentsGrid: function () {
        $('.contents-grid').DataTable({
            language: {
                url: '/content/assets/global/plugins/datatables/localization/persian.js'
            },
            ajax: {
                url: '/User/Article/ReadUserContents',
                type: 'GET'
            },
            columns: [
                {
                    data: 'Id',
                    render: function (data, type, full, meta) {
                        return '<label class="mt-checkbox mt-checkbox-single mt-checkbox-outline"><input type="checkbox" class="checkboxes"><span></span></label>'
                    }
                },
                { data: 'Title', title: zw.strings.title, width: '50%' },
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
                { data: 'PublishDateString', title: zw.strings.publishDate },
                {
                    data: 'Id',
                    title: '',
                    render: function (data, type, full, meta) {
                        return ('<div><a  target="_blank" href="{0}" class="btn btn-sm btn-outline grey-salsa">' +
                         '<i class="fa fa-search"></i> {1}' +
                         '</a>').replace('{0}', '/article/' + full.Id).replace('{1}', zw.strings.view) +

                         ('<a href="{0}" class="btn btn-sm blue">' +
                         '<i class="fa fa-pencil"></i> {1}' +
                         '</a>').replace('{0}', '/user/article/edit?id=' + full.Id).replace('{1}', zw.strings.edit);// +
                    }
                },
            ]
        });
    },

    goToEditPage: function (articleId) {
        $('#articleId').val(articleId);
    },

    deleteArticles: function (e) {
        var grid = $('.contents-grid').DataTable();
        var selectedRows = [], selectedRowsIds = [];
        $('tbody > tr > td:nth-child(1) input[type="checkbox"]:checked').each(function () {
            var row = $(this).parent().parent().parent();
            selectedRows.push(row);
            selectedRowsIds.push(grid.row(row).data().Id);
        });

        $.ajax('/user/article/delete', {
            type: 'POST',
            data: { ids: selectedRowsIds },
            success: function (response) {
                grid.rows(selectedRows).remove().draw(false);
                zw.ui.showMessage(zw.page.strings.articlesTrashed, '');
            },
        });
    }
}
