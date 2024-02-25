$(document).ready(function () {
    loadDataTableMaterial();
});

//Need an api method return json to use this
function loadDataTableMaterial() {
    dataTable = $('#tblMaterial').DataTable({
        "ajax": { url: '/Engineer/Material/GetAll' },
        "columns": [
            { data: 'id', },
            { data: 'name', },
            { data: 'inventoryQuantity', },
            { data: 'unitPrice', },
            { data: 'unit', },
            { data: 'categoryName', },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a href="/Engineer/Material/AddToQuote?MaterialId=${data}" class="text-nowrap btn btn-primary btn-main border-0 m-1"><i class="bi bi-plus-square"></i> Add</a>
                    </div >`
                },
            }
        ]
    });
}