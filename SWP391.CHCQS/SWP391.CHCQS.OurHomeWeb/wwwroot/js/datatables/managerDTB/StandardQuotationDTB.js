var dataTableStandardQuotation;

//specify when document is fully loaded.
$(document).ready(function () {
    loadDataTableStandardQuotation();
});

//define loadDataTableMaterial() function
function loadDataTableStandardQuotation() {
    //create DOM element as a datatable type
    dataTableStandardQuotation = $('#tblStandardQuotation').DataTable({
        "ajax": { url: '/Manager/StandardQuotation/GetAll' },
        "columns": [
            {
                data: 'id', "width": "5%"
            },
            { data: 'name', "width": "15%" },
            { data: 'description', "width": "5%" },
            { data: 'price', "width": "5%" },
            { data: 'status', "width": "5%" },
            { data: 'construction.name', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a href="/manager/standardquotation/Edit?id=${data}" class = "btn btn-primary btn-main border-0 m-1"><i class="bi bi-pencil"></i> Edit </a>
                    </div >`
                },
                "width": "15%"
            }
        ]
    });
}