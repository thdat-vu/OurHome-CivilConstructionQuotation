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
                    return `<a class="text-main text-pointer" onClick="ShowTaskDetail('/Engineer/Task/Detail?TaskId=${data}')" >${data}</a>`
                },
            },
            { data: 'taskName' },
            { data: 'price'},
            {
                data: "taskId",
                "render": function (data) {
                    // Generate a unique form ID using the material ID
                    var formId = 'updateQuantityForm' + data;

                    return `<form class="text-nowrap" id="${formId}" method="post">
                       <textarea name="${data}" placeholder="The reason in case of rejection" class="form-control" id="textAreaExample1" rows="4"></textarea>
                        <input type="text" name="MaterialId" hidden value="${data}">
                        <button class="btn-main text-nowrap border-0 rounded p-1 w-100 type="button" onclick="UpdateMaterialQuantity('/Engineer/Material/UpdateQuantity', '${formId}')">
                         <i class="bi bi-check-lg">Note</i>
                        </button>
                    </form>`
                },
            }
        ]
    });
}