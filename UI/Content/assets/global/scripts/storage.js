function ZwStorage(options) {
    var fileCheckCounter;
    var working = false;

    var init = function () {
        $('#dropzoneArea').dropzone({
            url: "/user/mediaservice/savemedia",
            acceptedFiles: 'image/*',
            parallelUploads: 1,
            maxFilesize: 50,
            previewTemplate: document.getElementById('dzPreviewTemplate').innerHTML,
            init: function () {
                this.on("maxfilesexceeded", function (file) {
                    this.removeFile(file);
                });
                this.on("addedfile", function (file) {
                    if (working) {
                        this.removeFile(file);
                        return;
                    }
                    $('#dropzoneArea .help').hide();
                });
                this.on("sending", function (file, xhr, formData) {
                    working = true;
                    $('.dropzone-msg').show().text('در حال ارسال ...');
                });
                this.on("success", function (file, response) {
                    $('.dropzone-msg').show().text('در حال پردازش ...');
                    startChecking(response);
                });
                this.on("error", function (file, errorMessage, xhr) {
                    working = false;
                    $('.dropzone-msg').hide();
                });
            }
        });
    }

    var startChecking = function (response) {
        var fileCheckCounter = 0;
        var timer = setInterval(function () {
            $.ajax({
                url: '/user/mediaservice/fileexist',
                data: { url: response.Url },
                success: function (data) {
                    if (!data) {
                        fileCheckCounter++;
                        if (fileCheckCounter > 20)
                            stopChecking(timer);
                        return
                    };
                    if (typeof (options.fileUploaded) == 'function') {
                        options.fileUploaded(response);
                    }
                    stopChecking(timer);
                }
            });
        }, 2000);
    }

    var stopChecking = function (timerId) {
        clearInterval(timerId);
        $('.dropzone-msg').hide();
        working = false;
    }

    init();
}

function ZwMediaBrowser(options) {
    var _mediaList = [];
    this.load = function () {
        getMediaList().then(function (response) {
            _mediaList = response.data;
            createMedias(_mediaList);
        });
    }

    this.selectedItems = function () {
        var items = $('.media-browser .item.selected');
        var arr = [];
        for (var i = 0; i < items.length; i++) {
            var itemId = $(items[i]).attr('data-mediaId');
            arr.push(_mediaList.find(function (m) { return m.Id == itemId }));
        }
        return arr;
    }

    this.reload = function () {
        reset();
        this.load();
    }

    this.deleteMedia = function (mediaId) {
        return $.ajax('/user/mediaservice/deletemedia', {
            type: 'POST',
            data: { id: mediaId },
            success: function () {
                $('.media-browser .item[data-mediaId=' + mediaId + ']').fadeOut().remove();
                if ($('.media-browser .item').length == 0) {
                    showEmptyListMsg(true);
                }
            }
        });
    }

    var reset = function () {
        $('.media-browser .item').remove();
        _mediaList = [];
    }

    var showEmptyListMsg = function (show) {
        var elm = $('.media-browser .empty-msg');
        if (show)
            elm.fadeIn();
        else
            elm.fadeOut();
    }

    var getMediaList = function (callback) {
        return $.ajax({
            url: '/user/mediaservice/readusermedia',
        });
    }

    var createMedias = function (medias) {
        if (medias.length == 0) {
            showEmptyListMsg(true);
            return;
        }

        var container = $('.media-browser');
        var itemTemplate = $('.item-template', container);

        for (var i = 0; i < medias.length; i++) {
            var newItem = itemTemplate.clone(false)
                .removeClass('item-template').addClass('item')
                .removeAttr("style").attr('data-mediaId', medias[i].Id)
                .click(medias[0], onItemClick);
            $('img', newItem).attr('src', medias[i].ThumbnailUrl);
            container.append(newItem);
        }
    }

    var onItemClick = function (e) {
        var item = $(e.currentTarget);
        var c = 'selected';
        if (typeof (options) == 'undefined' || !options.multiSelect)
            $('.media-browser .item').not(item).removeClass(c);
        if (item.hasClass(c))
            item.removeClass(c);
        else
            item.addClass(c);
    }

    var init = function () {
        options = typeof (options) == 'undefined' ? {} : options;
        if (options.maxHeight)
            $('.media-browser')
                .css('max-height', options.maxHeight)
                .css('overflow-y', 'auto');
    }
    init();
    return this;
}

function ZwMediaBrowserPopup(options) {
    var mediaBrowser;
    var options = typeof (options) == 'object' ? options : {};
    var modalDiv = $('#zwMediaBrowserModal');

    this.open = function () {
        if (!mediaBrowser) {
            mediaBrowser = new ZwMediaBrowser({
                multiSelect: false,
                maxHeight: options.maxHeight
            });
        }
        mediaBrowser.reload();
        modalDiv.modal({});
    }

    var onOkClick = function () {
        if (typeof (options.onOkClick) !== 'function') {
            modalDiv.modal('hide');
            return;
        }

        if (options.onOkClick(mediaBrowser.selectedItems()) === false)
            return;
        modalDiv.modal('hide');
    }

    function init() {
        $('#zwMediaBrowserModal .ok-button').click(onOkClick);
    };
    init();
    return this;
}
