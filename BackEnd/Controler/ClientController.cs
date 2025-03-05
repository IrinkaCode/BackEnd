using BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BackEnd.Controler
{
    class ClientController
    {
        public static async void AddClient(string json, HttpListenerContext context)
        {
            using (PostgresContext db = new PostgresContext())
            {
                Client? person = JsonSerializer.Deserialize<Client>(json);
                db.Clients.Add(new Client()
                {
                    City = person!.City,
                    Company = person.Company,
                    Firstname = person.Firstname,
                    Phone = person.Phone,
                    Lastname = person.Lastname,
                    Surname = person.Surname
                });
                await db.SaveChangesAsync();
                var response = context.Response;

                string responseText = "OK";
                byte[] buffer = Encoding.UTF8.GetBytes(responseText);
                response.ContentLength64 = buffer.Length;
                response.ContentType = "text/html";
                response.ContentEncoding = Encoding.UTF8;
                response.StatusCode = 200;
                using Stream output = response.OutputStream;
                await output.WriteAsync(buffer);
                await output.FlushAsync();
                Console.WriteLine("Запрос обработан");
            }
        }
}
