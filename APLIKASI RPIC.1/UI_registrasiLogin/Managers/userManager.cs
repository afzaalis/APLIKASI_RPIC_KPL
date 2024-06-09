using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_registrasiLogin.Managers
{
    public class userManager<T>
    {
        private readonly HttpClient httpClient;

        public userManager()
        {
            httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5000/api/") };
        }

        public async Task<bool> RegisterUser(T newUser)
        {
            var json = JsonSerializer.Serialize(newUser);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"users/register", content);
            return await HandleResponse(response);
        }

        public async Task<T> AuthenticateUser(string username, string password)
        {
            var loginUser = new { Username = username, Password = password };
            var json = JsonSerializer.Serialize(loginUser);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync($"users/authenticate", content);
            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(responseData);
            }
            else
            {
                MessageBox.Show(await response.Content.ReadAsStringAsync());
                return default; 
            }
        }

        private async Task<bool> HandleResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show(await response.Content.ReadAsStringAsync());
                return true;
            }
            else
            {
                MessageBox.Show(await response.Content.ReadAsStringAsync());
                return false;
            }
        }
    }
}
