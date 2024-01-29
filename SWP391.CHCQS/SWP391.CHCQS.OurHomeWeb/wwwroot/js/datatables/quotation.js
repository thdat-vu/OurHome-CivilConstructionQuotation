var dataTable;
$(document).ready(function () {
    loadDataTable();
});

//From dataTable
//Need API method to run this
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/Admin/Products/GetAll' },
        "columns": [
            { data: 'productTitle', "width": "20%" },
            { data: 'isbn', "width": "20%" },
            { data: 'listPrice', "width": "10%" },
            { data: 'author', "width": "15" },
            { data: 'category.categoryName', "width": "10%" },
            {
                data: 'productId',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/Admin/Products/Upsert?productId=${data}" class = "btn btn-primary mx-2"><i class="bi bi-pencil-square"></i>Edit</a>
                        <a onClick=Delete('/Admin/Products/Delete?productId=${data}') class = "btn btn-danger mx-2"><i class="bi bi-trash-fill"></i>Delete</a>
                    </div >`
                },
                "width": "15%"
            }
        ]
    });
}


//From Sweet Alert 2
function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    //From toastr message
                    toastr.success(data.message);
                }
            })
        }
    });
}