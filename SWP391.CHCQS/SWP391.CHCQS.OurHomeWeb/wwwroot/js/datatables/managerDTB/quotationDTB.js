


$(function () {
    dataTable = $('#tblQuotation').DataTable({
        "ajax": { url: '/Manager/Dashboard/GetAllQuote' },
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.24/i18n/Vietnamese.json"
        },
        "order": [[1, "desc"]], //sắp xếp theo cột thứ 2(cột ngày giảm dần)
        "pageLength": 5,
        "lengthChange": false,
        "info": true,
        "columns": [
            { data: 'id',  },
            { data: 'date',  },
            { data: 'acreage',  },
            { data: 'location',  },
            { data: 'description',  },
            {
                data: 'status', 
            }

        ]
    });
})
