namespace patitas_felices.APP.View;

public partial class SchedulesPage : ContentPage
{
    public SchedulesViewModel _vm;
    public SchedulesPage(SchedulesViewModel schedulesViewModel)
	{
		InitializeComponent();
        BindingContext = _vm = schedulesViewModel;
        LoadAfterConstruction();
    }

    private async void LoadAfterConstruction()
    {
        _vm.IsBusy = true;
        await _vm.LoadAsync();
        _vm.IsBusy = false;
    }
}
