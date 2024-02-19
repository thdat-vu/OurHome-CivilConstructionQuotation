$(document).ready(function () {
    loadDataTableRequest();
});

//Need an api method return json to use this
function loadDataTableRequest() {
    dataTable = $('#tblRequest').DataTable({
        "ajax": { url: '/Seller/Request/GetAll' },
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'generateDate', "width": "15%" },
            { data: 'cusName', "width": "15%" },
            { data: 'cusGender', "width": "15%" },
            { data: 'cusPhone', "width": "15%" },
            { data: 'cusEmail', "width": "15%" },   
            { data: 'constructType', "width": "15%" },
            { data: 'acreage', "width": "15%" },
            { data: 'location', "width": "15%" },  
            { data: 'description', "width": "5%" },
            { data: 'status', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a href="/Engineer/Task/AddToQuote?TaskId=${data}" class = "btn btn-primary btn-main border-0 m-1"><i class="bi bi-plus-square"></i> Add</a>
                    </div >`
                },
                "width": "15%"
            }
        ]
    });
}