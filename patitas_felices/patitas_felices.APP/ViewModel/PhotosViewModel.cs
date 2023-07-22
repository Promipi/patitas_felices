using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public partial class PhotosViewModel : BaseViewModel
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
            GetResponseDto<DataCollection<Photo>> result = new GetResponseDto<DataCollection<Photo>>();
            try
            {
                result = await client.GetFromJsonAsync<GetResponseDto<DataCollection<Photo>>>($"{url}/api/Photos?page=1&take=10&feederId=b8%3A27%3Aeb%3A6e%3Ac9%3A59"); //send the petition to get feeders
                
            }
            catch ( Exception ex )
            {
                var e = ex;
            }
            

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
