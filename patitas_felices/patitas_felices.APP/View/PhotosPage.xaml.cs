
namespace patitas_felices.APP.View;

public partial class PhotosPage : ContentPage
{
    public PhotosViewModel _vm;

    public PhotosPage(PhotosViewModel phtotosViewModel)
	{
		InitializeComponent();
        BindingContext = _vm = phtotosViewModel;
        LoadAfterConstruction();

    }

    private async void LoadAfterConstruction()
    {
        _vm.IsBusy = true;
        await _vm.LoadAsync();

        _vm.IsBusy = false;
    }
}