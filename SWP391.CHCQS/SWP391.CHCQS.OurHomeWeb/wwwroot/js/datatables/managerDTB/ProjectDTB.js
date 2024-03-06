var dataTableP;

//specify when document is fully loaded.
$(document).ready(function () {
    loadDataTableProject();
});

//define loadDataTableMaterial() function
function loadDataTableProject() {
    //create DOM element as a datatable type
    dataTableM = $('#tblProject').DataTable({
        "ajax": { url: '/Manager/Project/GetAll' },
        "columns": [
            {
                data: 'id',
                "render": function (data) {
                    return `<a class="text-main text-pointer" onClick="ShowMaterialDetail('/Engineer/Material/Detail?MaterialId=${data}')" >${data}</a>`
                }
                , "width": "5%"
            },
            { data: 'name', "width": "10%" },
            { data: 'location', "width": "10%" },
            { data: 'scale', "width": "10%" },
            { data: 'size', "width": "10%" },
            { data: 'description', "width": "10%" },
            { data: 'overview', "width": "10%" },
            { data: 'date', "width": "10%" },
            { data: 'customer.name', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a href="/manager/material/Edit?id=${data}" class = "btn btn-primary btn-main border-0 m-1"><i class="bi bi-pencil"></i> Edit </a>
                       <a href="/manager/material/Delete?id=${data}" class = "btn btn-danger border-0 m-1"><i class="bi bi-trash"></i> Delete </a>
                    </div >`
                },
                "width": "15%"
            }
        ]
    });
}