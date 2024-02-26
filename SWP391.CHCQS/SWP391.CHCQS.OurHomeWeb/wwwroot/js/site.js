// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//This function handle when user click back to index but not save the working session.
//This function will show a box to ask use for comfirm action before execute.
function BackToIndex(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "Any unsaved information will be lost!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#F27456",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, I understand!"
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = url;
        }
    });
}