using Library.Common;
using MudBlazor;
using WebClient.IServices;

namespace WebClient.Services
{
    public class SendMailService : ISendMailService
    {
        public SendMailService(HttpClient httpClient, ISnackbar snackBar)
        {
            _httpClient = httpClient;
            SnackBar = snackBar;
        }

        private HttpClient _httpClient { get; }

        private ISnackbar SnackBar { get; }


        public async Task<RequestResponse> SendMail(MailModel mail)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"api/SendMail/SendMail",mail);

            var requestResponse = await response.Content.ReadFromJsonAsync<RequestResponse>();

            if (!requestResponse.IsSuccessful)
            {
                SnackBar.Add(requestResponse.Message, Severity.Error);
            } else
            {
                SnackBar.Add(requestResponse.Message, Severity.Success);
            }

            return requestResponse;
        }
    }
}
