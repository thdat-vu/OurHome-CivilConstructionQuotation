$(document).ready(function () {
    loadDataTableQuotation();
});

//Need an api method return json to use this
function loadDataTableQuotation() {
    dataTable = $('#tblCustomQuotation').DataTable({
        "ajax": { url: '/Engineer/Quotation/GetAll' },
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'date', "width": "15%" },
            { data: 'acreage', "width": "5%" },
            { data: 'location', "width": "15%" },
            { data: 'status', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a href="/Engineer/Quotation/Quote?QuotationId=${data}" class = "btn btn-primary btn-main border-0 m-1"><i class="bi bi-folder2-open"></i> Quote</a>
                        <a href="/Engineer/Quotation/Edit?QuotationId=${data}" class = "btn btn-primary m-1"><i class="bi bi-pencil-square"></i> Edit</a>
                    </div >`
                },
                "width": "15%"
            }
        ]
    });
}