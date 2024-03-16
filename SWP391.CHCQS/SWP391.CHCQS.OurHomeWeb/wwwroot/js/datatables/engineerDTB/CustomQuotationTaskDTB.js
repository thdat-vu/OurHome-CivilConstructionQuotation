var dataTableCQT;
$(document).ready(function () {
    loadDataTableCustomQuotationTask();
});

//Need an api method return json to use this
function loadDataTableCustomQuotationTask() {
    dataTableCQT = $('#tblCustomQuotationTask').DataTable({
        "ajax": { url: '/Engineer/Task/GetTaskListSession' },
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
            {
                data: 'task.id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a onClick="DeleteTaskFromQuote('/Engineer/Task/DeleteFromQuote?TaskId=${data}')" class="text-nowrap btn btn-danger border-0 m-1"><i class="bi bi-trash"></i> Xóa</a>
                    </div >`
                },
            }
        ]
    });
}

function DeleteTaskFromQuote(url) {
    $.ajax({
        url: url,
        type: 'DELETE',
        success: function (data) {
            if (!data.success) {
                dataTableCQT.ajax.reload(null, false);
                dataTableT.ajax.reload(null, false);
                dataTableCQB.ajax.reload(null, false);
                toastr.error(data.message);
            } else {
                dataTableCQT.ajax.reload(null, false);
                dataTableT.ajax.reload(null, false);
                dataTableCQB.ajax.reload(null, false);
                toastr.success(data.message);
            }
        },
        error: function (data) {
            dataTableCQT.ajax.reload(null, false);
            dataTableT.ajax.reload(null, false);
            dataTableCQB.ajax.reload(null, false);
            toastr.error(data.message);
        }
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