using Newtonsoft.Json;
using OrderManagement.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace OrderManagement.Models
{
    public class DataService : IDataService
    {
        private string connectionString;
        public DataService()
        {
           connectionString = ConfigurationManager.AppSettings["DataServiceUri"];
        }
        public async Task <ObservableCollection<ProductModel>> GetProducts()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(connectionString);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method  
                HttpResponseMessage response = await client.GetAsync("GetProducts");
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var products =JsonConvert.DeserializeObject<ObservableCollection<ProductModel>>(jsonString);
                    return products;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    throw new ApplicationException(error);
                }
            }
        }

        public async Task<Tuple<bool, string>> PlaceOrder(ObservableCollection<OrdersModel> orders)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(connectionString);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var json = JsonConvert.SerializeObject(orders);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("PlaceOrder", content);
                var contentResponse = await response.Content.ReadAsStringAsync();
                return new Tuple<bool, string>(response.StatusCode == HttpStatusCode.OK, contentResponse);
            }
        }
    }
}
