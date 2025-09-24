using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EasyHttpsClient
{
    public class HttpDeleteClient : HttpBaseClient
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
                _httpResponseMessage = await _httpClient.DeleteAsync(_url, _cancellationToken);
                _httpResponseMessage.EnsureSuccessStatusCode();
                return this;
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("HttpRequestException in DELETE SendAsync", ex);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException("TimeoutException in DELETE SendAsync", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("DELETEasync", ex);
            }
        }
    }
}
