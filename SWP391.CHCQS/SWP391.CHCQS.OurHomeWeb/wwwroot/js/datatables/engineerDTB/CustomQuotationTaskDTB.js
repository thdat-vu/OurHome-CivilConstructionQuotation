var dataTableCQT;
$(document).ready(function () {
    loadDataTableCustomQuotationTask();
});

//Need an api method return json to use this
function loadDataTableCustomQuotationTask() {
    dataTableCQT = $('#tblCustomQuotationTask').DataTable({
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
                       <a onClick=DeleteTaskFromQuote('/Engineer/Task/DeleteFromQuote?TaskId=${data}') class="text-nowrap btn btn-danger border-0 m-1"><i class="bi bi-trash"></i> Delete</a>
                    </div >`
                },
            }
        ]
    });
}

function DeleteTaskFromQuote(url) {
    $.ajax({
        url: url,
        type: 'DELETE',
        success: function (data) {
            dataTableCQT.ajax.reload();
            dataTableT.ajax.reload();
            toastr.success(data.message);
        },
        error: function (data) {
            dataTableCQT.ajax.reload();
            dataTableT.ajax.reload();
            toastr.error(data.message);
        }
    });
}