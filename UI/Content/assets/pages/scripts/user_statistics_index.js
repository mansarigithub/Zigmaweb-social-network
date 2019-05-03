$(function () {
    zw.page.initialize();
});

zw.page = {
    strings: {

    },

    initialize: function () {
        zw.ui.charts.buildChart({
            dataUrl: "/User/Statistics/GetAuthorTotalArticlesVisitsChartData",
            chartDiv: "total-article-visits-chart-div",
            theme: "none",
            valueField: 'Visits',
            categoryField: 'Date'
        });

        //zw.ui.charts.buildChart({
        //    dataUrl: "/User/Statistics/GetUserProfileVisitsChartData",
        //    chartDiv: "total-profile-visits-chart-div",
        //    theme: "none",
        //    valueField: 'Visits',
        //    categoryField: 'Date'
        //});
    }
};

