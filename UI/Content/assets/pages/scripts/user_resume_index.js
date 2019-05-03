$(function () {
    zw.page.initialize();
})

zw.page = {
    strings: {
        organizationName: 'سازمان',
        educationalOrganizationName: 'موسسه آموزشی',
        UniversityFieldName: 'رشته تحصیلی',
        jobName: 'عنوان شغلی',
        EducationGrade: 'مقطع',
        startYear: 'شروع',
        endyear: 'پایان',
    },

    initialize: function () {
        this.educationalResumeTab.initialize();
        this.jobResumeTab.initialize();
    },

    educationalResumeTab: {
        initialize: function () {
            $('#addEducationalResumeButton').click(this.onAddEducationalResumeClick);
            $('#cancelAddEducationalResumeButton').click(this.onCancelAddEducationalResumeClick);
            this.initialGrid();
            this.initialOrgNameAutoComplete();
            this.initialUniFieldNameAutoComplete();
            this.registerFormSubmitHandler();
        },

        onAddEducationalResumeClick: function () {
            $('#educationalResumeForm').fadeIn();
        },

        onCancelAddEducationalResumeClick: function () {
            zw.page.educationalResumeTab.hideForm();
            zw.page.educationalResumeTab.resetForm();
        },

        registerFormSubmitHandler: function () {
            var form = $('#educationalResumeForm');
            var submitButton = $('button[type=submit]');
            form.submit(function (e) {
                e.preventDefault();
                if (!$(e.target).validate().valid()) {
                    return false;
                }

                $.ajax('/User/Resume/AddEducationalResume', {
                    method: 'POST',
                    data: form.serialize(),
                    beforeSend: function () {
                        submitButton.addClass('disabled');
                    },
                    success: function (response) {
                        $('#educationalresumeGrid').DataTable().ajax.reload();
                        zw.page.educationalResumeTab.resetForm();
                        zw.ui.showMessage('', zw.strings.saved, 'info');
                    },
                    complete: function (response) {
                        submitButton.removeClass('disabled');
                    },
                    error: function (response) {
                    }
                });

            });
        },

        hideForm: function () {
            var form = $('#educationalResumeForm');
            form.fadeOut();
        },

        resetForm: function () {
            var form = $('#educationalResumeForm');
            form.validate().resetForm();
            form[0].reset();
        },

        editResume: function (e) {
        },

        deleteResume: function (e) {
            var button = $(e.target);
            $.ajax('/User/Resume/DeleteEducationalResume', {
                method: 'POST',
                data: {
                    id: button.attr('data-id'),
                },
                beforeSend: function () {
                    button.addClass('disabled');
                },
                success: function (response) {
                    $('#educationalresumeGrid').DataTable().ajax.reload();
                },
                complete: function (response) {
                },
                error: function (response) {
                }
            });
        },

        initialOrgNameAutoComplete: function () {
            var orgNameTextBox = $("#EducationalResume_OrganizationName");
            orgNameTextBox.easyAutocomplete({
                url: function (phrase) {
                    return "/User/Resume/SuggestOrganizationNameForEducationalResume/" + phrase;
                },
                listLocation: "items",
                matchResponseProperty: "phrase",
                getValue: "Name",
                list: {
                    onChooseEvent: function () {
                        $("#educationalResumeForm").validate().element("#EducationalResume_OrganizationName");
                    }
                }
            });
        },

        initialUniFieldNameAutoComplete: function () {
            var fieldNameTextBox = $("#EducationalResume_UniversityFieldName");
            fieldNameTextBox.easyAutocomplete({
                url: function (phrase) {
                    return "/User/Resume/SuggestUniversityFieldName/" + phrase;
                },
                listLocation: "items",
                matchResponseProperty: "phrase",
                getValue: "Name",
                list: {
                    onChooseEvent: function () {
                        $("#educationalResumeForm").validate().element("#EducationalResume_UniversityFieldName");
                    }
                }
            });
        },

        initialGrid: function () {
            $('#educationalresumeGrid').DataTable({
                paging: false,
                searching: false,
                ordering: false,
                info: false,
                //autowidth: true,
                language: {
                    url: '/content/assets/global/plugins/datatables/localization/persian.js'
                },
                ajax: {
                    url: '/User/Resume/ReadEducationalResume',
                    type: 'GET'
                },
                columns: [
                    {
                        data: 'OrganizationName',
                        title: zw.page.strings.educationalOrganizationName,
                        width: '30%'
                    },
                    {
                        data: 'UniversityFieldName',
                        title: zw.page.strings.UniversityFieldName,
                        width: '30%'
                    },
                    {
                        data: 'EducationGradeString',
                        title: zw.page.strings.EducationGrade,
                    },
                    {
                        data: 'StartYear',
                        title: zw.page.strings.startYear
                    },
                    {
                        data: 'EndYear',
                        title: zw.page.strings.endyear
                    },
                    {
                        data: 'Id',
                        title: ' ',
                        render: function (data, type, full, meta) {
                            return ('<button class="btn btn-sm btn-outline grey-salsa hidden" data-id="{0}" onclick="zw.page.educationalResumeTab.editResume(event)">ویرایش</button>' +
                                    '<button class="btn btn-sm btn-outline grey-salsa" data-id="{1}" onclick="zw.page.educationalResumeTab.deleteResume(event)">حذف</button>')
                            .replace('{0}', full.Id)
                            .replace('{1}', full.Id);
                        }
                    },
                ]
            });
        },
    },

    jobResumeTab: {
        initialize: function () {
            $('#addJobResumeButton').click(this.onAddJobResumeClick);
            $('#cancelAddJobResumeButton').click(this.onCancelAddJobResumeClick);
            this.initialGrid();
            this.initialOrgNameAutoComplete();
            this.initialJobNameAutoComplete();
            this.registerFormSubmitHandler();
        },

        onAddJobResumeClick: function () {
            $('#jobResumeForm').fadeIn();
        },

        onCancelAddJobResumeClick: function () {
            zw.page.jobResumeTab.hideForm();
            zw.page.jobTab.resetForm();
        },

        registerFormSubmitHandler: function () {
            var form = $('#jobResumeForm');
            var submitButton = $('button[type=submit]');
            form.submit(function (e) {
                e.preventDefault();
                if (!$(e.target).validate().valid()) {
                    return false;
                }

                $.ajax('/User/Resume/AddJobResume', {
                    method: 'POST',
                    data: form.serialize(),
                    beforeSend: function () {
                        submitButton.addClass('disabled');
                    },
                    success: function (response) {
                        $('#jobResumeGrid').DataTable().ajax.reload();
                        zw.page.jobResumeTab.resetForm();
                        zw.ui.showMessage('', zw.strings.saved, 'info');
                    },
                    complete: function (response) {
                        submitButton.removeClass('disabled');
                    },
                    error: function (response) {
                    }
                });

            });
        },

        hideForm: function () {
            var form = $('#jobResumeForm');
            form.fadeOut();
        },

        resetForm: function () {
            var form = $('#jobResumeForm');
            form.validate().resetForm();
            form[0].reset();
        },

        editResume: function (e) {
        },

        deleteResume: function (e) {
            var button = $(e.target);
            $.ajax('/User/Resume/DeleteJobResume', {
                method: 'POST',
                data: {
                    id: button.attr('data-id'),
                },
                beforeSend: function () {
                    button.addClass('disabled');
                },
                success: function (response) {
                    $('#jobResumeGrid').DataTable().ajax.reload();
                },
                complete: function (response) {
                },
                error: function (response) {
                }
            });
        },

        initialOrgNameAutoComplete: function () {
            var orgNameTextBox = $("#JobResume_OrganizationName");
            orgNameTextBox.easyAutocomplete({
                url: function (phrase) {
                    return "/User/Resume/SuggestOrganizationNameForJobResume/" + phrase;
                },
                listLocation: "items",
                matchResponseProperty: "phrase",
                getValue: "Name",
                list: {
                    onChooseEvent: function () {
                        $("#jobResumeForm").validate().element("#JobResume_OrganizationName");
                    }
                }
            });
        },

        initialJobNameAutoComplete: function () {
            var jobNameTextBox = $("#JobResume_JobName");
            jobNameTextBox.easyAutocomplete({
                url: function (phrase) {
                    return "/User/Resume/SuggestJobName/" + phrase;
                },
                listLocation: "items",
                matchResponseProperty: "phrase",
                getValue: "Name",
                list: {
                    onChooseEvent: function () {
                        $("#jobResumeForm").validate().element("#JobResume_JobName");
                    }
                }
            });
        },

        initialGrid: function () {
            $('#jobResumeGrid').DataTable({
                paging: false,
                searching: false,
                ordering: false,
                info: false,
                //autowidth: true,
                language: {
                    url: '/content/assets/global/plugins/datatables/localization/persian.js'
                },
                ajax: {
                    url: '/User/Resume/ReadJobResume',
                    type: 'GET'
                },
                columns: [
                    {
                        data: 'OrganizationName',
                        title: zw.page.strings.organizationName,
                        width: '30%'
                    },
                    {
                        data: 'JobName',
                        title: zw.page.strings.jobName,
                        width: '30%'
                    },
                    {
                        data: 'StartYear',
                        title: zw.page.strings.startYear
                    },
                    {
                        data: 'EndYear',
                        title: zw.page.strings.endyear
                    },
                    {
                        data: 'Id',
                        title: ' ',
                        render: function (data, type, full, meta) {
                            return ('<button class="btn btn-sm btn-outline grey-salsa hidden" data-id="{0}" onclick="zw.page.jobResumeTab.editResume(event)">ویرایش</button>' +
                                    '<button class="btn btn-sm btn-outline grey-salsa" data-id="{1}" onclick="zw.page.jobResumeTab.deleteResume(event)">حذف</button>')
                            .replace('{0}', full.Id)
                            .replace('{1}', full.Id);
                        }
                    },
                ]
            });
        },
    },
}
