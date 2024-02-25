$(document).ready(function () {
    loadDataTableCustomQuotationTask();
});

//Need an api method return json to use this
function loadDataTableCustomQuotationTask() {
    dataTable = $('#tblCustomQuotationTask').DataTable({
        "ajax": { url: '/Engineer/Task/GetTaskListSession' },
        "columns": [
            { data: 'task.id', },
            { data: 'task.name', },
            { data: 'task.unitPrice', },
            { data: 'price', },
            {
                data: 'task.id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a href="/Engineer/Task/DeleteFromQuote?TaskId=${data}" class="text-nowrap btn btn-danger border-0 m-1"><i class="bi bi-trash"></i> Delete</a>
                    </div >`
                },
            }
        ]
    });
}