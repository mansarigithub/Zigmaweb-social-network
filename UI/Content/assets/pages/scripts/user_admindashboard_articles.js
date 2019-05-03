$(function() {
    zw.page.initialize();
});

zw.page = {
    strings: {
        selectedArticlesBlocked: "مقاله‌های انتخاب شده بلاک شدند",
        selectedArticlesUnblocked: "مقاله‌های انتخاب شده از بلاک خارج شدند",
        selectedArticlesHided: "مقاله‌های انتخاب شده پنهان شدند",
        selectedArticlesUnhided: "مقاله‌های انتخاب شده بلاک شدند"
    },

    initialize: function () {
        this.initialContentsGrid();
        $("#block-articles").click(this.blockArticles);
        $("#unblock-articles").click(this.unblockArticles);
        $("#hide-articles").click(this.hideArticles);
        $("#unhide-articles").click(this.unhideArticles);
    },

    initialContentsGrid: function () {
        $('.contents-grid').DataTable({
            language: {
                url: '/content/assets/global/plugins/datatables/localization/persian.js'
            },
            ajax: {
                url: '/User/AdminDashboard/GetAllContents',
                type: 'GET'
            },
            columns: [
                {
                    data: 'Id',
                    render: function (data, type, full, meta) {
                        return '<label class="mt-checkbox mt-checkbox-single mt-checkbox-outline"><input type="checkbox" class="checkboxes"><span></span></label>';
                    }
                },
                {
                    data: 'Title'
                    , title: zw.strings.title
                    , width: '50%'
                },
                {
                    data: 'AuthorFullName'
                    , title: zw.strings.authorName
                    , width: '20%'
                },
                {
                    data: 'VisitsCount',
                    title: zw.strings.visitsCount,
                    render: function (data, type, full, meta) {
                        return "<span class='label label-info'>{0}</span>"
                            .replace('{0}', data);
                    }
                },
                {
                    data: 'CommentsCount',
                    title: zw.strings.comment,
                    render: function (data, type, full, meta) {
                        return "<span class='label label-info'>{0}</span>"
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
                         '</a>').replace('{0}', '/user/article/edit/' + full.Id).replace('{1}', zw.strings.edit);// +

                    }
                },
            ]
        });
    },

    blockArticles: function (e) {
        var grid = $(".contents-grid").DataTable();
        var selectedRows = [], selectedRowsIds = [];
        $("tbody > tr > td:nth-child(1) input[type=\"checkbox\"]:checked").each(function () {
            var row = $(this).parent().parent().parent();
            selectedRows.push(row);
            selectedRowsIds.push(grid.row(row).data().Id);
        });

        $.ajax("/user/admindashboard/blockArticles", {
            type: "POST",
            data: { ids: selectedRowsIds },
            success: function (response) {
                grid.initialContentsGrid();
                zw.ui.showMessage(zw.page.strings.selectedArticlesBlocked, "");
            }
        });
    },
    unblockArticles: function (e) {
        var grid = $(".contents-grid").DataTable();
        var selectedRows = [], selectedRowsIds = [];
        $("tbody > tr > td:nth-child(1) input[type=\"checkbox\"]:checked").each(function () {
            var row = $(this).parent().parent().parent();
            selectedRows.push(row);
            selectedRowsIds.push(grid.row(row).data().Id);
        });

        $.ajax("/user/admindashboard/unblockArticles", {
            type: "POST",
            data: { ids: selectedRowsIds },
            success: function (response) {
                grid.initialContentsGrid();
                zw.ui.showMessage(zw.page.strings.selectedArticlesUnblocked, "");
            }
        });
     },
    hideArticles: function (e) {
         var grid = $(".contents-grid").DataTable();
         var selectedRows = [], selectedRowsIds = [];
         $("tbody > tr > td:nth-child(1) input[type=\"checkbox\"]:checked").each(function () {
             var row = $(this).parent().parent().parent();
             selectedRows.push(row);
             selectedRowsIds.push(grid.row(row).data().Id);
         });

         $.ajax("/user/admindashboard/hideArticles", {
             type: "POST",
             data: { ids: selectedRowsIds },
             success: function (response) {
                 grid.initialContentsGrid();
                 zw.ui.showMessage(zw.page.strings.selectedArticlesHided, "");
             }
         });
     },
    unhideArticles: function (e) {
         var grid = $(".contents-grid").DataTable();
         var selectedRows = [], selectedRowsIds = [];
         $("tbody > tr > td:nth-child(1) input[type=\"checkbox\"]:checked").each(function () {
             var row = $(this).parent().parent().parent();
             selectedRows.push(row);
             selectedRowsIds.push(grid.row(row).data().Id);
         });

         $.ajax("/user/admindashboard/unhideArticles", {
             type: "POST",
             data: { ids: selectedRowsIds },
             success: function (response) {
                 grid.rows(selectedRows).remove().draw(false);
                 zw.ui.showMessage(zw.page.strings.selectedArticlesUnhided, "");
             }
         });
     }

}
