var dataTable;
$(document).ready(function () {
    loadDataTableRequest();
});

//Need an api method return json to use this
function loadDataTableRequest() {
    dataTable = $('#tblUser').DataTable({
        "ajax": { url: '/admin/user/getall' },
        "columns": [
            { data: 'name', "width": "10%" },   
            { data: 'email', "width": "15%" },
            { data: 'userName', "width": "10%" },
            { data: 'phoneNumber', "width": "15%" },
            { data: 'manager.name', "width": "15%" },
            { data: 'role', "width": "15%" },
            {
                data: { id: "id", lockoutEnd: "lockoutEnd" },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();
                    if (lockout > today) {
                        // user is currently locked
                        return `
                            <div class="text-center d-flex">
                                <a onclick=LockUnlock('${data.id}') class = "btn btn-danger text-white mx-1" style="cursor:pointer; width:100px;">
                                    <i class="bi bi-lock-fill"></i> Lock
                                </a>
                                <a href="/admin/user/RoleManagement?userId=${data.id}" class = " btn btn-danger text-white mx-1" style="cursor:pointer; width:150px;">
                                    <i class="bi bi-pencil-square"></i> Permission
                                </a>
                            </div>
                        `;
                    }
                    else {
                        return `
                            <div class="text-center d-flex">
                                <a onclick=LockUnlock('${data.id}') class = " btn btn-success text-white mx-1" style="cursor:pointer; width:100px;">
                                    <i class="bi bi-unlock-fill"></i> Unlock
                                </a>
                                <a href="/admin/user/RoleManagement?userId=${data.id}" class = " btn btn-danger text-white mx-1" style="cursor:pointer; width:150px;">
                                    <i class="bi bi-pencil-square"></i> Permission
                                </a>
                            </div>
                        `;
                    }
                },
                "width": "20%"
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