var dataTableCQB;
$(document).ready(function () {
    loadDataTableQuotationBill();
});

//Need an api method return json to use this
function loadDataTableQuotationBill() {
    dataTableCQB = $('#tblCustomQuotationBill').DataTable({
        "ajax": { url: '/Engineer/Quotation/GetQuotationBill' },
        "lengthChange": false,
        "paging": false,
        "searching": false,
        "ordering": false,
        "columns": [
            { data: 'priceOnAcreage', },
            { data: 'acreage', },
            { data: 'foundationAcreage', },
            { data: 'basementAcreage', },
            { data: 'rooftopAcreage', },
            { data: 'balconyAcreage', },
            { data: 'totalAcreage', },
            { data: 'totalPriceTask', },
            { data: 'totalPriceMaterial', },
            { data: 'totalPrice', },
        ]
    });
}