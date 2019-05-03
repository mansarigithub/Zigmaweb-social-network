$(function () {
    zw.page.initialize();
})

zw.page = {
    mediaBrowser: null,
    strings: {
        mediaDeleted: 'تصاویر انتخاب شده حذف شدند',
        areYouSure: 'آیا از حذف آیتم انتخاب شده اطمینان دارید؟ این فایل‌ به طور کامل حذف خواهد شد و قابل بازیابی نخواهد بود'
    },

    initialize: function () {
        this.initialMediaBrowser();
        $('.delete-comments').click(this.deleteComments);
    },

    initialMediaBrowser: function () {
        this.mediaBrowser = new ZwMediaBrowser({
            multiSelect: false,
        });
        this.mediaBrowser.load();
    },

    deleteComments: function (e) {
        var selectedItems = zw.page.mediaBrowser.selectedItems();
        if (selectedItems.length == 0) {
            zw.ui.showMessage(zw.strings.selectAnItem, '', 'warning');
            return;
        }
        if (!confirm(zw.page.strings.areYouSure)) return;
        zw.page.mediaBrowser.deleteMedia(selectedItems[0].Id)
            .then(function () {
                zw.ui.showMessage(zw.page.strings.mediaDeleted, '');
            });
    }
}
