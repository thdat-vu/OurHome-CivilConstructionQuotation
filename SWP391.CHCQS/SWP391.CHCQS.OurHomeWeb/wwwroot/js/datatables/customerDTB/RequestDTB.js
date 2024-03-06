
$(document).ready(function () {
    loadDataTableRequest();
});

//Need an api method return json to use this
function loadDataTableRequest() {
    dataTable = $('#tblRequestHistory').DataTable({
        "ajax": { url: '/Customer/Request/GetRequestHistory' },
        "columns": [
            { data: 'numberOfOrder', "width": "5%" },
            { data: 'generateDate', "width": "10%" },   
            { data: 'constructType', "width": "10%" },
            { data: 'acreage', "width": "10%" },
            { data: 'location', "width": "15%" },
            { data: 'status', "width": "15%"},
            { data: 'description', "width": "25%" },
            {
                data: 'requestId',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a href="/Customer/Request/ViewResponse?id=${data}" class = "btn btn-primary btn-main border-0 m-1"><i class="bi bi-plus-square"></i> Response</a>
                    </div >`
                },
                "width": "10%"
            }
        ]
    });
}