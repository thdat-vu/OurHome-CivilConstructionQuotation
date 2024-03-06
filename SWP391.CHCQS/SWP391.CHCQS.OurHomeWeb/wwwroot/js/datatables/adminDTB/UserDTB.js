
$(document).ready(function () {
    loadDataTableRequest();
});

//Need an api method return json to use this
function loadDataTableRequest() {
    dataTable = $('#tblUser').DataTable({
        "ajax": { url: '/admin/user/getall' },
        "columns": [
            { data: 'name', "width": "10%" },   
            { data: 'email', "width": "15%" },
            { data: 'gender', "width": "10%" },
            { data: 'phoneNumber', "width": "15%" },
            { data: 'manager.name', "width": "15%" },
            { data: '', "width": "15%"},
            {
                data: 'requestId',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a href="/Customer/Request/ViewResponse?id=${data}" class = "btn btn-primary btn-main border-0 m-1"><i class="bi bi-plus-square"></i> Response</a>
                    </div >`
                },
                "width": "20%"
            }
        ]
    });
}