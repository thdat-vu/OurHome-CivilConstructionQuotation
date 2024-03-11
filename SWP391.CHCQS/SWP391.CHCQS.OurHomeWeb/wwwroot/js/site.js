// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//This function handle when user click back to index but not save the working session.
//This function will show a box to ask use for comfirm action before execute.
function BackToIndex(url) {
    Swal.fire({
        title: "Bạn có chắc chắn không?",
        text: "Bất kì thay đổi nào sẽ không được lưu lại!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#F27456",
        cancelButtonColor: "#d33",
        confirmButtonText: "Vâng, tôi hiểu!",
        cancelButtonText: "Hủy",
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = url;
        }
    });
}

//This function handle show TaskDetail when click on TaskId
function ShowTaskDetail(url) {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (data) {
            Swal.fire({
                icon: "info",
                html: `<div class="container">
        <div class="card">
            <div class="card-body">
                <ul class="list-group">
                    <li class="list-group-item"><strong>Mã:</strong> ${data.data.id}</li>
                    <li class="list-group-item"><strong>Tên:</strong> ${data.data.name}</li>
                    <li class="list-group-item"><strong>Mô tả:</strong> ${data.data.description}</li>
                    <li class="list-group-item"><strong>Giá gốc:</strong> ${data.data.unitPrice}</li>
                    <li class="list-group-item"><strong>Mã loại Id:</strong> ${data.data.categoryId}</li>
                    <li class="list-group-item"><strong>Loại:</strong> ${data.data.categoryName}</li>
                </ul>
            </div>
        </div>
    </div>
`,
                focusConfirm: false,
             
            });
        },
    });
}

//This fuction handle show MaterialDetail when click on MaterialId
function ShowMaterialDetail(url) {
    $.ajax({
        type: 'GET',
        url: url,
        success: function (data) {
            Swal.fire({
                icon: "info",
                html: `<div class="container">
        <div class="card">
            <div class="card-body">
                <ul class="list-group">
                    <li class="list-group-item"><strong>Mã:</strong> ${data.data.id}</li>
                    <li class="list-group-item"><strong>Tên:</strong> ${data.data.name}</li>
                    <li class="list-group-item"><strong>Mô tả:</strong> ${data.data.description}</li>
                    <li class="list-group-item"><strong>Giá gốc:</strong> ${data.data.unitPrice}</li>
                    <li class="list-group-item"><strong>Đơn vị:</strong> ${data.data.unit}</li>
                    <li class="list-group-item"><strong>Mã loại:</strong> ${data.data.categoryId}</li>
                    <li class="list-group-item"><strong>Loại:</strong> ${data.data.categoryName}</li>
                </ul>
            </div>
        </div>
    </div>
`,
               
                focusConfirm: false,
            });
        },
    });
}

