﻿var dataTableMD;
$(document).ready(function () {
    loadDataTableMaterialDetail();
});

//Need an api method return json to use this
function loadDataTableMaterialDetail() {
    dataTableMD = $('#tblMaterialDetail').DataTable({
        "ajax": { url: '/Engineer/Material/GetMaterialListSession' },
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
            {
                data: null,
                "render": function (data) {
                    // Generate a unique form ID using the material ID
                    var formId = 'updateQuantityForm' + data.material.id;

                    return `<form class="text-nowrap" id="${formId}" method="post">
                        <input style="border: 1px solid #aaa;" class="rounded p-1" type="number" name="MaterialQuantity" value="${data.quantity}" max="999999999">
                        <input type="text" name="MaterialId" hidden value="${data.material.id}">
                        <button class="btn-main text-white border-0 rounded p-1" type="button" onclick="UpdateMaterialQuantity('/Engineer/Material/UpdateQuantity', '${formId}')">
                            <i class="bi bi-arrow-clockwise"></i>
                        </button>
                    </form>`
                }
            },
            { data: 'price', render: formatCurrency },
            {
                data: 'material.id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a onClick=DeleteMaterialFromQuote('/Engineer/Material/DeleteFromQuote?MaterialId=${data}') class="text-nowrap btn btn-danger border-0 m-1"><i class="bi bi-trash"></i> Xóa</a>
                    </div>`
                },
            }
        ]
    });
}

function DeleteMaterialFromQuote(url) {
    $.ajax({
        url: url,
        type: 'DELETE',
        success: function (data) {
            dataTableMD.ajax.reload(null, false);
            dataTableM.ajax.reload(null, false);
            dataTableCQB.ajax.reload(null, false);
            toastr.success(data.message);
        },
        error: function (data) {
            dataTableMD.ajax.reload(null, false);
            dataTableM.ajax.reload(null, false);
            dataTableCQB.ajax.reload(null, false);
            toastr.error(data.message);
        }
    });
}

function UpdateMaterialQuantity(url, formId) {
    var formData = $('#' + formId).serialize();
    $.ajax({
        url: url,
        type: 'POST',
        data: formData,
        success: function (data) {
            if (!data.success) {
                dataTableMD.ajax.reload(null, false);
                dataTableCQB.ajax.reload(null, false);
                toastr.error(data.message);
            } else {
                dataTableMD.ajax.reload(null, false);
                dataTableCQB.ajax.reload(null, false);
                toastr.success(data.message);
            }
        },
        error: function (data) {
            dataTableMD.ajax.reload(null, false);
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