using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Text.Json;

namespace EasyHttpsClient
{
    public class HttpsClient
    {
        private readonly HttpClient _httpClient;
        private readonly HttpClientHandler _handler;
        private string _url;
        private TimeSpan _timeout;
        private string _jsonBodyRequest = null;
        private List<KeyValuePair<string, string>> _postData;
        private FormUrlEncodedContent _formUrlEncodedContent;
        private HttpResponseMessage _httpResponseMessage;
        private CancellationToken _cancellationToken;
        private Encoding _encoding = Encoding.UTF8;

        public HttpsClient()
        {
            _handler = new HttpClientHandler();
            _httpClient = new HttpClient(_handler);
            _timeout = TimeSpan.FromMinutes(1);
            _cancellationToken = CancellationToken.None;
            _handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;
        }

        #region SET and ADD Methods
        public HttpsClient SetUrl(string url)
        {
            this._url = url;
            return this;
        }

        public HttpsClient AddToRequestHeaders(string key, string value)
        {
            _httpClient.DefaultRequestHeaders.Add(key, value);
            return this;
        }

        public HttpsClient SetBodyPostData(List<KeyValuePair<string, string>> postData)
        {
            this._postData = postData;
            return this;
        }

        public HttpsClient AddBodyPostData(string key, string value)
        {
            if (this._postData == null)
            {
                this._postData = new List<KeyValuePair<string, string>>();
            }
            this._postData.Add(new KeyValuePair<string, string>(key, value));
            return this;
        }

        public HttpsClient SetBodyRequestData(object requestData)
        {
            this._jsonBodyRequest = JsonSerializer.Serialize(requestData);
            return this;
        }

        public HttpsClient SetBodyRequestJson(string requestJsonData)
        {
            this._jsonBodyRequest = requestJsonData;
            return this;
        }

        public HttpsClient SetBodyFormUrlEncodedContent(FormUrlEncodedContent formUrlEncodedContent)
        {
            this._formUrlEncodedContent = formUrlEncodedContent;
            return this;
        }

        public HttpsClient SetTimeout(int seconds)
        {
            this._timeout = TimeSpan.FromSeconds(seconds);
            return this;
        }

        public HttpsClient SetTimeout(TimeSpan timeout)
        {
            this._timeout = timeout;
            return this;
        }

        public HttpsClient SetEncoding(Encoding encoding)
        {
            this._encoding = encoding;
            return this;
        }

        public HttpsClient SetCancellationToken(CancellationToken cancellationToken)
        {
            this._cancellationToken = cancellationToken;
            return this;
        }
        #endregion

        #region GET and POST
        public async Task<HttpsClient> GETasync()
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
                throw new HttpRequestException("HttpRequestException in GETasync", ex);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException("TimeoutException in GETasync", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("GETasync", ex);
            }
        }

        public async Task<HttpsClient> POSTasync()
        {
            try
            {
                if (string.IsNullOrEmpty(_url))
                {
                    throw new ApplicationException("Url is not set");
                }

                _httpClient.Timeout = _timeout;

                if (_jsonBodyRequest != null)
                {
                    StringContent content = new StringContent(_jsonBodyRequest, _encoding, "application/json");
                    _httpResponseMessage = await _httpClient.PostAsync(_url, content, _cancellationToken);
                    _httpResponseMessage.EnsureSuccessStatusCode();
                }
                else if (_postData != null)
                {
                    FormUrlEncodedContent content = new FormUrlEncodedContent(_postData);
                    _httpResponseMessage = await _httpClient.PostAsync(_url, content, _cancellationToken);
                    _httpResponseMessage.EnsureSuccessStatusCode();
                }
                else if (_formUrlEncodedContent != null)
                {
                    _httpResponseMessage = await _httpClient.PostAsync(_url, _formUrlEncodedContent, _cancellationToken);
                    _httpResponseMessage.EnsureSuccessStatusCode();
                }
                else
                {
                    throw new ApplicationException("No Post Data Set");
                }
                return this;
            }
            catch (HttpRequestException ex)
            {
                throw new HttpRequestException("HttpRequestException in POSTasync", ex);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException("TimeoutException in POSTasync", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("POSTasync", ex);
            }
        }
        #endregion

        #region Retrive Result
        public async Task<byte[]> ReadResultAsByteArrayAsync()
        {
            HttpContent content = _httpResponseMessage.Content;
            byte[] resultByteArray = await content.ReadAsByteArrayAsync();
            return resultByteArray;
        }

        public async Task<string> ReadResultAsStringAsync()
        {
            byte[] resultByteArray = await ReadResultAsByteArrayAsync();
            string resultString = _encoding.GetString(resultByteArray);
            return resultString;
        }
        #endregion

        public void Dispose()
        {
            _httpClient?.Dispose();
            _handler?.Dispose();
        }

    }
}
