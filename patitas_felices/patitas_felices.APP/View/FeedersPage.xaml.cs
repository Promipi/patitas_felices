using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls.Xaml;
using patitas_felices.Common.Models.Feeder;

namespace patitas_felices.APP.View
{
    
    public partial class FeedersPage : ContentPage
    {

        private readonly FeedersViewModel _vm;

        public FeedersPage(FeedersViewModel feedersViewModel)
        {
            InitializeComponent();
            BindingContext = _vm = feedersViewModel;
            LoadAfterConstruction();
        }

        private async void LoadAfterConstruction()
        {
            _vm.IsBusy = true;
            await _vm.LoadAsync();
            _vm.IsBusy = false;
        }

      
    }
}