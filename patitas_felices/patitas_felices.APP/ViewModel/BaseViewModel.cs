
using CommunityToolkit.Mvvm.ComponentModel;

namespace patitas_felices.APP.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor("IsNotBusy")]
        bool isBusy;

        [ObservableProperty]
        string title;


        public bool IsNotBusy => !IsBusy;

        public BaseViewModel()
        {

        }
    }

}
