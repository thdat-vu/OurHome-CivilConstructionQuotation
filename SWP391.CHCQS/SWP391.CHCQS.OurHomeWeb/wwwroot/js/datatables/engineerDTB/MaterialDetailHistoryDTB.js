$(document).ready(function () {
    loadDataTableMaterialDetailHistory();
});

//Need an api method return json to use this
function loadDataTableMaterialDetailHistory() {
    dataTableMD = $('#tblMaterialDetail').DataTable({
        "ajax": { url: '/Engineer/Material/GetMaterialListHistory' },
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.24/i18n/Vietnamese.json"
        },
        "columns": [
            {
                data: 'material.id',
                "render": function (data) {
                    return `<a class="text-main text-pointer" onClick="ShowMaterialDetail('/Engineer/Material/Detail?MaterialId=${data}')" >${data}</a>`
                },
            },
            { data: 'material.name', },
            { data: 'material.unitPrice', render: formatCurrency },
            { data: 'material.unit', },
            { data: 'quantity', },
            { data: 'price', render: formatCurrency },
            { data: 'material.categoryId', },
        ]
    });
}

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