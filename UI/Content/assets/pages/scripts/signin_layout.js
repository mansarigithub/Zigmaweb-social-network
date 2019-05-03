$(function () {
    zw.page.initialize();
});

zw.page = {
    strings: {
    },

    initialize: function () {
        $(".login-bg").backstretch(["/content/assets/pages/img/login/bg1.jpg", "/content/assets/pages/img/login/bg2.jpg", "/content/assets/pages/img/login/bg3.jpg"], { fade: 1e3, duration: 8e3 }), $(".forget-form").hide()
    },
}