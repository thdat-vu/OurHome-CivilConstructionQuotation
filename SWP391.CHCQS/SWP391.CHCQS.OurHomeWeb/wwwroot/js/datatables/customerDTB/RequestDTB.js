
$(document).ready(function () {
    loadDataTableRequest();
});

//Need an api method return json to use this
function loadDataTableRequest() {
    dataTable = $('#tblRequestHistory').DataTable({
        "ajax": { url: '/Customer/Request/GetRequestHistory' },
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.24/i18n/Vietnamese.json"
        },
        "columns": [
            { data: 'numberOfOrder', "width": "5%" },
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
                "width": "10%"
            },
            { data: 'constructType', "width": "10%" },
            { data: 'acreage', "width": "10%" },
            { data: 'location', "width": "15%" },
            { data: 'status', "width": "15%" },
            { data: 'description', "width": "25%" },
            {
                data: 'requestId',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a href="/Customer/Request/ViewResponse?id=${data}" class = "text-nowrap btn btn-primary btn-main border-0 m-1"><i class="bi bi-eye"></i> Xem kết quả</a>
                    </div >`
                },
                "width": "10%"
            }
        ]
    });
}