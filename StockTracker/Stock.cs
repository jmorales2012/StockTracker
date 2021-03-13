using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp.Serialization;
using RestSharp.Deserializers;
using RestSharp.Serializers.Newtonsoft.Json;
using System.Linq;
using System.IO;

namespace StockTracker
{
    /*
     * Stock Class
     *  - The stock class outlines a stock object to be used in the watchlists.
     * 
     * Properties
     *  - Symbol: the stock's symbol (ex: FB, MSFT, GOOG, AAPL)
     *  - Name: the name of the company (ex: Facebook, Microsoft)
     *  - Close: the price the stock last closed at (daily)
     *  
     *  Methods
     * - UpdatePrice(): updates price of the stock to most recent
     */
    public class Stock
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public double Close { get; set; }

        public Stock(string symbol, string name, string close)
        {
            Symbol = symbol;
            Name = name.Substring(0, 9);
            Close = Convert.ToDouble(close);      // closing price of the stock
        }

        public void UpdatePrice()
        {
            /*
             * Get the latest price for the stock.
             */
            // Setup new client and request the JSON object from the API
            var client = new RestClient($"https://api.twelvedata.com/price?symbol={this.Symbol}&apikey=50445cceeb9540708d2f06e6db3988ff");
            var request = new RestSharp.RestRequest();
            var response = client.Get(request);

            if (response.IsSuccessful)
            {
                // use to convert response to regular object
                //var result = JsonConvert.DeserializeObject(response.Content);

                // create a new stock object from the returned JSON object
                //Stock stock = JsonConvert.DeserializeObject<Stock>(response.Content);
                var result = JObject.Parse(response.Content);

                this.Close = Convert.ToDouble(result["price"]);
            }
            else
            {
                Console.WriteLine("HTTP request failed.");
            }
        }
    }
}
