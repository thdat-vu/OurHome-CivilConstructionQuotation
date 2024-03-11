$(document).ready(function () {
    loadDataTableCustomQuotationTaskHistory();
});

//Need an api method return json to use this
function loadDataTableCustomQuotationTaskHistory() {
    dataTableCQT = $('#tblCustomQuotationTask').DataTable({
        "ajax": { url: '/Engineer/Task/GetTaskListHistory' },
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.24/i18n/Vietnamese.json"
        },
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