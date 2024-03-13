var dataTableAvailableMaterial;

//specify when document is fully loaded.
$(document).ready(function () {
    loadDataTableMaterial();
});

//define loadDataTableMaterial() function
function loadDataTableMaterial() {
    //create DOM element as a datatable type
    dataTableAvailableMaterial = $('#tblAvailableMaterial').DataTable({
        "ajax": { url: '/Manager/Material/GetAll' },
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.24/i18n/Vietnamese.json"
        },
        "columns": [
            {
                data: 'id',
                "render": function (data) {
                    return `<a class="text-main text-pointer" onClick="ShowMaterialDetail('/Engineer/Material/Detail?MaterialId=${data}')" >${data}</a>`
                }
                , "width": "5%"
            },
            { data: 'name', "width": "15%" },
            { data: 'unitPrice', "width": "5%" },
            { data: 'unit', "width": "5%" },
            { data: 'category.name', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a onClick="AddToListMaterial('/Manager/Material/AddToList?MaterialId=${data}')"  class = "btn btn-primary btn-main border-0 m-1"><i class="bi bi-plus-circle"></i> Add </a>
                    </div >`
                },
                "width": "15%"
            }
        ]
    });
}

function AddToListMaterial(url) {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (data) {
            if (!data.success) {
                comboMaterialListDTB.ajax.reload(null, false);
                dataTableAvailableMaterial.ajax.reload(null, false);
                
                toastr.error(data.message);
            } else {
                comboMaterialListDTB.ajax.reload(null, false);
                dataTableAvailableMaterial.ajax.reload(null, false);
                
                toastr.success(data.message);
            }
        },
        error: function (data) {
            comboMaterialListDTB.ajax.reload(null, false);
            dataTableAvailableMaterial.ajax.reload(null, false);
            
            toastr.error(data.message);
        }
    });
}