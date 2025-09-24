using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace EasyHttpsClient
{
    public class HttpPutClient : HttpBaseClient
    {
        private string _jsonBodyRequest = null;
        private List<KeyValuePair<string, string>> _postData;
        private FormUrlEncodedContent _formUrlEncodedContent;

        public void SetBodyPostData(List<KeyValuePair<string, string>> postData)
        {
            this._postData = postData;
        }

        public void AddBodyPostData(string key, string value)
        {
            if (this._postData == null)
            {
                this._postData = new List<KeyValuePair<string, string>>();
            }
            this._postData.Add(new KeyValuePair<string, string>(key, value));
        }

        public void SetBodyRequestData(object requestData)
        {
            this._jsonBodyRequest = JsonSerializer.Serialize(requestData);
        }

        public void SetBodyRequestJson(string requestJsonData)
        {
            this._jsonBodyRequest = requestJsonData;
        }

        public void SetBodyFormUrlEncodedContent(FormUrlEncodedContent formUrlEncodedContent)
        {
            this._formUrlEncodedContent = formUrlEncodedContent;
        }

        public override async Task<HttpBaseClient> SendAsync()
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
                    _httpResponseMessage = await _httpClient.PutAsync(_url, content, _cancellationToken);
                    _httpResponseMessage.EnsureSuccessStatusCode();
                }
                else if (_postData != null)
                {
                    FormUrlEncodedContent content = new FormUrlEncodedContent(_postData);
                    _httpResponseMessage = await _httpClient.PutAsync(_url, content, _cancellationToken);
                    _httpResponseMessage.EnsureSuccessStatusCode();
                }
                else if (_formUrlEncodedContent != null)
                {
                    _httpResponseMessage = await _httpClient.PutAsync(_url, _formUrlEncodedContent, _cancellationToken);
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
                throw new HttpRequestException("HttpRequestException in PUT SendAsync", ex);
            }
            catch (TimeoutException ex)
            {
                throw new TimeoutException("TimeoutException in PUT SendAsync", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("PUT SendAsync", ex);
            }
        }
    }
}
