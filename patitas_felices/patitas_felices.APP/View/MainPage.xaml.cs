

namespace patitas_felices.APP
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(LoginFormViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

    }
}