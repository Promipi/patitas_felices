using patitas_felices.APP.View;
using patitas_felices.Common.Models.Feeder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace patitas_felices.APP.ViewModel
{
    [QueryProperty("Feeder", "Feeder")]
    public partial class FeederDetailsViewModel : BaseViewModel
    {

        [ObservableProperty]
        Feeder feeder;

        public FeederDetailsViewModel()
        {

        }

        [RelayCommand]
        public async Task GoToPhotos()
        {
            await Shell.Current.GoToAsync($"{nameof(PhotosPage)}", true,
               new Dictionary<string, object>
               {
                    {"feederId",Feeder.Id}
               });
        }

    }

}
