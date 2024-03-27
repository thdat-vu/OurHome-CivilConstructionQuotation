var comboMaterialListDTB;
$(document).ready(function () {
    loadDataTableMaterialListSession();
});

//Need an api method return json to use this
function loadDataTableMaterialListSession() {
    comboMaterialListDTB = $('#tblComboMaterialList').DataTable({
        "ajax": { url: '/Manager/Material/GetMaterialListSession' },
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
            { data: 'categoryName', },
            {
                data: 'material.id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a onClick=DeleteMaterialFromList('/Manager/Material/DeleteFromList?MaterialId=${data}') class="text-nowrap btn btn-danger border-0 m-1"><i class="bi bi-trash"></i> Xóa</a>
                    </div>`
                },
            }
        ]
    });
}

function DeleteMaterialFromList(url) {
    $.ajax({
        url: url,
        type: 'DELETE',
        success: function (data) {
            comboMaterialListDTB.ajax.reload(null, false);
            comboMaterialListDTB.ajax.reload(null, false);
            comboMaterialListDTB.ajax.reload(null, false);
            toastr.success(data.message);
        },
        error: function (data) {
            comboMaterialListDTB.ajax.reload(null, false);
            comboMaterialListDTB.ajax.reload(null, false);
            comboMaterialListDTB.ajax.reload(null, false);
            toastr.error(data.message);
        }
    });
}

