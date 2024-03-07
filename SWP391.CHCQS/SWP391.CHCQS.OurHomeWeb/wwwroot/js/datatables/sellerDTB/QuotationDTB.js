$(document).ready(function () {
    loadDataTableRequest();
});

//Need an api method return json to use this
function loadDataTableRequest() {
    dataTable = $('#tblQuotation').DataTable({
        "ajax": { url: '/Seller/Quotation/GetAll' },
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'date', "width": "15%" },
            { data: 'acreage', "width": "15%" },
            { data: 'location', "width": "15%" },
            { data: 'description', "width": "35%" },
            {
                data: 'status', "width": "15%"
            },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a href="/Seller/Quotation/Details?id=${data}" class = "btn btn-primary btn-main border-0 m-1"><i class="bi bi-plus-square"></i> View Details</a>
                    </div >`
                },
                "width": "15%"
            }
        ]
    });
}

