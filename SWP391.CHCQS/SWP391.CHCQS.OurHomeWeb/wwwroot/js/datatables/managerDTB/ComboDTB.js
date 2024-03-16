var dataTableCombo;

//specify when document is fully loaded.
$(document).ready(function () {
    loadDataTableStandardQuotation();
});

//define loadDataTableMaterial() function
function loadDataTableStandardQuotation() {
    //create DOM element as a datatable type
    dataTableCombo = $('#tblCombo').DataTable({
        "ajax": { url: '/Manager/Combo/GetAll' },
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.24/i18n/Vietnamese.json"
        },
        "columns": [
            {
                data: 'id', "width": "5%"
            },
            { data: 'name', "width": "15%" },
            { data: 'description', "width": "5%" },
            { data: 'price', "width": "5%" },
           
            { data: 'construction.name', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a href="/manager/combo/Edit?id=${data}" class = "btn btn-primary btn-main border-0 m-1"><i class="bi bi-pencil"></i> Chỉnh sửa </a>
                       <a href="/manager/combo/Delete?id=${data}" class = "btn btn-danger border-0 m-1"><i class="bi bi-pencil"></i> Xóa </a>
                    </div >`
                },
                "width": "15%"
            }
        ]
    });
}