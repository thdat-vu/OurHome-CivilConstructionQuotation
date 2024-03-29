var dataTableRJ;
$(document).ready(function () {
    loadDataTableReasonReject();
});

//Need an api method return json to use this
function loadDataTableReasonReject() {
    dataTableRJ = $('#tblCustomQuotationReason').DataTable({
        "ajax": { url: '/Engineer/Quotation/GetReasonRejected' },
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.24/i18n/Vietnamese.json"
        },
        "columns": [
            { data: 'id', },
            { data: 'name', },
            { data: 'quantity', },
            { data: 'reason', },
        ]
    });
}