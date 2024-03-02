"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/notificationServer").build();

connection.start();

connection.on("refreshCustomQuotations", function () {
    dataTableCQ.ajax.reload();
    toastr.success('Quotation was refresh');
})

