var dataTableMD;
$(document).ready(function () {
    loadDataTableMaterialDetail();
});

//Need an api method return json to use this
function loadDataTableMaterialDetail() {
    dataTableMD = $('#tblMaterialDetail').DataTable({
        "ajax": { url: '/Engineer/Material/GetMaterialListSession' },
        "columns": [
            { data: 'material.id', },
            { data: 'material.name', },
            { data: 'material.unitPrice', },
            { data: 'material.unit', },
            {
                data: null,
                "render": function (data) {
                    // Generate a unique form ID using the material ID
                    var formId = 'updateQuantityForm' + data.material.id;

                    return `<form class="text-nowrap" id="${formId}" method="post">
                        <input style="border: 1px solid #aaa;" class="rounded p-1" type="number" name="MaterialQuantity" value="${data.quantity}">
                        <input type="text" name="MaterialId" hidden value="${data.material.id}">
                        <button class="btn-main text-white border-0 rounded p-1" type="button" onclick="UpdateMaterialQuantity('/Engineer/Material/UpdateQuantity', '${formId}')">
                            <i class="bi bi-arrow-clockwise"></i>
                        </button>
                    </form>`
                }
            },
            { data: 'price', },
            {
                data: 'material.id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a onClick=DeleteMaterialFromQuote('/Engineer/Material/DeleteFromQuote?MaterialId=${data}') class="text-nowrap btn btn-danger border-0 m-1"><i class="bi bi-trash"></i> Delete</a>
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
            dataTableMD.ajax.reload();
            dataTableM.ajax.reload();
            toastr.success(data.message);
        },
        error: function (data) {
            dataTableMD.ajax.reload();
            dataTableM.ajax.reload();
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
                dataTableMD.ajax.reload();
                toastr.error(data.message);
            } else {
                dataTableMD.ajax.reload();
                toastr.success(data.message);
            }
        },
        error: function (data) {
            dataTableMD.ajax.reload();
            toastr.error(data.message);
        }
    });
}