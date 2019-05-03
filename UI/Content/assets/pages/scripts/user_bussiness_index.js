$(function () {
    zw.page.initialize();
})

zw.page = {
    strings: {
    },

    initialize: function () {
        this.IntroduceTab.initialize();
    },

    IntroduceTab: {
        initialize: function () {
            this.setSubmitHandler();
        },

        setSubmitHandler: function () {
            var form = $('#introduceForm');
            form.submit(function (e) {
                e.preventDefault();
                if (!form.validate().valid()) return false;

                $.ajax('/user/business/updatebusinessintroduce', {
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
}
