$(document).ready(function () {
    loadDataTaskDetail();
});


//change name method - remember
//Need an api method return json to use this
function loadDataTaskDetail() {
    dataTable = $('#tblTaskDetail').DataTable({
        "ajax": { url: '/Manager/CustomQuotationTask/GetDetail' },
        "columns": [
            {
                data: "taskId",
                "render": function (data) {
                    return `<a href="/Manager/Task/Detail?id=${data}" >${data}</a>`
                },
                "width": "5%"
            },
            { data: 'taskName', "width": "15%" },
            { data: 'price', "width": "15%" },
            {
                data: "quoteId",
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a href="/Manager/CustomQuotation/GetDetail?id=${data}" class = "btn btn-primary btn-main border-0 m-1"><i class="bi bi-plus-square"></i>Delete</a>
                    </div >`
                },
                "width": "15%"
            }
        ]
    });
}