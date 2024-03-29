var dataTable;
$(document).ready(function () {
    loadDataTableRequest();
});

//Need an api method return json to use this
function loadDataTableRequest() {
    dataTable = $('#tblUser').DataTable({
        "ajax": { url: '/admin/user/getall' },
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.24/i18n/Vietnamese.json"
        },
        "columns": [
            { data: 'name' },   
            { data: 'email'},
            { data: 'userName'},
            { data: 'phoneNumber'},
            { data: 'manager.name'},
            { data: 'role'},
            {
                data: { id: "id", lockoutEnd: "lockoutEnd", role: "role"},
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();
                    if (lockout > today && data.role != "Admin") {
                        // user is currently locked
                        return `
                            <div class="w-100 btn-group" role="group">
                                <a onclick=LockUnlock('${data.id}') class = "text-nowrap btn btn-danger border-0 m-1">
                                    <i class="bi bi-lock-fill"></i> Khóa
                                </a>
                                <a href="/admin/user/RoleManagement?userId=${data.id}" class = "text-nowrap btn btn-danger border-0 m-1">
                                    <i class="bi bi-pencil-square"></i> Phân quyền
                                </a>
                            </div>
                        `;
                    }
                    else if (data.role != "Admin") {
                        return `
                            <div class="w-100 btn-group" role="group">
                                <a onclick=LockUnlock('${data.id}') class = " text-nowrap btn btn-success border-0 m-1">
                                    <i class="bi bi-unlock-fill"></i> Mở khóa
                                </a>
                                <a href="/admin/user/RoleManagement?userId=${data.id}" class = "text-nowrap btn btn-danger border-0 m-1">
                                    <i class="bi bi-pencil-square"></i> Phân quyền
                                </a>
                            </div>
                        `;
                    }
                    else {
                        return '';
                    }
                },
            }
        ]
    });
}

function LockUnlock(id) {
    $.ajax({
        type: "POST", 
        url: '/Admin/User/LockUnlock',
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            }
            else {
                toastr.error(data.message);
            }
        }
    });
}