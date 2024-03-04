$(document).ready(function () {
    loadDataMaterialDetail();
});


//change name method - remember
//Need an api method return json to use this
function loadDataMaterialDetail() {
    dataTable = $('#tblMaterialDetail').DataTable({
        "ajax": { url: '/Manager/MaterialDetail/GetDetail' },
        "columns": [
            {
                data: "materialId",
                "render": function (data) {
                    return `<a class="text-main text-pointer" onClick="ShowMaterialDetail('/Engineer/Material/Detail?MaterialId=${data}')" >${data}</a>`
                },
                "width": "5%"
            },
            { data: 'materialName', "width": "15%" },
            { data: 'materialCateName', "width": "15%" },
            { data: 'quantity', "width": "15%" },
            { data: 'price', "width": "15%" },
            { data: 'unit', "width": "15%" },
            {
                data: null,
                "render": function (data) {
                    // Generate a unique form ID using the material ID
                    var formId = 'updateQuantityForm' + data.material.id;

                    return `<form class="text-nowrap" id="${formId}" method="post">
                       <textarea name="${data.material.id}" placeholder="The reason in case of rejection" class="form-control" id="textAreaExample1" rows="4"></textarea>
                        <input type="text" name="MaterialId" hidden value="${data.material.id}">
                        <button class="btn-main text-white border-0 rounded p-1" type="button" onclick="UpdateMaterialQuantity('/Engineer/Material/UpdateQuantity', '${formId}')">
                           <i class="bi bi-check-lg"></i>
                        </button>
                    </form>`
                }
                "width": "15%"
            }
        ]
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
