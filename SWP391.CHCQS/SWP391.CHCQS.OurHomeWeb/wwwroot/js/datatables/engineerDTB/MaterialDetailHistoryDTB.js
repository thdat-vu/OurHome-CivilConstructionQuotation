$(document).ready(function () {
    loadDataTableMaterialDetailHistory();
});

//Need an api method return json to use this
function loadDataTableMaterialDetailHistory() {
    dataTableMD = $('#tblMaterialDetail').DataTable({
        "ajax": { url: '/Engineer/Material/GetMaterialListHistory' },
        "columns": [
            {
                data: 'material.id',
                "render": function (data) {
                    return `<a class="text-main text-pointer" onClick="ShowMaterialDetail('/Engineer/Material/Detail?MaterialId=${data}')" >${data}</a>`
                },
            },
            { data: 'material.name', },
            { data: 'material.unitPrice', },
            { data: 'material.unit', },
            { data: 'quantity', },
            { data: 'price', },
            { data: 'material.categoryId', },
        ]
    });
}