using Library.Common;
using System.Net.Http.Headers;

namespace WebClient.Common
{
    /// <summary>
    /// SpinnerHandler
    /// </summary>
    public class SpinnerHandler : DelegatingHandler
    {
        private readonly SpinnerService spinnerService;
        private readonly LocalStorageService _localStorageService;


        /// <summary>
        /// Initializes a new instance of the <see cref="SpinnerHandler"/> class.
        /// </summary>
        /// <param name="spinnerService">SpinnerService</param>
        public SpinnerHandler(SpinnerService spinnerService, LocalStorageService localStorageService)
        {
            this.spinnerService = spinnerService;
            _localStorageService = localStorageService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
        {
            this.spinnerService.Show();

            var token = await _localStorageService.GetItemAsync("authToken");

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await base.SendAsync(request, cancellationToken);

            this.spinnerService.Hide();

            return response;
        }

    }
}
