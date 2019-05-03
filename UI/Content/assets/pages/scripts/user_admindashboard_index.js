$(function () {
    zw.page.initialize();
});

zw.page = {
    strings: {

    },

    initialize: function () {
        zw.ui.charts.buildChart({
            dataUrl: "/AdminDashboard/GetSiteVisitChartData",
            chartDiv: "site-visits-chart-div",
            theme: "none",
            valueField: 'Visits',
            categoryField: 'Date'
        });
        zw.ui.charts.buildChart({
            dataUrl: "/AdminDashboard/GetUsersRegistrationStatisticChartData",
            chartDiv: "site-user-registration-chart-div",
            theme: "none",
            valueField: 'Visits',
            categoryField: 'Date'
        });
        zw.ui.charts.buildChart({
            dataUrl: "/AdminDashboard/GetArticlePublishStatisticChartData",
            chartDiv: "site-article-publish-chart-div",
            theme: "none",
            valueField: 'Visits',
            categoryField: 'Date'
        });
        zw.ui.charts.buildChart({
            dataUrl: "/AdminDashboard/GetBlogPostPublishStatisticChartData",
            chartDiv: "site-blogpost-publish-chart-div",
            theme: "none",
            valueField: 'Visits',
            categoryField: 'Date'
        });
    }

};

