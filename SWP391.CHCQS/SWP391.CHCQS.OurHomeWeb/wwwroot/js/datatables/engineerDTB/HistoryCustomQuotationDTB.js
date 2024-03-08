var dataTableCQ;
$(document).ready(function () {
    loadDataTableHistoryQuotation();
});

//Need an api method return json to use this
function loadDataTableHistoryQuotation() {
    dataTableCQ = $('#tblCustomQuotation').DataTable({
        "ajax": { url: '/Engineer/Quotation/GetHistory' },
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
            { data: 'total', },
            { data: 'status', },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                        <a href="/Engineer/Quotation/Detail?QuotationId=${data}" class="text-nowrap btn btn-primary btn-main border-0 m-1"><i class="bi bi-eye"></i> Detail</a>
                    </div >`
                },
            }
        ]
    });
}