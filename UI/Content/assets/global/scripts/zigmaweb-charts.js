zw.ui.charts = {
    buildChart: function (options) {
        $.ajax({
            url: options.dataUrl,
            type: "GET",
            success: function (data) {
                var ndata = data;
                //for (var i = 0; i < data.length; i++) {
                //    ndata.push({
                //        date: data[i][options.categoryField],
                //        visits: data[i][options.valueField]
                //    });
                //}

                var chart = AmCharts.makeChart(options.chartDiv, {
                    "type": "serial",
                    "theme": options.theme,
                    "language": "fa",
                    "marginRight": 80,
                    "autoMarginOffset": 20,
                    "marginTop": 7,
                    "dataProvider": ndata,
                    "startDuration": 1,
                    "startEffect": "easeInSine",
                    "valueAxes": [
                        {
                            "axisAlpha": 1,
                            "dashLength": 1,
                            "position": "left"
                        }
                    ],
                    "mouseWheelZoomEnabled": false,
                    "graphs": [
                        {
                            "id": "g1",
                            "balloonText": "[[value]]",
                            "bullet": "round",
                            "bulletBorderAlpha": 1,
                            "bulletColor": "#FFFFFF",
                            "lineThickness": 3,
                            "hideBulletsCount": 50,
                            "type": "smoothedLine",
                            "title": "red line",
                            "fillAlphas": 0.5,
                            "valueField": options.valueField,
                            "useLineColorForBulletBorder": true,
                            "balloon": {
                                "drop": true
                            }
                        }
                    ],

                    "categoryField": options.categoryField,
                    "categoryAxis": {
                        "parseDates": false,
                        "axisColor": "#DADADA",
                        "dashLength": 1,
                        "minorGridEnabled": true
                    },
                    "export": {
                        "enabled": true
                    }
                });

                chart.addListener("rendered", zw.ui.charts.zoomChart);
                zw.ui.charts.zoomChart(chart, ndata);
            },
            error: function (xhr) {
                alert('error');
            }
        });
    },

    // this method is called when chart is first inited as we listen for "rendered" event
    zoomChart: function (chart, chartData) {
        // different zoom methods can be used - zoomToIndexes, zoomToDates, zoomToCategoryValues
        chart.zoomToIndexes(chartData.length - 40, chartData.length - 1);
    }
}

