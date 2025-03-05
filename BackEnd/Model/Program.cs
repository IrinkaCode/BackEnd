using BackEnd.Controler;
using System.Net;
using System.Text;

HttpClient httpClient = new HttpClient();
HttpListener server = new HttpListener();
server.Prefixes.Add("http://127.0.0.1:8888/connection/");
server.Start();
while (true)
{
    var context = await server.GetContextAsync();
    var body = context.Request.InputStream;
    var method = context.Request.HttpMethod;
    var encoding = context.Request.ContentEncoding;
    var reader = new StreamReader(body, encoding);
    string query = reader.ReadToEnd();
    Console.WriteLine(query);
    string table = context.Request.Headers[0]!;
    if (method == "POST")
    {
        switch (table)
        {
            case "client": ClientController.AddClient(query, context); break;
        }
    }
    else if (method == "GET")
    {
        switch (query)
        {
            case "allClients":
                {
                    var response = context.Response;
                    string responseText =
                        @"<!DOCTYPE html>
                        <html>
                            <head>
                                <meta charset='utf8'>
                                <title>METANIT.COM</title>
                            </head>
                            <body>
                                <h2>Hello METANIT.COM</h2>
                            </body>
                        </html>";
                    byte[] buffer = Encoding.UTF8.GetBytes(responseText);
                    // получаем поток ответа и пишем в него ответ
                    response.ContentLength64 = buffer.Length;
                    using Stream output = response.OutputStream;
                    // отправляем данные
                    await output.WriteAsync(buffer);
                    await output.FlushAsync();
                    Console.WriteLine("Запрос обработан");
                }
                break;
        }
    }
    else if (method == "PUT")
    {

    }

}
server.Stop();