var dataTableT;
$(document).ready(function () {
    loadDataTableTask();
});

//Need an api method return json to use this
function loadDataTableTask() {
    dataTableT = $('#tblTask').DataTable({
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
                       <a onClick="AddToQuoteTask('/Engineer/Task/AddToQuote?TaskId=${data}')" class="text-nowrap btn btn-primary btn-main border-0 m-1"><i class="bi bi-plus-square"></i> Add</a>
                    </div>`
                },
            }
        ]
    });
}

function AddToQuoteTask(url) {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (data) {
            if (!data.success) {
                dataTableCQT.ajax.reload();
                dataTableT.ajax.reload();
                toastr.error(data.message);
            } else {
                dataTableCQT.ajax.reload();
                dataTableT.ajax.reload();
                toastr.success(data.message);
            }
        },
        error: function (data) {
            dataTableCQT.ajax.reload();
            dataTableT.ajax.reload();
            toastr.error(data.message);
        }
    });
}