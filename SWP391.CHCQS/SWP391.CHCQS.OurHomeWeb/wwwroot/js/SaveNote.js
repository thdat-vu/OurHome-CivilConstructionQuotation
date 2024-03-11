
$('#save_note').on('click', function () {
    var saveNoteActionUrl = '/Manager/CustomQuotation/SaveNote';
    console.log("save nè");
    $.ajax({
        url: saveNoteActionUrl,
        method: 'GET',
        success: function (response) {
            if (response.isSuccess) {
                //gọi tới action thông báo thành công
            }
        },
        error: function (xhr, status, error) {
            //gọi ajax đến action thông báo lỗi
        }
    })
});
