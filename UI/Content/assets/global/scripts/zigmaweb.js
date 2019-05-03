$(document).ajaxComplete(function (event, xhr, settings) {
    if (xhr.status == 200) return;
    var r = xhr.responseJSON;
    if (typeof (r) != 'undefined' && r.Type == zw.enums.processResultType.error) {
        zw.ui.showMessage(r.Message, '', 'error');
    }
});

$(function () {
    zw.app.initialize();
})

var zw = {
    page: {},
    strings: {
        saved: 'ذخیره شد',
        saving: 'در حال ذخیره سازی',
        error: 'خطا',
        yes: 'بله',
        no: 'خیر',
        title: 'عنوان',
        published: 'منتشر شده',
        publishDate: 'تاریخ انتشار',
        createDate: 'تاریخ ایجاد',
        comment: 'دیدگاه',
        view: 'مشاهده',
        'delete': 'حذف',
        edit: 'ویرایش',
        sender: 'ارسال کننده',
        authorName: 'نویسنده',
        visitsCount: 'تعداد بازدید',
        name: 'نام',
        email: 'ایمیل',
        selectAnItem: 'لطفا یک مورد را انتخاب کنید'
    },

    enums: {
        processResultType: {
            success: 1,
            error: 2,
            warning: 3,
        },
        pushNotificationType: {
            NewNotification: 1,
            NewMessage: 2,
        }
    },

    app: {
        initialize: function () {
            zw.ui.initialize();
            zw.pushNotification.initialize();
        }
    },

    ui: {
        initialize: function () {
            zw.ui.validation.initialize();
            zw.ui.notification.readNotifications();

            // Persian Number Plugin
            $('.number-fa').persiaNumber();

            // Bootstrap Switch Plugin
            if ($.fn.bootstrapSwitch)
                $('.switch-control').bootstrapSwitch({
                    onText: zw.strings.yes,
                    offText: zw.strings.no
                });

            // Bootstrap Toastr Plugin
            if (typeof toastr !== 'undefined')
                toastr.options = {
                    "closeButton": true,
                    "debug": false,
                    "positionClass": "toast-top-left",
                    "onclick": null,
                    "showDuration": "1000",
                    "hideDuration": "1000",
                    "timeOut": "5000",
                    "extendedTimeOut": "1000",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                }
        },
        localization: {
            toPersianNumber(selector, context) {
                $(selector, context).persiaNumber();
            }
        },
        showMessage: function (message, title, messageType, clearPreveiousMessages) {
            if (typeof(toastr) == 'undefined') return;
            if (clearPreveiousMessages)
                toastr.remove();

            if (messageType == 'error')
                toastr.error(message, title);
            else if (messageType == 'info')
                toastr.info(message, title);
            else if (messageType == 'warning')
                toastr.warning(message, title);
            else
                toastr.success(message, title);
        },

        validation: {
            initialize: function () {
                if (!$.fn.validate) return;

                var forms = $('.validatable-form');
                var error1 = $('.alert-danger', forms);
                var success1 = $('.alert-success', forms);

                forms.each(function (index, element) {
                    var settings = $(element).validate().settings;

                    settings.highlight = function (element) { // hightlight error inputs
                        var formGroup = $(element).closest('.form-group');
                        formGroup.removeClass('has-success').addClass('has-error'); // set error class to the control group
                        $('.help-block:not(.help-block-error)', formGroup).hide();
                    };
                    settings.unhighlight = function (element) { // revert the change done by hightlight
                        var formGroup = $(element).closest('.form-group');
                        formGroup.addClass('has-sucess').removeClass('has-error'); // set error class to the control group
                        $('.help-block:not(.help-block-error)', formGroup).show();
                    };
                    settings.ignore = ':hidden:not(.validatable-field)'
                    //settings.ignore = '.ignore';
                });
            },

            extendValidationRules() {
                jQuery.validator.addMethod("enforcetrue", function (value, element, param) {
                    return element.checked;
                });
                jQuery.validator.unobtrusive.adapters.addBool("enforcetrue");
            }
        },
        notification: {
            unReadNotifications: 0,
            readNotifications: function () {
                $.ajax({
                    url: '/User/NotificationService/ReadNotifications',
                    type: "GET",
                    success: function (data) {
                        for (var i = data.length - 1; i >= 0; i--) {
                            var n = data[i];
                            zw.ui.notification.push(n.Id, n.Title, n.Message, n.CreateDateString, n.IsRead, n.Url, n.ImageUrl);
                        }
                        zw.ui.notification._setMenu(zw.ui.notification.unReadNotifications);
                        zw.ui.notification._correctMenuHeight();

                    },
                    error: function (xhr) {
                    }
                });
            },

            push: function (id, title, message, date, isRead, url, imageUrl) {
                var list = $('#notificationList');
                var newItem = $('.template', list).clone(false);
                newItem.removeClass('template hide');
                newItem.attr('data-notification-id', id);
                $('.from', newItem).text(title);
                $('.message', newItem).text(message);
                $('.time', newItem).text(date).persiaNumber();
                $('img', newItem).attr('src', imageUrl);
                $('a', newItem).attr('href', url).attr('onclick', 'zw.ui.notification._onNotificationClick(event)');
                list.prepend(newItem);
                if (!isRead) {
                    this.unReadNotifications++;
                    this._setMenu(this.unReadNotifications);
                    newItem.addClass('unread');
                }
            },

            setNotificationAsRead: function (notificationId, successCallback) {
                $.ajax({
                    url: '/User/NotificationService/SetNotificationAsRead',
                    type: "Post",
                    data: {
                        id: notificationId,
                    },
                    success: function (data) {
                        successCallback(data);
                    }
                });
            },

            _correctMenuHeight: function () {
                var list = $('#notificationList');
                list.css('height', '').css('max-height', '250px');
                list.parent().css('height', '').css('max-height', '250px');
            },

            _setMenu: function (unReadNotificationsCount) {
                var context = $('#header_notification_bar');
                $('.notification-count', context).show().text(unReadNotificationsCount).persiaNumber();
                var firstH3 = $('.external>h3:eq(0)', context);
                var secondH3 = $('.external>h3:eq(1)', context);
                var link = $('.external>a', context);
                var badge = $('span.badge', context);

                if (unReadNotificationsCount == 0) {
                    firstH3.show();
                    secondH3.hide();
                    link.hide();
                    badge.hide();
                } else {
                    firstH3.hide();
                    secondH3.show();
                    link.show();
                    badge.show();
                }
            },

            _onNotificationClick: function (e) {
                debugger;
                var link = $(e.currentTarget);
                var id = link.parent().attr('data-notification-id');
                this.setNotificationAsRead(id, function (data) {
                    window.location = link.attr('href');
                });
                e.preventDefault();
            },
        },
    },

    pushNotification: {
        initialize: function () {
            hub = $.connection.usersHub;
            hub.client.pushNotification = function (type, data) {
                if (type == zw.enums.pushNotificationType.NewNotification)
                    zw.ui.notification.push(data.Id, data.Title, data.Message, data.CreateDateString, data.IsRead, data.Url, data.ImageUrl);
            }

            $.connection.hub.start().done(function () {

            });
        }
    }
};

(function ($) {
    if ($.fn.validate) {
        zw.ui.validation.extendValidationRules();
    }
}(jQuery));
