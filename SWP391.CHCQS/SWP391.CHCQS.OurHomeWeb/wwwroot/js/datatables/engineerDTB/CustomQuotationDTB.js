var dataTableCQ;
$(document).ready(function () {
    loadDataTableQuotation();
});

//Need an api method return json to use this
function loadDataTableQuotation() {
    dataTableCQ = $('#tblCustomQuotation').DataTable({
        "ajax": { url: '/Engineer/Quotation/GetAll' },
        "columns": [
            { data: 'id', },
            { data: 'date', },
            { data: 'acreage', },
            { data: 'location', },
            { data: 'status', },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a href="/Engineer/Quotation/Quote?QuotationId=${data}" class="text-nowrap btn btn-primary btn-main border-0 m-1"><i class="bi bi-folder2-open"></i> Quote</a>
                        <a href="/Engineer/Quotation/SendToManager?QuotationId=${data}" class="text-nowrap btn btn-primary m-1"><i class="bi bi-pencil-square"></i> Send</a>
                    </div >`
                },
            }
        ]
    });
}