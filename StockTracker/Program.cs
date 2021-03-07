using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using RestSharp.Serialization;
using RestSharp.Deserializers;
using RestSharp.Serializers.Newtonsoft.Json;
using System.IO;
using System.Linq;

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
         * API Usage
         *  This program uses the twelvedata API to retrieve price quotes.
         *  API Key: 50445cceeb9540708d2f06e6db3988ff
         *  
         * Todo:
         * [X] Allow user to input stock symbols into watchlist
         * [] Handle duplicate entries into list
         * [] Separate AddStock into GetStock and AddStock
         * [X] Delete stock from watchlist
         * [X] Sort watchlist (by symbol or by price)
         * [X] Save watchlist to file (streamwriter)
         * [X] Load watchlist from file (streamreader)
         * [] Update prices (use ?price query to API)
        */

        static void Main(string[] args)
        {
            // create a watchlist object to hold stocks
            Watchlist watchlist = new Watchlist("Watchlist");

            // Create a stock object and add it to the watchlist
            //AddStock(watchlist, "AAPL");
            // Display watchlist stocks
            //watchlist.DisplayStocks();

            // Create sample list for testing
            AddStock(watchlist, "AAPL");
            AddStock(watchlist, "TSLA");
            AddStock(watchlist, "FB");
            AddStock(watchlist, "AMZN");

            //// Ask user to input stock
            //AddStock(watchlist, GetUserAdd());
            //watchlist.DisplayStocks();

            //// Ask user to delete stock
            //watchlist.DeleteStock(GetUserDelete());
            //watchlist.DisplayStocks();

            // Sort the watchlist
            //watchlist.SortListAlpha();
            //watchlist.DisplayStocks();
            //watchlist.SaveList();

            // Load a watchlist
            //Watchlist loaded = LoadWatchList(GetUserList());
            //loaded.DisplayStocks();

            DisplayMenu();
        }

        public static int DisplayMenu()
        {
            /*
             * Display app menu and ask for user selection.
             * Ensure input is a valid selection.
             */
            int userInput = 0;

            while (userInput < 1 || userInput > 5)
            {
                //Console.Clear();
                Console.WriteLine("Welcome to Stock Tracker. Keep track of all your stocks right here!\n");
                Console.WriteLine("Menu");
                Console.WriteLine("1. Create a watchlist");
                Console.WriteLine("2. Add a stock to your watchlist");
                Console.WriteLine("3. Delete a stock from your watchlist");
                Console.WriteLine("4. Save your watchlist");
                Console.WriteLine("5. Delete your watchlist");

                userInput = Convert.ToInt32(Console.ReadLine());
                if (userInput < 1 || userInput > 5)
                {
                    Console.WriteLine("Please enter a valid selection.");
                }
            }

            return userInput;
        }

        public static string GetUserAdd()
        {
            Console.Write("Enter a stock symbol to add to your watchlist: ");
            return Console.ReadLine();
        }

        public static string GetUserDelete()
        {
            Console.Write("Enter a stock symbol to delete from your watchlist: ");
            return Console.ReadLine();
        }

        public static string GetUserList()
        {
            Console.Write("Enter a watchlist to load: ");
            return Console.ReadLine();
        }

        public static Watchlist LoadWatchList(string list)
        {
            /*
             * Load a given watchlist. 
             * 1: Look for the file. If not found, error.
             * 2: If found, create a watchlist with the given name.
             * 3: Read the file, each line is a new stock. 
             * 4: Split each line by commas to get stock properties.
             * 5: Create each stock and add to the watchlist.
             * 6: Return the watchlist.
             */

            try
            {
                string line;
                TextReader reader = new StreamReader(list);
                Watchlist watchlist = new Watchlist(list);

                while (true)
                {
                    line = reader.ReadLine();
                    if (line == null) break;

                    // insert logic to split the line and create a stock
                    string[] stockProps = line.Split(',');
                    Stock stock = new Stock(stockProps[0], stockProps[1], stockProps[2]);
                    watchlist.AddStock(stock);
                }
                reader.Close();

                return watchlist;
            }
            catch
            {
                Console.WriteLine("Watchlist not found. Creating one now.");
                return new Watchlist("Watchlist");
            }
            
        }

        /*
         * Uncomment below method for production. Using dev method so I don't 
         * go over on API requests.
         */
        //public static void AddStock(Watchlist watchlist, string symbol)
        //{
        //    /*
        //     * Gets the stock from the API and adds it to the watchlist. 
        //     * API allows 5 requests per minute or 500/day.
        //     * Separate into two methods: GetStock and AddStock
        //     */

        //    // Setup new client and request the JSON object from the API
        //    var client = new RestClient($"https://api.twelvedata.com/quote?symbol={symbol}&apikey=50445cceeb9540708d2f06e6db3988ff");
        //    var request = new RestSharp.RestRequest();
        //    var response = client.Get(request);

        //    if (response.IsSuccessful)
        //    {
        //        // create a new stock object from the returned JSON object
        //        Stock stock = JsonConvert.DeserializeObject<Stock>(response.Content);

        //        // add new stock to watchlist
        //        watchlist.AddStock(stock);
        //    }
        //    else
        //    {
        //        Console.WriteLine("Failed");
        //    }
        //}

        /*
         * Dev method below is only for dev use so you don't go over on API requests.
         */
        public static void AddStock(Watchlist watchlist, string symbol)
        {
            Stock stock = new Stock(symbol, "Test Name", "123.45");
            watchlist.AddStock(stock);
        }
    }
}
