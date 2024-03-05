using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWP391.CHCQS.Services.NotificationHub
{
    public class NotificationHub : Hub
    {
        public async Task SendQuotationToManager(string user, string message)
        {
            // Kiểm tra xem client có phải là người gửi không
            if (Context.ConnectionId != Context.ConnectionId)
            {
                // Gửi tin nhắn chỉ đến client khác, không bao gồm client gửi yêu cầu
                await Clients.AllExcept(Context.ConnectionId).SendAsync("RecieveQuotationFromEngineer", user, message);
            }
        }

        public async Task SendQuotationToEngineer(string user, string message)
        {
            // Kiểm tra xem client có phải là người gửi không
            if (Context.ConnectionId != Context.ConnectionId)
            {
                // Gửi tin nhắn chỉ đến client khác, không bao gồm client gửi yêu cầu
                await Clients.AllExcept(Context.ConnectionId).SendAsync("RecieveQuotationFromSeller", user, message);
            }
        }
        public async Task SendRequestToSeller(string user, string message)
        {
            // Kiểm tra xem client có phải là người gửi không
            if (Context.ConnectionId != Context.ConnectionId)
            {
                // Gửi tin nhắn chỉ đến client khác, không bao gồm client gửi yêu cầu
                await Clients.AllExcept(Context.ConnectionId).SendAsync("RecieveQuotationFromSeller", user, message);
            }
        }
    }
}
