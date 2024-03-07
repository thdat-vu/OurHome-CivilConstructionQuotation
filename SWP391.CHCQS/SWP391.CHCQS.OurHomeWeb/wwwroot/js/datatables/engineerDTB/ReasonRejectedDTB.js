var dataTableRJ;
$(document).ready(function () {
    loadDataTableReasonReject();
});

//Need an api method return json to use this
function loadDataTableReasonReject() {
    dataTableRJ = $('#tblCustomQuotationReason').DataTable({
        "ajax": { url: '/Engineer/Quotation/GetReasonRejected' },
        "columns": [
            { data: 'id', },
            { data: 'name', },
            { data: 'quantity', },
            { data: 'reason', },
        ]
    });
}