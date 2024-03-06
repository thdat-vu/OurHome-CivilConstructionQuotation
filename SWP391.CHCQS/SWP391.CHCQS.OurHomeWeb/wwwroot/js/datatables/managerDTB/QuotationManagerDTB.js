var dataTableCQ;
$(document).ready(function () {
    loadDataTableCustomQuotationManager();
});

//Need an api method return json to use this
function loadDataTableCustomQuotationManager() {
    dataTableCQ = $('#tblCustomQuotation').DataTable({
        "ajax": { url: '/Manager/CustomQuotation/GetAll' },
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'generatRequestDate', "width": "15%" },
            { data: 'acreage', "width": "15%" },
            { data: 'location', "width": "5%" },
            { data: 'construcType', "width": "5%" },
            { data: 'status', "width": "5%" },
            { data: 'sellerName', "width": "15%" },
            { data: 'engineerName', "width": "15%" },
            { data: 'managerName', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a href="/Manager/CustomQuotation/GetDetail?id=${data}" class = "btn btn-primary btn-main border-0 m-1"><i class="bi bi-plus-square"></i>Detail</a>
                    </div >`
                },
                "width": "15%"
            }
        ]
    });
} 