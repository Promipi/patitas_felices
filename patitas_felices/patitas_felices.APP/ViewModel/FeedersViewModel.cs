using Microsoft.Maui.ApplicationModel.Communication;
using patitas_felices.APP.View;
using patitas_felices.Common.Models.Feeder;
using patitas_felices.Common.Models.User.Auth;
using patitas_felices.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace patitas_felices.APP.ViewModel
{
    public partial class FeedersViewModel : BaseViewModel
    {
        public ObservableCollection<Feeder> Feeders { get; set; } = new ObservableCollection<Feeder>();

        public FeedersViewModel()
        {

        }

        [RelayCommand]
        public async Task GoToDetails(Feeder feeder) //Go to details command
        {
            if (feeder is null) return;

            await Shell.Current.GoToAsync($"{nameof(FeederDetailsPage)}", true,
                new Dictionary<string, object>
                {
                    {"Feeder",feeder}
                });
        }

        public async Task LoadAsync()
        {
            IsBusy = true;
            //do the task
            var url = $"{StaticData.ConnectionApi}";
            HttpClient client = new HttpClient();
            var result = await client.GetFromJsonAsync<GetResponseDto<DataCollection<Feeder>>>($"{url}/api/Feeders?page=1&take=10&userId=cbf57565-e55f-410e-94fe-3fee4d74b38e"); //send the petition to get feeders

            if (result.Success == true)
            {
                foreach(var f in result.Content.Items)
                {
                    Feeders.Add(f);
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Error en el request", result.Message, "Ok");
            }


            IsBusy = false;
        }
    }
}
