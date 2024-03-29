//click vào 1 nút yêu cầu báo giá
var isFirstLoad = true;
var buttonState = 1; //tương đương quotation button
//khác 1: tương đương request button
$(window).on("load", function () {
   
    if (isFirstLoad) {
        loadDataTableAllCustomQuotationManager();
        loadDataTableAllRequest();
        isFirstLoad = false;
        var buttonRequest = document.getElementById("request_button");
        buttonRequest.style.opacity = "0.5";
        buttonRequest.style.pointerEvents = "auto"; // Không thể nhấp vào nút request

        // Nút quotation trở lại bình thường
        var buttonQuotation = document.getElementById("quotation_button");
        buttonQuotation.style.opacity = "1";
        buttonQuotation.style.pointerEvents = "none"; // Có thể nhấp vào nút quotation
        // Hiện bảng quotation lên
        var allCQ = document.getElementById("quoteDiv");
        allCQ.style.display = "block";
        //giấu bảng request 
        var allRF = document.getElementById("requestDiv");
        allRF.style.display = "none";

    }
});

$("#request_button").on("click", function () {
    // Bấm nút thì nút quotation bị mờ đi
    var buttonQuotation = document.getElementById("quotation_button");
    buttonQuotation.style.opacity = "0.5";
    buttonQuotation.style.pointerEvents = "auto"; // Không thể nhấp vào nút quotation

    // Nút request trở lại bình thường
    var buttonRequest = document.getElementById("request_button");
    buttonRequest.style.opacity = "1";
    buttonRequest.style.pointerEvents = "none"; // Có thể nhấp vào nút request

    var allCQ = document.getElementById("quoteDiv");
    allCQ.style.display = "none";
    var allRF = document.getElementById("requestDiv");
    allRF.style.display = "block";

});

$("#quotation_button").on("click", function () {
    // Bấm nút thì request bị mờ đi
    var buttonRequest = document.getElementById("request_button");
    buttonRequest.style.opacity = "0.5";
    buttonRequest.style.pointerEvents = "auto"; // Không thể nhấp vào nút request

    // Nút quotation trở lại bình thường
    var buttonQuotation = document.getElementById("quotation_button");
    buttonQuotation.style.opacity = "1";
    buttonQuotation.style.pointerEvents = "none"; // Có thể nhấp vào nút quotation

    var allCQ = document.getElementById("quoteDiv");
    allCQ.style.display = "block";
    var allRF = document.getElementById("requestDiv");
    allRF.style.display = "none";
});





