# EasyHttpsClient

## v2.0.0

>EasyHttpsClient is a simple and easy-to-use HTTP client library for .NET, .NET Core and .NET Framework   
It provides an easier syntax for sending **GET, POST, PUT, DELETE** requests and retrieving response data.   

> *Work In Progress:*   
Different endpoint requires various requests.  
Feel Free to submit issues or pull requests on [GitHub aurel192 - EasyHttpsClient repository](https://github.com/aurel192/EasyHttpsClient) 

---

## Links

[GitHub aurel192 - EasyHttpsClient repository](https://github.com/aurel192/EasyHttpsClient)

[NuGet Package - EasyHttpsClient](https://www.nuget.org/packages/EasyHttpsClient)

---

## Usage 

---

**Setting / Adding  -  Headers, Timeout, Encoding, and more...**

```csharp
client.AddToRequestHeaders("Authorization", "Bearer token / ApiKey / etc ...");
client.SetTimeout(10);
client.SetEncoding(Encoding.UTF8);
```

**GET:**

```csharp
var getClient = new HttpGetClient();
getClient.SetUrl($"{BaseUrl}/api/TestItems/");   

// It will just send the request
await getClient.SendAsync();

// This line will read the result as string
string responseString = await getClient.ReadResultAsStringAsync();

// or byte array
byte[] responseByteArray = await getClient.ReadResultAsByteArrayAsync();

Console.WriteLine(response);

// You can access the raw HttpResponseMessage if you need it for some reason
HttpResponseMessage httpResponseMessage = getClient.GetHttpResponseMessage();
```

---

**POST:**

```csharp
var postClient = new HttpPostClient();
postClient.SetTimeout(10);
postClient.SetEncoding(Encoding.UTF8);

// POST FormUrlEncodedContent
postClient.SetUrl("https://www.example.com/api/PostKeyValuePairsHere/");
postClient.AddBodyPostData("key1", "value1");
postClient.AddBodyPostData("key2", "value2");

// POST StringContent
var payloadObject = new
{
    key = "NEW ITEM KEY",
    value = "NEW NEW",
    value2 = "BRAND NEW TEST ITEM",
    boolean = true,
};   

// Note: it can be any serializable object
postClient.SetBodyRequestData(payloadObject);   

// Or as Json string
// Note: it can be any format (e.g. XML, ...) depending on the endpoint
string payloadJson = @"{""key"": ""NEW ITEM KEY"",""value"": ""NEW NEW"",""value2"": ""BRAND NEW TEST ITEM"",""boolean"": true}";    

postClient.SetBodyRequestJson(payloadJson);

await postClient.SendAsync();

string responseString = await postClient.ReadResultAsStringAsync();
```

---

**PUT**

```csharp
var putClient = new HttpPutClient();
putClient.SetUrl($"{BaseUrl}/api/TestItems/1");

var modifiedObject = new
{
    key = "THIS IS NOT THE ORIGINAL",
    value = "IT WILL MODIFY",
    value2 = "MODIFY THE FIRST ELEMENT",
    boolean = true,
};   

putClient.SetBodyRequestData(payloadObject);   

await putClient.SendAsync();   

string responseString = await putClient.ReadResultAsStringAsync();

```

---

**DELETE**

```csharp
var deleteClient = new HttpDeleteClient();   

deleteClient.SetUrl($"{BaseUrl}/api/TestItems/15");   

await deleteClient.SendAsync();   

string responseString = await deleteClient.ReadResultAsStringAsync();   
```

---

## License

This library is licensed under the MIT License. See the LICENSE file for details.

---

## Contributing

Contributions are welcome.  
Submit issues or pull requests on [GitHub aurel192 - EasyHttpsClient repository](https://github.com/aurel192/EasyHttpsClient)

