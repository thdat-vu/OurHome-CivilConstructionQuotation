var dataTableTD;
$(document).ready(function () {
    loadDataTaskDetail();
});


//change name method - remember
//Need an api method return json to use this
function loadDataTaskDetail() {
    dataTableTD = $('#tblTaskDetail').DataTable({
        "ajax": { url: '/Manager/CustomQuotationTask/GetDetail' },
        "columns": [
            {
                data: "taskId",
                "render": function (data) {
                    return `<a class="text-main text-pointer" onClick="ShowTaskDetail('/Engineer/Task/Detail?TaskId=${data}')" >${data}</a>`
                },
            },
            { data: 'taskName' },
            { data: 'price'},
            {
                data: null,
                "render": function (data) {
                    // Generate a unique form ID using the material ID
                    var formId = 'takeNoteTask' + data.taskId;
                    var noteHere = data.note.value ?? "The reason in case of rejection";
                    return `<form class="text-nowrap" action="/Manager/CustomQuotation/TakeNoteTask" id="${formId}" method="post">
                       <textarea name="TaskNote" placeholder="${noteHere}" class="form-control" id="textAreaExample1" rows="4"></textarea>
                        <input type="text" name="TaskId" hidden value="${data.taskId}"/>
                        <button class="btn-main text-nowrap border-0 rounded p-1 w-100"/>
                         <i class="bi bi-check-lg">Note</i>
                        </button>
                    </form>`
                },
            }
        ]
    });
}

//function takeNoteTask(url, formId) {
//    var formData = $('#' + formId).serialize();
//    $.ajax({
//        url: url,
//        type: 'POST',
//        data: formData,
//        success: function (data) {
//            if (!data.success) {
//                dataTableTD.ajax.reload();
//                toastr.error(data.message);
//            } else {
//                dataTableTD.ajax.reload();
//                toastr.success(data.message);
//            }
//        },
//        error: function (data) {
//            dataTableTD.ajax.reload();
//            toastr.error(data.message);
//        }
//    });
//};