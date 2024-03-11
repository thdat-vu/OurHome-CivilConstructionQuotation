var dataTableT;
$(document).ready(function () {
    loadDataTableTask();
});

//Need an api method return json to use this
function loadDataTableTask() {
    dataTableT = $('#tblTask').DataTable({
        "ajax": { url: '/Engineer/Task/GetAll' },
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.24/i18n/Vietnamese.json"
        },
        "columns": [
            {
                data: "id",
                "render": function (data) {
                    return `<a class="text-main text-pointer" onClick="ShowTaskDetail('/Engineer/Task/Detail?TaskId=${data}')" >${data}</a>`
                },
            },
            { data: 'name', },
            { data: 'unitPrice', },
            { data: 'categoryName', },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a onClick="AddToQuoteTask('/Engineer/Task/AddToQuote?TaskId=${data}')" class="text-nowrap btn btn-primary btn-main border-0 m-1"><i class="bi bi-plus-square"></i> Thêm</a>
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
                dataTableCQT.ajax.reload(null, false);
                dataTableT.ajax.reload(null, false);
                dataTableCQB.ajax.reload(null, false);
                toastr.error(data.message);
            } else {
                dataTableCQT.ajax.reload(null, false);
                dataTableT.ajax.reload(null, false);
                dataTableCQB.ajax.reload(null, false);
                toastr.success(data.message);
            }
        },
        error: function (data) {
            dataTableCQT.ajax.reload(null, false);
            dataTableT.ajax.reload(null, false);
            dataTableCQB.ajax.reload(null, false);
            toastr.error(data.message);
        }
    });
}