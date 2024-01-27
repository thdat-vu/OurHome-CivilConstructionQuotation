$(document).ready(function () {
    loadDataTable();
});

//Need an api method return json to use this
function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/Engineer/Quotation/GetAll' },
        "columns": [
            { data: 'productTitle', "width": "20%" },
            { data: 'isbn', "width": "20%" },
            { data: 'listPrice', "width": "15%" },
            { data: 'author', "width": "15%" },
            { data: 'category.categoryName', "width": "15%" }
        ]
    });
}