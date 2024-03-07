

var dataTableCQ;
$(document).ready(function () {
    loadDataMaterialDetail();
});


//change name method - remember
//Need an api method return json to use this
function loadDataMaterialDetail() {
    dataTableCQ = $('#tblMaterialDetail').DataTable({
        "ajax": { url: '/Manager/MaterialDetail/GetDetail' },
        "columns": [
            {
                data: "materialId",
                "render": function (data) {
                    return `<a class="text-main text-pointer" onClick="ShowMaterialDetail('/Engineer/Material/Detail?MaterialId=${data}')" >${data}</a>`
                },
       
            },
            { data: 'materialName'},
            { data: 'materialCateName' },
            {
                data: null,
                "render": function (data) {
                    return `<p>${data.note.value.quantity ?? 0}</p>`
                },
            },
            { data: 'price'},
            { data: 'unit' },
            {
                data: null,
                "render": function (data) {
                    // Generate a unique form ID using the material ID
                    //var formId = `takeNoteMaterial${data.materialId}`;
                    var noteHere = data.note.value.note;
                    return `<form class="text-nowrap" action="/Manager/CustomQuotation/TakeNoteMaterial" id="takeNoteMaterial${data.materialId}" method="post">
                       <textarea name="materialNote" placeholder="${noteHere}" class="form-control" id="textAreaExample1" rows="4"></textarea>
                        <input type="text" name="materialId" hidden value="${data.materialId}"/>
                        <input type="text" name="quantity" hidden value="${data.quantity}"/>
                        <button type="submit" class="btn-main text-nowrap border-0 rounded p-1 w-100">
                         <i class="bi bi-check-lg">Note</i>
                        </button>
                    </form>`
                },
            }
        ]
    });
}

//function takeNoteMaterial(url, formId, quoteId) {
//    var formData = $('#' + formId).serialize();
//    $.ajax({
//        url: url,
//        type: 'POST',
//        data: formData,
//        success: function (data) {
//            if (!data.success) {
//                $(window).location.href = '/Manager/Home';
//                toastr.error(data.message);
//            } else {
//                $(window).location.href = '/Manager/CustomQuotation/GetDetail?id=' + quoteId;
//                toastr.success(data.message);
//            }
//        },
//        error: function (data) {
//            dataTableCQ.ajax.reload();
//            toastr.error(data.message);
//        }
//    });
//};


