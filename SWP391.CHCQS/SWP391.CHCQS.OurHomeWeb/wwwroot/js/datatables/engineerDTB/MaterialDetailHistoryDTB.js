$(document).ready(function () {
    loadDataTableMaterialDetailHistory();
});

//Need an api method return json to use this
function loadDataTableMaterialDetailHistory() {
    dataTableMD = $('#tblMaterialDetail').DataTable({
        "ajax": { url: '/Engineer/Material/GetMaterialListHistory' },
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
            { data: 'material.unitPrice', },
            { data: 'material.unit', },
            { data: 'quantity', },
            { data: 'price', },
            { data: 'material.categoryId', },
        ]
    });
}