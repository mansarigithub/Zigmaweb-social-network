$(function () {
    zw.page.initialize();
})

zw.page = {
    strings: {
        enterContent: 'متن اصلی خالی است',
        operationCanceled: 'عملیات لغو شد',
        fileIsNotPicture: 'فایل انتخاب شده یک تصویر نیست. لطفا یک تصویر انتخاب کنید.',
        filePicked: 'تصویر انتخاب شد. بعد از چند ثانیه فایل انتخاب شده بارگذاری و در مقاله شما درج می شود.',
        contentNotSavedMessage: 'تغییرات شما ذخیره نشده است. آیا این صفحه را ترک می کنید؟',
        uploadFinished: 'آپلود فایل به اتمام رسید',

    },
    mediaPopup: null,

    initialize: function () {
        CKEDITOR.replace('contentText');
        this.initializeTagsInput();
        $('#chooseImageBtn').click(zw.page.onChooseImageBtnClick);
        this.initStorage();
    },

    initStorage: function () {
        var storage = new ZwStorage({
            fileUploaded: function (response) {
                zw.page.InsertImageInEditor(response.Url);
                zw.ui.showMessage('', zw.page.strings.uploadFinished, 'info');
            }
        })
    },

    InsertImageInEditor(imgUrl) {
        var html = '<img src="{0}"/>'.replace('{0}', imgUrl);
        CKEDITOR.instances.contentText.insertHtml(html);
    },

    initializeTagsInput: function () {
        var input = $('#tags');
        input.tagsinput({
            maxChars: 25,
            maxTags: 15,
            trimValue: true,
            allowDuplicates: false,
        });
        $(".bootstrap-tagsinput input[type=text]").css('width', '');

    },

    onSaveClick: function () {
        var pageData = this.getPageData();
        if (!this.validateForm(pageData)) return;

        this.save(
            pageData,
            function (response) {
            },
            function () {
            });
    },

    onPreviewClick: function () {
        var pageData = this.getPageData();
        if (!this.validateForm(pageData)) return;

        this.save(
            pageData,
            function (response) {
                var url = '/article/' + response.Data.ContentId + '/preview';
                window.open(url, '_blank');
            },
            function () {
            });
    },

    save: function (pageData, successCallback, errorCallback) {
        $.ajax('/user/article/save', {
            method: 'POST',
            data: pageData,
            beforeSend: function () {
                $('.save-button, .preview-button').addClass('disabled');
                zw.ui.showMessage('', zw.strings.saving, 'info');
            },
            success: function (response) {
                $('#Content_Id').val(response.Data.ContentId);
                zw.ui.showMessage('', zw.strings.saved, 'info');
                successCallback(response);
            },
            complete: function (response) {
                $('.save-button, .preview-button').removeClass('disabled');
            },
            error: function (response) {
                errorCallback(response);
            }
        });
    },

    validateForm: function (pageData) {
        var validator = $('#contentForm').validate();
        validator.form();
        var isValidForm = validator.valid();
        if (!isValidForm) return false;
        if (validator.valid() && pageData.Content.Text == '') {
            zw.ui.showMessage(zw.page.strings.enterContent, zw.strings.error, 'error', true);
            return false;
        }
        return true;
    },

    getPageData: function () {
        return {
            Content: {
                Title: $('.contentTitle').val(),
                ShortAbstract: $('.contentShortAbstract').val(),
                Text: CKEDITOR.instances.contentText.getData().trim(),
                Published: $('#Content_Published').is(":checked"),
                Id: $('#Content_Id').val()
            },
            Tags: $('#tags').tagsinput('items')
        };
    },

    onChooseImageBtnClick: function (e) {
        if (zw.page.mediaPopup == null)
            zw.page.mediaPopup = new ZwMediaBrowserPopup({
                onOkClick: function (selectedFiles) {
                    if (selectedFiles.length == 0) return false;
                    zw.page.InsertImageInEditor(selectedFiles[0].Url);
                }
            });

        zw.page.mediaPopup.open();
    },
}

window.onbeforeunload = function () {
    var data = zw.page.getPageData();
    if ((data.Content.Title || data.Content.ShortAbstract || data.Content.Text) && !data.Content.Id) {
        return zw.page.strings.contentNotSavedMessage;
    }
}