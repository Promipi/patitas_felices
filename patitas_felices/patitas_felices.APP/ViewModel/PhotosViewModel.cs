using patitas_felices.Common.Models.Feeder;
using patitas_felices.Common.Models.Photo;
using patitas_felices.Common.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace patitas_felices.APP.ViewModel
{
    [QueryProperty("feederId", "feederId")]
    public class PhotosViewModel : BaseViewModel
    {
        string feederId { get; set; }

        public ObservableCollection<Photo> Photos { get; set; } = new ObservableCollection<Photo>();

        public PhotosViewModel() { 
            
        }

        public async Task LoadAsync()
        {
            IsBusy = true;
            //do the task
            var url = $"{StaticData.ConnectionApi}";
            HttpClient client = new HttpClient();
            var result = await client.GetFromJsonAsync<GetResponseDto<DataCollection<Photo>>>($"{url}"); //send the petition to get feeders

            if (result.Success == true)
            {
                foreach (var f in result.Content.Items)
                {
                    Photos.Add(f);
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
