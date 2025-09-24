using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace EasyHttpClient
{
    public abstract class HttpBaseClient
    {
        protected readonly HttpClient _httpClient;
        protected readonly HttpClientHandler _handler;
        protected string _url;
        protected TimeSpan _timeout;
        protected CancellationToken _cancellationToken;
        protected Encoding _encoding = Encoding.UTF8;
        protected HttpResponseMessage _httpResponseMessage;

        public HttpBaseClient()
        {
            _handler = new HttpClientHandler();
            _httpClient = new HttpClient(_handler);
            _timeout = TimeSpan.FromMinutes(1);
            _cancellationToken = CancellationToken.None;
            _handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
        }

        public void SetUrl(string url)
        {
            this._url = url;
        }

        public void AddToRequestHeaders(string key, string value)
        {
            _httpClient.DefaultRequestHeaders.Add(key, value);
        }

        public void SetTimeout(int seconds)
        {
            this._timeout = TimeSpan.FromSeconds(seconds);
        }

        public void SetTimeout(TimeSpan timeout)
        {
            this._timeout = timeout;
        }

        public void SetEncoding(Encoding encoding)
        {
            this._encoding = encoding;
        }

        public void SetCancellationToken(CancellationToken cancellationToken)
        {
            this._cancellationToken = cancellationToken;
        }

        public HttpResponseMessage GetHttpResponseMessage()
        {
            return this._httpResponseMessage;
        }

        public abstract Task<HttpBaseClient> SendAsync();

        public async Task<byte[]> ReadResultAsByteArrayAsync()
        {
            try
            {
                HttpContent content = _httpResponseMessage.Content;
                byte[] resultByteArray = await content.ReadAsByteArrayAsync();
                return resultByteArray;
            }
            catch
            {
                throw;
            }
        }

        public async Task<string> ReadResultAsStringAsync()
        {
            try
            {
                HttpContent content = _httpResponseMessage.Content;
                byte[] resultByteArray = await content.ReadAsByteArrayAsync();
                string resultString = _encoding.GetString(resultByteArray);
                return resultString;
            }
            catch
            {
                throw;
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
            _handler?.Dispose();
        }
    }
}
