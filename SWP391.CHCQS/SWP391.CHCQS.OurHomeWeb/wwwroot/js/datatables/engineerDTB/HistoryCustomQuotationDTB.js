var dataTableCQ;
$(document).ready(function () {
    loadDataTableHistoryQuotation();
});

//Need an api method return json to use this
function loadDataTableHistoryQuotation() {
    dataTableCQ = $('#tblCustomQuotation').DataTable({
        "ajax": { url: '/Engineer/Quotation/GetHistory' },
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
                        <a href="/Engineer/Quotation/Detail?QuotationId=${data}" class="text-nowrap btn btn-primary btn-main border-0 m-1"><i class="bi bi-eye"></i> Detail</a>
                    </div >`
                },
            }
        ]
    });
}