var dataTableM;
$(document).ready(function () {
    loadDataTableMaterial();
});

//Need an api method return json to use this
function loadDataTableMaterial() {
    dataTableM = $('#tblMaterial').DataTable({
        "ajax": { url: '/Engineer/Material/GetAll' },
        "columns": [
            {
                data: "id",
                "render": function (data) {
                    return `<a class="text-main text-pointer" onClick="ShowMaterialDetail('/Engineer/Material/Detail?MaterialId=${data}')" >${data}</a>`
                },
            },
            { data: 'name', },
            { data: 'unitPrice', },
            { data: 'unit', },
            { data: 'categoryName', },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a onClick="AddToQuoteMaterial('/Engineer/Material/AddToQuote?MaterialId=${data}')" class="text-nowrap btn btn-primary btn-main border-0 m-1"><i class="bi bi-plus-square"></i> Add</a>
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