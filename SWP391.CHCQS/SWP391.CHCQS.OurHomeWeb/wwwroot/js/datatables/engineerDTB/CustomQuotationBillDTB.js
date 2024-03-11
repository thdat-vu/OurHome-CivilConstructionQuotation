$(document).ready(function () {
    loadDataTableQuotationBill();
});

function loadDataTableQuotationBill() {
    dataTableCQB = $('#tblCustomQuotationBill').DataTable({
        "ajax": { url: '/Engineer/Quotation/GetQuotationBill' },
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.24/i18n/Vietnamese.json"
        },
        "lengthChange": false,
        "paging": false,
        "searching": false,
        "ordering": false,
        "info": false,
        "columns": [
            { data: 'priceOnAcreage', render: formatCurrency },
            {
                data: 'acreage',
                "render": function (data) {
                    return `${data} m2`;
                },
            },
            {
                data: 'foundationAcreage',
                "render": function (data) {
                    return `${data} m2`;
                },
            },
            {
                data: 'basementAcreage',
                "render": function (data) {
                    return `${data} m2`;
                },
            },
            {
                data: 'rooftopAcreage',
                "render": function (data) {
                    return `${data} m2`;
                },
            },
            {
                data: 'balconyAcreage',
                "render": function (data) {
                    return `${data} m2`;
                },
            },
            {
                data: 'totalAcreage',
                "render": function (data) {
                    return `${data} m2`;
                },
            },
            { data: 'totalPriceTask', render: formatCurrency },
            { data: 'totalPriceMaterial', render: formatCurrency },
            { data: 'totalPrice', render: formatCurrency },
        ]
    });
}

// Hàm định dạng giá tiền
function formatCurrency(data, type, row) {
    // Kiểm tra nếu dữ liệu không phải là số thì trả về dữ liệu nguyên thô
    if (type !== 'display' || isNaN(data)) {
        return data;
    }

    // Sử dụng hàm toLocaleString để định dạng giá tiền theo ngôn ngữ và quốc gia hiện tại
    return Number(data).toLocaleString('vi-VN', {
        style: 'currency',
        currency: 'VND'
    });
}
