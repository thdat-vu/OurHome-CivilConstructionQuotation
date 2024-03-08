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
            {
                data: 'date',
                "render": function (data) {
                    // Chuyển đổi ngày thành chuỗi định dạng dd/MM/yyyy
                    let date = new Date(data);
                    let day = ("0" + date.getDate()).slice(-2);
                    let month = ("0" + (date.getMonth() + 1)).slice(-2);
                    let year = date.getFullYear();
                    return `${day}/${month}/${year}`;
                },
                "width": "10%"
            },
            { data: 'customer.name', "width": "10%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a href="/Manager/Project/Edit?id=${data}" class = "btn btn-primary btn-main border-0 m-1"><i class="bi bi-pencil"></i> Edit </a>
                       <a href="/Manager/Project/Delete?id=${data}" class = "btn btn-danger border-0 m-1"><i class="bi bi-trash"></i> Delete </a>
                    </div >`
                },
                "width": "15%"
            }
        ]
    });
}