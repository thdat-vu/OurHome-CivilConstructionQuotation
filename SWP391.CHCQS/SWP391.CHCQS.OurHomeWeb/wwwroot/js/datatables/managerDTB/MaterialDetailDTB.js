var dataTableCQ;

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
                dataTableCQ.ajax.reload();
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

function loadDataMaterialDetail() {
    dataTableCQ = $('#tblMaterialDetail').DataTable({
        "ajax": { url: '/Manager/MaterialDetail/GetDetail' },
        "columns": [
            {
                data: "materialId",
                "render": function (data) {
                    return `<a class="text-main text-pointer" onKeyUp="ShowMaterialDetail('/Engineer/Material/Detail?MaterialId=${data}')" >${data}</a>`
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
            { data: 'price' },
            { data: 'unit' },
            {
                data: null,
                "render": function (data) {
                    return `<textarea class="form-control" row="4"
                    onChange="InputNoteMaterialEvent('TakeNoteMaterialToSession','${data.materialId}', '${data.quantity}')" id=${data.materialId}>${data.note== null? "" : data.note.value.note }</textarea>`
                },
            }
        ]
    });
}





