"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notificationServer").build();

// Start the connection
connection.start().then(function () {
    // After the connection is started, get the connection ID
//    var connectionId = connection.connectionId;

//    // Send the connection ID to the server to store in the database
//    $.ajax({
//        type: "POST",
//        url: "/Base/SaveConnectionId", // Adjust the URL to your server-side endpoint
//        data: { connectionId: connectionId },
//        success: function (response) {
//            console.log("Connection ID saved to the database:", connectionId);
//        },
//        error: function (error) {
//            console.error("Error saving connection ID to the database:", error);
//        }
//    });
//}).catch(function (error) {
//    console.error("Error starting SignalR connection:", error);
});

connection.on("RecieveQuotationFromEngineer", function (user, message) {
    //Your datatable need to reload
    /*dataTableCQ.ajax.reload();*/
    /*toastr.success(`${user}: ${message}`);*/
    dataTableCQ.ajax.reload();
    console.log("Reload" + user + message);
});

connection.on("RecieveQuotationFromSeller", function (user, message) {
    //Your datatable need to reload
    //dataTableCQ.ajax.reload();
    //toastr.success(`${user}: ${message}`);
    console.log("Reload" + user + message);
});

connection.on("RecieveRequestFromCustomer", function (user, message) {
    //Your datatable need to reload
    //dataTableCQ.ajax.reload();
    //toastr.success(`${user}: ${message}`);
    console.log("Reload" + user + message);
});


