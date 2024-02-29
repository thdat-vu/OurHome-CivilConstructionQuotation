"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/signalServer").build();

connection.start();

connection.on("refreshCustomQuotations", function () {
    dataTableCQ.ajax.reload();
    dataTableCQ.ajax.reload();
    dataTableCQ.ajax.reload();
    toastr.success('Submit Quote Successfully');
    console.log("Reloaded");
})

