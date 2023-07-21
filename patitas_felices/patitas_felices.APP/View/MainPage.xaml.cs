

namespace patitas_felices.APP
{
    public partial class MainPage : ContentPage
    {

        public MainPage(LoginFormViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;

        }

    }
}