using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EasyHttpsClient
{
    public class HttpGetClient : HttpBaseClient
    {
        public override async Task<HttpBaseClient> SendAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(_url))
                {
                    throw new ApplicationException("Url is not set");
                }
                _httpClient.Timeout = _timeout;
                _httpResponseMessage = await _httpClient.GetAsync(_url, _cancellationToken);
                _httpResponseMessage.EnsureSuccessStatusCode();
                return this;
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("HttpRequestException in GET SendAsync", ex);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException("TimeoutException in GET SendAsync", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("GET SendAsync", ex);
            }
        }
    }
}
