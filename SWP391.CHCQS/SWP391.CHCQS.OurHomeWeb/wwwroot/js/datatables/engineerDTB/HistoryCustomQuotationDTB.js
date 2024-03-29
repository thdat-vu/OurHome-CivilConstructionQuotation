var dataTableCQ;
$(document).ready(function () {
    loadDataTableHistoryQuotation();
});

//Need an api method return json to use this
function loadDataTableHistoryQuotation() {
    dataTableCQ = $('#tblCustomQuotation').DataTable({
        "ajax": { url: '/Engineer/Quotation/GetHistory' },
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
                        <a href="/Engineer/Quotation/Detail?QuotationId=${data}" class="text-nowrap btn btn-primary btn-main border-0 m-1"><i class="bi bi-eye"></i> Chi tiết</a>
                    </div >`
                },
            }
        ]
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