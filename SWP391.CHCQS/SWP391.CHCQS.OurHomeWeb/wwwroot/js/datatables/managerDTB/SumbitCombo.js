var ComboData;


function submitData() {
    comboData = {
        Name: $('#Combo_Name').val(),
        Description: $('#Combo_Description').val(),
        Price: $('#Combo_Price').val(),
        ConstructionId: $('#Combo_ConstructionId').val()
        // Add other properties as needed
    };

    // Gather data from the first data table (ComboTaskList)
    var comboTaskListData = $('#tblComboTaskList').DataTable().rows().data().toArray();

    // Gather data from the second data table (ComboMaterialList)
    var comboMaterialListData = $('#tblComboMaterialList').DataTable().rows().data().toArray();

    var postData = {
        Combo: comboData,
        ComboTaskList: comboTaskListData,
        ComboMaterialList: comboMaterialListData
    };

    $.ajax({
        url: '/Manager/Combo/Create',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(postData),
        success: function (response) {
            // Handle success
        },
        error: function (xhr, status, error) {
            // Handle error
        }
    });
}