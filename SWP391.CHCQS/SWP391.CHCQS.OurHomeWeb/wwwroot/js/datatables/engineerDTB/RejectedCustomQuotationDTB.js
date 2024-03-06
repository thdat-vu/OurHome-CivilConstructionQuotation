var dataTableCQ;
$(document).ready(function () {
    loadDataTableQuotation();
});

//Need an api method return json to use this
function loadDataTableQuotation() {
    dataTableCQ = $('#tblCustomQuotation').DataTable({
        "ajax": { url: '/Engineer/Quotation/GetRejected' },
        "columns": [
            { data: 'id', },
            { data: 'date', },
            { data: 'acreage', },
            { data: 'location', },
            { data: 'total', },
            { data: 'status', },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                        <a href="/Engineer/Quotation/Quote?QuotationId=${data}" class="text-nowrap btn btn-primary btn-main border-0 m-1"><i class="bi bi-folder2-open"></i> Quote</a>
                        <a onClick="SendQuoteToManager('/Engineer/Quotation/SendQuoteToManager?QuotationId=${data}')" class="text-nowrap btn btn-primary m-1"><i class="bi bi-pencil-square"></i> Send</a>
                    </div >`
                },
            }
        ]
    });
}

function SendQuoteToManager(url) {
    Swal.fire({
        title: "Are want to send to Manager?",
        text: "You won't be able to edit this!",
        icon: "question",
        showCancelButton: true,
        confirmButtonColor: "#F27456",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, Send it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'GET',
                success: function (data) {
                    if (!data.success) {
                        dataTableCQ.ajax.reload();
                        toastr.error(data.message);
                    }
                    else {
                        dataTableCQ.ajax.reload();
                        // From toastr message
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