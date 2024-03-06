var dataTableCM


$(document).ready(function () {
    loadDataCustomer();
});


//change name method - remember
//Need an api method return json to use this
function loadDataCustomer() {
    dataTableCM = $('#tblCustomer').DataTable({
        "ajax": { url: '/Manager/Customer/GetAll' },
        "columns": [
            
            { data: 'id', "width": "30%" },
            { data: 'name', "width": "30%" },
           
            {
                data: "id",
                "render": function (data) {
                    return `<div class="w-100 btn-group" role="group">
                       
                       <button class="btn btn-primary btn-main border-0 m-1" onclick="fillFormFields(${data})">
                        <i class="bi bi-plus-square"></i>Add
                    </button>

                    </div >`
                },
                "width": "40%"
            }
        ]
    });
}

function fillFormFields(customerId) {
    // Fetch customer details based on the customerId using an API or other method
    $.ajax({
        url: `/Manager/Customer/GetCustomerDetails?id=${customerId}`,
        method: 'GET',
        success: function (data) {
            // Assuming the API returns JSON data with 'id' and 'name' properties
            var customer = data;

            // Fill in the form fields with customer details
            $('#Name').val(customer.name);
            $('#CustomerId').val(customer.id);
        },
        error: function (error) {
            console.error('Error fetching customer details:', error);
        }
    });
}


