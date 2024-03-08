$(document).ready(function () {
    loadDataTableRequest();
});

//Need an api method return json to use this
function loadDataTableRequest() {
    dataTable = $('#tblQuotation').DataTable({
        "ajax": { url: '/Seller/Quotation/GetAll' },
        "columns": [
            { data: 'id', "width": "5%" },
            {
                data: 'date',
                "render": function (data) {
                    // Chuyển đổi ngày thành chuỗi định dạng dd/MM/yyyy
                    let date = new Date(data);
                    let day = ("0" + date.getDate()).slice(-2);
                    let month = ("0" + (date.getMonth() + 1)).slice(-2);
                    let year = date.getFullYear();
                    return `${day}/${month}/${year}`;
                },
                "width": "15%"
            },
            { data: 'acreage', "width": "15%" },
            { data: 'location', "width": "5%" },
            { data: 'description', "width": "35%" },
            {
                data: 'status', "width": "10%"
            },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a href="/Seller/Quotation/Details?id=${data}" class = "btn btn-primary btn-main border-0 m-1"><i class="bi bi-plus-square"></i> View Details</a>
                    </div >`
                },
                "width": "25%"
            }
        ]
    });
}

