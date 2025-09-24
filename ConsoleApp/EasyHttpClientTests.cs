using EasyHttpClient;
using System.Text;
using System.Text.Json;

namespace ConsoleApp
{
    public static class EasyHttpClientTests
    {
        private static string BaseUrl = "https://localhost:64946";

        public async static Task TryIt()
        {
            Console.WriteLine("============================ GET ============================");
            string responseGet = await EasyHttpClientTests.GET_TEST();
            Console.WriteLine($"GET response:\n{responseGet}\n\nPress any key!");
            Console.ReadKey();

            //Console.WriteLine("============================ POST(FormUrlEncoded) =============");
            //string responsePostFormUrlEncoded = await EasyHttpClientTests.POST_BodyFormUrlEncoded_TEST();
            //Console.WriteLine($"POST (FormUrlEncoded) response:\n{responsePostFormUrlEncoded}\n\nPress any key!");
            //Console.ReadKey();

            Console.WriteLine("============================ POST(BODY JSON OBJECT) ==========");
            string responsePostBodyRaw = await EasyHttpClientTests.POST_Body_Raw_TEST();
            Console.WriteLine($"POST (BODY JSON OBJECT) response:\n{responsePostBodyRaw}\n\nPress any key!");
            Console.ReadKey();

            Console.WriteLine("============================ PUT ============================");
            string responsePut = await EasyHttpClientTests.PUT_TEST();
            Console.WriteLine($"PUT response:\n{responsePut}\n\nPress any key!");
            Console.ReadKey();

            Console.WriteLine("============================ GET ============================");
            responseGet = await EasyHttpClientTests.GET_TEST();
            Console.WriteLine($"GET response:\n{responseGet}\n\nPress any key!");
            Console.ReadKey();

            Console.WriteLine("============================ DELETE ============================");
            string responseDelete = await EasyHttpClientTests.DELETE_TEST();
            Console.WriteLine($"DELETE response:\n{responseDelete}\n\nPress any key!");
            Console.ReadKey();
        }

        private async static Task<string> GET_TEST()
        {
            try
            {
                HttpGetClient getClient = new HttpGetClient();
                getClient.SetUrl($"{BaseUrl}/api/TestItems/");
                getClient.SetTimeout(10);
                getClient.SetEncoding(Encoding.UTF8);
                HttpBaseClient baseClient = await getClient.SendAsync();

                byte[] responseByteArray = await getClient.ReadResultAsByteArrayAsync();
                string responseString = await getClient.ReadResultAsStringAsync();

                HttpResponseMessage httpResponseMessage = getClient.GetHttpResponseMessage();

                responseString = IndentJson(responseString);
                getClient.Dispose();
                return responseString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async static Task<string> POST_BodyFormUrlEncoded_TEST()
        {
            try
            {
                HttpPostClient postClient = new HttpPostClient();
                postClient.SetUrl("https://www.example.com/api/PostKeyValuePairsHere/");
                postClient.SetTimeout(10);
                postClient.SetEncoding(Encoding.UTF8);
                postClient.AddBodyPostData("key1", "value1");
                postClient.AddBodyPostData("key2", "value2");
                HttpBaseClient baseclient = await postClient.SendAsync();
                byte[] responseByteArray = await postClient.ReadResultAsByteArrayAsync();
                string responseString = await postClient.ReadResultAsStringAsync();
                responseString = IndentJson(responseString);
                baseclient.Dispose();
                return responseString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async static Task<string> POST_Body_Raw_TEST()
        {
            try
            {
                string payloadJson = @"{""key"": ""NEW ITEM KEY"",""value"": ""NEW NEW"",""value2"": ""BRAND NEW"",""boolean"": true}";

                var payloadObject = new
                {
                    key = "NEW ITEM KEY",
                    value = "NEW NEW",
                    value2 = "BRAND NEW",
                    boolean = true,
                };

                HttpPostClient postClient = new HttpPostClient();
                postClient.SetUrl($"{BaseUrl}/api/TestItems/");
                postClient.SetTimeout(10);
                postClient.SetEncoding(Encoding.UTF8);

                // postClient.SetBodyRequestJson(payloadJson);
                // OR
                postClient.SetBodyRequestData(payloadObject);

                HttpBaseClient baseclient = await postClient.SendAsync();
                byte[] responseByteArray = await postClient.ReadResultAsByteArrayAsync();
                string responseString = await postClient.ReadResultAsStringAsync();
                responseString = IndentJson(responseString);
                baseclient.Dispose();
                return responseString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async static Task<string> PUT_TEST()
        {
            try
            {
                HttpPutClient putClient = new HttpPutClient();
                putClient.SetUrl($"{BaseUrl}/api/TestItems/1");
                putClient.SetTimeout(10);
                putClient.SetEncoding(Encoding.UTF8);

                var payloadObject = new
                {
                    key = "111 11111",
                    value = "11",
                    value2 = "id=1 !!! FIRST ENTRY !!!",
                    boolean = true,
                };

                putClient.SetBodyRequestData(payloadObject);

                HttpBaseClient baseclient = await putClient.SendAsync();
                byte[] responseByteArray = await putClient.ReadResultAsByteArrayAsync();
                string responseString = await putClient.ReadResultAsStringAsync();
                responseString = IndentJson(responseString);
                baseclient.Dispose();
                return responseString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async static Task<string> DELETE_TEST()
        {
            try
            {
                HttpDeleteClient deleteClient = new HttpDeleteClient();
                deleteClient.SetUrl($"{BaseUrl}/api/TestItems/15");
                deleteClient.SetTimeout(10);
                deleteClient.SetEncoding(Encoding.UTF8);

                HttpBaseClient baseclient = await deleteClient.SendAsync();
                byte[] responseByteArray = await deleteClient.ReadResultAsByteArrayAsync();
                string responseString = await deleteClient.ReadResultAsStringAsync();
                responseString = IndentJson(responseString);
                baseclient.Dispose();
                return responseString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string IndentJson(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            try
            {
                using (JsonDocument doc = JsonDocument.Parse(str))
                {
                    return JsonSerializer.Serialize(doc.RootElement, new JsonSerializerOptions { WriteIndented = true });
                }
            }
            catch
            {
                return str;
            }
        }
    }
}
