$(document).ready(function () {
    loadDataTableRequest();
});

//Need an api method return json to use this
function loadDataTableRequest() {
    dataTable = $('#tblRequestCompleted').DataTable({
        "ajax": { url: '/Seller/Request/GetAllRequestCompleted' },
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
            {
                data: 'status',
                "render": function (data) {
                    if (data === true) {
                        return 'Processing';
                    } else {
                        return 'Completed';
                    }
                },
                "width": "15%"
            },
            { data: 'description', "width": "25%" },
        ]
    });
}