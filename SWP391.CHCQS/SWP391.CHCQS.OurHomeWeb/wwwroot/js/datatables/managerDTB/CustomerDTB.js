var dataTableCM


$(document).ready(function () {
    loadDataCustomer();
});


//change name method - remember
//Need an api method return json to use this
function loadDataCustomer() {
    dataTableCM = $('#tblCustomer').DataTable({
        "ajax": { url: '/Manager/Customer/GetAll' },
        "language": {
            "url": "https://cdn.datatables.net/plug-ins/1.10.24/i18n/Vietnamese.json"
        },
        "columns": [
            {
                data: 'id',
                
                
                
            },
            { data: 'name', },
            { data: 'phoneNumber', },
            {
                data: null,
                render: function (data) {
                    return `<div class="text-center">
                        <a class="btn btn-main" onclick="fillForm('${data.id}',' ${data.name}')"> Thêm </a>
                    </div>`
                }
            }
            
        ]
    });
}

function fillForm(id, name) {
    var idField = document.getElementById('CustomerId');
    var nameField = document.getElementById('CustomerName');
    idField.value = id;
    nameField.value = name;
    idField.ajax.reload(null, false);
    nameField.ajax.reload(null,false);
}
