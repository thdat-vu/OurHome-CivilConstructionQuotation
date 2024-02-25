
$(window).on("load", function () {
    var yearList;
    $.ajax({
        url: "/Manager/Dashboard/GetYearList",
        type: "GET",
        dataType: "json",
        success: function (response) {
            yearList = response.data;
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


$('#yearSelect').on("change", function(){
    console.log("Bo m da change");
    handleDrawChart(this.value)
});
