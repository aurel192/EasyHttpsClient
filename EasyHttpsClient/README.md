# EasyHttpsClient

## v1.2.1

>EasyHttpsClient is a simple and easy-to-use HTTP client library for .NET. It provides a fluent API for sending HTTP POST and GET requests and retrieving responses.   

###### Work In Progress
>DELETE and PUT methods are yet to be implemented!

---

[GitHub aurel192 - EasyHttpsClient repository](https://github.com/aurel192/EasyHttpsClient)

[NuGet Package - EasyHttpsClient](https://www.nuget.org/packages/EasyHttpsClient)

---

## Usage 

**GET Request To send a GET request, use the GETasync method:**

```csharp
var client = new HttpsClient();
client.SetUrl("https://example.com/api/data");
await client.GETasync();
var response = await client.ReadResultAsStringAsync();
Console.WriteLine(response);
```

---

**To send a POST request, use the POSTasync method:**

```csharp
var client = new HttpsClient();
client.SetUrl("https://example.com/api/PostHereFormUrlEncodedData/");
client.SetBodyPostData(new List<KeyValuePair<string, string>>
{
    new KeyValuePair<string, string>("key1", "value1"),
    new KeyValuePair<string, string>("key2", "value2")
});
await client.POSTasync();
var response = await client.ReadResultAsStringAsync();
Console.WriteLine(response);
```

---

**To send a POST request with a JSON body, use the SetBodyRequestData method**

```csharp
var client = new HttpsClient();
client.SetUrl("https://example.com/api/PostHereInPostBody/");
//example object, it can be any object that can be serialized
var data = new { key1 = "value1", key2 = "value2", ... }; 

client.SetBodyRequestData(data);
// or use client.SetBodyRequestJson(...);

await client.POSTasync();
var response = await client.ReadResultAsStringAsync();
Console.WriteLine(response);
```

---

**Setting Timeout and Encoding
You can set the timeout and encoding for the HTTP client using the SetTimeout and SetEncoding methods:**

```csharp
var client = new HttpsClient();
client.SetTimeout(10); // set timeout to 10 seconds
client.SetEncoding(Encoding.UTF8); // set encoding to UTF-8
```

---


**Adding Headers
You can add headers to the HTTP request using the AddToRequestHeaders method:**

```csharp
var client = new HttpsClient();
client.AddToRequestHeaders("Authorization", "Bearer token ...");
```

---

**Supports Fluent syntax**

```csharp
HttpsClient client = new EasyHttpsClient.HttpsClient();

await client
    .SetUrl("https://example.com/etc/GetIt.html")
    .SetTimeout(15)
    .SetEncoding(Encoding.UTF8)
    .GETasync();

byte[] bytearray = await client.ReadResultAsByteArrayAsync();
string response = await client.ReadResultAsStringAsync();
```

---

Disposing
Don't forget to dispose the HTTP client when you're done with it to free up memory:

```csharp
var client = new HttpsClient();
// ...
client.Dispose();
```

## License

This library is licensed under the MIT License. See the LICENSE file for details.

---

## Contributing

Contributions are welcome.  
Submit issues or pull requests on the [GitHub aurel192 - EasyHttpsClient repository](https://github.com/aurel192/EasyHttpsClient)

