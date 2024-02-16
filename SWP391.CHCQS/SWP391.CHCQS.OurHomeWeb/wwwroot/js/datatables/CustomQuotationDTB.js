$(document).ready(function () {
    loadDataTable();
});

//Need an api method return json to use this
function loadDataTable() {
    dataTable = $('#tblCustomQuotation').DataTable({
        "ajax": { url: '/Engineer/Quotation/GetAll' },
        "columns": [
            { data: 'id', "width": "10%" },
            { data: 'date', "width": "20%" },
            { data: 'acreage', "width": "15%" },
            { data: 'location', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                        <a href="/Admin/Products/Upsert?quotationId=${data}" class = "btn btn-primary bg-main border-0"><i class="bi bi-pencil-square"></i> Quote</a>
                        <a onClick=Delete('/Admin/Products/Delete?quotationId=${data}') class = "btn btn-primary mx-2"><i class="bi bi-eye-fill"></i> View</a>
                    </div >`
                },
                "width": "15%"
            }
        ]
    });
}