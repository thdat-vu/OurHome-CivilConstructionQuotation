var dataTableM;

//specify when document is fully loaded.
$(document).ready(function () {
    loadDataTableMaterial();
});

//define loadDataTableMaterial() function
function loadDataTableMaterial() {
    //create DOM element as a datatable type
    dataTableM = $('#tblMaterial').DataTable({
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
                       <a href="/manager/material/Edit?id=${data}" class = "btn btn-primary btn-main border-0 m-1"><i class="bi bi-pencil"></i> Chỉnh sửa </a>
                       <a href="/manager/material/Delete?id=${data}" class = "btn btn-danger border-0 m-1"><i class="bi bi-trash"></i> Xóa </a>
                    </div >`
                },
                "width": "15%"
            }
        ]
    });
}