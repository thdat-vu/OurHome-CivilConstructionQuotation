var dataTableTDRS;
$(document).ready(function () {
    loadDataTaskDetail();
});

//change name method - remember
//Need an api method return json to use this
function loadDataTaskDetail() {
    dataTableTDRS = $('#tblTaskDetail').DataTable({
        "ajax": { url: '/Manager/CustomQuotationTask/GetDetail' },
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.24/i18n/Vietnamese.json"
        },
        "columns": [
            {
                data: "taskId",
                "render": function (data) {
                    return `<a class="text-main text-pointer" onClick="ShowTaskDetail('/Manager/CustomQuotationTask/Detail?TaskId=${data}')" >${data}</a>`
                },
            },
            { data: 'taskName' },
            { data: 'price', render: formatCurrency },
            {
                data: null,
                // Generate a unique form ID using the material ID
                "render": function (data) {
                    return `<textarea class="form-control" row="2"
                    onChange="InputNoteTaskEvent('/Manager/CustomQuotation/TakeNoteTaskToSession','${data.taskId}')" id=${data.taskId??""}>${data.note == null? "": data.note.value}</textarea>`
                },
            },
            
        ]
    });
}
function InputNoteTaskEvent(url, taskId) {
    var typingTimer
    clearTimeout(typingTimer); // Xóa bỏ bất kỳ setTimeout đang chờ thực hiện động còn đang chạy

    // Lấy giá trị của textarea và nội dung của thẻ p
    var note = document.getElementById(`${taskId}`).value;
    //console.log(note);
    typingTimer = setTimeout(function () {

        // Thực hiện cuộc gọi AJAX
        $.ajax({
            url: url,
            method: 'POST',
            data: { taskId: taskId, note: note},
            success: function () {
                //console.log(response.add);
                dataTableTDRS.ajax.reload();
            },
            error: function (xhr, status, error) {
                console.error('Ajax call failed:', error);
            }
        });
        //console.log(materialId);
        //console.log(quantity);
        //console.log(note);
    }, 100);
} 