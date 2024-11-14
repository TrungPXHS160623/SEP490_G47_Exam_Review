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

        /// <summary>
        /// Initializes a new instance of the <see cref="SpinnerHandler"/> class.
        /// </summary>
        /// <param name="spinnerService">SpinnerService</param>
        public SpinnerHandler(SpinnerService spinnerService)
        {
            this.spinnerService = spinnerService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken = default)
        {
            this.spinnerService.Show();

            var token = Constants.JWTToken;

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            //await Task.Delay(100000); // artificial delay for testing
            await Task.Delay(50);
            var response = await base.SendAsync(request, cancellationToken);
            this.spinnerService.Hide();

            return response;
        }
    }
}
