﻿var dataTableCQ;
$(document).ready(function () {
    loadDataTableQuotation();
});

//Need an api method return json to use this
function loadDataTableQuotation() {
    dataTableCQ = $('#tblCustomQuotation').DataTable({
        "ajax": { url: '/Engineer/Quotation/GetRejected' },
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.24/i18n/Vietnamese.json"
        },
        "order": [[1, "desc"]], //sắp xếp theo cột thứ 2(cột ngày giảm dần)
        "columns": [
            { data: 'id', },
            {
                data: 'date',
                "render": function (data) {
                    // Chuyển đổi ngày thành chuỗi định dạng dd/MM/yyyy
                    let date = new Date(data);
                    let day = ("0" + date.getDate()).slice(-2);
                    let month = ("0" + (date.getMonth() + 1)).slice(-2);
                    let year = date.getFullYear();
                    return `${day}/${month}/${year}`;
                },
            },
            { data: 'acreage', },
            { data: 'location', },
            { data: 'total', render: formatCurrency },
            { data: 'status', },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                        <a href="/Engineer/Quotation/Retake?QuotationId=${data}" class="text-nowrap btn btn-primary btn-main border-0 m-1"><i class="bi bi-folder2-open"></i> Báo giá</a>
                        <a onClick="SendQuoteToManager('/Engineer/Quotation/SendQuoteToManager?QuotationId=${data}')" class="text-nowrap btn btn-primary m-1"><i class="bi bi-pencil-square"></i> Gửi</a>
                    </div >`
                },
            }
        ]
    });
}

function SendQuoteToManager(url) {
    Swal.fire({
        title: "Bạn có chắc chắn muốn gửi cho Quản Lý hay không?",
        text: "Bạn sẽ không được chỉnh sửa nữa!",
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#F27456",
        cancelButtonColor: "#d33",
        confirmButtonText: "Vâng, cứ gửi!",
        cancelButtonText: "Hủy",
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'GET',
                success: function (data) {
                    if (!data.success) {
                        dataTableCQ.ajax.reload(null, false);
                        toastr.error(data.message);
                    }
                    else {
                        dataTableCQ.ajax.reload(null, false);
                        toastr.success(data.message);
                    }
                },
                error: function (data) {
                    dataTableCQ.ajax.reload();
                    toastr.error(data.message);
                }
            });
        }
    });
}

// Hàm định dạng giá tiền
function formatCurrency(data, type, row) {
    // Kiểm tra nếu dữ liệu không phải là số thì trả về dữ liệu nguyên thô
    if (type !== 'display' || isNaN(data)) {
        return data;
    }

    // Sử dụng hàm toLocaleString để định dạng giá tiền theo ngôn ngữ và quốc gia hiện tại
    return Number(data).toLocaleString('vi-VN', {
        style: 'currency',
        currency: 'VND'
    });
}