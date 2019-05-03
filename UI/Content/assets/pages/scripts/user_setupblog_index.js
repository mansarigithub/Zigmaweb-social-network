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

            $.ajax('/user/setupblog/create', {
                method: 'POST',
                data: form.serialize(),
                success: function (response) {
                    window.location = "/user/setupblog/created";
                },
                error: function (response) {
                }
            });
        });
    },
}
