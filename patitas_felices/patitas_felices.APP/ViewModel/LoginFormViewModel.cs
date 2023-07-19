

using patitas_felices.APP;
using patitas_felices.APP.View;
using patitas_felices.Common.Models.User.Auth;
using patitas_felices.Common.Responses;
using System.Net.Http.Json;

namespace patitas_felices.APP.ViewModel
{
    public partial class LoginFormViewModel : BaseViewModel
    {

        [ObservableProperty]
        string email;

        [ObservableProperty]
        string password;



        public LoginFormViewModel()
        {
           
        }

        [RelayCommand]
        async Task Login()
        {
            var url = $"{StaticData.ConnectionApi}";
            HttpClient client = new HttpClient();
            var loginDto = new LoginDto() { Email= email, Password = password };
            var resultLogin = await client.PostAsJsonAsync<LoginDto>($"{url}/api/users/LogIn", loginDto); //send the petition to login in our API
            var result = await resultLogin.Content.ReadFromJsonAsync<GetResponseDto<TokenInfo>>();
            if(result.Success == true)
            {
                StaticData.TokenUser = result.Content;
                await Shell.Current.GoToAsync($"{nameof(FeedersPage)}", true);
            }
            else
            {
                await Shell.Current.DisplayAlert("Error en el Login", result.Message, "Ok");
            }

        
        }


    }
}
