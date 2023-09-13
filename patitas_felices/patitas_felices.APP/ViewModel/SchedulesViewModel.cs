using Microsoft.AspNetCore.SignalR.Client;
using patitas_felices.Common.Responses;
using patitas_felices.COMMON.Models.Schedule;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace patitas_felices.APP.ViewModel
{
    [QueryProperty("feederId", "feederId")]
    public partial class SchedulesViewModel : BaseViewModel
    {
        string feederId { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor("IsNotBusy")]
        string time;

        public ObservableCollection<Schedule> Schedules { get; set; } = new ObservableCollection<Schedule>();

        public SchedulesViewModel()
        {

        }

        [RelayCommand]
        public async Task AddSchedule()
        {
            var connection = new HubConnectionBuilder()
                             .WithUrl("http://192.168.1.191:5000/instructionHub")
                             .Build();

            await connection.StartAsync();
            await connection.SendAsync("SendMessage", $"addSchedule b8:27:eb:6e:c9:59 {Time} ");
            await connection.SendAsync("SendMessage", $"getSchedules b8:27:eb:6e:c9:59");
        }

        public async Task WaitInstructions(HubConnection connection)
        {
            connection.On<string>("ReceiveMessage", (string message) =>
            {
                if (message.Split(" ")[0] == "SchedulesReceived")
                {
                    Schedules.Clear();
                    if (message.Split(" ")[1] == "b8:27:eb:6e:c9:59")
                    {
                        //format this [('d0d03afd-ca81-4761-b865-05007502701f', 'b8:27:eb:6e:c9:59', '12:51'), ('1532e260-d4b0-45d4-9994-d8ab70593b3c', 'b8:27:eb:6e:c9:59', '13:20'), ('499a141b-f7c6-4df9-b4f2-6049869557d6', 'b8:27:eb:6e:c9:59', '20:40')]
                        var schedules = message.Replace(" ", "").Replace("[", "").Replace("]", "").Replace("(", "").Replace(")", "").Split(",");

                        for (int i = 0; i <= schedules.Length; i += 3)
                        {
                            var a = schedules.Skip(i).Take(3).ToList();
                            var newSchedule = new Schedule()
                            {
                                Id = a[0],
                                FeederId = a[1],
                                Time = a[2]
                            };

                            Schedules.Add(newSchedule);
                            Time = "";
                        }
                    }
                }
            });

            await Task.Delay(99999);
        }

        public async Task LoadAsync()
        {
            IsBusy = true;
            var connection = new HubConnectionBuilder()
                             .WithUrl("http://192.168.1.191:5000/instructionHub")
                             .Build();

            await connection.StartAsync();

            WaitInstructions(connection);

            await connection.SendAsync("SendMessage", $"getSchedules b8:27:eb:6e:c9:59");
            await Task.Delay(3000);
            //do stuff
            IsBusy = false;
        }
    }
}
