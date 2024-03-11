
$('#save_note').on('click', function () {
    var saveNoteActionUrl = '/Manager/CustomQuotation/SaveNote';
    //console.log("save nè");
    $.ajax({
        url: saveNoteActionUrl,
        method: 'GET',
        success: function (response) {
            if (response.isSuccess)
                toastr.success(response.message);
            else
                toastr.error(response.message);
        },
        error: function (xhr, status, error) {
            console.log(error);
            toastr.error("Call Server Fail");
        }
    })
});
