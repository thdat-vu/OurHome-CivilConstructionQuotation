"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notificationServer").build();

connection.start();

connection.on("RecieveQuotationFromEngineer", function (user, message) {
    //Your datatable need to reload
    /*dataTableCQ.ajax.reload();*/
    /*toastr.success(`${user}: ${message}`);*/
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


