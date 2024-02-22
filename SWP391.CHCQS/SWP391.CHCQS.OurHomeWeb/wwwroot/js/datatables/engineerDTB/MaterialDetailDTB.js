$(document).ready(function () {
    loadDataTableMaterialDetail();
});

//Need an api method return json to use this
function loadDataTableMaterialDetail() {
    dataTable = $('#tblMaterialDetail').DataTable({
        "ajax": { url: '/Engineer/Material/GetMaterialListSession' },
        "columns": [
            { data: 'material.id', },
            { data: 'material.name', },
            { data: 'material.unitPrice', },
            { data: 'material.unit', },
            { data: 'quantity', },
            { data: 'price', },
            {
                data: 'material.id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a href="/Engineer/Material/DeleteFromQuote?MaterialId=${data}" class="text-nowrap btn btn-danger border-0 m-1"><i class="bi bi-trash"></i> Delete</a>
                    </div >`
                },
            }
        ]
    });
}