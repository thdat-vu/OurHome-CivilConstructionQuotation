var dataTableAllCQ;
var dataTableALlRF;
function loadDataTableAllCustomQuotationManager() {
    dataTableAllCQ = $('#tblAllCustomQuotation').DataTable({
        "ajax": { url: '/Manager/CustomQuotation/GetAllQuote' },
        "order": [[1, "desc"]],
        "pageLength": 5,
        "lengthMenu": [5, 10, 15, 20],
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.24/i18n/Vietnamese.json"
        },
        "order": [[1, "desc"]], //sắp xếp theo cột thứ 2(cột ngày giảm dần)
        "columns": [
            { data: 'id' },
            {
                data: 'generatRequestDate',
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
            { data: 'acreage' },
            { data: 'location' },
            { data: 'construcType' },
            { data: 'status' },
            { data: 'sellerName' },
            { data: 'engineerName' },
            { data: 'managerName' },
            //{
            //    data: 'id',
            //    "render": function (data) {
            //        return `<div class="w-100 btn-group" role="group">
            //           <a href="/Manager/CustomQuotation/GetDetail?id=${data}" class = "btn btn-primary btn-main border-0 m-1"><i class="bi bi-plus-square"></i>Detail</a>
            //        </div >`
            //    },
            //    "width": "15%"
            //}
        ]
    });
}
function loadDataTableAllRequest() {
    dataTableALlRF = $('#tblAllRequest').DataTable({
        "ajax": { url: '/Base/Base/GetAllRequest' },
        "order": [[1, "desc"]],
        "pageLength": 5,
        "lengthMenu": [5, 10, 15, 20],
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.24/i18n/Vietnamese.json"
        },
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
            //{ data: 'cusGender', "width": "15%" },
            { data: 'cusPhone', "width": "15%" },
            { data: 'cusEmail', "width": "15%" },
            //{ data: 'constructType', "width": "15%" },
            //{ data: 'acreage', "width": "15%" },
            //{ data: 'location', "width": "15%" },
            {
                data: 'status'
            },
            {
                data: 'sellerName'
            },
            {
                data: 'engineerName'
            },
            {
                data: 'managerName'
            },
            //{ data: 'description', "width": "25%" },
            //{
            //    data: 'id',
            //    "render": function (data) {
            //        return `<div class="w-100 btn-group" role="group">
            //           <a href="/Seller/Quotation/CreateConstructDetails?id=${data}" class = "btn btn-primary btn-main border-0 m-1"><i class="bi bi-plus-square"></i> Create Construct Details</a>
            //           <a href="/Seller/Request/RequestReject?id=${data}" class = "btn btn-primary btn-danger border-0 m-1"><i class="bi bi-plus-square"></i> Reject Quotation</a>
            //        </div >`
            //    },
            //    "width": "35%"
            //}
        ]
    });
}

