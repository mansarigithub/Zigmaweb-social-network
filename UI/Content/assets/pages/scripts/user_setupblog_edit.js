$(function () {
    zw.page.initialize();
})

zw.page = {
    strings: {
        
    },

    initialize: function () {
        zw.page.registerFormSubmitHandler();
    },

    registerFormSubmitHandler: function () {
        var form = $('#blogForm');
        form.submit(function (e) {
            e.preventDefault();
            if (!form.validate().valid()) return false;

            $.ajax('/user/setupblog/edit', {
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
}
