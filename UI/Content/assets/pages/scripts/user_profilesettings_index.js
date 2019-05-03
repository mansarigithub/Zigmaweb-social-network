$(function () {
    zw.page.initialize();
})

zw.page = {
    strings: {
        selectCropArea: 'ناحیه مورد نظر را انتخاب کنید',
        uploadingImage: 'تصویر شما در حال ارسال است. لطفا صبر کنید',
        imageUploaded: 'اکنون ناحیه مورد نظر از تصویر را انتخاب کرده و سپس آیکون تایید را کلیک کنید',
        imageSaved: 'تصویر ذخیره شد',
    },

    initialize: function () {
        this.initialGeneralSettingsTab();
        this.ChangeImageTab.initialize();
        this.socialNetworksTab.initialize();
        this.initalChangePasswordTab();
    },

    initialGeneralSettingsTab: function () {
        this.setGeneralSettingsSubmitHandler();
    },

    ChangeImageTab: {
        isMsgShown: false,
        initialize: function () {
            var cropperHeader = new Croppic('croppicArea', {
                uploadUrl: 'profilesettings/saveprofilepicture',
                cropUrl: 'profilesettings/cropprofileimage/',
                //outputUrlId:'userProfileLargeImageUrl',
                doubleZoomControls: false,
                rotateControls: false,
                //loadPicture: '/user/service/getprofileimage/large'
                imgEyecandy: true,
                imgEyecandyOpacity: 0,
                customUploadButtonId: 'chooseImage',
                onBeforeImgUpload: function () {
                    zw.page.ChangeImageTab.isMsgShown = false;
                    $('#currentProfileImage').remove();
                },
                onAfterImgUpload: function () {
                    if (zw.page.ChangeImageTab.isMsgShown) return
                    zw.page.ChangeImageTab.isMsgShown = true;
                    zw.ui.showMessage(zw.page.strings.imageUploaded, zw.page.strings.imageSaved);
                    debugger;
                },
                onAfterImgCrop: function () {
                    var rnd = Math.round(Math.random() * 999999).toString();
                    $('#smallProfileImage').attr('src', '/user/service/getprofileimage/small/' + rnd);
                    $('.profile-userpic img').attr('src', '/user/service/getprofileimage/large/' + rnd);
                },
            });
        }
    },

    socialNetworksTab: {
        initialize: function () {
            var form = $('#socialNetworksForm');
            form.submit(function (e) {
                e.preventDefault();
                if (!form.validate().valid()) return false;

                $.ajax('/user/profilesettings/updatesocialnetworkurls', {
                    method: 'POST',
                    data: form.serialize(),
                    success: function (response) {
                        zw.ui.showMessage(zw.strings.saved);
                    },
                    error: function (response) {
                    }
                });
            });
        },
    },

    initalChangePasswordTab: function () {
        this.setPasswordChangeSubmitHandler();
    },

    setGeneralSettingsSubmitHandler: function () {
        var form = $('#generalSettingsForm');
        form.submit(function (e) {
            e.preventDefault();
            if (!form.validate().valid()) return false;

            $.ajax('/user/profilesettings/updategeneralsettings', {
                method: 'POST',
                data: form.serialize(),
                success: function (response) {
                    zw.ui.showMessage(zw.strings.saved);
                },
                error: function (response) {
                }
            });
        });
    },

    setPasswordChangeSubmitHandler: function () {
        var form = $('#passwordChangeForm');
        form.submit(function (e) {
            e.preventDefault();
            if (!form.validate().valid()) return false;

            $.ajax('/user/profilesettings/updatepassword', {
                method: 'POST',
                data: form.serialize(),
                success: function (response) {
                    $('input', form).val('');
                    zw.ui.showMessage(zw.strings.saved);
                },
                error: function (response) {
                }
            });
        });
    },
}
