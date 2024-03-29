var dataTableMDRS;

$(document).ready(function () {
    //chạy datatable
    loadDataMaterialDetail();

});

function InputNoteMaterialEvent(url, materialId, quantity) {
    var typingTimer
    clearTimeout(typingTimer); // Xóa bỏ bất kỳ setTimeout đang chờ thực hiện động còn đang chạy

    // Lấy giá trị của textarea và nội dung của thẻ p
    var note = document.getElementById(`${materialId}`).value;
    //console.log(note);
    typingTimer = setTimeout(function () {

        // Thực hiện cuộc gọi AJAX
        $.ajax({
            url: url,
            method: 'POST',
            data: { materialId: materialId, note: note, quantity: quantity },
            success: function () {
                //console.log(response.add);
                dataTableMDRS.ajax.reload(null, false);
            },
            error: function (xhr, status, error) {
                console.error('Ajax call failed:', error);
            }
        });

        //console.log(materialId);
        //console.log(quantity);
        //console.log(note);

    }, 100);

}

function formatCurrency(data, type, row) {
    // Kiểm tra nếu dữ liệu không phải là số thì trả về dữ liệu nguyên thô
    if (type !== 'display' || isNaN(data)) {
        return data;
    }

    // Sử dụng hàm toLocaleString để định dạng giá tiền theo ngôn ngữ và quốc gia hiện tại
    return Number(data).toLocaleString('vi-VN', {
        style: 'currency',
        currency: 'VND'
    });
}

function loadDataMaterialDetail() {
    dataTableMDRS = $('#tblMaterialDetail').DataTable({
        "ajax": { url: '/Manager/MaterialDetail/GetDetail' },
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.24/i18n/Vietnamese.json"
        },
        "columns": [
            {
                data: "materialId",
                "render": function (data) {
                    return `<a class="text-main text-pointer" onClick="ShowMaterialDetail('/Manager/MaterialDetail/Detail?MaterialId=${data}')" >${data}</a>`
                },
            },
            { data: 'materialName' },
            { data: 'materialCateName' },
            {
                data: null,
                "render": function (data) {
                    return `<p>${data.quantity}</p>`
                }
            },
            { data: 'price', render: formatCurrency },
            { data: 'unit' },
            {
                data: null,
                "render": function (data) {
                    return `<textarea class="form-control text-area-here" row="4"
                    onChange="InputNoteMaterialEvent('TakeNoteMaterialToSession','${data.materialId}', '${data.quantity}')" id=${data.materialId}>${data.note.value.note ?? ""}</textarea>`
                },
            }
        ]
    });
}





