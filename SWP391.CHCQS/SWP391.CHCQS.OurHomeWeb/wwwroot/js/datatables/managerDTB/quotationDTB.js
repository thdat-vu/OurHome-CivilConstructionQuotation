


$(function () {
    dataTable = $('#tblQuotation').DataTable({
        "ajax": { url: '/Manager/Dashboard/GetAllQuote' },
        "pageLength": 5,
        "lengthChange": false,
        "info": true,
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'date', "width": "15%" },
            { data: 'acreage', "width": "15%" },
            { data: 'location', "width": "15%" },
            { data: 'description', "width": "35%" },
            {
                data: 'status', "width": "15%"
            }

        ]
    });
})
