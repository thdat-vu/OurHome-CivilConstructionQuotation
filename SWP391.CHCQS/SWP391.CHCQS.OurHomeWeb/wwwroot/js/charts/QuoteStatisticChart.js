var monthlyUrl = "/Manager/Dashboard/GetQuoteSummaryFilterByMonthAndYear?year=";
//var DayUrl = "/Manager/Dashboard/GetQuoteSummaryFilterByDay";
var url = monthlyUrl;

// Set new default font family and font color to mimic Bootstrap's default styling
Chart.defaults.global.defaultFontFamily = 'Nunito', '-apple-system,system-ui,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,sans-serif';
Chart.defaults.global.defaultFontColor = '#858796';
//tạo chart
var ctx = document.getElementById("myQuoteBarChart");
var myQuoteBarChart = new Chart(ctx, {
    type: 'bar',
    data: {
        labels: [],
        datasets: [
            {
                label: "Request",
                backgroundColor: "#0165b9",
                hoverBackgroundColor: "rgb(1,101,185, 0.5)",
                borderColor: "#000000 ",
                data: [],
            },
            {
                label: "Quotation",
                backgroundColor: "#02afae",
                hoverBackgroundColor: "rgb(2,175,174, 0.5)",
                borderColor: "#000000 ",
                data: [],
            },
            {
                label: "Cancled",
                backgroundColor: "#612697",
                hoverBackgroundColor: "rgb(97,38,151, 0.5)",
                borderColor: "#000000 ",
                data: [],
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
            myQuoteBarChart.data.labels = timLines;

            myQuoteBarChart.data.datasets[0].data = requestStatisticData;
            myQuoteBarChart.data.datasets[1].data = quoteStatisticData;
            myQuoteBarChart.data.datasets[2].data = cancledRequestStatisticData;
            myQuoteBarChart.update();
            
        }
    });
}


$(window).on("load", function () {

    $.ajax({
        url: "/Manager/Dashboard/GetYearList",
        type: "GET",
        dataType: "json",
        success: function (response) {
            var yearList = response.data;
            //console.log(yearList[0]);
            for (var i = 0; i < yearList.length; i++) {

                // Tạo một phần tử option mới cho năm
                var yearOption = document.createElement('option');
                yearOption.value = yearList[i];
                yearOption.text = yearList[i];

                //khi chạy lần cuối thì sẽ là selected
                if (i == yearList.length - 1) {
                    yearOption.selected = true;
                }

                // Thêm option vào thẻ select cho năm
                var yearSelect = document.getElementById('yearSelect');
                yearSelect.appendChild(yearOption);
            }
            var year = document.getElementById('yearSelect').value;
            console.log(year);
            handleDrawChart(year);

        }
    });
});


$('#yearSelect').on("change", function () {
    year = this.value;
    handleDrawChart(year);
});



