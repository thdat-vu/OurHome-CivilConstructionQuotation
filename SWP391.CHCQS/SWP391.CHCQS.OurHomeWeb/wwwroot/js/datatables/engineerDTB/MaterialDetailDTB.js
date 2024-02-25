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
            {
                data: null,
                "render": function (data) {
                    return `<form class="text-nowrap" action="/Engineer/Material/UpdateQuantity" method="post">
                    <input style="border: 1px solid #aaa;" class="rounded p-1" type="number" name="MaterialQuantity" value="${data.quantity}">
                    <input type="text" name="MaterialId" hidden value="${data.material.id}">
                    <button class="btn-main text-white border-0 rounded p-1" type="submit"><i class="bi bi-arrow-clockwise"></i></button>
                    </form>`
                }
            },
            { data: 'price', },
            {
                data: 'material.id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a href="/Engineer/Material/DeleteFromQuote?MaterialId=${data}" class="text-nowrap btn btn-danger border-0 m-1"><i class="bi bi-trash"></i> Delete</a>
                    </div>`
                },
            }
        ]
    });
}