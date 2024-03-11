$(document).ready(function () {
    loadDataTableRequest();
});

//Need an api method return json to use this
function loadDataTableRequest() {
    dataTable = $('#tblQuotation').DataTable({
        "ajax": { url: '/Seller/Quotation/GetAll' },
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.24/i18n/Vietnamese.json"
        },
        "columns": [
            { data: 'id', },
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
            { data: 'acreage', },
            { data: 'location', },
            { data: 'description', },
            {
                data: 'status',
            },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a href="/Seller/Quotation/Details?id=${data}" class = "text-nowrap btn btn-primary btn-main border-0 m-1"><i class="bi bi-eye"></i> Xem</a>
                       <a href="/Seller/Quotation/EditConstructDetails?id=${data}" class = "text-nowrap btn btn-primary btn-main border-0 m-1"><i class="bi bi-pencil-square"></i> Chỉnh sửa</a>
                    </div >`
                },
            }
        ]
    });
}

