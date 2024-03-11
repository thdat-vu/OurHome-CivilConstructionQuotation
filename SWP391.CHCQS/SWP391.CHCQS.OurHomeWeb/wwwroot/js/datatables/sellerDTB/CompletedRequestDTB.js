$(document).ready(function () {
    loadDataTableRequest();
});

//Need an api method return json to use this
function loadDataTableRequest() {
    dataTable = $('#tblRequestCompleted').DataTable({
        "ajax": { url: '/Seller/Request/GetAllRequestCompleted' },
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.24/i18n/Vietnamese.json"
        },
        "columns": [    
            { data: 'id'},
            {
                data: 'generateDate',
                "render": function (data) {
                    // Chuyển đổi ngày thành chuỗi định dạng dd/MM/yyyy
                    let date = new Date(data);
                    let day = ("0" + date.getDate()).slice(-2);
                    let month = ("0" + (date.getMonth() + 1)).slice(-2);
                    let year = date.getFullYear();
                    return `${day}/${month}/${year}`;
                },
            },
            { data: 'cusName'},
            { data: 'cusGender'},
            { data: 'cusPhone'},
            { data: 'cusEmail' },
            { data: 'constructType'},
            { data: 'acreage'},
            { data: 'location' },
            {
                data: 'status'
            },
            { data: 'description'},
        ]
    });
}