using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using RestSharp.Serialization;
using RestSharp.Deserializers;
using RestSharp.Serializers.Newtonsoft.Json;

namespace StockTracker
{
    class Program
    {

        /*
         * This project will be a stock tracker app that connects to the exchange API
         * to provide real time data for the user. 
         * 
         * Objectives
         *  1. User can input a stock symbol to add to the list
         *  2. User can delete a stock 
         *  3. App will store stocks and save stocks for continued use
         *  4. App will display real time pricing for stocks in console
         * 
         * Data Structures
         *  1. Stock - Class
         *      - Symbol - String
         *      - Company Name - String
         *      - Current Price - Double
         *  2. Watchlist - Class
         *      - Name - String
         *      - Stocks - List
         *      - Add Stock - Method
         *      - Delete Stock - Method
         * 
         * Methods
         *  1. 
         *  
         * API Usage
         *  This program uses the twelvedata API to retrieve price quotes.
         *  API Key: 50445cceeb9540708d2f06e6db3988ff
         *  
         * Todo:
         * [X] Allow user to input stock symbols into watchlist
         * [] Separate AddStock into GetStock and AddStock
         * [] Delete stock from watchlist
         * [] Sort watchlist (by symbol or by price)
         * [] Save watchlist to file (streamwriter)
         * [] Load watchlist from file (streamreader)
         * [] Update prices (use ?price query to API)
        */

        static void Main(string[] args)
        {
            // create a watchlist object to hold stocks
            Watchlist watchlist = new Watchlist("My Watchlist");

            // Create a stock object and add it to the watchlist
            //AddStock(watchlist, "AAPL");
            // Display watchlist stocks
            //watchlist.DisplayStocks();

            // Ask user to input stock
            AddStock(watchlist, GetUserInput());
            watchlist.DisplayStocks();
        }


        public static string GetUserInput()
        {
            Console.Write("Enter a stock symbol to add to your watchlist: ");
            return Console.ReadLine();
        }


        public static void AddStock(Watchlist watchlist, string symbol)
        {
            /*
             * Gets the stock from the API and adds it to the watchlist. 
             * API allows 5 requests per minute or 500/day.
             * Separate into two methods: GetStock and AddStock
             */

            // Setup new client and request the JSON object from the API
            var client = new RestClient($"https://api.twelvedata.com/quote?symbol={symbol}&apikey=50445cceeb9540708d2f06e6db3988ff");
            var request = new RestSharp.RestRequest();
            var response = client.Get(request);

            if (response.IsSuccessful)
            {
                // create a new stock object from the returned JSON object
                Stock stock = JsonConvert.DeserializeObject<Stock>(response.Content);

                // add new stock to watchlist
                watchlist.AddStock(stock);
            }
            else
            {
                Console.WriteLine("Failed");
            }
        }
    }
}
