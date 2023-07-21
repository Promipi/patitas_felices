namespace patitas_felices.APP.View;

public partial class FeederDetailsPage : ContentPage
{
	public FeederDetailsPage(FeederDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}