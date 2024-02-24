
$(document).ready(function () {
    loadDataTableTask();
});

//Need an api method return json to use this
function loadDataTableTask() {
    dataTable = $('#tblTask').DataTable({
        "ajax": { url: '/Engineer/Task/GetAll' },
        "columns": [
            { data: 'id', },
            { data: 'name', },
            { data: 'unitPrice', },
            { data: 'categoryName', },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a onClick=AddToQuote('/Engineer/Task/AddToQuote?TaskId=${data}')" class="text-nowrap btn btn-primary btn-main border-0 m-1"><i class="bi bi-plus-square"></i> Add</a>
                    </div >`
                },
            }
        ]
    });
}

function AddToQuote(url) {
    $.ajax({
        type: 'POST',
        url: url,
        success: function (data) {
            dataTable.ajax.reload();
            toastr.success(data.message);
        },
        error: function (data) {
            dataTable.ajax.reload();
            toastr.success(data.error);
        }
    });
}