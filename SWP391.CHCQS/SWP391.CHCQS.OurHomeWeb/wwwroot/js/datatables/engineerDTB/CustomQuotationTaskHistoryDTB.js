$(document).ready(function () {
    loadDataTableCustomQuotationTaskHistory();
});

//Need an api method return json to use this
function loadDataTableCustomQuotationTaskHistory() {
    dataTableCQT = $('#tblCustomQuotationTask').DataTable({
        "ajax": { url: '/Engineer/Task/GetTaskListHistory' },
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.24/i18n/Vietnamese.json"
        },
        "columns": [
            {
                data: 'task.id',
                "render": function (data) {
                    return `<a class="text-main text-pointer" onClick="ShowTaskDetail('/Engineer/Task/Detail?TaskId=${data}')" >${data}</a>`
                },
            },
            { data: 'task.name', },
            { data: 'task.unitPrice', render: formatCurrency },
            { data: 'price', render: formatCurrency },
            { data: 'task.categoryId', },
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