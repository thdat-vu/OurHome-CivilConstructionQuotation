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
       
            },
            { data: 'materialName'},
            { data: 'materialCateName' },
            { data: 'quantity'},
            { data: 'price'},
            { data: 'unit' },
            {
                data: "materialId",
                "render": function (data) {
                    // Generate a unique form ID using the material ID
                    var formId = 'updateQuantityForm' + data;

                    return `<form class="text-nowrap" id="${formId}" method="post">
                       <textarea name="${data}" placeholder="The reason in case of rejection" class="form-control" id="textAreaExample1" rows="4"></textarea>
                        <input type="text" name="MaterialId" hidden value="${data}">
                        <button class="btn-main text-nowrap border-0 rounded p-1 w-100 type="button" onclick="UpdateMaterialQuantity('/Engineer/Material/UpdateQuantity', '${formId}')">
                         <i class="bi bi-check-lg">Note</i>
                        </button>
                    </form>`
                },
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
