var dataTableP;

//specify when document is fully loaded.
$(document).ready(function () {
    loadDataTableProject();
});

//define loadDataTableMaterial() function
function loadDataTableProject() {
    //create DOM element as a datatable type
    dataTableM = $('#tblProject').DataTable({
        "ajax": { url: '/Manager/Project/GetAll' },
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.24/i18n/Vietnamese.json"
        },
        "columns": [
            {
                data: 'id',
                "render": function (data) {
                    return `<a class="text-main text-pointer">${data}</a>`
                }, 
            },
            { data: 'name',  },
            { data: 'location',  },
            { data: 'scale', },
            { data: 'size',  },
            { data: 'description',  },
            { data: 'overview', },
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
            { data: 'customer.name',},
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       <a href="/Manager/Project/Edit?id=${data}" class = "btn btn-primary btn-main border-0 m-1 text-nowrap"><i class="bi bi-pencil"></i> Chỉnh sửa </a>
                       <a href="/Manager/Project/Delete?id=${data}" class = "btn btn-danger border-0 m-1 text-nowrap"><i class="bi bi-trash"></i> Xóa </a>
                    </div >`
                },
                
            }
        ]
    });
}