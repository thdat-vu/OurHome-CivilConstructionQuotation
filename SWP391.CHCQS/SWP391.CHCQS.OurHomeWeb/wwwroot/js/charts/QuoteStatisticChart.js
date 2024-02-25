// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = 'Nunito', '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#858796';



var monthlyUrl = "/Manager/Dashboard/GetQuoteSummaryFilterByMonthAndYear?year=";
//var DayUrl = "/Manager/Dashboard/GetQuoteSummaryFilterByDay";
var url = monthlyUrl;

function handleDrawChart(year) {
    $.ajax({
        url: url + year,
        type: "GET",
        dataType: "json",
        success: function (data) {
            //lấy data dc trả về từ url và chuyển từng thuộc tính về chung 1 mang - Gom nhóm 
            //Lấy request
            var requestStatisticData = data.quoteStatistic.map(function (index) {
                return index.request;
            });
            //lấy custom quotation 
            var quoteStatisticData = data.quoteStatistic.map(function (index) {
                return index.customQuotation;
            });
            //lấy cancled request
            var cancledRequestStatisticData = data.quoteStatistic.map(function (index) {
                return index.cancledRequest;
            });

            //lấy ra danh sách timeline
            var timLines = data.quoteStatistic.map(function (index) {
                return index.timeline;
            });
            //tạo chart
            var ctx = document.getElementById("myQuoteBarChart");
            var myQuoteBarChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: timLines,
                    datasets: [
                        {
                            label: "Request",
                            backgroundColor: "#0165b9",
                            hoverBackgroundColor: "rgb(1,101,185, 0.5)",
                            borderColor: "#000000 ",
                            data: requestStatisticData,
                        },
                        {
                            label: "Quotation",
                            backgroundColor: "#02afae",
                            hoverBackgroundColor: "rgb(2,175,174, 0.5)",
                            borderColor: "#000000 ",
                            data: quoteStatisticData,
                        },
                        {
                            label: "Cancled",
                            backgroundColor: "#612697",
                            hoverBackgroundColor: "rgb(97,38,151, 0.5)",
                            borderColor: "#000000 ",
                            data: cancledRequestStatisticData,
                        }
                    ],
                },
                options: {
                    maintainAspectRatio: false,
                    layout: {
                        padding: {
                            left: 10,
                            right: 25,
                            top: 25,
                            bottom: 0
                        }
                    },
                    scales: {
                        xAxes: [{
                            gridLines: {
                                display: false,
                            },
                        }],
                        yAxes: [{
                            ticks: {
                                min: 0,
                                max: 50,
                                maxTicksLimit: 10,
                                padding: 10,
                            },
                        }],
                    },
                    legend: {
                        display: true
                    },
                    tooltips: {
                        titleMarginBottom: 10,
                        titleFontColor: '#6e707e',
                        titleFontSize: 14,
                        backgroundColor: "rgb(255,255,255)",
                        bodyFontColor: "#858796",
                        borderColor: '#dddfeb',
                        borderWidth: 1,
                        xPadding: 15,
                        yPadding: 15,
                        displayColors: true,
                        caretPadding: 10,
                    },
                }
            });
            console.log("bo m da ve xong");
        }
    });
}


