$(document).ready(function () {
    loadDataMaterialDetail();
});


//change name method - remember
//Need an api method return json to use this
function loadDataMaterialDetail() {
    dataTable = $('#tblMaterialDetail').DataTable({
        "ajax": { url: '/Manager/MaterialDetail/GetDetail' },
        "columns": [
            { data: "materialId", "width": "5%" },
            { data: 'materialName', "width": "15%" },
            { data: 'materialCateName', "width": "15%" },
            { data: 'quantity', "width": "15%" },
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
