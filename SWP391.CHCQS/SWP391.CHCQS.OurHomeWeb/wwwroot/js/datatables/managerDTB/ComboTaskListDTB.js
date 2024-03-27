var comboTaskListDTB;

$(document).ready(function () {
    loadDataTableTaskListSession();
});

function loadDataTableTaskListSession() {
    comboTaskListDTB = $('#tblComboTaskList').DataTable({
        "ajax": { url: '/Manager/Task/GetTaskListSession' },
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.24/i18n/Vietnamese.json"
        },
        "columns": [
            {
                data: 'task.id',
                "render": function (data) {
                    return `<a class="text-main text-pointer" onClick="ShowMaterialDetail('/Engineer/Material/Detail?MaterialId=${data}')" >${data}</a>`
                },
            },
            { data: 'task.name', },
            { data: 'task.unitPrice', render: formatCurrency },
           
            { data: 'categoryName', },
            {
                data: 'task.id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a onClick=DeleteTaskFromList('/Manager/Task/DeleteFromList?TaskId=${data}') class="text-nowrap btn btn-danger border-0 m-1"><i class="bi bi-trash"></i> Xóa</a>
                    </div>`
                },
            }
        ]
    });
}

function DeleteTaskFromList(url) {
    $.ajax({
        url: url,
        type: 'DELETE',
        success: function (data) {
            comboTaskListDTB.ajax.reload(null, false);
            dataTableAvailableTask.ajax.reload(null, false);
            
            toastr.success(data.message);
        },
        error: function (data) {
            comboTaskListDTB.ajax.reload(null, false);
            dataTableAvailableTask.ajax.reload(null, false);
  
            toastr.error(data.message);
        }
    });
}