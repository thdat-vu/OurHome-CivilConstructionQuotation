var dataTableCQ;
$(document).ready(function () {
    loadDataTableCustomQuotationManager();
});

//Need an api method return json to use this
function loadDataTableCustomQuotationManager() {
    dataTableCQ = $('#tblCustomQuotation').DataTable({
        "ajax": { url: '/Manager/CustomQuotation/GetAll?filterStatus=3' },
        "order": [[1, "desc"]],
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.24/i18n/Vietnamese.json"
        },
        "columns": [
            { data: 'id'},
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
               
            },
            { data: 'acreage',  },
            { data: 'location',  },
            { data: 'construcType' },
            { data: 'status'},
            { data: 'sellerName' },
            { data: 'engineerName' },
            { data: 'managerName'},
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a href="/Manager/CustomQuotation/GetDetail?id=${data}" class = "btn btn-main border-0 m-1 text-nowrap"><i class="bi bi-eye"></i> Chi tiết</a>
                    </div >`
                },
            
            }
        ]
    });
}

