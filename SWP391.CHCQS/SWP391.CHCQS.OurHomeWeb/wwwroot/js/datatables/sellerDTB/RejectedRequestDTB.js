﻿$(document).ready(function () {
    loadDataTableRequest();
});

//Need an api method return json to use this
function loadDataTableRequest() {
    dataTable = $("#tblRequestRejected").DataTable({
        "ajax": { url: '/Seller/Request/GetAllRequestRejected' },
        "columns": [
            { data: 'id', "width": "5%" },
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
                "width": "15%"
            },
            { data: 'cusName', "width": "15%" },
            { data: 'cusGender', "width": "15%" },
            { data: 'cusPhone', "width": "15%" },
            { data: 'cusEmail', "width": "15%" },
            { data: 'constructType', "width": "15%" },
            { data: 'acreage', "width": "15%" },
            { data: 'location', "width": "15%" },
            {
                data: 'status', "width": "15%"
            },
            { data: 'description', "width": "25%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a href="/Seller/Request/UndoRejectRequest?id=${data}" class = "btn btn-primary btn-main border-0 m-1"><i class="bi bi-plus-square"></i> Undo Reject Request</a>
                    </div >`
                },
                "width": "35%"
            }
        ]
    });
}