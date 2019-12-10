using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;


namespace NGK_WeatherMeasurements.Hub
{
    public class MeasurementHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task SendMessage()
        {
            await Clients.All.SendAsync("SendMeasurement");
        }
    }
}
