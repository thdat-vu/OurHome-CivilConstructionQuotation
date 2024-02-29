$(document).ready(function () {
    loadDataTableCustomQuotationTaskHistory();
});

//Need an api method return json to use this
function loadDataTableCustomQuotationTaskHistory() {
    dataTableCQT = $('#tblCustomQuotationTask').DataTable({
        "ajax": { url: '/Engineer/Task/GetTaskListHistory' },
        "columns": [
            {
                data: 'task.id',
                "render": function (data) {
                    return `<a class="text-main text-pointer" onClick="ShowTaskDetail('/Engineer/Task/Detail?TaskId=${data}')" >${data}</a>`
                },
            },
            { data: 'task.name', },
            { data: 'task.unitPrice', },
            { data: 'price', },
            { data: 'task.categoryId', },
        ]
    });
}