using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using RestSharp.Serialization;
using RestSharp.Deserializers;
using RestSharp.Serializers.Newtonsoft.Json;
using System.Linq;
using System.IO;

namespace StockTracker
{
    /*
     * Watchlist Class
     *  - The watchlist is what holds the stocks that the user requests. You can
     *    create a new watchlist or load an existing one from a file. You can 
     *    also add or remove stocks from it and sort the list (by symbol or price)
     * 
     * Properties
     *  - Name: the user-defined name of the list
     *  - Stocks: a list containing stocks (list instead of array since length
     *            of list is not known
     *            
     * Constructors
     *  - Watchlist()       -> creates a default watchlist
     *  - Watchlist(name)   -> creates a watchlist with user-defined name
     *            
     * Methods
     *  - DisplayStocks()   -> displays stocks in watchlist
     *  - AddStock(symbol)  -> requests stock info from API and adds to list
     *  - RemoveStock(symbl)-> removes stock from list
     *  - SortListAlpha()   -> sorts the list by symbol (a to z)
     *  - SortListPrice()   -> sorts the list by price (least to greatest)
     *  - SaveList()        -> save the list in a file for later use
     */
    public class Watchlist
    {
        public string Name { get; set; }
        public List<Stock> Stocks;


        public Watchlist()
        {
            Name = "default";
            Stocks = new List<Stock>();
        }


        public Watchlist(string name)
        {
            Name = name;
            Stocks = new List<Stock>();
        }


        public void DisplayStocks()
        {
            /*
             * Display the stocks held in the watchlist to the console.
             */
            Console.WriteLine($"\t{Name}\t\n");
            Console.WriteLine("Symbol\t  Name\t\tPrice");

            foreach (Stock stock in Stocks)
            {
                Console.Write($"{stock.Symbol}\t  ");
                Console.Write($"{stock.Name}\t");
                Console.WriteLine($"${Math.Round(Convert.ToDouble(stock.Close), 2)}");
            }
        }


        /*
         * Uncomment below method for production. 
         */
        public string AddStock(string symbol)
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
                // ensure response is a stock, and not an error
                if (response.Content.Contains("error"))
                {
                    return "Stock not found.";
                }
                else
                {
                    // use to convert response to regular object
                    //var result = JsonConvert.DeserializeObject(response.Content);

                    // create a new stock object from the returned JSON object
                    Stock stock = JsonConvert.DeserializeObject<Stock>(response.Content);

                    // add new stock to watchlist
                    Stocks.Add(stock);

                    return "Stock added!";
                }
            }
            else
            {
                return "HTTP request failed.";
            }
        }


        /* 
         * Uncomment below for testing. 
         */
        //public void AddStock(string symbol)
        //{
        //    /*
        //     * TEST USE ONLY
        //     * Allows dev to create test stocks without requesting from API (limited
        //     * to 5 request/min or 500/day on free version).
        //     * 
        //     * Gets info for the stock and adds it to the watchlist.
        //     */

        //    Stock stock = new Stock(symbol, "Test Name", "123.45");
        //    Stocks.Add(stock);
        //}


        public void AddStock(string symbol, string name, string price)
        {
            /*
             * Overloaded method used to add stocks to watchlist from a file.
             */

            Stock stock = new Stock(symbol, name, price);
            Stocks.Add(stock);
        }


        public string RemoveStock(string symbol)
        {
            /*
             * Searches for the given stock and removes it from the watchlist
             * if found. If not found, let user know stock doesn't exist in list.
             */

            var stock = Stocks.SingleOrDefault(s => s.Symbol == symbol.ToUpper());
            if (stock == null)
            {
                return "Stock not found.";
            }
            else
            {
                Stocks.Remove(stock);
                return "Stock removed!";
            }
        }


        public void SortListAlpha()
        {
            /*
             * Sorts the list alphabetically by stock symbols.
             */

            // use lambda expression for sorting, comparing symbols
            Stocks.Sort((x, y) => string.Compare(x.Symbol, y.Symbol));
        }


        public void SortListPrice()
        {
            /*
             * Sorts the list numerically by stock price.
             */

            //Stocks.Sort((x, y) => string.Compare(x.Close, y.Close));
            Stocks.Sort((x, y) => x.Close.CompareTo(y.Close));
        }


        public void SaveList()
        {
            /*
             * Save the list to a file. Each stock will be on its own line.
             * Each property in the stock is separated by a comma.
             */

            using (StreamWriter writer = new StreamWriter(Name))
            {
                // save watchlist name to top of file
                writer.Write($"{Name},");
                writer.WriteLine();

                foreach (Stock stock in Stocks)
                {
                    writer.Write($"{stock.Symbol},{stock.Name},{stock.Close}");
                    writer.WriteLine();
                }
            }
        }
    }
}
