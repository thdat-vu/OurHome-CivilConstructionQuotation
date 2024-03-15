var dataTableAvailableTask;
$(document).ready(function () {
    loadDataTableTask();
});

//Need an api method return json to use this
function loadDataTableTask() {
    dataTableAvailableTask = $('#tblAvailableTask').DataTable({
        "ajax": { url: '/Manager/Task/GetAll' },
        "columns": [
            {
                data: "id",
                "render": function (data) {
                    return `<a class="text-main text-pointer" onClick="ShowTaskDetail('/Engineer/Task/Detail?TaskId=${data}')" >${data}</a>`
                },
            },
            { data: 'name', },
            { data: 'unitPrice', },
            { data: 'category.name', },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a onClick="AddToListTask('/Manager/Task/AddToList?TaskId=${data}')" class="btn btn-primary btn-main border-0 m-1"><i class="bi bi-plus-circle"></i> Add </a>
                       
                    </div>`
                },
            }
        ]
    });
}



function AddToListTask(url) {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (data) {
            if (!data.success) {
                comboTaskListDTB.ajax.reload(null, false);
                dataTableAvailableTask.ajax.reload(null, false);

                toastr.error(data.message);
            } else {
                comboTaskListDTB.ajax.reload(null, false);
                dataTableAvailableTask.ajax.reload(null, false);

                toastr.success(data.message);
            }
        },
        error: function (data) {
            comboTaskListDTB.ajax.reload(null, false);
            dataTableAvailableTask.ajax.reload(null, false);

            toastr.error(data.message);
        }
    });
}