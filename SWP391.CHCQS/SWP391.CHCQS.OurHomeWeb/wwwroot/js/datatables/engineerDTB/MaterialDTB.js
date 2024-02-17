$(document).ready(function () {
    loadDataTableMaterial();
});

//Need an api method return json to use this
function loadDataTableMaterial() {
    dataTable = $('#tblMaterial').DataTable({
        "ajax": { url: '/Engineer/Material/GetAll' },
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'name', "width": "15%" },
            { data: 'inventoryQuantity', "width": "5%" },
            { data: 'unitPrice', "width": "5%" },
            { data: 'unit', "width": "5%" },
            { data: 'categoryName', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a href="/Engineer/Material/AddToQuote?MaterialId=${data}" class = "btn btn-primary btn-main border-0 m-1"><i class="bi bi-plus-square"></i> Add</a>
                    </div >`
                },
                "width": "15%"
            }
        ]
    });
}