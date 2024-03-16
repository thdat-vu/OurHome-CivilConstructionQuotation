var dataTableM;
$(document).ready(function () {
    loadDataTableMaterial();
});

//Need an api method return json to use this
function loadDataTableMaterial() {
    dataTableM = $('#tblMaterial').DataTable({
        "ajax": { url: '/Engineer/Material/GetAll' },
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.24/i18n/Vietnamese.json"
        },
        "columns": [
            {
                data: "id",
                "render": function (data) {
                    return `<a class="text-main text-pointer" onClick="ShowMaterialDetail('/Engineer/Material/Detail?MaterialId=${data}')" >${data}</a>`
                },
            },
            { data: 'name', },
            { data: 'unitPrice', render: formatCurrency },
            { data: 'unit', },
            { data: 'categoryName', },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a onClick="AddToQuoteMaterial('/Engineer/Material/AddToQuote?MaterialId=${data}')" class="text-nowrap btn btn-primary btn-main border-0 m-1"><i class="bi bi-plus-square"></i> Thêm</a>
                    </div >`
                },
            }
        ]
    });
}

function AddToQuoteMaterial(url) {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (data) {
            if (!data.success) {
                dataTableMD.ajax.reload(null, false);
                dataTableM.ajax.reload(null, false); 
                dataTableCQB.ajax.reload(null, false);
                toastr.error(data.message);
            } else {
                dataTableMD.ajax.reload(null, false);
                dataTableM.ajax.reload(null, false);
                dataTableCQB.ajax.reload(null, false);
                toastr.success(data.message);
            }
        },
        error: function (data) {
            dataTableMD.ajax.reload(null, false);
            dataTableM.ajax.reload(null, false);
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